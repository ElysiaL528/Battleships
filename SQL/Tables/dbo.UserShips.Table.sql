USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[UserShips]    Script Date: 8/28/2017 3:00:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserShips](
	[X] [tinyint] NOT NULL,
	[Y] [tinyint] NOT NULL,
	[ShipOrientation] [nchar](10) NOT NULL,
	[ShipID] [tinyint] NOT NULL,
	[UserID] [tinyint] NOT NULL
) ON [PRIMARY]

GO
