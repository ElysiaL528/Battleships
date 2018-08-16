USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckUserReady]    Script Date: 8/31/2017 3:05:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[usp_CheckUserReady]
	@UserID int
,	@RoomID int
as


DECLARE @HostPlayerID int = (SELECT HostPlayerID FROM Rooms WHERE RoomID = @RoomID)
DECLARE @JoinedPlayerID int = (SELECT JoinedPlayerID FROM Rooms WHERE RoomID = @RoomID)
DECLARE @UserShipCount int = (SELECT COUNT(ShipID) FROM UserShips WHERE UserID = @UserID)

if(@UserShipCount = 5)
	BEGIN

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
	END


GO
