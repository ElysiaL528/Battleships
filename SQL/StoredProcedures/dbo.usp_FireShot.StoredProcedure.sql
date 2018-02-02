USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_FireShot]    Script Date: 2/2/2018 11:39:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROC [dbo].[usp_FireShot]
	@UserID			int
,	@RoomID			int
,	@ShotX			int	
,	@ShotY			int		
,	@IsTestData		bit
as

IF NOT EXISTS(SELECT * FROM Shots WHERE @ShotX = X AND @ShotY = Y)
BEGIN

	DECLARE @hitShipID int = (SELECT	ShipID	FROM	UserShips	WHERE	(dbo.fn_intersects(@UserID, @RoomID, ShipID, @ShotX, @ShotY)) = 1 AND RoomID = @RoomID AND UserID = @UserID)

	DECLARE @isHit bit = 0

	IF @hitShipID IS NOT NULL
	BEGIN
		SET @isHit = 1
		UPDATE UserShips
		SET hitCount = hitCount + 1 WHERE ShipID = @hitShipID
	END

	print @isHit

	INSERT INTO Shots
	VALUES (@UserID, @RoomID, @ShotX, @ShotY, @isHit, @IsTestData)

	RETURN @isHit
END 







GO
