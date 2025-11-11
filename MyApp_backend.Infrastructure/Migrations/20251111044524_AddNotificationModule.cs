using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("3161e5c7-3faa-4970-aa60-2cdb7da84544"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3161e5c7-3faa-4970-aa60-2cdb7da84544"));

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RecipientUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecipientProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"), 0, null, "1806d23d-f087-4129-bcdf-60301585b61a", new DateTime(2025, 11, 11, 4, 45, 24, 505, DateTimeKind.Utc).AddTicks(4581), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEP4saXJQpyGjt4DGPHfcVTtI/4jn4rGh5A1IVcb4NXLPhUVyZhgnlZH98Y7CaRG2ow==", "+1234567890", false, null, null, null, "4694deea-ab2e-48e3-b218-7d39d1e4d309", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 11, 4, 45, 24, 542, DateTimeKind.Utc).AddTicks(2713), null, "[]", new Guid("5d4e80d5-8cd1-4b7b-b590-e8a987c0c056"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("48bc68ae-a45c-48c5-80f6-9bde4022b63c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("3161e5c7-3faa-4970-aa60-2cdb7da84544"), 0, null, "174539d3-683d-4a10-b73f-0c58a69b4bf6", new DateTime(2025, 11, 8, 4, 41, 22, 73, DateTimeKind.Utc).AddTicks(8612), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEP4gMJfaQK1pQAgav+w8kVDlvRi0zpvBC2UudlkAfXWuWs46CapGRwtZG9d0L/USUQ==", "+1234567890", false, null, null, null, "5b64e790-b366-4220-b7fb-ee49804aa171", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("3161e5c7-3faa-4970-aa60-2cdb7da84544"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 8, 4, 41, 22, 112, DateTimeKind.Utc).AddTicks(7040), null, "[]", new Guid("75c769bf-3e1a-4889-a0bd-4c43c62ecb23"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }
    }
}
