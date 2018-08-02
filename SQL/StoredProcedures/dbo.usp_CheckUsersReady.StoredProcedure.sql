USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckUsersReady]    Script Date: 8/1/2018 5:29:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[usp_CheckUsersReady]
	@UserID		int
,	@RoomID		int
as
DECLARE @HostPlayerID int = (SELECT HostPlayerID FROM Rooms WHERE RoomID = @RoomID)
DECLARE @JoinedPlayerID int = (SELECT JoinedPlayerID FROM Rooms WHERE RoomID = @RoomID)

if(@HostPlayerID = @UserID)
BEGIN
	SELECT isJoinedPlayerReady
	FROM Rooms
	WHERE RoomID = @RoomID
END
else
BEGIN
	SELECT	isHostPlayerReady
	FROM	Rooms 
	WHERE	RoomID	=	@RoomID
END


GO
