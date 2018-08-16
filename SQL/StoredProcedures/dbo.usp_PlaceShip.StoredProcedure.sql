USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 10/6/2017 2:47:56 PM ******/
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
		

DECLARE	@EndX	int
DECLARE	@EndY	int

DECLARE @shipLength	int
DECLARE	@changeX	int
DECLARE	@changeY	int

SELECT	@shipLength  = s.ShipLength,
		@changeX	=	so.OrientationValueX,
		@changeY	=	so.OrientationValueY
FROM	UserShips	us
JOIN	Ships	s
ON		us.ShipID	=	s.ShipID
JOIN	ShipOrientations	so
ON		us.ShipOrientationID	=	so.ShipOrientationID
WHERE us.ShipID	=	@ShipID


SET @EndX	=	@X + (@shipLength - 1) * @changeX
SET @EndY	=	@Y + (@shipLength - 1) * @changeY

DECLARE	@StartCoordIsOverlapping	int =	(SELECT (dbo.fn_intersects(@UserID, @RoomID, ShipID, @X, @Y))
										FROM	UserShips)

DECLARE	@EndCoordIsOverlapping	int =	(SELECT (dbo.fn_intersects(@UserID, @RoomID, ShipID, @EndX, @EndY))
										FROM	UserShips)

if NOT EXISTS(SELECT ShipID FROM UserShips WHERE UserID = @UserID AND RoomID = @RoomID AND ShipID = @ShipID)
	BEGIN
		INSERT INTO UserShips
		VALUES (@ShipID, @UserID, @RoomID, @X, @Y, @ShipOrientationID, 0, 0)
	END


GO
