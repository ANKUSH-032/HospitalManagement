      
CREATE PROCEDURE [dbo].[uspAppointmentListForAdmin] (        
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
        
 --  select * from [dbo].[tblAppointment]  
        
   SELECT         
 AppointmentTitle      
 ,AppointmentDescription      
 ,AppointmentType      
 ,AppointmentDate      
 ,AppointmentTime   
 ,p.FirstName + '' + p.LastName AS PatinetName  
 ,d.FirstName + '' + d.LastName AS DoctorName  
 ,(IIF(u.Status = 0, 'Pending','approved')) AS [Status]  
   FROM [dbo].[tblAppointment] u WITH(NOLOCK)   
   INNER JOIN [dbo].[tblPatient] p ON P.PatientId = u.PatientId  
   INNER JOIN [dbo].[tblDoctor] d ON D.DoctorId = u.DoctorId  
   WHERE u.[IsDeleted] = 0        
            AND (u.AppointmentTitle LIKE CONCAT ('%',@SearchKey,'%') OR         
   u.AppointmentTitle LIKE CONCAT ('%',@SearchKey,'%'))        
   ORDER BY        
   CASE        
   WHEN @SortCol = 'appointmentTitle_asc' THEN AppointmentTitle END ASC        
   ,---it will performing sortion based upon condition        
   CASE        
   WHEN @SortCol = 'appointmentTitle_desc' THEN AppointmentTitle END DESC,        
     CASE        
   WHEN @SortCol = 'patinetName_asc' THEN AppointmentTitle END ASC        
   ,---it will performing sortion based upon condition        
   CASE        
   WHEN @SortCol = 'patinetName_desc' THEN AppointmentTitle END DESC,      
   CASE        
   WHEN @SortCol = 'DoctorName_asc' THEN AppointmentTitle END ASC        
   ,---it will performing sortion based upon condition        
   CASE        
   WHEN @SortCol = 'DoctorName_desc' THEN AppointmentTitle END DESC,      
        
   CASE        
   WHEN @SortCol NOT IN         
   ('appointmentTitle_asc','appointmentTitle_desc') THEN u.[CreatedOn]        
   END DESC OFFSET @Start ROWS --- it will limit the no of pages        
        
        
        
FETCH NEXT (        
   CASE        
   WHEN @PageSize = - 1        
   THEN (SELECT COUNT(1) FROM  [dbo].[tblUsers] WITH(NOLOCK)  )        
   ELSE @PageSize        
   END        
   ) ROWS ONLY --- it will the next row         
        
        
        
-- TOTAL ROW COUNT        
   DECLARE @recordsTotal Int = (        
   SELECT COUNT(DISTINCT AppointmentId) FROM [dbo].[tblAppointment] WITH(NOLOCK)        
   WHERE [IsDeleted] = 0        
   AND (        
   AppointmentTitle LIKE CONCAT ('%',@SearchKey,'%')        
      )        
   ) --- it will fetch total row count from table        
        
        
        
SELECT @recordsTotal AS TotalRecords        
        
        
        
END        
END TRY        
BEGIN CATCH        
DECLARE @Msg nvarchar(100) =ERROR_MESSAGE() ;        
        
DECLARE @ErrorSeverity INT = ERROR_SEVERITY();        
DECLARE @ErrorState INT = ERROR_STATE();        
RAISERROR(@Msg, @ErrorSeverity, @ErrorState);        
END CATCH        
        
END