USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_SetUserReady]    Script Date: 9/22/2017 2:47:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[usp_SetUserReady]
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

SELECT	RoomID
	,		RoomName
	,		hp.UserID		AS HostPlayerID
	,		jp.UserID
	,		Rooms.isHostPlayerReady
	,		Rooms.isJoinedPlayerReady
	FROM	Rooms
	LEFT JOIN	Users		jp
	ON		Rooms.JoinedPlayerID	=	jp.UserID
	JOIN		Users		hp
	ON		Rooms.HostPlayerID		=	hp.UserID
	WHERE RoomID	=	@RoomID
	AND		jp.UserID	=	Rooms.JoinedPlayerID


GO
