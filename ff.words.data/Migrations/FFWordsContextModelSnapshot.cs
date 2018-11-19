﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ff.words.data.Context;

namespace ff.words.data.Migrations
{
    [DbContext(typeof(FFWordsContext))]
    partial class FFWordsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ff.words.data.Models.CategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(100);

                    b.Property<string>("Description");

                    b.Property<bool>("Inactivated");

                    b.Property<string>("Name")
                        .HasMaxLength(512);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ff.words.data.Models.EntryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorName")
                        .HasMaxLength(100);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(100);

                    b.Property<int>("CurrentStatus");

                    b.Property<string>("Excerpt");

                    b.Property<string>("FeaturedImage");

                    b.Property<bool>("Inactivated");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ThumbnailImage");

                    b.Property<string>("Title")
                        .HasMaxLength(512);

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("ff.words.data.Models.PageSettingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(100);

                    b.Property<bool>("Inactivated");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SettingKey")
                        .HasMaxLength(512);

                    b.Property<string>("SettingName")
                        .HasMaxLength(512);

                    b.Property<string>("SettingValue");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("PageSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
