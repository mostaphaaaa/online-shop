using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(object id);
        T GetByIdWithInclude(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithIncludeAsync(int id);
        bool Remove(int id);
        bool Remove(T sender);
        void Add(T sender);
        void Update(T sender);
        int Save();
        Task<int> SaveAsync();

        public T Select(Expression<Func<T, bool>> where);
        public Task<T> SelectAsync(Expression<Func<T, bool>> where);
    }
}
