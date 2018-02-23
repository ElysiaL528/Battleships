USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateUser]    Script Date: 2/23/2018 12:45:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROC	[dbo].[usp_CreateUser]
	@Username	varchar(20)
,	@IsTestData	bit
as
INSERT INTO Users
VALUES (@Username, @IsTestData)

SELECT	 SCOPE_IDENTITY() AS UserID
,		 @Username AS UserName
FROM	Users
WHERE	UserID	=	SCOPE_IDENTITY()


GO
