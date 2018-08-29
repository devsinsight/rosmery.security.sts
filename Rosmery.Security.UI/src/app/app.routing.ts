import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';

const router: Routes = [
  { path: '', loadChildren: './account/account.module#AccountModule' }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(router, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRouting { }
