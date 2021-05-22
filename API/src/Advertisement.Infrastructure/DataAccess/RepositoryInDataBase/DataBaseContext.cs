using Advertisement.Domain;
using Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityUser = Advertisement.Infrastructure.Identity.IdentityUser;


namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase
{
    public class DataBaseContext : IdentityDbContext<IdentityUser>
    {
    
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<User> DomainUsers { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Petition> Petitions { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdConfiguration());
            modelBuilder.ApplyConfiguration(new UserDomainConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());
            modelBuilder.ApplyConfiguration(new PetitionConfiguration());

            UserIdentityConfiguration.SeedIdentity(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }
       
    }
}