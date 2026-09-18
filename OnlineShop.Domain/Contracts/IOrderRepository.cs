using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface IOrderRepository
    {
        bool IsOpenOrderExist(int userId);
    }
}
