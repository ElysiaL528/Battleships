USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_FireShot]    Script Date: 10/6/2017 2:47:56 PM ******/
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

IF NOT EXISTS(SELECT * FROM Shots WHERE @ShotX = X AND @ShotY = Y)
BEGIN

	DECLARE		@isHit	bit	=	(SELECT (dbo.fn_intersects(@UserID, @RoomID, ShipID, @ShotX, @ShotY))
								FROM	UserShips)

	INSERT INTO Shots
	VALUES (@UserID, @RoomID, @ShotX, @ShotY, @isHit)
END




GO
