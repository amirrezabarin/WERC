USE [WERCTEST]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 10/20/2018 3:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[DateOfIssue] [datetime] NOT NULL,
	[InvoiceNumber] [bigint] NOT NULL,
	[InvoiceTotal] [decimal](18, 0) NOT NULL,
	[Subtotal] [decimal](18, 0) NOT NULL,
	[Tax] [decimal](18, 0) NOT NULL,
	[Total] [decimal](18, 0) NOT NULL,
	[AmountDue] [decimal](18, 0) NOT NULL,
	[Finished] [bit] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 10/20/2018 3:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[TeamId] [int] NOT NULL,
	[PaymentRuleId] [int] NOT NULL,
	[IsFirstTeam] [bit] NOT NULL,
	[TeamUnitCost] [decimal](18, 0) NOT NULL,
	[ExtraParticipantCount] [int] NOT NULL,
	[ExtraParticipantUnitCost] [decimal](18, 0) NOT NULL,
	[ExtraParticipantAmount] [decimal](18, 0) NOT NULL,
	[ExtraTeamDiscount] [decimal](18, 0) NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentRule]    Script Date: 10/20/2018 3:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentRule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeOfRegistration] [nvarchar](max) NOT NULL,
	[FirstTeamFee] [decimal](18, 0) NOT NULL,
	[ExtraTeamDiscount] [decimal](18, 0) NOT NULL,
	[DueDatePrefix] [nvarchar](max) NULL,
	[DueDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PaymentRule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 10/20/2018 3:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 10/20/2018 3:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Date] [datetime] NOT NULL,
	[State] [int] NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[LabResultUrl] [nvarchar](max) NULL,
	[WrittenReportUrl] [nvarchar](max) NULL,
	[TeamNumber] [int] NULL,
	[PayStatus] [bit] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeamMember]    Script Date: 10/20/2018 3:36:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamMember](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeamId] [int] NULL,
	[MemberUserId] [nvarchar](128) NULL,
	[RegistrationStatus] [bit] NULL,
	[Survey] [bit] NULL,
 CONSTRAINT [PK_TeamMember] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/20/2018 3:36:43 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/20/2018 3:36:43 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[RegisterDate] [datetime] NULL,
	[AuthenticationCode] [nvarchar](max) NULL,
	[LastSignIn] [datetime] NULL,
	[UserDefiner] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DietType]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DietType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Display] [bit] NULL,
 CONSTRAINT [PK_DietType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[JacketSizeId] [int] NULL,
	[DietTypeId] [int] NULL,
	[SizeId] [int] NULL,
	[UniversityId] [int] NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[StreetLine1] [nvarchar](max) NULL,
	[StreetLine2] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[ZipCode] [nvarchar](max) NULL,
	[Identifier] [nvarchar](128) NULL,
	[Sex] [bit] NULL,
	[BirthDate] [date] NULL,
	[ShortBio] [nvarchar](max) NULL,
	[EmgyPersonFirstName] [nvarchar](max) NULL,
	[EmgyPersonLastName] [nvarchar](max) NULL,
	[EmgyPersonPhoneNumber] [nvarchar](max) NULL,
	[EmgyPersonRelationship] [nvarchar](max) NULL,
	[ProfilePictureUrl] [nvarchar](max) NULL,
	[ResumeUrl] [nvarchar](max) NULL,
	[WelcomeDinner] [bit] NOT NULL,
	[LunchOnMonday] [bit] NOT NULL,
	[LunchOnTuesday] [bit] NOT NULL,
	[ReceptionNetworkOnTuesday] [bit] NOT NULL,
	[AwardBanquet] [bit] NOT NULL,
	[NoneOfTheAbove] [bit] NOT NULL,
	[Allergies] [nvarchar](max) NULL,
	[SecondaryEmail] [nvarchar](max) NULL,
	[Agreement] [bit] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[University]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[University](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[UniversityPictureUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewPersonInRole]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewPersonInRole]
AS
SELECT        dbo.Person.LastName + N' ' + dbo.Person.FirstName AS Name, dbo.Person.Identifier, dbo.Person.Sex, dbo.Person.BirthDate, dbo.Person.UserId, dbo.Person.Id, dbo.AspNetUsers.UserName, dbo.AspNetUsers.Email, 
                         dbo.AspNetUsers.RegisterDate, dbo.AspNetRoles.Name AS RoleName, dbo.AspNetUserRoles.RoleId, dbo.AspNetUsers.UserDefiner, dbo.AspNetUsers.LastSignIn, dbo.Person.UniversityId, dbo.University.Name AS University, 
                         dbo.Person.StreetLine1, dbo.Person.StreetLine2, dbo.Person.City, dbo.Person.State, dbo.Person.ZipCode, dbo.Person.ShortBio, dbo.Person.ProfilePictureUrl, dbo.Person.ResumeUrl, dbo.Person.FirstName, 
                         dbo.Person.LastName, dbo.AspNetUsers.LockoutEnabled, dbo.AspNetUsers.EmailConfirmed, dbo.Person.SizeId, Size_1.Name AS T_Shirt_Size, dbo.Person.EmgyPersonFirstName, dbo.Person.EmgyPersonLastName, 
                         dbo.Person.EmgyPersonPhoneNumber, dbo.Person.EmgyPersonRelationship, dbo.AspNetUsers.PhoneNumber, dbo.Person.JacketSizeId, dbo.Size.Name AS JacketSize, dbo.Person.DietTypeId, dbo.DietType.Name AS DietType, 
                         dbo.University.UniversityPictureUrl, dbo.Person.WelcomeDinner, dbo.Person.LunchOnMonday, dbo.Person.LunchOnTuesday, dbo.Person.ReceptionNetworkOnTuesday, dbo.Person.AwardBanquet, dbo.Person.NoneOfTheAbove,
                          dbo.Person.Allergies, dbo.Person.SecondaryEmail, dbo.Person.Agreement
FROM            dbo.Person INNER JOIN
                         dbo.AspNetUsers ON dbo.Person.UserId = dbo.AspNetUsers.Id INNER JOIN
                         dbo.AspNetUserRoles ON dbo.AspNetUsers.Id = dbo.AspNetUserRoles.UserId INNER JOIN
                         dbo.AspNetRoles ON dbo.AspNetUserRoles.RoleId = dbo.AspNetRoles.Id LEFT OUTER JOIN
                         dbo.DietType ON dbo.Person.DietTypeId = dbo.DietType.Id LEFT OUTER JOIN
                         dbo.Size ON dbo.Person.JacketSizeId = dbo.Size.Id LEFT OUTER JOIN
                         dbo.Size AS Size_1 ON dbo.Person.SizeId = Size_1.Id LEFT OUTER JOIN
                         dbo.University ON dbo.Person.UniversityId = dbo.University.Id
GO
/****** Object:  View [dbo].[ViewTeam]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTeam]
AS
SELECT        dbo.Team.Id, dbo.Team.TaskId, dbo.Team.Name, dbo.Task.Name AS Task, dbo.TeamMember.Survey, dbo.ViewPersonInRole.Name AS MemberName, dbo.ViewPersonInRole.Identifier, dbo.ViewPersonInRole.Sex, 
                         dbo.ViewPersonInRole.BirthDate, dbo.ViewPersonInRole.UserName, dbo.ViewPersonInRole.Email, dbo.ViewPersonInRole.RegisterDate, dbo.ViewPersonInRole.RoleName, dbo.ViewPersonInRole.RoleId, 
                         dbo.ViewPersonInRole.UserDefiner, dbo.ViewPersonInRole.LastSignIn, dbo.ViewPersonInRole.UniversityId, dbo.ViewPersonInRole.University, dbo.ViewPersonInRole.StreetLine1, dbo.ViewPersonInRole.StreetLine2, 
                         dbo.ViewPersonInRole.City, dbo.ViewPersonInRole.State, dbo.ViewPersonInRole.ZipCode, dbo.ViewPersonInRole.ShortBio, dbo.ViewPersonInRole.ProfilePictureUrl, dbo.ViewPersonInRole.ResumeUrl, 
                         dbo.TeamMember.MemberUserId, dbo.Team.Date, dbo.Team.State AS TeamState, dbo.Team.ImageUrl AS TeamImageUrl, dbo.ViewPersonInRole.EmailConfirmed, dbo.ViewPersonInRole.LockoutEnabled, 
                         dbo.ViewPersonInRole.LastName, dbo.ViewPersonInRole.FirstName, dbo.TeamMember.RegistrationStatus, dbo.ViewPersonInRole.SizeId, dbo.ViewPersonInRole.T_Shirt_Size, dbo.ViewPersonInRole.EmgyPersonFirstName, 
                         dbo.ViewPersonInRole.EmgyPersonLastName, dbo.ViewPersonInRole.EmgyPersonPhoneNumber, dbo.ViewPersonInRole.EmgyPersonRelationship, dbo.ViewPersonInRole.PhoneNumber, dbo.Team.LabResultUrl, 
                         dbo.Team.WrittenReportUrl, dbo.Team.TeamNumber, dbo.ViewPersonInRole.JacketSizeId, dbo.ViewPersonInRole.JacketSize, dbo.ViewPersonInRole.DietTypeId, dbo.ViewPersonInRole.DietType, 
                         dbo.ViewPersonInRole.UniversityPictureUrl, dbo.Task.Description AS TaskDescription, dbo.Team.PayStatus
FROM            dbo.Team INNER JOIN
                         dbo.Task ON dbo.Team.TaskId = dbo.Task.Id INNER JOIN
                         dbo.TeamMember ON dbo.Team.Id = dbo.TeamMember.TeamId INNER JOIN
                         dbo.ViewPersonInRole ON dbo.TeamMember.MemberUserId = dbo.ViewPersonInRole.UserId
WHERE        (dbo.ViewPersonInRole.RoleId = N'58c326dd-38ea-4d3c-92f9-3935e3763e68')
GO
/****** Object:  View [dbo].[ViewInvoice]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewInvoice]
AS
SELECT        dbo.Invoice.Id, dbo.Invoice.Title, dbo.Invoice.DateOfIssue, dbo.Invoice.InvoiceNumber, dbo.Invoice.InvoiceTotal, dbo.InvoiceDetail.Id AS InvoiceDetailId, dbo.InvoiceDetail.PaymentRuleId, dbo.InvoiceDetail.IsFirstTeam, 
                         dbo.InvoiceDetail.TeamUnitCost, dbo.InvoiceDetail.ExtraParticipantCount, dbo.InvoiceDetail.ExtraParticipantUnitCost, dbo.InvoiceDetail.ExtraParticipantAmount, dbo.InvoiceDetail.Amount, dbo.Invoice.Subtotal, dbo.Invoice.Tax, 
                         dbo.Invoice.Total, dbo.Invoice.AmountDue, dbo.PaymentRule.TypeOfRegistration, dbo.PaymentRule.FirstTeamFee, dbo.PaymentRule.DueDate, dbo.ViewTeam.Name AS TeamName, dbo.ViewTeam.RoleName, 
                         dbo.ViewTeam.RoleId, dbo.ViewTeam.UniversityId, dbo.ViewTeam.University, dbo.ViewTeam.StreetLine1, dbo.ViewTeam.StreetLine2, dbo.ViewTeam.City, dbo.ViewTeam.State, dbo.ViewTeam.ZipCode, dbo.ViewTeam.PayStatus, 
                         dbo.ViewTeam.FirstName, dbo.ViewTeam.LastName, dbo.ViewTeam.PhoneNumber, dbo.ViewTeam.Email, dbo.ViewTeam.Sex, dbo.ViewTeam.MemberUserId AS UserId, dbo.ViewTeam.Id AS TeamId, dbo.Invoice.Finished, 
                         dbo.InvoiceDetail.ExtraTeamDiscount
FROM            dbo.Invoice INNER JOIN
                         dbo.PaymentRule INNER JOIN
                         dbo.InvoiceDetail ON dbo.PaymentRule.Id = dbo.InvoiceDetail.PaymentRuleId ON dbo.Invoice.Id = dbo.InvoiceDetail.InvoiceId RIGHT OUTER JOIN
                         dbo.ViewTeam ON dbo.InvoiceDetail.TeamId = dbo.ViewTeam.Id
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskGrade]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskGrade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[GradeId] [int] NOT NULL,
 CONSTRAINT [PK_TaskGrade] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewTask]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTask]
AS
SELECT        dbo.Task.Id, dbo.Task.Name, dbo.Task.ImageUrl, dbo.Task.Description, dbo.TaskGrade.GradeId, dbo.Grade.Name AS Grade
FROM            dbo.Task INNER JOIN
                         dbo.TaskGrade ON dbo.Task.Id = dbo.TaskGrade.TaskId INNER JOIN
                         dbo.Grade ON dbo.TaskGrade.GradeId = dbo.Grade.Id
GO
/****** Object:  Table [dbo].[UserTask]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[TaskId] [int] NOT NULL,
 CONSTRAINT [PK_UserTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewUserTask]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUserTask]
AS
SELECT        dbo.UserTask.Id, dbo.UserTask.UserId, dbo.UserTask.TaskId, dbo.Task.Name AS TaskName, dbo.ViewPersonInRole.Name, dbo.ViewPersonInRole.UserName, dbo.ViewPersonInRole.Email, dbo.ViewPersonInRole.RoleName, 
                         dbo.ViewPersonInRole.RoleId, dbo.ViewPersonInRole.StreetLine1, dbo.ViewPersonInRole.StreetLine2, dbo.ViewPersonInRole.City, dbo.ViewPersonInRole.State, dbo.ViewPersonInRole.ZipCode, dbo.ViewPersonInRole.ShortBio, 
                         dbo.ViewPersonInRole.ProfilePictureUrl, dbo.ViewPersonInRole.FirstName, dbo.ViewPersonInRole.LastName, dbo.ViewPersonInRole.LockoutEnabled, dbo.ViewPersonInRole.EmailConfirmed, dbo.ViewPersonInRole.SizeId, 
                         dbo.ViewPersonInRole.T_Shirt_Size, dbo.ViewPersonInRole.PhoneNumber, dbo.Task.ImageUrl, dbo.ViewPersonInRole.JacketSizeId, dbo.ViewPersonInRole.JacketSize, dbo.ViewPersonInRole.DietTypeId, 
                         dbo.ViewPersonInRole.DietType, dbo.ViewPersonInRole.UniversityPictureUrl, dbo.Task.Description AS TaskDescription
FROM            dbo.UserTask INNER JOIN
                         dbo.Task ON dbo.UserTask.TaskId = dbo.Task.Id INNER JOIN
                         dbo.ViewPersonInRole ON dbo.UserTask.UserId = dbo.ViewPersonInRole.UserId
GO
/****** Object:  View [dbo].[ViewTaskFullInfo]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTaskFullInfo]
AS
SELECT        Id, Name, ImageUrl, Grade, GradeId, Judges = STUFF
                             ((SELECT        ', ' + vu.FirstName + ' ' + vu.LastName
                                 FROM            dbo.ViewUserTask vu
                                 WHERE        vu.RoleId = N'4d7951d8-8eda-4452-8ff1-dfc9076d8bbe' AND vu.TaskId = vt.Id FOR XML path('')), 1, 1, ''), Description
FROM            dbo.ViewTask vt
GO
/****** Object:  View [dbo].[ViewTaskTeam]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTaskTeam]
AS
SELECT        dbo.UserTask.Id, dbo.UserTask.UserId, dbo.UserTask.TaskId, dbo.Task.Name AS TaskName, dbo.Task.ImageUrl AS TaskImageUrl, dbo.Team.ImageUrl AS TeamImageUrl, dbo.Team.State AS TeamState, dbo.Team.Date, 
                         dbo.Team.Name AS TeamName, dbo.Team.Id AS TeamId, dbo.ViewTeam.University, dbo.ViewTeam.Survey, dbo.ViewTeam.RegistrationStatus, dbo.Team.LabResultUrl, dbo.ViewTeam.MemberName, dbo.Team.WrittenReportUrl, 
                         dbo.Team.TeamNumber, dbo.ViewTeam.UniversityPictureUrl, dbo.ViewTeam.PayStatus, dbo.Task.Description AS TaskDescription
FROM            dbo.UserTask INNER JOIN
                         dbo.Task ON dbo.UserTask.TaskId = dbo.Task.Id INNER JOIN
                         dbo.Team ON dbo.Task.Id = dbo.Team.TaskId INNER JOIN
                         dbo.ViewTeam ON dbo.Team.Id = dbo.ViewTeam.Id
GO
/****** Object:  View [dbo].[ViewTeamMember]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTeamMember]
AS
SELECT        dbo.Team.Id AS TeamId, dbo.Team.TaskId, dbo.Team.Name AS TeamName, dbo.Task.Name AS Task, dbo.TeamMember.Survey, dbo.ViewPersonInRole.Name AS MemberName, dbo.ViewPersonInRole.Identifier, 
                         dbo.ViewPersonInRole.Sex, dbo.ViewPersonInRole.BirthDate, dbo.ViewPersonInRole.UserName, dbo.ViewPersonInRole.Email, dbo.ViewPersonInRole.RegisterDate, dbo.ViewPersonInRole.RoleName, 
                         dbo.ViewPersonInRole.RoleId, dbo.ViewPersonInRole.UserDefiner, dbo.ViewPersonInRole.LastSignIn, dbo.ViewPersonInRole.UniversityId, dbo.ViewPersonInRole.University, dbo.ViewPersonInRole.StreetLine1, 
                         dbo.ViewPersonInRole.StreetLine2, dbo.ViewPersonInRole.City, dbo.ViewPersonInRole.State, dbo.ViewPersonInRole.ZipCode, dbo.ViewPersonInRole.ShortBio, dbo.ViewPersonInRole.ProfilePictureUrl, 
                         dbo.ViewPersonInRole.ResumeUrl, dbo.TeamMember.MemberUserId, dbo.Team.Date, dbo.TeamMember.Id, dbo.Team.State AS TeamState, dbo.Team.ImageUrl AS TeamImageUrl, dbo.ViewPersonInRole.FirstName, 
                         dbo.ViewPersonInRole.LastName, dbo.ViewPersonInRole.EmailConfirmed, dbo.ViewPersonInRole.LockoutEnabled, dbo.TeamMember.RegistrationStatus, dbo.ViewPersonInRole.PhoneNumber, 
                         dbo.ViewPersonInRole.EmgyPersonFirstName, dbo.ViewPersonInRole.EmgyPersonLastName, dbo.ViewPersonInRole.EmgyPersonPhoneNumber, dbo.ViewPersonInRole.EmgyPersonRelationship, 
                         dbo.ViewPersonInRole.T_Shirt_Size, dbo.ViewPersonInRole.SizeId, dbo.Team.LabResultUrl, dbo.Team.WrittenReportUrl, dbo.Team.TeamNumber, dbo.ViewPersonInRole.JacketSize, dbo.ViewPersonInRole.JacketSizeId, 
                         dbo.ViewPersonInRole.DietTypeId, dbo.ViewPersonInRole.DietType, dbo.ViewPersonInRole.UniversityPictureUrl, dbo.Team.PayStatus
FROM            dbo.Team INNER JOIN
                         dbo.TeamMember ON dbo.Team.Id = dbo.TeamMember.TeamId INNER JOIN
                         dbo.ViewPersonInRole ON dbo.TeamMember.MemberUserId = dbo.ViewPersonInRole.UserId INNER JOIN
                         dbo.Task ON dbo.Team.TaskId = dbo.Task.Id
GO
/****** Object:  View [dbo].[ViewTeamFullInfo]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTeamFullInfo]
AS
SELECT        T .Id, T .TaskId, T .Name, dbo.ViewPersonInRole.Name AS Advisor,
                             (SELECT        vt.MemberName
                                FROM            dbo.ViewTeamMember vt
                                WHERE        vt.RoleId = N'291d6069-44a3-4960-86d3-b91bda430e71' AND vt.TeamId = T .Id) AS Leader, dbo.Task.Name AS TaskName, Judges = STUFF
                             ((SELECT        ', ' + vu.FirstName + ' ' + vu.LastName
                                 FROM            dbo.ViewUserTask vu
                                 WHERE        vu.RoleId = N'4d7951d8-8eda-4452-8ff1-dfc9076d8bbe' AND vu.TaskId = T .TaskId FOR XML path('')), 1, 1, ''), CASE WHEN EXISTS
                             (SELECT        tm.Survey
                                FROM            dbo.TeamMember tm
                                WHERE       tm.TeamId = T .Id AND Survey = 0) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS Survey, CASE WHEN EXISTS
                             (SELECT        tm.RegistrationStatus
                                FROM            dbo.TeamMember tm
                                WHERE        tm.TeamId = T .Id AND RegistrationStatus = 0) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS RegistrationStatus, dbo.ViewPersonInRole.Identifier, dbo.ViewPersonInRole.Sex, 
                         dbo.ViewPersonInRole.BirthDate, dbo.ViewPersonInRole.UserName, dbo.ViewPersonInRole.Email, dbo.ViewPersonInRole.RegisterDate, dbo.ViewPersonInRole.RoleName, dbo.ViewPersonInRole.RoleId, 
                         dbo.ViewPersonInRole.UserDefiner, dbo.ViewPersonInRole.LastSignIn, dbo.ViewPersonInRole.UniversityId, dbo.ViewPersonInRole.University, dbo.ViewPersonInRole.StreetLine1, dbo.ViewPersonInRole.StreetLine2, 
                         dbo.ViewPersonInRole.City, dbo.ViewPersonInRole.State, dbo.ViewPersonInRole.ZipCode, dbo.ViewPersonInRole.ShortBio, dbo.ViewPersonInRole.ProfilePictureUrl, dbo.ViewPersonInRole.ResumeUrl, 
                         dbo.TeamMember.MemberUserId, T .Date, T .State AS TeamState, T .ImageUrl AS TeamImageUrl, dbo.ViewPersonInRole.EmailConfirmed, dbo.ViewPersonInRole.LockoutEnabled, dbo.ViewPersonInRole.LastName, 
                         dbo.ViewPersonInRole.FirstName, dbo.ViewPersonInRole.SizeId, dbo.ViewPersonInRole.T_Shirt_Size, dbo.ViewPersonInRole.EmgyPersonFirstName, dbo.ViewPersonInRole.EmgyPersonLastName, 
                         dbo.ViewPersonInRole.EmgyPersonPhoneNumber, dbo.ViewPersonInRole.EmgyPersonRelationship, dbo.ViewPersonInRole.PhoneNumber, T .PayStatus, T .LabResultUrl, T .WrittenReportUrl, T .TeamNumber, 
                         dbo.ViewPersonInRole.JacketSizeId, dbo.ViewPersonInRole.JacketSize, dbo.ViewPersonInRole.DietTypeId, dbo.ViewPersonInRole.DietType, dbo.ViewPersonInRole.UniversityPictureUrl
FROM            dbo.Team T INNER JOIN
                         dbo.Task ON T .TaskId = dbo.Task.Id INNER JOIN
                         dbo.TeamMember ON T .Id = dbo.TeamMember.TeamId INNER JOIN
                         dbo.ViewPersonInRole ON dbo.TeamMember.MemberUserId = dbo.ViewPersonInRole.UserId
WHERE        (dbo.ViewPersonInRole.RoleId = N'58c326dd-38ea-4d3c-92f9-3935e3763e68')
GO
/****** Object:  Table [dbo].[GradeDetail]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GradeDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GradeId] [int] NOT NULL,
	[EvaluationItem] [nvarchar](max) NOT NULL,
	[Point] [float] NOT NULL,
	[Coefficient] [float] NOT NULL,
 CONSTRAINT [PK_GradeDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewGradeDetail]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewGradeDetail]
AS
SELECT        dbo.Grade.Name, dbo.GradeDetail.EvaluationItem, dbo.GradeDetail.Point, dbo.GradeDetail.Coefficient, dbo.GradeDetail.Id, dbo.GradeDetail.GradeId
FROM            dbo.Grade INNER JOIN
                         dbo.GradeDetail ON dbo.Grade.Id = dbo.GradeDetail.GradeId
GO
/****** Object:  Table [dbo].[TeamGradeDetail]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamGradeDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JudgeUserId] [nvarchar](128) NOT NULL,
	[TeamId] [int] NOT NULL,
	[GradeDetailId] [int] NOT NULL,
	[Point] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_TeamGradeDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewTeamGradeDetail]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTeamGradeDetail]
AS
SELECT        dbo.TeamGradeDetail.Id, dbo.TeamGradeDetail.TeamId, dbo.TeamGradeDetail.GradeDetailId, dbo.Team.TaskId, dbo.Team.Name AS TeamName, dbo.Team.Date, dbo.Team.State, dbo.Team.ImageUrl, dbo.Team.LabResultUrl, 
                         dbo.Team.WrittenReportUrl, dbo.GradeDetail.GradeId, dbo.GradeDetail.EvaluationItem, dbo.GradeDetail.Point AS MaxPoint, dbo.Grade.Name AS Grade, dbo.TeamGradeDetail.Point, dbo.Team.TeamNumber, 
                         dbo.TeamGradeDetail.Description, dbo.TeamGradeDetail.JudgeUserId, dbo.GradeDetail.Coefficient
FROM            dbo.TeamGradeDetail INNER JOIN
                         dbo.Team ON dbo.TeamGradeDetail.TeamId = dbo.Team.Id INNER JOIN
                         dbo.Grade INNER JOIN
                         dbo.GradeDetail ON dbo.Grade.Id = dbo.GradeDetail.GradeId ON dbo.TeamGradeDetail.GradeDetailId = dbo.GradeDetail.Id
GO
/****** Object:  View [dbo].[ViewTeamGradeMetaData]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewTeamGradeMetaData]
AS
SELECT        dbo.Team.Id, dbo.Team.TaskId, dbo.ViewGradeDetail.EvaluationItem, dbo.ViewGradeDetail.Point, dbo.ViewGradeDetail.Coefficient, dbo.ViewGradeDetail.Id AS GradeDetailId, dbo.ViewGradeDetail.GradeId, 
                         dbo.Team.Name AS TeamName, dbo.Team.Date, dbo.Team.State, dbo.Team.ImageUrl, dbo.Team.LabResultUrl, dbo.Team.WrittenReportUrl, dbo.ViewGradeDetail.Name AS Grade, dbo.Team.TeamNumber, 
                         dbo.Task.Description AS TaskDescription, dbo.Team.PayStatus, dbo.UserTask.UserId
FROM            dbo.Team INNER JOIN
                         dbo.Task ON dbo.Team.TaskId = dbo.Task.Id INNER JOIN
                         dbo.TaskGrade ON dbo.Task.Id = dbo.TaskGrade.TaskId INNER JOIN
                         dbo.ViewGradeDetail ON dbo.TaskGrade.GradeId = dbo.ViewGradeDetail.GradeId INNER JOIN
                         dbo.UserTask ON dbo.Task.Id = dbo.UserTask.TaskId
GO
/****** Object:  View [dbo].[ViewJudgeFullInfo]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewJudgeFullInfo]
AS
SELECT        Id, UserId, RoleId, Sex, UserName, Email, RoleName, StreetLine1, StreetLine2, City, State, ZipCode, ShortBio, ProfilePictureUrl, ResumeUrl, FirstName, LastName, EmailConfirmed, SizeId, T_Shirt_Size, PhoneNumber, 
                         DietTypeId, DietType
, Tasks = STUFF
                             ((SELECT        ', ' + vu.TaskName
                                 FROM            dbo.ViewUserTask vu
                                 WHERE        vu.UserId = P.UserId FOR XML path('')), 1, 1, ''), Teams = STUFF
                             ((SELECT        ', ' + vt.TeamName
                                 FROM            dbo.ViewTaskTeam vt
                                 WHERE        vt.UserId = P.UserId FOR XML path('')), 1, 1, '')
FROM            dbo.ViewPersonInRole AS P
WHERE        (RoleId = N'4d7951d8-8eda-4452-8ff1-dfc9076d8bbe')
GO
/****** Object:  View [dbo].[ViewUserRole]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUserRole]
AS
SELECT        dbo.AspNetRoles.Id, dbo.AspNetUserRoles.UserId, dbo.AspNetRoles.Name AS RoleName, dbo.AspNetUsers.UserName, dbo.AspNetUsers.Email
FROM            dbo.AspNetUserRoles INNER JOIN
                         dbo.AspNetRoles ON dbo.AspNetUserRoles.RoleId = dbo.AspNetRoles.Id INNER JOIN
                         dbo.AspNetUsers ON dbo.AspNetUserRoles.UserId = dbo.AspNetUsers.Id
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/20/2018 3:36:44 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/20/2018 3:36:44 PM ******/
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
/****** Object:  Table [dbo].[City]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ProvinceId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Iso] [nvarchar](4) NOT NULL,
	[Name] [nvarchar](160) NOT NULL,
	[NiceName] [nvarchar](160) NOT NULL,
	[Iso3] [nvarchar](6) NULL,
	[NumCode] [smallint] NULL,
	[PhoneCode] [int] NOT NULL,
 CONSTRAINT [PK__Country__3214EC07B0B900E1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dictionary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CultureInfoCode] [nvarchar](255) NOT NULL,
	[RefrenceWordId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Dictionary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExcelDictionary]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExcelDictionary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[en-US] [nvarchar](255) NULL,
	[fa-IR] [nvarchar](255) NULL,
	[Orig] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Type] [tinyint] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_WERCImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](255) NULL,
	[TwoLetterCountryCode] [nvarchar](255) NULL,
	[ThreeLetterCountryCode] [nvarchar](255) NULL,
	[Language] [nvarchar](255) NULL,
	[TwoLetterLangCode] [nvarchar](255) NULL,
	[ThreeLetterLangCode] [nvarchar](255) NULL,
	[CultureInfoCode] [nvarchar](255) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[CultureInfoCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageContent]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageContent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_FirstPageContent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParticipantRule]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParticipantRule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstTeamMaxMember] [int] NOT NULL,
	[EachExtraTeamMaxMember] [int] NOT NULL,
	[ExtraParticipantFee] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_ParticipantRule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Province]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reference]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reference](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[ArticleId] [int] NULL,
	[VisitCount] [int] NULL,
	[VisitDate] [date] NULL,
	[Browser] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Reference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefrenceWord]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefrenceWord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Word] [nvarchar](max) NULL,
 CONSTRAINT [PK_RefrenceWord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteInfo]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteName] [nvarchar](max) NULL,
 CONSTRAINT [PK_SiteInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorldUniversity]    Script Date: 10/20/2018 3:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorldUniversity](
	[AD] [nvarchar](255) NULL,
	[Name] [nvarchar](max) NULL,
	[http://www#uda#ad/] [nvarchar](255) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoles] ADD  CONSTRAINT [DF_AspNetRoles_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_LockoutEndDateUtc]  DEFAULT ('2018-01-01T00:00:00.000') FOR [LockoutEndDateUtc]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_RegisterDate]  DEFAULT ('2018-01-01T00:00:00.000') FOR [RegisterDate]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_LastSignIn]  DEFAULT ('2018-01-01T00:00:00.000') FOR [LastSignIn]
GO
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_Active]  DEFAULT ((0)) FOR [Active]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF__Country__Iso3__4E9398CC]  DEFAULT (NULL) FOR [Iso3]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF__Country__NumCode__4F87BD05]  DEFAULT (NULL) FOR [NumCode]
GO
ALTER TABLE [dbo].[DietType] ADD  CONSTRAINT [DF_DietType_Type]  DEFAULT ((0)) FOR [Display]
GO
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_Priority]  DEFAULT ((0)) FOR [Priority]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_InvoiceTotal]  DEFAULT ((0)) FOR [InvoiceTotal]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_Finished]  DEFAULT ((0)) FOR [Finished]
GO
ALTER TABLE [dbo].[InvoiceDetail] ADD  CONSTRAINT [DF_InvoiceDetail_IsFirstTeam]  DEFAULT ((0)) FOR [IsFirstTeam]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_IsDefault]  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_A0D4FE3A8E1C4519B8E29BC7F53AC475_Sex]  DEFAULT ((0)) FOR [Sex]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_WelcomeDinner]  DEFAULT ((0)) FOR [WelcomeDinner]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_LunchOnMonday]  DEFAULT ((0)) FOR [LunchOnMonday]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_LunchOnTuesday]  DEFAULT ((0)) FOR [LunchOnTuesday]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_ReceptionNetworkOnTuesday]  DEFAULT ((0)) FOR [ReceptionNetworkOnTuesday]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_AwardBanquet]  DEFAULT ((0)) FOR [AwardBanquet]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_NoneOfTheAbove]  DEFAULT ((0)) FOR [NoneOfTheAbove]
GO
ALTER TABLE [dbo].[Reference] ADD  CONSTRAINT [DF_Reference_VisitCount]  DEFAULT ((1)) FOR [VisitCount]
GO
ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_State]  DEFAULT ((0)) FOR [State]
GO
ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_PayStatus]  DEFAULT ((0)) FOR [PayStatus]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
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
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_ProvinceCity] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[Province] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_ProvinceCity]
GO
ALTER TABLE [dbo].[Dictionary]  WITH CHECK ADD  CONSTRAINT [FK_Dictionary_Language] FOREIGN KEY([CultureInfoCode])
REFERENCES [dbo].[Language] ([CultureInfoCode])
GO
ALTER TABLE [dbo].[Dictionary] CHECK CONSTRAINT [FK_Dictionary_Language]
GO
ALTER TABLE [dbo].[Dictionary]  WITH CHECK ADD  CONSTRAINT [FK_Dictionary_RefrenceWord] FOREIGN KEY([RefrenceWordId])
REFERENCES [dbo].[RefrenceWord] ([Id])
GO
ALTER TABLE [dbo].[Dictionary] CHECK CONSTRAINT [FK_Dictionary_RefrenceWord]
GO
ALTER TABLE [dbo].[GradeDetail]  WITH CHECK ADD  CONSTRAINT [FK_GradeDetail_Grade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GradeDetail] CHECK CONSTRAINT [FK_GradeDetail_Grade]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_AspNetUsers]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_InvoiceDetail] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_InvoiceDetail]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_PaymentRule] FOREIGN KEY([PaymentRuleId])
REFERENCES [dbo].[PaymentRule] ([Id])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_PaymentRule]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_AspNetUsers]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_DietType] FOREIGN KEY([DietTypeId])
REFERENCES [dbo].[DietType] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_DietType]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_University] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_University]
GO
ALTER TABLE [dbo].[Province]  WITH CHECK ADD  CONSTRAINT [FK_CountryProvince] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Province] CHECK CONSTRAINT [FK_CountryProvince]
GO
ALTER TABLE [dbo].[TaskGrade]  WITH CHECK ADD  CONSTRAINT [FK_TaskGrade_Grade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grade] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskGrade] CHECK CONSTRAINT [FK_TaskGrade_Grade]
GO
ALTER TABLE [dbo].[TaskGrade]  WITH CHECK ADD  CONSTRAINT [FK_TaskGrade_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskGrade] CHECK CONSTRAINT [FK_TaskGrade_Task]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Task]
GO
ALTER TABLE [dbo].[TeamGradeDetail]  WITH CHECK ADD  CONSTRAINT [FK_TeamGradeDetail_GradeDetail1] FOREIGN KEY([GradeDetailId])
REFERENCES [dbo].[GradeDetail] ([Id])
GO
ALTER TABLE [dbo].[TeamGradeDetail] CHECK CONSTRAINT [FK_TeamGradeDetail_GradeDetail1]
GO
ALTER TABLE [dbo].[TeamGradeDetail]  WITH CHECK ADD  CONSTRAINT [FK_TeamGradeDetail_Team1] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
GO
ALTER TABLE [dbo].[TeamGradeDetail] CHECK CONSTRAINT [FK_TeamGradeDetail_Team1]
GO
ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD  CONSTRAINT [FK_TeamMember_AspNetUsers] FOREIGN KEY([MemberUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TeamMember] CHECK CONSTRAINT [FK_TeamMember_AspNetUsers]
GO
ALTER TABLE [dbo].[TeamMember]  WITH CHECK ADD  CONSTRAINT [FK_TeamMember_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TeamMember] CHECK CONSTRAINT [FK_TeamMember_Team]
GO
ALTER TABLE [dbo].[UserTask]  WITH CHECK ADD  CONSTRAINT [FK_UserTask_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTask] CHECK CONSTRAINT [FK_UserTask_AspNetUsers]
GO
ALTER TABLE [dbo].[UserTask]  WITH CHECK ADD  CONSTRAINT [FK_UserTask_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTask] CHECK CONSTRAINT [FK_UserTask_Task]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Allow to Show, 0 =  Don''t Show' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DietType', @level2type=N'COLUMN',@level2name=N'Display'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = First Page Image, 
1 = Left Sponsor Image,
2 = Right Sponsor Image' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Image', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = T-Shirt-Size, 1 = Jacket-Size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Size', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Grade"
            Begin Extent = 
               Top = 41
               Left = 74
               Bottom = 137
               Right = 244
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GradeDetail"
            Begin Extent = 
               Top = 7
               Left = 446
               Bottom = 158
               Right = 621
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGradeDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewGradeDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[61] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4[50] 3) )"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3) )"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[30] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4[60] 2) )"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4) )"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2) )"
      End
      ActivePaneConfig = 4
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Invoice"
            Begin Extent = 
               Top = 0
               Left = 892
               Bottom = 253
               Right = 1063
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PaymentRule"
            Begin Extent = 
               Top = 170
               Left = 665
               Bottom = 336
               Right = 856
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "InvoiceDetail"
            Begin Extent = 
               Top = 13
               Left = 402
               Bottom = 337
               Right = 619
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ViewTeam"
            Begin Extent = 
               Top = 0
               Left = 18
               Bottom = 515
               Right = 251
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 42
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Wi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewInvoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'dth = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 2385
         Alias = 2190
         Table = 1635
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewInvoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewInvoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[28] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[75] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4[60] 2) )"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2) )"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "P"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 410
               Right = 271
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 26
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1410
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewJudgeFullInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewJudgeFullInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[35] 4[45] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[62] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4[50] 3) )"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3) )"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[66] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4) )"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 4
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = -192
      End
      Begin Tables = 
         Begin Table = "Person"
            Begin Extent = 
               Top = 51
               Left = 821
               Bottom = 645
               Right = 1042
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 21
               Left = 504
               Bottom = 365
               Right = 728
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUserRoles"
            Begin Extent = 
               Top = 18
               Left = 260
               Bottom = 114
               Right = 430
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetRoles"
            Begin Extent = 
               Top = 45
               Left = 2
               Bottom = 154
               Right = 172
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DietType"
            Begin Extent = 
               Top = 95
               Left = 1221
               Bottom = 191
               Right = 1391
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Size"
            Begin Extent = 
               Top = 492
               Left = 286
               Bottom = 605
               Right = 456
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Size_1"
            Begin Extent = 
               Top = 289
               Left = 1138
               Bottom = 385
               Right = 1308
            End
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPersonInRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'     DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "University"
            Begin Extent = 
               Top = 409
               Left = 1114
               Bottom = 505
               Right = 1310
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 37
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 4200
         Alias = 1050
         Table = 1200
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 3990
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPersonInRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPersonInRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[38] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Grade"
            Begin Extent = 
               Top = 17
               Left = 846
               Bottom = 113
               Right = 1016
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Task"
            Begin Extent = 
               Top = 22
               Left = 71
               Bottom = 200
               Right = 241
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TaskGrade"
            Begin Extent = 
               Top = 0
               Left = 457
               Bottom = 113
               Right = 627
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1905
         Alias = 1470
         Table = 2505
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTask'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTask'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[18] 4[20] 2[46] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTaskFullInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTaskFullInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[45] 4[16] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[39] 4[54] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2[66] 3) )"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[75] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4[60] 2) )"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4) )"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2) )"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserTask"
            Begin Extent = 
               Top = 9
               Left = 21
               Bottom = 146
               Right = 191
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Task"
            Begin Extent = 
               Top = 18
               Left = 277
               Bottom = 178
               Right = 447
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Team"
            Begin Extent = 
               Top = 12
               Left = 562
               Bottom = 228
               Right = 732
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ViewTeam"
            Begin Extent = 
               Top = 18
               Left = 802
               Bottom = 506
               Right = 1035
            End
            DisplayFlags = 280
            TopColumn = 27
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 36
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
     ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTaskTeam'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'    Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 9210
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1425
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTaskTeam'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTaskTeam'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[27] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[48] 4[28] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4[50] 3) )"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[35] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4) )"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Team"
            Begin Extent = 
               Top = 43
               Left = 356
               Bottom = 220
               Right = 518
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Task"
            Begin Extent = 
               Top = 69
               Left = 25
               Bottom = 221
               Right = 195
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TeamMember"
            Begin Extent = 
               Top = 33
               Left = 664
               Bottom = 191
               Right = 837
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ViewPersonInRole"
            Begin Extent = 
               Top = 0
               Left = 976
               Bottom = 704
               Right = 1184
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 43
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 150' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeam'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'0
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3105
         Alias = 1380
         Table = 1635
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 3810
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeam'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeam'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[23] 4[10] 2[49] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2[66] 3) )"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[28] 4[46] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2) )"
      End
      ActivePaneConfig = 5
   End
   Begin DiagramPane = 
      PaneHidden = 
      Begin Origin = 
         Top = -288
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 54
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 135' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamFullInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'0
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamFullInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamFullInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[56] 4[21] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[39] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -282
      End
      Begin Tables = 
         Begin Table = "TeamGradeDetail"
            Begin Extent = 
               Top = 9
               Left = 339
               Bottom = 202
               Right = 509
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Team"
            Begin Extent = 
               Top = 9
               Left = 38
               Bottom = 251
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Grade"
            Begin Extent = 
               Top = 98
               Left = 919
               Bottom = 194
               Right = 1089
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "GradeDetail"
            Begin Extent = 
               Top = 23
               Left = 638
               Bottom = 181
               Right = 808
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1770
         ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamGradeDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Table = 1575
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamGradeDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamGradeDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[43] 4[47] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[60] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[53] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 9
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Team"
            Begin Extent = 
               Top = 18
               Left = 1018
               Bottom = 258
               Right = 1196
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Task"
            Begin Extent = 
               Top = 8
               Left = 661
               Bottom = 203
               Right = 831
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TaskGrade"
            Begin Extent = 
               Top = 28
               Left = 414
               Bottom = 151
               Right = 584
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ViewGradeDetail"
            Begin Extent = 
               Top = 78
               Left = 67
               Bottom = 258
               Right = 237
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserTask"
            Begin Extent = 
               Top = 171
               Left = 361
               Bottom = 284
               Right = 531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Wid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamGradeMetaData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'th = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1830
         Alias = 1470
         Table = 1515
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamGradeMetaData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamGradeMetaData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[29] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4) )"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Team"
            Begin Extent = 
               Top = 18
               Left = 317
               Bottom = 246
               Right = 480
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "TeamMember"
            Begin Extent = 
               Top = 19
               Left = 581
               Bottom = 174
               Right = 754
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ViewPersonInRole"
            Begin Extent = 
               Top = 0
               Left = 917
               Bottom = 719
               Right = 1216
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Task"
            Begin Extent = 
               Top = 23
               Left = 47
               Bottom = 154
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 47
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 15' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamMember'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'00
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3060
         Alias = 1380
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamMember'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewTeamMember'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[51] 4[10] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[75] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 9
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspNetUserRoles"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetRoles"
            Begin Extent = 
               Top = 163
               Left = 279
               Bottom = 259
               Right = 449
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 6
               Left = 635
               Bottom = 564
               Right = 859
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1050
         Table = 1575
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUserRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUserRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[52] 4[22] 2[7] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[38] 4[37] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[13] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserTask"
            Begin Extent = 
               Top = 0
               Left = 616
               Bottom = 114
               Right = 786
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Task"
            Begin Extent = 
               Top = 8
               Left = 918
               Bottom = 161
               Right = 1088
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ViewPersonInRole"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 749
               Right = 271
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1515
         Alias = 1050
         Table = 1635
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUserTask'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUserTask'
GO
