using FluentValidation;

namespace MediatR.Application.Features.Commands.ItemUpdate
{
    public partial class ItemUpdate
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(s => s.Id).NotEmpty();
                RuleFor(s => s.Title).NotEmpty().MaximumLength(100);
                RuleFor(s => s.UniqueNumber).NotEmpty();                   
            }
        }
    }
}
