using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class InitialCreate : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Events",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            LocationStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Status = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Events", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Shift",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            ShiftDate = table.Column<DateOnly>(type: "date", nullable: false),
            StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
            EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
            IsCancelled = table.Column<bool>(type: "bit", nullable: false),
            EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Shift", x => x.Id);
            table.ForeignKey(
                      name: "FK_Shift_Events_EventId",
                      column: x => x.EventId,
                      principalTable: "Events",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "ShiftRole",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            RequiresApproval = table.Column<bool>(type: "bit", nullable: false),
            EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ShiftRole", x => x.Id);
            table.ForeignKey(
                      name: "FK_ShiftRole_Events_EventId",
                      column: x => x.EventId,
                      principalTable: "Events",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "ShiftRoleAssignment",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            MaxVolunteers = table.Column<int>(type: "int", nullable: false),
            ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ShiftRoleAssignment", x => x.Id);
            table.ForeignKey(
                      name: "FK_ShiftRoleAssignment_ShiftRole_RoleId",
                      column: x => x.RoleId,
                      principalTable: "ShiftRole",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_ShiftRoleAssignment_Shift_ShiftId",
                      column: x => x.ShiftId,
                      principalTable: "Shift",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Shift_EventId",
          table: "Shift",
          column: "EventId");

      migrationBuilder.CreateIndex(
          name: "IX_ShiftRole_EventId",
          table: "ShiftRole",
          column: "EventId");

      migrationBuilder.CreateIndex(
          name: "IX_ShiftRoleAssignment_RoleId",
          table: "ShiftRoleAssignment",
          column: "RoleId");

      migrationBuilder.CreateIndex(
          name: "IX_ShiftRoleAssignment_ShiftId",
          table: "ShiftRoleAssignment",
          column: "ShiftId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "ShiftRoleAssignment");

      migrationBuilder.DropTable(
          name: "ShiftRole");

      migrationBuilder.DropTable(
          name: "Shift");

      migrationBuilder.DropTable(
          name: "Events");
    }
  }
}
