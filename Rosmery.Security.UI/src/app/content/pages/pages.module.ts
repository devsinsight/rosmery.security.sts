import { LayoutModule } from '../layout/layout.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutingModule } from './pages-routing.module';
import { PagesComponent } from './pages.component';
import { PartialsModule } from '../partials/partials.module';
import { ActionComponent } from './header/action/action.component';
import { ProfileComponent } from './header/profile/profile.component';
import { CoreModule } from '../../core/core.module';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorPageComponent } from './snippets/error-page/error-page.component';
import { InnerComponent } from './components/inner/inner.component';

//auth
import { AuthGuardService } from '../../core/auth/services/auth-guard.service';
import { LoginComponent } from './auth/login/login.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { AuthInterceptorService } from '../../core/auth/services/auth-interceptor.service';
import { AuthService } from '../../core/auth/services/auth.service';

@NgModule({
	declarations: [
		PagesComponent,
		ActionComponent,
		ProfileComponent,
		ErrorPageComponent,
		InnerComponent,
		LoginComponent,
		LogoutComponent
	],
	imports: [
		CommonModule,
		HttpClientModule,
		FormsModule,
		PagesRoutingModule,
		CoreModule,
		LayoutModule,
		PartialsModule,
		AngularEditorModule
	],
	providers: [
		AuthService,
		AuthGuardService,
		{
		  provide: HTTP_INTERCEPTORS,
		  useClass: AuthInterceptorService,
		  multi: true
		}]
})
export class PagesModule {
}
