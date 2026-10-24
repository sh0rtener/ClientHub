using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientHub.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodsMarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisassignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsMarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualEntrepreneurRequisites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    Registrator = table.Column<string>(type: "text", nullable: false),
                    TaxAuthority = table.Column<string>(type: "text", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaxRegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualEntrepreneurRequisites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonDaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    RegionCode = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDaos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inn = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ogrn = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    GoodsMarkId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LiquidationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_GoodsMarks_GoodsMarkId",
                        column: x => x.GoodsMarkId,
                        principalTable: "GoodsMarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArbitrageDeals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseNumber = table.Column<string>(type: "text", nullable: false),
                    CourtName = table.Column<string>(type: "text", nullable: false),
                    Instanse = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    PartyRole = table.Column<string>(type: "text", nullable: false),
                    CountrAgentName = table.Column<string>(type: "text", nullable: false),
                    CountrAgentInn = table.Column<string>(type: "text", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: true),
                    FillingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClientDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArbitrageDeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArbitrageDeals_Clients_ClientDaoId",
                        column: x => x.ClientDaoId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ClientDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientActions_Clients_ClientDaoId",
                        column: x => x.ClientDaoId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    ClientDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientContacts_Clients_ClientDaoId",
                        column: x => x.ClientDaoId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClientEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    SourceUrl = table.Column<string>(type: "text", nullable: false),
                    EventedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEvents_Clients_ClientDaoId",
                        column: x => x.ClientDaoId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseNumber = table.Column<string>(type: "text", nullable: false),
                    CourtName = table.Column<string>(type: "text", nullable: false),
                    Instanse = table.Column<string>(type: "text", nullable: false),
                    CaseType = table.Column<string>(type: "text", nullable: false),
                    PartyRole = table.Column<string>(type: "text", nullable: false),
                    CountrAgentName = table.Column<string>(type: "text", nullable: false),
                    CountrAgentInn = table.Column<string>(type: "text", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LiquidationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClientDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courts_Clients_ClientDaoId",
                        column: x => x.ClientDaoId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Enforcements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FsspDepartament = table.Column<string>(type: "text", nullable: false),
                    Bailiff = table.Column<string>(type: "text", nullable: false),
                    BasicDocType = table.Column<string>(type: "text", nullable: false),
                    BasicDocNumber = table.Column<string>(type: "text", nullable: false),
                    BasicDocDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Creditor = table.Column<string>(type: "text", nullable: false),
                    CreditorInn = table.Column<string>(type: "text", nullable: false),
                    DeptAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    OpenedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClosureReason = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ClientDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enforcements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enforcements_Clients_ClientDaoId",
                        column: x => x.ClientDaoId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IndividualEntrepreneurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    RequisiteId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualEntrepreneurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualEntrepreneurs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualEntrepreneurs_IndividualEntrepreneurRequisites_Re~",
                        column: x => x.RequisiteId,
                        principalTable: "IndividualEntrepreneurRequisites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Okato = table.Column<string>(type: "text", nullable: false),
                    HeadCount = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalEntities_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisassignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IncomeShareInDollars = table.Column<decimal>(type: "numeric", nullable: false),
                    IncomeShareInPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    LegalEntityDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiars_LegalEntities_LegalEntityDaoId",
                        column: x => x.LegalEntityDaoId,
                        principalTable: "LegalEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Beneficiars_PersonDaos_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonDaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FounderClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisassignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IncomeShareInDollars = table.Column<decimal>(type: "numeric", nullable: false),
                    IncomeShareInPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FounderClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FounderClients_LegalEntities_ClientId",
                        column: x => x.ClientId,
                        principalTable: "LegalEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FounderPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisassignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IncomeShareInDollars = table.Column<decimal>(type: "numeric", nullable: false),
                    IncomeShareInPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    LegalEntityDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FounderPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FounderPersons_LegalEntities_LegalEntityDaoId",
                        column: x => x.LegalEntityDaoId,
                        principalTable: "LegalEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FounderPersons_PersonDaos_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonDaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kpps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    LegalEntityDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kpps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kpps_LegalEntities_LegalEntityDaoId",
                        column: x => x.LegalEntityDaoId,
                        principalTable: "LegalEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Maintainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisassignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IncomeShareInDollars = table.Column<decimal>(type: "numeric", nullable: false),
                    IncomeShareInPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    LegalEntityDaoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintainers_LegalEntities_LegalEntityDaoId",
                        column: x => x.LegalEntityDaoId,
                        principalTable: "LegalEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maintainers_PersonDaos_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PersonDaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArbitrageDeals_ClientDaoId",
                table: "ArbitrageDeals",
                column: "ClientDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiars_LegalEntityDaoId",
                table: "Beneficiars",
                column: "LegalEntityDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiars_PersonId",
                table: "Beneficiars",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientActions_ClientDaoId",
                table: "ClientActions",
                column: "ClientDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContacts_ClientDaoId",
                table: "ClientContacts",
                column: "ClientDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEvents_ClientDaoId",
                table: "ClientEvents",
                column: "ClientDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_GoodsMarkId",
                table: "Clients",
                column: "GoodsMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Courts_ClientDaoId",
                table: "Courts",
                column: "ClientDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Enforcements_ClientDaoId",
                table: "Enforcements",
                column: "ClientDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FounderClients_ClientId",
                table: "FounderClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FounderPersons_LegalEntityDaoId",
                table: "FounderPersons",
                column: "LegalEntityDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FounderPersons_PersonId",
                table: "FounderPersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEntrepreneurs_ClientId",
                table: "IndividualEntrepreneurs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualEntrepreneurs_RequisiteId",
                table: "IndividualEntrepreneurs",
                column: "RequisiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpps_LegalEntityDaoId",
                table: "Kpps",
                column: "LegalEntityDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntities_ClientId",
                table: "LegalEntities",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainers_LegalEntityDaoId",
                table: "Maintainers",
                column: "LegalEntityDaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainers_PersonId",
                table: "Maintainers",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArbitrageDeals");

            migrationBuilder.DropTable(
                name: "Beneficiars");

            migrationBuilder.DropTable(
                name: "ClientActions");

            migrationBuilder.DropTable(
                name: "ClientContacts");

            migrationBuilder.DropTable(
                name: "ClientEvents");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Enforcements");

            migrationBuilder.DropTable(
                name: "FounderClients");

            migrationBuilder.DropTable(
                name: "FounderPersons");

            migrationBuilder.DropTable(
                name: "IndividualEntrepreneurs");

            migrationBuilder.DropTable(
                name: "Kpps");

            migrationBuilder.DropTable(
                name: "Maintainers");

            migrationBuilder.DropTable(
                name: "IndividualEntrepreneurRequisites");

            migrationBuilder.DropTable(
                name: "LegalEntities");

            migrationBuilder.DropTable(
                name: "PersonDaos");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "GoodsMarks");
        }
    }
}
