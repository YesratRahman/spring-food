using Microsoft.EntityFrameworkCore;
using SpringFoodBackend.Exceptions;
using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Repos
{
    public class UserRepo : IUser
    {
        private SpringFoodDbContext _context;
        public UserRepo(SpringFoodDbContext context)
        {
            _context = context;
        }

        
        public int AddUser(User toAdd)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException("User is null"); 
            }
            _context.Users.Add(toAdd);
            _context.SaveChanges();
            return toAdd.Id; 
        }

        public void DeleteUser(int id)
        {
            if(id <= 0)
            {
                throw new InvalidIdException("Invalid user Id"); 
            }
            User toDelete = _context.Users.Find(id);
            if (toDelete == null)
            {
                throw new UserNotFoundException($"Couldn't find user with id {id}"); 
            }
            _context.Attach(toDelete);
            _context.Remove(toDelete);
            _context.SaveChanges(); 
        }

        public void EditUser(User toEdit)
        {
            if (toEdit == null)
            {
                throw new ArgumentNullException("Null user can not be edited!");
            }
            if(toEdit.Id <= 0)
            {
                throw new InvalidIdException("User Id is invalid"); 
            }
            if (_context.Users.Find(toEdit.Id) == null)
            {
                throw new UserNotFoundException($"Couldn't find user with id {toEdit.Id}");
            }
            _context.Attach(toEdit);
            _context.Entry(toEdit).State = EntityState.Modified;
            _context.SaveChanges(); 
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList(); 
        }

        public User GetUserById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Invalid user Id");
            }
            User toGet = _context.Users.Find(id);
            if (toGet == null)
            {
                throw new UserNotFoundException($"Couldn't find user with id {id}");
            }
            return toGet; 
        }
        public Role GetRoleByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Role name is null"); 
            }
          Role role =  _context.Roles.SingleOrDefault(r => r.Name.ToLower() == name.ToLower());
            if (role == null)
                throw new InvalidRoleNameException($"Couldn't find product with {name}");
            return role; 
        }

        public User GetUserByUserName(string username)
        {
            if (username == null)
                throw new ArgumentNullException("Username is null!");
            User toReturnUser = _context.Users.Include("Roles.SelectedRole").SingleOrDefault(u => u.Username.ToLower() == username.ToLower());
            if (toReturnUser == null)
                throw new InvalidUsernameException($"Couldn't find user with {username}");
            return toReturnUser; 
        }
    }
}
