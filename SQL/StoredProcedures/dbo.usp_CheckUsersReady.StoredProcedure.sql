USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckUsersReady]    Script Date: 9/1/2017 3:57:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_CheckUsersReady]
@RoomID		int
as
SELECT	isHostPlayerReady, isJoinedPlayerReady
FROM	Rooms 
WHERE	RoomID	=	@RoomID

GO
