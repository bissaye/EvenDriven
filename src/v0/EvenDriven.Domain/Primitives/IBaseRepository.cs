using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EvenDriven.Domain.Primitives
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
