  select COUNT(*), SUM(CASE WHEN STREAMID  IS NULL THEN 0 ELSE 1 END) AS VALIDN
  FROM [ICP3].[oa].[IMessages]
  where [CreateDate] >= DATEADD(DAY,-20, GETDATE())
  	and 
	(messageto like '%@cityocean.com%'
		or messagecc like '%@cityocean.com%'
		or messagefrom like '%@cityocean.com%')

 
		 

  select *
  FROM [ICP3].[oa].[IMessages]
  where [CreateDate] >= DATEADD(DAY,-1, GETDATE())
  	and 
	(messageto like '%rainwang@cityocean.net%'
		or messagecc like '%rainwang@cityocean.net%'
		or messagefrom like '%rainwang@cityocean.net%')
	and STREAMID is null
	and not MessageFrom like '%@cityocean.net%'
	order by messagefrom

SELECT *,messagefrom as bb
  FROM [ICP3].[oa].[IMessages]
  where [CreateDate] >= DATEADD(DAY,-1, GETDATE())
  	and 
	(messageto like '%@cityocean.com%'
		or messagecc like '%@cityocean.com%'
		or messagefrom like '%@cityocean.com%')
	and STREAMID is null
	order by messagefrom
	
	SELECT *
  FROM [ICP3].[oa].[IMessages]
  where [CreateDate] >= DATEADD(DAY,-1, GETDATE())
  	and 
	Subject like '%ArrivalNotice NO:OILAX14120753%'
	order by messagefrom

	  select *
  FROM [ICP3].[oa].[IMessages]
  where [CreateDate] >= DATEADD(DAY,-1, GETDATE())
	and STREAMID is null
	and (not MessageFrom like '%@cityocean.net%')
	and (not MessageFrom like '%@cityocean.com%')
	order by messagefrom