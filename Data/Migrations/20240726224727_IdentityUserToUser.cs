using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUserToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Follows_AspNetUsers_User1Id",
            //     table: "Follows");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Follows_AspNetUsers_User2Id",
            //     table: "Follows");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "26c208d4-0afc-4b7d-acde-d44abeaa4be5");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "59099be8-2664-4eaf-90ae-d616e9a67c48");

            // migrationBuilder.DropColumn(
            //     name: "Discriminator",
            //     table: "AspNetUsers");

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "1de2de35-157c-485f-a90f-9e9453b0eb3d", "e34217c0-deb3-44a1-a6c8-7751d20ca025", "ADMIN", "ADMIN" },
            //         { "66c66a18-69a4-4939-ace3-3362bb201d94", "981db961-e0e8-4046-9c72-f5ccf2a534ea", "USER", "USER" }
            //     });

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Follows_AspNetUsers_User1Id",
            //     table: "Follows",
            //     column: "User1Id",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Follows_AspNetUsers_User2Id",
            //     table: "Follows",
            //     column: "User2Id",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Follows_AspNetUsers_User1Id",
            //     table: "Follows");

            // migrationBuilder.DropForeignKey(
            //     name: "FK_Follows_AspNetUsers_User2Id",
            //     table: "Follows");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "1de2de35-157c-485f-a90f-9e9453b0eb3d");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "66c66a18-69a4-4939-ace3-3362bb201d94");

            // migrationBuilder.AddColumn<string>(
            //     name: "Discriminator",
            //     table: "AspNetUsers",
            //     type: "nvarchar(13)",
            //     maxLength: 13,
            //     nullable: false,
            //     defaultValue: "");

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "26c208d4-0afc-4b7d-acde-d44abeaa4be5", "ef169475-b557-4241-9a10-adc5c33fdf8f", "ADMIN", "ADMIN" },
            //         { "59099be8-2664-4eaf-90ae-d616e9a67c48", "d16a6f3c-1ad6-4bf1-8247-9bce4fb51935", "USER", "USER" }
            //     });

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Follows_AspNetUsers_User1Id",
            //     table: "Follows",
            //     column: "User1Id",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Follows_AspNetUsers_User2Id",
            //     table: "Follows",
            //     column: "User2Id",
            //     principalTable: "AspNetUsers",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }
    }
}
