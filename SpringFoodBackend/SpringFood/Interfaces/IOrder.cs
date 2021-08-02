
using SpringFoodBackend.Models.Domain;
using System.Collections.Generic;

namespace SpringFoodBackend.Interfaces
{
    public interface IOrder
    {
        int AddOrder(Order toAdd);
        Order GetOrderById(int id);
        List<Order> GetAllOrders();
        void EditOrder(Order toEdit);
        void DeleteOrder(int id);
        public List<Order> GetOrdersByUserId(int curUserId);

    }
}
