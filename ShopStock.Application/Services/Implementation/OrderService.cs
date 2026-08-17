using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implementation
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IGenericRepository<Product> genericRepository;

        public OrderService(IOrderRepository orderRepository,IGenericRepository<Product> genericRepository)
        {
            this.orderRepository = orderRepository;
            this.genericRepository = genericRepository;
        }
    }
}
