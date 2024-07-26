using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "3c53e5d0-0c3a-4ded-ab5e-a36fd1d92472");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "6174b21b-72e6-4d84-b581-4e965ba432f9");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "5c195709-1849-4f36-831e-c3298ec19177");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "c56ae952-126c-4ab7-b2e3-c2572420ae87", "63e13c7b-d6fa-4cfb-8b72-7d0d773ec659", "USER", "USER" },
            //         { "d8b63499-48cc-476d-8fe1-c6469fbb268e", "4bbc4532-f620-4fa9-aaf4-78ae49f72687", "ADMIN", "ADMIN" }
            //     });

            migrationBuilder.CreateIndex(
                name: "IX_Post_Title",
                table: "Post",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "c56ae952-126c-4ab7-b2e3-c2572420ae87");

            // migrationBuilder.DeleteData(
            //     table: "AspNetRoles",
            //     keyColumn: "Id",
            //     keyValue: "d8b63499-48cc-476d-8fe1-c6469fbb268e");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "5c195709-1849-4f36-831e-c3298ec19177",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //     values: new object[,]
            //     {
            //         { "3c53e5d0-0c3a-4ded-ab5e-a36fd1d92472", "2019a2e7-04f0-4e6a-b0a4-c7d217246c22", "USER", "USER" },
            //         { "6174b21b-72e6-4d84-b581-4e965ba432f9", "0fb89b13-c15f-4de8-95e8-6ba6341718e6", "ADMIN", "ADMIN" }
            //     });
        }
    }
}
