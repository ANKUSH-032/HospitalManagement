      
CREATE PROCEDURE [dbo].[uspAppointmentSatusUpdate]    
@AppointmentId VARCHAR(50)    
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
      
       
   UPDATE [dbo].[tblAppointment]     
   SET      
  
  Status = 1,     
  UpdatedOn = GETUTCDATE()     
  WHERE AppointmentId = @AppointmentId AND IsDeleted=0    
   SET @Status = 1;        
   SET @Message = 'Record updated successfully.';      
       
 
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