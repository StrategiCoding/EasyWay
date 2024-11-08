using FluentValidation;

namespace EasyWay.Samples.Commands
{
    public class SampleCommandValidator : AbstractValidator<SampleCommand>
    {
        public SampleCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithErrorCode("EMPTY");
        }
    }
}
