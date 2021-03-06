/****** Object:  Database [CivionicsContext]    Script Date: 11/21/2014 2:07:50 PM ******/
CREATE DATABASE [CivionicsContext]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CivionicsContext.mdf', FILENAME = N'C:\Users\Lewis\Documents\3900-Civionics-Project\Civionics\App_Data\CivionicsContext.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CivionicsContext_log.ldf', FILENAME = N'C:\Users\Lewis\Documents\3900-Civionics-Project\Civionics\App_Data\CivionicsContext_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CivionicsContext] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CivionicsContext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CivionicsContext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CivionicsContext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CivionicsContext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CivionicsContext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CivionicsContext] SET ARITHABORT OFF 
GO
ALTER DATABASE [CivionicsContext] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CivionicsContext] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CivionicsContext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CivionicsContext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CivionicsContext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CivionicsContext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CivionicsContext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CivionicsContext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CivionicsContext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CivionicsContext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CivionicsContext] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CivionicsContext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CivionicsContext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CivionicsContext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CivionicsContext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CivionicsContext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CivionicsContext] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CivionicsContext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CivionicsContext] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CivionicsContext] SET  MULTI_USER 
GO
ALTER DATABASE [CivionicsContext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CivionicsContext] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CivionicsContext] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CivionicsContext] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/21/2014 2:07:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 11/21/2014 2:07:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectAccess]    Script Date: 11/21/2014 2:07:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAccess](
	[ProjectAccessID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ProjectAccess] PRIMARY KEY CLUSTERED 
(
	[ProjectAccessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reading]    Script Date: 11/21/2014 2:07:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reading](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SensorID] [int] NOT NULL,
	[isAnomalous] [bit] NOT NULL,
	[LoggedTime] [datetime] NOT NULL,
	[Data] [real] NOT NULL,
 CONSTRAINT [PK_dbo.Reading] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sensor]    Script Date: 11/21/2014 2:07:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[SiteID] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[MinSafeReading] [real] NOT NULL,
	[MaxSafeReading] [real] NOT NULL,
	[AutoRange] [bit] NOT NULL,
	[AutoPercent] [int] NOT NULL,
	[serial] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Sensor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SensorType]    Script Date: 11/21/2014 2:07:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SensorType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Units] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.SensorType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_SensorID]    Script Date: 11/21/2014 2:07:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_SensorID] ON [dbo].[Reading]
(
	[SensorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProjectID]    Script Date: 11/21/2014 2:07:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProjectID] ON [dbo].[Sensor]
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TypeID]    Script Date: 11/21/2014 2:07:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_TypeID] ON [dbo].[Sensor]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Sensor] ADD  DEFAULT ((0)) FOR [AutoRange]
GO
ALTER TABLE [dbo].[Sensor] ADD  DEFAULT ((0)) FOR [AutoPercent]
GO
ALTER TABLE [dbo].[Reading]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Reading_dbo.Sensor_SensorID] FOREIGN KEY([SensorID])
REFERENCES [dbo].[Sensor] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reading] CHECK CONSTRAINT [FK_dbo.Reading_dbo.Sensor_SensorID]
GO
ALTER TABLE [dbo].[Sensor]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sensor_dbo.Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sensor] CHECK CONSTRAINT [FK_dbo.Sensor_dbo.Project_ProjectID]
GO
ALTER TABLE [dbo].[Sensor]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sensor_dbo.SensorType_TypeID] FOREIGN KEY([TypeID])
REFERENCES [dbo].[SensorType] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sensor] CHECK CONSTRAINT [FK_dbo.Sensor_dbo.SensorType_TypeID]
GO
ALTER DATABASE [CivionicsContext] SET  READ_WRITE 
GO
