using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRazorpayFieldsToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"));

            migrationBuilder.AddColumn<string>(
                name: "RazorpayOrderId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpayPaymentId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazorpaySignature",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("254dc4c0-05de-4a54-904b-2ff7b9ad33b7"), 0, null, "dcdfdc62-c846-46c7-b10a-d2097bdbdece", new DateTime(2025, 10, 30, 12, 4, 40, 422, DateTimeKind.Utc).AddTicks(7230), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEJyX0rknWdhFyr5obDCcHeGLislVhP26DBWR2XQX3ebxcEmGA37/vgAJqqUMd/dxyw==", "+1234567890", false, null, null, null, "92b80c38-1793-49b6-b43e-46df9e8fb74d", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("254dc4c0-05de-4a54-904b-2ff7b9ad33b7"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 10, 30, 12, 4, 40, 460, DateTimeKind.Utc).AddTicks(1118), null, "[]", new Guid("988c524a-049e-45f0-b405-49f02de758c8"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("254dc4c0-05de-4a54-904b-2ff7b9ad33b7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("254dc4c0-05de-4a54-904b-2ff7b9ad33b7"));

            migrationBuilder.DropColumn(
                name: "RazorpayOrderId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "RazorpayPaymentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "RazorpaySignature",
                table: "Payments");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"), 0, null, "9d156635-9513-495d-8b05-758bda3fad80", new DateTime(2025, 10, 30, 9, 8, 52, 758, DateTimeKind.Utc).AddTicks(5025), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEHy8bPNuz+J7Wptbsw0hIgNCj6gACyYenVUisJikpt9OfIHf5fhEAXph3zMHsCWVZA==", "+1234567890", false, null, null, null, "81fdfca6-6330-4e68-bedf-188df8ae8b2d", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 10, 30, 9, 8, 52, 794, DateTimeKind.Utc).AddTicks(9843), null, "[]", new Guid("047387df-b791-4144-b537-4e013661b985"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }
    }
}
