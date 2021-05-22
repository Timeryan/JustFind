using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class ChangeImplementAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Addresses_LocationId",
                table: "Ads");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Ads_LocationId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Ads");

            migrationBuilder.AddColumn<long>(
                name: "LocationKladrId",
                table: "Ads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "LocationText",
                table: "Ads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationX",
                table: "Ads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationY",
                table: "Ads",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "397bffd6-9189-4549-8224-b3250a59e023");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "fde851dc-0321-4fad-82ca-a7208ce22cc4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6631d74c-4b6f-4dfe-8a22-ed553f7b9667", "AQAAAAEAACcQAAAAEPs5hVyuwRTXwNEsZdWGAv8v9S0B90LExlvpzwGI1mMF+tI2WTtRq1Q4Twu+KumsLQ==", "d780bf97-5ad8-4b1e-8f1b-ffc49b9212dd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationKladrId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "LocationText",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "LocationX",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "LocationY",
                table: "Ads");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Ads",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Addresses",
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
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "4ae9f716-24a9-4c28-a037-543265d1764f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "5b4087fd-8068-4187-bd87-59846010d4fe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "054b22e6-6797-4f06-afd3-53b7a092f983", "AQAAAAEAACcQAAAAENJFiEhuIL6dwvONjJ6KWRYEZz+1BTdek4Q9tVGZBJuZxlNx6QsRyfsaaCsc/8FNIw==", "0aaf77f1-7de3-49e2-862d-672634bacd3b" });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_LocationId",
                table: "Ads",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Addresses_LocationId",
                table: "Ads",
                column: "LocationId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
