/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [ICP3].[oa].[IMessages]
    where [CreateDate] >= DATEADD(DAY,-7, GETDATE())
	and  streamid is null
	and 
	(messageto like '%rickydeng@cityocean.com%'
		or messagecc like '%rickydeng@cityocean.com%')
		and 
			(messagefrom = 'natalie@cityocean.net')
		


	
		

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [ICP3].[oa].[IMessages]
    where subject = 'ArrivalNotice NO:OILAX14121100'
	-- messageid like '%201501040729.t047tfnz009356%'


SELECT *
  FROM [ICP3].[oa].[IMessages]
  where messageid like '%mail.cityocean.net%'
		

		select * 
		from icp3.fcm.OperationMessages
		where imessageid = 'C8CC37E8-ED9A-E411-A309-0026551CA878'


		SELECT *
  FROM [ICP3].[oa].[IMessages]
    where [CreateDate] >= DATEADD(DAY,-7, GETDATE())
	and  streamid is null
	and 
	(messageto like '%rainwang@cityocean.net%'
		or messagecc like '%rainwang@cityocean.net%'
		or 
			messagefrom = 'rainwang@cityocean.net')