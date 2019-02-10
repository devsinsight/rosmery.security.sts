import { Component, OnInit, Inject, ChangeDetectionStrategy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators, AsyncValidatorFn, AbstractControl } from '@angular/forms';

import { RoleModel } from '../../models/role.model';
import { RoleService } from '../../services/role.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, take, map } from 'rxjs/operators';

@Component({
	selector: 'm-role-edit-dialog',
	templateUrl: './role-edit.dialog.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class RoleEditDialogComponent implements OnInit {
	role: RoleModel;
	roleForm: FormGroup;
	hasFormErrors: boolean = false;
	viewLoading: boolean = false;
	loadingAfterSubmit: boolean = false;
	debouncedSubject = new Subject<string>()
	validatorSubject = new Subject();

	constructor(public dialogRef: MatDialogRef<RoleEditDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: any,
		private fb: FormBuilder,
		private roleService: RoleService) { }

	ngOnInit() {
		this.role = this.data.role;
		this.createForm();
		this.createRoleNameValidation();
	}

	createRoleNameValidation(){
		this.debouncedSubject
			.pipe(
             debounceTime(500),
             distinctUntilChanged()
			).subscribe(roleName => {
                this.roleService.validateRoleName(roleName)
                    .subscribe(reserved => 	this.validatorSubject.next(reserved));
			 });

		this.validatorSubject = new Subject<boolean>();
	}

	createForm() {
		let isEdit = this.role.id;
		this.roleForm = this.fb.group({
			name: [ {value: this.role.name, disabled: isEdit }, 
					[ Validators.required, 
					 Validators.minLength(5),
					 Validators.maxLength(50),
					 Validators.pattern(/^$|^[A-Za-z0-9_]+$/)] ,
					[ this.validateRoleName() ]],
			description: [ this.role.description, 
						[ Validators.maxLength(500)] ]
		});
	}
	
	validateRoleName(): AsyncValidatorFn  {
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
		if (this.role.id) {
			return `Edit Role '${this.role.name}'`;
		}

		return 'New Role';
	}

	isControlInvalid(controlName: string): boolean {
		const control = this.roleForm.controls[controlName];
		const result = control.invalid && control.touched;
		return result;
	}

	prepareRole(): RoleModel {
		const controls = this.roleForm.controls;
		const _role = new RoleModel();
		_role.id = this.role.id;
		_role.name = controls['name'].value;
		_role.description = controls['description'].value;
		return _role;
	}

	onSubmit() {
		this.hasFormErrors = false;
		this.loadingAfterSubmit = false;
		const controls = this.roleForm.controls;

		if (this.roleForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);

			this.hasFormErrors = true;
			return;
		}

		const editRole = this.prepareRole();
		if (editRole.id) {
			this.updateRole(editRole);
		} else {
			this.createRole(editRole);
		}
	}

	updateRole(_role: RoleModel) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		this.roleService.updateRole(_role).subscribe(res => {
			this.viewLoading = false;
			this.dialogRef.close({
				_role,
				isEdit: true
			});
		});
	}

	createRole(_role: RoleModel) {
		this.loadingAfterSubmit = true;
		this.viewLoading = true;
		this.roleService.createRole(_role).subscribe(res => {
			this.viewLoading = false;
			this.dialogRef.close({
				_role,
				isEdit: false
			});
		});
	}

	onAlertClose($event) {
		this.hasFormErrors = false;
	}
}
