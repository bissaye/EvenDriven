using EvenDriven.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Auth.DTOs
{
    public sealed record UserAuthResDto(
        Guid id,
        string email,
        string firstName,
        string lastName,
        string token
    )
    {
        public static UserAuthResDto FromEntity( UserEntity user, string token)
        {
            return new UserAuthResDto (
                id: user.Id,
                email: user.Email,
                firstName: user.FirstName,
                lastName: user.LastName,
                token: token
            );
        }
    }
}
