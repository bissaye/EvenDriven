using EvenDriven.App.Common;
using EvenDriven.App.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace EvenDriven.Infra.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                if(IsUniqueConstraintViolationOnProperty(ex, "Email"))
                {
                    throw new EmailAlreadyExistsException(ex);
                }

                throw;
            }
        }

        private bool IsUniqueConstraintViolationOnProperty(DbUpdateException ex, string propertyName)
        {
            var errorMessage = ex.InnerException?.Message;
            if (string.IsNullOrEmpty(errorMessage))
            {
                return false;
            }

            foreach(var entry in ex.Entries)
            {

                var entityType = _dbContext.Model.FindEntityType(entry.Entity.GetType());
                if (entityType == null)
                    continue;

                var property = entityType.GetProperties()
                    .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
                if (property == null)
                    continue;

                var uniqueInddexes = entityType.GetIndexes()
                    .Where(i => i.IsUnique && i.Properties.Contains(property));

                foreach (var index in uniqueInddexes)
                {
                    var indexName = index.GetDatabaseName();

                    if (!string.IsNullOrEmpty(indexName) && errorMessage.Contains(indexName, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;

        }
    }
}
