using FluentValidation;
using TicketSystem.API.DTOs;

namespace TicketSystem.API.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginRequest>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("Debe ser un email válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
        }
    }
}