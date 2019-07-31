
namespace JWTDemo
{
    public class User
    {

        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public User()
        {
        }

        public User(string userId, string emailAddress, string password, string userType)
        {
            this.UserId = userId;
            this.EmailAddress = emailAddress;
            this.Password = password;
            this.UserType = userType;
        }
    }
}
