USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_FireShot]    Script Date: 9/29/2017 1:53:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[usp_FireShot]
	@UserID		int
,	@RoomID		int
,	@ShotX			int	
,	@ShotY			int		
as

--Can be selected from tables
DECLARE @shipStartX			tinyint
DECLARE @shipStartY			tinyint
DECLARE @shipLength			tinyint
DECLARE @shipOrientationID	tinyint

--Can be derived from known data
DECLARE @shipEndX		tinyint
DECLARE @shipEndY		tinyint
DECLARE @changeX		int
DECLARE @changeY		int

DECLARE @shipID int

SELECT	@shipStartX			=	us.X,
		@shipStartY			=	us.Y,
		@shipLength			=	s.ShipLength,
		@shipOrientationID	=	us.ShipOrientationID
FROM	UserShips	us
JOIN	Ships		s
ON		s.ShipID			=	us.ShipID
WHERE	us.RoomID			=	@RoomID
AND		us.UserID			=	@UserID
AND		s.ShipID			=	@shipID


SELECT	@changeX	=	OrientationValueX,
		@changeY	=	OrientationValueY
FROM ShipOrientations
WHERE ShipOrientationID	=	@shipOrientationID

SET @shipEndX	=	@shipStartX + (@shipLength - 1) * @changeX
SET @shipEndY	=	@shipStartY	+ (@shipLength - 1) * @changeY

IF(@shipEndX < @shipStartX)
BEGIN
	DECLARE @tempX int	=	@shipStartX
	SET		@shipStartX = @shipEndX
	SET		@shipEndX = @tempX
END

IF(@shipEndY < @shipStartY)
BEGIN
	DECLARE @tempY int	=	@shipStartY
	SET		@shipStartY =	@shipEndY
	SET		@shipEndY	=	@tempY
END

IF EXISTS(	SELECT 1 AS IsHit
			WHERE	(@ShotX BETWEEN @shipStartX AND @shipEndX)
			AND		(@ShotY BETWEEN @shipStartY AND @shipEndY))
BEGIN
	print 'Hit'
END
ELSE
BEGIN
	print 'Miss'
END
	



GO
