using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Corzbank.Data.Migrations
{
    public partial class AddTokenUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_AspNetUsers_Id",
                table: "Tokens",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_AspNetUsers_Id",
                table: "Tokens");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_AspNetUsers_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
