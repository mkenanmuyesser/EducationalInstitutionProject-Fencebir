USE [master]
GO
/****** Object:  Database [FenCebirProjectDB]    Script Date: 13.07.2022 22:34:05 ******/
CREATE DATABASE [FenCebirProjectDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FenCebirProjectDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\FenCebirProjectDB.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FenCebirProjectDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\FenCebirProjectDB_log.ldf' , SIZE = 270336KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FenCebirProjectDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FenCebirProjectDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FenCebirProjectDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FenCebirProjectDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FenCebirProjectDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FenCebirProjectDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FenCebirProjectDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET RECOVERY FULL 
GO
ALTER DATABASE [FenCebirProjectDB] SET  MULTI_USER 
GO
ALTER DATABASE [FenCebirProjectDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FenCebirProjectDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FenCebirProjectDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FenCebirProjectDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FenCebirProjectDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FenCebirProjectDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FenCebirProjectDB', N'ON'
GO
ALTER DATABASE [FenCebirProjectDB] SET QUERY_STORE = OFF
GO
USE [FenCebirProjectDB]
GO
/****** Object:  Table [dbo].[Ayar]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ayar](
	[AyarId] [int] NOT NULL,
	[IpBloklamaAktifMi] [bit] NOT NULL,
	[IpBlokListesi] [varchar](500) NULL,
	[UygulamaAktifMi] [bit] NOT NULL,
	[IslemKullaniciId] [int] NOT NULL,
	[IslemTarih] [datetime] NOT NULL,
 CONSTRAINT [PK_Ayar] PRIMARY KEY CLUSTERED 
(
	[AyarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[BannerId] [int] IDENTITY(1,1) NOT NULL,
	[BannerTipId] [int] NOT NULL,
	[SubeId] [int] NOT NULL,
	[Adi] [varchar](50) NOT NULL,
	[ResimUrl] [varchar](250) NULL,
	[Resim] [image] NULL,
	[Aciklama1] [varchar](500) NULL,
	[Aciklama2] [varchar](500) NULL,
	[Aciklama3] [varchar](500) NULL,
	[Link] [varchar](100) NULL,
	[LinkAciklama] [varchar](50) NULL,
	[BannerOlusturma] [bit] NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[BannerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BannerTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BannerTip](
	[BannerTipId] [int] NOT NULL,
	[BannerTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_BannerTip] PRIMARY KEY CLUSTERED 
(
	[BannerTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[BlogTipId] [int] NOT NULL,
	[SubeId] [int] NOT NULL,
	[Baslik] [varchar](100) NOT NULL,
	[KisaIcerik] [varchar](100) NOT NULL,
	[Icerik] [varchar](max) NOT NULL,
	[ResimUrl] [varchar](250) NULL,
	[Resim] [image] NULL,
	[Etiketler] [varchar](100) NULL,
	[YayinTarihi] [date] NOT NULL,
	[Anasayfa] [bit] NOT NULL,
	[OkunmaSayisi] [int] NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogTip](
	[BlogTipId] [int] NOT NULL,
	[BlogTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_BlogTip] PRIMARY KEY CLUSTERED 
(
	[BlogTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Duyuru]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Duyuru](
	[DuyuruId] [int] IDENTITY(1,1) NOT NULL,
	[SubeId] [int] NOT NULL,
	[Icerik] [varchar](1000) NOT NULL,
	[Tarih] [date] NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Duyuru] PRIMARY KEY CLUSTERED 
(
	[DuyuruId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etkinlik]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etkinlik](
	[EtkinlikId] [int] IDENTITY(1,1) NOT NULL,
	[EtkinlikTipId] [int] NOT NULL,
	[SubeId] [int] NOT NULL,
	[EtkinlikKonu] [varchar](100) NOT NULL,
	[Tarih] [date] NOT NULL,
	[BaslangicZaman] [time](7) NOT NULL,
	[BitisZaman] [time](7) NOT NULL,
	[Yer] [varchar](100) NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Etkinlik] PRIMARY KEY CLUSTERED 
(
	[EtkinlikId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EtkinlikTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EtkinlikTip](
	[EtkinlikTipId] [int] NOT NULL,
	[EtkinlikTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_EtkinlikTip] PRIMARY KEY CLUSTERED 
(
	[EtkinlikTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Galeri]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Galeri](
	[GaleriId] [int] IDENTITY(1,1) NOT NULL,
	[GaleriTipId] [int] NOT NULL,
	[SubeId] [int] NOT NULL,
	[Aciklama] [varchar](100) NULL,
	[Tarih] [date] NOT NULL,
	[ResimUrl] [varchar](250) NULL,
	[Resim] [image] NULL,
	[Anasayfa] [bit] NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Galeri] PRIMARY KEY CLUSTERED 
(
	[GaleriId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GaleriTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GaleriTip](
	[GaleriTipId] [int] NOT NULL,
	[GaleriTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_GaleriTip] PRIMARY KEY CLUSTERED 
(
	[GaleriTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Icerik]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Icerik](
	[IcerikId] [int] IDENTITY(1,1) NOT NULL,
	[IcerikTipId] [int] NOT NULL,
	[SubeId] [int] NOT NULL,
	[IcerikMetin] [varchar](max) NOT NULL,
	[HtmlIcerik] [bit] NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeKullaniciId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
 CONSTRAINT [PK_Icerik] PRIMARY KEY CLUSTERED 
(
	[IcerikId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IcerikTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IcerikTip](
	[IcerikTipId] [int] NOT NULL,
	[IcerikTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_IcerikTip] PRIMARY KEY CLUSTERED 
(
	[IcerikTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KonuTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KonuTip](
	[KonuTipId] [int] NOT NULL,
	[KonuTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_KonuTip] PRIMARY KEY CLUSTERED 
(
	[KonuTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanici]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanici](
	[KullaniciId] [int] IDENTITY(1,1) NOT NULL,
	[SubeId] [int] NOT NULL,
	[Eposta] [varchar](50) NOT NULL,
	[Sifre] [varchar](50) NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeKullaniciId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[KullaniciId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KurumTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KurumTip](
	[KurumTipId] [int] NOT NULL,
	[KurumTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_KurumTip] PRIMARY KEY CLUSTERED 
(
	[KurumTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesaj]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesaj](
	[MesajId] [int] IDENTITY(1,1) NOT NULL,
	[MesajTipId] [int] NOT NULL,
	[SubeId] [int] NOT NULL,
	[MesajIcerik] [varchar](max) NOT NULL,
	[GonderimTarihi] [datetime] NOT NULL,
	[Dosya] [image] NULL,
	[DosyaAdi] [varchar](100) NULL,
 CONSTRAINT [PK_Mesaj] PRIMARY KEY CLUSTERED 
(
	[MesajId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MesajTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MesajTip](
	[MesajTipId] [int] NOT NULL,
	[MesajTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_MesajTip] PRIMARY KEY CLUSTERED 
(
	[MesajTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OgrenciYorum]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OgrenciYorum](
	[OgrenciYorumId] [int] IDENTITY(1,1) NOT NULL,
	[SubeId] [int] NOT NULL,
	[OgrenciAdSoyad] [varchar](50) NOT NULL,
	[Yorum] [varchar](1000) NOT NULL,
	[ResimUrl] [varchar](250) NULL,
	[Resim] [image] NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_OgrenciYorum] PRIMARY KEY CLUSTERED 
(
	[OgrenciYorumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ogretmen]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ogretmen](
	[OgretmenId] [int] IDENTITY(1,1) NOT NULL,
	[SubeId] [int] NOT NULL,
	[AdSoyad] [varchar](100) NOT NULL,
	[Unvan] [varchar](100) NULL,
	[Aciklama] [varchar](500) NULL,
	[ResimUrl] [varchar](250) NULL,
	[Resim] [image] NULL,
	[FacebookHesapUrl] [varchar](100) NULL,
	[InstagramHesapUrl] [varchar](100) NULL,
	[TwitterHesapUrl] [varchar](100) NULL,
	[WhatsappHesapUrl] [varchar](100) NULL,
	[YoutubeHesapUrl] [varchar](100) NULL,
	[Anasayfa] [bit] NOT NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Ogretmen] PRIMARY KEY CLUSTERED 
(
	[OgretmenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sube]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sube](
	[SubeId] [int] IDENTITY(1,1) NOT NULL,
	[SubeTipId] [int] NOT NULL,
	[SubeSehirId] [int] NOT NULL,
	[SubeAdi] [varchar](100) NOT NULL,
	[SubeAttribute] [varchar](100) NOT NULL,
	[Aciklama] [varchar](100) NULL,
	[Adres] [varchar](1000) NOT NULL,
	[Telefon1] [varchar](50) NOT NULL,
	[Telefon2] [varchar](50) NULL,
	[Fax1] [varchar](50) NULL,
	[Fax2] [varchar](50) NULL,
	[Eposta] [varchar](100) NOT NULL,
	[FacebookHesapUrl] [varchar](100) NULL,
	[InstagramHesapUrl] [varchar](100) NULL,
	[TwitterHesapUrl] [varchar](100) NULL,
	[WhatsappHesapUrl] [varchar](100) NULL,
	[YoutubeHesapUrl] [varchar](100) NULL,
	[GonderilecekEpostaTanim] [varchar](100) NULL,
	[GonderilecekEpostaKullaniciAdi] [varchar](100) NULL,
	[GonderilecekEpostaSifre] [varchar](50) NULL,
	[GonderilecekEpostaHost] [varchar](100) NULL,
	[GonderilecekEpostaPort] [int] NULL,
	[GonderilecekEpostaSsl] [bit] NOT NULL,
	[GonderilecekEpostaAktifMi] [bit] NOT NULL,
	[ResimUrl] [varchar](250) NULL,
	[Resim] [image] NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Sube] PRIMARY KEY CLUSTERED 
(
	[SubeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubeSehir]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubeSehir](
	[SubeSehirId] [int] NOT NULL,
	[SubeSehirAdi] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SubeSehir] PRIMARY KEY CLUSTERED 
(
	[SubeSehirId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubeTip]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubeTip](
	[SubeTipId] [int] NOT NULL,
	[SubeTipAdi] [varchar](50) NOT NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_SubeTip] PRIMARY KEY CLUSTERED 
(
	[SubeTipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Yayin]    Script Date: 13.07.2022 22:34:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Yayin](
	[YayinId] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [varchar](100) NOT NULL,
	[EskiFiyat] [decimal](10, 2) NULL,
	[YeniFiyat] [decimal](10, 2) NULL,
	[ResimUrl] [varchar](250) NULL,
	[Resim] [image] NULL,
	[OzetDosyaUrl] [varchar](250) NULL,
	[OzetDosya] [image] NULL,
	[KayitKullaniciId] [int] NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[GuncellemeId] [int] NULL,
	[GuncellemeTarih] [datetime] NULL,
	[Sira] [int] NOT NULL,
	[AktifMi] [bit] NOT NULL,
 CONSTRAINT [PK_Yayin] PRIMARY KEY CLUSTERED 
(
	[YayinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Icerik] ADD  CONSTRAINT [DF_Icerik_HtmlIcerik]  DEFAULT ((0)) FOR [HtmlIcerik]
GO
ALTER TABLE [dbo].[Banner]  WITH CHECK ADD  CONSTRAINT [FK_Banner_BannerTip] FOREIGN KEY([BannerTipId])
REFERENCES [dbo].[BannerTip] ([BannerTipId])
GO
ALTER TABLE [dbo].[Banner] CHECK CONSTRAINT [FK_Banner_BannerTip]
GO
ALTER TABLE [dbo].[Banner]  WITH CHECK ADD  CONSTRAINT [FK_Banner_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Banner] CHECK CONSTRAINT [FK_Banner_Sube]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_BlogTip] FOREIGN KEY([BlogTipId])
REFERENCES [dbo].[BlogTip] ([BlogTipId])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_BlogTip]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Sube]
GO
ALTER TABLE [dbo].[Duyuru]  WITH CHECK ADD  CONSTRAINT [FK_Duyuru_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Duyuru] CHECK CONSTRAINT [FK_Duyuru_Sube]
GO
ALTER TABLE [dbo].[Etkinlik]  WITH CHECK ADD  CONSTRAINT [FK_Etkinlik_EtkinlikTip] FOREIGN KEY([EtkinlikTipId])
REFERENCES [dbo].[EtkinlikTip] ([EtkinlikTipId])
GO
ALTER TABLE [dbo].[Etkinlik] CHECK CONSTRAINT [FK_Etkinlik_EtkinlikTip]
GO
ALTER TABLE [dbo].[Etkinlik]  WITH CHECK ADD  CONSTRAINT [FK_Etkinlik_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Etkinlik] CHECK CONSTRAINT [FK_Etkinlik_Sube]
GO
ALTER TABLE [dbo].[Galeri]  WITH CHECK ADD  CONSTRAINT [FK_Galeri_GaleriTip] FOREIGN KEY([GaleriTipId])
REFERENCES [dbo].[GaleriTip] ([GaleriTipId])
GO
ALTER TABLE [dbo].[Galeri] CHECK CONSTRAINT [FK_Galeri_GaleriTip]
GO
ALTER TABLE [dbo].[Galeri]  WITH CHECK ADD  CONSTRAINT [FK_Galeri_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Galeri] CHECK CONSTRAINT [FK_Galeri_Sube]
GO
ALTER TABLE [dbo].[Icerik]  WITH CHECK ADD  CONSTRAINT [FK_Icerik_IcerikTip] FOREIGN KEY([IcerikTipId])
REFERENCES [dbo].[IcerikTip] ([IcerikTipId])
GO
ALTER TABLE [dbo].[Icerik] CHECK CONSTRAINT [FK_Icerik_IcerikTip]
GO
ALTER TABLE [dbo].[Icerik]  WITH CHECK ADD  CONSTRAINT [FK_Icerik_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Icerik] CHECK CONSTRAINT [FK_Icerik_Sube]
GO
ALTER TABLE [dbo].[Kullanici]  WITH CHECK ADD  CONSTRAINT [FK_Kullanici_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Kullanici] CHECK CONSTRAINT [FK_Kullanici_Sube]
GO
ALTER TABLE [dbo].[Mesaj]  WITH CHECK ADD  CONSTRAINT [FK_Mesaj_MesajTip] FOREIGN KEY([MesajTipId])
REFERENCES [dbo].[MesajTip] ([MesajTipId])
GO
ALTER TABLE [dbo].[Mesaj] CHECK CONSTRAINT [FK_Mesaj_MesajTip]
GO
ALTER TABLE [dbo].[Mesaj]  WITH CHECK ADD  CONSTRAINT [FK_Mesaj_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Mesaj] CHECK CONSTRAINT [FK_Mesaj_Sube]
GO
ALTER TABLE [dbo].[OgrenciYorum]  WITH CHECK ADD  CONSTRAINT [FK_OgrenciYorum_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[OgrenciYorum] CHECK CONSTRAINT [FK_OgrenciYorum_Sube]
GO
ALTER TABLE [dbo].[Ogretmen]  WITH CHECK ADD  CONSTRAINT [FK_Ogretmen_Sube] FOREIGN KEY([SubeId])
REFERENCES [dbo].[Sube] ([SubeId])
GO
ALTER TABLE [dbo].[Ogretmen] CHECK CONSTRAINT [FK_Ogretmen_Sube]
GO
ALTER TABLE [dbo].[Sube]  WITH CHECK ADD  CONSTRAINT [FK_Sube_SubeSehir] FOREIGN KEY([SubeSehirId])
REFERENCES [dbo].[SubeSehir] ([SubeSehirId])
GO
ALTER TABLE [dbo].[Sube] CHECK CONSTRAINT [FK_Sube_SubeSehir]
GO
ALTER TABLE [dbo].[Sube]  WITH CHECK ADD  CONSTRAINT [FK_Sube_SubeTip] FOREIGN KEY([SubeTipId])
REFERENCES [dbo].[SubeTip] ([SubeTipId])
GO
ALTER TABLE [dbo].[Sube] CHECK CONSTRAINT [FK_Sube_SubeTip]
GO
USE [master]
GO
ALTER DATABASE [FenCebirProjectDB] SET  READ_WRITE 
GO
