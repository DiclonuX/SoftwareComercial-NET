using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class ComercianteValidator : AbstractValidator<ComercianteDTO>
    {
        public ComercianteValidator()
        {
            RuleFor(c => c.NombreRazonSocial)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(200).WithMessage("El nombre no puede superar los 200 caracteres");

            RuleFor(c => c.IdMunicipio)
                .NotEmpty().WithMessage("El municipio es obligatorio");

            RuleFor(c => c.Telefono)
                .Matches(@"^\d{7,15}$").When(c => !string.IsNullOrEmpty(c.Telefono))
                .WithMessage("El teléfono debe contener entre 7 y 15 dígitos numéricos");

            RuleFor(c => c.CorreoElectronico)
                .EmailAddress().When(c => !string.IsNullOrEmpty(c.CorreoElectronico))
                .WithMessage("El correo electrónico no es válido");

            RuleFor(c => c.Estado)
                .NotNull().WithMessage("El estado es obligatorio");
        }
    }
}
