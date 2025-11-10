using Api.Dtos.Request;
using FluentValidation;
using Api.Repositories.Interfaces;

namespace Api.Validation
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        private readonly IEmployeeRepository _repo;

        public RegisterRequestDtoValidator(IEmployeeRepository repo)
        {
            _repo = repo;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MustAsync(async (name, cancellation) =>
                    !await _repo.ExistsByNameAsync(name))
                .WithMessage("El nombre de usuario ya existe");

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress().WithMessage("Email inválido")
                .MustAsync(async (email, cancellation) =>
                    !await _repo.ExistsByEmailAsync(email))
                .WithMessage("El correo ya está registrado");

            RuleFor(x => x.PasswordHash)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("La contraseña debe tener al menos 6 caracteres");
        }
    }
}
