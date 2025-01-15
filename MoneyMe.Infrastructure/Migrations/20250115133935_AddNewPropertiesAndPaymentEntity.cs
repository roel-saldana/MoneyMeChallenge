using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertiesAndPaymentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstablishmentFee",
                table: "LoanApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterest",
                table: "LoanApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "LoanPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationId = table.Column<int>(type: "int", nullable: false),
                    TermNo = table.Column<int>(type: "int", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPaymentDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanPaymentDetails");

            migrationBuilder.DropColumn(
                name: "EstablishmentFee",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "LoanApplications");
        }
    }
}
