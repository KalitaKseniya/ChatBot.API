using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ChatBot.Core.Models
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress2019;Database=ChatBot;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.Property(e => e.BotResponse)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.NextIds)
                    .HasMaxLength(20)
                    .HasColumnName("NextIDs");

                entity.Property(e => e.UserRequest)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
