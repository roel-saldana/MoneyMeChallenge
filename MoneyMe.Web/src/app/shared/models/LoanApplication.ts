export type LoanApplication = {
    id: number;
    amountRequired: number;
    term: number;
    title: string;
    firstName: string;
    lastName: string;
    email: string;
    mobile: string;
    dateOfBirth: string;
    productType: string;
    repaymentAmount: number;
    monthlyPaymentAmount: number;
    establishmentFee: number;
    totalInterest: number;
};