using CodingWiki_Model.Models;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        //public DbSet<Genere> Generes { get; set; }
        public DbSet<BookDetail> BookDetails{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(u => u.BookId);
                entity.Property(u=> u.ISBN).IsRequired().HasMaxLength(20);
                entity.Ignore(u=> u.PriceRange);
            });

            modelBuilder.Entity<BookDetail>(entity =>
            {
                entity.HasKey(u => u.BookDetail_Id);

                entity.HasOne(d => d.Book)
                .WithOne(b => b.BookDetail)
                .HasForeignKey<BookDetail>(d => d.BookId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(u => u.Author_Id);
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(u => u.Publisher_Id);
                entity.Property(u => u.Name).IsRequired();

                entity.HasMany(u => u.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.Publisher_Id);
            });

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(u => u.ToTable("BookAuthorMap01"));



            //modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(10, 5);

            //modelBuilder.Entity<Book>().HasData(new Book { BookId = 1 ,Title="Spiderman",ISBN="123456",Price=12.3}); 
            //modelBuilder.Entity<Book>().HasData(new Book { BookId = 2 ,Title="Superman",ISBN="333333",Price=22.3});

            //var booklist = new Book[] {
            //     new Book { BookId = 3 ,Title="Spiderman",ISBN="123456",Price=12.3},
            //     new Book { BookId = 4, Title = "Superman", ISBN = "333333", Price = 22.3 }
            //};

            //modelBuilder.Entity<Book>().HasData(booklist);

        }
    }
}
