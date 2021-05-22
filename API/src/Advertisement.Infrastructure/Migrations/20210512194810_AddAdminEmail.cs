using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class AddAdminEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7181a199-40ad-43b0-b8aa-c13c7625479d", "admin@admin.com", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEKWJO1cDA/kwbyEzjVghfbGk4gbtKtE/Jdt56acddOJRNKCQl4jD8wh9scx64nQXDA==", "a92480a0-0e06-4129-aaa1-bcfed55db2eb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "6c1b220b-2704-484d-8cb7-d3c05a62a954");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "afeeb5fd-174d-41b2-b1b7-152d7734e538");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc30e874-f032-4049-bc14-0df662773273", null, null, "AQAAAAEAACcQAAAAEIMj9JzRVlhNFzIXzAbch1oGur7ZbWYisIGejRMAkHLZ2X4Nqai5akRpYzvlb6h2BA==", "b3c4420b-c67b-4dcf-b5e5-7d18d9bb6589" });
        }
    }
}
