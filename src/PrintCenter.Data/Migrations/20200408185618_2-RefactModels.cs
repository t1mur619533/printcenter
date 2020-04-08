using Microsoft.EntityFrameworkCore.Migrations;

namespace PrintCenter.Data.Migrations
{
    public partial class _2RefactModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompositeSerialProductionSerialProduction_CompositeSerialPr~",
                table: "CompositeSerialProductionSerialProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_CompositeSerialProductionSerialProduction_SerialProductions~",
                table: "CompositeSerialProductionSerialProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionSerialProduction_MaterialConsumptions_Ma~",
                table: "MaterialConsumptionSerialProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionSerialProduction_SerialProductions_Seria~",
                table: "MaterialConsumptionSerialProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionStream_MaterialConsumptions_MaterialCons~",
                table: "MaterialConsumptionStream");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionStream_Streams_StreamId",
                table: "MaterialConsumptionStream");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialConsumptionStream",
                table: "MaterialConsumptionStream");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialConsumptionSerialProduction",
                table: "MaterialConsumptionSerialProduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompositeSerialProductionSerialProduction",
                table: "CompositeSerialProductionSerialProduction");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "MaterialConsumptionStream",
                newName: "MaterialConsumptionStreams");

            migrationBuilder.RenameTable(
                name: "MaterialConsumptionSerialProduction",
                newName: "MaterialConsumptionSerialProductions");

            migrationBuilder.RenameTable(
                name: "CompositeSerialProductionSerialProduction",
                newName: "CompositeSerialProductionSerialProductions");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialConsumptionStream_StreamId",
                table: "MaterialConsumptionStreams",
                newName: "IX_MaterialConsumptionStreams_StreamId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialConsumptionSerialProduction_SerialProductionId",
                table: "MaterialConsumptionSerialProductions",
                newName: "IX_MaterialConsumptionSerialProductions_SerialProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_CompositeSerialProductionSerialProduction_SerialProductionId",
                table: "CompositeSerialProductionSerialProductions",
                newName: "IX_CompositeSerialProductionSerialProductions_SerialProduction~");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialConsumptionStreams",
                table: "MaterialConsumptionStreams",
                columns: new[] { "MaterialConsumptionId", "StreamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialConsumptionSerialProductions",
                table: "MaterialConsumptionSerialProductions",
                columns: new[] { "MaterialConsumptionId", "SerialProductionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompositeSerialProductionSerialProductions",
                table: "CompositeSerialProductionSerialProductions",
                columns: new[] { "CompositeSerialProductionId", "SerialProductionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompositeSerialProductionSerialProductions_CompositeSerialP~",
                table: "CompositeSerialProductionSerialProductions",
                column: "CompositeSerialProductionId",
                principalTable: "CompositeSerialProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompositeSerialProductionSerialProductions_SerialProduction~",
                table: "CompositeSerialProductionSerialProductions",
                column: "SerialProductionId",
                principalTable: "SerialProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionSerialProductions_MaterialConsumptions_M~",
                table: "MaterialConsumptionSerialProductions",
                column: "MaterialConsumptionId",
                principalTable: "MaterialConsumptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionSerialProductions_SerialProductions_Seri~",
                table: "MaterialConsumptionSerialProductions",
                column: "SerialProductionId",
                principalTable: "SerialProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionStreams_MaterialConsumptions_MaterialCon~",
                table: "MaterialConsumptionStreams",
                column: "MaterialConsumptionId",
                principalTable: "MaterialConsumptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionStreams_Streams_StreamId",
                table: "MaterialConsumptionStreams",
                column: "StreamId",
                principalTable: "Streams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompositeSerialProductionSerialProductions_CompositeSerialP~",
                table: "CompositeSerialProductionSerialProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_CompositeSerialProductionSerialProductions_SerialProduction~",
                table: "CompositeSerialProductionSerialProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionSerialProductions_MaterialConsumptions_M~",
                table: "MaterialConsumptionSerialProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionSerialProductions_SerialProductions_Seri~",
                table: "MaterialConsumptionSerialProductions");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionStreams_MaterialConsumptions_MaterialCon~",
                table: "MaterialConsumptionStreams");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptionStreams_Streams_StreamId",
                table: "MaterialConsumptionStreams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialConsumptionStreams",
                table: "MaterialConsumptionStreams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialConsumptionSerialProductions",
                table: "MaterialConsumptionSerialProductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompositeSerialProductionSerialProductions",
                table: "CompositeSerialProductionSerialProductions");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "MaterialConsumptionStreams",
                newName: "MaterialConsumptionStream");

            migrationBuilder.RenameTable(
                name: "MaterialConsumptionSerialProductions",
                newName: "MaterialConsumptionSerialProduction");

            migrationBuilder.RenameTable(
                name: "CompositeSerialProductionSerialProductions",
                newName: "CompositeSerialProductionSerialProduction");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialConsumptionStreams_StreamId",
                table: "MaterialConsumptionStream",
                newName: "IX_MaterialConsumptionStream_StreamId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialConsumptionSerialProductions_SerialProductionId",
                table: "MaterialConsumptionSerialProduction",
                newName: "IX_MaterialConsumptionSerialProduction_SerialProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_CompositeSerialProductionSerialProductions_SerialProduction~",
                table: "CompositeSerialProductionSerialProduction",
                newName: "IX_CompositeSerialProductionSerialProduction_SerialProductionId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialConsumptionStream",
                table: "MaterialConsumptionStream",
                columns: new[] { "MaterialConsumptionId", "StreamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialConsumptionSerialProduction",
                table: "MaterialConsumptionSerialProduction",
                columns: new[] { "MaterialConsumptionId", "SerialProductionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompositeSerialProductionSerialProduction",
                table: "CompositeSerialProductionSerialProduction",
                columns: new[] { "CompositeSerialProductionId", "SerialProductionId" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Name", "Password", "Role", "Surname" },
                values: new object[] { 1, "admin", "admin", "admin", 4, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompositeSerialProductionSerialProduction_CompositeSerialPr~",
                table: "CompositeSerialProductionSerialProduction",
                column: "CompositeSerialProductionId",
                principalTable: "CompositeSerialProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompositeSerialProductionSerialProduction_SerialProductions~",
                table: "CompositeSerialProductionSerialProduction",
                column: "SerialProductionId",
                principalTable: "SerialProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionSerialProduction_MaterialConsumptions_Ma~",
                table: "MaterialConsumptionSerialProduction",
                column: "MaterialConsumptionId",
                principalTable: "MaterialConsumptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionSerialProduction_SerialProductions_Seria~",
                table: "MaterialConsumptionSerialProduction",
                column: "SerialProductionId",
                principalTable: "SerialProductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionStream_MaterialConsumptions_MaterialCons~",
                table: "MaterialConsumptionStream",
                column: "MaterialConsumptionId",
                principalTable: "MaterialConsumptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptionStream_Streams_StreamId",
                table: "MaterialConsumptionStream",
                column: "StreamId",
                principalTable: "Streams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
