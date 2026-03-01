using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketCommentsAndWaitingState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComentariosTicket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TicketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AutorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Mensaje = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    EsInterno = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentariosTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentariosTicket_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComentariosTicket_Usuarios_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosTicket_AutorId",
                table: "ComentariosTicket",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosTicket_TicketId",
                table: "ComentariosTicket",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentariosTicket");
        }
    }
}
