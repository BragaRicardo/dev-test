using DevTest_API.DTOs;
using FluentValidation;

namespace DevTest_API.Validator
{
    public class UsuarioValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotNull().WithErrorCode("1").WithMessage("Nombre Completo nulo o vacío")
                .NotEmpty().WithErrorCode("1").WithMessage("Nombre Completo nulo o vacío")
                .MaximumLength(100).WithErrorCode("1").WithMessage("El Nombre Completo no puede superar los 100 caracteres");

            RuleFor(x => x.Cedula)
                .NotNull().WithErrorCode("2").WithMessage("Cédula nulo o vacío")
                .NotEmpty().WithErrorCode("2").WithMessage("Cédula nulo o vacío")
                .MaximumLength(15).WithErrorCode("2").WithMessage("La Cédula no puede superar los 15 caracteres");

            RuleFor(x => x.Sexo)
                .IsInEnum().WithErrorCode("3").WithMessage("Sexo inválido. Valores posibles: MASCULINO o FEMENINO");

            RuleFor(x => x.Correo)
                .NotNull().WithErrorCode("4").WithMessage("El email/correo es requerido")
                .NotEmpty().WithErrorCode("4").WithMessage("El email/correo es requerido")
                .MaximumLength(250).WithErrorCode("4").WithMessage("El email/correo no puede superar los 250 caracteres");

            RuleFor(x => x.Direccion)
                .NotNull().WithErrorCode("5").WithMessage("La dirección de residencia es requerida")
                .NotEmpty().WithErrorCode("5").WithMessage("La dirección de residencia es requerida")
                .MaximumLength(150).WithErrorCode("5").WithMessage("La dirección de residencia no puede superar los 150 caracteres");

            RuleFor(x => x.Password)
                .NotNull().WithErrorCode("6").WithMessage("La contraseña es requerida")
                .NotEmpty().WithErrorCode("6").WithMessage("La contraseña es requerida")
                .MaximumLength(64).WithErrorCode("6").WithMessage("La contraseña no puede superar los 64 caracteres");

            RuleFor(x => x.Rol)
                .IsInEnum().WithErrorCode("7").WithMessage("Rol inválido. Valores posibles: ADMIN o CONSULTOR");
        }
    }
}
