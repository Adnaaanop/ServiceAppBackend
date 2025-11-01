using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMessagesRelationToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("9314b266-7529-4f0c-bfb8-ccd06a2a351e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9314b266-7529-4f0c-bfb8-ccd06a2a351e"));

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("486214f3-1c95-45c8-b153-f035f8b38234"), 0, null, "1b668811-5e49-43c4-b303-cb86f7d9cc67", new DateTime(2025, 11, 1, 5, 56, 46, 781, DateTimeKind.Utc).AddTicks(8233), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEGVmPYD9VncbpTDyqbVUrA5BnQDR/EI+Fg8LZFea4xt+y0AuEZ+eR/8CukCPgSvo2g==", "+1234567890", false, null, null, null, "d581dda6-fc73-42ef-ab3a-bdc85d844dbe", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("486214f3-1c95-45c8-b153-f035f8b38234"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 1, 5, 56, 46, 819, DateTimeKind.Utc).AddTicks(5848), null, "[]", new Guid("c668ea48-3746-4c14-acb6-b42f4a196803"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BookingId",
                table: "Messages",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DeleteData(
                table: "ProviderProfiles",
                keyColumn: "UserId",
                keyValue: new Guid("486214f3-1c95-45c8-b153-f035f8b38234"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("486214f3-1c95-45c8-b153-f035f8b38234"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AddressJson", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "IsVerified", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PreferredServicesJson", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9314b266-7529-4f0c-bfb8-ccd06a2a351e"), 0, null, "474a5180-aacf-4b1d-bff9-6e4bf45e9738", new DateTime(2025, 11, 1, 5, 54, 29, 582, DateTimeKind.Utc).AddTicks(6946), null, "provider@example.com", true, true, false, true, null, null, null, false, null, "John Doe", null, null, "AQAAAAIAAYagAAAAEO98R6K3y6H3wxWUi4OXEtOQAo2gHTZ4INGiAH7VVc5zK7XSF2IWMONzM+f7gHMokw==", "+1234567890", false, null, null, null, "90ef7a53-8ae3-4ce9-9ccd-6b7381e51ef8", false, "provider@example.com" });

            migrationBuilder.InsertData(
                table: "ProviderProfiles",
                columns: new[] { "UserId", "AvailabilityJson", "BusinessDescription", "BusinessName", "CertificateUrlsJson", "CreatedAt", "DeletedAt", "DocumentUrlsJson", "Id", "IsActive", "IsApproved", "IsDeleted", "LastLogout", "LastUpdatedAt", "LastUpdatedBy", "LicenseUrlsJson", "ServiceAreasJson", "ServiceCategoriesJson" },
                values: new object[] { new Guid("9314b266-7529-4f0c-bfb8-ccd06a2a351e"), "{\"monday\": [\"9-5\"]}", "We provide cleaning and handyman services.", "John's Home Services", "[]", new DateTime(2025, 11, 1, 5, 54, 29, 620, DateTimeKind.Utc).AddTicks(1148), null, "[]", new Guid("4eb3c22f-849a-4a05-b0a0-03ae74c1a77a"), true, true, false, null, null, null, "[]", "[\"New York, NY\"]", "[\"Cleaning\",\"General Handyman\"]" });
        }
    }
}
