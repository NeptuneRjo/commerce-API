﻿// <auto-generated />
using System;
using CommerceClone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommerceClone.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230727071708_References")]
    partial class References
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CommerceClone.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "public_key");

                    b.Property<string>("SecretKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "secret_key");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("CommerceClone.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("Subtotal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TotalItems")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "total_items");

                    b.Property<int?>("TotalUniqueItems")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "total_unique_items");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("CommerceClone.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ItemId");

                    b.ToTable("CartItems");

                    b.HasAnnotation("Relational:JsonPropertyName", "cart_items");
                });

            modelBuilder.Entity("CommerceClone.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("CommerceClone.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Stores");

                    b.HasAnnotation("Relational:JsonPropertyName", "stores");
                });

            modelBuilder.Entity("CommerceClone.Models.Cart", b =>
                {
                    b.HasOne("CommerceClone.Models.Store", "Store")
                        .WithMany("Carts")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("CommerceClone.Models.CartItem", b =>
                {
                    b.HasOne("CommerceClone.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommerceClone.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("CommerceClone.Models.Item", b =>
                {
                    b.HasOne("CommerceClone.Models.Store", "Store")
                        .WithMany("Items")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("CommerceClone.Models.Store", b =>
                {
                    b.HasOne("CommerceClone.Models.Admin", "Admin")
                        .WithMany("Stores")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("CommerceClone.Models.Admin", b =>
                {
                    b.Navigation("Stores");
                });

            modelBuilder.Entity("CommerceClone.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("CommerceClone.Models.Store", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
