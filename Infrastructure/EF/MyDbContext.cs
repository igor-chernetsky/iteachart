using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.EF.Mappings;

namespace Infrastructure.EF
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(string connectionString)
            : base(connectionString)
        {
            // ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UserMap)));
        }
    }
}
