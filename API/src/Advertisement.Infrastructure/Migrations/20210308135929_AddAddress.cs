using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class AddAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Ads");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Ads",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KladrId = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    X = table.Column<string>(type: "text", nullable: false),
                    Y = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "3f784051-0a58-4383-865f-e634d3411991");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "2a0f6c5e-95db-4fcc-b3ce-129daa93570f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2baa0729-e32e-4c89-970c-96966e869a60", "AQAAAAEAACcQAAAAEPkbGCoHliPuL9fr33ykv6lWZDVJoXGArT/CkntaU1YKPH3ICOTnUNR+HbuzqC2oRg==", "d67ba92c-b67c-4425-a760-e5aa4414be8d" });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_LocationId",
                table: "Ads",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Address_LocationId",
                table: "Ads",
                column: "LocationId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Address_LocationId",
                table: "Ads");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Ads_LocationId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Ads");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Ads",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "20269cd6-a8ee-4337-b7a7-f06b9028d661");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "b2065ce1-f2e1-4eef-9e9c-0c4f2cecbed4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5c178f2-b5a2-4f22-9aaa-822197419f5b", "AQAAAAEAACcQAAAAELF6Rjx3WFtliHCmfQcLP0wBlB0ODKY9yF7Bm3nFNVEWMPurQW5EDkDLl4+8DoRO+Q==", "4a11b404-cd36-4412-85bf-7ae084f59444" });
        }
    }
}
