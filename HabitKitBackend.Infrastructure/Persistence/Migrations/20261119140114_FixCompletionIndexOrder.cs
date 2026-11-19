using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitKitBackend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCompletionIndexOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HabitCompletions_Date_HabitId",
                table: "HabitCompletions");

            migrationBuilder.DropIndex(
                name: "IX_HabitCompletions_HabitId",
                table: "HabitCompletions");

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_HabitId_Date",
                table: "HabitCompletions",
                columns: new[] { "HabitId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HabitCompletions_HabitId_Date",
                table: "HabitCompletions");

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_Date_HabitId",
                table: "HabitCompletions",
                columns: new[] { "Date", "HabitId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_HabitId",
                table: "HabitCompletions",
                column: "HabitId");
        }
    }
}
