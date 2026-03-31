using System.Linq;
using EventApi.Models;
using EventApi.Data;

namespace EventApi.Services
{
    public class UserService
    {
        public void Register(User user)
        {
            AppDb.Users.Add(user);
        }

        public User Login(string email, string password)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return AppDb.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}