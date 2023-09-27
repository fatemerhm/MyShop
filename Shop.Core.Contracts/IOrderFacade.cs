using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Contracts
{
    public interface IOrderFacade
    {
        void SaveOrder(Order order);

        void SetTransactionId(int orderId, string token);

        void PaymentDone(string token, string tId);
    }
}
