using FluentValidation;

namespace CleanArchitecture.Applicattion.Features.Directors.Commands.CreateDirector
{
    public class CreateCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(p => p.Nombre)
                   .NotNull()
                   .WithMessage("{Nombre} no puede ser nulo");

            RuleFor(p => p.Apellido)
                   .NotNull()
                   .WithMessage("{Apellido} no puede ser nulo");
        }
    }
}
