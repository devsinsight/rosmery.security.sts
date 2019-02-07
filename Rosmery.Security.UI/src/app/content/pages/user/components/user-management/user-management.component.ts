import { Component, OnInit, ElementRef, ViewChild, ChangeDetectionStrategy } from '@angular/core';
// Material
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatSnackBar, MatDialog } from '@angular/material';
// RXJS
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { fromEvent, merge, forkJoin } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
// Services
import { UserService } from '../../services/user.service';
import { LayoutUtilsService, MessageType } from '../../services/layout-utils.service';
import { HttpUtilsService } from '../../services/http-utils.service';
// Models
import { QueryParamsModel } from '../../models/query-params.model';
import { UserModel } from '../../models/user.model';
import { UserDataSource } from '../../datasources/user.datasource';
// Components
import { UserEditDialogComponent } from '../user-edit/user-edit.dialog.component';
import { RoleService } from '../../services/role.service';
import { RoleModel } from '../../models/role.model';

@Component({
	selector: 'app-user-management',
	templateUrl: './user-management.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserManagementComponent implements OnInit {

	dataSource: UserDataSource;
	displayedColumns = ['select', 'lastName', 'firstName', 'email', 'phoneNumber', 'userName', 'roles', 'actions'];
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;

	@ViewChild('searchInput') searchInput: ElementRef;
	filterStatus: string = '';
	filterType: string = '';

	selection = new SelectionModel<UserModel>(true, []);
	usersResult: UserModel[] = [];
	rolesResult: RoleModel[] = [];

	constructor(
		private userService: UserService,
		private roleService: RoleService,
		public dialog: MatDialog,
		public snackBar: MatSnackBar,
		private layoutUtilsService: LayoutUtilsService,
		private translate: TranslateService
	) {}

	ngOnInit() {
		// If the user changes the sort order, reset back to the first page.
		this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

		/* Data load will be triggered in two cases:
		- when a pagination event occurs => this.paginator.page
		- when a sort event occurs => this.sort.sortChange
		**/
		merge(this.sort.sortChange, this.paginator.page)
			.pipe(
				tap(() => {
					this.loadUsersList();
				})
			)
			.subscribe();

		// Filtration, bind to searchInput
		fromEvent(this.searchInput.nativeElement, 'keyup')
			.pipe(
				// tslint:disable-next-line:max-line-length
				debounceTime(150), // The user can type quite quickly in the input box, and that could trigger a lot of server requests. With this operator, we are limiting the amount of server requests emitted to a maximum of one every 150ms
				distinctUntilChanged(), // This operator will eliminate duplicate values
				tap(() => {
					this.paginator.pageIndex = 0;
					this.loadUsersList();
				})
			)
			.subscribe();

		// Init DataSource
		const queryParams = new QueryParamsModel(this.filterConfiguration(false));
		this.dataSource = new UserDataSource(this.userService);
		// First load
		this.dataSource.loadUsers(queryParams);
		this.dataSource.entitySubject.subscribe(res => (this.usersResult = res));

		//Load List of Roles
		this.roleService.getAllRoles().subscribe( result => this.rolesResult = result );
	}

	loadUsersList() {
		this.selection.clear();
		const queryParams = new QueryParamsModel(
			this.filterConfiguration(true),
			this.sort.direction,
			this.sort.active,
			this.paginator.pageIndex,
			this.paginator.pageSize
		);
		this.dataSource.loadUsers(queryParams);
		this.selection.clear();
	}

	filterConfiguration(isGeneralSearch: boolean = true): any {
		const filter: any = {};
		const searchText: string = this.searchInput.nativeElement.value;

		filter.lastName = searchText;
		if (!isGeneralSearch) {
			return filter;
		}

		filter.firstName = searchText;
		filter.email = searchText;

		return filter;
	}

	deleteUser(_item: UserModel) {
		const _title: string = this.translate.instant('SECURITY.USERS.DELETE_USER_SIMPLE.TITLE');
		const _description: string = this.translate.instant('SECURITY.USERS.DELETE_USER_SIMPLE.DESCRIPTION');
		const _waitDesciption: string = this.translate.instant('SECURITY.USERS.DELETE_USER_SIMPLE.WAIT_DESCRIPTION');
		const _deleteMessage = this.translate.instant('SECURITY.USERS.DELETE_USER_SIMPLE.MESSAGE');
		
		const dialogRef = this.layoutUtilsService.deleteElement(_title, _description, _waitDesciption);
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}

			this.userService.deleteUser(_item.userId).subscribe(() => {
				this.layoutUtilsService.showActionNotification(_deleteMessage, MessageType.Delete);
				this.loadUsersList();
			});
		});
		
	}

	deleteUsers() {
		const _title: string = this.translate.instant('SECURITY.USERS.DELETE_USER_MULTY.TITLE');
		const _description: string = this.translate.instant('SECURITY.USERS.DELETE_USER_MULTY.DESCRIPTION');
		const _waitDesciption: string = this.translate.instant('SECURITY.USERS.DELETE_USER_MULTY.WAIT_DESCRIPTION');
		const _deleteMessage = this.translate.instant('SECURITY.USERS.DELETE_USER_MULTY.MESSAGE');
		
		const dialogRef = this.layoutUtilsService.deleteElement(_title, _description, _waitDesciption);
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}

			const idsForDeletion: string[] = [];
			for (let i = 0; i < this.selection.selected.length; i++) {
				idsForDeletion.push(this.selection.selected[i].userId);
			}
			this.userService
				.deleteUsers(idsForDeletion)
				.subscribe(() => {
					this.layoutUtilsService.showActionNotification(_deleteMessage, MessageType.Delete);
					this.loadUsersList();
					this.selection.clear();
				});
		});
		
	}

	fetchUsers() {
		const messages = [];
		this.selection.selected.forEach(elem => {
			messages.push({
				text: elem.firstName + ' ' + elem.lastName,
				id: elem.userId.toString(),
				
			});
		});
		this.layoutUtilsService.fetchElements(messages);
	}

	updateStatusForUsers() {
		const _title = this.translate.instant('SECURITY.USERS.UPDATE_STATUS.TITLE');
		const _updateMessage = this.translate.instant('SECURITY.USERS.UPDATE_STATUS.MESSAGE');
		const _statuses = [{ value: 0, text: 'Suspended' }, { value: 1, text: 'Active' }, { value: 2, text: 'Pending' }];
		const _messages = [];

		this.selection.selected.forEach(elem => {
			_messages.push({
				text: `${elem.lastName}, ${elem.firstName}`,
				id: elem.userId.toString(),
			});
		});
		
		const dialogRef = this.layoutUtilsService.updateStatusForUsers(_title, _statuses, _messages);
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				this.selection.clear();
				return;
			}

			this.userService
				.updateStatusForUser(this.selection.selected, res)
				.subscribe(() => {
					this.layoutUtilsService.showActionNotification(_updateMessage, MessageType.Update);
					this.loadUsersList();
					this.selection.clear();
				});
		});
		
	}

	addUser() {
		const newUser = new UserModel();
		newUser.clear();
		this.editUser(newUser);
	}

	editUser(user: UserModel) {
		let saveMessageTranslateParam = 'SECURITY.USERS.EDIT.';
		saveMessageTranslateParam += !!user.userId ? 'UPDATE_MESSAGE' : 'ADD_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !!user.userId ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(UserEditDialogComponent, { data: { user, roles: this.rolesResult } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}

			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType, 10000, true, false);
			this.loadUsersList();
		});
		
	}

	isAllSelected(): boolean {
		const numSelected = this.selection.selected.length;
		const numRows = this.usersResult.length;
		return numSelected === numRows;
	}

	masterToggle() {
		if (this.selection.selected.length === this.usersResult.length) {
			this.selection.clear();
		} else {
			this.usersResult.forEach(row => this.selection.select(row));
		}
	}

}
