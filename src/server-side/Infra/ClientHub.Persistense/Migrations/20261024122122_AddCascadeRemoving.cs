using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientHub.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeRemoving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArbitrageDeals_Clients_ClientDaoId",
                table: "ArbitrageDeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiars_LegalEntities_LegalEntityDaoId",
                table: "Beneficiars");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientActions_Clients_ClientDaoId",
                table: "ClientActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContacts_Clients_ClientDaoId",
                table: "ClientContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientEvents_Clients_ClientDaoId",
                table: "ClientEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Clients_ClientDaoId",
                table: "Courts");

            migrationBuilder.DropForeignKey(
                name: "FK_Enforcements_Clients_ClientDaoId",
                table: "Enforcements");

            migrationBuilder.DropForeignKey(
                name: "FK_FounderPersons_LegalEntities_LegalEntityDaoId",
                table: "FounderPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpps_LegalEntities_LegalEntityDaoId",
                table: "Kpps");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintainers_LegalEntities_LegalEntityDaoId",
                table: "Maintainers");

            migrationBuilder.DropIndex(
                name: "IX_LegalEntities_ClientId",
                table: "LegalEntities");

            migrationBuilder.DropIndex(
                name: "IX_IndividualEntrepreneurs_ClientId",
                table: "IndividualEntrepreneurs");

            migrationBuilder.DropIndex(
                name: "IX_IndividualEntrepreneurs_RequisiteId",
                table: "IndividualEntrepreneurs");

            migrationBuilder.AddColumn<int>(
                name: "LegalEntityDaoId",
                table: "FounderClients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntities_ClientId",
                table: "LegalEntities",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEntrepreneurs_ClientId",
                table: "IndividualEntrepreneurs",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEntrepreneurs_RequisiteId",
                table: "IndividualEntrepreneurs",
                column: "RequisiteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FounderClients_LegalEntityDaoId",
                table: "FounderClients",
                column: "LegalEntityDaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitrageDeals_Clients_ClientDaoId",
                table: "ArbitrageDeals",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiars_LegalEntities_LegalEntityDaoId",
                table: "Beneficiars",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientActions_Clients_ClientDaoId",
                table: "ClientActions",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContacts_Clients_ClientDaoId",
                table: "ClientContacts",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEvents_Clients_ClientDaoId",
                table: "ClientEvents",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Clients_ClientDaoId",
                table: "Courts",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enforcements_Clients_ClientDaoId",
                table: "Enforcements",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FounderClients_LegalEntities_LegalEntityDaoId",
                table: "FounderClients",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FounderPersons_LegalEntities_LegalEntityDaoId",
                table: "FounderPersons",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kpps_LegalEntities_LegalEntityDaoId",
                table: "Kpps",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainers_LegalEntities_LegalEntityDaoId",
                table: "Maintainers",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArbitrageDeals_Clients_ClientDaoId",
                table: "ArbitrageDeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiars_LegalEntities_LegalEntityDaoId",
                table: "Beneficiars");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientActions_Clients_ClientDaoId",
                table: "ClientActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContacts_Clients_ClientDaoId",
                table: "ClientContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientEvents_Clients_ClientDaoId",
                table: "ClientEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Clients_ClientDaoId",
                table: "Courts");

            migrationBuilder.DropForeignKey(
                name: "FK_Enforcements_Clients_ClientDaoId",
                table: "Enforcements");

            migrationBuilder.DropForeignKey(
                name: "FK_FounderClients_LegalEntities_LegalEntityDaoId",
                table: "FounderClients");

            migrationBuilder.DropForeignKey(
                name: "FK_FounderPersons_LegalEntities_LegalEntityDaoId",
                table: "FounderPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpps_LegalEntities_LegalEntityDaoId",
                table: "Kpps");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintainers_LegalEntities_LegalEntityDaoId",
                table: "Maintainers");

            migrationBuilder.DropIndex(
                name: "IX_LegalEntities_ClientId",
                table: "LegalEntities");

            migrationBuilder.DropIndex(
                name: "IX_IndividualEntrepreneurs_ClientId",
                table: "IndividualEntrepreneurs");

            migrationBuilder.DropIndex(
                name: "IX_IndividualEntrepreneurs_RequisiteId",
                table: "IndividualEntrepreneurs");

            migrationBuilder.DropIndex(
                name: "IX_FounderClients_LegalEntityDaoId",
                table: "FounderClients");

            migrationBuilder.DropColumn(
                name: "LegalEntityDaoId",
                table: "FounderClients");

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntities_ClientId",
                table: "LegalEntities",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEntrepreneurs_ClientId",
                table: "IndividualEntrepreneurs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEntrepreneurs_RequisiteId",
                table: "IndividualEntrepreneurs",
                column: "RequisiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArbitrageDeals_Clients_ClientDaoId",
                table: "ArbitrageDeals",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiars_LegalEntities_LegalEntityDaoId",
                table: "Beneficiars",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientActions_Clients_ClientDaoId",
                table: "ClientActions",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContacts_Clients_ClientDaoId",
                table: "ClientContacts",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEvents_Clients_ClientDaoId",
                table: "ClientEvents",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Clients_ClientDaoId",
                table: "Courts",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enforcements_Clients_ClientDaoId",
                table: "Enforcements",
                column: "ClientDaoId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FounderPersons_LegalEntities_LegalEntityDaoId",
                table: "FounderPersons",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpps_LegalEntities_LegalEntityDaoId",
                table: "Kpps",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainers_LegalEntities_LegalEntityDaoId",
                table: "Maintainers",
                column: "LegalEntityDaoId",
                principalTable: "LegalEntities",
                principalColumn: "Id");
        }
    }
}
