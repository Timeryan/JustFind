using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class PetitionReviewResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DecisionEnum",
                table: "Petitions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReviewResult",
                table: "Petitions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DomainUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "8442868b-7409-4fa5-b87c-6403b03770b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "9dc1a45e-b5fe-4a4f-b9cf-eb01c98c5525");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c59f45fa-a4c3-43fe-b0ea-4de54cdc8628", "AQAAAAEAACcQAAAAEMyYMP3sjmNT0kzVVucSB/Ke7T0QEAYLguJi7in1o6mbwoPh/rY43rFIXsBtmBYOgw==", "4b03b81d-c7fc-42da-8304-2548be85cccf" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecisionEnum",
                table: "Petitions");

            migrationBuilder.DropColumn(
                name: "ReviewResult",
                table: "Petitions");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "DomainUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "185230d2-58d8-4e29-aefd-a257fb82a150",
                column: "ConcurrencyStamp",
                value: "63ef3a9e-5a43-48e8-b741-ae7fc157244e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3300ca5-846f-4e6b-ac5f-1d3933115e67",
                column: "ConcurrencyStamp",
                value: "adc4439a-585d-479d-bbaf-ef789680bf22");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "98b651ae-c9aa-4731-9996-57352d525f7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e3b6a45-78a6-4f27-a8ab-fca46a4f3085", "AQAAAAEAACcQAAAAEKIgd4HqHuunq55PX98/sJ5Du0Xs3DnkPppag2Vu7xB5t59Gvo1/9k+zDZ1w6R/Ljw==", "530c6fea-75df-41af-816c-af6ccdcde91e" });
        }
    }
}
