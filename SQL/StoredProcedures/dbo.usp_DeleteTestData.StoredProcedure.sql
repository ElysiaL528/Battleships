USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteTestData]    Script Date: 2/23/2018 12:45:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[usp_DeleteTestData]
as

DELETE Shots 
WHERE isTestData = 1

DELETE UserShips 
WHERE isTestData = 1

DELETE Rooms 
WHERE isTestData = 1

DELETE Users 
WHERE isTestData = 1



GO
