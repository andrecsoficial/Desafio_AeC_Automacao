-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================



CREATE PROCEDURE [dbo].[NLogAddEntry] (
  @machineName nvarchar(200),
  @level varchar(5),
  @logged datetime,
  @userName nvarchar(200),
  @threadid nvarchar(200),
  @message nvarchar(max),
  @logger nvarchar(250),
  @exception nvarchar(max)
) AS
BEGIN
  INSERT INTO [dbo].[NLog] (
    [MachineName],
    [Level],
	[Logged],
    [UserName],
	[ThreadId],
    [Message],
	[Logger],
    [Exception]
  ) VALUES (
    @machineName,
    @level,
	@logged,
    @userName,
	@threadid,
    @message,
	@logger,
    @exception
  );
END

GO
