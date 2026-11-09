using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Primitives
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
