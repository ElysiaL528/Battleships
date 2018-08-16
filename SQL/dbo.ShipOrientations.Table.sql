USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[ShipOrientations]    Script Date: 9/22/2017 2:47:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipOrientations](
	[ShipOrientationID] [tinyint] IDENTITY(1,1) NOT NULL,
	[OrientationName] [varchar](10) NOT NULL,
	[OrientationValueX] [int] NOT NULL,
	[OrientationValueY] [int] NOT NULL,
 CONSTRAINT [PK_ShipOrientations] PRIMARY KEY CLUSTERED 
(
	[ShipOrientationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
