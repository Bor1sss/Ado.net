using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Author.Models
{
    public partial class AuthorContext:DbContext
    {

        static DbContextOptions<AuthorContext> _options;

        static AuthorContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AuthorContext>();
            _options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
        }
        public AuthorContext() : base(_options)
        {
            if (Database.EnsureCreated())
            {
               /* Author shakespeare = new Author { Name = "William Shakespeare" };
                Author dostoevsky = new Author { Name = "Fyodor Dostoevsky" };
                Author tolkien = new Author { Name = "J.R.R. Tolkien" };

                Authors?.Add(shakespeare);
                Authors?.Add(dostoevsky);
                Authors?.Add(tolkien);

                Books?.Add(new Book { Name = "Hamlet", Title = "The Tragedy of Hamlet, Prince of Denmark", Author = shakespeare });
                Books?.Add(new Book { Name = "Romeo and Juliet", Title = "Romeo and Juliet", Author = shakespeare });

                Books?.Add(new Book { Name = "Crime and Punishment", Title = "Преступление и наказание", Author = dostoevsky });
                Books?.Add(new Book { Name = "The Brothers Karamazov", Title = "Братья Карамазовы", Author = dostoevsky });

                Books?.Add(new Book { Name = "The Lord of the Rings", Title = "The Fellowship of the Ring", Author = tolkien });
                Books?.Add(new Book { Name = "The Hobbit", Title = "The Hobbit, or There and Back Again", Author = tolkien });

                SaveChanges();*/
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            modelBuilder.Entity<Book>().HasOne(p => p.Author).WithMany(t => t.Books).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }*/
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_dbo.Authors");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_dbo.Books");

                entity.HasIndex(e => e.Id, "IX_Authors_Id");

                entity.HasOne(d => d.Author).WithMany(p => p.Books)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_dbo.Books_dbo.FK_Books_Authors_AuthorId");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
