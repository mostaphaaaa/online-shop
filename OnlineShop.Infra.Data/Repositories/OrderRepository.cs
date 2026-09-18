using OnlineShop.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public bool IsOpenOrderExist(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
