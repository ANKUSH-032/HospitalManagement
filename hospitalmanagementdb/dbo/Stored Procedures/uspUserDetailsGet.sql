      
CREATE PROC [dbo].[uspUserDetailsGet]      
(                      
 @UserId VARCHAR(50) = NULL,                       
 @Email VARCHAR(150) = NULL                      
)                      
AS                      
BEGIN                      
 SET NOCOUNT ON;                      
 DECLARE @Status BIT=0;                      
 DECLARE @Msg VARCHAR(4000);      
                      
 BEGIN TRY                    
  IF @UserId IS NULL AND @Email IS NULL                      
  BEGIN                      
   SET @Status=0;                      
   SET @Msg='UserId or Email is required';                       
  END                      
  ELSE                       
  BEGIN                    
   IF EXISTS(SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE (UserId= TRIM(@UserId) OR Email = TRIM(@Email)) AND IsDeleted = 0)                    
   BEGIN                    
    SELECT        
     U.[UserId],                    
     [Email],                    
     U.[PasswordHash],                    
     U.[PasswordSalt],                    
     ISNULL([FirstName],'') AS [Name],                                 
    U.[RoleId] AS [Role]                     
    FROM [dbo].[tblUsers] U WITH(NOLOCK)                                             
    WHERE (U.UserId=@UserId OR U.Email = @Email)                 
                     
    SET @Status=1;                    
    SET @Msg='Success';                  
    SELECT @Status [Status], @Msg [Message]                    
    RETURN                    
   END  IF EXISTS(SELECT 1 FROM [dbo].[tblPatient] WITH(NOLOCK) WHERE (PatientId= TRIM(@UserId) OR Email = TRIM(@Email)) AND IsDeleted = 0)                    
   BEGIN                    
    SELECT        
    U.[PatientId] AS UserId,                    
    [Email],                    
    U.[PasswordHash],                    
    U.[PasswordSalt],                    
    ISNULL([FirstName],'') AS [Name],                                 
    U.[RoleId] AS [Role]                     
    FROM [dbo].[tblPatient] U WITH(NOLOCK)                                             
    WHERE (U.PatientId=@UserId OR U.Email = @Email)                 
                     
    SET @Status=1;                    
    SET @Msg='Success';                  
    SELECT @Status [Status], @Msg [Message]                    
    RETURN    
 END  
   ELSE  IF EXISTS(SELECT 1 FROM [dbo].[tblDoctor] WITH(NOLOCK) WHERE (DoctorId = TRIM(@UserId) OR Email = TRIM(@Email)) AND IsDeleted = 0)                    
   BEGIN                    
    SELECT        
    U.[DoctorId] AS UserId,                    
    [Email],                    
    U.[PasswordHash],                    
    U.[PasswordSalt],                    
    ISNULL([FirstName],'') AS [Name],                                 
    U.[RoleId] AS [Role]                     
    FROM [dbo].[tblDoctor] U WITH(NOLOCK)                                             
    WHERE (U.DoctorId=@UserId OR U.Email = @Email)                 
                     
    SET @Status=1;                    
    SET @Msg='Success';                  
    SELECT @Status [Status], @Msg [Message]                    
    RETURN    
 END                  
   BEGIN                    
    IF NOT EXISTS (SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE (UserId= TRIM(@UserId) OR Email = TRIM(@Email)) AND IsDeleted = 0 )                    
   SELECT 'UserNotRegister' AS [Name]       
    ELSE IF EXISTS (SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE (UserId= TRIM(@UserId) OR Email = TRIM(@Email)) AND IsDeleted = 1 )                    
   SELECT 'DELETED' AS [Name]      
   SET @Status=0;                    
   SET @Msg='Failure';                    
   SELECT @Status [Status], @Msg [Message]                    
    RETURN                    
   END                          
  END                       
 END TRY                    
 BEGIN CATCH                    
  SET @Status=0;                    
  SET @Msg= ERROR_MESSAGE();                       
                      
  DECLARE @ErrorSeverity INT = ERROR_SEVERITY();                           
  DECLARE @ErrorState INT = ERROR_STATE();                           
  RAISERROR(@Msg,@ErrorSeverity,@ErrorState);                     
                      
  SELECT @Status [Status], @Msg [Message]                    
 END CATCH                     
END                    
                      
                      
/*                       
EXEC uspUserDetailsGet                      
@UserId = '',                      
@Email = 'rahul@osplabs.com'                      
*/