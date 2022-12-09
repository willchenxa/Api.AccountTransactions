namespace Api.AccountTransactions.Models
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transaction>().HasKey(t => t.id);
            modelBuilder.Entity<Customer>().HasKey(c => c.id);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.owner);

            base.OnModelCreating(modelBuilder);
        }
    }
}
