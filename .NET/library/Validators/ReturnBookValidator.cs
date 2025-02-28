using FluentValidation;
using OneBeyondApi.Constans;
using OneBeyondApi.Model;
using OneBeyondApi.Services;

namespace OneBeyondApi.Validators;

public class ReturnBookValidator : AbstractValidator<BookStock>
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReturnBookValidator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;

        RuleFor(x => x.LoanEndDate)
            .Cascade(CascadeMode.Stop)
            .Must(HasLoanDateTime)
            .WithMessage(ValidatorErrorMessages.LoanDateMissing)
            .Must(BeBeforeCurrentDate)
            .WithMessage(ValidatorErrorMessages.BookReturnedTooLate);
    }

    private bool HasLoanDateTime(DateTime? loanDateTime)
    {
        return loanDateTime.HasValue;
    }

    private bool BeBeforeCurrentDate(DateTime? loanDateTime)
    {
        return _dateTimeProvider.GetCurrentUtcDate() <= loanDateTime.Value;
    }
}