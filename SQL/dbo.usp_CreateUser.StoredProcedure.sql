USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateUser]    Script Date: 9/22/2017 2:47:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC	[dbo].[usp_CreateUser]
@Username varchar(20)
as
INSERT INTO Users
VALUES (@Username)

SELECT	 SCOPE_IDENTITY() AS UserID
,		 @Username AS UserName
FROM	Users
WHERE	UserID	=	SCOPE_IDENTITY()





GO
