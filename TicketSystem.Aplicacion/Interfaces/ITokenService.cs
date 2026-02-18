using TicketSystem.Dominio.Entidades;

public interface ITokenService
{
    string GenerarToken(Usuario usuario);
}
