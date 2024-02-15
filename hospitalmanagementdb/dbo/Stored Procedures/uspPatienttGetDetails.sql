-- exec uspUserGetDetails '2F8D4881-064D-4F66-9E75-5014C723DFBC'

CREATE PROCEDURE [dbo].[uspPatienttGetDetails]
@PatientId VARCHAR(50)
AS
BEGIN

SET NOCOUNT ON;

	DECLARE @Status BIT = 0;  
	DECLARE @Message VARCHAR(MAX);  


	BEGIN TRY     
	BEGIN 

		IF EXISTS( SELECT 1 FROM [dbo].[tblPatient] WITH(NOLOCK) WHERE Email = @PatientId AND IsDeleted = 1)
		BEGIN
		SELECT 0 AS [Status], 'User is not register in this system' AS [Message]
		RETURN
		END
		IF  EXISTS( SELECT 1 FROM [dbo].[tblPatient] WITH(NOLOCK) WHERE PatientId = @PatientId AND IsDeleted=0)
		BEGIN 
			
SELECT 1 AS [Status], 'Record get successfully.' AS [Message]
			SELECT 
			PatientId
,FirstName
,LastName
,Email
,Gender
,ContactNo
,PasswordHash
,PasswordSalt
,RoleId
,Active
,Address
,HospitalName
,Zipcode
,CreatedOn
,CreatedBy
,UpdatedOn
,UpdatedBy
,DeletedOn
,DeletedBy
,IsDeleted
			FROM [dbo].[tblPatient] WITH(NOLOCK) WHERE PatientId = @PatientId AND IsDeleted=0
		END
		ElSE
		BEGIN
				SELECT 0 AS [Status], 'No Data Found' AS [Message]
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

--	SELECT @Status [Status], @Message [Message] , @UserId [Data]  

END