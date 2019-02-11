import { Component, OnInit, ElementRef, ViewChild, ChangeDetectionStrategy } from '@angular/core';
// Material
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, MatSort, MatSnackBar, MatDialog } from '@angular/material';
// RXJS
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { fromEvent, merge, forkJoin } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
// Services
import { RoleService } from '../../services/role.service';
import { LayoutUtilsService, MessageType } from '../../services/layout-utils.service';

// Models
import { QueryParamsModel } from '../../models/query-params.model';
import { RoleModel } from '../../models/role.model';
import { RoleDataSource } from '../../datasources/role.datasource';
// Components
import { RoleEditDialogComponent } from '../role-edit/role-edit.dialog.component';


@Component({
	selector: 'app-role-management',
	templateUrl: './role-management.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class RoleManagementComponent implements OnInit {

	dataSource: RoleDataSource;
	displayedColumns = ['select', 'name', 'hasUsers', 'actions'];
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;

	@ViewChild('searchInput') searchInput: ElementRef;
	filterStatus: string = '';
	filterType: string = '';

	selection = new SelectionModel<RoleModel>(true, []);
	rolesResult: RoleModel[] = [];

	constructor(
		private roleService: RoleService,
		public dialog: MatDialog,
		public snackBar: MatSnackBar,
		private layoutUtilsService: LayoutUtilsService,
		private translate: TranslateService
	) {}

	ngOnInit() {

		this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

		merge(this.sort.sortChange, this.paginator.page)
			.pipe(
				tap(() => {
					this.loadRolesList();
				})
			)
			.subscribe();


		fromEvent(this.searchInput.nativeElement, 'keyup')
			.pipe(
				debounceTime(150), 
				distinctUntilChanged(), 
				tap(() => {
					this.paginator.pageIndex = 0;
					this.loadRolesList();
				})
			)
			.subscribe();

		// Init DataSource
		const queryParams = new QueryParamsModel(this.filterConfiguration(false));
		this.dataSource = new RoleDataSource(this.roleService);
		// First load
		this.dataSource.loadRoles(queryParams);
		this.dataSource.entitySubject.subscribe(res => (this.rolesResult = res));
	}

	loadRolesList() {
		this.selection.clear();
		const queryParams = new QueryParamsModel(
			this.filterConfiguration(true),
			this.sort.direction,
			this.sort.active,
			this.paginator.pageIndex,
			this.paginator.pageSize
		);
		this.dataSource.loadRoles(queryParams);
		this.selection.clear();
	}

	filterConfiguration(isGeneralSearch: boolean = true): any {
		const filter: any = {};
		const searchText: string = this.searchInput.nativeElement.value;


		filter.name = searchText;
		

		return filter;
	}

	deleteRole(_item: RoleModel) {
		const _title: string = this.translate.instant('SECURITY.ROLES.DELETE_ROLE_SIMPLE.TITLE');
		const _description: string = this.translate.instant('SECURITY.ROLES.DELETE_ROLE_SIMPLE.DESCRIPTION');
		const _waitDesciption: string = this.translate.instant('SECURITY.ROLES.DELETE_ROLE_SIMPLE.WAIT_DESCRIPTION');
		const _deleteMessage = this.translate.instant('SECURITY.ROLES.DELETE_ROLE_SIMPLE.MESSAGE');
		
		const dialogRef = this.layoutUtilsService.deleteElement(_title, _description, _waitDesciption);
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}

			this.roleService.deleteRole(_item.id).subscribe(() => {
				this.layoutUtilsService.showActionNotification(_deleteMessage, MessageType.Delete);
				this.loadRolesList();
			});
		});
		
	}

	fetchRoles() {
		const messages = [];
		this.selection.selected.forEach(elem => {
			messages.push({
				text: elem.name,
				id: elem.id,
				hasUsers: elem.hasUsers ? 'Has Users' : 'Empty'
			});
		});
		this.layoutUtilsService.fetchElements(messages);
	}

	addRole() {
		const newRole = new RoleModel();
		newRole.clear();
		this.editRole(newRole);
	}

	editRole(role: RoleModel) {
		let saveMessageTranslateParam = 'SECURITY.ROLES.EDIT.';
		saveMessageTranslateParam += !!role.id ? 'UPDATE_MESSAGE' : 'ADD_MESSAGE';
		const _saveMessage = this.translate.instant(saveMessageTranslateParam);
		const _messageType = !!role.id ? MessageType.Update : MessageType.Create;
		const dialogRef = this.dialog.open(RoleEditDialogComponent, { data: { role } });
		dialogRef.afterClosed().subscribe(res => {
			if (!res) {
				return;
			}

			this.layoutUtilsService.showActionNotification(_saveMessage, _messageType, 10000, true, false);
			this.loadRolesList();
		});
		
	}

	isAllSelected(): boolean {
		const numSelected = this.selection.selected.length;
		const numRows = this.rolesResult.length;
		return numSelected === numRows;
	}

	masterToggle() {
		if (this.selection.selected.length === this.rolesResult.length) {
			this.selection.clear();
		} else {
			this.rolesResult.forEach(row => this.selection.select(row));
		}
	}

}
