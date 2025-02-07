using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class AnaokuluContext : DbContext
    {
        public AnaokuluContext()
        {
        }

        public AnaokuluContext(DbContextOptions<AnaokuluContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<LogKullanici> LogKullanicis { get; set; }
        public virtual DbSet<LogOgrenci> LogOgrencis { get; set; }
        public virtual DbSet<Ogrenci> Ogrencis { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Sinif> Sinifs { get; set; }
        public virtual DbSet<ViewOgrenci> ViewOgrencis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=HATICE-PC\\SQLEXPRESS;database=anaokulu;user id=sa;Password=58");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.ToTable("Kullanici");

                entity.Property(e => e.KullaniciAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Sifre)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Tckimlik)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("TCkimlik")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Kullanicis)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kullanici_Kullanici");
            });

            modelBuilder.Entity<LogKullanici>(entity =>
            {
                entity.ToTable("LogKullanici");

                entity.Property(e => e.DegisiklikTarihi).HasColumnType("datetime");

                entity.Property(e => e.Islem)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.KullaniciAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Sifre)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Tckimlik)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("TCKimlik")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<LogOgrenci>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LogOgrenci");

                entity.Property(e => e.DegisiklikTarihi).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Islem)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.ToTable("Ogrenci");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                entity.Property(e => e.SoyAd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Sinif)
                    .WithMany(p => p.Ogrencis)
                    .HasForeignKey(d => d.SinifId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ogrenci_Sinif");

                entity.HasOne(d => d.Veli)
                    .WithMany(p => p.Ogrencis)
                    .HasForeignKey(d => d.VeliId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ogrenci_Kullanici");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Sinif>(entity =>
            {
                entity.ToTable("Sinif");

                entity.Property(e => e.Adi)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ViewOgrenci>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewOgrenci");

                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                entity.Property(e => e.Sinifi)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SoyAd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Veli)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
