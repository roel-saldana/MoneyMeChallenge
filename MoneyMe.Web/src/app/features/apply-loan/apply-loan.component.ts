import { Component, inject } from '@angular/core';
import { LoanService } from '../../core/services/loan.service';
import { ActivatedRoute, RedirectCommand, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CurrencyPipe } from '@angular/common';
import { LoanApplication } from '../../shared/models/LoanApplication';
import { LoanApplicationResult } from '../../shared/models/LoanApplicationResult';
import { DialogComponent } from '../../shared/components/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog'
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-apply-loan',
  standalone: false,  
  templateUrl: './apply-loan.component.html',
  styleUrl: './apply-loan.component.css'
})
export class ApplyLoanComponent {
  constructor(private fb: FormBuilder, 
                private route: ActivatedRoute,
                private router: Router,
                public currencyPipe: CurrencyPipe,
                private dialog: MatDialog) { }                

  loanService: LoanService = inject(LoanService);

  loanApplicationId: number;
  isBasicDetailsInEditMode: boolean = false;
  isFinanceDetailsInEditMode: boolean = false;
  fullName: string;
  firstName: string;
  lastName: string;
  email: string;
  mobile: string
  amountRequired: number;
  term: number;
  basicDetailsEditButtonLabel: string = 'Edit';
  financeDetailsEditButtonLabel: string = 'Edit';
  amountRequiredSliderLabel: string = "How much do you need?";
  amountRequiredSliderValue: number = 1;
  termSliderLabel: string = "Term (months)";
  termSliderValue: number = 1;
  termSliderMinValue: number = 1;
  monthlyPaymentAmount: number;
  repaymentAmount: number;  
  interestAmount: number = 0;

  //move to a const file
  establishmentFee: number = 300;

  applyLoanApplicationFormGroup : FormGroup;

  ngOnInit() : void {
    const loanApplication = history.state.data;

    this.applyLoanApplicationFormGroup = this.fb.group({
      firstName: [loanApplication.firstName],
      lastName: [loanApplication.lastName],
      dateOfBirth: [formatDate(loanApplication.dateOfBirth, "yyyy-MM-dd", "en")],
      email: [loanApplication.email],
      mobile: [loanApplication.mobile],
      amountRequired: [loanApplication.amountRequired],
      term: [loanApplication.term],
      title: [loanApplication.title],
      id: [loanApplication.id],
      productType: [loanApplication.productType],
      repaymentAmount: [loanApplication.repaymentAmount],
      monthlyPaymentAmount: [loanApplication.monthlyPaymentAmount]
    });

    this.amountRequiredSliderValue = loanApplication.amountRequired;
    this.termSliderValue = loanApplication.term;
    this.interestAmount = loanApplication.repaymentAmount - loanApplication.amountRequired - this.establishmentFee;
    this.repaymentAmount = loanApplication.repaymentAmount;
    if(loanApplication.productType == "ProductB") {
      this.termSliderMinValue = 6;
    }    
  }

  editBasicDetailsClick() {
    this.isBasicDetailsInEditMode = !this.isBasicDetailsInEditMode;
    this.basicDetailsEditButtonLabel = this.isBasicDetailsInEditMode ? 'Save' : 'Edit';
  }

  editFinanceDetailsClick () {
    this.isFinanceDetailsInEditMode = !this.isFinanceDetailsInEditMode;
    this.financeDetailsEditButtonLabel = this.isFinanceDetailsInEditMode ? 'Save' : 'Edit';

    if (!this.isFinanceDetailsInEditMode) {
      this.recalculateQuote();
    }
  }

  recalculateQuote() {
    this.loanService.calculateLoanApplicationQuote(this.applyLoanApplicationFormGroup.value).subscribe({
      next: (data) => {
        var applicationData = data as LoanApplication
        this.applyLoanApplicationFormGroup.patchValue({
          amountRequired: applicationData.amountRequired,
          repaymentAmount: applicationData.repaymentAmount,
          monthlyPaymentAmount: applicationData.monthlyPaymentAmount,
          term: applicationData.term,
        });
        this.interestAmount = applicationData.repaymentAmount - applicationData.amountRequired - this.establishmentFee;
        this.repaymentAmount = applicationData.repaymentAmount;
      }
    });
  }

  onAmountRequiredSliderValueChange(newValue: number) { 
    this.amountRequiredSliderValue = newValue;
    this.applyLoanApplicationFormGroup.value.amountRequired = newValue;
  }

  onTermSliderValueChange(newValue: number) { 
    this.termSliderValue = newValue;
    this.applyLoanApplicationFormGroup.value.term = newValue;
  }

  onSubmit(): void {
    if(this.isBasicDetailsInEditMode || this.isFinanceDetailsInEditMode) {
      this.openDialog('Please save your changes before submitting', 'Information');
      return;
    }

    this.loanService.applyLoan(this.applyLoanApplicationFormGroup.value).subscribe({
      next: (data) => {
        var applicationResult = data as LoanApplicationResult;
        if(applicationResult.isSuccess) {
          this.openDialog('Your application has been submitted successfully<br />You may now close this window', 'Success');
          //Redirect to success page
        }
        else {
          this.openDialog(applicationResult.errors.join('<br />'));
        }
      },
      error: (err: any) => {
        console.error(err);
        this.router.navigate(['/error']);
      }
    });
  }

  openDialog(messages: string, title: string = 'Error'): void { 
    if(messages != '') {
      this.dialog.open(DialogComponent, {
        data: {
          message: messages,
          title: title,
          showCancelButton: false
        }
      });
      messages = '';
    }
  }
}
