import { Component, OnInit, Inject, ChangeDetectionStrategy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TypesUtilsService } from '../../services/types-utils.service';
import { UserService } from '../../services/user.service';
import { UserModel } from '../../models/user.model';

@Component({
	selector: 'm-user-edit-dialog',
	templateUrl: './user-edit.dialog.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserEditDialogComponent implements OnInit {
	user: UserModel;
	userForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;

	constructor(public dialogRef: MatDialogRef<UserEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private userService: UserService,
		private typesUtilsService: TypesUtilsService) { }

	/** LOAD DATA */
	ngOnInit() {
		this.user = this.data.user;
		this.createForm();

		/* Server loading imitation. Remove this on real code */
		this.viewLoading = true;
		//setTimeout(() => {
			this.viewLoading = false;
		//}, 1000);
	}

	createForm() {
		this.userForm = this.fb.group({
			userName: [ this.user.userName, Validators.required],
			firstName: [this.user.firstName, Validators.required],
			lastName: [this.user.lastName, Validators.required],
			email: [ this.user.email, [Validators.required, Validators.email]],
			phoneNumber: [this.user.phoneNumber, Validators.required]	
		});
	}

	/** UI */
	getTitle(): string {
		if (this.user.id) {
			return `Edit User '${this.user.firstName} ${this.user.lastName}'`;
		}

		return 'New User';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.userForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	/** ACTIONS */
	prepareCustomer(): UserModel {
		const controls = this.userForm.controls;
		const _user = new UserModel();
		_user.id = this.user.id;
		_user.firstName = controls['firstName'].value;
		_user.lastName = controls['lastName'].value;
		_user.email = controls['email'].value;
		_user.userName = controls['userName'].value;
		_user.phoneNumber = controls['phoneNumber'].value;
		return _user;
	}

	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		const controls = this.userForm.controls;
		/** check form */
		if (this.userForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);

			this.hasFormErrors = true;
			return;
		}

		const editedCustomer = this.prepareCustomer();
		if (editedCustomer.id) {
			this.updateCustomer(editedCustomer);
		} else {
			this.createCustomer(editedCustomer);
		}
	}

	updateCustomer(_user: UserModel) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		this.userService.updateUser(_user).subscribe(res => {
			/* Server loading imitation. Remove this on real code */
			this.viewLoading = false;
			this.viewLoading = false;
			this.dialogRef.close({
				_user,
				isEdit: true
			});
		});
	}

	createCustomer(_user: UserModel) {
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
