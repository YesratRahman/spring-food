using SpringFoodBackend.Models.Auth;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Models.ViewModels.Requests;
using System.Collections.Generic;

namespace SpringFoodBackend.Services
{
    public interface ISpringFoodService
    {
        int AddCategory(Category toAdd);
        int AddOrder(Order toAdd);
        int AddProduct(Product product);
        void DeleteOrder(int id);
        void DeleteProduct(int id);
        void DeleteUser(int id);
        void EditProduct(Product product);
        void EditOrder(Order toEdit);
        List<Category> GetAllCategories();
        List<Order> GetAllOrders();
        List<Product> GetAllProducts();
        List<User> GetAllUsers();
        Order GetOrderById(int id);
        List<Order> getOrdersByUserId(int curUserId);
        List<Product> GetProductByCatId(int id);
        Product GetProductById(int id);
        User GetUserById(int id);
        LoginResponse Login(LoginRequest loginRe);
        void RegisterUser(RegisterUserViewModel toAdd);
    }
}