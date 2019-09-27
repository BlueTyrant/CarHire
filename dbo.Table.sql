CREATE TABLE [dbo].[Car](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VehicleRegNo] [varchar](10) NOT NULL,
	[Make] [varchar](50) NOT NULL,
	[EngineSize] [varchar](10) NOT NULL,
	[DateRegistered] [date] NULL,
	[RentalPerDay] [decimal](8, 2) NOT NULL,
	[Available] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleRegNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO