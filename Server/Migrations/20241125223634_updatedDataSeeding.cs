using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestHub.Server.Migrations
{
    public partial class updatedDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 11, 25, 17, 36, 34, 627, DateTimeKind.Local).AddTicks(7805));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 11, 25, 17, 36, 34, 627, DateTimeKind.Local).AddTicks(8589), new byte[] { 27, 93, 65, 209, 239, 24, 183, 167, 177, 103, 161, 197, 98, 112, 28, 48, 103, 82, 62, 210, 132, 26, 216, 85, 138, 59, 103, 174, 255, 103, 158, 99, 27, 39, 174, 151, 58, 1, 141, 18, 171, 200, 129, 29, 97, 174, 144, 211, 138, 25, 24, 22, 232, 229, 238, 110, 7, 222, 193, 121, 93, 59, 127, 218 }, new byte[] { 122, 86, 217, 83, 209, 104, 65, 15, 25, 2, 54, 127, 137, 164, 125, 38, 27, 62, 27, 240, 45, 39, 24, 30, 199, 158, 35, 124, 113, 158, 89, 45, 78, 190, 169, 179, 168, 63, 95, 123, 90, 241, 109, 77, 172, 212, 171, 160, 160, 34, 156, 207, 78, 51, 252, 238, 191, 86, 63, 110, 21, 189, 80, 69, 172, 187, 251, 54, 153, 167, 95, 111, 226, 58, 216, 25, 233, 74, 108, 104, 241, 254, 8, 71, 159, 158, 58, 227, 26, 54, 69, 227, 255, 31, 137, 152, 193, 80, 210, 68, 11, 16, 72, 90, 98, 104, 65, 85, 73, 34, 242, 32, 8, 12, 141, 76, 91, 211, 45, 195, 235, 108, 11, 33, 58, 202, 134, 13 }, new DateTime(2024, 11, 25, 17, 36, 34, 627, DateTimeKind.Local).AddTicks(8591) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 11, 23, 12, 4, 50, 472, DateTimeKind.Local).AddTicks(3982));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt", "VerifiedAt" },
                values: new object[] { new DateTime(2024, 11, 23, 12, 4, 50, 472, DateTimeKind.Local).AddTicks(4723), new byte[] { 117, 249, 81, 48, 244, 31, 74, 71, 104, 122, 66, 175, 60, 156, 2, 191, 91, 72, 43, 19, 221, 12, 140, 49, 78, 56, 65, 1, 70, 212, 30, 58, 233, 155, 214, 222, 83, 95, 66, 188, 202, 23, 140, 210, 237, 198, 133, 118, 195, 127, 158, 182, 46, 60, 254, 153, 208, 90, 223, 161, 226, 25, 98, 139 }, new byte[] { 48, 69, 163, 203, 163, 198, 137, 237, 110, 25, 16, 165, 165, 128, 130, 14, 56, 56, 224, 124, 199, 214, 217, 192, 142, 214, 40, 244, 238, 31, 202, 3, 3, 133, 42, 52, 58, 1, 255, 204, 104, 2, 115, 148, 108, 218, 49, 139, 81, 34, 3, 235, 241, 103, 243, 23, 25, 167, 186, 2, 250, 127, 78, 0, 192, 191, 7, 216, 133, 9, 156, 195, 254, 198, 111, 209, 146, 241, 228, 101, 68, 26, 108, 141, 1, 192, 200, 250, 220, 26, 87, 81, 248, 106, 13, 55, 98, 103, 202, 98, 195, 182, 152, 58, 89, 184, 106, 204, 20, 237, 61, 202, 19, 78, 104, 17, 210, 70, 209, 15, 156, 88, 28, 196, 254, 193, 210, 237 }, null });
        }
    }
}
