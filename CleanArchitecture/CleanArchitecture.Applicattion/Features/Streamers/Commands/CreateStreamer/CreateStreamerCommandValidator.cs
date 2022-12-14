using FluentValidation;

namespace CleanArchitecture.Applicattion.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
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
