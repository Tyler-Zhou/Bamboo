DECLARE @OperationDate datetime
set @OperationDate = '2015-10-21'

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT U.ID, U.CName AS 用户, SUBSTRING(O.FULLCNAME, 21, 200) AS 部门
INTO #TMP_USERS
FROM SM.USERS U INNER JOIN SM.VOrganizationUsers OU ON U.ID = OU.UserID
	INNER JOIN SM.Organizations O ON OU.OrganizationID = O.ID 
	WHERE OU.OrganizationIsValid = 1 AND OU.ISDEFAULT = 1

--SELECT [FunctionName] as 动作, cast([OperationContent] as decimal(18,2)) / 1000 as 秒数, u.用户, u.部门, OperationDate as 操作时间
--  FROM [ICP3].[sm].[OperationLogs] o
--  inner join #TMP_USERS u on o.userid = u.id
--  where [FunctionName] = '账单保存' 
--  order by cast([OperationContent] as int) desc

  SELECT [FunctionName] as 功能, [OperationContent] as 动作
	, isnull(cast([f6] as decimal(18,2)),0) / 1000 as 秒数
	,  u.用户
	, u.部门
	, OperationDate as 操作时间
  FROM [ICP3].[sm].[OperationLogs] o
  inner join #TMP_USERS u on o.userid = u.id
  where not [OperationContent] like 'login%' and LEN([OperationContent]) >= 5 and OperationDate >= @OperationDate
  order by 
  --部门, 
  cast([f6] as int) desc

  SELECT 功能 --,[OperationContent]
	 ,cast(avg(秒数) as decimal(18,1)) as 平均秒数
	 ,max(秒数) as 最大秒数
	 ,min(秒数) as 最小秒数
	 ,count(*) as 总次数
	 ,sum(case when 秒数>=5 then 1 else 0 end) as 五秒以上次数
	 ,cast(avg (case when 部门 like '%深圳%' then 秒数 else null end) as decimal(18,1))as 深圳平均
	,cast(avg (case when 部门 like '%广州%' then 秒数 else null end) as decimal(18,1))as 广州平均
	,cast(avg (case when 部门 like '%厦门%' then 秒数 else null end) as decimal(18,1))as 厦门平均
	,cast(avg (case when 部门 like '%宁波%' then 秒数 else null end) as decimal(18,1))as 宁波平均
	,cast(avg (case when 部门 like '%上海%' then 秒数 else null end) as decimal(18,1))as 上海平均
	,cast(avg (case when 部门 like '%青岛%' then 秒数 else null end) as decimal(18,1))as 青岛平均
	,cast(avg (case when 部门 like '%天津%' then 秒数 else null end) as decimal(18,1))as 天津平均
	,cast(avg (case when 部门 like '%马来西亚%' then 秒数 else null end) as decimal(18,1))as 马来平均
	,cast(avg (case when 部门 like '%越南%' then 秒数 else null end) as decimal(18,1))as 越南平均
	,cast(avg (case when 部门 like '%澳大利亚%' then 秒数 else null end) as decimal(18,1))as 澳洲平均
	,cast(avg (case when 部门 like '%PACGRAN INC%' then 秒数 else null end) as decimal(18,1))as PACGRAN
	,cast(avg (case when 部门 like '%洛杉矶公司%' then 秒数 else null end) as decimal(18,1))as 洛杉矶
	,cast(avg (case when 部门 like '%纽约公司%' then 秒数 else null end) as decimal(18,1))as 纽约
	,cast(avg (case when 部门 like '%温哥华%' then 秒数 else null end) as decimal(18,1))as 温哥华
  from 
  (SELECT [FunctionName] as 功能, 
	isnull(cast(cast([f6] as int) / 1000 as decimal(18,1)),0) as 秒数,  
	u.用户, u.部门, OperationDate as 操作时间
	  FROM [ICP3].[sm].[OperationLogs] o
	  inner join #TMP_USERS u on o.userid = u.id
	  where OperationDate >= @OperationDate-- dateadd(day, -7, getdate())
   ) tmp_log
  group by 功能
  order by 功能


  drop table #TMP_USERS