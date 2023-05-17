using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adstra_task
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {

        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)  : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
    }
}
