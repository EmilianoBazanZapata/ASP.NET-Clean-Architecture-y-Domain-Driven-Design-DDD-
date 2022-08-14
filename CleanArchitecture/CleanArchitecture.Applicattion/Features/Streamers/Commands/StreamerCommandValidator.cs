using FluentValidation;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands
{
    public class StreamerCommandValidator : AbstractValidator<StreamerCommand>
    {
        public StreamerCommandValidator()
        {
            RuleFor(s => s.Nombre)
                   .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
                   .NotNull().WithMessage("{Nombre} no puede ser nulo")
                   .MaximumLength(50).WithMessage("{Numbre} no puede exceder los 50 caracteres");

            RuleFor(s => s.Url)
                   .NotEmpty().WithMessage("la {url} no puede estar en blanco");
        }
    }
}
