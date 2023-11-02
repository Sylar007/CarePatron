using FluentValidation;

namespace api.Models
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.PhoneNumber).NotNull();
            RuleFor(x => x.Email).NotNull();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
