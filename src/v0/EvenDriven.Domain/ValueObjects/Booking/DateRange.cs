using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.ValueObjects.Booking
{
    public sealed class DateRange
    {
        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
                throw new ArgumentException("End date must be after start date.");

            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate 
        { 
            get;
            init;
        }

        public DateTime EndDate
        {
            get;
            init;
        }

        public int NbDays => (EndDate - StartDate).Days;
    }
}
