namespace EvenDriven.Api.Services
{
    public interface IUserContext
    {
        Guid UserId { get; }
    }

    public interface IUserContextAccessor
    {
        void SetUserId(Guid? userId);
    }
}
