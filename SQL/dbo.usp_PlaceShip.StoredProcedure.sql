USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 10/27/2017 12:46:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROC [dbo].[usp_PlaceShip]
	@UserID				int
,	@RoomID				int
,   @ShipID				int
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


SET @EndX	=	@X + (@shipLength - 1) * @changeX
SET @EndY	=	@Y + (@shipLength - 1) * @changeY

DECLARE	@StartCoordIsOverlapping	int =	(SELECT ShipID
										FROM	UserShips WHERE (dbo.fn_intersects(@UserID, @RoomID, ShipID, @X, @Y))= 1)

DECLARE	@EndCoordIsOverlapping	int =	(SELECT ShipID
										FROM	UserShips WHERE (dbo.fn_intersects(@UserID, @RoomID, ShipID, @EndX, @EndY)) = 1)

if NOT EXISTS(SELECT ShipID FROM UserShips WHERE UserID = @UserID AND RoomID = @RoomID AND ShipID = @ShipID)
	BEGIN
		INSERT INTO UserShips
		VALUES (@ShipID, @UserID, @RoomID, @X, @Y, @ShipOrientationID, 0, 0, null)

		print 'Placed ship'
	END
else
	BEGIN
		print 'Invalid placement'
	END

	SELECT ShipID
	FROM UserShips
	WHERE ShipID = @ShipID
	AND UserID = @UserID
	AND RoomID = @RoomID


GO
