using EvenDriven.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.User.DTOs
{
    public sealed record UserDtoRes (
        Guid id,
        string? firstName,
        string? lastName,
        string email    
        )
    {
        public static UserDtoRes FromEntity(UserEntity user)
        {
            return new UserDtoRes(
                id: user.Id,
                firstName: user.FirstName,
                lastName: user.LastName,
                email: user.Email
            );
        }
    }
}
