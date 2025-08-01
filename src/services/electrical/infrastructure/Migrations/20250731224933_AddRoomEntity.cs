using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriPower.Electrical.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Perimeter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Classification = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Lighting_MinimumLoad = table.Column<int>(type: "int", nullable: false),
                    GeneralSockets_RequiredLoad = table.Column<int>(type: "int", nullable: false),
                    GeneralSockets_RequiredCount = table.Column<int>(type: "int", nullable: false),
                    GeneralSockets_Modifier = table.Column<int>(type: "int", nullable: false),
                    GeneralSockets_CorrectedLoad = table.Column<int>(type: "int", nullable: false),
                    GeneralSockets_CorrectedCount = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ProjectId",
                table: "Rooms",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
