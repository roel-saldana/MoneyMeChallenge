import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewLoanApplicationComponent } from './view-loan-application.component';

describe('ViewLoanApplicationComponent', () => {
  let component: ViewLoanApplicationComponent;
  let fixture: ComponentFixture<ViewLoanApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewLoanApplicationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewLoanApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
