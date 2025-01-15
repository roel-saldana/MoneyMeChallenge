import { Component, inject } from '@angular/core';
import { LoanService } from '../../core/services/loan.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, RedirectCommand, Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { LoanApplication } from '../../shared/models/LoanApplication';
import { DialogComponent } from '../../shared/components/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog'

@Component({
  selector: 'app-view-loan-application',
  standalone: false,  
  templateUrl: './view-loan-application.component.html',
  styleUrl: './view-loan-application.component.css'
})
export class ViewLoanApplicationComponent {
  loanService: LoanService = inject(LoanService);
  
  loanApplicationId: number;
  selectedTitle: string;
  amountRequiredSliderValue: number;
  termSliderValue: number;  

  amountRequiredSliderLabel: string = "How much do you need?";
  termSliderLabel: string = "Term (months)";

  titleOptions = [ 
    { value: 'Mr.', label: 'Mr.' }, 
    { value: 'Mrs.', label: 'Mrs.' }, 
    { value: 'Ms.', label: 'Ms.' }
  ];

  productTypeOptions = [
    { value: 'ProductA', label: 'Product A' },
    { value: 'ProductB', label: 'Product B' },
    { value: 'ProductC', label: 'Product C' }
  ];

  quoteCalculatorFormGroup : FormGroup;
  constructor(private fb: FormBuilder, 
              private route: ActivatedRoute,
              private router: Router,
              private dialog: MatDialog) { }

  ngOnInit() : void {
    this.route.params.subscribe(params => { 
      this.loanApplicationId = params['id'];
    });

    this.quoteCalculatorFormGroup = this.fb.group({
      firstName: [''],
      lastName: [''],
      dateOfBirth: [''],
      email: [''],
      mobile: [''],
      amountRequired: [''],
      term: [''],
      title: [''],
      id: [''],
      productType: [this.productTypeOptions[0].value]
    });

    this.loanService.getLoanApplicationById(this.loanApplicationId).subscribe({
      next: (loanApplication) => {
        this.quoteCalculatorFormGroup.patchValue({
          firstName: loanApplication.firstName,
          lastName: loanApplication.lastName,
          dateOfBirth: formatDate(loanApplication.dateOfBirth, "yyyy-MM-dd", "en"),
          email: loanApplication.email,
          mobile: loanApplication.mobile,
          title: loanApplication.title,
          id: loanApplication.id,
          productType: loanApplication.productType == "" ? this.productTypeOptions[0].value : loanApplication.productType
        });

        this.termSliderValue = loanApplication.term;
        this.amountRequiredSliderValue = loanApplication.amountRequired;
      },
      error: (err: any) => {
        //Not found, invalid link
        console.error(err);
        this.router.navigate(['/error']);
      }
    });
  }

  onAmountRequiredSliderValueChange(newValue: number) { 
    this.amountRequiredSliderValue = newValue; 
  }

  onTermSliderValueChange(newValue: number) { 
    this.termSliderValue = newValue; 
  }

  openDialog(): void { 
    const dialogRef = this.dialog.open(DialogComponent, {
      data: {
        message: 'Product B requires a minimum of 6 months to apply. <br />Do you want to continue?',
        title: 'Information',
        showCancelButton: true
      }
    }); 
    dialogRef.afterClosed().subscribe(result => { 
      if (result.event === 'confirm') {
        this.termSliderValue = 6;
        this.updateLoanApplication();
      }
    });
  }

  updateLoanApplication() : void {
    this.quoteCalculatorFormGroup.value.term =  this.termSliderValue;
    this.quoteCalculatorFormGroup.value.amountRequired = this.amountRequiredSliderValue;

    this.loanService.calculateLoanApplicationQuote(this.quoteCalculatorFormGroup.value).subscribe({
      next: (applicationData) => {
        this.router.navigate([`/apply`], { state: { data: applicationData }}); 
      },
      error: (err: any) => {
        console.error(err);
        this.router.navigate(['/error']);
      }
    });
  }

  onSubmit(): void {
    if(this.quoteCalculatorFormGroup.value.productType === 'ProductB' && this.termSliderValue < 6) {
      this.openDialog();
      return;
    }

    this.updateLoanApplication();    
  }
}
