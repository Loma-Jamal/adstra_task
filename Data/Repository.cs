using Adstra_task;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected DbContext Context { get; }
    protected DbSet<TEntity> Set { get; }
    public Repository(AppDBContext context)
    {
        Context = context;
        Set = context.Set<TEntity>();
    }


    // protected DbQuery<TQuery> dbQuery { get; }

    public virtual async Task<TEntity> Find(object[] keyValues, CancellationToken cancellationToken = default)
        => Set.Find(keyValues, cancellationToken);

    public virtual async Task<TEntity> Find<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        => await Find(new object[] { keyValue }, cancellationToken);

    public virtual async Task<bool> Exists(object[] keyValues, CancellationToken cancellationToken = default)
    {
        var item = await Find(keyValues, cancellationToken);
        return item != null;
    }

    public virtual IQueryable<TEntity> Queryable() => Set;

    public virtual void Insert(TEntity item)
            => Context.Entry(item).State = EntityState.Added;

    public virtual void Update(TEntity item)
        => Context.Entry(item).State = EntityState.Modified;

    public virtual TEntity Update(TEntity t, params Object[] key)
    {
        if (t == null)
            return null;
        TEntity exist = Context.Set<TEntity>().Find(key);
        if (exist != null)
        {
            Context.Entry(exist).CurrentValues.SetValues(t);

        }
        return exist;
    }
    public virtual TEntity Update(TEntity t, int Key)
    {

        Context.Entry(t).CurrentValues.SetValues(t);

        return t;
    }
    public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = default)
    {
        var item = await Find(keyValues, cancellationToken);
        if (item == null) return false;
        Context.Entry(item).State = EntityState.Deleted;
        return true;
    }
    public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default)
        => await DeleteAsync(new object[] { keyValue }, cancellationToken);
    // Call Store Procdure Any Table

    public virtual void Delete(TEntity item)
   => Context.Entry(item).State = EntityState.Deleted;

    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                Context.Dispose();
                GC.Collect();
            }
            this.disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        GC.Collect();
    }

    private AppDBContext AddToContext(AppDBContext context,
        TEntity entity, int count, int commitCount, bool recreateContext, bool Insert)
    {
        if (Insert)
            context.Set<TEntity>().Add(entity);
        else
            context.Set<TEntity>().Update(entity);

        if (count % commitCount == 0)
        {
            context.SaveChanges();
            if (recreateContext)
            {
                context.Dispose();
                context = new AppDBContext();
                context.ChangeTracker.AutoDetectChangesEnabled = false;
            }
        }

        return context;
    }



}

