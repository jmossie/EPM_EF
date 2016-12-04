using EPM_EF.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EPM_EF.DAL
{
    public class EPMContext : DbContext
    {

        public EPMContext() : base("EPMContext")
        {
        }

        public DbSet<Drawing> Drawings { get; set; }
        public DbSet<DrawingRevision> DrawingRevisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}