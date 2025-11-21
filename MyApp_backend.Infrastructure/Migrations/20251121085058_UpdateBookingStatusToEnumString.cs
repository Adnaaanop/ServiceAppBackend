using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp_backend.Infrastructure.Migrations
{
    public partial class UpdateBookingStatusToEnumString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alter Booking.Status column to store enum as string
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(25)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int"
            );

            // --- Your existing data seeding ---
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("01f659cd-1a59-443f-9b28-b506cd7c8262"), 0, null, "12071479-df6e-4bf1-be36-de3b0fe51561", new DateTime(2025, 11, 21, 8, 48, 13, 599, DateTimeKind.Utc).AddTicks(6469), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEDgHRCkadkd4Ph3d34LK+3xul0oh7SBJuU0x8GJw8vNr4zqleWEH4ZhEhESYmVdbpA==", "+1234567890", false, null, null, null, "351517e9-da3b-42d5-814e-293f816bdcea", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ProfilePhotoUrl", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("01f659cd-1a59-443f-9b28-b506cd7c8262"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 21, 8, 48, 13, 641, DateTimeKind.Utc).AddTicks(1118), null, "[]", new Guid("de77f49a-6e6f-453d-9119-803836c2fa8f"), true, true, false, null, null, null, "[]", null, "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert Booking.Status to int if needed
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)"
            );

            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("01f659cd-1a59-443f-9b28-b506cd7c8262"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01f659cd-1a59-443f-9b28-b506cd7c8262"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"), 0, null, "7f7c4962-96b0-4218-86ca-80af5b41844c", new DateTime(2025, 11, 20, 7, 44, 36, 626, DateTimeKind.Utc).AddTicks(1865), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEFAdMSjbs27v3l4U6BqrOBvHD1BWEtuGEuMRbBjoK6UfQZZEGtBMBOzYEDuL2fEfeA==", "+1234567890", false, null, null, null, "06fcd1bc-77d1-42f3-9c35-d3c6dbbea8a9", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ProfilePhotoUrl", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("58bc95e1-ce64-4119-a0b2-dccea4c1494a"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 20, 7, 44, 36, 664, DateTimeKind.Utc).AddTicks(8112), null, "[]", new Guid("ada7ee31-be0a-4ef4-bb5a-3cf7f7b7e963"), true, true, false, null, null, null, "[]", null, "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }
    }
}
