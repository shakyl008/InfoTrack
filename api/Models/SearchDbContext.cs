using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api.Models;

public partial class SearchDbContext : DbContext
{
    public SearchDbContext()
    {
    }

    public SearchDbContext(DbContextOptions<SearchDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Search> Searches { get; set; }

    private static string GetConnectionString()
    {
        string connectionString = null;

        // try to get local secrets file
        var configFile = "./secrets.config";
        if (File.Exists(configFile))
        {
            var configFileJson = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(configFile));
            connectionString = configFileJson["ConnectionString"];
        }
        else
        {
            // get connectionString from environment variables - this would be added when deploying the containers
            connectionString = Environment.GetEnvironmentVariable("ApiConnectionString");
        }

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Can't find connection string.");
        }

        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Search>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Search__3213E83F36159D58");

            entity.ToTable("Search");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Positions)
                .HasMaxLength(355)
                .HasColumnName("positions");
            entity.Property(e => e.SearchDate)
                .HasColumnType("datetime")
                .HasColumnName("searchDate");
            entity.Property(e => e.SearchQuery)
                .HasMaxLength(255)
                .HasColumnName("searchQuery");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
