USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckForOpponent]    Script Date: 8/30/2017 3:12:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_CheckForOpponent]
@RoomID	int
as
SELECT JoinedPlayerID
FROM Rooms
WHERE RoomID = @RoomID
GO
