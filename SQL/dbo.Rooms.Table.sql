USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 9/22/2017 2:47:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [varchar](20) NOT NULL,
	[HostPlayerID] [int] NOT NULL,
	[JoinedPlayerID] [int] NULL,
	[isHostPlayerReady] [bit] NOT NULL,
	[isJoinedPlayerReady] [bit] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Users] FOREIGN KEY([HostPlayerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_Users]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Users1] FOREIGN KEY([JoinedPlayerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_Users1]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [CK_Rooms] CHECK  (([HostPlayerID]<>[JoinedPlayerID]))
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [CK_Rooms]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ensures that the host player and joined player are different users' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Rooms', @level2type=N'CONSTRAINT',@level2name=N'CK_Rooms'
GO
