using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookArchive.DAL.Migrations
{
    public partial class I : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2020, 12, 30, 5, 37, 49, 705, DateTimeKind.Local).AddTicks(1907)),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValue: new DateTime(2020, 12, 30, 5, 37, 49, 711, DateTimeKind.Local).AddTicks(9472))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CoverImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2020, 12, 30, 5, 37, 49, 712, DateTimeKind.Local).AddTicks(7331)),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValue: new DateTime(2020, 12, 30, 5, 37, 49, 712, DateTimeKind.Local).AddTicks(9004))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BookId",
                table: "AuthorBook",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
