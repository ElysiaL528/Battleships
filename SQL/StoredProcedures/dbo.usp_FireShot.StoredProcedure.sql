USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_FireShot]    Script Date: 9/15/2017 2:49:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_FireShot]
	@UserID		int
,	@RoomID		int
,	@X			int
,	@Y			int
as

DECLARE @JoinedPlayerID int = (SELECT JoinedPlayerID FROM Rooms WHERE RoomID = @RoomID)
DECLARE @HostPlayerID int = (SELECT HostPlayerID FROM Rooms WHERE RoomID = @RoomID)

--if NOT EXISTS(SELECT ShotID FROM Shots WHERE X = @X AND Y = @Y AND RoomID = @RoomID AND UserID = @UserID)
	

GO
