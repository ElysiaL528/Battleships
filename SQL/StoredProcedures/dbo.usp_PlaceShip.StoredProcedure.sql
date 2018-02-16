USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_PlaceShip]    Script Date: 2/16/2018 12:39:46 PM ******/
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
,	@IsTestData			bit
as

DECLARE @ErrorMessage varchar(21)		

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

DECLARE @HostPlayerID int = (SELECT HostPlayerID FROM Rooms WHERE RoomID = @RoomID)
DECLARE @JoinedPlayerID int = (SELECT JoinedPlayerID FROM Rooms WHERE RoomID = @RoomID)


IF (@HostPlayerID != @UserID AND @JoinedPlayerID != @UserID)
	BEGIN
	SET @ErrorMessage = 'Player is not in room'
	END
ELSE
BEGIN
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

		DECLARE	@StartCoordIsOverlapping	int =	(SELECT TOP 1 ShipID
											FROM	UserShips WHERE (dbo.fn_intersects(@UserID, @RoomID, ShipID, @X, @Y))= 1 AND RoomID = @RoomID)

		DECLARE	@EndCoordIsOverlapping	int =	(SELECT TOP 1 ShipID
											FROM	UserShips WHERE (dbo.fn_intersects(@UserID, @RoomID, ShipID, @EndX, @EndY)) = 1 AND RoomID = @RoomID)

		IF(@StartCoordIsOverlapping IS NULL AND @EndCoordIsOverlapping IS NULL)
			BEGIN
				INSERT INTO UserShips
				VALUES (@ShipID, @UserID, @RoomID, @X, @Y, @ShipOrientationID, 0, 0, 0)

				SET @ErrorMessage = 'Placed ship'
			END
		ELSE
			BEGIN
				SET @ErrorMessage = 'Overlapping ship'
			END
		END
	ELSE
		BEGIN
			SET @ErrorMessage = 'Pre-existing ship'
		END

	--SELECT dbo.fn_intersects(2, 22, ShipID, 1, 1), RoomID, UserID FROM UserShips

	END
END

SELECT @ErrorMessage



GO
