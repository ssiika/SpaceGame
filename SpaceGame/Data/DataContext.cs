namespace SpaceGame.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<SaveFile> SaveFiles => Set<SaveFile>();
    }
}
