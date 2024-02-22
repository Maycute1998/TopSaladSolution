using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopSaladSolution.Infrastructure.Migrations
{
    public partial class RolesSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppUserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AppUserTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("22819c23-06bb-484b-8e0d-343b390df2a9"), "2e737bc6-556b-48de-b9ab-efba3e5e8e9a", "TopSalad Staff Manager", "StaffManager", "STAFFMANAGER" },
                    { new Guid("2823aa5c-643b-4095-a3ea-99dbd4d8e3af"), "cb5c6109-b201-403a-85b9-995ac5dbbcba", "TopSalad Staff", "Staff", "STAFF" },
                    { new Guid("ad823447-d39d-47ad-b529-bdbd4de94bb7"), "fda3a611-b256-488e-bccc-7ce2b3c78ac2", "TopSalad User", "User", "USER" },
                    { new Guid("c3fcc50f-072a-4f85-8d19-4ed727ae0282"), "bbfea170-8875-4fe7-8eb9-754617471918", "TopSalad Admin", "admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("22819c23-06bb-484b-8e0d-343b390df2a9"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("2823aa5c-643b-4095-a3ea-99dbd4d8e3af"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad823447-d39d-47ad-b529-bdbd4de94bb7"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("c3fcc50f-072a-4f85-8d19-4ed727ae0282"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppUserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AppUserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AppUserLogins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
