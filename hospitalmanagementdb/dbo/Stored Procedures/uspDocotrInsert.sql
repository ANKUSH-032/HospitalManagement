
CREATE PROCEDURE [dbo].[uspDocotrInsert]  
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
	DECLARE @DoctorId VARCHAR(50)=NEWID(); 

	BEGIN TRY     
	BEGIN 

		IF EXISTS( SELECT 1 FROM [dbo].[tblUsers] WITH(NOLOCK) WHERE Email = @Email AND IsDeleted = 0)
		BEGIN
		SELECT 0 AS [Status], 'Record with same email is already existed.' AS [Message]
		RETURN
		END

		IF NOT EXISTS( SELECT 1 FROM [dbo].[tblDoctor] WITH(NOLOCK) WHERE DoctorId = @DoctorId AND IsDeleted=0)
		BEGIN 
			INSERT INTO [dbo].[tblDoctor]
			(
			   DoctorId,
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
			   @DoctorId, 
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

	SELECT @Status [Status], @Message [Message] , @DoctorId [Data]  
END