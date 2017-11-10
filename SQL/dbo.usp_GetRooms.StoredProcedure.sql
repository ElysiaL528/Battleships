USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRooms]    Script Date: 11/3/2017 12:44:41 PM ******/
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
WHERE JoinedPlayerID IS null



GO
