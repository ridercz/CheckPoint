using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Altairis.CheckPoint.Data.Migrations {
    /// <inheritdoc />
    public partial class Add_Events : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new {
                    Id = table.Column<string>(type: "TEXT", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
