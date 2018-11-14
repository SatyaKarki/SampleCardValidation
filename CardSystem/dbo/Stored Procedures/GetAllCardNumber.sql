

CREATE PROCEDURE [dbo].[GetAllCardNumber] 	
AS
BEGIN
	SELECT * From CardNumber WITH(NOLOCK);
END
