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

        public virtual DbSet<Tinventory> Tinventories { get; set; } = null!;
        public virtual DbSet<Titem> Titems { get; set; } = null!;
        public virtual DbSet<Tmarket> Tmarkets { get; set; } = null!;
        public virtual DbSet<Tuser> Tusers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-N5RJ51B;Initial Catalog=Hotel_des_ventes;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tinventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TInventory");

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

            modelBuilder.Entity<Titem>(entity =>
            {
                entity.ToTable("TItem");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasColumnType("text");
            });

            modelBuilder.Entity<Tmarket>(entity =>
            {
                entity.ToTable("TMarket");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdItem).HasColumnName("Id_item");

                entity.Property(e => e.IdSeller).HasColumnName("Id_seller");

                entity.Property(e => e.IsSold).HasColumnName("Is_sold");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Tmarket)
                    .HasForeignKey<Tmarket>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Market_User");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Tmarkets)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Market_Item");
            });

            modelBuilder.Entity<Tuser>(entity =>
            {
                entity.ToTable("TUser");

                entity.Property(e => e.Name).HasColumnType("text");

                entity.Property(e => e.Password).HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
