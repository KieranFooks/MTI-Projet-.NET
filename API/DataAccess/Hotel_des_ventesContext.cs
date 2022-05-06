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
                optionsBuilder.UseSqlServer("Data Source=.\\;Initial Catalog=Hotel_des_ventes;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tinventory>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdItem });

                entity.ToTable("TInventory");

                entity.Property(e => e.IdUser).HasColumnName("Id_user");

                entity.Property(e => e.IdItem).HasColumnName("Id_item");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Tinventories)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Item");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Tinventories)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_User");
            });
            modelBuilder.Entity<Tmarket>(entity =>
            {
                entity.ToTable("TMarket");

                entity.Property(e => e.IdItem).HasColumnName("Id_item");

                entity.Property(e => e.IdSeller).HasColumnName("Id_seller");

                entity.Property(e => e.IsSold).HasColumnName("Is_sold");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Tmarkets)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Market_Item");

                entity.HasOne(d => d.IdSellerNavigation)
                    .WithMany(p => p.Tmarkets)
                    .HasForeignKey(d => d.IdSeller)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Market_User");
            });
            modelBuilder.Entity<Titem>(entity =>
            {
                entity.ToTable("TItem");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(100);              
            });

            modelBuilder.Entity<Tuser>(entity =>
            {
                entity.ToTable("TUser");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);                
            });

            modelBuilder.Entity<Titem>().HasData(new Titem
            {
                Id = 1,
                Name = "Palantír",
                Description = "Used for communication and to see events, whether past or future.",
            });

            modelBuilder.Entity<Titem>().HasData(new Titem
            {
                Id = 2,
                Name = "Lightsaber",
                Description = "Luminescent plasma blade, can cut steel.",
            });

            modelBuilder.Entity<Titem>().HasData(new Titem
            {
                Id = 3,
                Name = "Mug",
                Description = "Use it to transport a small amount of fluid, like coffee.",
            });

            modelBuilder.Entity<Titem>().HasData(new Titem
            {
                Id = 4,
                Name = "Stomb's fries",
                Description = "Mhh tasty...",
            });

            modelBuilder.Entity<Tuser>().HasData(new Tuser
            {
                Id = 1,
                Name = "Gabriel",
                Password = "test",
                Money = 5000
            });

            modelBuilder.Entity<Tuser>().HasData(new Tuser
            {
                Id = 2,
                Name = "Hugo",
                Password = "test",
                Money = 5000
            });

            modelBuilder.Entity<Tuser>().HasData(new Tuser
            {
                Id = 3,
                Name = "Kieran",
                Password = "test",
                Money = 5000
            });

            modelBuilder.Entity<Tuser>().HasData(new Tuser
            {
                Id = 4,
                Name = "Eliott",
                Password = "test",
                Money = 5000
            });

            modelBuilder.Entity<Tmarket>().HasData(new Tmarket
            {
                Id = 1,
                IdItem = 1,
                Price = 500,
                IdSeller = 1,
                Quantity = 1,
                IsSold = false
            });

            modelBuilder.Entity<Tmarket>().HasData(new Tmarket
            {
                Id = 2,
                IdItem = 2,
                IdSeller = 1,
                Price = 10000,
                Quantity = 5,
                IsSold = false
            });
            modelBuilder.Entity<Tmarket>().HasData(new Tmarket
            {
                Id = 3,
                IdItem = 2,
                IdSeller = 1,
                Price = 100,
                Quantity = 1,
                IsSold = true
            });

            modelBuilder.Entity<Tmarket>().HasData(new Tmarket
            {
                Id = 4,
                IdItem = 2,
                IdSeller = 2,
                Price = 1000,
                Quantity = 3,
                IsSold = false
            });

            modelBuilder.Entity<Tinventory>().HasData(new Tinventory
            {
                IdUser = 1,
                IdItem = 1,
                Quantity = 1               
            });
            modelBuilder.Entity<Tinventory>().HasData(new Tinventory
            {
                IdUser = 1,
                IdItem = 2,
                Quantity = 10
            });
            modelBuilder.Entity<Tinventory>().HasData(new Tinventory
            {
                IdUser = 2,
                IdItem = 3,
                Quantity = 5
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
