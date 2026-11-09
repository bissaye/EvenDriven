namespace EvenDriven.Api.Services.Concretes
{
    public class UserContext : IUserContext, IUserContextAccessor
    {
        private Guid? _userId;
        public Guid UserId => _userId ?? throw new UnauthorizedAccessException("User identity is not available.");
        public void SetUserId(Guid? userId)
        {
            if (_userId ==  null)
            {
                _userId = userId;
            }
        }
    }
}
