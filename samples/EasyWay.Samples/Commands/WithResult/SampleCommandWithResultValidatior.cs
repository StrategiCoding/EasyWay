using FluentValidation;

namespace EasyWay.Samples.Commands.WithResult
{
    public class SampleCommandWithResultValidatior : AbstractValidator<SampleCommandWithResult>
    {
        public SampleCommandWithResultValidatior()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithErrorCode("EMPTY");
        }
    }
}
