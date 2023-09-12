﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoncotesLibrary.Migrations
{
    [DbContext(typeof(LoncotesLibraryDbContext))]
    [Migration("20230912175243_UpdateWithCheckout")]
    partial class UpdateWithCheckout
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LoncotesLibrary.Models.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("PatronId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("PatronId");

                    b.ToTable("Checkouts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckoutDate = new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 1,
                            PatronId = 1
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "History"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Nonfiction"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Music"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaterialTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OutOfCirculationSince")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreId = 3,
                            MaterialName = "Romeo and Juliet",
                            MaterialTypeId = 1,
                            OutOfCirculationSince = new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 5,
                            MaterialName = "Shakespeare Biography",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 3,
                            GenreId = 6,
                            MaterialName = "Best of the 90s",
                            MaterialTypeId = 3
                        },
                        new
                        {
                            Id = 4,
                            GenreId = 3,
                            MaterialName = "The Notebook",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 5,
                            GenreId = 5,
                            MaterialName = "How to Win Friends and Influence People",
                            MaterialTypeId = 3
                        },
                        new
                        {
                            Id = 6,
                            GenreId = 4,
                            MaterialName = "It",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 7,
                            GenreId = 1,
                            MaterialName = "The Eyes of the Dragon",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 8,
                            GenreId = 1,
                            MaterialName = "The Wheel of Time",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 9,
                            GenreId = 5,
                            MaterialName = "Time Magazine",
                            MaterialTypeId = 2
                        },
                        new
                        {
                            Id = 10,
                            GenreId = 2,
                            MaterialName = "World History",
                            MaterialTypeId = 1
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CheckoutDays")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MaterialTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckoutDays = 14,
                            Name = "Book"
                        },
                        new
                        {
                            Id = 2,
                            CheckoutDays = 7,
                            Name = "Periodical"
                        },
                        new
                        {
                            Id = 3,
                            CheckoutDays = 10,
                            Name = "CD"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Patron", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patrons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Fake Street",
                            Email = "Suzy@gmail.comx",
                            FirstName = "Suzy",
                            IsActive = true,
                            LastName = "Jones"
                        },
                        new
                        {
                            Id = 2,
                            Address = "456 Main Street",
                            Email = "Bob@gmail.comx",
                            FirstName = "Bob",
                            IsActive = true,
                            LastName = "Jones"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Checkout", b =>
                {
                    b.HasOne("LoncotesLibrary.Models.Material", "Material")
                        .WithMany("Checkouts")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoncotesLibrary.Models.Patron", "Patron")
                        .WithMany()
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Patron");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.HasOne("LoncotesLibrary.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoncotesLibrary.Models.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MaterialTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.Navigation("Checkouts");
                });
#pragma warning restore 612, 618
        }
    }
}
