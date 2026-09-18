using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EshopDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public void Add(T sender)
        {
            _context.Add(sender);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetByIdWithInclude(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdWithIncludeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            try
            {
                T item = _dbSet.Find(id);
                _dbSet.Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(T sender)
        {
            try
            {
                _dbSet.Remove(sender);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public T Select(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public Task<T> SelectAsync(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefaultAsync(where);
        }

        public void Update(T sender)
        {
            _dbSet.Update(sender);
        }
    }
}
