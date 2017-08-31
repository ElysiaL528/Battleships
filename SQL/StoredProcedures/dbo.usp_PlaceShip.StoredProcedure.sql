USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 8/31/2017 2:22:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_PlaceShip]
	@ShipID				int
,	@UserID				int
,	@RoomID				int
,	@X					int
,	@Y					int
,	@ShipOrientationID	int
as
INSERT INTO UserShips
VALUES (@ShipID, @UserID, @RoomID, @X, @Y, @ShipOrientationID)
GO
