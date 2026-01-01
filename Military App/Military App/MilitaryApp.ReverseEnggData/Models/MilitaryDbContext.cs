using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MilitaryApp.ReverseEnggData.Models;

public partial class MilitaryDbContext : DbContext
{
    public MilitaryDbContext()
    {
    }

    public MilitaryDbContext(DbContextOptions<MilitaryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Battle> Battles { get; set; }

    public virtual DbSet<Horse> Horses { get; set; }

    public virtual DbSet<King> Kings { get; set; }

    public virtual DbSet<Military> Militaries { get; set; }

    public virtual DbSet<Quote> Quotes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local)\\SQLEXPRESS01;Initial Catalog=MilitaryDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Horse>(entity =>
        {
            entity.HasIndex(e => e.MilitaryId, "IX_Horses_MilitaryId").IsUnique();

            entity.HasOne(d => d.Military).WithOne(p => p.Horse).HasForeignKey<Horse>(d => d.MilitaryId);
        });

        modelBuilder.Entity<Military>(entity =>
        {
            entity.HasIndex(e => e.KingId, "IX_Militaries_KingId");

            entity.HasOne(d => d.King).WithMany(p => p.Militaries).HasForeignKey(d => d.KingId);

            entity.HasMany(d => d.Battles).WithMany(p => p.Militaries)
                .UsingEntity<Dictionary<string, object>>(
                    "MilitaryBattle",
                    r => r.HasOne<Battle>().WithMany().HasForeignKey("BattleId"),
                    l => l.HasOne<Military>().WithMany().HasForeignKey("MilitaryId"),
                    j =>
                    {
                        j.HasKey("MilitaryId", "BattleId");
                        j.ToTable("MilitaryBattle");
                        j.HasIndex(new[] { "BattleId" }, "IX_MilitaryBattle_BattleId");
                    });
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasIndex(e => e.MilitaryId, "IX_Quotes_MilitaryId");

            entity.HasOne(d => d.Military).WithMany(p => p.Quotes).HasForeignKey(d => d.MilitaryId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
