using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Auth.DTOs
{
    public sealed record UserAuthReqDto(
        string email,
        string password
    );
}
