using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostCommentsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "c56ae952-126c-4ab7-b2e3-c2572420ae87");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "d8b63499-48cc-476d-8fe1-c6469fbb268e");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatededAt",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatededAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PostComments_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "8b14f36e-60ba-476d-a131-bfdc81c44c4a", "2b864b3e-5606-4fe3-ad04-e3f1279c1e2d", "ADMIN", "ADMIN" },
            //         { "b4eaec16-a374-4e3e-b95e-347e14975412", "a54691ba-f40f-455b-acca-807222a85eaa", "USER", "USER" }
            //     });

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_UserId",
                table: "PostComments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostComments");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "8b14f36e-60ba-476d-a131-bfdc81c44c4a");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "b4eaec16-a374-4e3e-b95e-347e14975412");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "UpdatededAt",
                table: "Post");

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "c56ae952-126c-4ab7-b2e3-c2572420ae87", "63e13c7b-d6fa-4cfb-8b72-7d0d773ec659", "USER", "USER" },
            //         { "d8b63499-48cc-476d-8fe1-c6469fbb268e", "4bbc4532-f620-4fa9-aaf4-78ae49f72687", "ADMIN", "ADMIN" }
            //     });
        }
    }
}
