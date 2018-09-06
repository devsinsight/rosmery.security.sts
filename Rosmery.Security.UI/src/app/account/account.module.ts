import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { CreateUserComponent } from './user/create-user/create-user.component';
import { UpdateUserComponent } from './user/update-user/update-user.component';
import { CreateRoleComponent } from './role/create-role/create-role.component';
import { UpdateRoleComponent } from './role/update-role/update-role.component';
import { AccountRouting } from './account.routing';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { LogoutComponent } from './logout/logout.component';
import { AccountService } from './shared/account.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    AccountRouting,
    FormsModule
  ],
  declarations: [
    LoginComponent,
    CreateUserComponent,
    UpdateUserComponent,
    CreateRoleComponent,
    UpdateRoleComponent,
    UnauthorizedComponent,
    LogoutComponent],
  providers: [AccountService]
})
export class AccountModule { }
