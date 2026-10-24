using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.ValueObjects.Booking
{
    public sealed class DateRange
    {
        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate 
        { 
            get; 
            init => field = value >= DateTime.UtcNow  ? value : throw new ArgumentException("Start date must be in the future.");
        }

        public DateTime EndDate
        {
            get;
            init => field = value < StartDate ? throw new ArgumentException("End date must be after start date.") : value;
        }

        public int NbDays => (EndDate - StartDate).Days;
    }
}
