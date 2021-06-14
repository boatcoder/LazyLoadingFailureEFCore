using System;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace LazyLoader
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<ParticipantList> ParticipantLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .LogTo(Console.WriteLine)
                    .EnableSensitiveDataLogging(true);
                
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasOne(d => d.ParticipantList)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.ParticipantListId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
