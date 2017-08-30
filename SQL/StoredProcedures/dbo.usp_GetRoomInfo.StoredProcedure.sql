USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRoomInfo]    Script Date: 8/30/2017 3:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetRoomInfo]
@RoomID		int
AS
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
