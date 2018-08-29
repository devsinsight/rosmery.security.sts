import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './user/create-user/create-user.component';
import { UpdateUserComponent } from './user/update-user/update-user.component';
import { CreateRoleComponent } from './role/create-role/create-role.component';
import { UpdateRoleComponent } from './role/update-role/update-role.component';
import { AccountRouting } from './account.routing';
import { AccountComponent } from './account.component';

@NgModule({
  imports: [
    CommonModule,
    AccountRouting
  ],
  declarations: [
    LoginComponent,
    CreateUserComponent,
    UpdateUserComponent,
    CreateRoleComponent,
    UpdateRoleComponent,
    AccountComponent]
})
export class AccountModule { }
