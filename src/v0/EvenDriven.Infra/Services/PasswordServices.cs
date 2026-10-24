using EvenDriven.App.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.Infra.Services
{
    public sealed class PasswordServices : IPasswordServices
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
