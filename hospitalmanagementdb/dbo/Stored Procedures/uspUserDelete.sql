CREATE PROCEDURE [dbo].[uspUserDelete]  
 @UserId VARCHAR(50),

 @DeletedBy VARCHAR(50) = null
 
AS
BEGIN		
SET NOCOUNT ON;

	DECLARE @Status BIT = 0;  
	DECLARE @Message VARCHAR(MAX);  


	BEGIN TRY     
	BEGIN 

		IF EXISTS( SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE Email = @UserId AND IsDeleted = 1)
		BEGIN
		SELECT 0 AS [Status], 'User is not register in this system' AS [Message]
		RETURN
		END

		IF  EXISTS( SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE UserId = @UserId AND IsDeleted=0)
		BEGIN 
			Update [dbo].[tblUsers]
			SET IsDeleted = 1
			   WHERE UserId = @UserId AND IsDeleted=0 
			 
			SET @Status = 1;  
			SET @Message = 'Record Deleted successfully.';
		END
		ElSE
		BEGIN
				SELECT 0 AS [Status], 'Record with same name is already deleted.' AS [Message]
				RETURN
		END
	END   
	END TRY   
	BEGIN CATCH  
		SET @Message = ERROR_MESSAGE();  
  
		DECLARE @ErrorSeverity INT = ERROR_SEVERITY();  
		DECLARE @ErrorState INT = ERROR_STATE();    
		RAISERROR(@Message, @ErrorSeverity, @ErrorState);  
	END CATCH  

	SELECT @Status [Status], @Message [Message] , @UserId [Data]  
END