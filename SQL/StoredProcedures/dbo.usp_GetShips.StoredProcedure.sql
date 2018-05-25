USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetShips]    Script Date: 5/25/2018 11:50:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[usp_GetShips]
	@RoomID	int
,	@UserID	int
as
	SELECT	UserShips.X
,			UserShips.Y
,			ShipOrientations.ShipOrientationID
,			Ships.ShipLength
,			UserShips.HitCount
	FROM	UserShips
	JOIN	Ships
	ON		UserShips.ShipTypeID	=	Ships.ShipID
	JOIN ShipOrientations
	ON UserShips.ShipOrientationID = ShipOrientations.ShipOrientationID
	WHERE UserID	=	@UserID 
	AND RoomID		=	@RoomID



GO
