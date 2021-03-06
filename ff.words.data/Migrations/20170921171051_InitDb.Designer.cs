﻿// <auto-generated />
using ff.words.data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ff.words.data.Migrations
{
    [DbContext(typeof(FFWordsContext))]
    [Migration("20170921171051_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ff.words.data.Models.EntryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(100);

                    b.Property<string>("EntryContent")
                        .HasColumnType("ntext");

                    b.Property<string>("EntryTitle")
                        .HasMaxLength(512);

                    b.Property<bool>("Inactivated");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("WordsEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
