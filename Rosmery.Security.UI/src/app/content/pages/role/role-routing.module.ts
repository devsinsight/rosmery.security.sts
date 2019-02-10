import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleManagementComponent } from './components/role-management/role-management.component';

const routes: Routes = [
    { path: '', component: RoleManagementComponent },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RoleRoutingModule {}
