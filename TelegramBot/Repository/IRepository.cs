using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Repository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T obj);
        Task<T> FindAsync(string discription);
        T FindByChatId(long chatId);
        T FindLast(int n);
        Task<T> FindByNumberAsync(int discription);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        T FindById(Guid id);
        Task Edit(T obj);
        T FindFirst();


    }
}
