﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShelterAPI.Models;

namespace ShelterAPI.Migrations
{
    [DbContext(typeof(AnimalShelterContext))]
    partial class AnimalShelterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShelterAPI.Models.Cat", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Coloring");

                    b.Property<string>("Description");

                    b.Property<bool>("IsFemale");

                    b.Property<string>("Name");

                    b.Property<double>("WeightKilo");

                    b.HasKey("CatId");

                    b.ToTable("Cats");
                });

            modelBuilder.Entity("ShelterAPI.Models.Dog", b =>
                {
                    b.Property<int>("DogId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Breed");

                    b.Property<string>("Coloring");

                    b.Property<string>("Description");

                    b.Property<bool>("IsFemale");

                    b.Property<string>("Name");

                    b.Property<double>("WeightKilo");

                    b.HasKey("DogId");

                    b.ToTable("Dogs");
                });
#pragma warning restore 612, 618
        }
    }
}
