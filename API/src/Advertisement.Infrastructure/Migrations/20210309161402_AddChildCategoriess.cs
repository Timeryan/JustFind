using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class AddChildCategoriess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "534bd128-d837-44a5-ace7-3dd95314d4a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "79ee4866-6d55-45ee-8dd7-775ab25b8c1a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac63bef3-e7a3-4852-b3da-c5537f4b2b1e", "AQAAAAEAACcQAAAAEGe8K/xl4V175wVMGphvSQfmspyTNozP9afeWdMz2gfjwTfcQ2Y8hGFyFkbWonddyA==", "6d466a3e-df7e-4ce6-a077-0386cdfb6271" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
