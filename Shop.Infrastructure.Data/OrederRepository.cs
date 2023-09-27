using Shop.Core.Contracts;
using Shop.Core.Domain;
using Shop.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data
{
    public class OrederRepository : IOrderRepository
    {
        private readonly ShopContext context;

        public OrederRepository(ShopContext context)
        {

            this.context = context;
        }
        public void PaymentDone(string token, string tId)
        {
            try
            {
                var order = context.Orders.Where(c => c.paymentToken == token.ToString()).First();
                order.PaymentDate = DateTime.Now;
                order.PaymentId = tId.ToString();

                context.SaveChanges();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void Save(Order order)
        {
            context.AttachRange(order.Lines.Select(a => a.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }

        public void SetOrderToken(int orderId, string token)
        {
            try
            {
                var order = context.Orders.Find(orderId);
                order.paymentToken = token;
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
