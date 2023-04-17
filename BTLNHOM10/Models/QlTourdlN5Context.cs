using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLNHOM10.Models;

public partial class QlTourdlN5Context : DbContext
{
    public QlTourdlN5Context()
    {
    }

    public QlTourdlN5Context(DbContextOptions<QlTourdlN5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhTin> AnhTins { get; set; }

    public virtual DbSet<AnhTour> AnhTours { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<DiadiemTour> DiadiemTours { get; set; }

    public virtual DbSet<DiemThamQuan> DiemThamQuans { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhachSan> KhachSans { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TinTuc> TinTucs { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-J7EGSMK;Initial Catalog=QL_TOURDL_N5;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhTin>(entity =>
        {
            entity.HasKey(e => e.TenFileAnh).HasName("PK__AnhTin__8E7F3621B9030A21");

            entity.ToTable("AnhTin");

            entity.Property(e => e.TenFileAnh).HasMaxLength(50);
            entity.Property(e => e.MaTin).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(50);

            entity.HasOne(d => d.MaTinNavigation).WithMany(p => p.AnhTins)
                .HasForeignKey(d => d.MaTin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnhTin__MaTin__6E01572D");
        });

        modelBuilder.Entity<AnhTour>(entity =>
        {
            entity.HasKey(e => e.TenFileAnh).HasName("PK__AnhTour__8E7F36213A767BA3");

            entity.ToTable("AnhTour");

            entity.Property(e => e.TenFileAnh).HasMaxLength(50);
            entity.Property(e => e.MaTour).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(50);

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.AnhTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnhTour__MaTour__3D5E1FD2");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.MaBk).HasName("PK__Booking__272475AEA907F767");

            entity.ToTable("Booking");

            entity.Property(e => e.MaBk)
                .HasMaxLength(50)
                .HasColumnName("MaBK");
            entity.Property(e => e.MaKh)
                .HasMaxLength(50)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MaTour).HasMaxLength(50);

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__MaKH__3E52440B");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__MaNV__3F466844");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__MaTour__403A8C7D");
        });

        modelBuilder.Entity<DiadiemTour>(entity =>
        {
            entity.HasKey(e => new { e.MaDd, e.MaTour }).HasName("PK__DIADIEM___D3C0D3183037BACD");

            entity.ToTable("DIADIEM_TOUR");

            entity.Property(e => e.MaDd)
                .HasMaxLength(50)
                .HasColumnName("MaDD");
            entity.Property(e => e.MaTour).HasMaxLength(50);
            entity.Property(e => e.TenFileAnh).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(50);

            entity.HasOne(d => d.MaDdNavigation).WithMany(p => p.DiadiemTours)
                .HasForeignKey(d => d.MaDd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIADIEM_TO__MaDD__4222D4EF");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.DiadiemTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIADIEM_T__MaTou__412EB0B6");
        });

        modelBuilder.Entity<DiemThamQuan>(entity =>
        {
            entity.HasKey(e => e.MaDd).HasName("PK__DiemTham__2725866586BD410F");

            entity.ToTable("DiemThamQuan");

            entity.Property(e => e.MaDd)
                .HasMaxLength(50)
                .HasColumnName("MaDD");
            entity.Property(e => e.Mien).HasMaxLength(50);
            entity.Property(e => e.MoTa).HasMaxLength(4000);
            entity.Property(e => e.TenDiaDiem).HasMaxLength(100);
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E51498485");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(50)
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhachHang__UserN__4316F928");
        });

        modelBuilder.Entity<KhachSan>(entity =>
        {
            entity.HasKey(e => e.MaKs).HasName("PK__KhachSan__2725CF1333637293");

            entity.ToTable("KhachSan");

            entity.Property(e => e.MaKs)
                .HasMaxLength(50)
                .HasColumnName("MaKS");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.TenKs)
                .HasMaxLength(50)
                .HasColumnName("TenKS");
            entity.Property(e => e.XepHangKs).HasColumnName("XepHang_KS");

            entity.HasMany(d => d.MaTours).WithMany(p => p.MaKs)
                .UsingEntity<Dictionary<string, object>>(
                    "KhachsanTour",
                    r => r.HasOne<Tour>().WithMany()
                        .HasForeignKey("MaTour")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__KHACHSAN___MaTou__440B1D61"),
                    l => l.HasOne<KhachSan>().WithMany()
                        .HasForeignKey("MaKs")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__KHACHSAN_T__MaKS__44FF419A"),
                    j =>
                    {
                        j.HasKey("MaKs", "MaTour").HasName("PK__KHACHSAN__D3C09A6E18A2F967");
                        j.ToTable("KHACHSAN_TOUR");
                        j.IndexerProperty<string>("MaKs")
                            .HasMaxLength(50)
                            .HasColumnName("MaKS");
                        j.IndexerProperty<string>("MaTour").HasMaxLength(50);
                    });
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A3B438AEA");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.SoDienThoai).HasMaxLength(50);
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__UserNa__45F365D3");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__TaiKhoan__C9F284570AD25F01");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasKey(e => e.MaTin).HasName("PK__TinTuc__31490335D0430DB4");

            entity.ToTable("TinTuc");

            entity.Property(e => e.MaTin).HasMaxLength(50);
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MoTa).HasMaxLength(4000);
            entity.Property(e => e.NoiDung)
                .HasMaxLength(4000)
                .HasColumnName("Noi_Dung");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TinTucs)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TinTuc__MaNV__46E78A0C");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.MaTour).HasName("PK__Tour__4E5557DEE22E17BC");

            entity.ToTable("Tour");

            entity.Property(e => e.MaTour).HasMaxLength(50);
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.DiemXuatPhat).HasMaxLength(50);
            entity.Property(e => e.SoNgayDl).HasColumnName("SoNgayDL");
            entity.Property(e => e.TenTour).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
