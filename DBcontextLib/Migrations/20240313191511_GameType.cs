using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBcontextLib.Migrations
{
    /// <inheritdoc />
    public partial class GameType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_TypeId",
                table: "Games",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameType_TypeId",
                table: "Games",
                column: "TypeId",
                principalTable: "GameType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameType_TypeId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameType");

            migrationBuilder.DropIndex(
                name: "IX_Games_TypeId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Games");
        }
    }
}
