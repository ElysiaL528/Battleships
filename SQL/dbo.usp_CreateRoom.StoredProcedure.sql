USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateRoom]    Script Date: 3/2/2018 12:45:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROC [dbo].[usp_CreateRoom]
	@RoomName	varchar(20)
,	@UserID		int
as
	DECLARE @IsTestData bit = (SELECT isTestData FROM Users WHERE UserID = @UserID)

	INSERT INTO Rooms
	VALUES(@RoomName, @UserID, null, 0, 0, @IsTestData)

	SELECT	 SCOPE_IDENTITY() AS RoomID
FROM	Rooms
WHERE	RoomID	=	SCOPE_IDENTITY()



GO
