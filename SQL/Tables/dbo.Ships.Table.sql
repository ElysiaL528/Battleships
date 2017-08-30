USE [ElysiaLopezBattleships2017]
GO
/****** Object:  Table [dbo].[Ships]    Script Date: 8/30/2017 11:50:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ships](
	[ShipID] [tinyint] NOT NULL,
	[ShipName] [varchar](16) NOT NULL,
	[ShipLength] [int] NOT NULL,
	[isSunk] [bit] NOT NULL,
 CONSTRAINT [PK_Ships] PRIMARY KEY CLUSTERED 
(
	[ShipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
