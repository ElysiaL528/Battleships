USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[Shots]    Script Date: 10/27/2017 11:47:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shots](
	[ShotID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[X] [tinyint] NOT NULL,
	[Y] [tinyint] NOT NULL,
	[isHit] [bit] NOT NULL,
	[isTestData] [bit] NULL,
 CONSTRAINT [PK_Shots] PRIMARY KEY CLUSTERED 
(
	[ShotID] ASC,
	[UserID] ASC,
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Shots]  WITH CHECK ADD  CONSTRAINT [FK_Shots_Rooms] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO
ALTER TABLE [dbo].[Shots] CHECK CONSTRAINT [FK_Shots_Rooms]
GO
ALTER TABLE [dbo].[Shots]  WITH CHECK ADD  CONSTRAINT [FK_Shots_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Shots] CHECK CONSTRAINT [FK_Shots_Users]
GO
ALTER TABLE [dbo].[Shots]  WITH CHECK ADD  CONSTRAINT [CK_Shots] CHECK  (([X]>=(1) AND [X]<=(10) AND ([Y]>=(1) AND [Y]<=(10))))
GO
ALTER TABLE [dbo].[Shots] CHECK CONSTRAINT [CK_Shots]
GO
