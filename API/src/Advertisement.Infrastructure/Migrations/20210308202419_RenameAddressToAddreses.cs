using Microsoft.EntityFrameworkCore.Migrations;

namespace Advertisement.Infrastructure.Migrations
{
    public partial class RenameAddressToAddreses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Address_LocationId",
                table: "Ads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Addresses_LocationId",
                table: "Ads",
                column: "LocationId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Addresses_LocationId",
                table: "Ads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Address_LocationId",
                table: "Ads",
                column: "LocationId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
