USE [SuperManager]
GO
/****** Object:  Table [dbo].[T_VoteType]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_VoteType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_VoteText]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_VoteText](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[VoteID] [int] NOT NULL,
	[ItemID] [int] NOT NULL,
	[VoteText] [nvarchar](300) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_VoteItem]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_VoteItem](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [varchar](50) NOT NULL,
	[VoteID] [int] NOT NULL,
	[ItemTitle] [nvarchar](200) NOT NULL,
	[ItemType] [int] NOT NULL,
	[ItemContent] [nvarchar](100) NOT NULL,
	[ItemMaxCount] [int] NOT NULL,
	[ItemNum] [int] NOT NULL,
 CONSTRAINT [PK__T_Vote_I__30667A25797309D9] PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Vote]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Vote](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[VoteType] [int] NOT NULL,
	[VoteTitle] [nvarchar](200) NOT NULL,
	[VoteSummary] [nvarchar](1000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_UserLog]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_UserLog](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [varchar](50) NOT NULL,
	[LoginIP] [varchar](20) NOT NULL,
	[LoginDate] [datetime] NOT NULL,
	[LoginStatus] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_User]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_User](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [varchar](50) NOT NULL,
	[NickName] [nvarchar](30) NOT NULL,
	[UserPassword] [varchar](64) NOT NULL,
	[RoleID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_TopicType]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_TopicType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_TopicPositionType]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_TopicPositionType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Topic]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Topic](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TopicType] [int] NOT NULL,
	[PositionTypeList] [varchar](200) NOT NULL,
	[TopicTitle] [nvarchar](100) NOT NULL,
	[TopicTags] [nvarchar](50) NULL,
	[TopicCoverImageUrl] [varchar](200) NULL,
	[TopicSummary] [nvarchar](500) NULL,
	[TopicContent] [nvarchar](max) NOT NULL,
	[TopicOriginalWebsite] [nvarchar](50) NULL,
	[TopicOriginalUrl] [nvarchar](200) NULL,
	[TopicUserCode] [varchar](50) NOT NULL,
	[TopicStatus] [int] NOT NULL,
	[TopicVisitNum] [int] NOT NULL,
	[TopicDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Role]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Role](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[MenuList] [varchar](2000) NOT NULL,
	[ActionList] [varchar](max) NOT NULL,
 CONSTRAINT [PK__T_Role__8A2B616108EA5793] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_ProjectType]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_ProjectType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeSort] [int] NOT NULL,
 CONSTRAINT [PK__T_Projec__30667A2560A75C0F] PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Project]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Project](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectType] [int] NOT NULL,
	[ProjectName] [nvarchar](100) NOT NULL,
	[FlowID] [int] NOT NULL,
	[FlowStepID] [int] NOT NULL,
 CONSTRAINT [PK__T_Projec__30667A25656C112C] PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_NoticeType]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_NoticeType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Notice]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Notice](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[NoticeType] [int] NOT NULL,
	[NoticeTitle] [nvarchar](50) NOT NULL,
	[NoticeContent] [nvarchar](1000) NOT NULL,
	[NoticeDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Module]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Module](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleCode] [varchar](50) NOT NULL,
	[ModuleName] [nvarchar](30) NULL,
	[ActionList] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ModuleCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_MessageReply]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_MessageReply](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[MessageID] [int] NOT NULL,
	[ReplyContent] [nvarchar](max) NOT NULL,
	[UserCode] [varchar](50) NOT NULL,
	[NickName] [nvarchar](30) NOT NULL,
	[ReplyDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Message]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Message](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[ContactName] [nvarchar](50) NOT NULL,
	[ContactTelphone] [varchar](20) NULL,
	[ContactEmail] [varchar](50) NULL,
	[MessageContent] [nvarchar](max) NOT NULL,
	[ContactIP] [varchar](20) NOT NULL,
	[MessageStatus] [int] NOT NULL,
	[MessageDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Menu]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Menu](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[MenuUrl] [nvarchar](200) NULL,
	[BelongModule] [varchar](50) NULL,
	[ActionList] [varchar](500) NULL,
	[MenuSort] [int] NOT NULL,
	[MenuIcon] [varchar](100) NULL,
 CONSTRAINT [PK__T_Menu__30667A250425A276] PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_LinkFriendType]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_LinkFriendType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_LinkFriend]    Script Date: 10/10/2018 16:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_LinkFriend](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[LinkFriendType] [int] NOT NULL,
	[LinkFriendCoverImageUrl] [nvarchar](200) NULL,
	[LinkFriendName] [nvarchar](50) NOT NULL,
	[LinkFriendUrl] [varchar](200) NOT NULL,
	[LinkFriendSort] [int] NOT NULL,
 CONSTRAINT [PK__T_LinkFr__30667A2530F848ED] PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_IndexMapper]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_IndexMapper](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[IndexType] [int] NOT NULL,
	[IndexID] [int] NOT NULL,
	[MapperID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_FlowType]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_FlowType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_FlowSymbolType]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_FlowSymbolType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeCode] [varchar](50) NOT NULL,
	[TypeName] [nvarchar](30) NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_FlowStep]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_FlowStep](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[FlowID] [int] NOT NULL,
	[StepCode] [varchar](50) NOT NULL,
	[StepSymbol] [varchar](50) NOT NULL,
	[StepName] [nvarchar](50) NOT NULL,
	[StepAddrName] [nvarchar](20) NOT NULL,
	[RoleList] [varchar](100) NOT NULL,
	[StepList] [varchar](200) NOT NULL,
	[NextStep] [varchar](50) NULL,
	[PositionTop] [int] NOT NULL,
	[PositionLeft] [int] NOT NULL,
 CONSTRAINT [PK__T_Flow_S__90304C174316F928] PRIMARY KEY CLUSTERED 
(
	[StepCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Flow]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Flow](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[FlowType] [int] NOT NULL,
	[FlowName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Attachment]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Attachment](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[AttachmentType] [varchar](20) NOT NULL,
	[AttachmentName] [nvarchar](100) NOT NULL,
	[AttachmentSize] [int] NOT NULL,
	[AttachmentPath] [varchar](200) NOT NULL,
	[AttachmentDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Application]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Application](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationIcon] [varchar](50) NULL,
	[ApplicationUrl] [nvarchar](200) NOT NULL,
	[ApplicationName] [nvarchar](50) NOT NULL,
	[ApplicationType] [varchar](20) NOT NULL,
	[ApplicationX] [int] NOT NULL,
	[ApplicationY] [int] NOT NULL,
	[ApplicationWidth] [int] NOT NULL,
	[ApplicationHeight] [int] NOT NULL,
 CONSTRAINT [PK__T_Applic__30667A256BE40491] PRIMARY KEY CLUSTERED 
(
	[IdentityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_ActionType]    Script Date: 10/10/2018 16:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_ActionType](
	[IdentityID] [int] IDENTITY(1,1) NOT NULL,
	[TypeCode] [varchar](50) NOT NULL,
	[TypeName] [nvarchar](30) NULL,
	[TypeSort] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__T_Module___TypeS__1CBC4616]    Script Date: 10/10/2018 16:46:16 ******/
