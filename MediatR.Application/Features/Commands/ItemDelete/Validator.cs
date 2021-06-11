using FluentValidation;

namespace MediatR.Application.Features.Commands.ItemDelete
{
    public partial class ItemDelete
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(s => s.Id).NotEmpty();                  
            }
        }
    }
}
