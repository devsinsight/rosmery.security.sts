import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { CallbackComponent } from './common/components/callback.component';
import { HomeComponent } from './home/home.component';
import { AuthGuardService } from './common/services/auth-guard.service';

const router: Routes =
[
  { path: '', component: HomeComponent, canActivate: [AuthGuardService] },
  { path: 'callback', component: CallbackComponent },
  { path: 'home', component: HomeComponent },
  { path: 'account', loadChildren: './account/account.module#AccountModule' }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(router, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRouting { }
