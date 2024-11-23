using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestHub.Server.Migrations
{
    public partial class dataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SiteLocationId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_SiteLocations_SiteLocationId",
                        column: x => x.SiteLocationId,
                        principalTable: "SiteLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustedFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadFiles_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "Software Engineering" },
                    { 2, "Cloud Architect" }
                });

            migrationBuilder.InsertData(
                table: "SiteLocations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Germany" },
                    { 2, "United States" },
                    { 3, "Switzerland" },
                    { 4, "Romania" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "PasswordHash", "PasswordResetToken", "PasswordSalt", "RequestorName", "ResetTokenExpires", "Role", "VerificationToken", "VerifiedAt" },
                values: new object[] { 1, new DateTime(2024, 11, 23, 12, 4, 50, 472, DateTimeKind.Local).AddTicks(4723), "testuser@user.com", new byte[] { 117, 249, 81, 48, 244, 31, 74, 71, 104, 122, 66, 175, 60, 156, 2, 191, 91, 72, 43, 19, 221, 12, 140, 49, 78, 56, 65, 1, 70, 212, 30, 58, 233, 155, 214, 222, 83, 95, 66, 188, 202, 23, 140, 210, 237, 198, 133, 118, 195, 127, 158, 182, 46, 60, 254, 153, 208, 90, 223, 161, 226, 25, 98, 139 }, null, new byte[] { 48, 69, 163, 203, 163, 198, 137, 237, 110, 25, 16, 165, 165, 128, 130, 14, 56, 56, 224, 124, 199, 214, 217, 192, 142, 214, 40, 244, 238, 31, 202, 3, 3, 133, 42, 52, 58, 1, 255, 204, 104, 2, 115, 148, 108, 218, 49, 139, 81, 34, 3, 235, 241, 103, 243, 23, 25, 167, 186, 2, 250, 127, 78, 0, 192, 191, 7, 216, 133, 9, 156, 195, 254, 198, 111, 209, 146, 241, 228, 101, 68, 26, 108, 141, 1, 192, 200, 250, 220, 26, 87, 81, 248, 106, 13, 55, 98, 103, 202, 98, 195, 182, 152, 58, 89, 184, 106, 204, 20, 237, 61, 202, 19, 78, 104, 17, 210, 70, 209, 15, 156, 88, 28, 196, 254, 193, 210, 237 }, "Test User", null, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Comment", "DepartmentId", "Description", "EquipmentName", "IsValid", "SiteLocationId", "Timestamp", "UserId" },
                values: new object[] { 1, "enter comment here...", 1, "Enter description here", "enter equipment name here", true, 1, new DateTime(2024, 11, 23, 12, 4, 50, 472, DateTimeKind.Local).AddTicks(3982), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DepartmentId",
                table: "Tickets",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SiteLocationId",
                table: "Tickets",
                column: "SiteLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadFiles_TicketId",
                table: "UploadFiles",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadFiles");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "SiteLocations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
