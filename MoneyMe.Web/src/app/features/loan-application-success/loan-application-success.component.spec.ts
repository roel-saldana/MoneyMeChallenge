import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanApplicationSuccessComponent } from './loan-application-success.component';

describe('LoanApplicationSuccessComponent', () => {
  let component: LoanApplicationSuccessComponent;
  let fixture: ComponentFixture<LoanApplicationSuccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoanApplicationSuccessComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanApplicationSuccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
