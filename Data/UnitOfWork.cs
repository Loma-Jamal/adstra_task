using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Adstra_task
{
    public class UnitOfWork :IUnitOfWork
    {

        protected AppDBContext Context { get; }
        public UnitOfWork()
        {
            Context = new AppDBContext();
        }
        public UnitOfWork(AppDBContext context)
        {
            Context = context;

            Context.ChangeTracker.LazyLoadingEnabled = false;
            Context.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds);
        }
        private IRepository<User> _TblUsers;
        public IRepository<User> TblUsers => _TblUsers ?? (_TblUsers = new Repository<User>(Context));
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
                => await Context.SaveChangesAsync(cancellationToken);
        public virtual int SaveChanges()
                => Context.SaveChanges();

      
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

    }
}
