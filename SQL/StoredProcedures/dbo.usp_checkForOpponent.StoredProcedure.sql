USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_checkForOpponent]    Script Date: 8/29/2017 3:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_checkForOpponent]
@RoomID	int
as
SELECT JoinedPlayerID
FROM Rooms
WHERE RoomID = @RoomID
GO
