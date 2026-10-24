using EvenDriven.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Domain.Entities
{
    public sealed class UserEntity : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public IEnumerable<BookingEntity> Bookings { get; set; }


        private UserEntity() { }

        public static UserEntity Create(string? firstName, string? lastName, string email, string passwordHash)
        {
            return new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash
            };
        }
    }
}
