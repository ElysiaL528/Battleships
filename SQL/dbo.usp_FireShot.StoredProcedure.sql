USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_FireShot]    Script Date: 9/22/2017 2:47:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[usp_FireShot]
	@UserID		int
,	@RoomID		int
,	@X			int
,	@Y			int
as

--Given as parameters
DECLARE @shotX	int
DECLARE @shotY	int

--Can be selected from tables
DECLARE @shipStartX			tinyint
DECLARE @shipStartY			tinyint
DECLARE @shipLength			tinyint
DECLARE @shipOrientationID	tinyint

--Can be derived from known data
DECLARE @shipEndX		tinyint
DECLARE @shipEndY		tinyint
DECLARE @changeX		tinyint
DECLARE @changeY		tinyint

SELECT	@changeX	=	OrientationValueX,
		@changeY	=	OrientationValueY
FROM ShipOrientations
WHERE ShipOrientationID	=	@shipOrientationID

SET @shipEndX	=	@shipStartX + (@shipLength - 1) * @changeX
SET @shipEndY	=	@shipStartY	+ (@shipLength - 1) * @changeY

IF(@ShipEndX > @ShipStartX)
BEGIN
	DECLARE @tempX int	=	@shipStartX
	SET		@shipStartX = @shipStartY
	SET		@shipStartY = @tempX
END

IF(@shipEndY > @shipStartY)
BEGIN
	DECLARE @tempY int	=	@shipStartY
	SET		@shipStartY =	@shipEndY
	SET		@shipEndY	=	@tempY
END

IF EXISTS(	SELECT 1 AS IsHit
			WHERE	(@shotX BETWEEN @shipStartX AND @shipEndX)
			AND		(@shotY BETWEEN @shipStartY AND @shipEndY))
BEGIN
	print 'Hit'
END
ELSE
BEGIN
	print 'Miss'
END
	


GO
