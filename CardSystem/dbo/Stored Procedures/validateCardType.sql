-- =============================================
-- Author:		<Satyadevi karki>
-- Create date: <Create Date,,>
-- Description:	<validateCard>
-- =============================================
CREATE PROCEDURE [dbo].[validateCardType]
	@CNumber nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	IF(@CNumber like '4%') and LEN(@CNumber)=16
		Select 'Visa' as Result
	ELSE IF (@CNumber like '5%') and LEN(@CNumber)=16
		Select 'MasterCard' as Result
	ELSE IF (@CNumber like '34%') and LEN(@CNumber)=15
		Select 'Amex' as Result
	ELSE IF (@CNumber like '37%') and LEN(@CNumber)=15
		Select 'Amex' as Result
	ELSE IF (@CNumber like '35283589%') and LEN(@CNumber)=16
		Select 'JCB' as Result
	ELSE IF LEN(@CNumber)=16
		Select 'Other' as Result
	ELSE 
		Select 'Invalid' as Result
END
