﻿// <auto-generated />
using System;
using Hv.Sos100.DataService.Statistics.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    [DbContext(typeof(StatisticsContext))]
    partial class EventStatisticsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hv.Sos100.DataService.Statistics.Api.Models.ActivityStatistics", b =>
                {
                    b.Property<int>("ActivityStatisticsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityStatisticsID"));

                    b.Property<int>("ActivityID")
                        .HasColumnType("int");

                    b.Property<int?>("Age16To30Saved")
                        .HasColumnType("int");

                    b.Property<int?>("Age31To50Saved")
                        .HasColumnType("int");

                    b.Property<int?>("AgeAbove50Saved")
                        .HasColumnType("int");

                    b.Property<int?>("AgeBelow16Saved")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int?>("FemaleSaved")
                        .HasColumnType("int");

                    b.Property<int?>("MaleSaved")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotalSaved")
                        .HasColumnType("int");

                    b.HasKey("ActivityStatisticsID");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Hv.Sos100.DataService.Statistics.Api.Models.AdStatistics", b =>
                {
                    b.Property<int>("AdvertisementStatisticsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdvertisementStatisticsID"));

                    b.Property<int>("AdvertisementID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotalViews")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AdvertisementStatisticsID");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("Hv.Sos100.DataService.Statistics.Api.Models.EventStatistics", b =>
                {
                    b.Property<int>("EventStatisticsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventStatisticsID"));

                    b.Property<int?>("Age16To30Signups")
                        .HasColumnType("int");

                    b.Property<int?>("Age31To50Signups")
                        .HasColumnType("int");

                    b.Property<int?>("AgeAbove50Signups")
                        .HasColumnType("int");

                    b.Property<int?>("AgeBelow16Signups")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int?>("FemaleSignups")
                        .HasColumnType("int");

                    b.Property<int?>("MaleSignups")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotalSignups")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("EventStatisticsID");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
