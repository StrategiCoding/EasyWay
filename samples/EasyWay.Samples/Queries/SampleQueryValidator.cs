using FluentValidation;

namespace EasyWay.Samples.Queries
{
    public class SampleQueryValidator : AbstractValidator<SampleQuery>
    {
        public SampleQueryValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithErrorCode("EMPTY")
                .MinimumLength(3).WithErrorCode("MIN_3_CHARS");
        }
    }
}
