using FluentValidation;
using NetMessaging.Application.Persons;

namespace NetMessaging.Api.ApiHandlers
{
    public class PersonAddValidator : AbstractValidator<PersonAddCommand>
    {        

        public PersonAddValidator()
        {            
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
