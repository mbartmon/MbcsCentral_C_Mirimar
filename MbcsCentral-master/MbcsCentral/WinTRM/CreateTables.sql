/****** Object:  Table [dbo].[tr_Accounts]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_Accounts](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](16) NULL,
	[account_type] [smallint] NULL DEFAULT ((0)),
	[permissions] [smallint] NULL DEFAULT ((0)),
	[password] [nvarchar](16) NULL,
	[name] [nvarchar](80) NULL,
	[email] [nvarchar](50) NULL,
	[sms] [nvarchar](80) NULL,
	[ip] [nvarchar](32) NULL,
	[port] [smallint] NULL,
	[parent_account] [int] NULL DEFAULT ((0)),
	[use_outlook] [bit] NULL DEFAULT ((0)),
	[outlook_folder] [nvarchar](255) NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_Accounts$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_BuildingLocation]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_BuildingLocation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](255) NULL,
 CONSTRAINT [tr_BuildingLocation$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_categories]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_categories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category] [nvarchar](80) NULL,
 CONSTRAINT [tr_categories$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_contents]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_contents](
	[content_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[description] [nvarchar](80) NULL,
	[content_type] [int] NULL,
	[content] [nvarchar](max) NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_contents$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[content_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_issues]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_issues](
	[issue_id] [int] IDENTITY(1,1) NOT NULL,
	[sequence_number] [int] NULL,
	[account_id] [int] NULL,
	[timestamp] [datetime2](0) NULL DEFAULT (getdate()),
	[note] [nvarchar](max) NULL,
	[recipient_ids] [nvarchar](255) NULL,
	[due_date] [datetime2](0) NULL,
	[notify_responsible] [bit] NULL DEFAULT ((0)),
	[priority] [smallint] NOT NULL DEFAULT ((0)),
	[status] [smallint] NULL DEFAULT ((0)),
	[subject] [nvarchar](80) NULL,
	[alert_mode] [smallint] NULL,
	[person_responsible] [int] NULL,
	[last_update] [datetime2](0) NULL DEFAULT (getdate()),
	[reported] [datetime2](0) NULL,
	[assigned] [datetime2](0) NULL,
	[vendor_id] [int] NULL,
	[location_id] [int] NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_issues$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[issue_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_IssueTenants]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_IssueTenants](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[issue_id] [int] NULL,
	[sequence_number] [int] NULL,
	[timestamp] [datetime2](0) NOT NULL DEFAULT (getdate()),
	[buildingNumber] [nvarchar](255) NULL,
	[tenantNumber] [nvarchar](255) NULL,
	[apartment] [nvarchar](255) NULL,
 CONSTRAINT [tr_IssueTenants$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_JobMaster]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_JobMaster](
	[jobId] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[jobName] [nvarchar](64) NULL,
	[status] [smallint] NULL,
	[listIds] [nvarchar](255) NULL,
	[contentId] [int] NULL,
	[deliveryTime] [datetime2](0) NULL,
	[created] [datetime2](0) NULL,
	[finished] [datetime2](0) NULL,
	[messageCount] [int] NULL,
	[smtpAcctId] [int] NULL,
	[msgSchedId] [int] NULL,
	[lastError] [nvarchar](255) NULL,
 CONSTRAINT [tr_JobMaster$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[jobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_LocalSMTP]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_LocalSMTP](
	[accountID] [int] NOT NULL,
	[UserName] [nvarchar](32) NULL,
	[Password] [nvarchar](32) NULL,
	[IncomingHost] [nvarchar](255) NULL,
	[OutgoingHost] [nvarchar](255) NULL,
	[IncomingPort] [int] NULL,
	[OutgoingPort] [int] NULL,
	[EmailAddress] [nvarchar](255) NULL,
	[UseInSSL] [bit] NULL DEFAULT ((0)),
	[UseInTSL] [bit] NULL DEFAULT ((0)),
	[UseOutSSL] [bit] NULL DEFAULT ((0)),
	[UseOutTSL] [bit] NULL DEFAULT ((0)),
	[AuthenticationReceive] [int] NULL,
	[Authentication] [int] NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_LocalSMTP$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[accountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_messages]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_messages](
	[message_id] [int] IDENTITY(1,1) NOT NULL,
	[issue_id] [int] NULL,
	[account_id] [int] NULL,
	[date_created] [datetime2](0) NULL,
	[direction] [int] NULL,
	[date_processed] [datetime2](0) NULL,
	[request_confirm] [bit] NULL,
	[confirm_received] [bit] NULL,
	[date_confirmed] [datetime2](0) NULL,
	[filename] [nvarchar](255) NULL,
	[read] [bit] NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_messages$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[message_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_MessageSchedule]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_MessageSchedule](
	[msg_sched_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL DEFAULT ((0)),
	[schedule_id] [int] NULL DEFAULT ((0)),
	[content_id] [int] NULL DEFAULT ((0)),
	[description] [nvarchar](255) NULL,
	[status] [int] NULL,
	[recipients] [nvarchar](max) NULL,
	[created] [datetime2](0) NULL DEFAULT (getdate()),
	[last_sent] [datetime2](0) NULL DEFAULT (NULL),
	[number_to_process] [int] NULL,
	[process_count] [int] NULL,
	[day_to_process] [int] NULL,
	[day_to_process_2] [int] NULL,
	[from] [nvarchar](80) NULL,
	[subject] [nvarchar](128) NULL,
	[frequency] [int] NULL,
	[start_date] [datetime2](0) NULL,
	[end_date] [datetime2](0) NULL,
	[no_end_date] [bit] NULL DEFAULT ((0)),
	[exception_override] [int] NULL,
	[smtp_acct_id] [int] NULL,
	[returnReceipt] [bit] NULL DEFAULT ((0)),
	[useWeekends] [bit] NULL DEFAULT ((0)),
	[useHolidays] [bit] NULL DEFAULT ((0)),
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_MessageSchedule$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[msg_sched_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_notes]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_notes](
	[note_id] [int] IDENTITY(1,1) NOT NULL,
	[issue_id] [int] NULL,
	[account_id] [int] NULL,
	[note] [nvarchar](max) NULL,
	[timestamp] [datetime2](0) NULL DEFAULT (getdate()),
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_notes$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[note_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_options]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_options](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[retryQueueAtStartup] [bit] NULL DEFAULT ((0)),
	[autoDeleteMessages] [bit] NULL DEFAULT ((0)),
	[autoDeleteAge] [int] NULL,
	[messageCreationCheckInterval] [int] NULL,
	[companyName] [nvarchar](128) NULL,
	[emailSignature] [nvarchar](255) NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_options$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_RecipientMaster]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_RecipientMaster](
	[listId] [int] IDENTITY(1,1) NOT NULL,
	[accountId] [int] NULL,
	[listName] [nvarchar](32) NULL,
	[companyIds] [nvarchar](255) NULL,
	[buildingIds] [nvarchar](255) NULL,
	[tenantTypes] [nvarchar](255) NULL,
	[ownerFilter] [bit] NULL DEFAULT ((0)),
	[buildingFilter] [bit] NULL DEFAULT ((0)),
	[typeFilter] [bit] NULL DEFAULT ((0)),
	[arrearsFilter] [bit] NULL DEFAULT ((0)),
	[arrearsAmount] [money] NULL,
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_RecipientMaster$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[listId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_Recipients]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_Recipients](
	[recipient_Id] [int] IDENTITY(1,1) NOT NULL,
	[list_Id] [int] NULL,
	[first_name] [nvarchar](32) NULL,
	[last_name] [nvarchar](32) NULL,
	[email_address] [nvarchar](128) NULL,
	[address_one] [nvarchar](32) NULL,
	[address_two] [nvarchar](32) NULL,
	[address_three] [nvarchar](32) NULL,
	[city] [nvarchar](32) NULL,
	[state] [nvarchar](32) NULL,
	[zip] [nvarchar](10) NULL,
	[phone] [nvarchar](32) NULL,
	[date_of_birth] [datetime2](0) NULL,
 CONSTRAINT [tr_Recipients$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[recipient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_schedules]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_schedules](
	[schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[content_id] [int] NULL,
	[schedule_name] [nvarchar](128) NULL,
	[start_date] [datetime2](0) NULL,
	[end_date] [datetime2](0) NULL,
	[number_to_process] [int] NULL DEFAULT ((0)),
	[last_processed] [datetime2](0) NULL,
	[process_count] [int] NULL DEFAULT ((0)),
	[active] [bit] NULL DEFAULT ((0)),
	[frequency] [int] NULL,
	[day_to_process] [int] NULL DEFAULT ((0)),
	[day_to_process_2] [int] NULL DEFAULT ((0)),
	[no_end_date] [bit] NULL DEFAULT ((0)),
	[useWeekends] [bit] NULL DEFAULT ((0)),
	[useHolidays] [bit] NULL DEFAULT ((0)),
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_schedules$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_SmtpConfig]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_SmtpConfig](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](32) NULL,
	[emailAddress] [nvarchar](64) NULL,
	[userName] [nvarchar](64) NULL,
	[password] [nvarchar](32) NULL,
	[server] [nvarchar](255) NULL,
	[port] [int] NULL,
	[SSL] [bit] NULL DEFAULT ((0)),
	[TSL] [bit] NULL DEFAULT ((0)),
	[authentication] [int] NULL,
	[in_userName] [nvarchar](64) NULL,
	[in_password] [nvarchar](32) NULL,
	[in_server] [nvarchar](255) NULL,
	[in_port] [int] NULL,
	[in_SSL] [bit] NULL DEFAULT ((0)),
	[in_TSL] [bit] NULL DEFAULT ((0)),
	[in_authentication] [int] NULL,
	[defaultAccount] [bit] NULL DEFAULT ((0)),
	[SSMA_TimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [tr_SmtpConfig$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tr_Vendors]    Script Date: 3/8/2016 09:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tr_Vendors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [nvarchar](64) NULL,
	[Address1] [nvarchar](64) NULL,
	[Address2] [nvarchar](64) NULL,
	[City] [nvarchar](32) NULL,
	[State] [nvarchar](2) NULL,
	[Zip] [nvarchar](10) NULL,
 CONSTRAINT [tr_Vendors$PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tr_Accounts] ON 

GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (1, N'admin', 0, 3, N'abc123', N'Administrator', N'mbartmon@gmail.com', N'', N'', 0, 0, 0, NULL)
GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (2, N'ChadF', 0, 3, N'106cmfspring', N'Chad Feinstein', N'chadf@nyc.rr.com', N'9734172315@messaging.nextel.com', N'', 0, 0, 0, NULL)
GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (3, N'Barry', 0, 3, N'271bmfspring', N'Barry M. Fishman', N'barryf@nyc.rr.com', N'', N'', 0, 0, 0, NULL)
GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (4, N'Marisol', 0, 1, N'marisol', N'Marisol', N'', N'', N'', 0, 0, 0, NULL)
GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (5, N'Nathan', 0, 3, N'3nlfspring', N'Nathan Fishman', N'nfishman@nyc.rr.com', N'9734172438@messaging.nextel.com', N'', 0, 0, 0, NULL)
GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (6, N'felice', 0, 1, N'20flcspring', N'Felice L Char', N'', N'', N'', 0, 0, 0, NULL)
GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (7, N'ina', 0, 1, N'900iafspring', N'Ina A. Feinstein', N'', N'', N'', 0, 0, 0, NULL)
GO
INSERT [dbo].[tr_Accounts] ([account_id], [login], [account_type], [permissions], [password], [name], [email], [sms], [ip], [port], [parent_account], [use_outlook], [outlook_folder]) VALUES (8, N'mike', 0, 3, N'vietnam65', N'Mike Bartmon', N'mike@bartmon.com', N'', N'', 0, 0, 0, N'')
GO
SET IDENTITY_INSERT [dbo].[tr_Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_BuildingLocation] ON 

GO
INSERT [dbo].[tr_BuildingLocation] ([ID], [Location]) VALUES (1, N'Lobby')
GO
INSERT [dbo].[tr_BuildingLocation] ([ID], [Location]) VALUES (3, N'Roof')
GO
INSERT [dbo].[tr_BuildingLocation] ([ID], [Location]) VALUES (4, N'Cellar')
GO
INSERT [dbo].[tr_BuildingLocation] ([ID], [Location]) VALUES (5, N'Boiler Room')
GO
INSERT [dbo].[tr_BuildingLocation] ([ID], [Location]) VALUES (6, N'Elevator')
GO
INSERT [dbo].[tr_BuildingLocation] ([ID], [Location]) VALUES (7, N'Apartment')
GO
INSERT [dbo].[tr_BuildingLocation] ([ID], [Location]) VALUES (8, N'')
GO
SET IDENTITY_INSERT [dbo].[tr_BuildingLocation] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_categories] ON 

GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (1, N'PAINTING - COMPLETE')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (2, N'PAINTING - REPAIRS')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (3, N'LEGAL - DISPOSSESS')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (4, N'LEGAL - HOLDOVER')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (5, N'LEGAL - OTHER')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (6, N'REQUEST TO MOVE TO ANOTHER APT')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (7, N'REPAIRS - APT')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (8, N'REPAIRS - BUILDING')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (9, N'NOISE COMPLAINT')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (10, N'MOVING OUT')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (11, N'LOOKING TO MOVE')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (12, N'ILLEGAL TENANT')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (13, N'TENANT NEGLIGENCE')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (14, N'DOG SITUATION')
GO
INSERT [dbo].[tr_categories] ([category_id], [category]) VALUES (15, N'ILLEGAL EQUIPMENT IN APT')
GO
SET IDENTITY_INSERT [dbo].[tr_categories] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_contents] ON 

GO
INSERT [dbo].[tr_contents] ([content_id], [account_id], [description], [content_type], [content]) VALUES (1, 2, N'Test Message', 0, N'This is a test')
GO
INSERT [dbo].[tr_contents] ([content_id], [account_id], [description], [content_type], [content]) VALUES (2, 8, N'First Test for Job Creation', 0, N'Dear Test Subject,

Test message 1

Sincerely,
Mike Bartmon')
GO
INSERT [dbo].[tr_contents] ([content_id], [account_id], [description], [content_type], [content]) VALUES (3, 8, N'Test Message 2', 0, N'Dear client

Test message 2')
GO
SET IDENTITY_INSERT [dbo].[tr_contents] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_issues] ON 

GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (1, 0, 2, CAST(N'2011-07-18 17:07:26.0000000' AS DateTime2), N'', N'2', CAST(N'2011-07-18 17:07:52.0000000' AS DateTime2), 0, 0, -1, N'test', 0, -1, CAST(N'2011-07-18 17:07:26.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (2, 0, 1, CAST(N'2011-07-19 09:07:26.0000000' AS DateTime2), N'', N'', CAST(N'2011-07-19 09:07:51.0000000' AS DateTime2), 0, 0, -1, N'Robbery', 0, 2, CAST(N'2011-07-19 09:07:26.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (3, 0, 1, CAST(N'2011-07-19 20:07:03.0000000' AS DateTime2), N'', N'1', CAST(N'2011-07-19 20:07:31.0000000' AS DateTime2), 0, 0, -1, N'', 0, -1, CAST(N'2011-07-19 20:07:03.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (6, 0, 1, CAST(N'2011-07-27 16:07:58.0000000' AS DateTime2), N'', N'', CAST(N'2011-07-27 16:07:29.0000000' AS DateTime2), 0, 0, -1, N'Repairs', 0, 2, CAST(N'2011-07-27 16:07:11.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (8, 0, 3, CAST(N'2011-07-27 17:07:12.0000000' AS DateTime2), N'REQUEST TO MOVE TO ANOTHER APT', N'1,2', CAST(N'2011-07-27 17:07:45.0000000' AS DateTime2), 0, 0, 0, N'DENIED REQUEST TO MOVE TO ANOTHER APT', 0, 2, CAST(N'2011-08-09 11:08:38.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (11, 0, 2, CAST(N'2011-08-19 12:08:06.0000000' AS DateTime2), N'LEGAL - OTHER', N'', CAST(N'2011-08-19 12:08:25.0000000' AS DateTime2), 0, 0, -1, N'ILLEGAL TENANT', 0, 2, CAST(N'2011-08-19 12:08:57.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (12, 0, 2, CAST(N'2011-08-25 12:08:42.0000000' AS DateTime2), N'LOOKING TO MOVE', N'', CAST(N'2011-08-25 12:08:00.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-08-25 12:08:42.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (13, 0, 2, CAST(N'2011-08-26 16:08:01.0000000' AS DateTime2), N'REPAIRS - APT', N'2', CAST(N'2011-08-26 16:08:41.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-12 15:10:35.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (14, 0, 2, CAST(N'2011-08-26 16:08:34.0000000' AS DateTime2), N'PAINTING - REPAIRS', N'', CAST(N'2011-08-26 16:08:18.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-08-26 16:08:34.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (16, 0, 2, CAST(N'2011-09-02 10:09:59.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-09-02 10:09:12.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-09-02 10:09:35.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (17, 0, 2, CAST(N'2011-09-02 10:09:50.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-09-02 10:09:33.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-09-02 10:09:50.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (18, 0, 2, CAST(N'2011-09-02 10:09:05.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-09-02 10:09:40.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-09-02 10:09:05.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (20, 0, 2, CAST(N'2011-09-27 09:09:02.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-09-27 09:09:45.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-09-27 09:09:08.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (21, 0, 2, CAST(N'2011-09-27 17:09:05.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-09-27 17:09:46.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-09-27 17:09:41.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (22, 0, 2, CAST(N'2011-09-27 17:09:16.0000000' AS DateTime2), N'LOOKING TO MOVE', N'', CAST(N'2011-09-27 17:09:04.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-09-27 17:09:33.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (23, 0, 2, CAST(N'2011-09-27 17:09:06.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-09-27 17:09:50.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-09-27 17:09:06.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (25, 0, 2, CAST(N'2011-10-04 10:10:46.0000000' AS DateTime2), N'NOISE COMPLAINT', N'', CAST(N'2011-10-04 10:10:19.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-04 10:10:10.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (26, 0, 2, CAST(N'2011-10-06 13:10:14.0000000' AS DateTime2), N'ILLEGAL TENANT', N'', CAST(N'2011-10-06 13:10:03.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-06 13:10:14.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (31, 0, 2, CAST(N'2011-10-11 11:10:43.0000000' AS DateTime2), N'REQUEST TO MOVE TO ANOTHER APT', N'', CAST(N'2011-10-11 11:10:14.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-11 11:10:43.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (32, 0, 2, CAST(N'2011-10-11 12:10:02.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-10-11 12:10:48.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-11 12:10:02.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (33, 0, 2, CAST(N'2011-10-13 16:10:39.0000000' AS DateTime2), N' ', N'', CAST(N'2011-10-13 16:10:23.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-13 16:10:39.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (35, 0, 2, CAST(N'2011-10-20 12:10:17.0000000' AS DateTime2), N'LOOKING TO MOVE', N'', CAST(N'2011-10-20 12:10:56.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-20 12:10:17.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (36, 0, 2, CAST(N'2011-10-24 10:10:07.0000000' AS DateTime2), N'NOISE COMPLAINT', N'', CAST(N'2011-10-24 10:10:51.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-25 11:10:02.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (37, 0, 2, CAST(N'2011-10-26 11:10:41.0000000' AS DateTime2), N' ', N'', CAST(N'2011-10-26 11:10:28.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-26 11:10:41.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (38, 0, 2, CAST(N'2011-10-28 10:10:37.0000000' AS DateTime2), N'ILLEGAL TENANT', N'', CAST(N'2011-10-28 10:10:17.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-10-28 10:10:37.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (39, 0, 2, CAST(N'2011-11-07 09:11:56.0000000' AS DateTime2), N' ', N'', CAST(N'2011-11-07 09:11:14.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-11-07 09:11:56.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (40, 0, 2, CAST(N'2011-11-08 11:11:04.0000000' AS DateTime2), N'TENANT NEGLIGENCE', N'', CAST(N'2011-11-08 11:11:56.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-11-08 11:11:04.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (41, 0, 2, CAST(N'2011-11-15 12:11:22.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2011-11-15 12:11:03.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-11-15 12:11:22.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (43, 0, 2, CAST(N'2011-11-21 09:11:39.0000000' AS DateTime2), N' ', N'', CAST(N'2011-11-21 09:11:39.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-11-21 09:11:47.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (44, 0, 2, CAST(N'2011-11-21 10:11:19.0000000' AS DateTime2), N'LOOKING TO MOVE', N'', CAST(N'2011-11-21 10:11:03.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-11-21 10:11:19.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (45, 0, 2, CAST(N'2011-12-13 16:12:50.0000000' AS DateTime2), N'NOISE COMPLAINT', N'', CAST(N'2011-12-13 16:12:31.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-12-13 16:12:50.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (46, 0, 2, CAST(N'2011-12-22 09:12:46.0000000' AS DateTime2), N'NOISE COMPLAINT', N'', CAST(N'2011-12-22 09:12:29.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2011-12-22 09:12:49.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (47, 0, 2, CAST(N'2012-01-25 14:01:23.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2012-01-25 14:01:01.0000000' AS DateTime2), 0, 0, -1, N'', 0, 2, CAST(N'2012-01-25 14:01:23.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (48, 0, 2, CAST(N'2012-01-25 14:01:02.0000000' AS DateTime2), N'REPAIRS - APT', N'2', CAST(N'2012-01-25 14:01:38.0000000' AS DateTime2), 0, 0, -1, N'TEST', 0, 2, CAST(N'2012-01-25 14:01:02.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (50, 0, 2, CAST(N'2012-01-30 11:01:12.0000000' AS DateTime2), N'LOOKING TO MOVE', N'', CAST(N'2012-01-30 11:01:55.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-01-30 11:01:58.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (51, 0, 2, CAST(N'2012-01-31 16:01:08.0000000' AS DateTime2), N'LOOKING TO MOVE', N'', CAST(N'2012-01-31 16:01:45.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-01-31 16:01:32.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (52, 0, 2, CAST(N'2012-01-31 16:01:17.0000000' AS DateTime2), N'NOISE COMPLAINT', N'', CAST(N'2012-01-31 16:01:54.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-01-31 16:01:17.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (53, 0, 2, CAST(N'2012-01-31 17:01:59.0000000' AS DateTime2), N'LOOKING TO MOVE', N'', CAST(N'2012-01-31 17:01:45.0000000' AS DateTime2), 0, 0, 0, N'', 0, 3, CAST(N'2012-01-31 17:01:16.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (54, 0, 2, CAST(N'2012-02-06 10:02:11.0000000' AS DateTime2), N'NOISE COMPLAINT', N'', CAST(N'2012-02-06 10:02:50.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-02-06 10:02:11.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (55, 0, 2, CAST(N'2012-02-06 17:02:40.0000000' AS DateTime2), N'DOG SITUATION', N'', CAST(N'2012-02-06 17:02:19.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-02-06 17:02:40.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (56, 0, 2, CAST(N'2012-02-23 09:02:38.0000000' AS DateTime2), N'TENANT NEGLIGENCE', N'', CAST(N'2012-02-23 09:02:18.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-02-23 09:02:38.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (57, 0, 2, CAST(N'2012-03-02 10:03:15.0000000' AS DateTime2), N'ILLEGAL EQUIPMENT IN APT', N'', CAST(N'2012-03-02 10:03:32.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-03-02 10:03:15.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (58, 0, 2, CAST(N'2012-03-06 15:03:13.0000000' AS DateTime2), N'NOISE COMPLAINT', N'', CAST(N'2012-03-06 15:03:52.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-03-06 15:03:13.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (59, 0, 2, CAST(N'2012-03-06 15:03:10.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2012-03-06 15:03:59.0000000' AS DateTime2), 0, 0, 0, N'', 0, 2, CAST(N'2012-03-06 15:03:10.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (60, 0, 2, CAST(N'2012-03-06 17:03:53.0000000' AS DateTime2), N'REPAIRS - APT', N'', CAST(N'2012-03-06 17:03:18.0000000' AS DateTime2), 0, 0, 0, N'', 4, 2, CAST(N'2012-03-06 17:03:53.0000000' AS DateTime2), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (61, 0, 2, CAST(N'2012-03-13 10:03:02.0000000' AS DateTime2), N'NOISE COMPLAINT', N'1,8', CAST(N'2012-08-01 10:08:04.0000000' AS DateTime2), 0, 0, 0, N'', 42, 8, CAST(N'2012-07-24 11:07:22.0000000' AS DateTime2), CAST(N'2001-01-01 00:01:00.0000000' AS DateTime2), CAST(N'2001-01-01 00:01:00.0000000' AS DateTime2), 0, 0)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (62, 0, 2, CAST(N'2012-03-21 20:03:35.0000000' AS DateTime2), N'ILLEGAL EQUIPMENT IN APT', N'', CAST(N'2012-03-21 20:03:51.0000000' AS DateTime2), 0, 0, 0, N'Radio Transmitter', 0, -1, CAST(N'2012-03-27 10:03:38.0000000' AS DateTime2), CAST(N'2012-03-21 20:03:51.0000000' AS DateTime2), CAST(N'2012-03-21 20:03:51.0000000' AS DateTime2), 1, 3)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (63, 0, 8, CAST(N'2012-09-16 11:09:47.0000000' AS DateTime2), N'ILLEGAL TENANT', N'8', CAST(N'2999-12-31 00:12:00.0000000' AS DateTime2), 0, 0, 0, N'Illegal Tenant', 42, 8, CAST(N'2016-03-07 15:03:02.0000000' AS DateTime2), CAST(N'2001-01-01 00:01:00.0000000' AS DateTime2), CAST(N'2001-01-01 00:01:00.0000000' AS DateTime2), 0, 0)
GO
INSERT [dbo].[tr_issues] ([issue_id], [sequence_number], [account_id], [timestamp], [note], [recipient_ids], [due_date], [notify_responsible], [priority], [status], [subject], [alert_mode], [person_responsible], [last_update], [reported], [assigned], [vendor_id], [location_id]) VALUES (64, 0, 8, CAST(N'2012-09-16 11:09:47.0000000' AS DateTime2), N'REPAIRS - BUILDING', N'1,8', CAST(N'2999-12-31 00:12:00.0000000' AS DateTime2), 0, 0, 0, N'Roof Vent', 42, -1, CAST(N'2012-09-16 14:09:27.0000000' AS DateTime2), CAST(N'2012-09-16 11:09:12.0000000' AS DateTime2), CAST(N'2012-09-16 11:09:12.0000000' AS DateTime2), 1, 3)
GO
SET IDENTITY_INSERT [dbo].[tr_issues] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_IssueTenants] ON 

GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (1, 4, 2769, CAST(N'2011-07-25 23:07:06.0000000' AS DateTime2), N'072', N'B5B ', N'5B  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (2, 5, 1340, CAST(N'2011-07-26 08:07:55.0000000' AS DateTime2), N'072', N'F1F ', N'1F  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (3, 6, 187, CAST(N'2011-07-27 16:07:58.0000000' AS DateTime2), N'207', N'A4A ', N'4A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (4, 7, 187, CAST(N'2011-07-27 16:07:43.0000000' AS DateTime2), N'207', N'A4A ', N'4A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (5, 8, 1958, CAST(N'2011-07-27 17:07:12.0000000' AS DateTime2), N'207', N'A6A ', N'6A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (6, 9, 2690, CAST(N'2011-07-29 10:07:58.0000000' AS DateTime2), N'207', N'E4E ', N'4E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (7, 10, 1391, CAST(N'2011-07-29 10:07:03.0000000' AS DateTime2), N'207', N'A2A ', N'2A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (8, 11, 653, CAST(N'2011-08-19 12:08:07.0000000' AS DateTime2), N'609', N'C4C ', N'4C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (9, 12, 1670, CAST(N'2011-08-25 12:08:43.0000000' AS DateTime2), N'830', N'E4E ', N'4E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (10, 13, 1001, CAST(N'2011-08-26 16:08:01.0000000' AS DateTime2), N'194', N'E1E ', N'1E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (11, 14, 1001, CAST(N'2011-08-26 16:08:34.0000000' AS DateTime2), N'194', N'E1E ', N'1E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (12, 15, 3149, CAST(N'2011-08-26 16:08:30.0000000' AS DateTime2), N'297', N'323 ', N'203 ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (13, 16, 3169, CAST(N'2011-09-02 10:09:59.0000000' AS DateTime2), N'185', N'C5C ', N'5C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (14, 17, 2477, CAST(N'2011-09-02 10:09:50.0000000' AS DateTime2), N'441', N'G3G ', N'3G  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (15, 18, 1008, CAST(N'2011-09-02 10:09:05.0000000' AS DateTime2), N'204', N'C5C ', N'5C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (16, 19, 3037, CAST(N'2011-09-02 11:09:46.0000000' AS DateTime2), N'185', N'K6K ', N'6K  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (17, 20, 755, CAST(N'2011-09-27 09:09:02.0000000' AS DateTime2), N'599', N'555 ', N'55  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (18, 21, 1907, CAST(N'2011-09-27 17:09:06.0000000' AS DateTime2), N'194', N'A2A ', N'2A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (19, 22, 1336, CAST(N'2011-09-27 17:09:17.0000000' AS DateTime2), N'072', N'B1B ', N'1B  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (20, 23, 2313, CAST(N'2011-09-27 17:09:07.0000000' AS DateTime2), N'072', N'G2G ', N'2G  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (21, 24, 1165, CAST(N'2011-09-30 11:09:07.0000000' AS DateTime2), N'599', N'363 ', N'63  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (22, 25, 3197, CAST(N'2011-10-04 10:10:46.0000000' AS DateTime2), N'072', N'E6E ', N'6E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (23, 25, 3307, CAST(N'2011-10-04 10:10:47.0000000' AS DateTime2), N'072', N'E5E ', N'5E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (24, 26, 1683, CAST(N'2011-10-06 13:10:14.0000000' AS DateTime2), N'830', N'G5G ', N'5G  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (25, 27, 2045, CAST(N'2011-10-06 15:10:13.0000000' AS DateTime2), N'204', N'E5E ', N'5E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (26, 28, 2045, CAST(N'2011-10-06 15:10:30.0000000' AS DateTime2), N'204', N'E5E ', N'5E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (27, 29, 2045, CAST(N'2011-10-07 11:10:01.0000000' AS DateTime2), N'204', N'E5E ', N'5E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (28, 30, 2045, CAST(N'2011-10-07 11:10:20.0000000' AS DateTime2), N'204', N'E5E ', N'5E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (29, 31, 3100, CAST(N'2011-10-11 11:10:43.0000000' AS DateTime2), N'358', N'323 ', N'23  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (30, 32, 1937, CAST(N'2011-10-11 12:10:03.0000000' AS DateTime2), N'194', N'00A ', N'A   ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (31, 33, 1955, CAST(N'2011-10-13 16:10:39.0000000' AS DateTime2), N'342', N'C2C ', N'2C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (32, 34, 1336, CAST(N'2011-10-19 10:10:06.0000000' AS DateTime2), N'072', N'B1B ', N'1B  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (33, 35, 44, CAST(N'2011-10-20 12:10:18.0000000' AS DateTime2), N'185', N'C1C ', N'1C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (34, 36, 3242, CAST(N'2011-10-24 10:10:08.0000000' AS DateTime2), N'442', N'B5B ', N'5B  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (35, 37, 1206, CAST(N'2011-10-26 11:10:41.0000000' AS DateTime2), N'185', N'A6A ', N'6A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (36, 38, 2045, CAST(N'2011-10-28 10:10:37.0000000' AS DateTime2), N'204', N'E5E ', N'5E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (37, 39, 2781, CAST(N'2011-11-07 09:11:56.0000000' AS DateTime2), N'194', N'F1F ', N'1F  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (38, 40, 118, CAST(N'2011-11-08 11:11:05.0000000' AS DateTime2), N'194', N'C6C ', N'6C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (39, 41, 1336, CAST(N'2011-11-15 12:11:23.0000000' AS DateTime2), N'072', N'B1B ', N'1B  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (40, 42, 1381, CAST(N'2011-11-15 12:11:45.0000000' AS DateTime2), N'072', N'AB2 ', N'2AB ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (41, 43, 1958, CAST(N'2011-11-21 09:11:39.0000000' AS DateTime2), N'207', N'A6A ', N'6A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (42, 44, 2681, CAST(N'2011-11-21 10:11:19.0000000' AS DateTime2), N'185', N'G6G ', N'6G  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (43, 45, 2985, CAST(N'2011-12-13 16:12:50.0000000' AS DateTime2), N'243', N'C2C ', N'2C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (44, 46, 3319, CAST(N'2011-12-22 09:12:47.0000000' AS DateTime2), N'297', N'444 ', N'404 ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (45, 49, 2785, CAST(N'2012-01-30 11:01:48.0000000' AS DateTime2), N'442', N'J5J ', N'5J  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (46, 50, 2785, CAST(N'2012-01-30 11:01:12.0000000' AS DateTime2), N'442', N'J5J ', N'5J  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (47, 51, 2422, CAST(N'2012-01-31 16:01:08.0000000' AS DateTime2), N'297', N'353 ', N'503 ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (48, 52, 2901, CAST(N'2012-01-31 16:01:17.0000000' AS DateTime2), N'830', N'F6F ', N'6F  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (49, 53, 2970, CAST(N'2012-01-31 17:01:00.0000000' AS DateTime2), N'204', N'G4G ', N'4G  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (50, 54, 3172, CAST(N'2012-02-06 10:02:12.0000000' AS DateTime2), N'830', N'C6C ', N'6C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (51, 55, 1581, CAST(N'2012-02-06 17:02:40.0000000' AS DateTime2), N'342', N'E2E ', N'2E  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (52, 56, 114, CAST(N'2012-02-23 09:02:39.0000000' AS DateTime2), N'194', N'C2C ', N'2C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (53, 57, 353, CAST(N'2012-03-02 10:03:16.0000000' AS DateTime2), N'360', N'141 ', N'41  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (54, 58, 2665, CAST(N'2012-03-06 15:03:14.0000000' AS DateTime2), N'600', N'C2C ', N'2C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (55, 59, 2889, CAST(N'2012-03-06 15:03:11.0000000' AS DateTime2), N'072', N'A1A ', N'1A  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (56, 60, 200, CAST(N'2012-03-06 17:03:53.0000000' AS DateTime2), N'207', N'C6C ', N'6C  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (57, 61, 2910, CAST(N'2012-03-13 10:03:03.0000000' AS DateTime2), N'441', N'G1G ', N'1G  ')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (58, 62, -1, CAST(N'2012-03-21 20:03:36.0000000' AS DateTime2), N'185', N'', N'')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (59, 62, -1, CAST(N'2012-03-26 16:03:04.0000000' AS DateTime2), N'194', N'', N'')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (61, 64, -1, CAST(N'2012-09-16 11:09:47.0000000' AS DateTime2), N'340', N'', N'')
GO
INSERT [dbo].[tr_IssueTenants] ([ID], [issue_id], [sequence_number], [timestamp], [buildingNumber], [tenantNumber], [apartment]) VALUES (62, 63, 620, CAST(N'2016-03-07 15:03:02.0000000' AS DateTime2), N'600', N'F2F ', N'2F  ')
GO
SET IDENTITY_INSERT [dbo].[tr_IssueTenants] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_JobMaster] ON 

GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (232, 8, N'Daily at 12:05', 4, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-16 21:58:44.0000000' AS DateTime2), CAST(N'2012-11-26 12:18:06.0000000' AS DateTime2), 2, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (233, 8, N'Daily at 12:30', 4, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-16 21:58:44.0000000' AS DateTime2), CAST(N'2012-11-26 12:33:12.0000000' AS DateTime2), 2, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (234, 8, N'Daily at 9:00', 4, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-16 21:58:44.0000000' AS DateTime2), CAST(N'2012-11-26 10:47:59.0000000' AS DateTime2), 2, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (235, 8, N'Weekly Wednesday', 4, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-16 21:58:44.0000000' AS DateTime2), CAST(N'2012-11-21 08:11:03.0000000' AS DateTime2), 2, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (236, 8, N'Weekly Thursday', 4, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-16 21:58:44.0000000' AS DateTime2), CAST(N'2012-11-22 08:11:28.0000000' AS DateTime2), 2, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (237, 8, N'Weekly Friday', 4, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-16 21:58:44.0000000' AS DateTime2), CAST(N'2012-11-23 17:10:56.0000000' AS DateTime2), 2, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (238, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-16 21:58:45.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'Last Error Message Test')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (239, 8, N'Daily at 12:05', 4, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-27 14:38:25.0000000' AS DateTime2), CAST(N'2012-11-29 12:05:26.0000000' AS DateTime2), 2, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (240, 8, N'Daily at 12:30', 4, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-27 14:38:26.0000000' AS DateTime2), CAST(N'2012-11-29 12:35:37.0000000' AS DateTime2), 2, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (241, 8, N'Daily at 9:00', 4, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-27 14:38:26.0000000' AS DateTime2), CAST(N'2012-11-30 09:05:52.0000000' AS DateTime2), 2, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (242, 8, N'Weekly Wednesday', 4, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-27 14:38:26.0000000' AS DateTime2), CAST(N'2012-11-27 14:49:02.0000000' AS DateTime2), 2, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (243, 8, N'Weekly Thursday', 4, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-27 14:38:26.0000000' AS DateTime2), CAST(N'2012-11-29 08:05:11.0000000' AS DateTime2), 2, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (244, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-27 14:38:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (245, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-11-27 14:38:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (246, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:10:21.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (247, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:10:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (248, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:10:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (249, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:10:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (250, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:10:23.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (251, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:10:23.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (252, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:10:23.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (253, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:25:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (254, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:25:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (255, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:25:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (256, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:25:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (257, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:25:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (258, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:25:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (259, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-06 17:25:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (260, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:14:25.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (261, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:14:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (262, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:14:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (263, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:14:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (264, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:14:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (265, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:14:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (266, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:14:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (267, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:31:59.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (268, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-07 15:36:51.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (269, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 10:46:01.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (270, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:09:25.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (271, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:29:35.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (272, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:29:35.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (273, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:29:35.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (274, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:29:35.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (275, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:29:35.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (276, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:29:35.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (277, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:34:23.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (278, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:34:47.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (279, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:34:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (280, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:34:52.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (281, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:34:54.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (282, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:34:56.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (283, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 11:34:58.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (284, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 13:11:50.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (285, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 13:24:47.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (286, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 13:29:52.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (287, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 13:58:28.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (288, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 14:18:39.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (289, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 15:05:36.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (290, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 15:12:38.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (291, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 15:21:39.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (292, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 17:31:17.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (293, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 17:59:53.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (294, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 18:08:08.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (295, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 18:19:47.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (296, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 19:02:42.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (297, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 19:14:33.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (298, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 19:16:03.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (299, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 19:34:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (300, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 20:15:21.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (301, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 20:44:20.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (302, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 20:47:03.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (303, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 20:47:16.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (304, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 20:47:16.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (305, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 20:47:16.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (306, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:04:34.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (307, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:04:36.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (308, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:04:36.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (309, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:04:36.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (310, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:04:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (311, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:04:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (312, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:04:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (313, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:35:29.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (314, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:35:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (315, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:35:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (316, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:35:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (317, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:35:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (318, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:35:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (319, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:35:37.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (320, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:43:17.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (321, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:43:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (322, 8, N'Daily at 9:00', 1, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:43:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (323, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:43:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (324, 8, N'Weekly Thursday', 1, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:43:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (325, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:43:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (326, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:43:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (327, 8, N'Daily at 12:05', 4, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:54:21.0000000' AS DateTime2), CAST(N'2012-12-08 22:49:38.0000000' AS DateTime2), 509, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (328, 8, N'Daily at 12:30', 4, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:54:26.0000000' AS DateTime2), CAST(N'2012-12-08 23:02:04.0000000' AS DateTime2), 509, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (329, 8, N'Daily at 9:00', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:54:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (330, 8, N'Weekly Wednesday', 2, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:54:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (331, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:54:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (332, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:54:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (333, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-08 21:54:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (334, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-10 09:57:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (335, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-10 09:57:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (336, 8, N'Daily at 9:00', 5, N'4', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-10 09:57:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'NO JOB RECIPIENTS FOUND')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (337, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-10 09:57:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (338, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-10 09:57:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (339, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-10 09:57:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (340, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-10 09:57:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (341, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 10:33:25.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (342, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 10:33:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (343, 8, N'Daily at 9:00', 5, N'4', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 10:33:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'NO JOB RECIPIENTS FOUND')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (344, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 10:33:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (345, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 10:33:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (346, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 10:33:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (347, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 10:33:26.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (348, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:14:48.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (349, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:14:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (350, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:14:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (351, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:14:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (352, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:14:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (353, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:14:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (354, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:14:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (355, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:29:13.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (356, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:29:14.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (357, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:29:14.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (358, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:29:14.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (359, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:29:14.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (360, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:29:15.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (361, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:29:15.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (362, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:41:27.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (363, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:41:28.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (364, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:41:28.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (365, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:41:28.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (366, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:41:28.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (367, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:41:29.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (368, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:41:29.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (369, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:48:09.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (370, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:48:10.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (371, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:48:10.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (372, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:48:10.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (373, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:48:10.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (374, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:48:10.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (375, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 11:48:10.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (376, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 12:02:40.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (377, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 12:02:41.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (378, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 12:02:41.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (379, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 12:02:41.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (380, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 12:02:41.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (381, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 12:02:41.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (382, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 12:02:41.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (383, 8, N'Daily at 12:05', 5, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 13:29:19.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'NO JOB RECIPIENTS FOUND')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (384, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 13:29:20.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (385, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 13:29:20.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (386, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 13:29:20.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (387, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 13:29:20.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (388, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 13:29:20.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (389, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 13:29:20.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (390, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 16:25:22.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (391, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 16:51:49.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (392, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 16:53:31.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (393, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 16:53:57.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (394, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-17 16:54:53.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (395, 8, N'Daily at 12:05', 0, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:28:08.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (396, 8, N'Daily at 12:30', 0, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:28:09.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 9, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (397, 8, N'Daily at 9:00', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:28:09.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (398, 8, N'Weekly Wednesday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:28:09.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (399, 8, N'Weekly Thursday', 0, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:28:09.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (400, 8, N'Weekly Friday', 0, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:28:09.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (401, 8, N'Future Start', 0, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:28:10.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 14, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (402, 8, N'Daily at 12:05', 4, N'2', 1, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:38:12.0000000' AS DateTime2), CAST(N'2013-01-09 12:16:11.0000000' AS DateTime2), 2, 1, 8, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (403, 8, N'Daily at 12:30', 4, N'3', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:38:13.0000000' AS DateTime2), CAST(N'2013-01-09 12:31:16.0000000' AS DateTime2), 2, 1, 9, N'NO JOB RECIPIENTS FOUND')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (404, 8, N'Daily at 9:00', 4, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:38:13.0000000' AS DateTime2), CAST(N'2013-01-09 09:01:04.0000000' AS DateTime2), 2, 1, 10, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (405, 8, N'Weekly Wednesday', 4, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:38:13.0000000' AS DateTime2), CAST(N'2013-01-09 08:00:58.0000000' AS DateTime2), 2, 1, 11, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (406, 8, N'Weekly Thursday', 4, N'2', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:38:14.0000000' AS DateTime2), CAST(N'2013-01-03 08:11:18.0000000' AS DateTime2), 2, 1, 12, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (407, 8, N'Weekly Friday', 4, N'2', 2, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:38:14.0000000' AS DateTime2), CAST(N'2013-01-04 17:12:20.0000000' AS DateTime2), 2, 1, 13, N'')
GO
INSERT [dbo].[tr_JobMaster] ([jobId], [accountId], [jobName], [status], [listIds], [contentId], [deliveryTime], [created], [finished], [messageCount], [smtpAcctId], [msgSchedId], [lastError]) VALUES (408, 8, N'Future Start', 4, N'3', 3, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), CAST(N'2012-12-18 12:38:14.0000000' AS DateTime2), CAST(N'2013-01-08 10:15:12.0000000' AS DateTime2), 2, 1, 14, N'')
GO
SET IDENTITY_INSERT [dbo].[tr_JobMaster] OFF
GO
INSERT [dbo].[tr_LocalSMTP] ([accountID], [UserName], [Password], [IncomingHost], [OutgoingHost], [IncomingPort], [OutgoingPort], [EmailAddress], [UseInSSL], [UseInTSL], [UseOutSSL], [UseOutTSL], [AuthenticationReceive], [Authentication]) VALUES (2, N'chadf@nyc.rr.com', N'106cmfspring', N'pop-server.nyc.rr.com', N'smtp-server.nyc.rr.com', 110, 25, N'chadf@nyc.rr.com', 0, 0, 0, 0, -1, 0)
GO
SET IDENTITY_INSERT [dbo].[tr_MessageSchedule] ON 

GO
INSERT [dbo].[tr_MessageSchedule] ([msg_sched_id], [account_id], [schedule_id], [content_id], [description], [status], [recipients], [created], [last_sent], [number_to_process], [process_count], [day_to_process], [day_to_process_2], [from], [subject], [frequency], [start_date], [end_date], [no_end_date], [exception_override], [smtp_acct_id], [returnReceipt], [useWeekends], [useHolidays]) VALUES (8, 8, 4, 1, N'Daily at 12:05', 0, N'2', CAST(N'2012-11-13 12:05:00.0000000' AS DateTime2), CAST(N'2013-01-09 12:16:11.0000000' AS DateTime2), 0, 2, 0, 0, N'mike@bartmon.com', N'Daily at 12:05', 1, CAST(N'2012-11-13 12:05:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 0, 1, 0, 0, 0)
GO
INSERT [dbo].[tr_MessageSchedule] ([msg_sched_id], [account_id], [schedule_id], [content_id], [description], [status], [recipients], [created], [last_sent], [number_to_process], [process_count], [day_to_process], [day_to_process_2], [from], [subject], [frequency], [start_date], [end_date], [no_end_date], [exception_override], [smtp_acct_id], [returnReceipt], [useWeekends], [useHolidays]) VALUES (9, 8, 5, 2, N'Daily at 12:30', 0, N'3', CAST(N'2012-11-13 11:28:16.0000000' AS DateTime2), CAST(N'2013-01-09 12:31:16.0000000' AS DateTime2), 0, 2, 0, 0, N'mike@bartmon.com', N'Daily at 12:30', 1, CAST(N'2012-11-13 12:30:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 0, 1, 0, 0, 0)
GO
INSERT [dbo].[tr_MessageSchedule] ([msg_sched_id], [account_id], [schedule_id], [content_id], [description], [status], [recipients], [created], [last_sent], [number_to_process], [process_count], [day_to_process], [day_to_process_2], [from], [subject], [frequency], [start_date], [end_date], [no_end_date], [exception_override], [smtp_acct_id], [returnReceipt], [useWeekends], [useHolidays]) VALUES (10, 8, 6, 3, N'Daily at 9:00', 0, N'2', CAST(N'2012-11-13 11:28:53.0000000' AS DateTime2), CAST(N'2013-01-09 09:01:04.0000000' AS DateTime2), 0, 2, 0, 0, N'mbartmon@gmail.com', N'Daily at 9:00', 1, CAST(N'2012-11-14 09:00:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 0, 1, 0, 0, 0)
GO
INSERT [dbo].[tr_MessageSchedule] ([msg_sched_id], [account_id], [schedule_id], [content_id], [description], [status], [recipients], [created], [last_sent], [number_to_process], [process_count], [day_to_process], [day_to_process_2], [from], [subject], [frequency], [start_date], [end_date], [no_end_date], [exception_override], [smtp_acct_id], [returnReceipt], [useWeekends], [useHolidays]) VALUES (11, 8, 7, 3, N'Weekly Wednesday', 0, N'2', CAST(N'2012-11-13 17:14:14.0000000' AS DateTime2), CAST(N'2013-01-09 08:00:58.0000000' AS DateTime2), 0, 2, 0, 0, N'mbartmon@gmail.com', N'Weekly Wednesday', 2, CAST(N'2012-11-14 08:00:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 0, 1, 0, 0, 0)
GO
INSERT [dbo].[tr_MessageSchedule] ([msg_sched_id], [account_id], [schedule_id], [content_id], [description], [status], [recipients], [created], [last_sent], [number_to_process], [process_count], [day_to_process], [day_to_process_2], [from], [subject], [frequency], [start_date], [end_date], [no_end_date], [exception_override], [smtp_acct_id], [returnReceipt], [useWeekends], [useHolidays]) VALUES (12, 8, 8, 3, N'Weekly Thursday', 0, N'2', CAST(N'2012-11-13 17:15:00.0000000' AS DateTime2), CAST(N'2013-01-03 08:11:18.0000000' AS DateTime2), 0, 2, 0, 0, N'mbartmon@gmail.com', N'Weekly Thursday', 2, CAST(N'2012-11-15 08:00:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 0, 1, 0, 0, 0)
GO
INSERT [dbo].[tr_MessageSchedule] ([msg_sched_id], [account_id], [schedule_id], [content_id], [description], [status], [recipients], [created], [last_sent], [number_to_process], [process_count], [day_to_process], [day_to_process_2], [from], [subject], [frequency], [start_date], [end_date], [no_end_date], [exception_override], [smtp_acct_id], [returnReceipt], [useWeekends], [useHolidays]) VALUES (13, 8, 9, 2, N'Weekly Friday', 0, N'2', CAST(N'2012-11-13 17:15:35.0000000' AS DateTime2), CAST(N'2013-01-04 17:12:20.0000000' AS DateTime2), 0, 2, 0, 0, N'mike@bartmon.com', N'Weekly Friday', 2, CAST(N'2012-11-16 17:00:19.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 0, 1, 0, 0, 0)
GO
INSERT [dbo].[tr_MessageSchedule] ([msg_sched_id], [account_id], [schedule_id], [content_id], [description], [status], [recipients], [created], [last_sent], [number_to_process], [process_count], [day_to_process], [day_to_process_2], [from], [subject], [frequency], [start_date], [end_date], [no_end_date], [exception_override], [smtp_acct_id], [returnReceipt], [useWeekends], [useHolidays]) VALUES (14, 8, 10, 3, N'Future Start', 0, N'3', CAST(N'2012-11-16 10:08:18.0000000' AS DateTime2), CAST(N'2013-01-08 10:15:12.0000000' AS DateTime2), 0, 2, 0, 0, N'mike@bartmon.com', N'Future', 2, CAST(N'2013-01-01 10:07:44.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 0, 1, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[tr_MessageSchedule] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_notes] ON 

GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (1, -1, 1, N'On July 21st 2011, Chad received a voicemail from Ms. Otero.  Her apt was robbed.  Ratko said that he could see on the cameras that someone was ringing the intercom for 5 minutes.  Someone finally let the person into the building.  Ratko thinks the robber came in through the front door.  Chad left a message for Ms. Oter 7/22/11', CAST(N'2011-07-19 09:34:15.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (5, 6, 1, N'Tenant requesting that we replace wood under kitchen sink & that we repair kitchen draws & replace tracks to draws.  Told her to call me on 8/16/11, when Duke returns so he can check it out.', CAST(N'2011-07-27 16:39:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (7, 8, 1, N'Tenant requesting that we move them to another apartment in the building.  Told them they do no pay their rent on time and the rent on Apt 5C is more expensive.', CAST(N'2011-07-27 17:11:52.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (10, 11, 2, N'DAUGHTER LIVING IN APT - SHE HAS NO RIGHTS TO APT', CAST(N'2011-08-19 12:33:03.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (11, 11, 2, N'8/17/2011 - TENANT PUNCTURED REFRIGERATOR - DAUGHTER CALLED - SHE IS AN ILLEGAL TENANT - TOLD HER TO HAVE MR. TORRES CALL OFFICE - HE WILL HAVE TO PAY A RENT INCREASE FOR A NEW REFRIGERATOR', CAST(N'2011-08-19 12:33:49.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (12, 13, 2, N'TEST', CAST(N'2011-08-26 16:34:58.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (13, 14, 2, N'ALDFLADF', CAST(N'2011-08-26 16:37:08.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (14, 16, 2, N'TENANT HAVING ISSUES WITH FROM EXTERIOR OF BUILDING - BRICKS ARE LEAKING - CAUSING DAMAGE IN BEDROOM & BATHROOM', CAST(N'2011-09-02 10:17:32.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (15, 17, 2, N'TENANT GETTING LEAK FROM APT 4G SHOWER STALL - NECA GROUTED 4G 8/31 - IF NO  MORE LEAK - REPAIR BEDROOM & BATHROOM IN 3G', CAST(N'2011-09-02 10:19:37.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (16, 18, 2, N'TENANT GETTING LEAK FROM EXTERIOR BRICKS - NATHAN SPOKE TO YUNIS ABOUT THIS REPAIR - NEED TO PLASTER & PAINT APT', CAST(N'2011-09-02 10:22:35.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (18, 20, 2, N'TENANT COMPLAINTED OF GETTING MILDEW AGAIN IN BATHROOM ON 9/26/11 - CHAD TOLD NIDE TO CHECK UPSTAIRS - APT #65 BATHROOM NEEDS GROUT - ITS BAD - NECA GOING 9/27/11 - WHEN DONE GET LLOYD TO PAINT APT', CAST(N'2011-09-27 09:25:02.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (19, 21, 2, N'TENANT GETTING MILDEW IN SMALL BATHROOM - TOLD CHARLES TO HAVE TENANT KEEP WINDOW OPEN - NEED TO HAVE PAINTER GET INTO APT ASAP', CAST(N'2011-09-27 17:28:39.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (20, 22, 2, N'TENANT ASKED AGAIN FOR US TO MOVE HIM TO A HIGHER FLOOR.  HE HAS AN ELDERLY MOTHER LIVING WITH HIM.  HE ALSO COMPLAINS HAS NOTHING NICE TO SAY ABOUT US.  CHAD TOLD HIM THAT IF HIS MOTHER IS ELDERLY HE WOULD PROBABLY BE BETTER OFF STAYING ON THE 1ST FLOOR.  HE WAS NOT HAPPY WITH THAT ANSWER.', CAST(N'2011-09-27 17:30:31.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (21, 23, 2, N'TENANT BROKE BEDROOM DOOR IN APT.  WE REPLACED IT.  WHEN NECA WAS IN APT HE MENTIOND THAT SHE HAS SOME PEELING PAINT.  CHAD TRIED TO HAVE TOMMY SCHEDULE WITH OGGI TO GET INTO HER APARTMENT BUT SHE REFUSED PAINTING ON 9/26/11.  TOLD OGGI TO TELL HER IT MUST GET DONE.', CAST(N'2011-09-27 17:32:20.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (22, 20, 2, N'APT 65 HAD TILES THAT FELL OFF OF WALL - WE REPAIRED IT 9/27/11 - ONCE 55 CEILING IS DRY NEED TO P&P', CAST(N'2011-09-28 09:45:42.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (24, 25, 2, N'APT 5E COMPLAINTED 10/4/11 THAT 6E IS PLAYING HIS MUSIC TOO LOUD.  OGGI TOLD CHAD THAT HE SPOKE TO 6E ABOUT 1 WEEK PRIOR TO COMPLAINT.  CHAD CALLED 6E & TOLD HIM HE NEEDS TO KEEP IT DOWN.  HE SAID ITS HIS SURROUND SOUND.  CHAD TOLD HIM WE WILL NOT TOLERATE THIS.', CAST(N'2011-10-04 10:42:07.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (25, 26, 2, N'TENANT MOVED OUT OF APT & MOVED HER NIECE INTO APT ILLLEGALLY - CHAD TOLD THEM THEY CANNOT DO THIS - SO, MS. SHIN MOVED HER STUFF BACK INTO APT - WE THINK THAT HER STUFF IS JUST BACK IN APT, BUT SHE IS LIVING IN NJ.  IVAN WILL DO SPOT CHECKS - HE DID 1ST SPOT-CHECK 10/5/11 @ 9PM & NOBODY WAS HOME & IVAN SAID SHE TYPICALLY IS HOME BY 7PM', CAST(N'2011-10-06 13:50:51.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (28, 31, 2, N'TENANT LOOKING FOR A 3BR APT', CAST(N'2011-10-11 11:41:56.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (29, 32, 2, N'WE HAD AN ISSUE WITH WINDOW LEAKING WATER FROM OUTSIDE - NECA CAULKED ALL AROUND IT - NEED TO REPAIR HIS WALLS WHEN EVERYTHING IS DRY.', CAST(N'2011-10-11 12:06:49.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (30, 33, 2, N'MILUN TOLD CHAD THAT TENANT''S LEAVE ENTRANCE DOOR TO APT OPEN WHEN THEY COOK & IT SMELLS THE BUILDING.  MILUN HAS TOLD THEM NOT TO DO THIS.  HE ALSO SAID THEY ARE ONLY TENANT''S THAT LEAVE GARBAGE IN FRONT OF THEIR APARTMENT DOOR.', CAST(N'2011-10-13 16:36:40.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (31, 35, 2, N'TENANT WANTS TO MOVE OUT SOMETIME DURING THE SPRING/SUMMER OF 2012.  THEY DONT KNOW WHAT TO DO REGARDING THEIR LEASE RENEWAL.  CHAD TOLD THEM TO SIGN IT & WRITE LETTER STATING THAT THEY ARE LOOKING TO MOVE SOMETIME SOON.  TOLD THEM IF THEY GIVE SUFFICIENT NOTICE & NOT GIVE US APT DURING THE WINTER THEN WE WILL PROBABLY HAVE NO ISSUE WITH IT.', CAST(N'2011-10-20 12:12:48.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (32, 36, 2, N'TENANT HAD A PARTY IN HER APT ON FRIDAY 10/21/11 UNTIL 2AM WITH LOUD NOISE &  LOTS OF PEOPLE - APT 5C SAID IT WAS EXTREMELY LOUD & PEOPLE WERE SCREAMING - CHAD SPOKE WITH THE TEANANT & TOLD HER IF THIS HAPPENS AGAIN SHE WILL END UP IN COURT.  ALSO TOLD HER SHE SHOULD APOLOGIZE TO HER NEIGHBORS.  APT 5C MENTIONED THAT SHE SEES A MAN COMING & GOING FROM APT SOMETIMES.  THIS WAS TENANT WHOSE EX-HUSBAND CAME TO THE LEASE SIGNING.', CAST(N'2011-10-24 10:22:03.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (33, 36, 2, N'APT 5A (GOMEZ) SAID THAT 5B HAS A GUY THAT SHE SEES COMING & GOING FROM THE APT - SHE INTRODUCED HERSELF & HE SAID HE IS HER EX-HUSBAND.  SAME GUY THAT CAME TO LEASE SIGNING THAT SAID HE WASN''T LIVING IN APT.', CAST(N'2011-10-25 11:29:56.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (34, 38, 2, N'APT 5F (RUBIANO) CAME TO OFFICE & SAID THAT SHE SEES ANOTHER LADY STAYING IN THIS APT WITH A CHILD.  SHE SAID THE CHILD STANDS ON THE WINDOW SIL & PUSHES ON THE SCREEN & IS WORRIED SHE MAY FALL OUT.  GUS TOLD CHAD THAT THE LITTLE GIRL IS THE TENANTS NIECE & SHE IS NO LONGER IN THE APT', CAST(N'2011-10-28 10:14:43.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (35, 38, 2, N'CHAD FOLLOWED UP WITH GUS TO SEE IF THE LITTLE GIRL IS BACK IN THE APT & GUS SAID HE SAW HER ONE DAY THIS WEEK - CHAD LEFT A MESSAGE & EMAILED MS. BACA SO WE CAN INSTALL WINDOW GUARDS ASAP.', CAST(N'2011-10-28 10:15:41.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (36, 39, 2, N'TENANT''S BROTHER ON FRIDAY EVENING 11/5 KEPT RINGING BELL TO BUILDING TO TRY TO GET INTO BUILDING.  HE HAS BEEN LIVING IN BUILDING FOR 8 MONTHS.  TENANT PICKED LOCK TO BUILDING TO GET IN.  WE HAVE RECENTLY HAD ISSUES WITH THE LOCK & CHARLES THINKS HE MAY BE TAMPERING WITH IT.', CAST(N'2011-11-07 09:30:13.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (37, 40, 2, N'CHARLES INFORMED OFFICE THAT TENANT HAS LOTS OF ROACHES IN APT & WHEN THEY THROW AWAY GARBAGE ITS FILLED WITH ROACHES.  ALSO, THEY ARE NOT ALLOWING EXTERMINATOR INTO APT WHEN HE COMES EACH MONTH.  HE SAID APARTMENT IS INFESTED.', CAST(N'2011-11-08 11:25:55.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (39, 43, 2, N'MR. MOYA COMPLAINED THAT ON THURSDAY 11/17/11 THE APARTMENT WAS COLD AT 11:15PM.  THE HEAT COMPUTER SHOWS THAT THE APARTMENT WAS 71 DEGREES AT THAT TIME.  TOM WILL GO UP & CHECK APT BUT THAT IS ADEQUATE TEMPERATURE.', CAST(N'2011-11-21 09:32:44.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (40, 44, 2, N'TENANT WOULD LIKE TO RENEW LEASE & BE ABLE TO VACATE IT BY JULY 31ST.  TOLD HER TO SEND EMAIL', CAST(N'2011-11-21 10:13:47.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (41, 45, 2, N'MS. HARMON IN APT 1D CALLED REGARDING NOISE FROM APT.  SHE SAID TENANT PLAYING LOUD MUSIC ALL AFTERNOON.  TONY WENT UPSTAIRS 12/13 @ 4:30PM AND HEARD MUSIC WAS LOUD.  TONY SAID TENANT''S FAMLY IS HANGING OUT IN APT & THEY ARE THERE ALL DAY JUST HANGING OUT.  TENANT SAID ON APPLICATION THAT SHE IS QUIET.', CAST(N'2011-12-13 16:35:07.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (42, 46, 2, N'TENANT HAD A PARTY IN APT ON 12/17/11 - APT 304 SAID THAT SHE HAD PEOPLE IN THE APARTMENT UNTIL 2AM IN THE MORNING WITH LOUD MUSIC.  SARAH TOLD CHAD THAT THIS TENANT ALSO HAD A PARTY IN APT RIGHT AFTER SHE MOVED IN TO THE BUILDING & SHE RECEIVED SEVERAL COMPLAINS.  THIS IS NOW THE 2ND TIME.  CHAD SPOKE TO MS. HIDALGO 12/22/11 & TOLD HER THAT THIS BEHAVIOR MUST STOP IMMEDIATELY & THAT SHE CANNOT HAVE PARTIES IN THE BUILDING.', CAST(N'2011-12-22 09:34:46.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (43, 50, 2, N'TENANT IS TRYING TO RENT HER APT - SHE POSTED HER OWN AD & IS SENDING THE RESPONSES SHE RECEIVES TO CHAD.', CAST(N'2012-01-30 11:15:44.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (44, 51, 2, N'REGARDING HER POTENTIALLY MOVING TO A LARGER APT', CAST(N'2012-01-31 16:25:27.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (45, 52, 2, N'TENANT HAS BEEN COMPLAINING ABOUT NOISE COMING FROM BEDROOM RADIATORS.  IVAN CHANGES AIR VALVES & HEAT IS FINE, BUT SHE IS STILL COMPLAINING ABOUT THE NOISE.  CHAD WENT TO THE APT ON 1/31/12 & IVAN MANUALLY TURNED THE HEAT ON.  THE SOUND OF THE RADIATORS SEEMED NORMAL, DID NOT SEEM TO BE TOO LOUD.  CHAD SPOKE TO TENANT & INFORMED HER THAT IT IS A TYPICAL SOUND FROM A RADIATOR & THERE IS NOTHING THAT CAN BE DONE ABOUT THIS.  SHE WILL TRY TO DEAL WITH HER NOISE SENSITITY.', CAST(N'2012-01-31 16:43:23.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (46, 53, 2, N'TENANT VACATING LETTER', CAST(N'2012-01-31 17:36:12.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (47, 54, 2, N'APT 5C CALLED TO SAY THAT TENANT PLAYS HER MUSIC LOUD ALL THE TIME & IS NOT BEING CONSIDERING.  CHAD TOLD IVAN THAT 5C WILL CALL HIM WHEN HE HEARS IT.  AT 12:30AM ON SUNDAY 12/5/12 IVAN RECEIVED A CALL FROM 5C.  IVAN SAID THE MUSIC WAS EXTREMELY LOUD & TOLD HER TO KEEP IT DOWN.  CHAD LEFT A MESSAGE FOR TENANT 12/6/12.', CAST(N'2012-02-06 10:14:49.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (48, 55, 2, N'TENANT''S DAUGHTER CALLED 2/6/12 ASKING IF SHE CAN HAVE A DOG IN THE APT.  CHAD TOLD HER NO.  SHE ALSO ASKED MARISOL A LOT OF QUESTIONS ABOUT HAVING A DOG IN THE BUILDING BEFORE CHAD SPOKE TO HER.  MARISOL ALSO TOLD HER NO DOGS.  TOLD MILUN TO KEEP ANY EYE ON THIS APARTMENT TO MAKE SURE THEY DONT BRING A DOG INTO THE BUILDING.', CAST(N'2012-02-06 17:04:37.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (49, 56, 2, N'NECA WAS IN TENANT''S APT 2/22/12 & SAID THAT IT SMELLS AWFUL.  THEY HAVE MANY CATS & LET THEM URINATE ALL OVER THE APT.  HE SAID THEY HAVE FLYS ALL OVER THE APT & THE SMELL IS TERRIBLE.  TENANT DOES NOT HAVE A PHONE#, NEED TO WRITE A LETTER.', CAST(N'2012-02-23 09:49:41.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (50, 57, 2, N'TENANT PURCHASED HIS OWN GAS RANGE ON 2/28/2012.  WE CAN SEE DELIVERY COMING IN ON THE CAMERAS.  CHAD SPOKE TO TENANT''S SON, HE TOLD HIM IT WAS ILLEGAL.  HE TOLD HIM HOW MUCH IT WOULD COST FOR A NEW STOVE FOR A RENT INCREASE.  HIS SON SAID HE WOULD DISCUSS IT WITH HIS FATHER.', CAST(N'2012-03-02 10:15:25.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (51, 58, 2, N'ON 3/3/12 TENANT HAD A LARGE PARTY IN HER APARTMENT.  APT 1C COMPLAINED AS DID OTHER TENANT''S.  MARIA WENT UPSTAIRS & SAID IT WAS LOUD & THE PARTY LASTED UNTIL AROUND 5:30AM.  NATHAN SPOKE TO TENANT ON 3/6/12 & TOLD HER THIS BEHAVIOR CANNOT OCCUR IN THE BUILDING.', CAST(N'2012-03-06 15:29:20.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (52, 59, 2, N'TENANT COMPLAINED TO CHAD ON 3/6/12 THAT BATHTUB IS PEELING - OGGIE WILL CHECK IT OUT & LET CHAD KNOW IF IT NEEDS TO BE REGLAZED.', CAST(N'2012-03-06 15:31:39.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (53, 60, 2, N'TEST', CAST(N'2012-03-06 17:50:56.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (54, 61, 2, N'TENANT HAS BEEN COMPLAINING THAT APT 2H IS PLAYING HER MUSIC LOUD & MAKING TOO MUCH NOISE.  CHAD SPOKE TO 2H & SHE SAID SHE IS NOT.', CAST(N'2012-03-13 10:45:01.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (55, 62, 2, N'Comcast wants to place a cable transmitter on the roof of the building to provide all tenants with wireless access to their cable service.', CAST(N'2012-03-27 10:45:34.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (56, 62, 2, N'Will distribute a survey to tenants.', CAST(N'2012-03-27 13:00:19.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (57, 62, 2, N'Results of survey indicate that the majority of tenants would like to have wireless cable reception.', CAST(N'2012-03-27 13:01:26.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (58, 62, 2, N'Notified Comcast that we are ready to go ahead with the installation of their wireless cable equipment.', CAST(N'2012-03-27 13:06:47.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (59, 62, 2, N'Comcast notified us that they can begin installation on April 15.', CAST(N'2012-03-27 13:07:21.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (60, 62, 2, N'Gave Comcast the okay to come and install their equipment on April 15', CAST(N'2012-03-27 13:07:43.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (61, 62, 2, N'Construction began', CAST(N'2012-03-27 13:48:46.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (62, 62, 2, N'Construction continuing', CAST(N'2012-03-27 13:48:57.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (63, 62, 2, N'Construction completed', CAST(N'2012-03-27 13:49:08.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (64, 62, 2, N'Comcast will need access to tenants who want to use their wireless service', CAST(N'2012-03-27 13:49:32.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (65, 64, 8, N'Vent needs cleaning', CAST(N'2012-09-16 11:37:31.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_notes] ([note_id], [issue_id], [account_id], [note], [timestamp]) VALUES (66, 63, 8, N'This is a note', CAST(N'2012-11-26 10:33:21.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[tr_notes] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_options] ON 

GO
INSERT [dbo].[tr_options] ([ID], [retryQueueAtStartup], [autoDeleteMessages], [autoDeleteAge], [messageCreationCheckInterval], [companyName], [emailSignature]) VALUES (1, 1, 0, 0, 5, N'', N'')
GO
SET IDENTITY_INSERT [dbo].[tr_options] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_RecipientMaster] ON 

GO
INSERT [dbo].[tr_RecipientMaster] ([listId], [accountId], [listName], [companyIds], [buildingIds], [tenantTypes], [ownerFilter], [buildingFilter], [typeFilter], [arrearsFilter], [arrearsAmount]) VALUES (1, 8, N'list', N'', N'185', N'CS', 0, 1, 1, 1, 75.0000)
GO
INSERT [dbo].[tr_RecipientMaster] ([listId], [accountId], [listName], [companyIds], [buildingIds], [tenantTypes], [ownerFilter], [buildingFilter], [typeFilter], [arrearsFilter], [arrearsAmount]) VALUES (2, 8, N'Simple Test List', N'5', N'', N'', 0, 0, 0, 0, 0.0000)
GO
INSERT [dbo].[tr_RecipientMaster] ([listId], [accountId], [listName], [companyIds], [buildingIds], [tenantTypes], [ownerFilter], [buildingFilter], [typeFilter], [arrearsFilter], [arrearsAmount]) VALUES (3, 8, N'Test List Two', N'', N'', N'', 0, 0, 0, 0, 0.0000)
GO
INSERT [dbo].[tr_RecipientMaster] ([listId], [accountId], [listName], [companyIds], [buildingIds], [tenantTypes], [ownerFilter], [buildingFilter], [typeFilter], [arrearsFilter], [arrearsAmount]) VALUES (4, 8, N'Arrears', N'', N'', N'', 0, 0, 0, 1, 60.0000)
GO
SET IDENTITY_INSERT [dbo].[tr_RecipientMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_Recipients] ON 

GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (1, 1, N'Administrator', N'', N'mbartmon@gmail.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (2, 1, N'Chad', N'Feinstein', N'chadf@nyc.rr.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (3, 1, N'Chad', N'Feinstein', N'chadf@nyc.rr.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (4, 1, N'Barry M.', N'Fishman', N'barryf@nyc.rr.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (5, 1, N'Nathan', N'Fishman', N'nfishman@nyc.rr.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (6, 1, N'Nathan', N'Fishman', N'nfishman@nyc.rr.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (7, 1, N'Mike', N'Bartmon', N'mbartmon@gmail.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (8, 1, N'VANESSA', N'AYALA', N'VAYALA@ROCKETMAIL.COM', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (9, 1, N'MICHAEL J.', N'COLOMBO', N'MC724944@NYCIA.NET', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (10, 1, N'Nick', N'Vakirtzidelis', N'nvakirtzidelis@gmail.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (14, 2, N'Mike ', N'Bartmon', N'mike@bartmon.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (15, 2, N'', N'', N'mbartmon@gmail.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (16, 3, N'', N'', N'sales@bartmon.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (17, 3, N'', N'', N'support@bartmon.com', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[tr_Recipients] ([recipient_Id], [list_Id], [first_name], [last_name], [email_address], [address_one], [address_two], [address_three], [city], [state], [zip], [phone], [date_of_birth]) VALUES (18, 1, N'RAMIRO', N'ALBAN', N'ALBAN1950@YAHOO.COM', N'', N'', N'', N'', N'', N'', N'', CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[tr_Recipients] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_schedules] ON 

GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (3, 2, 0, N'Test Sched', CAST(N'2012-06-05 12:46:45.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 1, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (4, 8, 0, N'Daily at 12:05 pm', CAST(N'2012-11-13 12:05:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 1, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (5, 8, 0, N'Daily at 12:30 PM', CAST(N'2012-11-13 12:30:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 1, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (6, 8, 0, N'Daily at 9:00 AM', CAST(N'2012-11-14 09:00:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, 1, 1, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (7, 8, 0, N'Weekly - Wednesday', CAST(N'2012-11-14 08:00:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1900-01-01 00:00:00.0000000' AS DateTime2), 0, 1, 2, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (8, 8, 0, N'Weekly - Thursday', CAST(N'2012-11-15 08:00:00.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1900-01-01 00:00:00.0000000' AS DateTime2), 0, 1, 2, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (9, 8, 0, N'Weekly - Friday', CAST(N'2012-11-16 17:00:19.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1900-01-01 00:00:00.0000000' AS DateTime2), 0, 1, 2, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[tr_schedules] ([schedule_id], [account_id], [content_id], [schedule_name], [start_date], [end_date], [number_to_process], [last_processed], [process_count], [active], [frequency], [day_to_process], [day_to_process_2], [no_end_date], [useWeekends], [useHolidays]) VALUES (10, 8, 0, N'Future Start', CAST(N'2013-01-01 10:07:44.0000000' AS DateTime2), CAST(N'1899-12-30 00:00:00.0000000' AS DateTime2), 0, CAST(N'1900-01-01 00:00:00.0000000' AS DateTime2), 0, 1, 2, 0, 0, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[tr_schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_SmtpConfig] ON 

GO
INSERT [dbo].[tr_SmtpConfig] ([ID], [description], [emailAddress], [userName], [password], [server], [port], [SSL], [TSL], [authentication], [in_userName], [in_password], [in_server], [in_port], [in_SSL], [in_TSL], [in_authentication], [defaultAccount]) VALUES (1, N'gmail', N'mbartmon@gmail.com', N'mbartmon@gmail.com', N'vietnam65', N'smtp.gmail.com', 465, 1, 0, -1, N'mbartmon', N'vietnam65', N'map.gmail.com', 993, 1, 0, -1, 1)
GO
INSERT [dbo].[tr_SmtpConfig] ([ID], [description], [emailAddress], [userName], [password], [server], [port], [SSL], [TSL], [authentication], [in_userName], [in_password], [in_server], [in_port], [in_SSL], [in_TSL], [in_authentication], [defaultAccount]) VALUES (2, N'MBCS', N'mike@bartmon.com', N'webmaster@chrunners.net', N'vietnam65', N'smtpout.secureserver.net', 80, 0, 0, 1, N'mike@bartmon.com', N'viet65', N'pop3.webhero.com', 110, 0, 0, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[tr_SmtpConfig] OFF
GO
SET IDENTITY_INSERT [dbo].[tr_Vendors] ON 

GO
INSERT [dbo].[tr_Vendors] ([ID], [VendorName], [Address1], [Address2], [City], [State], [Zip]) VALUES (1, N'MBCS', N'7768 Sterling Dr', N'', N'Anna', N'tx', N'75409')
GO
SET IDENTITY_INSERT [dbo].[tr_Vendors] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [tr_Accounts$login]    Script Date: 3/8/2016 09:44:19 ******/
CREATE UNIQUE NONCLUSTERED INDEX [tr_Accounts$login] ON [dbo].[tr_Accounts]
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_contents$account_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_contents$account_id] ON [dbo].[tr_contents]
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_contents$content_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_contents$content_id] ON [dbo].[tr_contents]
(
	[content_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_issues$account_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_issues$account_id] ON [dbo].[tr_issues]
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_issues$location_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_issues$location_id] ON [dbo].[tr_issues]
(
	[location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_issues$note_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_issues$note_id] ON [dbo].[tr_issues]
(
	[issue_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [tr_issues$recipient_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_issues$recipient_id] ON [dbo].[tr_issues]
(
	[recipient_ids] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_issues$vendor_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_issues$vendor_id] ON [dbo].[tr_issues]
(
	[vendor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_IssueTenants$note_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_IssueTenants$note_id] ON [dbo].[tr_IssueTenants]
(
	[issue_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_JobMaster$accountId]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_JobMaster$accountId] ON [dbo].[tr_JobMaster]
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_JobMaster$contentId]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_JobMaster$contentId] ON [dbo].[tr_JobMaster]
(
	[contentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_JobMaster$jobMasterId]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_JobMaster$jobMasterId] ON [dbo].[tr_JobMaster]
(
	[msgSchedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_JobMaster$smtpAcctId]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_JobMaster$smtpAcctId] ON [dbo].[tr_JobMaster]
(
	[smtpAcctId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_LocalSMTP$accountID]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_LocalSMTP$accountID] ON [dbo].[tr_LocalSMTP]
(
	[accountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_messages$account_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_messages$account_id] ON [dbo].[tr_messages]
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_messages$issue_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_messages$issue_id] ON [dbo].[tr_messages]
(
	[issue_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_messages$message_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_messages$message_id] ON [dbo].[tr_messages]
(
	[message_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_MessageSchedule$account_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_MessageSchedule$account_id] ON [dbo].[tr_MessageSchedule]
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_MessageSchedule$content_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_MessageSchedule$content_id] ON [dbo].[tr_MessageSchedule]
(
	[content_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_MessageSchedule$msg_sched_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_MessageSchedule$msg_sched_id] ON [dbo].[tr_MessageSchedule]
(
	[msg_sched_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_MessageSchedule$number_to_process]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_MessageSchedule$number_to_process] ON [dbo].[tr_MessageSchedule]
(
	[number_to_process] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_MessageSchedule$schedule_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_MessageSchedule$schedule_id] ON [dbo].[tr_MessageSchedule]
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_MessageSchedule$smtp_acct_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_MessageSchedule$smtp_acct_id] ON [dbo].[tr_MessageSchedule]
(
	[smtp_acct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_notes$account_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_notes$account_id] ON [dbo].[tr_notes]
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_notes$issue_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_notes$issue_id] ON [dbo].[tr_notes]
(
	[issue_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_notes$note_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_notes$note_id] ON [dbo].[tr_notes]
(
	[note_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_RecipientMaster$accountId]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_RecipientMaster$accountId] ON [dbo].[tr_RecipientMaster]
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_RecipientMaster$recipId]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_RecipientMaster$recipId] ON [dbo].[tr_RecipientMaster]
(
	[listId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_Recipients$list_Id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_Recipients$list_Id] ON [dbo].[tr_Recipients]
(
	[list_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_Recipients$recipient_Id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_Recipients$recipient_Id] ON [dbo].[tr_Recipients]
(
	[recipient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_schedules$account_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_schedules$account_id] ON [dbo].[tr_schedules]
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_schedules$content_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_schedules$content_id] ON [dbo].[tr_schedules]
(
	[content_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_schedules$number_times]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_schedules$number_times] ON [dbo].[tr_schedules]
(
	[number_to_process] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_schedules$schedule_id]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_schedules$schedule_id] ON [dbo].[tr_schedules]
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [tr_SmtpConfig$ID]    Script Date: 3/8/2016 09:44:19 ******/
CREATE NONCLUSTERED INDEX [tr_SmtpConfig$ID] ON [dbo].[tr_SmtpConfig]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tr_messages] ADD  DEFAULT (getdate()) FOR [date_created]
GO
ALTER TABLE [dbo].[tr_messages] ADD  DEFAULT ((0)) FOR [request_confirm]
GO
ALTER TABLE [dbo].[tr_messages] ADD  DEFAULT ((0)) FOR [confirm_received]
GO
ALTER TABLE [dbo].[tr_messages] ADD  DEFAULT ((0)) FOR [read]
GO
ALTER TABLE [dbo].[tr_Accounts]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$tr_Accounts$login$disallow_zero_length] CHECK  ((len([login])>(0)))
GO
ALTER TABLE [dbo].[tr_Accounts] CHECK CONSTRAINT [SSMA_CC$tr_Accounts$login$disallow_zero_length]
GO
ALTER TABLE [dbo].[tr_Accounts]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$tr_Accounts$outlook_folder$disallow_zero_length] CHECK  ((len([outlook_folder])>(0)))
GO
ALTER TABLE [dbo].[tr_Accounts] CHECK CONSTRAINT [SSMA_CC$tr_Accounts$outlook_folder$disallow_zero_length]
GO
ALTER TABLE [dbo].[tr_Accounts]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$tr_Accounts$password$disallow_zero_length] CHECK  ((len([password])>(0)))
GO
ALTER TABLE [dbo].[tr_Accounts] CHECK CONSTRAINT [SSMA_CC$tr_Accounts$password$disallow_zero_length]
GO
ALTER TABLE [dbo].[tr_IssueTenants]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$tr_IssueTenants$apartment$disallow_zero_length] CHECK  ((len([apartment])>(0)))
GO
ALTER TABLE [dbo].[tr_IssueTenants] CHECK CONSTRAINT [SSMA_CC$tr_IssueTenants$apartment$disallow_zero_length]
GO
ALTER TABLE [dbo].[tr_IssueTenants]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$tr_IssueTenants$buildingNumber$disallow_zero_length] CHECK  ((len([buildingNumber])>(0)))
GO
ALTER TABLE [dbo].[tr_IssueTenants] CHECK CONSTRAINT [SSMA_CC$tr_IssueTenants$buildingNumber$disallow_zero_length]
GO
ALTER TABLE [dbo].[tr_IssueTenants]  WITH NOCHECK ADD  CONSTRAINT [SSMA_CC$tr_IssueTenants$tenantNumber$disallow_zero_length] CHECK  ((len([tenantNumber])>(0)))
GO
ALTER TABLE [dbo].[tr_IssueTenants] CHECK CONSTRAINT [SSMA_CC$tr_IssueTenants$tenantNumber$disallow_zero_length]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[login]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'login'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[account_type]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'account_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[permissions]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'permissions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[password]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[name]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[email]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[sms]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'sms'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[ip]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[port]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'port'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[parent_account]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'parent_account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[use_outlook]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'use_outlook'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[outlook_folder]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'COLUMN',@level2name=N'outlook_folder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'CONSTRAINT',@level2name=N'tr_Accounts$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Accounts].[login]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Accounts', @level2type=N'INDEX',@level2name=N'tr_Accounts$login'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_BuildingLocation].[ID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_BuildingLocation', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_BuildingLocation].[Location]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_BuildingLocation', @level2type=N'COLUMN',@level2name=N'Location'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_BuildingLocation]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_BuildingLocation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_BuildingLocation].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_BuildingLocation', @level2type=N'CONSTRAINT',@level2name=N'tr_BuildingLocation$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_categories].[category_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_categories', @level2type=N'COLUMN',@level2name=N'category_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_categories].[category]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_categories', @level2type=N'COLUMN',@level2name=N'category'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_categories]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_categories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_categories].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_categories', @level2type=N'CONSTRAINT',@level2name=N'tr_categories$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[content_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'COLUMN',@level2name=N'content_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'COLUMN',@level2name=N'account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[description]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[content_type]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'COLUMN',@level2name=N'content_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[content]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'COLUMN',@level2name=N'content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'CONSTRAINT',@level2name=N'tr_contents$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'INDEX',@level2name=N'tr_contents$account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_contents].[content_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_contents', @level2type=N'INDEX',@level2name=N'tr_contents$content_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[issue_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'issue_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[sequence_number]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'sequence_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[timestamp]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'timestamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[note]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'note'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[recipient_ids]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'recipient_ids'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[due_date]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'due_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[notify_responsible]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'notify_responsible'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[priority]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[status]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[subject]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[alert_mode]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'alert_mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[person_responsible]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'person_responsible'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[last_update]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'last_update'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[reported]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'reported'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[assigned]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'assigned'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[vendor_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'vendor_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[location_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'COLUMN',@level2name=N'location_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'CONSTRAINT',@level2name=N'tr_issues$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'INDEX',@level2name=N'tr_issues$account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[location_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'INDEX',@level2name=N'tr_issues$location_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[note_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'INDEX',@level2name=N'tr_issues$note_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[recipient_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'INDEX',@level2name=N'tr_issues$recipient_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_issues].[vendor_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_issues', @level2type=N'INDEX',@level2name=N'tr_issues$vendor_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[ID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[issue_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'COLUMN',@level2name=N'issue_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[sequence_number]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'COLUMN',@level2name=N'sequence_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[timestamp]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'COLUMN',@level2name=N'timestamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[buildingNumber]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'COLUMN',@level2name=N'buildingNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[tenantNumber]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'COLUMN',@level2name=N'tenantNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[apartment]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'COLUMN',@level2name=N'apartment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'CONSTRAINT',@level2name=N'tr_IssueTenants$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_IssueTenants].[note_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_IssueTenants', @level2type=N'INDEX',@level2name=N'tr_IssueTenants$note_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[jobId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'jobId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[accountId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'accountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[jobName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'jobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[status]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[listIds]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'listIds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[contentId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'contentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[deliveryTime]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'deliveryTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[created]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[finished]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'finished'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[messageCount]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'messageCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[smtpAcctId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'smtpAcctId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[msgSchedId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'msgSchedId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[lastError]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'COLUMN',@level2name=N'lastError'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'CONSTRAINT',@level2name=N'tr_JobMaster$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[accountId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'INDEX',@level2name=N'tr_JobMaster$accountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[contentId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'INDEX',@level2name=N'tr_JobMaster$contentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[jobMasterId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'INDEX',@level2name=N'tr_JobMaster$jobMasterId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_JobMaster].[smtpAcctId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_JobMaster', @level2type=N'INDEX',@level2name=N'tr_JobMaster$smtpAcctId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[accountID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'accountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[UserName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[Password]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[IncomingHost]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'IncomingHost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[OutgoingHost]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'OutgoingHost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[IncomingPort]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'IncomingPort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[OutgoingPort]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'OutgoingPort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[EmailAddress]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'EmailAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[UseInSSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'UseInSSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[UseInTSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'UseInTSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[UseOutSSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'UseOutSSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[UseOutTSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'UseOutTSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[AuthenticationReceive]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'AuthenticationReceive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[Authentication]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'COLUMN',@level2name=N'Authentication'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'CONSTRAINT',@level2name=N'tr_LocalSMTP$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_LocalSMTP].[accountID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_LocalSMTP', @level2type=N'INDEX',@level2name=N'tr_LocalSMTP$accountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[message_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'message_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[issue_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'issue_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[date_created]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'date_created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[direction]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'direction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[date_processed]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'date_processed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[request_confirm]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'request_confirm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[confirm_received]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'confirm_received'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[date_confirmed]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'date_confirmed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[filename]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'filename'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[read]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'COLUMN',@level2name=N'read'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'CONSTRAINT',@level2name=N'tr_messages$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'INDEX',@level2name=N'tr_messages$account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[issue_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'INDEX',@level2name=N'tr_messages$issue_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_messages].[message_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_messages', @level2type=N'INDEX',@level2name=N'tr_messages$message_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[msg_sched_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'msg_sched_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[schedule_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'schedule_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[content_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'content_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[description]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[status]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[recipients]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'recipients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[created]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[last_sent]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'last_sent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[number_to_process]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'number_to_process'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[process_count]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'process_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[day_to_process]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'day_to_process'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[day_to_process_2]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'day_to_process_2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[from]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'from'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[subject]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[frequency]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'frequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[start_date]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'start_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[end_date]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'end_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[no_end_date]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'no_end_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[exception_override]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'exception_override'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[smtp_acct_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'smtp_acct_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[returnReceipt]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'returnReceipt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[useWeekends]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'useWeekends'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[useHolidays]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'COLUMN',@level2name=N'useHolidays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'CONSTRAINT',@level2name=N'tr_MessageSchedule$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'INDEX',@level2name=N'tr_MessageSchedule$account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[content_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'INDEX',@level2name=N'tr_MessageSchedule$content_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[msg_sched_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'INDEX',@level2name=N'tr_MessageSchedule$msg_sched_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[number_to_process]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'INDEX',@level2name=N'tr_MessageSchedule$number_to_process'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[schedule_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'INDEX',@level2name=N'tr_MessageSchedule$schedule_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_MessageSchedule].[smtp_acct_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_MessageSchedule', @level2type=N'INDEX',@level2name=N'tr_MessageSchedule$smtp_acct_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[note_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'COLUMN',@level2name=N'note_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[issue_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'COLUMN',@level2name=N'issue_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'COLUMN',@level2name=N'account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[note]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'COLUMN',@level2name=N'note'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[timestamp]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'COLUMN',@level2name=N'timestamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'CONSTRAINT',@level2name=N'tr_notes$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'INDEX',@level2name=N'tr_notes$account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[issue_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'INDEX',@level2name=N'tr_notes$issue_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_notes].[note_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_notes', @level2type=N'INDEX',@level2name=N'tr_notes$note_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[ID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[retryQueueAtStartup]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'COLUMN',@level2name=N'retryQueueAtStartup'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[autoDeleteMessages]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'COLUMN',@level2name=N'autoDeleteMessages'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[autoDeleteAge]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'COLUMN',@level2name=N'autoDeleteAge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[messageCreationCheckInterval]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'COLUMN',@level2name=N'messageCreationCheckInterval'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[companyName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'COLUMN',@level2name=N'companyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[emailSignature]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'COLUMN',@level2name=N'emailSignature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_options].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_options', @level2type=N'CONSTRAINT',@level2name=N'tr_options$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[listId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'listId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[accountId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'accountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[listName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'listName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[companyIds]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'companyIds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[buildingIds]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'buildingIds'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[tenantTypes]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'tenantTypes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[ownerFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'ownerFilter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[buildingFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'buildingFilter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[typeFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'typeFilter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[arrearsFilter]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'arrearsFilter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[arrearsAmount]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'COLUMN',@level2name=N'arrearsAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'CONSTRAINT',@level2name=N'tr_RecipientMaster$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[accountId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'INDEX',@level2name=N'tr_RecipientMaster$accountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_RecipientMaster].[recipId]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_RecipientMaster', @level2type=N'INDEX',@level2name=N'tr_RecipientMaster$recipId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[recipient_Id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'recipient_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[list_Id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'list_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[first_name]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'first_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[last_name]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'last_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[email_address]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'email_address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[address_one]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'address_one'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[address_two]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'address_two'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[address_three]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'address_three'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[city]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'city'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[state]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'state'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[zip]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[phone]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[date_of_birth]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'COLUMN',@level2name=N'date_of_birth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'CONSTRAINT',@level2name=N'tr_Recipients$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[list_Id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'INDEX',@level2name=N'tr_Recipients$list_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Recipients].[recipient_Id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Recipients', @level2type=N'INDEX',@level2name=N'tr_Recipients$recipient_Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[schedule_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'schedule_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[content_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'content_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[schedule_name]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'schedule_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[start_date]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'start_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[end_date]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'end_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[number_to_process]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'number_to_process'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[last_processed]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'last_processed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[process_count]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'process_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[active]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'active'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[frequency]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'frequency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[day_to_process]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'day_to_process'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[day_to_process_2]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'day_to_process_2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[no_end_date]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'no_end_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[useWeekends]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'useWeekends'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[useHolidays]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'COLUMN',@level2name=N'useHolidays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'CONSTRAINT',@level2name=N'tr_schedules$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[account_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'INDEX',@level2name=N'tr_schedules$account_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[content_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'INDEX',@level2name=N'tr_schedules$content_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[number_times]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'INDEX',@level2name=N'tr_schedules$number_times'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_schedules].[schedule_id]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_schedules', @level2type=N'INDEX',@level2name=N'tr_schedules$schedule_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[ID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[description]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[emailAddress]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'emailAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[userName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'userName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[password]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[server]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'server'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[port]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'port'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[SSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'SSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[TSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'TSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[authentication]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'authentication'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[in_userName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'in_userName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[in_password]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'in_password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[in_server]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'in_server'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[in_port]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'in_port'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[in_SSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'in_SSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[in_TSL]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'in_TSL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[in_authentication]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'in_authentication'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[defaultAccount]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'COLUMN',@level2name=N'defaultAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'CONSTRAINT',@level2name=N'tr_SmtpConfig$PrimaryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_SmtpConfig].[ID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_SmtpConfig', @level2type=N'INDEX',@level2name=N'tr_SmtpConfig$ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[ID]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[VendorName]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'COLUMN',@level2name=N'VendorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[Address1]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'COLUMN',@level2name=N'Address1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[Address2]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'COLUMN',@level2name=N'Address2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[City]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'COLUMN',@level2name=N'City'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[State]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[Zip]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'COLUMN',@level2name=N'Zip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'WinTRM.[tr_Vendors].[PrimaryKey]' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tr_Vendors', @level2type=N'CONSTRAINT',@level2name=N'tr_Vendors$PrimaryKey'
GO
