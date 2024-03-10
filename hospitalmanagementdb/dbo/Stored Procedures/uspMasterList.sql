         -- exec    uspMasterList 'DOCTOR'           
CREATE PROCEDURE [dbo].[uspMasterList] 
  @Type VARCHAR(50)                
                 
AS                
BEGIN                
 SET NOCOUNT ON;                
 SET @Type = UPPER(@Type);                
                
 BEGIN TRY          
  SELECT 1 AS [Status], 'Data Fetched Successfully' AS [Message]    
  IF @Type = 'DOCTOR'                
   SELECT DoctorId AS ID                
    ,TRIM(FirstName +''+LastName) AS [Value]                
   FROM [dbo].[tblDoctor] WITH (NOLOCK)       
   WHERE IsDeleted = 0
   ORDER BY DoctorId ASC                
   ELSE 
               
   SELECT 0 AS Id                
    ,'' AS [Value]                
 END TRY                
                
 BEGIN CATCH                
  DECLARE @Msg VARCHAR(500) = Error_message();                
  DECLARE @ErrorSeverity INT = Error_severity();                
  DECLARE @ErrorState INT = Error_state();                
                
  RAISERROR (                
    @Msg                
    ,@ErrorSeverity                
    ,@ErrorState                
    );                
 END CATCH                
END