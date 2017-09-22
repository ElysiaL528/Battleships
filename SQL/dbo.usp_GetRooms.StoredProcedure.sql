USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRooms]    Script Date: 9/22/2017 2:47:42 PM ******/
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
