import { SignoutCallbackComponent } from './shared/components/signout-callback.component';
import { SigninCallbackComponent } from './shared/components/signin-callback.component';
import { AppComponent } from './app.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuardService } from './shared/services/auth-guard.service';

const router: Routes =
[
  { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuardService]},
  { path: 'signin-callback', component: SigninCallbackComponent },
  { path: 'signout-callback', component: SignoutCallbackComponent },
  { path: 'account', loadChildren: './account/account.module#AccountModule' },
  { path: 'token-provider', loadChildren: './token-provider/token-provider.module#TokenProviderModule' }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(router, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRouting { }
