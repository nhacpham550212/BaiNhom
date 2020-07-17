namespace ModelBb.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDbNhom : DbContext
    {
        public ModelDbNhom()
            : base("name=ModelDbNhom")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Dia> Dias { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<Redential> Redentials { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TheLoaiDia> TheLoaiDias { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.GiaThanh)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.Tongtien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Content>()
                .Property(e => e.GiaThanh)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Content>()
                .Property(e => e.MaTheLoai)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Dia>()
                .Property(e => e.GiaThanh)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Dia>()
                .Property(e => e.MaTheLoai)
                .IsUnicode(false);

            modelBuilder.Entity<Dia>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Dia>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.Dia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dia>()
                .HasMany(e => e.Contents)
                .WithRequired(e => e.Dia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.IDGroup);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Redential>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<Redential>()
                .Property(e => e.UserGroupID)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<TheLoaiDia>()
                .Property(e => e.MaTheLoai)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.IDGroup)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.RePassWord)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.KhachHangs)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
