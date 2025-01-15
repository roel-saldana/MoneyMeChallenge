import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoanApplication } from '../../shared/models/LoanApplication';

@Injectable({
  providedIn: 'root'
})

export class LoanService {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);

  //Create loan will receive a json object and return a redirect link
  createLoanApplication(loanApplication: any) {
    return this.http.post(`${this.baseUrl}/LoanApplication/create`, loanApplication, { responseType: 'text' });
  }

  getLoanApplicationById(id: number) {
    return this.http.get<LoanApplication>(`${this.baseUrl}/LoanApplication/${id}`);
  }

  calculateLoanApplicationQuote(loanApplication: LoanApplication) {
    return this.http.post(`${this.baseUrl}/LoanApplication/calculate`, loanApplication, { responseType: 'json' });
  }

  applyLoan(loanApplication: LoanApplication) {
    return this.http.post(`${this.baseUrl}/LoanApplication/apply`, loanApplication, { responseType: 'json' });
  }
}
