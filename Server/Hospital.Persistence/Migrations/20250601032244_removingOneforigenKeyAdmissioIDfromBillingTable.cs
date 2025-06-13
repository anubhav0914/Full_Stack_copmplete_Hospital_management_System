using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removingOneforigenKeyAdmissioIDfromBillingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingTransactions_AdmissionDischarges_AdmissionAdmitId",
                table: "BillingTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BillingTransactions_AdmissionAdmitId",
                table: "BillingTransactions");

            migrationBuilder.DropColumn(
                name: "AdmissionAdmitId",
                table: "BillingTransactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdmissionAdmitId",
                table: "BillingTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BillingTransactions_AdmissionAdmitId",
                table: "BillingTransactions",
                column: "AdmissionAdmitId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingTransactions_AdmissionDischarges_AdmissionAdmitId",
                table: "BillingTransactions",
                column: "AdmissionAdmitId",
                principalTable: "AdmissionDischarges",
                principalColumn: "AdmitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
