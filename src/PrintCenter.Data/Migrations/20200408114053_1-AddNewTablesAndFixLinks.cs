using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PrintCenter.Data.Migrations
{
    public partial class _1AddNewTablesAndFixLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptions_Streams_StreamId",
                table: "MaterialConsumptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialProductions_CompositeSerialProductions_CompositeSeria~",
                table: "SerialProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Users_UserId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_UserId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_SerialProductions_CompositeSerialProductionId",
                table: "SerialProductions");

            migrationBuilder.DropIndex(
                name: "IX_MaterialConsumptions_StreamId",
                table: "MaterialConsumptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "CompositeSerialProductionId",
                table: "SerialProductions");

            migrationBuilder.DropColumn(
                name: "StreamId",
                table: "MaterialConsumptions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompositeSerialProductions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CompositeSerialProductions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CompositeSerialProductionSerialProduction",
                columns: table => new
                {
                    CompositeSerialProductionId = table.Column<int>(nullable: false),
                    SerialProductionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositeSerialProductionSerialProduction", x => new { x.CompositeSerialProductionId, x.SerialProductionId });
                    table.ForeignKey(
                        name: "FK_CompositeSerialProductionSerialProduction_CompositeSerialPr~",
                        column: x => x.CompositeSerialProductionId,
                        principalTable: "CompositeSerialProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompositeSerialProductionSerialProduction_SerialProductions~",
                        column: x => x.SerialProductionId,
                        principalTable: "SerialProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialConsumptionSerialProduction",
                columns: table => new
                {
                    MaterialConsumptionId = table.Column<int>(nullable: false),
                    SerialProductionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialConsumptionSerialProduction", x => new { x.MaterialConsumptionId, x.SerialProductionId });
                    table.ForeignKey(
                        name: "FK_MaterialConsumptionSerialProduction_MaterialConsumptions_Ma~",
                        column: x => x.MaterialConsumptionId,
                        principalTable: "MaterialConsumptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialConsumptionSerialProduction_SerialProductions_Seria~",
                        column: x => x.SerialProductionId,
                        principalTable: "SerialProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialConsumptionStream",
                columns: table => new
                {
                    MaterialConsumptionId = table.Column<int>(nullable: false),
                    StreamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialConsumptionStream", x => new { x.MaterialConsumptionId, x.StreamId });
                    table.ForeignKey(
                        name: "FK_MaterialConsumptionStream_MaterialConsumptions_MaterialCons~",
                        column: x => x.MaterialConsumptionId,
                        principalTable: "MaterialConsumptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialConsumptionStream_Streams_StreamId",
                        column: x => x.StreamId,
                        principalTable: "Streams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialMovements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaterialId = table.Column<int>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Count = table.Column<float>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialMovements_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialMovements_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialMovements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTechnologies",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TechnologyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTechnologies", x => new { x.UserId, x.TechnologyId });
                    table.ForeignKey(
                        name: "FK_UserTechnologies_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTechnologies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Name", "Password", "Role", "Surname" },
                values: new object[] { 1, "admin", "admin", "admin", 4, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_CompositeSerialProductions_Code",
                table: "CompositeSerialProductions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompositeSerialProductions_Name",
                table: "CompositeSerialProductions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompositeSerialProductionSerialProduction_SerialProductionId",
                table: "CompositeSerialProductionSerialProduction",
                column: "SerialProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptionSerialProduction_SerialProductionId",
                table: "MaterialConsumptionSerialProduction",
                column: "SerialProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptionStream_StreamId",
                table: "MaterialConsumptionStream",
                column: "StreamId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialMovements_InvoiceId",
                table: "MaterialMovements",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialMovements_MaterialId",
                table: "MaterialMovements",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialMovements_UserId",
                table: "MaterialMovements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTechnologies_TechnologyId",
                table: "UserTechnologies",
                column: "TechnologyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompositeSerialProductionSerialProduction");

            migrationBuilder.DropTable(
                name: "MaterialConsumptionSerialProduction");

            migrationBuilder.DropTable(
                name: "MaterialConsumptionStream");

            migrationBuilder.DropTable(
                name: "MaterialMovements");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "UserTechnologies");

            migrationBuilder.DropIndex(
                name: "IX_CompositeSerialProductions_Code",
                table: "CompositeSerialProductions");

            migrationBuilder.DropIndex(
                name: "IX_CompositeSerialProductions_Name",
                table: "CompositeSerialProductions");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Technologies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompositeSerialProductionId",
                table: "SerialProductions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreamId",
                table: "MaterialConsumptions",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompositeSerialProductions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CompositeSerialProductions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_UserId",
                table: "Technologies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SerialProductions_CompositeSerialProductionId",
                table: "SerialProductions",
                column: "CompositeSerialProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_StreamId",
                table: "MaterialConsumptions",
                column: "StreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptions_Streams_StreamId",
                table: "MaterialConsumptions",
                column: "StreamId",
                principalTable: "Streams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SerialProductions_CompositeSerialProductions_CompositeSeria~",
                table: "SerialProductions",
                column: "CompositeSerialProductionId",
                principalTable: "CompositeSerialProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Users_UserId",
                table: "Technologies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
