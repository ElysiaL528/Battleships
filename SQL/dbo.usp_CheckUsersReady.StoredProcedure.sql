USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckUsersReady]    Script Date: 9/22/2017 2:47:42 PM ******/
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
