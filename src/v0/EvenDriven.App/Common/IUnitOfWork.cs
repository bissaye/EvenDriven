using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Common
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
