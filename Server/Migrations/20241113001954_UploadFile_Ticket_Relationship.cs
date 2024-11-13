using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RequestHub.Server.Migrations
{
    public partial class UploadFile_Ticket_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_UploadFiles_UploadFileId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UploadFileId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UploadFileId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "UploadFiles",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 11, 12, 19, 19, 54, 57, DateTimeKind.Local).AddTicks(6364));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 11, 12, 19, 19, 54, 57, DateTimeKind.Local).AddTicks(7136), new byte[] { 201, 12, 170, 180, 226, 208, 72, 25, 8, 230, 6, 181, 114, 228, 96, 234, 30, 167, 46, 89, 200, 92, 97, 163, 144, 153, 125, 42, 249, 214, 138, 46, 82, 157, 83, 235, 195, 101, 72, 149, 66, 205, 151, 189, 229, 146, 235, 250, 210, 46, 2, 104, 117, 152, 238, 232, 183, 195, 118, 94, 196, 80, 203, 50 }, new byte[] { 57, 167, 116, 161, 123, 49, 105, 253, 83, 76, 75, 27, 162, 250, 161, 195, 166, 117, 96, 174, 222, 14, 177, 165, 4, 194, 146, 60, 229, 143, 109, 148, 212, 143, 173, 243, 38, 198, 99, 62, 79, 135, 248, 180, 47, 168, 242, 104, 170, 39, 198, 225, 189, 42, 115, 32, 124, 146, 225, 188, 146, 237, 189, 118, 158, 113, 93, 160, 111, 153, 117, 139, 103, 49, 205, 187, 108, 232, 198, 185, 243, 221, 205, 180, 35, 75, 121, 220, 115, 172, 24, 129, 155, 39, 56, 163, 100, 98, 65, 27, 11, 198, 125, 137, 254, 220, 21, 89, 86, 160, 241, 56, 59, 23, 210, 15, 4, 175, 3, 202, 73, 98, 130, 224, 143, 78, 142, 206 } });

            migrationBuilder.CreateIndex(
                name: "IX_UploadFiles_TicketId",
                table: "UploadFiles",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadFiles_Tickets_TicketId",
                table: "UploadFiles",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadFiles_Tickets_TicketId",
                table: "UploadFiles");

            migrationBuilder.DropIndex(
                name: "IX_UploadFiles_TicketId",
                table: "UploadFiles");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "UploadFiles");

            migrationBuilder.AddColumn<int>(
                name: "UploadFileId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2024, 10, 1, 16, 17, 30, 375, DateTimeKind.Local).AddTicks(513));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 10, 1, 16, 17, 30, 375, DateTimeKind.Local).AddTicks(1716), new byte[] { 236, 193, 30, 173, 113, 173, 73, 152, 178, 214, 2, 10, 79, 4, 194, 62, 83, 22, 157, 228, 240, 238, 117, 221, 140, 2, 205, 245, 125, 252, 137, 134, 243, 101, 3, 189, 201, 187, 228, 254, 181, 192, 20, 90, 244, 193, 68, 69, 97, 203, 170, 239, 211, 221, 5, 35, 82, 15, 89, 151, 231, 206, 173, 147 }, new byte[] { 71, 139, 167, 5, 180, 57, 49, 154, 183, 47, 195, 69, 156, 113, 199, 24, 246, 159, 21, 149, 163, 7, 58, 93, 211, 45, 113, 18, 86, 58, 166, 102, 149, 104, 1, 69, 86, 75, 173, 182, 12, 134, 196, 198, 176, 177, 21, 6, 81, 114, 22, 75, 186, 83, 235, 126, 15, 125, 89, 9, 60, 208, 76, 47, 71, 166, 81, 190, 169, 152, 40, 220, 76, 4, 112, 87, 6, 19, 102, 191, 218, 189, 176, 70, 76, 160, 63, 253, 238, 180, 192, 251, 15, 46, 90, 11, 150, 193, 246, 198, 158, 145, 59, 59, 152, 221, 143, 148, 74, 68, 2, 31, 91, 123, 192, 86, 197, 157, 53, 227, 17, 104, 216, 141, 32, 220, 236, 219 } });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UploadFileId",
                table: "Tickets",
                column: "UploadFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_UploadFiles_UploadFileId",
                table: "Tickets",
                column: "UploadFileId",
                principalTable: "UploadFiles",
                principalColumn: "Id");
        }
    }
}
