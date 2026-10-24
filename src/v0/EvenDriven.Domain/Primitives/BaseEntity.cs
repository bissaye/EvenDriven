using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Primitives
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
