This project uses
	.Net 8.0
	Angular
	
	
To restore the database, run:
	dotnet ef database update -s MoneyMe.Api -p MoneyMe.Infrastructure
	
API Project runs on https://localhost:5001/
while Web Project runs on http://localhost:4200/


API Endpoints:
	/api/LoanApplication/create
		accepts JSON data to create the loan application
	
	/api/LoanApplication/{id}
		retrieves an existing loan application by id
		
	/api/LoanApplication/calculate
		accepts JSON data of an existing loan application, which then returns the calculated quotes
		
	/api/LoanApplication/apply
		accepts JSON data, runs checks on the loan application, saves details on database when all checks passed
		