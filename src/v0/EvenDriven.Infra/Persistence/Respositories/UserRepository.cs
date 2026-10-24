using EvenDriven.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Infra.Persistence.Respositories
{
    public sealed class UserRepository : BaseRepository<UserEntity>
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
