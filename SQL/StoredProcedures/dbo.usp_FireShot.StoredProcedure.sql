USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_FireShot]    Script Date: 2/16/2018 12:39:46 PM ******/
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

DECLARE @ErrorMessage varchar(20)

IF NOT EXISTS(SELECT * FROM Shots WHERE @ShotX = X AND @ShotY = Y AND RoomID = @RoomID AND UserID = @UserID)
BEGIN

	DECLARE @hitShipID int = (SELECT	ShipID	FROM	UserShips	WHERE	(dbo.fn_intersects(@UserID, @RoomID, ShipID, @ShotX, @ShotY)) = 1 AND RoomID = @RoomID AND UserID = @UserID)

	DECLARE @isHit bit = 0

	IF @hitShipID IS NOT NULL
	BEGIN
		SET @isHit = 1
		UPDATE UserShips
		SET hitCount = hitCount + 1 WHERE ShipID = @hitShipID
		SET @ErrorMessage = 'Hit'
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
