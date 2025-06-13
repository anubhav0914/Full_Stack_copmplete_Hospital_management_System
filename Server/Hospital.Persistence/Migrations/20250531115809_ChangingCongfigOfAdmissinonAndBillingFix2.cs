using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangingCongfigOfAdmissinonAndBillingFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingTransactions_AdmissionDischarges_AdmitId",
                table: "BillingTransactions");

            migrationBuilder.RenameColumn(
                name: "AdmitId",
                table: "BillingTransactions",
                newName: "AdmissionAdmitId");

            migrationBuilder.RenameIndex(
                name: "IX_BillingTransactions_AdmitId",
                table: "BillingTransactions",
                newName: "IX_BillingTransactions_AdmissionAdmitId");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "AdmissionDischarges",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "AdmissionDischarges",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingTransactions_AdmissionDischarges_AdmissionAdmitId",
                table: "BillingTransactions",
                column: "AdmissionAdmitId",
                principalTable: "AdmissionDischarges",
                principalColumn: "AdmitId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingTransactions_AdmissionDischarges_AdmissionAdmitId",
                table: "BillingTransactions");

            migrationBuilder.RenameColumn(
                name: "AdmissionAdmitId",
                table: "BillingTransactions",
                newName: "AdmitId");

            migrationBuilder.RenameIndex(
                name: "IX_BillingTransactions_AdmissionAdmitId",
                table: "BillingTransactions",
                newName: "IX_BillingTransactions_AdmitId");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "AdmissionDischarges",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "AdmissionDischarges",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillingTransactions_AdmissionDischarges_AdmitId",
                table: "BillingTransactions",
                column: "AdmitId",
                principalTable: "AdmissionDischarges",
                principalColumn: "AdmitId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
