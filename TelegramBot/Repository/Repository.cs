using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Models;

namespace TelegramBot.Repository
{
    public class Repository<T> : IRepository<T>  where T : class
    {
        private readonly TelegramContext _telegramContext;
        private readonly DbSet<T> _dbSet;

        public Repository(TelegramContext telegramContext)
        {
            _telegramContext = telegramContext;
            _dbSet = _telegramContext.Set<T>();
        }

        public async Task AddAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public async Task<T> FindAsync(string discription)
        {
            return await _dbSet.FindAsync(discription);
        }

        public T FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public T FindByChatId(long chatId)
        {
            return _dbSet.Find(chatId);
        }

        public T FindLast(int n)
        {
            return _dbSet.Find(n);
        }

        public T FindFirst()
        {
            return _dbSet.First();
        }
        
        public async Task<T> FindByNumberAsync(int number)
        {
            return await _dbSet.FindAsync(number);
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {


            var result = _dbSet.AsQueryable();

            if (includes != null)
                result = includes(result);

            return await Task.FromResult(predicate != null ? result.Where(predicate) : result);
        }

        public async Task Edit(T obj)
        {
            _dbSet.Update(obj);
            await SaveChangeAsync();
        }

        private async Task SaveChangeAsync()
        {
            await _telegramContext.SaveChangesAsync();
        }





    }
}
