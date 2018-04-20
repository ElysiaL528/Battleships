USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckForNewShots]    Script Date: 3/2/2018 12:45:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_CheckForNewShots]
	@RoomID		int
,	@LastShotID	int
as

SELECT * 
FROM Shots
WHERE RoomID = @RoomID
AND ShotID > @LastShotID
GO
