using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace WebAppProject.Interfaces
{
    public interface IOrderRepository
    {


        bool CreateOrder(Order order);


        Order GetOrder(Guid id);


        List<Order> GetUnCompletedOrders();


        List<Order> GetCompletedOrders();


        List<CartProduct> ViewOrderProducts(Guid id);


        void MarkAsCompletedOrder(Guid id);


    }
}
