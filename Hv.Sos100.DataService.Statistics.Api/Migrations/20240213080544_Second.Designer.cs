﻿// <auto-generated />
using System;
using Hv.Sos100.DataService.Statistics.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    [DbContext(typeof(StatisticsContext))]
    [Migration("20240213080544_Second")]
    partial class Second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatatserviceAPI.Models.ActivityStatistics", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<int>("MonthlyViews")
                        .HasColumnType("int");

                    b.Property<int>("SavedActivity")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("DatatserviceAPI.Models.AdStatistics", b =>
                {
                    b.Property<int>("AdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdId"));

                    b.Property<int>("Age16To30Views")
                        .HasColumnType("int");

                    b.Property<int>("Age31To50Views")
                        .HasColumnType("int");

                    b.Property<int>("Age50PlusViews")
                        .HasColumnType("int");

                    b.Property<int>("Clicks")
                        .HasColumnType("int");

                    b.Property<int>("FemaleViews")
                        .HasColumnType("int");

                    b.Property<int>("MaleViews")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalViews")
                        .HasColumnType("int");

                    b.HasKey("AdId");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("DatatserviceAPI.Models.CountyStatistics", b =>
                {
                    b.Property<int>("CountyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountyId"));

                    b.Property<int>("TotalActiveUsers")
                        .HasColumnType("int");

                    b.Property<int>("TotalActivities")
                        .HasColumnType("int");

                    b.Property<int>("TotalAdvertiserAccounts")
                        .HasColumnType("int");

                    b.Property<int>("TotalEnterpriseAccounts")
                        .HasColumnType("int");

                    b.Property<int>("TotalEvents")
                        .HasColumnType("int");

                    b.Property<int>("TotalPeopleAccounts")
                        .HasColumnType("int");

                    b.HasKey("CountyId");

                    b.ToTable("Counties");
                });

            modelBuilder.Entity("DatatserviceAPI.Models.EventStatistics", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<int>("Age16To30Signups")
                        .HasColumnType("int");

                    b.Property<int>("Age31To50Signups")
                        .HasColumnType("int");

                    b.Property<int>("Age50PlusSignups")
                        .HasColumnType("int");

                    b.Property<int>("FemaleSignups")
                        .HasColumnType("int");

                    b.Property<int>("MaleSignups")
                        .HasColumnType("int");

                    b.Property<int>("Rating1")
                        .HasColumnType("int");

                    b.Property<int>("Rating2")
                        .HasColumnType("int");

                    b.Property<int>("Rating3")
                        .HasColumnType("int");

                    b.Property<int>("Rating4")
                        .HasColumnType("int");

                    b.Property<int>("Rating5")
                        .HasColumnType("int");

                    b.Property<int>("SavedEvents")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalSignups")
                        .HasColumnType("int");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
