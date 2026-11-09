namespace EvenDriven.App.Common.Helpers
{
    public static class NormalizeEmail
    {
        public static string Normalize(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or whitespace.", nameof(email));
            }
            return email.Trim().ToLowerInvariant();
        }
    }
}
