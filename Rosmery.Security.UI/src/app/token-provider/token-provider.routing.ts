import { CreateClientComponent } from './client/create-client/create-client.component';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuardService } from '../shared/services/auth-guard.service';

const router: Routes =
[
  { path: 'create-client', component: CreateClientComponent, canActivate: [AuthGuardService] }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(router)
  ],
  exports: [RouterModule]
})
export class TokenProviderRouting { }
