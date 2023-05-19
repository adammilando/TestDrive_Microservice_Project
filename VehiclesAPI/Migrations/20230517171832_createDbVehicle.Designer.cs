﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehiclesAPI.DbContexts;

#nullable disable

namespace VehiclesAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20230517171832_createDbVehicle")]
    partial class createDbVehicle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VehiclesAPI.Models.Vehicles", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaxSpeed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("displacement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("height")
                        .HasColumnType("float");

                    b.Property<double>("length")
                        .HasColumnType("float");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<double>("witdh")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
