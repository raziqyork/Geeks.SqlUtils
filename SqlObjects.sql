USE [BBC.TBS]
GO
/****** Object:  Table [dbo].[Entity range IDs]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entity range IDs](
	[Entity name] [nvarchar](50) NOT NULL,
	[Range id] [int] NOT NULL,
 CONSTRAINT [PK_Entity range IDs] PRIMARY KEY CLUSTERED 
(
	[Entity name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[Assets]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Assets]
AS
SELECT        
	dbo.ToGuid(2, fsdesc_id) AS Id
	, fsdesc_id AS LegacyId
	, dbo.ToGuid(1, fstocktypes_id) AS CategoryId
	, cast(case when fstocktypes_id = 19 then 1 else 0 end as bit) as IsKit
	, cast(istemplate as bit) as IsTemplate
	, cast(useserialnumbers as bit) as IsSerialised
	, cast(usesalestransactions as bit) as IsConsumable
	, cast(isunique as bit) as IsUnique
	, cast(insdatetime as datetime) as DateCreated
	, cast(enddatetime as datetime) as DateRemoved
	, dbo.OrNull(dbo.TrimSpace(sd_desc)) AS Name0
	, dbo.OrNull(dbo.TrimSpace(sd_desc1)) AS Name1
	, dbo.OrNull(dbo.TrimSpace(sd_desc2)) AS Name2
	, dbo.OrNull(dbo.TrimSpace(sd_code)) AS Code0
	, dbo.OrNull(dbo.TrimSpace(sd_code1)) AS Code1
	, dbo.OrNull(dbo.TrimSpace(sd_code2)) AS Code2
	, dbo.ToGuid(3, fmanufacturer_id) AS ManufacturerId
	, dbo.OrNull(dbo.TrimSpace(sd_terin)) AS CountryOfOrigin
	, isnull(sd_replace, 0) as ReplacementCost 
	, dailycost as DefaultChargeRate
	, resaleprice as ResalePrice
	, customsvalue as CustomsValue
	, itemweight as ItemWeight
	, warrantydays as WarrantyDays
	, prepdays as PrepDays
	, deprepdays as DeprepDays

	FROM PG2.tbsbbc.[public].sdesc
	WHERE fsdesc_id > 0




GO
/****** Object:  View [dbo].[Categories]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Categories]
AS
	SELECT 
		dbo.ToGuid(1, stocktypekey2) AS Id, 
		stocktypekey2 AS LegacyId, 
		dbo.ToTitleCase(dbo.TrimSpace(stocktypedesc2)) AS Name
		FROM  PG2.tbsbbc.[public].stocktype2
		WHERE (stocktypeactive2 = 1)



GO
/****** Object:  View [dbo].[Clients]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Clients]
AS
SELECT        
	dbo.ToGuid(4, clientkey) AS Id
	, clientkey AS LegacyId
	, dbo.OrNull(dbo.TrimSpace(clientname)) AS Name
	, dbo.OrNull(dbo.TrimSpace(clientcode)) AS Code
	, cast(clientactive as bit) as IsActive
	, dbo.OrNull(dbo.TrimSpace(clientaddress1)) AS AddressLine0
	, dbo.OrNull(dbo.TrimSpace(clientadd11)) AS AddressLine1
	, dbo.OrNull(dbo.TrimSpace(clientadd12)) AS AddressLine2
	, dbo.OrNull(dbo.TrimSpace(clientadd13)) AS AddressLine3
	, dbo.OrNull(dbo.TrimSpace(clientadd14)) AS AddressLine4
	, dbo.OrNull(dbo.TrimSpace(clientpost1)) AS AddressPostcode
	, dbo.OrNull(dbo.TrimSpace(clientcountry1)) AS AddressRegion
	, dbo.OrNull(dbo.TrimSpace(clientphone11)) AS Phone

	FROM PG2.tbsbbc.[public].clientlookup4
	WHERE clientkey > 0



GO
/****** Object:  View [dbo].[Contacts]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[Contacts]
AS
SELECT        
	dbo.ToGuid(2, fcontacts_id) AS Id
	, fcontacts_id AS LegacyId
	, dbo.OrNull(dbo.TrimSpace(cc_title)) AS Title
	, dbo.OrNull(dbo.TrimSpace(cc_name)) AS Name
	, dbo.OrNull(dbo.TrimSpace(cc_code)) AS Code

	, dbo.OrNull(dbo.TrimSpace(cc_email)) AS Email

	, dbo.ToGuid(4, fclients_id) AS ClientId
	, dbo.ToGuid(5, fjobtitles_id) AS JobTitleId

	, dbo.OrNull(dbo.TrimSpace(cc_phone)) AS Phone0
	, dbo.OrNull(dbo.TrimSpace(cc_phone2)) AS Phone1
	, dbo.OrNull(dbo.TrimSpace(cc_phone3)) AS Phone2

	, cast(insdatetime as datetime) as DateCreated
	, cast(enddatetime as datetime) as DateRemoved

	FROM PG2.tbsbbc.[public].contacts
	WHERE fcontacts_id > 0





GO
/****** Object:  View [dbo].[JobTitles]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[JobTitles]
AS
SELECT        
	dbo.ToGuid(5, fjobtitles_id) AS Id
	, fjobtitles_id AS LegacyId
	, dbo.OrNull(dbo.TrimSpace(jt_desc)) AS Name
	, dbo.OrNull(dbo.TrimSpace(jt_code)) AS Code

	, cast(jt_default as bit) as IsDefault

	, cast(insdatetime as datetime) as DateCreated
	, cast(enddatetime as datetime) as DateRemoved

	FROM PG2.tbsbbc.[public].jobtitles
	WHERE fjobtitles_id > 0





GO
/****** Object:  View [dbo].[Manufacturers]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Manufacturers]
AS
SELECT        
	dbo.ToGuid(3, fmanufacturer_id) AS Id
	, fmanufacturer_id AS LegacyId
	, dbo.OrNull(dbo.TrimSpace(fma_name)) AS Name
	, cast(insdatetime as datetime) as DateCreated
	, cast(enddatetime as datetime) as DateRemoved
	, dbo.OrNull(dbo.TrimSpace(fma_country)) AS CountryOfOrigin
	
	FROM PG2.tbsbbc.[public].manufacturer
	WHERE fmanufacturer_id > 0




GO
/****** Object:  View [dbo].[Users]    Script Date: 04/12/2013 21:47:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[Users]
AS
SELECT        
	dbo.ToGuid(6, usr_pkey) AS Id
	, usr_pkey AS LegacyId
	, dbo.OrNull(dbo.TrimSpace(userid)) AS Username
	, dbo.OrNull(dbo.TrimSpace(userformalname)) AS Name
	, dbo.OrNull(dbo.TrimSpace(usersortname)) AS SortName
	, dbo.OrNull(dbo.TrimSpace(userpassword)) AS [Password]
	, cast(active as bit) as IsActive
	, cast(issysadmin as bit) as IsSystemAdmin
	, cast(insdatetime as datetime) as DateCreated
	
	FROM PG2.tbsbbc.[public].usr
	WHERE usr_pkey > 0





GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Categories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Categories'
GO
