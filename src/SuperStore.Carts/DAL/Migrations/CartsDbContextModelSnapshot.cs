﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuperStore.Carts.DAL;

#nullable disable

namespace SuperStore.Carts.DAL.Migrations
{
    [DbContext(typeof(CartsDbContext))]
    partial class CartsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SuperStore.Carts")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SuperStore.Carts.DAL.Model.CustomerFundsModel", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("CustomerId"));

                    b.Property<decimal>("CurrentFunds")
                        .HasColumnType("numeric");

                    b.HasKey("CustomerId");

                    b.ToTable("CustomerFunds", "SuperStore.Carts");
                });

            modelBuilder.Entity("SuperStore.Shared.Deduplication.DeduplicationModel", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("text");

                    b.Property<DateTime>("ProcessedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("MessageId");

                    b.ToTable("Deduplications", "SuperStore.Carts");
                });
#pragma warning restore 612, 618
        }
    }
}
