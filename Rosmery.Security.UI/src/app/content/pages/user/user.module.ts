import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from './user-routing.module';
import { CoreModule } from '../../../core/core.module';
import { PartialsModule } from '../../partials/partials.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {
	MatInputModule,
	MatPaginatorModule,
	MatProgressSpinnerModule,
	MatSortModule,
	MatTableModule,
	MatSelectModule,
	MatMenuModule,
	MatProgressBarModule,
	MatButtonModule,
	MatCheckboxModule,
	MatDialogModule,
	MatTabsModule,
	MatNativeDateModule,
	MatCardModule,
	MatRadioModule,
	MatIconModule,
	MatDatepickerModule,
	MatAutocompleteModule,
	MAT_DIALOG_DEFAULT_OPTIONS,
	MatSnackBarModule,
	MatTooltipModule
} from '@angular/material';
import { TranslateModule } from '@ngx-translate/core';

import { UserComponent } from './user.component';
import { UserService } from './services/user.service';
import { HttpUtilsService } from './services/http-utils.service';
import { AuthGuardService } from '../../../core/auth/services/auth-guard.service';
import { AuthInterceptorService } from '../../../core/auth/services/auth-interceptor.service';
import { LayoutUtilsService } from './services/layout-utils.service';
import { ActionNotificationComponent } from './components/action-notification/action-notification.component';
import { DeleteEntityDialogComponent } from './components/delete-entity-dialog/delete-entity-dialog.component';
import { FetchEntityDialogComponent } from './components/fetch-entity-dialog/fetch-entity-dialog.component';
import { UpdateStatusDialogComponent } from './components/update-status-dialog/update-status-dialog.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import { UserEditDialogComponent } from './components/user-edit/user-edit.dialog.component';
import { AlertComponent } from './components/alert/alert.component';
import { TypesUtilsService } from './services/types-utils.service';

@NgModule({
    declarations: [
        UserComponent,
		UserManagementComponent,

		ActionNotificationComponent,
		UserEditDialogComponent,
		DeleteEntityDialogComponent,
		FetchEntityDialogComponent,
		UpdateStatusDialogComponent,
		AlertComponent
    ],
    imports: [ 
        
		CommonModule,
		HttpClientModule,
		PartialsModule,
		FormsModule,
		ReactiveFormsModule,
		TranslateModule.forChild(),
		MatDialogModule,
		MatButtonModule,
		MatMenuModule,
		MatSelectModule,
        MatInputModule,
		MatTableModule,
		MatAutocompleteModule,
		MatRadioModule,
		MatIconModule,
		MatNativeDateModule,
		MatProgressBarModule,
		MatDatepickerModule,
		MatCardModule,
		MatPaginatorModule,
		MatSortModule,
		MatCheckboxModule,
		MatProgressSpinnerModule,
		MatSnackBarModule,
		MatTabsModule,
        MatTooltipModule,
        UserRoutingModule,
        CoreModule
    ],
	exports: [],
	entryComponents: [
		ActionNotificationComponent,
		UserEditDialogComponent,
		DeleteEntityDialogComponent,
		FetchEntityDialogComponent,
		UpdateStatusDialogComponent,
		AlertComponent
	],
    providers: [
		UserService,
		HttpUtilsService,
		LayoutUtilsService,
		TypesUtilsService,
        AuthGuardService,
		{
		  provide: HTTP_INTERCEPTORS,
		  useClass: AuthInterceptorService,
		  multi: true
		},
        {
			provide: MAT_DIALOG_DEFAULT_OPTIONS,
			useValue: {
				hasBackdrop: true,
				panelClass: 'm-mat-dialog-container__wrapper',
				height: 'auto',
				width: '900px'
			}
		},
    ],
})
export class UserModule {}