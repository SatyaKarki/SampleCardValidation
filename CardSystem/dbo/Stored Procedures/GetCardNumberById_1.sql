

CREATE PROCEDURE [dbo].[GetCardNumberById] 	
	@Id bigint
AS
BEGIN
	SELECT * From CardNumber WITH(NOLOCK) WHERE Id=@Id;
END
