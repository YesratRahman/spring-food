
using Microsoft.EntityFrameworkCore;
using SpringFoodBackend.Exceptions;
using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Domain;
using SpringFoodBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringFoodBackend.Repos
{
    public class OrderRepo: IOrder
    {
        private SpringFoodDbContext _context;

        public OrderRepo(SpringFoodDbContext context)
        {
            _context = context;
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
            _context.Orders.Add(toAdd);
            _context.SaveChanges(); 
            foreach(OrderDetails details in toAdd.OrderDetails)
            {
                if (details.ProductId <= 0)
                    throw new InvalidIdException("Invalid Id");
                if (details.Quantity <= 0)
                    throw new InvalidQuantityException("Invalid quantity");
                if (details.OrderId <= 0)
                    throw new InvalidIdException("Invalid Id"); 
                details.OrderId = toAdd.Id;
                _context.OrderDetails.Add(details); 
            }
            return toAdd.Id; 
        }

     

        public List<Order> GetAllOrders()
        {
            List<Order> toReturn = _context.Orders.ToList();
            foreach (Order order in toReturn)
            {
                order.OrderDetails = _context.OrderDetails.Where(oDetails => oDetails.OrderId == order.Id).ToList();
            }

            return toReturn;
        }

        public Order GetOrderById(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid Id");
            Order toGet = _context.Orders.Find(id);
            if (toGet == null)
            {
                throw new ArgumentNullException("Order is null"); 
            }
            toGet.OrderDetails = _context.OrderDetails.Where(oDetails => oDetails.OrderId == id).ToList();
            return toGet;
        }
        public void DeleteOrder(int id)
        {
            if (id <= 0)
                throw new InvalidIdException("Invalid Id");
            Order toDelete = _context.Orders.Find(id);
            if (toDelete == null)
            {
                throw new ArgumentNullException("Order is null");
            }
            _context.Attach(toDelete);
            _context.Remove(toDelete);
            _context.SaveChanges();
        }

        public void EditOrder(Order toEdit)
        {
            if (toEdit == null)
            {
                throw new ArgumentNullException("Order is null");
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
            _context.Attach(toEdit);
            _context.Entry(toEdit).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Order> GetOrdersByUserId(int curUserId)
        {
            if (curUserId <= 0)
                throw new InvalidIdException("Invalid Id"); 
            return _context.Orders.Where(o => o.Purchaser.Id == curUserId).ToList(); 
        }
    }
}
