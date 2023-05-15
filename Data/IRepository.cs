using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


    public interface IRepository<TEntity> where TEntity :class
    {

        void Insert(TEntity item);

        void Update(TEntity item);
        TEntity Update(TEntity t, params Object[] key);
        TEntity Update(TEntity t, int Key);
        void Delete(TEntity item);
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default);


    //void Get(TEntity item);
    //TEntity Get(TEntity t, params Object[] key);


    //EntityEntry<TEntity> AddIfNotExists(this DbSet<TEntity> dbSet, TEntity entity, Expression<Func<TEntity, bool>> predicate = null);
}

