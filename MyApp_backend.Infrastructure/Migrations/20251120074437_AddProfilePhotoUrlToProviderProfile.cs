using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePhotoUrlToProviderProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"));

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoUrl",
                table: "ProviderProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"), 0, null, "7f7c4962-96b0-4218-86ca-80af5b41844c", new DateTime(2025, 11, 20, 7, 44, 36, 626, DateTimeKind.Utc).AddTicks(1865), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEFAdMSjbs27v3l4U6BqrOBvHD1BWEtuGEuMRbBjoK6UfQZZEGtBMBOzYEDuL2fEfeA==", "+1234567890", false, null, null, null, "06fcd1bc-77d1-42f3-9c35-d3c6dbbea8a9", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ProfilePhotoUrl", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 20, 7, 44, 36, 664, DateTimeKind.Utc).AddTicks(8112), null, "[]", new Guid("ada7ee31-be0a-4ef4-bb5a-3cf7f7b7e963"), true, true, false, null, null, null, "[]", null, "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"));

            migrationBuilder.DropColumn(
                name: "ProfilePhotoUrl",
                table: "ProviderProfiles");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"), 0, null, "1806d23d-f087-4129-bcdf-60301585b61a", new DateTime(2025, 11, 11, 4, 45, 24, 505, DateTimeKind.Utc).AddTicks(4581), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEP4saXJQpyGjt4DGPHfcVTtI/4jn4rGh5A1IVcb4NXLPhUVyZhgnlZH98Y7CaRG2ow==", "+1234567890", false, null, null, null, "4694deea-ab2e-48e3-b218-7d39d1e4d309", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 11, 4, 45, 24, 542, DateTimeKind.Utc).AddTicks(2713), null, "[]", new Guid("5d4e80d5-8cd1-4b7b-b590-e8a987c0c056"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }
    }
}
