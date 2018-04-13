USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_JoinRoom]    Script Date: 4/13/2018 12:43:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[usp_JoinRoom]
	@UserID int
,	@RoomID int
as	
DECLARE @HostPlayerID int = (SELECT HostPlayerID FROM Rooms WHERE RoomID = @RoomID)
DECLARE @ErrorMessage char(20)

IF @UserID != @HostPlayerID
BEGIN
	UPDATE Rooms
	SET JoinedPlayerID = @UserID 
	WHERE RoomID = @RoomID
	SET @ErrorMessage = 'Successfully joined'
END
ELSE
BEGIN
	SET @ErrorMessage = 'Failed to join'
END
SELECT @ErrorMessage



GO
