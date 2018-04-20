USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteTestData]    Script Date: 3/2/2018 12:45:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[usp_DeleteTestData]
AS

DELETE Shots 
WHERE isTestData = 1

DELETE UserShips 
WHERE isTestData = 1

DELETE	UserShips
WHERE	RoomID IN
(
	SELECT	RoomID
	FROM	Rooms
	WHERE	isTestData = 1
)

DELETE Rooms 
WHERE isTestData = 1

DELETE Users 
WHERE isTestData = 1


GO
