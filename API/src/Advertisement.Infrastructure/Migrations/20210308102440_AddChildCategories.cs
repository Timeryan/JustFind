using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class AddChildCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "0ca89e4f-61de-4767-9b8d-aa988d90af8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "071f8ebf-52b7-4961-9b57-4545aa5c1275");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70816e38-00fe-46c0-8058-9db016e75553", "AQAAAAEAACcQAAAAEF4poRG8tKVyNdxL5AhTph0XwWmbRGLyO6kjjhWz26sh85ScJPBdBgRm7mSRyTw59w==", "c27cfccb-549a-463a-a88e-2153d21df260" });
        }
    }
}
