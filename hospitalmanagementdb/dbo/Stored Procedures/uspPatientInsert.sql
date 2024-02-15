
CREATE PROCEDURE [dbo].[uspPatientInsert]  
 @FirstName VARCHAR(50), 
 @LastName VARCHAR(50), 
 @ContactNo VARCHAR(15),
 @Email VARCHAR(150), 
 @RoleId VARCHAR(50), 
 @Address VARCHAR(500),
 @HospitalName VARCHAR(100),
 @Zipcode VARCHAR(10),
 @Gender VARCHAR(10),
 @PasswordHash VARBINARY(MAX),  
 @PasswordSalt VARBINARY(MAX),  
 @CreatedBy VARCHAR(50) = null
 
AS
BEGIN
SET NOCOUNT ON;

	DECLARE @Status BIT = 0;  
	DECLARE @Message VARCHAR(MAX);  
	DECLARE @PatientId VARCHAR(50)=NEWID(); 

	BEGIN TRY     
	BEGIN 

		IF EXISTS( SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE Email = @Email AND IsDeleted = 0)
		BEGIN
		SELECT 0 AS [Status], 'Record with same email is already existed.' AS [Message]
		RETURN
		END

		IF NOT EXISTS( SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE UserId = @PatientId AND IsDeleted=0)
		BEGIN 
			INSERT INTO [dbo].[tblPatient]
			(
			   PatientId,
			   FirstName,
			   
			   LastName,
			   Email,
			   ContactNo,
			   PasswordHash, 
			   PasswordSalt, 
			   RoleId,
			   Address,
               HospitalName,
               Zipcode,
               Gender,
			   CreatedOn, 
			   CreatedBy,
			   IsDeleted
			
			)
			VALUES
			(
			   @PatientId, 
			   @FirstName,
			   
			   @LastName, 
			   @Email,
			   @ContactNo,
			   @PasswordHash,
			   @PasswordSalt,
			   @RoleId, 
			   @Address ,
 @HospitalName,
 @Zipcode,
 @Gender,
			   GETUTCDATE(),
			   @CreatedBy,
			   0
			)
		
		
			INSERT INTO [dbo].[tblModuleMapping]
			(
			 UserId,
			 ModuleId,
			 IsView, 
			 IsAdd, 
			 IsEdit, 
			 IsDelete,
			 CreatedBy, 
			 CreatedOn
			 
			)
			 SELECT @PatientId,
			 ModuleId, 
			 IsView,
			 IsAdd, 
			 IsEdit, 
			 IsDelete, 
			 @CreatedBy,
			 GETUTCDATE()
			 FROM [dbo].[tblDefaultRoleAccess] WITH(NOLOCK)   WHERE RoleId=@RoleId
			 
			SET @Status = 1;  
			SET @Message = 'Record added successfully.';
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

	SELECT @Status [Status], @Message [Message] , @PatientId [Data]  
END