﻿// <auto-generated />
using API.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Hotel_des_ventesContext))]
    partial class Hotel_des_ventesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API.DataAccess.Tinventory", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("int")
                        .HasColumnName("Id_user");

                    b.Property<int>("IdItem")
                        .HasColumnType("int")
                        .HasColumnName("Id_item");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("IdUser", "IdItem");

                    b.HasIndex("IdItem");

                    b.ToTable("TInventory", (string)null);

                    b.HasData(
                        new
                        {
                            IdUser = 1,
                            IdItem = 1,
                            Quantity = 1
                        },
                        new
                        {
                            IdUser = 1,
                            IdItem = 2,
                            Quantity = 10
                        },
                        new
                        {
                            IdUser = 2,
                            IdItem = 3,
                            Quantity = 5
                        });
                });

            modelBuilder.Entity("API.DataAccess.Titem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TItem", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Used for communication and to see events, whether past or future.",
                            Name = "Palantír"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Luminescent plasma blade, can cut steel.",
                            Name = "Lightsaber"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Use it to transport a small amount of fluid, like coffee.",
                            Name = "Mug"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Mhh tasty...",
                            Name = "Stomb's fries"
                        });
                });

            modelBuilder.Entity("API.DataAccess.Tmarket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdItem")
                        .HasColumnType("int")
                        .HasColumnName("Id_item");

                    b.Property<int>("IdSeller")
                        .HasColumnType("int")
                        .HasColumnName("Id_seller");

                    b.Property<bool>("IsSold")
                        .HasColumnType("bit")
                        .HasColumnName("Is_sold");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdItem");

                    b.HasIndex("IdSeller");

                    b.ToTable("TMarket", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IdItem = 1,
                            IdSeller = 1,
                            IsSold = false,
                            Price = 500,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 2,
                            IdItem = 2,
                            IdSeller = 1,
                            IsSold = false,
                            Price = 10000,
                            Quantity = 5
                        },
                        new
                        {
                            Id = 3,
                            IdItem = 2,
                            IdSeller = 1,
                            IsSold = true,
                            Price = 100,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 4,
                            IdItem = 2,
                            IdSeller = 2,
                            IsSold = false,
                            Price = 1000,
                            Quantity = 3
                        });
                });

            modelBuilder.Entity("API.DataAccess.Tuser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TUser", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Money = 5000,
                            Name = "Gabriel",
                            Password = "test"
                        },
                        new
                        {
                            Id = 2,
                            Money = 5000,
                            Name = "Hugo",
                            Password = "test"
                        },
                        new
                        {
                            Id = 3,
                            Money = 5000,
                            Name = "Kieran",
                            Password = "test"
                        },
                        new
                        {
                            Id = 4,
                            Money = 5000,
                            Name = "Eliott",
                            Password = "test"
                        });
                });

            modelBuilder.Entity("API.DataAccess.Tinventory", b =>
                {
                    b.HasOne("API.DataAccess.Titem", "IdItemNavigation")
                        .WithMany("Tinventories")
                        .HasForeignKey("IdItem")
                        .IsRequired()
                        .HasConstraintName("FK_Inventory_Item");

                    b.HasOne("API.DataAccess.Tuser", "IdUserNavigation")
                        .WithMany("Tinventories")
                        .HasForeignKey("IdUser")
                        .IsRequired()
                        .HasConstraintName("FK_Inventory_User");

                    b.Navigation("IdItemNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("API.DataAccess.Tmarket", b =>
                {
                    b.HasOne("API.DataAccess.Titem", "IdItemNavigation")
                        .WithMany("Tmarkets")
                        .HasForeignKey("IdItem")
                        .IsRequired()
                        .HasConstraintName("FK_Market_Item");

                    b.HasOne("API.DataAccess.Tuser", "IdSellerNavigation")
                        .WithMany("Tmarkets")
                        .HasForeignKey("IdSeller")
                        .IsRequired()
                        .HasConstraintName("FK_Market_User");

                    b.Navigation("IdItemNavigation");

                    b.Navigation("IdSellerNavigation");
                });

            modelBuilder.Entity("API.DataAccess.Titem", b =>
                {
                    b.Navigation("Tinventories");

                    b.Navigation("Tmarkets");
                });

            modelBuilder.Entity("API.DataAccess.Tuser", b =>
                {
                    b.Navigation("Tinventories");

                    b.Navigation("Tmarkets");
                });
#pragma warning restore 612, 618
        }
    }
}
