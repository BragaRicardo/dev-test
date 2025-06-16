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
                .MaximumLength(100).WithErrorCode("1").WithMessage("El Nombre Completo no pueden superar los 100 caracteres");

            RuleFor(x => x.Cedula)
                        .NotNull().WithErrorCode("2").WithMessage("Cédula nulo o vacío")
                        .NotEmpty().WithErrorCode("2").WithMessage("Cédula nulo o vacío")
                        .MaximumLength(100).WithErrorCode("2").WithMessage("La Cédula no puede superar los 8 caracteres");

            RuleFor(x => x.Cedula)
                        .NotNull().WithErrorCode("3").WithMessage("La Cédula es requerido")
                        .NotEmpty().WithErrorCode("3").WithMessage("La Cédula es requerido")
                        .MaximumLength(15).WithErrorCode("3").WithMessage("La Cédula no puede superar los 15 caracteres");

            RuleFor(x => x.Sexo)
                        .NotNull().WithErrorCode("4").WithMessage("El sexo es requerido")
                        .NotEmpty().WithErrorCode("4").WithMessage("El sexo es requerido");

            RuleFor(x => x.Correo)
                        .NotNull().WithErrorCode("5").WithMessage("El email/correo es requerido")
                        .NotEmpty().WithErrorCode("5").WithMessage("El email/correo es requerido")
                        .MaximumLength(70).WithErrorCode("5").WithMessage("El email/correo no puede superar los 250 caracteres");

            RuleFor(x => x.Direccion)
                        .NotNull().WithErrorCode("6").WithMessage("La dirección de residencia es requerida")
                        .NotEmpty().WithErrorCode("6").WithMessage("La dirección de residencia es requerida")
                        .MaximumLength(100).WithErrorCode("6").WithMessage("La dirección de residencia no puede superar los 150 caracteres");

            RuleFor(x => x.Password)
                        .NotNull().WithErrorCode("7").WithMessage("La contraseña es requerido")
                        .NotEmpty().WithErrorCode("7").WithMessage("La contraseña es requerido")
                        .MaximumLength(80).WithErrorCode("7").WithMessage("La contraseña no puede superar los 64 caracteres");

            RuleFor(x => x.Password)
                        .NotNull().WithErrorCode("8").WithMessage("El rol es requerido")
                        .NotEmpty().WithErrorCode("8").WithMessage("El rol es requerido");
        }
    }
}
