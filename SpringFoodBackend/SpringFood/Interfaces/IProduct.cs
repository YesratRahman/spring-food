
using SpringFoodBackend.Models.Domain;
using System.Collections.Generic;


namespace SpringFoodBackend.Interfaces
{
    public interface IProduct
    {
        int AddProduct(Product product);
        Product GetProductById(int id);
        Product GetProductByName(string name);
        List<Product> GetAllProducts();
        void EditProduct(Product product);
        void DeleteProduct(int id);
        public List<Product> GetProductByCatId(int id);

    }
}
