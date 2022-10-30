using System.Collections.Generic;
using System.Linq;
using  ProjetoMySQL.Models;

namespace ProjetoMySQL.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "chewbacca", Password = "chewdagalera", Role = "candidato" },
                new User { Id = 2, Username = "joao", Password = "kdmeus2ptsandre", Role = "eleitor" }
            };
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}