import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PagesComponent } from './pages.component';
import { ActionComponent } from './header/action/action.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { AuthGuardService } from '../../core/auth/services/auth-guard.service';
import { ProfileComponent } from './header/profile/profile.component';
import { ErrorPageComponent } from './snippets/error-page/error-page.component';
import { InnerComponent } from "./components/inner/inner.component";
import { LoginComponent } from './auth/login/login.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { SigninCallbackComponent } from '../../core/auth/components/signin-callback.component';
import { SignoutCallbackComponent } from '../../core/auth/components/signout-callback.component';
import { SilentRenewCallbackComponent } from '../../core/auth/components/silent-renew-callback.component';

const routes: Routes = [
	{
		path: '',
		component: PagesComponent,
		canActivate: [AuthGuardService],
		children: [
			{
				path: '',
				loadChildren: () => import('./components/dashboard/dashboard.module').then(m => m.DashboardModule),
				canActivate: [AuthGuardService]
			},
			{
				path: 'builder',
				loadChildren: () => import('./builder/builder.module').then(m => m.BuilderModule),
				canActivate: [AuthGuardService]
			},
			{
				path: 'header/actions',
				component: ActionComponent,
				canActivate: [AuthGuardService]
			},
			{
				path: 'profile',
				component: ProfileComponent,
				canActivate: [AuthGuardService]
			},
			{
				path: 'inner',
				component: InnerComponent,
				canActivate: [AuthGuardService]
			},
			{
				path: 'user', 
				loadChildren: () => import('./user/user.module').then(m => m.UserModule),
				canActivate: [AuthGuardService]
			},
			{
				path: 'role', 
				loadChildren: () => import('./role/role.module').then(m => m.RoleModule),
				canActivate: [AuthGuardService]
			}
		]
	},
	{ path: 'login', component: LoginComponent },
	{ path: 'logout', component: LogoutComponent },
	{ path: 'signin-callback', component: SigninCallbackComponent },
  	{ path: 'signout-callback', component: SignoutCallbackComponent },
  	{ path: 'silent-renew-callback', component: SilentRenewCallbackComponent },
	{
		path: '404',
		component: ErrorPageComponent
	},
	{
		path: 'error/:type',
		component: ErrorPageComponent
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class PagesRoutingModule {
}
