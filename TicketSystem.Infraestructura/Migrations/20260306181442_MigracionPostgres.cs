using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Tickets"" DROP CONSTRAINT IF EXISTS ""FK_Tickets_Usuarios_UsuarioId"";
                ALTER TABLE ""Tickets"" DROP CONSTRAINT IF EXISTS ""FK_Tickets_Usuarios_OperadorAsignadoId"";
                ALTER TABLE ""ComentariosTicket"" DROP CONSTRAINT IF EXISTS ""FK_ComentariosTicket_Usuarios_AutorId"";
                ALTER TABLE ""ComentariosTicket"" DROP CONSTRAINT IF EXISTS ""FK_ComentariosTicket_Tickets_TicketId"";
                ALTER TABLE ""AuditLogs"" DROP CONSTRAINT IF EXISTS ""FK_AuditLogs_Usuarios_UsuarioId"";
                ALTER TABLE ""AuditLogs"" DROP CONSTRAINT IF EXISTS ""FK_AuditLogs_Tickets_TicketId"";
                ALTER TABLE ""ArchivosAdjuntos"" DROP CONSTRAINT IF EXISTS ""FK_ArchivosAdjuntos_ComentariosTicket_ComentarioId"";

                ALTER TABLE ""Tickets"" ALTER COLUMN ""SLACumplido"" DROP DEFAULT;
                ALTER TABLE ""Tickets"" ALTER COLUMN ""IsDeleted"" DROP DEFAULT;
                ALTER TABLE ""ComentariosTicket"" ALTER COLUMN ""EsInterno"" DROP DEFAULT;

                ALTER TABLE ""Usuarios""
                    ALTER COLUMN ""Id"" TYPE uuid USING ""Id""::uuid,
                    ALTER COLUMN ""Rol"" TYPE integer USING ""Rol""::integer,
                    ALTER COLUMN ""RefreshTokenExpires"" TYPE timestamp with time zone USING ""RefreshTokenExpires""::timestamp with time zone,
                    ALTER COLUMN ""PasswordResetTokenExpires"" TYPE timestamp with time zone USING ""PasswordResetTokenExpires""::timestamp with time zone;

                ALTER TABLE ""Tickets""
                    ALTER COLUMN ""Id"" TYPE uuid USING ""Id""::uuid,
                    ALTER COLUMN ""UsuarioId"" TYPE uuid USING ""UsuarioId""::uuid,
                    ALTER COLUMN ""OperadorAsignadoId"" TYPE uuid USING ""OperadorAsignadoId""::uuid,
                    ALTER COLUMN ""FechaCreacion"" TYPE timestamp with time zone USING ""FechaCreacion""::timestamp with time zone,
                    ALTER COLUMN ""FechaAsignacion"" TYPE timestamp with time zone USING ""FechaAsignacion""::timestamp with time zone,
                    ALTER COLUMN ""FechaLimite"" TYPE timestamp with time zone USING ""FechaLimite""::timestamp with time zone,
                    ALTER COLUMN ""FechaResolucion"" TYPE timestamp with time zone USING ""FechaResolucion""::timestamp with time zone,
                    ALTER COLUMN ""SLACumplido"" TYPE boolean USING CASE WHEN ""SLACumplido""::text = '1' THEN true ELSE false END,
                    ALTER COLUMN ""IsDeleted"" TYPE boolean USING CASE WHEN ""IsDeleted""::text = '1' THEN true ELSE false END;

                ALTER TABLE ""ComentariosTicket""
                    ALTER COLUMN ""Id"" TYPE uuid USING ""Id""::uuid,
                    ALTER COLUMN ""TicketId"" TYPE uuid USING ""TicketId""::uuid,
                    ALTER COLUMN ""AutorId"" TYPE uuid USING ""AutorId""::uuid,
                    ALTER COLUMN ""FechaCreacion"" TYPE timestamp with time zone USING ""FechaCreacion""::timestamp with time zone,
                    ALTER COLUMN ""EsInterno"" TYPE boolean USING CASE WHEN ""EsInterno""::text = '1' THEN true ELSE false END;

                ALTER TABLE ""AuditLogs""
                    ALTER COLUMN ""Id"" TYPE uuid USING ""Id""::uuid,
                    ALTER COLUMN ""TicketId"" TYPE uuid USING ""TicketId""::uuid,
                    ALTER COLUMN ""UsuarioId"" TYPE uuid USING ""UsuarioId""::uuid,
                    ALTER COLUMN ""Fecha"" TYPE timestamp with time zone USING ""Fecha""::timestamp with time zone;

                ALTER TABLE ""ArchivosAdjuntos""
                    ALTER COLUMN ""Id"" TYPE uuid USING ""Id""::uuid,
                    ALTER COLUMN ""ComentarioId"" TYPE uuid USING ""ComentarioId""::uuid,
                    ALTER COLUMN ""FechaSubida"" TYPE timestamp with time zone USING ""FechaSubida""::timestamp with time zone,
                    ALTER COLUMN ""TamañoBytes"" TYPE bigint USING ""TamañoBytes""::bigint;

                ALTER TABLE ""Tickets""
                    ADD CONSTRAINT ""FK_Tickets_Usuarios_UsuarioId"" FOREIGN KEY (""UsuarioId"") REFERENCES ""Usuarios""(""Id"") ON DELETE CASCADE,
                    ADD CONSTRAINT ""FK_Tickets_Usuarios_OperadorAsignadoId"" FOREIGN KEY (""OperadorAsignadoId"") REFERENCES ""Usuarios""(""Id"");

                ALTER TABLE ""ComentariosTicket""
                    ADD CONSTRAINT ""FK_ComentariosTicket_Usuarios_AutorId"" FOREIGN KEY (""AutorId"") REFERENCES ""Usuarios""(""Id"") ON DELETE CASCADE,
                    ADD CONSTRAINT ""FK_ComentariosTicket_Tickets_TicketId"" FOREIGN KEY (""TicketId"") REFERENCES ""Tickets""(""Id"") ON DELETE CASCADE;

                ALTER TABLE ""AuditLogs""
                    ADD CONSTRAINT ""FK_AuditLogs_Usuarios_UsuarioId"" FOREIGN KEY (""UsuarioId"") REFERENCES ""Usuarios""(""Id"");

                ALTER TABLE ""ArchivosAdjuntos""
                    ADD CONSTRAINT ""FK_ArchivosAdjuntos_ComentariosTicket_ComentarioId"" FOREIGN KEY (""ComentarioId"") REFERENCES ""ComentariosTicket""(""Id"") ON DELETE CASCADE;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Tickets"" DROP CONSTRAINT IF EXISTS ""FK_Tickets_Usuarios_UsuarioId"";
                ALTER TABLE ""Tickets"" DROP CONSTRAINT IF EXISTS ""FK_Tickets_Usuarios_OperadorAsignadoId"";
                ALTER TABLE ""ComentariosTicket"" DROP CONSTRAINT IF EXISTS ""FK_ComentariosTicket_Usuarios_AutorId"";
                ALTER TABLE ""ComentariosTicket"" DROP CONSTRAINT IF EXISTS ""FK_ComentariosTicket_Tickets_TicketId"";
                ALTER TABLE ""AuditLogs"" DROP CONSTRAINT IF EXISTS ""FK_AuditLogs_Usuarios_UsuarioId"";
                ALTER TABLE ""ArchivosAdjuntos"" DROP CONSTRAINT IF EXISTS ""FK_ArchivosAdjuntos_ComentariosTicket_ComentarioId"";

                ALTER TABLE ""Usuarios""
                    ALTER COLUMN ""Id"" TYPE text USING ""Id""::text,
                    ALTER COLUMN ""RefreshTokenExpires"" TYPE text USING ""RefreshTokenExpires""::text,
                    ALTER COLUMN ""PasswordResetTokenExpires"" TYPE text USING ""PasswordResetTokenExpires""::text;

                ALTER TABLE ""Tickets""
                    ALTER COLUMN ""Id"" TYPE text USING ""Id""::text,
                    ALTER COLUMN ""UsuarioId"" TYPE text USING ""UsuarioId""::text,
                    ALTER COLUMN ""OperadorAsignadoId"" TYPE text USING ""OperadorAsignadoId""::text,
                    ALTER COLUMN ""FechaCreacion"" TYPE text USING ""FechaCreacion""::text,
                    ALTER COLUMN ""FechaAsignacion"" TYPE text USING ""FechaAsignacion""::text,
                    ALTER COLUMN ""FechaLimite"" TYPE text USING ""FechaLimite""::text,
                    ALTER COLUMN ""FechaResolucion"" TYPE text USING ""FechaResolucion""::text,
                    ALTER COLUMN ""SLACumplido"" TYPE int USING CASE WHEN ""SLACumplido"" THEN 1 ELSE 0 END,
                    ALTER COLUMN ""IsDeleted"" TYPE int USING CASE WHEN ""IsDeleted"" THEN 1 ELSE 0 END;

                ALTER TABLE ""ComentariosTicket""
                    ALTER COLUMN ""Id"" TYPE text USING ""Id""::text,
                    ALTER COLUMN ""TicketId"" TYPE text USING ""TicketId""::text,
                    ALTER COLUMN ""AutorId"" TYPE text USING ""AutorId""::text,
                    ALTER COLUMN ""FechaCreacion"" TYPE text USING ""FechaCreacion""::text,
                    ALTER COLUMN ""EsInterno"" TYPE int USING CASE WHEN ""EsInterno"" THEN 1 ELSE 0 END;

                ALTER TABLE ""AuditLogs""
                    ALTER COLUMN ""Id"" TYPE text USING ""Id""::text,
                    ALTER COLUMN ""TicketId"" TYPE text USING ""TicketId""::text,
                    ALTER COLUMN ""UsuarioId"" TYPE text USING ""UsuarioId""::text,
                    ALTER COLUMN ""Fecha"" TYPE text USING ""Fecha""::text;

                ALTER TABLE ""ArchivosAdjuntos""
                    ALTER COLUMN ""Id"" TYPE text USING ""Id""::text,
                    ALTER COLUMN ""ComentarioId"" TYPE text USING ""ComentarioId""::text,
                    ALTER COLUMN ""FechaSubida"" TYPE text USING ""FechaSubida""::text,
                    ALTER COLUMN ""TamañoBytes"" TYPE int USING ""TamañoBytes""::int;
            ");
        }
    }
}