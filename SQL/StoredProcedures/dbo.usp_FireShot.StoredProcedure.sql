USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_FireShot]    Script Date: 10/5/2018 2:23:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROC [dbo].[usp_FireShot]
	@UserID			int
,	@RoomID			int
,	@ShotX			int	
,	@ShotY			int		
as

DECLARE @ErrorMessage varchar(20)

IF NOT EXISTS(SELECT * FROM Shots WHERE @ShotX = X AND @ShotY = Y AND RoomID = @RoomID AND UserID = @UserID)
BEGIN

	DECLARE @hitShipID int = (SELECT	ShipID	FROM	UserShips	WHERE	(dbo.fn_intersects(@UserID, @RoomID, ShipID, @ShotX, @ShotY)) = 1 AND RoomID = @RoomID AND UserID = @UserID)

	DECLARE @isHit bit = 0

	DECLARE @IsTestData bit = (SELECT isTestData FROM Users WHERE UserID = @UserID)

	IF @hitShipID IS NOT NULL
	BEGIN
		SET @isHit = 1
		UPDATE UserShips
		SET hitCount = hitCount + 1 WHERE ShipID = @hitShipID
		DECLARE @hitCount int = (SELECT hitCount FROM UserShips WHERE ShipID = @hitShipID);
		DECLARE @shipSize int = (	SELECT s.ShipLength 
									FROM Ships s 
									JOIN UserShips us
									ON us.ShipTypeID = s.ShipID
									WHERE us.ShipID = @hitShipID);
		IF(@hitCount < @shipSize)
		BEGIN
			SET @ErrorMessage = 'Hit'
		END
		ELSE
		BEGIN
			DECLARE @shipType int = (SELECT ShipTypeID FROM UserShips WHERE ShipID = @hitShipID);
			SET @ErrorMessage = @shipType;
		END
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Miss'
	END

	INSERT INTO Shots
	VALUES (@UserID, @RoomID, @ShotX, @ShotY, @isHit, @IsTestData)

END 
ELSE 
BEGIN
SET @ErrorMessage = 'Pre-existing shot'
END

SELECT @ErrorMessage




GO
