
CREATE PROCEDURE [dbo].[uspAppointmentInsert]  
@AppointmentTitle VARCHAR(500),
@AppointmentDescription VARCHAR(500),
@AppointmentType VARCHAR(500),
@AppointmentDate VARCHAR(15),
@AppointmentTime VARCHAR(10),
@DoctorId VARCHAR(50),
@PatientId VARCHAR(50),
 @CreatedBy VARCHAR(50) = null
 
AS
BEGIN
SET NOCOUNT ON;

	DECLARE @Status BIT = 0;  
	DECLARE @Message VARCHAR(MAX);  
	DECLARE @AppointmentId VARCHAR(50)=NEWID(); 

	BEGIN TRY     
	BEGIN 

		

		IF NOT EXISTS( SELECT 1 FROM [dbo].[tblAppointment] WITH(NOLOCK) WHERE AppointmentId = @AppointmentId AND IsDeleted=0)
		BEGIN 
			INSERT INTO [dbo].[tblAppointment]
			(
			   AppointmentId,
			   AppointmentTitle,
			   AppointmentDescription,
			   AppointmentType,
			   AppointmentDate,
			   AppointmentTime,
			   DoctorId,
				PatientId,
			   CreatedOn, 
			   CreatedBy,
			   IsDeleted
			
			)
			VALUES
			(
			   @AppointmentId, 
			    @AppointmentTitle,
				@AppointmentDescription,
				@AppointmentType,
				@AppointmentDate,
				@AppointmentTime,
				@DoctorId,
				@PatientId,
			   GETUTCDATE(),
			   @CreatedBy,
			   0
			)
		
			 
			SET @Status = 1;  
			SET @Message = 'Record added successfully.';
		END
		ElSE
		BEGIN
				SELECT 0 AS [Status], 'Record with same id is already existed.' AS [Message]
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