using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SRMWebApiApp.Models;

namespace SRMWebApiApp.Data;

public partial class Ivp3308V3Context : DbContext
{
    public Ivp3308V3Context()
    {
    }

    public Ivp3308V3Context(DbContextOptions<Ivp3308V3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ReferenceDatum> ReferenceData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.0.13\\sqlexpress,49753;Database=IVP_3308_v3;TrustServerCertificate=True;user id=sa; password=sa@12345678");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReferenceDatum>(entity =>
        {
            entity.HasKey(e => e.RfId).HasName("PK__Referenc__DC46C603525B3778");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
