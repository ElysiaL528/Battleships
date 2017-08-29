USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[ShipOrientations]    Script Date: 8/29/2017 11:28:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipOrientations](
	[ShipID] [int] IDENTITY(1,1) NOT NULL,
	[OrientationName] [varchar](10) NOT NULL,
	[OrientationValue] [int] NOT NULL,
 CONSTRAINT [PK_ShipOrientations] PRIMARY KEY CLUSTERED 
(
	[ShipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
