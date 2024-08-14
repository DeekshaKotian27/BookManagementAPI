using BookManagementAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Infrastructure.Data
{
    public class AuthorDBContext: DbContext
    {
        public AuthorDBContext(DbContextOptions<AuthorDBContext> dbContextOptions): base(dbContextOptions) { }
        
        public DbSet<Author> Authors { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //author-book relationship
            modelBuilder.Entity<Books>()
                .HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);


            //publisher-book relationship
            modelBuilder.Entity<Books>()
                .HasOne(p => p.Publisher)
                .WithMany(b => b.Books)
                .HasForeignKey(p => p.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);

            //category-book relationship
            modelBuilder.Entity<Books>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Books)
                .HasForeignKey(c => c.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
