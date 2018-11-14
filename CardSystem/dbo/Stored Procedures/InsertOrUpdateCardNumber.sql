
CREATE PROCEDURE [dbo].[InsertOrUpdateCardNumber]
		   @Id bigint,		   
		   @CardTypeId bigint,
		   @CNumber nvarchar(50),
		   @Expiry nvarchar(6),
		   @Active bit,
           @Editor bigint,
           @Modified datetime,
           @Author bigint,
           @Created datetime		   
AS
BEGIN
	IF(@Id =0) 
		BEGIN
			INSERT INTO [dbo].[CardNumber]
           ([CardTypeId]
		   ,[CNumber]
		   ,[Expiry]
           ,[Active]
           ,[Editor]
           ,[Modified]
           ,[Author]
           ,[Created])
			VALUES
			   (@CardTypeId
			   ,@CNumber
			   ,@Expiry
			   ,@Active
			   ,@Editor
			   ,@Modified
			   ,@Author
			   ,@Created);
			SELECT SCOPE_IDENTITY();
		END
	ELSE
		BEGIN
			UPDATE CardNumber WITH(XLOCK,ROWLOCK)
			SET
				CardTypeId=@CardTypeId,
			    CNumber =@CNumber,
			    Expiry =@Expiry,
				Active = @Active, 
				Editor = @Editor , 
				Modified = @Modified
			WHERE Id = @Id
			SELECT @Id;
		END
END
