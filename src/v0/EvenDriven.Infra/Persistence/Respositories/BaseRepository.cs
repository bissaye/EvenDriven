using EvenDriven.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EvenDriven.Infra.Persistence.Respositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
       
        protected readonly AppDbContext _dbContext;
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<Result<PagedResult<T>>> GetAllAsync(PagedQuery pagedQuery, CancellationToken cancellationToken = default)
        {
            var totalCount = await _dbContext.Set<T>().CountAsync(cancellationToken);
            var Data = await _dbContext.Set<T>().Skip(pagedQuery.skip).Take(pagedQuery.PageSize).ToListAsync(cancellationToken);
            return Result<PagedResult<T>>.Success(new PagedResult<T>()
            {
                Items = Data,
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalCount = totalCount
            });
        }

        public async Task<Result<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
            if (entity == null) 
            {
                return Result<T>.Failure(new Error("404", "Data not found", ErrorType.NotFound));
            }
            return Result<T>.Success(entity);
        }

        public async Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
            if(entity == null)
            {
                return Result<T>.Failure(new Error("404", "Data not found", ErrorType.NotFound));
            }
            return Result<T>.Success(entity);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
