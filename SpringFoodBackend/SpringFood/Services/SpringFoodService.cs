using Microsoft.IdentityModel.Tokens;
using SpringFoodBackend.Exceptions;
using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Auth;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Models.ViewModels.Requests;
using SpringFoodBackend.Repos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SpringFoodBackend.Services
{
    public class SpringFoodService : ISpringFoodService
    {
        IProduct _product;
        IUser _user;
        ICategory _category;
        IOrder _order;
        public SpringFoodService(IProduct productDao, IUser userDao, ICategory categoryDao, IOrder orderDao)
        {
            _product = productDao;
            _user = userDao;
            _category = categoryDao;
            _order = orderDao;
        }

        public void RegisterUser(RegisterUserViewModel toAdd)
        {
            if (toAdd.Username == null)
            {
                throw new ArgumentNullException("Username is null");
            }
            if (toAdd.Email == null)
            {
                throw new ArgumentNullException("Email is null");
            }
            if (toAdd.Password == null)
            {
                throw new ArgumentNullException("Password is null");
            }
            if (toAdd.Username == "" || toAdd.Username.Trim().Length == 0)
            {
                throw new InvalidNameException("Username can't be empty or have white space.");
            }
            if (toAdd.Email == "" || toAdd.Email.Trim().Length == 0)
            {
                throw new InvalidEmailException("Email address can not be empty or have white space");
            }
            if (toAdd.Password.Length > 8 || toAdd.Password == "" || toAdd.Password.Trim().Length == 0)
            {
                throw new InvalidPasswordException("Password has to be in the length of 8, can't be empty or have white space.");
            }
            User previouslyUserName = _user.GetUserByUserName(toAdd.Username);
            if (previouslyUserName != null)
            {
                throw new UserNameInUseException();
            }
            Role basicRole = _user.GetRoleByName("user");
            UserRole bridgeRow = new UserRole();
            bridgeRow.RoleId = basicRole.Id;
            bridgeRow.SelectedRole = basicRole;

            User toAddUser = new User();
            bridgeRow.EnrolledUser = toAddUser;
            toAddUser.Roles.Add(bridgeRow);

            toAddUser.Email = toAdd.Email;
            toAddUser.Username = toAdd.Username;
            using (var hMac = new System.Security.Cryptography.HMACSHA512())
            {
                byte[] salt = hMac.Key;
                byte[] hash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(toAdd.Password));
                toAddUser.PasswordSalt = salt;
                toAddUser.PasswordHash = hash;
            }
            _user.AddUser(toAddUser);
        }

        public LoginResponse Login(LoginRequest loginRe)
        {
            if (loginRe.Username == null)
            {
                throw new ArgumentNullException("Username is null.");

            }
            if (loginRe.Password == null)
            {
                throw new ArgumentNullException("Password is null.");
            }
            
            User curUser = _user.GetUserByUserName(loginRe.Username);
            bool passValidated = this.ValidatePassword(loginRe.Password, curUser.PasswordSalt, curUser.PasswordHash);
            if (!passValidated)
            {
                throw new Exceptions.InvalidPasswordException();
            }
            string token = this.GenerateToken(curUser);
            LoginResponse response = new LoginResponse();
            response.Token = token;
            response.Username = curUser.Username;
            response.IsAdmin = curUser.Roles.Any(r => r.SelectedRole.Name == "Admin");
            return response;
        }

        private string GenerateToken(User curUser)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    curUser.Roles.Select(r => new Claim(
                        ClaimTypes.Role, r.SelectedRole.Name)).Append(
                        new Claim(ClaimTypes.NameIdentifier, curUser.Id.ToString()))),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private bool ValidatePassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hMac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                byte[] passHased = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != passHased[i])
                    {
                        return false;
                    }
                }
                return true;

            }
        }
        public void DeleteUser(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("User id is Invalid");
            }
            _user.DeleteUser(id);
        }
        public List<User> GetAllUsers()
        {
            return _user.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("User id is Invalid");
            }
            return _user.GetUserById(id);
        }
        public int AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Can't add a null product");
            }
            if (product.Name == null)
            {
                throw new ArgumentNullException("Can't add a product with null name");
            }
            if (product.Description == null)
            {
                throw new ArgumentNullException("Can't add a product with null description");
            }

            if (product.Image == null)
            {
                throw new ArgumentNullException("Can't add a product with null image");
            }
            if (product.Name == "")
            {
                throw new InvalidNameException("Can't add a product with empty name");
            }
            if (product.CategoryId <= 0)
            {
                throw new InvalidIdException("Invalid Id");
            }
            if (product.Price <= 0)
            {
                throw new InvalidPriceException("Invalid Price");
            }
            if (product.Quantity <= 0)
            {
                throw new InvalidQuantityException("Invalid quantity");
            }

            return _product.AddProduct(product);
        }

        public List<Product> GetProductByCatId(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Invalid category Id");
            }
            return _product.GetProductByCatId(id);
        }

        public Product GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Invalid product Id");

            }
            return _product.GetProductById(id);
        }

        public Product GetProductByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Product name is null");
            }
            if (name == "")
            {
                throw new InvalidNameException("Product name is invalid");
            }
            return _product.GetProductByName(name);
        }

        public List<Product> GetAllProducts()
        {
            return _product.GetAllProducts();
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Invalid product Id");

            }
            _product.DeleteProduct(id);
        }

        public void EditProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Can't add a null product");
            }
            if (product.Name == null)
            {
                throw new ArgumentNullException("Can't add a product with null name");
            }
            if (product.Description == null)
            {
                throw new ArgumentNullException("Can't add a product with null description");
            }

            if (product.Image == null)
            {
                throw new ArgumentNullException("Can't add a product with null image");
            }
            if (product.Name == "" || product.Name.Trim().Length == 0)
            {
                throw new InvalidNameException("Can't add a product with empty name");
            }
            if (product.CategoryId <= 0)
            {
                throw new InvalidIdException("Invalid Id");
            }
            if (product.Price <= 0)
            {
                throw new InvalidPriceException("Invalid Price");
            }
            if (product.Quantity <= 0)
            {
                throw new InvalidQuantityException("Invalid quantity");
            }

            _product.EditProduct(product);
        }

        public int AddCategory(Category toAdd)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException("Category is null.");
            }
            if (toAdd.Name == null)
            {
                throw new ArgumentNullException("Category name is null.");
            }
            if (toAdd.Name == "" || toAdd.Name.Trim().Length == 0)
            {
                throw new InvalidNameException("Category name can't be empty");
            }
            return _category.AddCategory(toAdd);
        }

        public List<Category> GetAllCategories()
        {
            return _category.GetAllCategories();
        }

        public int AddOrder(Order toAdd)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException("Order is null.");
            }
            if (toAdd.Name == null)
            {
                throw new ArgumentNullException("Name is null.");
            }
            if (toAdd.City == null)
            {
                throw new ArgumentNullException("City is null");
            }
            if (toAdd.Street == null)
            {
                throw new ArgumentNullException("Street is null");
            }
            if (toAdd.Email == null)
            {
                throw new ArgumentNullException("Email is null");
            }
            if (toAdd.HomeNumber == null)
            {
                throw new ArgumentNullException("Homenumber is null");
            }
            if (toAdd.Email == "" || toAdd.Email.Trim().Length == 0)
            {
                throw new InvalidEmailException("Email can't be empty or have white spaces.");
            }
            if (toAdd.Name == "" || toAdd.Name.Trim().Length == 0)
            {
                throw new InvalidNameException("Name can't be empty");
            }
            foreach (OrderDetails od in toAdd.OrderDetails)
            {
                if (od.ProductId <= 0)
                    throw new InvalidIdException("Invalid Id");
                if (od.Quantity <= 0)
                    throw new InvalidQuantityException("Invalid quantity");
            }
            return _order.AddOrder(toAdd);
        }
        public Order GetOrderById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Invalid order Id");
            }
            return _order.GetOrderById(id);
        }

        public List<Order> GetAllOrders()
        {
            return _order.GetAllOrders();
        }
        public List<Order> getOrdersByUserId(int curUserId)
        {
            if (curUserId <= 0)
            {
                throw new InvalidIdException("Invalid user id!");
            }
            return _order.GetOrdersByUserId(curUserId);
        }
        public void EditOrder(Order toEdit)
        {
            if (toEdit == null)
            {
                throw new ArgumentNullException("Order is null.");
            }
            if (toEdit.Name == null)
            {
                throw new ArgumentNullException("Name is null.");
            }
            if (toEdit.City == null)
            {
                throw new ArgumentNullException("City is null");
            }
            if (toEdit.Street == null)
            {
                throw new ArgumentNullException("Street is null");
            }
            if (toEdit.Email == null)
            {
                throw new ArgumentNullException("Email is null");
            }
            if (toEdit.HomeNumber == null)
            {
                throw new ArgumentNullException("Homenumber is null");
            }
            if (toEdit.Email == "" || toEdit.Email.Trim().Length == 0)
            {
                throw new InvalidEmailException("Invalid email, can't be empty or have white spaces.");
            }
            if (toEdit.Name == "" || toEdit.Email.Trim().Length == 0)
            {
                throw new InvalidNameException("Name can't be empty");
            }
            foreach (OrderDetails od in toEdit.OrderDetails)
            {
                if (od.ProductId <= 0)
                    throw new InvalidIdException("Invalid Id");
                if (od.Quantity <= 0)
                    throw new InvalidQuantityException("Invalid quantity");
            }
            _order.EditOrder(toEdit);
        }

        public void DeleteOrder(int id)
        {
            if (id <= 0)
            {
                throw new InvalidIdException("Invalid order Id.");
            }
            _order.DeleteOrder(id);
        }

    }
} 