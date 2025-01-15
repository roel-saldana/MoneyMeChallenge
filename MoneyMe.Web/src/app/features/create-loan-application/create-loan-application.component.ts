//About this component:
  //- This is an added feature to create loan application
  //- This will generate the redirect link to apply for a loan
  //    - JSON will be sent to the api and a redirect link will be displayed as required by the test

import { Component, inject } from '@angular/core';
import { LoanService } from '../../core/services/loan.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DialogComponent } from '../../shared/components/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog'

@Component({
  selector: 'app-create-loan-application',
  standalone: false,  
  templateUrl: './create-loan-application.component.html',
  styleUrl: './create-loan-application.component.css'
})
export class CreateLoanApplicationComponent {
  loanService: LoanService = inject(LoanService);

  amountRequiredSliderLabel: string = "How much do you need?";
  amountRequiredSliderValue: number = 1;
  termSliderLabel: string = "Term (months)";
  termSliderValue: number = 1;
  redirectLink: string = "";

  selectedTitle: string = 'Mr.';
  titleOptions = [ 
    { value: 'Mr.', label: 'Mr.' }, 
    { value: 'Mrs.', label: 'Mrs.' }, 
    { value: 'Ms.', label: 'Ms.' }
  ];
  
  createLoanApplicationFormGroup: FormGroup;
  constructor(private fb: FormBuilder,
              private dialog: MatDialog) { }

  ngOnInit() : void {
    this.createLoanApplicationFormGroup = this.fb.group({
      title: [this.selectedTitle],
      firstName: [''],
      lastName: [''],
      dateOfBirth: [''],
      email: [''],
      mobile: [''],
      amountRequired: [''],
      term: [this.termSliderValue],
      productType: ['']
    });
  }

  onAmountRequiredSliderValueChange(newValue: number) { 
    this.amountRequiredSliderValue = newValue; 
  }

  onTermSliderValueChange(newValue: number) { 
    this.termSliderValue = newValue; 
  }

  onSubmit(): void {
    this.createLoanApplicationFormGroup.value.amountRequired = this.amountRequiredSliderValue;
    this.createLoanApplicationFormGroup.value.term = this.termSliderValue;

    this.loanService.createLoanApplication(this.createLoanApplicationFormGroup.value).subscribe({
      next: (data) => {
        this.redirectLink = data.toString();
        this.openDialog('Loan application created successfully. Redirect link: ' + this.redirectLink);
      },
      error: (err) => { console.error(err); }
    });
  }

  openDialog(message: string): void { 
    if(message != '') {
      this.dialog.open(DialogComponent, {
        data: {
          message: message,
          title: 'Information',
          showCancelButton: false
        }
      });
    }
  }
}