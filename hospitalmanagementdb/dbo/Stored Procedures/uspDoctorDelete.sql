CREATE PROCEDURE [dbo].[uspDoctorDelete]  
 @DoctorId VARCHAR(50),

 @DeletedBy VARCHAR(50) = null
 
AS
BEGIN		
SET NOCOUNT ON;

	DECLARE @Status BIT = 0;  
	DECLARE @Message VARCHAR(MAX);  


	BEGIN TRY     
	BEGIN 


		IF  EXISTS( SELECT 1 FROM [dbo].[tblDoctor] WITH(NOLOCK) WHERE DoctorId = @DoctorId AND IsDeleted=0)
		BEGIN 
			Update [dbo].[tblDoctor]
			SET IsDeleted = 1
			   WHERE DoctorId = @DoctorId AND IsDeleted=0 
			 
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

	SELECT @Status [Status], @Message [Message] , @DoctorId [Data]  
END