using System;
using Microsoft.EntityFrameworkCore;
using FencebirSubeProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FencebirSubeProject.Data
{
    public partial class ProjectDBContext : DbContext
    {
        public ProjectDBContext()
        {

        }

        public ProjectDBContext(DbContextOptions<ProjectDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ayar> Ayar { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<BannerTip> BannerTip { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<BlogTip> BlogTip { get; set; }
        public virtual DbSet<Duyuru> Duyuru { get; set; }
        public virtual DbSet<Etkinlik> Etkinlik { get; set; }
        public virtual DbSet<EtkinlikTip> EtkinlikTip { get; set; }
        public virtual DbSet<Galeri> Galeri { get; set; }
        public virtual DbSet<GaleriTip> GaleriTip { get; set; }
        public virtual DbSet<Icerik> Icerik { get; set; }
        public virtual DbSet<IcerikTip> IcerikTip { get; set; }
        public virtual DbSet<KonuTip> KonuTip { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<KurumTip> KurumTip { get; set; }
        public virtual DbSet<Mesaj> Mesaj { get; set; }
        public virtual DbSet<MesajTip> MesajTip { get; set; }
        public virtual DbSet<OgrenciYorum> OgrenciYorum { get; set; }
        public virtual DbSet<Ogretmen> Ogretmen { get; set; }
        public virtual DbSet<Sube> Sube { get; set; }
        public virtual DbSet<SubeSehir> SubeSehir { get; set; }
        public virtual DbSet<SubeTip> SubeTip { get; set; }
        public virtual DbSet<Yayin> Yayin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ayar>(entity =>
            {
                entity.Property(e => e.IpBlokListesi)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AyarId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.Aciklama1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Aciklama2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Aciklama3)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Adi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Resim).HasColumnType("image");

                entity.Property(e => e.ResimUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.BannerTip)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.BannerTipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Banner_BannerTip");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Banner_Sube");
            });

            modelBuilder.Entity<BannerTip>(entity =>
            {
                entity.Property(e => e.BannerTipId).ValueGeneratedNever();

                entity.Property(e => e.BannerTipAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.Baslik)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Etiketler)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.Icerik)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.KisaIcerik)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Resim).HasColumnType("image");

                entity.Property(e => e.ResimUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.YayinTarihi).HasColumnType("date");

                entity.HasOne(d => d.BlogTip)
                    .WithMany(p => p.Blog)
                    .HasForeignKey(d => d.BlogTipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blog_BlogTip");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Blog)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blog_Sube");
            });

            modelBuilder.Entity<BlogTip>(entity =>
            {
                entity.Property(e => e.BlogTipId).ValueGeneratedNever();

                entity.Property(e => e.BlogTipAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Duyuru>(entity =>
            {
                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.Icerik)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.Tarih).HasColumnType("date");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Duyuru)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Duyuru_Sube");
            });

            modelBuilder.Entity<Etkinlik>(entity =>
            {
                entity.Property(e => e.EtkinlikKonu)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.Tarih).HasColumnType("date");

                entity.Property(e => e.Yer)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.EtkinlikTip)
                    .WithMany(p => p.Etkinlik)
                    .HasForeignKey(d => d.EtkinlikTipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Etkinlik_EtkinlikTip");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Etkinlik)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Etkinlik_Sube");
            });

            modelBuilder.Entity<EtkinlikTip>(entity =>
            {
                entity.Property(e => e.EtkinlikTipId).ValueGeneratedNever();

                entity.Property(e => e.EtkinlikTipAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Galeri>(entity =>
            {
                entity.Property(e => e.Aciklama)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.Resim).HasColumnType("image");

                entity.Property(e => e.ResimUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Tarih).HasColumnType("date");

                entity.HasOne(d => d.GaleriTip)
                    .WithMany(p => p.Galeri)
                    .HasForeignKey(d => d.GaleriTipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Galeri_GaleriTip");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Galeri)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Galeri_Sube");
            });

            modelBuilder.Entity<GaleriTip>(entity =>
            {
                entity.Property(e => e.GaleriTipId).ValueGeneratedNever();

                entity.Property(e => e.GaleriTipAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Icerik>(entity =>
            {
                entity.Property(e => e.IcerikMetin)
                    .IsRequired()
                    .HasColumnName("IcerikMetin")
                    .IsUnicode(false);

                entity.HasOne(d => d.IcerikTip)
                    .WithMany(p => p.Icerik)
                    .HasForeignKey(d => d.IcerikTipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Icerik_IcerikTip");

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Icerik)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Icerik_Sube");
            });

            modelBuilder.Entity<IcerikTip>(entity =>
            {
                entity.Property(e => e.IcerikTipId).ValueGeneratedNever();

                entity.Property(e => e.IcerikTipAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Kullanici)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kullanici_Sube");
            });

            modelBuilder.Entity<OgrenciYorum>(entity =>
            {
                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.OgrenciAdSoyad)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Yorum)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.OgrenciYorum)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OgrenciYorum_Sube");
            });

            modelBuilder.Entity<Ogretmen>(entity =>
            {
                entity.Property(e => e.Aciklama)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AdSoyad)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FacebookHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.Resim).HasColumnType("image");

                entity.Property(e => e.ResimUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TwitterHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unvan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.YoutubeHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InstagramHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sube)
                    .WithMany(p => p.Ogretmen)
                    .HasForeignKey(d => d.SubeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ogretmen_Sube");
            });

            modelBuilder.Entity<Sube>(entity =>
            {
                entity.Property(e => e.Aciklama)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FacebookHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GonderilecekEpostaHost)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GonderilecekEpostaKullaniciAdi)

                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GonderilecekEpostaSifre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GonderilecekEpostaTanim)
                    .HasMaxLength(100)
                    .IsUnicode(false);


                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Eposta)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fax1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TwitterHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.YoutubeHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InstagramHesapUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");


                entity.Property(e => e.SubeAdi)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubeAttribute)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.SubeSehir)
                    .WithMany(p => p.Sube)
                    .HasForeignKey(d => d.SubeSehirId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sube_SubeSehir");

                entity.HasOne(d => d.SubeTip)
                    .WithMany(p => p.Sube)
                    .HasForeignKey(d => d.SubeTipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sube_SubeTip");
            });

            modelBuilder.Entity<SubeSehir>(entity =>
            {
                entity.Property(e => e.SubeSehirId).ValueGeneratedNever();

                entity.Property(e => e.SubeSehirAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubeTip>(entity =>
            {
                entity.Property(e => e.SubeTipId).ValueGeneratedNever();

                entity.Property(e => e.SubeTipAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Yayin>(entity =>
            {
                entity.Property(e => e.Ad)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EskiFiyat).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.GuncellemeTarih).HasColumnType("datetime");

                entity.Property(e => e.KayitTarih).HasColumnType("datetime");

                entity.Property(e => e.Resim).HasColumnType("image");

                entity.Property(e => e.ResimUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OzetDosya).HasColumnType("image");

                entity.Property(e => e.OzetDosyaUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.YeniFiyat).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
