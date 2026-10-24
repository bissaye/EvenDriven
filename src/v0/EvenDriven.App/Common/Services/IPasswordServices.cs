using System;
using System.Collections.Generic;
using System.Text;

namespace EvenDriven.App.Common.Services
{
    public interface IPasswordServices
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
