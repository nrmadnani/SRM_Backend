using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SRMWebApiApp.Models;

namespace SRMWebApiApp.Data;

public partial class IVP_3308_v3Context : DbContext
{
    public IVP_3308_v3Context()
    {
    }

    public IVP_3308_v3Context(DbContextOptions<IVP_3308_v3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BondRisk> BondRisks { get; set; }

    public virtual DbSet<CallSchedule> CallSchedules { get; set; }

    public virtual DbSet<DividendHistory> DividendHistories { get; set; }

    public virtual DbSet<EquityRisk> EquityRisks { get; set; }

    public virtual DbSet<PricingDetail> PricingDetails { get; set; }

    public virtual DbSet<PutSchedule> PutSchedules { get; set; }

    public virtual DbSet<ReferenceDatum> ReferenceData { get; set; }

    public virtual DbSet<RegulatoryDetail> RegulatoryDetails { get; set; }

    public virtual DbSet<SecurityDetailsBond> SecurityDetailsBonds { get; set; }

    public virtual DbSet<SecurityDetailsEquity> SecurityDetailsEquities { get; set; }

    public virtual DbSet<SecurityIdentifier> SecurityIdentifiers { get; set; }

    public virtual DbSet<SecuritySummary> SecuritySummaries { get; set; }

    public virtual DbSet<SecuritySummaryBond> SecuritySummaryBonds { get; set; }

    public virtual DbSet<SecuritySummaryEquity> SecuritySummaryEquities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.0.13\\sqlexpress,49753;Database=IVP_3308_v3;TrustServerCertificate=True;User Id=sa;password=sa@12345678");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BondRisk>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__BondRisk__CA195970B44F9D05");
        });

        modelBuilder.Entity<CallSchedule>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__CallSche__CA1959707AB96237");
        });

        modelBuilder.Entity<DividendHistory>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__Dividend__CA1959706EB74C10");
        });

        modelBuilder.Entity<EquityRisk>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__EquityRi__CA195970673DECF0");
        });

        modelBuilder.Entity<PricingDetail>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__PricingD__CA19597015ECF495");
        });

        modelBuilder.Entity<PutSchedule>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__PutSched__CA1959700698C3CC");
        });

        modelBuilder.Entity<ReferenceDatum>(entity =>
        {
            entity.HasOne(d => d.SIDNavigation).WithMany().HasConstraintName("FK_SID_ReferenceData");
        });

        modelBuilder.Entity<RegulatoryDetail>(entity =>
        {
            entity.HasKey(e => e.PFId).HasName("PK__Regulato__5944FB171D4EB8EE");
        });

        modelBuilder.Entity<SecurityDetailsBond>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__Security__CA1959704061764D");
        });

        modelBuilder.Entity<SecurityDetailsEquity>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__Security__CA1959702C48CD26");
        });

        modelBuilder.Entity<SecurityIdentifier>(entity =>
        {
            entity.HasKey(e => e.SIdentifier).HasName("PK__Security__1E4E6088653FBF10");

            entity.Property(e => e.BloombergGlobalId)
                .HasDefaultValue("NA")
                .IsFixedLength();
            entity.Property(e => e.BloombergUniqueName).HasDefaultValue("NA");
            entity.Property(e => e.CUSIP).IsFixedLength();
            entity.Property(e => e.ISIN).IsFixedLength();
            entity.Property(e => e.SEDOL).IsFixedLength();
        });

        modelBuilder.Entity<SecuritySummary>(entity =>
        {
            entity.HasKey(e => e.SID).HasName("PK__Security__CA19597060D594BF");
        });

        modelBuilder.Entity<SecuritySummaryBond>(entity =>
        {
            entity.HasOne(d => d.Sec).WithMany().HasConstraintName("FK_SecId_SecuritySummaryBonds");
        });

        modelBuilder.Entity<SecuritySummaryEquity>(entity =>
        {
            entity.HasOne(d => d.Sec).WithMany().HasConstraintName("FK_SecId_SecuritySummaryEquity");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
