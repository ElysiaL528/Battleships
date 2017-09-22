USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckForNewShots]    Script Date: 9/22/2017 2:47:42 PM ******/
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
