using FluentValidation;

namespace MediatR.Application.Features.Commands.ItemCreate
{
    public partial class ItemCreate
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(s => s.Title).NotEmpty().MaximumLength(100);
                RuleFor(s => s.UniqueNumber).NotEmpty();
            }
        }
    }
}
