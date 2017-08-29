USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_joinRoom]    Script Date: 8/29/2017 3:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_joinRoom]
	@UserID int
,	@RoomID int
as	
	if EXISTS(	SELECT RoomID 
				FROM Rooms
				WHERE RoomID = @RoomID)
		BEGIN
			UPDATE Rooms
			SET JoinedPlayerID = @UserID 
			WHERE RoomID = @RoomID
		END
GO
