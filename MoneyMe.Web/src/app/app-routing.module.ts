import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateLoanApplicationComponent } from './features/create-loan-application/create-loan-application.component';
import { ViewLoanApplicationComponent } from './features/view-loan-application/view-loan-application.component';
import { ApplyLoanComponent } from './features/apply-loan/apply-loan.component';
import { ErrorHandlerComponent } from './shared/components/error-handler/error-handler.component';

const routes: Routes = [
  {path: '', component: CreateLoanApplicationComponent, pathMatch: 'full'},
  {path: 'view/:id', component: ViewLoanApplicationComponent, pathMatch: 'full'},
  {path: 'apply', component: ApplyLoanComponent, pathMatch: 'full'},
  {path: 'error', component: ErrorHandlerComponent, pathMatch: 'full'},
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
