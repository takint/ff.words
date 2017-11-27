﻿namespace ff.words.data.Context
{
    using ff.words.data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public class FFWordsContext : DbContext
    {

        public FFWordsContext(DbContextOptions<FFWordsContext> options)
            : base(options)
        {
        }

        DbSet<EntryModel> WordsEntries { get; set; }

        DbSet<CategoryModel> WordsCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}