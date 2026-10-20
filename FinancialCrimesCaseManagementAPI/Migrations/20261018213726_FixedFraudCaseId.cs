using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialCrimesCaseManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedFraudCaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FraudCases_Transactions_TransactionId",
                table: "FraudCases");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "FraudCases",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_FraudCases_Transactions_TransactionId",
                table: "FraudCases",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FraudCases_Transactions_TransactionId",
                table: "FraudCases");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "FraudCases",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FraudCases_Transactions_TransactionId",
                table: "FraudCases",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
