USE [ElysiaLopezBattleships2017]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateUser]    Script Date: 8/29/2017 3:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC	[dbo].[usp_CreateUser]
@Username varchar(20)
as
INSERT INTO Users
VALUES (@Username)

GO
