USE [ElysiaLopezBattleships2017]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_intersects]    Script Date: 10/6/2017 2:47:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_intersects](@UserID int, @RoomID int, @ShipID int, @InputX int, @InputY int)
RETURNS bit
AS
BEGIN

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

	SELECT	@shipStartX			=	us.X,
			@shipStartY			=	us.Y,
			@shipLength			=	s.ShipLength,
			@shipOrientationID	=	us.ShipOrientationID
	FROM	UserShips	us
	JOIN	Ships		s
	ON		s.ShipID			=	us.ShipID
	WHERE	us.RoomID			=	@RoomID
	AND		us.UserID			=	@UserID
	AND		s.ShipID			=	@ShipID


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

	DECLARE @isHit bit = 0

	IF EXISTS(	SELECT 1 AS IsHit
				WHERE	(@InputX BETWEEN @shipStartX AND @shipEndX)
				AND		(@InputY BETWEEN @shipStartY AND @shipEndY))
	BEGIN
		SET @isHit = 1
	END
	RETURN @isHit

END


GO
