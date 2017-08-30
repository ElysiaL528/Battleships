USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetShips]    Script Date: 8/30/2017 3:12:41 PM ******/
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
,			UserShips.ShipOrientationID
,			Ships.ShipLength
	FROM	UserShips
	JOIN	Ships
	ON		UserShips.ShipID	=	Ships.ShipID
	WHERE UserID	=	@UserID
GO
