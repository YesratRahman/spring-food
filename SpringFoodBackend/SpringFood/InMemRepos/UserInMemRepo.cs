using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.InMemRepos
{
    public class UserInMemRepo : IUser
    {
        private List<User> _allUsers = new List<User>();
        private List<Role> _allRoles = new List<Role>(); 

        public UserInMemRepo()
        {
            _allRoles.Add(new Role { Id = 1, Name = "Admin" });
            _allRoles.Add(new Role { Id = 2, Name = "User" }); 
        }
        public int AddUser(User toAdd)
        {
            toAdd.Id = _allUsers.Count + 1;
            _allUsers.Add(toAdd);
            return toAdd.Id;
        }

        public void DeleteUser(int id)
        {
            _allUsers.RemoveAll(p => p.Id == id);
        }

        public void EditUser(User toEdit)
        {
            _allUsers = _allUsers.Select(w => w.Id == toEdit.Id ? toEdit : w).ToList();
        }

        public List<User> GetAllUsers()
        {
            return _allUsers;
        }

        public Role GetRoleByName(string name)
        {
            return _allRoles.SingleOrDefault(r => r.Name.ToLower() == name.ToLower()); 
        }

        public User GetUserById(int id)
        {
            return _allUsers.SingleOrDefault(u => u.Id == id);
        }

        public User GetUserByUserName(string username)
        {

            return _allUsers.SingleOrDefault(u => u.Username == username);
        }
    }
}
