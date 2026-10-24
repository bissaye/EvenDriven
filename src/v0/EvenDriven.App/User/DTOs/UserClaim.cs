using EvenDriven.Domain.Entities;


namespace EvenDriven.App.User.DTOs
{
    public sealed record UserClaim
    {
        public Guid id { get; init; }
        public string email { get; init; }

        public UserClaim() { }
        public UserClaim(Guid id, string email)
        {
            this.id = id;
            this.email = email;
        }

        public static UserClaim FromEntity(UserEntity user)
        {
            return new UserClaim(
                user.Id,
                user.Email
            );
        }
    }
}