using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPayment_Module : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("0cb7d4e4-cb7c-4001-81f9-dcdb8f710426"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0cb7d4e4-cb7c-4001-81f9-dcdb8f710426"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("47a2fccb-3e77-4ada-9c5b-dcb627840abe"), 0, null, "19e9d6ab-7d11-4dd4-b7ef-85a2bbaa9540", new DateTime(2025, 10, 30, 5, 22, 32, 437, DateTimeKind.Utc).AddTicks(1911), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEH991Ceo6BjoEMFpTvSMt04A0CbqQXgyBBvn9OemSvwQQXirhstLwf0bDXNMB+Sk+w==", "+1234567890", false, null, null, null, "67ec4139-36f2-43d6-98d4-c2dfb694824d", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("47a2fccb-3e77-4ada-9c5b-dcb627840abe"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 10, 30, 5, 22, 32, 473, DateTimeKind.Utc).AddTicks(903), null, "[]", new Guid("99e0157d-94e3-4627-b1f6-ab9cf954ed6b"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("47a2fccb-3e77-4ada-9c5b-dcb627840abe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("47a2fccb-3e77-4ada-9c5b-dcb627840abe"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("0cb7d4e4-cb7c-4001-81f9-dcdb8f710426"), 0, null, "389ac2af-2f8f-4a89-8d1a-f91362507a10", new DateTime(2025, 10, 30, 5, 20, 5, 999, DateTimeKind.Utc).AddTicks(3547), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEFqoNSBpr5Ke9CpD8s7vrN74RNS0sUxFjHij8KhNRYxArB8TK4bTpRaAPd6a76Ji/Q==", "+1234567890", false, null, null, null, "40b46570-dcc6-42cf-8a78-369168b2ca06", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("0cb7d4e4-cb7c-4001-81f9-dcdb8f710426"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 10, 30, 5, 20, 6, 45, DateTimeKind.Utc).AddTicks(7134), null, "[]", new Guid("9a64e81e-31ba-45ab-a961-9c66ca3a6c23"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }
    }
}
