CREATE TABLE [dbo].[tblAppointment] (
    [AppointmentId]          VARCHAR (50)  NOT NULL,
    [AppointmentTitle]       VARCHAR (500) NOT NULL,
    [AppointmentDescription] VARCHAR (500) NOT NULL,
    [AppointmentType]        VARCHAR (500) NOT NULL,
    [AppointmentDate]        VARCHAR (15)  NOT NULL,
    [AppointmentTime]        VARCHAR (10)  NOT NULL,
    [DoctorId]               VARCHAR (50)  NULL,
    [PatientId]              VARCHAR (50)  NULL,
    [CreatedOn]              DATETIME      NULL,
    [CreatedBy]              VARCHAR (50)  NULL,
    [UpdatedOn]              DATETIME      NULL,
    [UpdatedBy]              VARCHAR (50)  NULL,
    [DeletedOn]              DATETIME      NULL,
    [DeletedBy]              VARCHAR (50)  NULL,
    [IsDeleted]              BIT           CONSTRAINT [DF_tblAppointment_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblAppointment] PRIMARY KEY CLUSTERED ([AppointmentId] ASC)
);

