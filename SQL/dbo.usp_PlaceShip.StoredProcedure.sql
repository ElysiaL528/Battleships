USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 9/22/2017 2:47:42 PM ******/
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
