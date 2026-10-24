using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.User.DTOs
{
    public sealed record RegisterUserDtoReq(
        string? FirstName,
        string? LastName,
        string Email,
        string Password
        );
}
