using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientHub.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonToIP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "IndividualEntrepreneurs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEntrepreneurs_PersonId",
                table: "IndividualEntrepreneurs",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualEntrepreneurs_PersonDaos_PersonId",
                table: "IndividualEntrepreneurs",
                column: "PersonId",
                principalTable: "PersonDaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualEntrepreneurs_PersonDaos_PersonId",
                table: "IndividualEntrepreneurs");

            migrationBuilder.DropIndex(
                name: "IX_IndividualEntrepreneurs_PersonId",
                table: "IndividualEntrepreneurs");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "IndividualEntrepreneurs");
        }
    }
}
