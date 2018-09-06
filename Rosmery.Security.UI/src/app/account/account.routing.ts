import { NgModule } from '@angular/core';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './user/create-user/create-user.component';
import { UpdateRoleComponent } from './role/update-role/update-role.component';
import { UpdateUserComponent } from './user/update-user/update-user.component';
import { CreateRoleComponent } from './role/create-role/create-role.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { LogoutComponent } from './logout/logout.component';
import { AuthGuardService } from '../shared/services/auth-guard.service';

const router: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'create-user', component: CreateUserComponent },
  { path: 'update-user', component: UpdateUserComponent },
  { path: 'create-role', component: CreateRoleComponent },
  { path: 'update-role', component: UpdateRoleComponent },
  { path: 'unauthorized', component: UnauthorizedComponent }
];

@NgModule({
  imports: [RouterModule.forChild(router)],
  exports: [RouterModule]
})
export class AccountRouting { }
