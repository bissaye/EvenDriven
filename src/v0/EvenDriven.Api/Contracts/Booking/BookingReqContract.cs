using EvenDriven.App.Booking.DTOs;
using System.Runtime.CompilerServices;

namespace EvenDriven.Api.Contracts.Booking
{
    public sealed record BookingReqContract
    (
        DateTime startDate,
        DateTime endDate
    );

    public static class BookingReqContractExtensions 
    {
        extension(BookingReqContract contract)
        {
            public BookingDtoReq ToBookingDto(Guid userId)
            {
                return new BookingDtoReq(userId, contract.startDate, contract.endDate) { };
            }
        }

    }
}
