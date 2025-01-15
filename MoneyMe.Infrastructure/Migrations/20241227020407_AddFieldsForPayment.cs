using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsForPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RepaymentAmount",
                table: "LoanApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "RepaymentAmount",
                table: "LoanApplications");
        }
    }
}
