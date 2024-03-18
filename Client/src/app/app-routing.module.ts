import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ReloadComponent } from './pages/reload/reload.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'reload', component: ReloadComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
