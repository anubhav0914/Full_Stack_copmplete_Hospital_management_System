using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangingCongfigOfAdmissinonAndBilling2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDischarges_Doctors_DoctorId",
                table: "AdmissionDischarges");

            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDischarges_Patients_PatientId",
                table: "AdmissionDischarges");

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDischarges_Doctors_DoctorId",
                table: "AdmissionDischarges",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDischarges_Patients_PatientId",
                table: "AdmissionDischarges",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDischarges_Doctors_DoctorId",
                table: "AdmissionDischarges");

            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionDischarges_Patients_PatientId",
                table: "AdmissionDischarges");

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDischarges_Doctors_DoctorId",
                table: "AdmissionDischarges",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionDischarges_Patients_PatientId",
                table: "AdmissionDischarges",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
