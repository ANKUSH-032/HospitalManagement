    
CREATE PROCEDURE [dbo].[uspAppointmentUpdate] 
@AppointmentId VARCHAR(50),
@AppointmentTitle VARCHAR(500),
@AppointmentDescription VARCHAR(500),
@AppointmentType VARCHAR(500),
@AppointmentDate VARCHAR(15),
@AppointmentTime VARCHAR(10),
@DoctorId VARCHAR(50),
@PatientId VARCHAR(50),
 @UpdatedBy VARCHAR(50) = null    
     
AS    
BEGIN      
SET NOCOUNT ON;    
    
 DECLARE @Status BIT = 0;      
 DECLARE @Message VARCHAR(MAX);      
    
    
 BEGIN TRY         
 BEGIN     
    
  --IF EXISTS( SELECT 1 FROM [dbo].[tblDoctor] WITH(NOLOCK) WHERE Email = @Email AND IsDeleted = 1)    
  --BEGIN    
  --SELECT 0 AS [Status], 'User is not register in this system' AS [Message]    
  --RETURN    
  --END    
    
  IF  EXISTS( SELECT 1 FROM [dbo].[tblAppointment] WITH(NOLOCK) WHERE AppointmentId = @AppointmentId AND IsDeleted=0)    
  BEGIN     
   UPDATE [dbo].[tblAppointment]   
   SET    
		@AppointmentTitle=AppointmentTitle,
		@AppointmentDescription=AppointmentDescription,
		@AppointmentType=AppointmentType,
		@AppointmentDate=AppointmentDate,
		@AppointmentTime=AppointmentTime, 
         @DoctorId = DoctorId,
		 @PatientId=PatientId,
      UpdatedOn = GETUTCDATE(),     
      UpdatedBy = @UpdatedBy    
      WHERE AppointmentId = @AppointmentId AND IsDeleted=0  
   SET @Status = 1;      
   SET @Message = 'Record updated successfully.';    
  END    
  ElSE    
  BEGIN    
    SELECT 0 AS [Status], 'Record with same name is already existed.' AS [Message]    
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