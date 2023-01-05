using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoicing.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyContact = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CompanyUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientSecret = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "000000"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyProviders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyProviders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CompanyContact", "CompanyName", "CompanyUrl" },
                values: new object[,]
                {
                    { 1, null, "Company ABC", null },
                    { 2, null, "Company DEF", null },
                    { 3, null, "Company XYZ", null }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "ProviderCode", "ProviderName" },
                values: new object[] { 1, "XERO", "Xero" });

            migrationBuilder.InsertData(
                table: "CompanyProviders",
                columns: new[] { "Id", "CompanyId", "IsActive", "ProviderId" },
                values: new object[] { 1, 1, true, 1 });

            migrationBuilder.InsertData(
                table: "CompanyProviders",
                columns: new[] { "Id", "CompanyId", "IsActive", "ProviderId" },
                values: new object[] { 2, 2, true, 1 });

            migrationBuilder.InsertData(
                table: "CompanyProviders",
                columns: new[] { "Id", "CompanyId", "IsActive", "ProviderId" },
                values: new object[] { 3, 3, true, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProviders_CompanyId",
                table: "CompanyProviders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProviders_ProviderId",
                table: "CompanyProviders",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyProviders");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
