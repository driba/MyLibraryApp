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

            #region PublisherSeed
            builder.Entity<Publisher>().HasData(
                new { PublisherId = 1, Title = "Školska knjiga"},
                new { PublisherId = 2, Title = "Mozaik knjiga"},
                new { PublisherId = 3, Title = "Algebra"});
            #endregion

            #region AuthorsSeed
            builder.Entity<Author>().HasData(
                new { AuthorId = 1, PenName = "Luka Nekić"},
                new { AuthorId = 2, PenName = "J. R. R. Tolkien", FullName = "John Ronald Reuel Tolkien" },
                new { AuthorId = 3, PenName = "Mato Lovrak", FullName = "Mato Lovrak" },
                new { AuthorId = 4, PenName = "Braća Grimm", FullName = "Jacob i Wilhelm Grimm" },
                new { AuthorId = 5, PenName = "D.R.", FullName = "Dorian Ribarić" });
            #endregion
        }


        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}