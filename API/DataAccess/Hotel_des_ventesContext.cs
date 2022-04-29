using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.DataAccess
{
    public partial class Hotel_des_ventesContext : DbContext
    {
        public Hotel_des_ventesContext()
        {
        }

        public Hotel_des_ventesContext(DbContextOptions<Hotel_des_ventesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Market> Markets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-N5RJ51B;Initial Catalog=Hotel_des_ventes;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Inventory");

                entity.Property(e => e.IdItem).HasColumnName("Id_item");

                entity.Property(e => e.IdUser).HasColumnName("Id_user");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Item");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_User");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasColumnType("text");
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.ToTable("Market");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IdItem).HasColumnName("Id_item");

                entity.Property(e => e.IdSeller).HasColumnName("Id_seller");

                entity.Property(e => e.IsSold).HasColumnName("Is_sold");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Market)
                    .HasForeignKey<Market>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Market_User");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Markets)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Market_Item");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("text");

                entity.Property(e => e.Password).HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
