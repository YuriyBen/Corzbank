using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Corzbank.Data.Migrations
{
    public partial class AddVerificationUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_AspNetUsers_UserId",
                table: "Verifications");

            migrationBuilder.DropIndex(
                name: "IX_Verifications_UserId",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Verifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_AspNetUsers_Id",
                table: "Verifications",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verifications_AspNetUsers_Id",
                table: "Verifications");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Verifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_UserId",
                table: "Verifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Verifications_AspNetUsers_UserId",
                table: "Verifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
