USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 8/28/2017 3:00:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [varchar](20) NOT NULL,
	[HostPlayerID] [int] NOT NULL,
	[JoinedPlayerID] [int] NOT NULL,
	[isHostPlayerReady] [bit] NOT NULL,
	[isJoinedPlayerReady] [bit] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
