import { Component, OnInit, Inject, ChangeDetectionStrategy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';

import { UserService } from '../../services/user.service';
import { UserModel } from '../../models/user.model';
import { RoleModel } from '../../models/role.model';
import { map, debounceTime, distinctUntilChanged, take } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
	selector: 'm-user-edit-dialog',
	templateUrl: './user-edit.dialog.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserEditDialogComponent implements OnInit {
	user: UserModel;
	roles: RoleModel[];
	userForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	debouncedSubject = new Subject<string>();
	validatorSubject = new Subject();

	constructor(public dialogRef: MatDialogRef<UserEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private userService: UserService) { }

	ngOnInit() {
		this.user = this.data.user;
		this.roles = this.data.roles;
		this.createForm();
		this.createUserNameValidation();
	}

	createUserNameValidation(){
		this.debouncedSubject
			.pipe(
             debounceTime(500),
             distinctUntilChanged()
			).subscribe(username => {
                this.userService.validateUserName(username)
                    .subscribe(reserved => 	this.validatorSubject.next(reserved));
			 });

		this.validatorSubject = new Subject<boolean>();
	}

	createForm() {
		let isEdit = !!this.user.userId;
		this.userForm = this.fb.group({
			userName: [ { value: this.user.userName, disabled: isEdit},
						[ Validators.required,
						  Validators.maxLength(50),
						  Validators.minLength(5),
						  Validators.pattern(/^$|^[A-Za-z0-9_]+$/)],
						[this.validateUserName()]],
			firstName: [this.user.firstName, 
						[ Validators.required,
						  Validators.minLength(2),
						  Validators.maxLength(200)]],
			lastName: [this.user.lastName, 
						[ Validators.required,
						  Validators.minLength(2),
						  Validators.maxLength(200)]],
			email: [ this.user.email, 
						[ Validators.required, 
						  Validators.email,
						  Validators.minLength(8),
						  Validators.maxLength(256)]],
			phoneNumber: [this.user.phoneNumber, 
						[ Validators.required,
						  Validators.minLength(5),
						  Validators.maxLength(100)]],
			roleId: [this.user.role ? this.user.role.id : null, Validators.required ]
		});
	}

	validateUserName(): AsyncValidatorFn  {
		return (control: AbstractControl) => {
			this.debouncedSubject.next(control.value);
			return this.validatorSubject
						.asObservable()
						.pipe(
							take(1),
							map(reserved => reserved ? {reserved: true} : null)
						);
		};

	}

	getTitle(): string {
		if (this.user.userId) {
			return `Edit User '${this.user.firstName} ${this.user.lastName}'`;
		}

		return 'New User';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.userForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	prepareUser(): UserModel {
		const controls = this.userForm.controls;
		const _user = new UserModel();
		_user.userId = this.user.userId;
		_user.firstName = controls['firstName'].value;
		_user.lastName = controls['lastName'].value;
		_user.email = controls['email'].value;
		_user.userName = controls['userName'].value;
		_user.phoneNumber = controls['phoneNumber'].value;
		_user.roleId = controls['roleId'].value;
		return _user;
	}

	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		const controls = this.userForm.controls;

		if (this.userForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);

			this.hasFormErrors = true;
			return;
		}

		const editUser = this.prepareUser();
		if (editUser.userId) {
			this.updateUser(editUser);
		} else {
			this.createUser(editUser);
		}
	}

	updateUser(_user: UserModel) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		this.userService.updateUser(_user).subscribe(res => {
			this.viewLoading = false;
			this.dialogRef.close({
				_user,
				isEdit: true
			});
		});
	}

	createUser(_user: UserModel) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		this.userService.createUser(_user).subscribe(res => {
			this.viewLoading = false;
			this.dialogRef.close({
				_user,
				isEdit: false
			});
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}


}