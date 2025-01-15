import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CreateLoanApplicationComponent } from './features/create-loan-application/create-loan-application.component';
import { ViewLoanApplicationComponent } from './features/view-loan-application/view-loan-application.component';
import { SliderComponent } from './shared/components/slider/slider.component';
import { ErrorHandlerComponent } from './shared/components/error-handler/error-handler.component';
import { CurrencyPipe } from '@angular/common';
import { ApplyLoanComponent } from './features/apply-loan/apply-loan.component';
import { DialogComponent } from './shared/components/dialog/dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateLoanApplicationComponent,
    ViewLoanApplicationComponent,
    ApplyLoanComponent,
    SliderComponent,
    ErrorHandlerComponent,
    ApplyLoanComponent,
    DialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    provideAnimationsAsync(),
    CurrencyPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
