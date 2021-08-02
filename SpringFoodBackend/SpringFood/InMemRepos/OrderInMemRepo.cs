using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.InMemRepos
{
    public class OrderInMemRepo : IOrder
    {
        private List<Order> _allOrders = new List<Order>(); 
        public int AddOrder(Order toAdd)
        {
            toAdd.Id = _allOrders.Count + 1;
            _allOrders.Add(toAdd);
            return toAdd.Id; 
        }

        public void DeleteOrder(int id)
        {
             _allOrders.RemoveAll(o => o.Id == id); 
        }

        public void EditOrder(Order toEdit)
        {
            _allOrders = _allOrders.Select(o => o.Id == toEdit.Id ? toEdit : o).ToList(); 
        }

        public List<Order> GetAllOrders()
        {
            return _allOrders; 
        }

        public Order GetOrderById(int id)
        {
            return _allOrders.SingleOrDefault(o => o.Id == id); 
        }

        public List<Order> GetOrdersByUserId(int curUserId)
        {
            List<Order> allOrders = new List<Order>(); 
            foreach (Order o in _allOrders)
            {
                if(o.Purchaser.Id == curUserId)
                {
                    allOrders.Add(o); 
                }
            }
            return allOrders; 
        }
    }
}
