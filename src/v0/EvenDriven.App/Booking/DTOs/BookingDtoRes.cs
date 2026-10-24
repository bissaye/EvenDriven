using EvenDriven.Domain.Entities;
using EvenDriven.Domain.Enums;
using EvenDriven.Domain.ValueObjects.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Booking.DTOs
{
    public sealed record BookingDtoRes(
        Guid id, 
        Guid userId,
        string status,
        DateTime startDate,
        DateTime endDate,
        DateTime createdAt
    )
    {
        public static BookingDtoRes FromEntity(BookingEntity entity)
        {
            return new BookingDtoRes(
                id: entity.Id,
                userId: entity.UserId,
                status: entity.Status.ToString(),
                startDate: entity.DateRange.StartDate,
                endDate: entity.DateRange .EndDate,
                createdAt: entity.CreatedAt

            );
        }
    }
}
