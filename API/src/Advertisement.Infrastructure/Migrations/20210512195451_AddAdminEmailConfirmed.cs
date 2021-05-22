using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class AddAdminEmailConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "6584a50e-c162-4079-8941-1c4ed55a58e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "f2ea4efc-8b98-4c2d-84fa-3b5e2c779b82");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4a51d6a-094c-42d5-b854-993da01a6dd6", true, "AQAAAAEAACcQAAAAEN41gRcozGp2OH9F0hRIwLm3kacGbzGV/BV7UcGmuxLCwWVoG1hDvqi78bey2uYKdA==", "4cea42e7-e9f1-40d5-82ea-e81aa1f77a42" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "42062134-c5bf-4ce0-8b46-db4828e8459e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "75d941cf-7aca-4b28-86bb-99e26826321f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7181a199-40ad-43b0-b8aa-c13c7625479d", false, "AQAAAAEAACcQAAAAEKWJO1cDA/kwbyEzjVghfbGk4gbtKtE/Jdt56acddOJRNKCQl4jD8wh9scx64nQXDA==", "a92480a0-0e06-4129-aaa1-bcfed55db2eb" });
        }
    }
}
