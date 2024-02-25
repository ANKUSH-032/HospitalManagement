CREATE PROCEDURE [dbo].[uspAppointmentDelete]  
 @AppointmentId VARCHAR(50),

 @DeletedBy VARCHAR(50) = null
 
AS
BEGIN		
SET NOCOUNT ON;

	DECLARE @Status BIT = 0;  
	DECLARE @Message VARCHAR(MAX);  


	BEGIN TRY     
	BEGIN 


		IF  EXISTS( SELECT 1 FROM [dbo].[tblAppointment] WITH(NOLOCK) WHERE AppointmentId = @AppointmentId AND IsDeleted=0)
		BEGIN 
			Update [dbo].[tblAppointment]
			SET IsDeleted = 1
			   WHERE AppointmentId = @AppointmentId AND IsDeleted=0 
			 
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

	SELECT @Status [Status], @Message [Message] , @AppointmentId [Data]  
END