﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tupla.Data.Context;

namespace Tupla.Data.Context.Migrations
{
    [DbContext(typeof(TuplaContext))]
    [Migration("20200506093034_platformandtag")]
    partial class platformandtag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tupla.Data.Core.CompanyData.Company", b =>
                {
                    b.Property<int>("companyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bank_account")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("bank_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("company_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("company_country")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime>("company_create_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("company_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("company_zipcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("companyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Tupla.Data.Core.CustomerData.Customer", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("Date_of_birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("First_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Zipcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Tupla.Data.Core.GameData.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("HtmlText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GameId");

                    b.HasIndex("CompanyID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Tupla.Data.Core.PictureData.CompanyPicture", b =>
                {
                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("imageType")
                        .HasColumnType("int");

                    b.HasKey("Path");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyPicture");
                });

            modelBuilder.Entity("Tupla.Data.Core.PictureData.CustomerPicture", b =>
                {
                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("imageType")
                        .HasColumnType("int");

                    b.HasKey("Path");

                    b.HasIndex("Username");

                    b.ToTable("CustomerPicture");
                });

            modelBuilder.Entity("Tupla.Data.Core.PictureData.GamePicture", b =>
                {
                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("imageType")
                        .HasColumnType("int");

                    b.HasKey("Path");

                    b.HasIndex("GameId");

                    b.ToTable("GamePicture");
                });

            modelBuilder.Entity("Tupla.Data.Core.PlatformData.Platform", b =>
                {
                    b.Property<int>("PlatformId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Platform_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlatformId");

                    b.ToTable("Platform");
                });

            modelBuilder.Entity("Tupla.Data.Core.PlatformData.PlatformOfGame", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<string>("Authentication")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("PlatformOfGame");
                });

            modelBuilder.Entity("Tupla.Data.Core.Tag.GameTag", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("GameId", "TagId", "Username");

                    b.HasIndex("TagId");

                    b.HasIndex("Username");

                    b.ToTable("GameTag");
                });

            modelBuilder.Entity("Tupla.Data.Core.Tag.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("TagId");

                    b.HasIndex("Username");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Tupla.Data.Core.GameData.Game", b =>
                {
                    b.HasOne("Tupla.Data.Core.CompanyData.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tupla.Data.Core.PictureData.CompanyPicture", b =>
                {
                    b.HasOne("Tupla.Data.Core.CompanyData.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tupla.Data.Core.PictureData.CustomerPicture", b =>
                {
                    b.HasOne("Tupla.Data.Core.CustomerData.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tupla.Data.Core.PictureData.GamePicture", b =>
                {
                    b.HasOne("Tupla.Data.Core.GameData.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tupla.Data.Core.PlatformData.PlatformOfGame", b =>
                {
                    b.HasOne("Tupla.Data.Core.GameData.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tupla.Data.Core.PlatformData.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tupla.Data.Core.Tag.GameTag", b =>
                {
                    b.HasOne("Tupla.Data.Core.GameData.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tupla.Data.Core.Tag.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tupla.Data.Core.CustomerData.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tupla.Data.Core.Tag.Tag", b =>
                {
                    b.HasOne("Tupla.Data.Core.CustomerData.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
