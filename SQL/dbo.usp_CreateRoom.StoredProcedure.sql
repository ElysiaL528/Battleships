USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateRoom]    Script Date: 11/3/2017 12:44:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[usp_CreateRoom]
	@RoomName	varchar(20)
,	@UserID		int
as
	INSERT INTO Rooms
	VALUES(@RoomName, @UserID, null, 0, 0)



GO
