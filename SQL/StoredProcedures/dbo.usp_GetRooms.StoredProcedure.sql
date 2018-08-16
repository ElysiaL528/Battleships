USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRooms]    Script Date: 8/30/2017 3:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[usp_GetRooms]
as
SELECT	RoomID
,		RoomName 
,		HostPlayerID
FROM Rooms
WHERE JoinedPlayerID = null


GO
