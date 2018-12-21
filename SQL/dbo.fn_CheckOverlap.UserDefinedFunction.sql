USE [ElysiaLopezBattleships2017]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CheckOverlap]    Script Date: 12/21/2018 2:45:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_CheckOverlap]
(
	@UserID int,
	@RoomID int,
	@ShipID int,
	@ShipTypeID int,
	@InputX int,
	@InputY int,
	@InputOrientationID int
)
RETURNS bit
AS
BEGIN
	DECLARE @isOverlapping bit = 0

	--Can be selected from tables
	DECLARE @shipStartX			int
	DECLARE @shipStartY			int
	DECLARE @shipLength			int
	DECLARE @shipOrientationID	int

	--Can be derived from known data
	DECLARE @shipEndX		int
	DECLARE @shipEndY		int
	DECLARE @changeX		int
	DECLARE @changeY		int

	SELECT	@shipStartX			=	us.X,
			@shipStartY			=	us.Y,
			@shipLength			=	s.ShipLength,
			@shipOrientationID	=	us.ShipOrientationID
	FROM	UserShips	us
	JOIN	Ships		s
	ON		s.ShipID			=	us.ShipTypeID
	WHERE	us.RoomID			=	@RoomID
	AND		us.UserID			=	@UserID
	AND		us.ShipID			=	@ShipID

	SELECT	@changeX	=	OrientationValueX,
			@changeY	=	OrientationValueY
	FROM ShipOrientations
	WHERE ShipOrientationID	=	@shipOrientationID

	SET @shipEndX	=	@shipStartX + (@shipLength - 1) * @changeX
	SET @shipEndY	=	@shipStartY	+ (@shipLength - 1) * @changeY

	DECLARE @InputtedShipLength int = (SELECT ShipLength FROM Ships WHERE ShipID = @ShipTypeID)

	DECLARE @counter int = 0

	IF @InputOrientationID = 1 OR @InputOrientationID = 2
		BEGIN
		WHILE @counter < @InputtedShipLength
			BEGIN
				IF EXISTS(	SELECT 1 AS IsHit
							WHERE	(@InputX BETWEEN @shipStartX AND @shipEndX)
							AND		(@InputY + @counter BETWEEN @shipStartY AND @shipEndY))
				BEGIN
					SET @isOverlapping = 1
				END

				SET @counter += 1
			END
		END
	ELSE
		BEGIN
			WHILE @counter < @InputtedShipLength
			BEGIN
				IF EXISTS(	SELECT 1 AS IsHit
							WHERE	(@InputX + @counter BETWEEN @shipStartX AND @shipEndX)
							AND		(@InputY BETWEEN @shipStartY AND @shipEndY))
				BEGIN
					SET @isOverlapping = 1
				END

				SET @counter += 1
			END
		END
	

	-- Return the result of the function
	RETURN @isOverlapping

END


GO
