USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateUser]    Script Date: 11/3/2017 12:44:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC	[dbo].[usp_CreateUser]
@Username varchar(20)
as
INSERT INTO Users
VALUES (@Username, null)

SELECT	 SCOPE_IDENTITY() AS UserID
,		 @Username AS UserName
FROM	Users
WHERE	UserID	=	SCOPE_IDENTITY()






GO
