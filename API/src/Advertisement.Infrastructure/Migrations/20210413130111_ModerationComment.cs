using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class ModerationComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "ModerationComment",
                table: "Ads",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "ec8f902a-e980-4970-88c1-8ba9baf1cf58");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "61708d38-1bd2-4df5-9c42-9b6d42472cd0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9401b89-0d58-4a05-bd95-e906ecec96a5", "AQAAAAEAACcQAAAAEDZfRHFQ1o9q432mtBWv+3m8KHoxOpBmNx1twUYS3ZcGhA376JuozCYLKwSIcEdLmg==", "891065e6-562f-4b31-b228-7a794b9e516a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationComment",
                table: "Ads");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "e9514ae1-c45c-417c-8beb-c9de9eef72d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "82de0aee-95c6-4c8c-92b7-2784d7bb45a9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78afd3b9-8300-4b47-9980-80aba3f41637", "AQAAAAEAACcQAAAAECKA2NB1cUHEw06SlwqCJyDNd5pzb8TZDEvQ+3xCWiN+Ms+OIbm0HfDO8wZxvvy8og==", "9199d646-8304-4825-a3e7-4796efdf0636" });
        }
    }
}
