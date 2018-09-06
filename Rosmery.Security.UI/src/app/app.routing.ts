import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { CallbackComponent } from './shared/components/callback.component';
import { HomeComponent } from './home/home.component';
import { AuthGuardService } from './shared/services/auth-guard.service';

const router: Routes =
[
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'callback', component: CallbackComponent },
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
