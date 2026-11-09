using FluentValidation;

namespace EvenDriven.App.Booking.DTOs
{
    public sealed record BookingDtoReq(
        Guid userId,
        DateTime startDate,
        DateTime endDate
    );

    public class BookingDtoReqValidator : AbstractValidator<BookingDtoReq>
    {
        public BookingDtoReqValidator () {

            RuleFor(b => b.startDate)
                .NotEmpty().WithMessage("the field startDate is required")
                .Must(startDate => startDate >= DateTime.UtcNow).WithMessage("startDate must be in the future");

            RuleFor(b => b.endDate)
                .NotEmpty().WithMessage("the field endDate is required")
                .Must((dto, enDate) => enDate > dto.startDate).WithMessage("EndDate must be after startDate.");

        }
    }
}
