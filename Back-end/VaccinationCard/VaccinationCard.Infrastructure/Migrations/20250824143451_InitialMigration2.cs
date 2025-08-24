using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationCard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationRecords_Persons_PersonId",
                table: "VaccinationRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationRecords_Vaccines_VaccineId",
                table: "VaccinationRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccinationRecords",
                table: "VaccinationRecords");

            migrationBuilder.RenameTable(
                name: "VaccinationRecords",
                newName: "Vaccination");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinationRecords_VaccineId",
                table: "Vaccination",
                newName: "IX_Vaccination_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinationRecords_PersonId",
                table: "Vaccination",
                newName: "IX_Vaccination_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccination",
                table: "Vaccination",
                columns: new[] { "EntityId", "VaccineId", "PersonId", "DoseNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_Persons_PersonId",
                table: "Vaccination",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_Vaccines_VaccineId",
                table: "Vaccination",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_Persons_PersonId",
                table: "Vaccination");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_Vaccines_VaccineId",
                table: "Vaccination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccination",
                table: "Vaccination");

            migrationBuilder.RenameTable(
                name: "Vaccination",
                newName: "VaccinationRecords");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccination_VaccineId",
                table: "VaccinationRecords",
                newName: "IX_VaccinationRecords_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccination_PersonId",
                table: "VaccinationRecords",
                newName: "IX_VaccinationRecords_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccinationRecords",
                table: "VaccinationRecords",
                columns: new[] { "EntityId", "VaccineId", "PersonId", "DoseNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationRecords_Persons_PersonId",
                table: "VaccinationRecords",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationRecords_Vaccines_VaccineId",
                table: "VaccinationRecords",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "EntityId");
        }
    }
}
