USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 9/1/2017 3:57:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[usp_PlaceShip]
	@ShipID				int
,	@UserID				int
,	@RoomID				int
,	@X					tinyint
,	@Y					tinyint
,	@ShipOrientationID	int
as
if NOT EXISTS(SELECT ShipID FROM UserShips WHERE UserID = @UserID)
	BEGIN
		INSERT INTO UserShips
		VALUES (@ShipID, @UserID, @RoomID, @X, @Y, @ShipOrientationID)
	END

GO
