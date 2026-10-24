using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Primitives
{
    public sealed class PagedResult<T>
    {
        public int TotalCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public IReadOnlyList<T> Items { get; init; } = new List<T>().AsReadOnly();
    }
}
