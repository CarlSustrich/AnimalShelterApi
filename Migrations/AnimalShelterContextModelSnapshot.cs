﻿// <auto-generated />
using System;
using AnimalShelter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimalShelterApi.Migrations
{
    [DbContext(typeof(AnimalShelterContext))]
    partial class AnimalShelterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnimalShelter.Models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AnimalName")
                        .HasColumnType("longtext");

                    b.Property<string>("Breed")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateAcquired")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ShelterId")
                        .HasColumnType("int");

                    b.Property<string>("Species")
                        .HasColumnType("longtext");

                    b.HasKey("AnimalId");

                    b.HasIndex("ShelterId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("AnimalShelter.Models.Shelter", b =>
                {
                    b.Property<int>("ShelterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<string>("ShelterName")
                        .HasColumnType("longtext");

                    b.HasKey("ShelterId");

                    b.ToTable("Shelters");
                });

            modelBuilder.Entity("AnimalShelter.Models.Animal", b =>
                {
                    b.HasOne("AnimalShelter.Models.Shelter", "Shelter")
                        .WithMany()
                        .HasForeignKey("ShelterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shelter");
                });
#pragma warning restore 612, 618
        }
    }
}
