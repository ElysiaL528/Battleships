USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckUserReady]    Script Date: 8/30/2017 3:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_CheckUserReady]
	@UserID int
,	@RoomID int
,	@HostPlayerID int
,	@JoinedPlayerID int
as
if(@UserID = @HostPlayerID)
	BEGIN
		UPDATE Rooms
		SET isHostPlayerReady = 1
		WHERE RoomID = @RoomID
	END

else if(@UserID = @JoinedPlayerID)
	BEGIN
		UPDATE Rooms
		SET isJoinedPlayerReady = 1
		WHERE RoomID = @RoomID
	END
GO
