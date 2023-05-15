using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Adstra_task
{
    interface IUnitOfWork: IDisposable
    {
      
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();



    }



}