ALTER TABLE [dbo].[T_ActionType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_Attachm__Attac__43D61337]    Script Date: 10/10/2018 16:46:16 ******/
ALTER TABLE [dbo].[T_Attachment] ADD  DEFAULT (getdate()) FOR [AttachmentDate]
GO
/****** Object:  Default [DF_T_Flow_Step_Top]    Script Date: 10/10/2018 16:46:16 ******/
ALTER TABLE [dbo].[T_FlowStep] ADD  CONSTRAINT [DF_T_Flow_Step_Top]  DEFAULT ((0)) FOR [PositionTop]
GO
/****** Object:  Default [DF_T_Flow_Step_Left]    Script Date: 10/10/2018 16:46:16 ******/
ALTER TABLE [dbo].[T_FlowStep] ADD  CONSTRAINT [DF_T_Flow_Step_Left]  DEFAULT ((0)) FOR [PositionLeft]
GO
/****** Object:  Default [DF__T_Flow_Sy__TypeS__17F790F9]    Script Date: 10/10/2018 16:46:16 ******/
ALTER TABLE [dbo].[T_FlowSymbolType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_Flow_Ty__TypeS__52593CB8]    Script Date: 10/10/2018 16:46:16 ******/
ALTER TABLE [dbo].[T_FlowType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_LinkFri__LinkF__32E0915F]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_LinkFriend] ADD  CONSTRAINT [DF__T_LinkFri__LinkF__32E0915F]  DEFAULT ((0)) FOR [LinkFriendSort]
GO
/****** Object:  Default [DF__T_LinkFri__TypeS__489AC854]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_LinkFriendType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_Menu__MenuSort__060DEAE8]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Menu] ADD  CONSTRAINT [DF__T_Menu__MenuSort__060DEAE8]  DEFAULT ((0)) FOR [MenuSort]
GO
/****** Object:  Default [DF__T_Message__Messa__32AB8735]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Message] ADD  DEFAULT ((0)) FOR [MessageStatus]
GO
/****** Object:  Default [DF__T_Message__Messa__339FAB6E]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Message] ADD  DEFAULT (getdate()) FOR [MessageDate]
GO
/****** Object:  Default [DF__T_Message__Reply__3864608B]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_MessageReply] ADD  DEFAULT (getdate()) FOR [ReplyDate]
GO
/****** Object:  Default [DF__T_Notice__Notice__58D1301D]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Notice] ADD  DEFAULT (getdate()) FOR [NoticeDateTime]
GO
/****** Object:  Default [DF__T_NoticeT__TypeS__540C7B00]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_NoticeType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_Project__FlowN__6754599E]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Project] ADD  CONSTRAINT [DF__T_Project__FlowN__6754599E]  DEFAULT ((0)) FOR [FlowStepID]
GO
/****** Object:  Default [DF__T_Project__TypeS__628FA481]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_ProjectType] ADD  CONSTRAINT [DF__T_Project__TypeS__628FA481]  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_Topic__TopicSt__36B12243]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Topic] ADD  DEFAULT ((0)) FOR [TopicStatus]
GO
/****** Object:  Default [DF__T_Topic__TopicVi__37A5467C]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Topic] ADD  DEFAULT ((0)) FOR [TopicVisitNum]
GO
/****** Object:  Default [DF__T_Topic__TopicDa__38996AB5]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_Topic] ADD  DEFAULT (getdate()) FOR [TopicDateTime]
GO
/****** Object:  Default [DF__T_Topic_P__TypeS__31EC6D26]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_TopicPositionType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_Topic_T__TypeS__1ED998B2]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_TopicType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
/****** Object:  Default [DF__T_User_Lo__Login__2CF2ADDF]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_UserLog] ADD  DEFAULT (getdate()) FOR [LoginDate]
GO
/****** Object:  Default [DF__T_User_Lo__Login__2DE6D218]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_UserLog] ADD  DEFAULT ((0)) FOR [LoginStatus]
GO
/****** Object:  Default [DF__T_Vote_It__ItemN__7B5B524B]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_VoteItem] ADD  CONSTRAINT [DF__T_Vote_It__ItemN__7B5B524B]  DEFAULT ((0)) FOR [ItemNum]
GO
/****** Object:  Default [DF__T_Vote_Ty__TypeS__00200768]    Script Date: 10/10/2018 16:46:17 ******/
ALTER TABLE [dbo].[T_VoteType] ADD  DEFAULT ((0)) FOR [TypeSort]
GO
