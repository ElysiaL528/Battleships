USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[UserShips]    Script Date: 10/27/2017 11:47:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserShips](
	[ShipID] [tinyint] NOT NULL,
	[UserID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[X] [tinyint] NOT NULL,
	[Y] [tinyint] NOT NULL,
	[ShipOrientationID] [tinyint] NOT NULL,
	[isSunk] [bit] NULL,
	[HitCount] [int] NOT NULL,
	[isTestData] [bit] NULL,
 CONSTRAINT [PK_UserShips] PRIMARY KEY CLUSTERED 
(
	[ShipID] ASC,
	[UserID] ASC,
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserShips]  WITH CHECK ADD  CONSTRAINT [FK_UserShips_Rooms] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO
ALTER TABLE [dbo].[UserShips] CHECK CONSTRAINT [FK_UserShips_Rooms]
GO
ALTER TABLE [dbo].[UserShips]  WITH CHECK ADD  CONSTRAINT [FK_UserShips_ShipOrientations] FOREIGN KEY([ShipOrientationID])
REFERENCES [dbo].[ShipOrientations] ([ShipOrientationID])
GO
ALTER TABLE [dbo].[UserShips] CHECK CONSTRAINT [FK_UserShips_ShipOrientations]
GO
ALTER TABLE [dbo].[UserShips]  WITH CHECK ADD  CONSTRAINT [FK_UserShips_Ships] FOREIGN KEY([ShipID])
REFERENCES [dbo].[Ships] ([ShipID])
GO
ALTER TABLE [dbo].[UserShips] CHECK CONSTRAINT [FK_UserShips_Ships]
GO
ALTER TABLE [dbo].[UserShips]  WITH CHECK ADD  CONSTRAINT [FK_UserShips_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserShips] CHECK CONSTRAINT [FK_UserShips_Users]
GO
ALTER TABLE [dbo].[UserShips]  WITH CHECK ADD  CONSTRAINT [CK_UserShips] CHECK  (([X]>=(1) AND [X]<=(10) AND ([Y]>=(1) AND [Y]<=(10))))
GO
ALTER TABLE [dbo].[UserShips] CHECK CONSTRAINT [CK_UserShips]
GO
