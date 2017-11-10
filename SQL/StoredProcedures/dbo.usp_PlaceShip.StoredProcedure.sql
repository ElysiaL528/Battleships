USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 11/10/2017 12:47:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_PlaceShip]
	@UserID				int
,	@RoomID				int
,   @ShipID				int
,	@X					int
,	@Y					int
,	@ShipOrientationID	int
as

DECLARE @ErrorMessage varchar(20)		

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
WHERE s.ShipID = @ShipID
AND so.ShipOrientationID = @ShipOrientationID
AND us.UserID = @UserID
AND us.RoomID = @RoomID


SET @EndX	=	@X + (@shipLength - 1) * @changeX
SET @EndY	=	@Y + (@shipLength - 1) * @changeY

IF (@EndX > 10 OR @EndX < 0) OR (@EndY > 10 OR @EndY < 0)
BEGIN
	SET @ErrorMessage = 'Off the board'
END
ELSE
BEGIN

IF @EndX < @X
BEGIN
	DECLARE @tempX int = @EndX
	SET @EndX = @X
	SET @X = @tempX
END

IF @EndY < @Y
BEGIN
	DECLARE @tempY int = @EndY
	SET @EndY = @Y
	SET @Y = @tempY
END

if NOT EXISTS(SELECT ShipID FROM UserShips WHERE UserID = @UserID AND RoomID = @RoomID AND ShipID = @ShipID)
	BEGIN

	DECLARE	@StartCoordIsOverlapping	int =	(SELECT ShipID
										FROM	UserShips WHERE (dbo.fn_intersects(@UserID, @RoomID, ShipID, @X, @Y))= 1)

	DECLARE	@EndCoordIsOverlapping	int =	(SELECT ShipID
										FROM	UserShips WHERE (dbo.fn_intersects(@UserID, @RoomID, ShipID, @EndX, @EndY)) = 1)

	IF(@StartCoordIsOverlapping IS NULL AND @EndCoordIsOverlapping IS NULL)
		BEGIN
			INSERT INTO UserShips
			VALUES (@ShipID, @UserID, @RoomID, @X, @Y, @ShipOrientationID, 0, 0)

			SET @ErrorMessage = 'Placed ship'
		END
	ELSE
		BEGIN
			SET @ErrorMessage = 'Overlapping ship'
		END
	END
else
	BEGIN
		SET @ErrorMessage = 'Pre-existing ship'
	END

	

END
SELECT @ErrorMessage

GO
