EXEC sys.sp_db_vardecimal_storage_format N'AutogearDB', N'ON'
GO
USE [AutogearDB]
GO
/****** Object:  DatabaseRole [user]    Script Date: 05-12-2015 14:36:49 ******/
CREATE ROLE [user]
GO
/****** Object:  DatabaseRole [instructor]    Script Date: 05-12-2015 14:36:49 ******/
CREATE ROLE [instructor]
GO
/****** Object:  DatabaseRole [admin]    Script Date: 05-12-2015 14:36:49 ******/
CREATE ROLE [admin]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 05-12-2015 14:36:49 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 05-12-2015 14:36:49 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Hometown] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Jobtitle] [nvarchar](100) NULL,
	[Educationlevel] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[EmployeeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[Booking_ID] [int] NOT NULL,
	[S_Number] [int] NOT NULL,
	[I_Number] [int] NOT NULL,
	[Booking_Date] [datetime] NULL,
	[Start_Time] [time](7) NULL,
	[End_Time] [time](7) NULL,
	[Pick_Location] [nvarchar](150) NULL,
	[Drop_Location] [nvarchar](150) NULL,
	[Package_ID] [int] NOT NULL,
	[Discount] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[Cancelled_Date] [datetime] NULL,
	[Cancelled_Reason] [nvarchar](200) NULL,
	[Type] [nvarchar](50) NULL,
	[RTA_ID] [int] NULL,
	[DrivingTest_Status] [nvarchar](50) NULL,
	[DrivingTest_Attempt] [int] NULL,
	[Created_Date] [datetime] NULL,
	[Modified_Date] [datetime] NULL,
	[Created_By] [nvarchar](50) NULL,
	[Modified_By] [nvarchar](50) NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[Booking_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instructor_Leaves]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor_Leaves](
	[ID] [int] NOT NULL,
	[I_Number] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Reason] [nvarchar](200) NULL,
	[Status] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
	[Created_By] [nvarchar](50) NULL,
	[Modified_Date] [datetime] NULL,
	[Modified_By] [nvarchar](50) NULL,
 CONSTRAINT [PK_Instructor_Leaves] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instructor_Student]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor_Student](
	[ID] [int] NOT NULL,
	[I_Number] [int] NOT NULL,
	[S_Number] [int] NOT NULL,
	[DateLinked] [datetime] NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
	[Modified_Date] [datetime] NULL,
	[Modified_By] [nvarchar](50) NULL,
 CONSTRAINT [PK_Instructor_Student] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instructors]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructors](
	[InstructorId] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Phone] [int] NULL,
	[Mobile] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
	[Modified_Date] [datetime] NULL,
	[Modified_By] [nvarchar](50) NULL,
 CONSTRAINT [PK_Instructors] PRIMARY KEY CLUSTERED 
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Package_Details]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package_Details](
	[Package_ID] [int] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[NumberOfHours] [int] NOT NULL,
	[Cost] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Created_Date] [datetime] NULL,
	[Modified_Date] [datetime] NULL,
	[Created_By] [nvarchar](50) NULL,
	[Modified_By] [nvarchar](50) NULL,
 CONSTRAINT [PK_Package_Details] PRIMARY KEY CLUSTERED 
(
	[Package_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostCodes]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCodes](
	[PostCode] [int] NOT NULL,
	[SuburbID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RTAs]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RTAs](
	[RTA_ID] [int] NOT NULL,
	[RTA_Name] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[States]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[State_ID] [int] NOT NULL,
	[State_Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[State_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student_Address]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_Address](
	[S_Address_ID] [int] NOT NULL,
	[S_Number] [int] NOT NULL,
	[AddressLine1] [nvarchar](150) NULL,
	[AddressLine2] [nvarchar](150) NULL,
	[SuburbID] [int] NOT NULL,
	[PostCode] [int] NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
	[Created_By] [nvarchar](50) NOT NULL,
	[Modified_Date] [datetime] NOT NULL,
	[Modified_By] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Student_Address] PRIMARY KEY CLUSTERED 
(
	[S_Address_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student_License]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_License](
	[ID] [int] NOT NULL,
	[S_Number] [int] NULL,
	[License_No] [nvarchar](50) NULL,
	[State_ID] [int] NOT NULL,
	[Expiry_Date] [date] NOT NULL,
	[Class] [nvarchar](50) NULL,
	[License_passed_Date] [date] NULL,
	[Remarks] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[S_Number] [int] NOT NULL,
	[First_Name] [nvarchar](100) NOT NULL,
	[Last_Name] [nvarchar](100) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Start_Date] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Created_Date] [datetime] NULL,
	[Created_By] [nvarchar](128) NOT NULL,
	[Modified_Date] [datetime] NULL,
	[Modified_By] [nvarchar](50) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[S_Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Suburbs]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suburbs](
	[Suburb_ID] [int] NOT NULL,
	[Suburb_Name] [nvarchar](100) NULL,
	[State_ID] [int] NOT NULL,
 CONSTRAINT [PK_Suburbs] PRIMARY KEY CLUSTERED 
(
	[Suburb_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 05-12-2015 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](300) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_EmployeeId]  DEFAULT ((0)) FOR [EmployeeId]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Bookings] FOREIGN KEY([Booking_ID])
REFERENCES [dbo].[Bookings] ([Booking_ID])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Bookings]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Students] FOREIGN KEY([S_Number])
REFERENCES [dbo].[Students] ([S_Number])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Students]
GO
ALTER TABLE [dbo].[Instructors]  WITH CHECK ADD  CONSTRAINT [FK_Instructors_AspNetUsers] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Instructors] CHECK CONSTRAINT [FK_Instructors_AspNetUsers]
GO
ALTER TABLE [dbo].[Student_Address]  WITH CHECK ADD  CONSTRAINT [FK_Student_Address_Students] FOREIGN KEY([S_Number])
REFERENCES [dbo].[Students] ([S_Number])
GO
ALTER TABLE [dbo].[Student_Address] CHECK CONSTRAINT [FK_Student_Address_Students]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_AspNetUsers] FOREIGN KEY([Created_By])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_AspNetUsers]
GO
ALTER TABLE [dbo].[Suburbs]  WITH CHECK ADD  CONSTRAINT [FK_Suburbs_Suburbs] FOREIGN KEY([State_ID])
REFERENCES [dbo].[States] ([State_ID])
GO
ALTER TABLE [dbo].[Suburbs] CHECK CONSTRAINT [FK_Suburbs_Suburbs]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_AspNetUsers]
GO
USE [master]
GO
ALTER DATABASE [AutogearDB] SET  READ_WRITE 
GO
