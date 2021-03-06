/****** Object:  Database [aspnet-Civionics-20141031081608]    Script Date: 11/27/2014 12:58:26 PM ******/
CREATE DATABASE [aspnet-Civionics-20141031081608]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'aspnet-Civionics-20141031081608.mdf', FILENAME = N'C:\Users\Lewis\Documents\3900-Civionics-Project\Civionics\App_Data\aspnet-Civionics-20141031081608.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'aspnet-Civionics-20141031081608_log.ldf', FILENAME = N'C:\Users\Lewis\Documents\3900-Civionics-Project\Civionics\App_Data\aspnet-Civionics-20141031081608_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [aspnet-Civionics-20141031081608].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET ARITHABORT OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET  ENABLE_BROKER 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET  MULTI_USER 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET DB_CHAINING OFF 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/27/2014 12:58:27 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/27/2014 12:58:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/27/2014 12:58:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[User_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/27/2014 12:58:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[UserId] [nvarchar](128) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/27/2014 12:58:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/27/2014 12:58:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201411012019055_InitialCreate', N'Civionics.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5C596FDC36107E2FD0FF20E829299C958F26488DDD04CE3A6E8DC607B24ED0B7802B71D744254A1529778DA2BFAC0FFD49FD0B25758B878E95F6B011C0B0A5E1CC70F8CDF0FA94FFFEF977FC7EE5B9C6030C09F2F1C43C1A1D9A06C4B6EF20BC9C98115DBC7A6BBE7FF7FD77E38F8EB732BE6672275C8EB5C46462DE531A9C5A16B1EFA107C8C84376E8137F4147B6EF59C0F1ADE3C3C39FACA3230B321526D36518E3CF11A6C883F11FECCFA98F6D18D008B857BE035D923E676F66B156E31A789004C08613738A1E980BC826A344D634CE5C04981F33E82E4C0360EC534099C8E917026734F4F17216B007C0BD7B0C20935B0097C0D4FBD342BC6D470E8F7947ACA261A6CA8E08F5BD8E0A8F4ED2C85862F3B5E26BE69163B1FBC8624C1F79AFE3F84DCC4B07C68F3EFB2E0B8068F074EA865C78625EE526CE48700DE9286B384A545E844CDD9F7EF8FBA8ACF1C068DDEE204712031CFF77604C239746219C6018D110B807C66D347791FD2B7CBCF37F87787272345F9CBC7DFD0638276F7E8427AFCB3D657D65729507ECD16DE8073064BEC145DE7FD3B0AAED2CB161DEACD426890AC3124B0AD3B802AB4F102FE93D4B97E3B7A6718156D0C99EA4E0FAC200CAD039316918B13FAF23D7057317E6EFAD5A9BFC678D55F66B7FAB63AB80472D68CE82808D438C109650611D6EC4D41C096DD92867126B2000B3EE98C60740601A1996EF39C212D7AC56F0E7B25317206FD81CC8D53EF744B8C4F4E45801AF521867D40FE1CF10C31050E8DC024A61888B11684A80388CDCD80059D0C2D257E0461B30750D1ED0328E87603401EB67E8C62FC93D0A14688E3DFB96885E84BEC74BAC02C3B1C4B7991F8536EF825F2B7607C225A46B16826AAA0D9D39CF3D69B6377BF0680E3483D41BBA0584B0F1707E01E47EE3C666D08E42868219055EB0C55C8DB3870C9BAD591A36646B96D46D5DFDE42F116E763516AB75B590A875B524D6D555AEACD9532E55EB682E50EB6721A57273ADFA17777DF82218ABDDFF4AC85D95ABA15234EE117BFC801C7181A6699109334FBAD7DBCCB36DD75CA19BDB365F89D9C68DF75BD7742A3F75EB1A4595EA9DD7C3EF8C33ADCF2AAB7987D6590EED2A3D337F77971A09B0DACC778964CD7C17FFA84B8C424A9517BD13B8C3ACDCE865E7EC3D23C4B751EC9E6E5A4E7CABF6F823768C7685467DB490CEF7572C0D113FD2608F27E60F52601BCDE4CB14DD0946D5C29129E6E20D3E872EA4D038B39333C6292036706424B2F839D5272C7D61C86D0177CA0697D51184A99CEB08DB28006EAB7E08AD5B6E9BB877B91DF1CD390C20E6465B8D571B07B475CDCA6D09A16B8AD4D82AC1B03D3A8BF46E831A45AEAB4193148CF5A029970A85119581FD42A6D48D2D03531AAB36F6D593E8AE70D9BA682AAAFB8670F9F42BA6D48D5DE0F2C9D6CBD2994A1BCCA88E43D5A0490FFFD783A6EA1C67DBD8D406345934B141A06C0860285FE19CCFF94BB8A28A5D0EF339DDE89074BD2BC6832B9F415A3DC429166ACA79430A6A558914DA3A8DA5C1EBA0B649632765D9215BADC674B9D8416D532C8B822A282D01A1C6E174BD5E12AEDF998B406DBD7ECEFBA88E9A54535AAF98358A259D95D460E21DA355DA82D5074BB398EBB09CD3F4284542CB48C90BB8925AA5AA2102D4064D9A55458775C57001DA0592CA3702F591D2CD735D663A4DA7B28ADA32588AB96D8068657BF97C5A2AE83D56C2EFC9784096860834BE024180F0B2440C4A9F18B39415F46AD69D30E3253A2C9B287833B9B7B925EA87600985B7CC34F3F40285849E030AE6809F0A4D1D4F12534EC29A092233599E67E511CCA68A4C9AFF9E5E5A89448CCA942C2F02530D17AC7F1E5F49C6D7F78A91573737384D0BB820545C7E4E7D37F2B07E61AB6F9D5C6196DB274F640D634BF05F5AB84AB1925657D5C0B71A966A3AF41A1E9163B3FE08A99BF71E21865707C5CBF4982BA1600595D59C236287C84318302F3731620DF1BD24FCF79BC50B6D1E709F5FEE5F9895ADB92D39198AA7ED35552FECCBDAAA6FDA6B146EE5CB2A85575B0741EDD8EFDFD04B1956DDA16C35BD062888E9C263B0014B767DFD464DA36333595BA2B09595941E77D49592D42465E9F3BD4441BAED1B0C05C9BEBA1F0A343AEAAAAF8804DD09995E8BC00F282B6B6048E875562EFD2BC55CCFA0D8135C0CB8A2CD0F46FAA1A2DBCA761850641700652DBA4B81CD0F5B75FBD650D9D37D6D97E29D36E95CA0F95655AACC755B563972ADCA7BAC427DEE5DF2606DE7B407F06BCF3D7ABFC4BDB83CDED2965C14C9D1966FCD852DF838DD0EB7F86047DC1F2722A691D529B6567C24147A232E309AFDE14E5DC46258085C018C1690D084EB621E1F1E1D0B5FFDECCF173816218EDBE1339CEAC06D81888C1F4068DF8350E6BFF4F84A2553FAC203AB97437D79A25F713FA938153BB58658ADCBB9EEA550C9ABEEA551D89EF48AE65A58514C23DB000CE225ABF1C3978EE091BE84E93534F2D72EBDD4E553A638C6D2D5E22576E06A62FE15373C352E7FFB96B63D306E4256E14F8D43E3EF6D6043B1FA7F5E9CE7CE83D06B0C646F944CE8C16AA982E8BCCDCAB2EE8CDD013CC3506B778D822AE1B6933749D30DD48541A9A3BB2124D5DC96F7A5AEF6E63AED840CFAB4084DED09A0090361175C4CFD2D7A4FA25F2F80694E78364E9E7BCE44CE7D2A61BBC6D72E0A586B7CED4DFDEA48C8DC2780A5ACC17E84D0270731DDAD991A639AD3CFC141261C3D667B538946A3B95650FE6736C5924F396126478E13D399FB0C04C9D231F90A4FCD91D3194BD0AB35961E716B8DA9995375C65264D65A4C65EACDAA9960CD54D7764CD77ADB6AF667231FB6151DB6DEB27A7477C999D532EABA9163EB99B64F81145B1E619964D685DE59CB147D2AF4D7FEB8D87628364870ED1F8C4ADDD45C346E84C12A5F8BB199B2F4BFDDB1F99AA065A182137F30B42B73642E7389177E36570B1E6522C2C1CB15A4C06113E85948D102D894BDB62121F1F7CE29B1E8A33787CE25BE89681051D665E8CDDDCAB1239FF2EBECC734DDAACFE39B20FE6278882E303711EB02BCC11F22E43AB9DF178AA31E8D0ABE96480FE1F958527E18BF7CCC355DFBB8A5A2347CF912E80E7A81CB94911B3C030F701DDF18FE3EC125B01F8B43529D92E681A8867D7C8EC032041E497514EDD99F0CC38EB77AF73F36C95D64F4510000, N'6.1.1-30610')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'58bc62ad-632b-4bbb-bde5-bf3f5cba6c80', N'admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8bc24a3c-3087-4e2e-8783-bfcd1ee510f7', N'user')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c550d44a-20d0-4408-8678-3638395c8a25', N'58bc62ad-632b-4bbb-bde5-bf3f5cba6c80')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [PasswordHash], [SecurityStamp], [Discriminator]) VALUES (N'c550d44a-20d0-4408-8678-3638395c8a25', N'admin', N'AKq09TnOHVrRkkm6dmMxH8L1v+An/T14/p3NpSZRdjYBDc9ZAc3QndCAztymPtyrtg==', N'e4da073a-18dd-48b6-a620-8f14d568bb84', N'ApplicationUser')
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_User_Id]    Script Date: 11/27/2014 12:58:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_User_Id] ON [dbo].[AspNetUserClaims]
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 11/27/2014 12:58:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 11/27/2014 12:58:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 11/27/2014 12:58:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER DATABASE [aspnet-Civionics-20141031081608] SET  READ_WRITE 
GO
