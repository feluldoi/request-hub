using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestHub.Server.Migrations
{
    public partial class initial : Migration
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
                name: "UploadFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadFileId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Tickets_UploadFiles_UploadFileId",
                        column: x => x.UploadFileId,
                        principalTable: "UploadFiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[] { 1, new DateTime(2024, 10, 1, 16, 17, 30, 375, DateTimeKind.Local).AddTicks(1716), "testuser@user.com", new byte[] { 236, 193, 30, 173, 113, 173, 73, 152, 178, 214, 2, 10, 79, 4, 194, 62, 83, 22, 157, 228, 240, 238, 117, 221, 140, 2, 205, 245, 125, 252, 137, 134, 243, 101, 3, 189, 201, 187, 228, 254, 181, 192, 20, 90, 244, 193, 68, 69, 97, 203, 170, 239, 211, 221, 5, 35, 82, 15, 89, 151, 231, 206, 173, 147 }, null, new byte[] { 71, 139, 167, 5, 180, 57, 49, 154, 183, 47, 195, 69, 156, 113, 199, 24, 246, 159, 21, 149, 163, 7, 58, 93, 211, 45, 113, 18, 86, 58, 166, 102, 149, 104, 1, 69, 86, 75, 173, 182, 12, 134, 196, 198, 176, 177, 21, 6, 81, 114, 22, 75, 186, 83, 235, 126, 15, 125, 89, 9, 60, 208, 76, 47, 71, 166, 81, 190, 169, 152, 40, 220, 76, 4, 112, 87, 6, 19, 102, 191, 218, 189, 176, 70, 76, 160, 63, 253, 238, 180, 192, 251, 15, 46, 90, 11, 150, 193, 246, 198, 158, 145, 59, 59, 152, 221, 143, 148, 74, 68, 2, 31, 91, 123, 192, 86, 197, 157, 53, 227, 17, 104, 216, 141, 32, 220, 236, 219 }, "Test User", null, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Comment", "DepartmentId", "Description", "EquipmentName", "IsValid", "SiteLocationId", "Timestamp", "UploadFileId", "UserId" },
                values: new object[] { 1, "enter comment here...", 1, "Enter description here", "enter equipment name here", true, 1, new DateTime(2024, 10, 1, 16, 17, 30, 375, DateTimeKind.Local).AddTicks(513), null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DepartmentId",
                table: "Tickets",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SiteLocationId",
                table: "Tickets",
                column: "SiteLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UploadFileId",
                table: "Tickets",
                column: "UploadFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "SiteLocations");

            migrationBuilder.DropTable(
                name: "UploadFiles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
