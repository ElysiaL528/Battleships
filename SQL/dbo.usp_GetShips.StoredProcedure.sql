USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetShips]    Script Date: 11/3/2017 12:44:41 PM ******/
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
,			ShipOrientations.OrientationValueX
,			ShipOrientations.OrientationValueY
,			Ships.ShipLength
	FROM	UserShips
	JOIN	Ships
	ON		UserShips.ShipID	=	Ships.ShipID
	JOIN ShipOrientations
	ON UserShips.ShipOrientationID = ShipOrientations.ShipOrientationID
	WHERE UserID	=	@UserID AND UserShips.ShipOrientationID = ShipOrientations.ShipOrientationID

GO
