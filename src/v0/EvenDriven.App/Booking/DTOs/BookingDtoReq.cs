using EvenDriven.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Booking.DTOs
{
    public sealed record BookingDtoReq(
        Guid userId,
        DateTime startDate,
        DateTime endDate
    );
}
