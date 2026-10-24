using EvenDriven.Domain.Enums;
using EvenDriven.Domain.Primitives;
using EvenDriven.Domain.ValueObjects.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Entities
{
    public sealed class BookingEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public DateRange DateRange { get; set; }
        public BookingStatus Status { get; set; }

        private BookingEntity() { }

        public static BookingEntity Create(Guid userId, DateTime startDate, DateTime endDate)
        {
            return new BookingEntity
            {
                UserId = userId,
                DateRange = new DateRange(startDate, endDate),
                Status = BookingStatus.Pending
            };
        }


    }
}
