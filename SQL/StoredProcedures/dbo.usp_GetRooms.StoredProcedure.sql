USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRooms]    Script Date: 2/16/2018 12:39:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[usp_GetRooms]
as
SELECT	RoomID
,		RoomName 
,		HostPlayerID
,		us.Username
FROM Rooms r
JOIN Users us
ON us.UserID = r.HostPlayerID
WHERE JoinedPlayerID IS null




GO
