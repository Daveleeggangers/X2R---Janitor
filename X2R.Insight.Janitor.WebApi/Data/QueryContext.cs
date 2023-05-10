using Microsoft.EntityFrameworkCore;
using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApi.Data
{
    public class QueryContext : DbContext
    {
        public QueryContext(DbContextOptions<QueryContext> options) : base(options)
        {

        }
        public DbSet<_Querys> Querys { get; set; }
        public DbSet<QueryResult> QueryResult { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QueryResult>()
                .HasKey(b => b.ResultId);

            modelBuilder.Entity<_Querys>()
                .HasKey(b => b.TaskId);

            modelBuilder.Entity<QueryResult>()
                .HasOne(p => p.Query)
                .WithMany(d => d.QueryResult)
                .HasForeignKey(con => con.ResultId);


        }
    }
}
