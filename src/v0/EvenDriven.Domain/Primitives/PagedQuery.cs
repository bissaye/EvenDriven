using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Primitives
{
    public sealed class PagedQuery
    {
        public int Page        
        { 
            get; 
            init => field = value >= 1 ? value : throw new ArgumentOutOfRangeException(nameof(Page), "Page number must be greater than 0.");
        }
        public int PageSize 
        { 
            get; 
            init => field = DefinePageSize(value);
        }

        public int skip => (Page - 1) * PageSize;

        private int DefinePageSize(int value) {
                if (value< 1)
                    throw new ArgumentOutOfRangeException(nameof(PageSize), "Page size must be greater than 0.");
            return value;
        }
    }
}
