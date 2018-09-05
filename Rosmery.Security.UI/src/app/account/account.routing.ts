import { NgModule } from '@angular/core';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './user/create-user/create-user.component';
import { UpdateRoleComponent } from './role/update-role/update-role.component';
import { UpdateUserComponent } from './user/update-user/update-user.component';
import { CreateRoleComponent } from './role/create-role/create-role.component';
import { AccountComponent } from './account.component';
import { AuthGuardService } from '../common/services/auth-guard.service';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

const router: Routes = [
  {
    path: '',
    component: AccountComponent,
    canActivate: [AuthGuardService],
    pathMatch: 'full'
  },
  { path: 'login', component: LoginComponent },
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
