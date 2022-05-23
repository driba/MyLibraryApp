using Ispit.Books.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ispit.Books.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Dodatno podešavanje ograničenja između Book i Author
            builder
                .Entity<Book>()
                .HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Dodatno ograničenje između Book i Publisher
            builder
                .Entity<Book>()
                .HasOne(p => p.Publisher)
                .WithMany(b => b.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

        }


        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}