
CREATE PROCEDURE [dbo].[uspPatientGetList] (  
@Start INT = 0,  
@PageSize INT=-1,  
@SortCol VARCHAR(100) = NULL,  
@SearchKey VARCHAR(max) = ''  
)  
AS  
BEGIN  
SET NOCOUNT ON;  
BEGIN TRY  
BEGIN  
SET NOCOUNT ON;  
SET @SortCol = TRIM(ISNULL(@SortCol, ''));  
SET @SearchKey = TRIM(ISNULL(@SearchKey, ''));  
IF ISNULL(@Start, 0) = 0  
SET @Start = 0  
IF ISNULL(@PageSize, 0) <= 0  
SET @PageSize = - 1  
  
  
  
SELECT 1 AS [Status],'Data Fetched Successfully' AS [Message]  
  
  
  
   SELECT  
	PatientId,
	FirstName,
	LastName,
	Email,
	Gender,
	ContactNo,
	RoleId,
	Active,
	Address,
	HospitalName,
	Zipcode 
   FROM [dbo].[tblPatient] u WITH(NOLOCK)    
   WHERE u.[IsDeleted] = 0  
            AND (u.FirstName LIKE CONCAT ('%',@SearchKey,'%') OR   
   u.LastName LIKE CONCAT ('%',@SearchKey,'%'))  
   ORDER BY  
   CASE  
   WHEN @SortCol = 'firstName_asc' THEN FirstName END ASC  
   ,---it will performing sortion based upon condition  
   CASE  
   WHEN @SortCol = 'firstName_desc' THEN FirstName END DESC,  
  
   CASE  
   WHEN @SortCol = 'lastName_asc' THEN LastName END ASC  
   ,---it will performing sortion based upon condition  
   CASE  
   WHEN @SortCol = 'lastName_desc'THEN LastName END DESC,  
   CASE  
   WHEN @SortCol = 'contactNo_asc' THEN ContactNo END ASC  
   ,---it will performing sortion based upon condition  
   CASE  
   WHEN @SortCol = 'contactNo_desc' THEN ContactNo END DESC,  
  
   CASE  
   WHEN @SortCol NOT IN   
   ('firstName_asc','firstName_desc','lastName_asc','lastName_desc','contactNo_asc','contactNo_desc') THEN [CreatedOn]  
   END DESC OFFSET @Start ROWS --- it will limit the no of pages  
  
  
  
FETCH NEXT (  
   CASE  
   WHEN @PageSize = - 1  
   THEN (SELECT COUNT(1) FROM  [dbo].[tblPatient] WITH(NOLOCK)  )  
   ELSE @PageSize  
   END  
   ) ROWS ONLY --- it will the next row   
  
  
  
-- TOTAL ROW COUNT  
   DECLARE @recordsTotal Int = (  
   SELECT COUNT(DISTINCT PatientId) FROM [dbo].[tblPatient] WITH(NOLOCK)  
   WHERE [IsDeleted] = 0  
   AND (  
   [FirstName] LIKE CONCAT ('%',@SearchKey,'%')OR  
   [LastName] LIKE CONCAT ('%',@SearchKey,'%') OR    
   [Email] LIKE CONCAT ('%',@SearchKey,'%') OR  
   [ContactNo] LIKE CONCAT ('%',@SearchKey,'%')  
      )  
   ) --- it will fetch total row count from table  
  
  
  
SELECT @recordsTotal AS totalRecords  
  
  
  
END  
END TRY  
BEGIN CATCH  
DECLARE @Msg nvarchar(100) =ERROR_MESSAGE() ;  
  
DECLARE @ErrorSeverity INT = ERROR_SEVERITY();  
DECLARE @ErrorState INT = ERROR_STATE();  
RAISERROR(@Msg, @ErrorSeverity, @ErrorState);  
END CATCH  
  
END