using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentsReviewsPricingRulesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("c4fbf52c-8e13-40af-985f-bfc3b6b4dee7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4fbf52c-8e13-40af-985f-bfc3b6b4dee7"));

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastLogout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PricingRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastLogout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastLogout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"), 0, null, "9d156635-9513-495d-8b05-758bda3fad80", new DateTime(2025, 10, 30, 9, 8, 52, 758, DateTimeKind.Utc).AddTicks(5025), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEHy8bPNuz+J7Wptbsw0hIgNCj6gACyYenVUisJikpt9OfIHf5fhEAXph3zMHsCWVZA==", "+1234567890", false, null, null, null, "81fdfca6-6330-4e68-bedf-188df8ae8b2d", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 10, 30, 9, 8, 52, 794, DateTimeKind.Utc).AddTicks(9843), null, "[]", new Guid("047387df-b791-4144-b537-4e013661b985"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PricingRules");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("974ebc2f-e2a9-4335-b2f0-a12c5dbbd88b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("c4fbf52c-8e13-40af-985f-bfc3b6b4dee7"), 0, null, "02a32721-a765-4e33-aa08-266ebc582583", new DateTime(2025, 10, 30, 9, 4, 44, 173, DateTimeKind.Utc).AddTicks(5455), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAELI0WcRnzrgat6j1mOJrX3l3Iyhcv7617gvn53LhGRqS1GbALmuLrqPjkItnKtrASQ==", "+1234567890", false, null, null, null, "0f1aed10-e886-4b29-9f2b-57a1a2ae32f4", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("c4fbf52c-8e13-40af-985f-bfc3b6b4dee7"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 10, 30, 9, 4, 44, 211, DateTimeKind.Utc).AddTicks(5760), null, "[]", new Guid("7b84789a-0647-444c-b997-07c34a58f0ff"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }
    }
}
