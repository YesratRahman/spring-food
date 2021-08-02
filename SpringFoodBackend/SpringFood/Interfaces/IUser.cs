 
using SpringFoodBackend.Models.Auth;
using System.Collections.Generic;

namespace SpringFoodBackend.Interfaces
{
    public interface IUser
    {
        int AddUser(User toAdd);
        User GetUserById(int id);
        List<User> GetAllUsers();
        void EditUser(User toEdit);
        void DeleteUser(int id);
        Role GetRoleByName(string name);
        User GetUserByUserName(string username);

    }
}
