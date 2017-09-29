USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 9/29/2017 1:53:56 PM ******/
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
if NOT EXISTS(SELECT ShipID FROM UserShips WHERE UserID = @UserID AND RoomID = @RoomID AND ShipID = @ShipID)
	BEGIN
		INSERT INTO UserShips
		VALUES (@ShipID, @UserID, @RoomID, @X, @Y, @ShipOrientationID, 0)
	END

GO
