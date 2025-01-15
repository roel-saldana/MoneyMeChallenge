using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePaymentDetailProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "LoanPaymentDetails",
                newName: "IsPaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "LoanPaymentDetails",
                newName: "PaymentStatus");
        }
    }
}
