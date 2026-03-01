using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class AddOperadorAsignadoToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OperadorAsignadoId",
                table: "Tickets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OperadorAsignadoId",
                table: "Tickets",
                column: "OperadorAsignadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Usuarios_OperadorAsignadoId",
                table: "Tickets",
                column: "OperadorAsignadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Usuarios_OperadorAsignadoId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_OperadorAsignadoId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "OperadorAsignadoId",
                table: "Tickets");
        }
    }
}
