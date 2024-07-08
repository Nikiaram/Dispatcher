using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dispatcher.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "JobRequests",
                newName: "Payload");

            migrationBuilder.RenameColumn(
                name: "ResultPath",
                table: "JobRequests",
                newName: "JobType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payload",
                table: "JobRequests",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "JobRequests",
                newName: "ResultPath");
        }
    }
}
