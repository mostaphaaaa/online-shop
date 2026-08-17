using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface IOrderRepository
    {
        bool IsOpenOrderExist(int userId);
    }
}
