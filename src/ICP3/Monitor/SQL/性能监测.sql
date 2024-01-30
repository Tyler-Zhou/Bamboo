DECLARE @OperationDate datetime
set @OperationDate = '2015-10-21'

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT U.ID, U.CName AS �û�, SUBSTRING(O.FULLCNAME, 21, 200) AS ����
INTO #TMP_USERS
FROM SM.USERS U INNER JOIN SM.VOrganizationUsers OU ON U.ID = OU.UserID
	INNER JOIN SM.Organizations O ON OU.OrganizationID = O.ID 
	WHERE OU.OrganizationIsValid = 1 AND OU.ISDEFAULT = 1

--SELECT [FunctionName] as ����, cast([OperationContent] as decimal(18,2)) / 1000 as ����, u.�û�, u.����, OperationDate as ����ʱ��
--  FROM [ICP3].[sm].[OperationLogs] o
--  inner join #TMP_USERS u on o.userid = u.id
--  where [FunctionName] = '�˵�����' 
--  order by cast([OperationContent] as int) desc

  SELECT [FunctionName] as ����, [OperationContent] as ����
	, isnull(cast([f6] as decimal(18,2)),0) / 1000 as ����
	,  u.�û�
	, u.����
	, OperationDate as ����ʱ��
  FROM [ICP3].[sm].[OperationLogs] o
  inner join #TMP_USERS u on o.userid = u.id
  where not [OperationContent] like 'login%' and LEN([OperationContent]) >= 5 and OperationDate >= @OperationDate
  order by 
  --����, 
  cast([f6] as int) desc

  SELECT ���� --,[OperationContent]
	 ,cast(avg(����) as decimal(18,1)) as ƽ������
	 ,max(����) as �������
	 ,min(����) as ��С����
	 ,count(*) as �ܴ���
	 ,sum(case when ����>=5 then 1 else 0 end) as �������ϴ���
	 ,cast(avg (case when ���� like '%����%' then ���� else null end) as decimal(18,1))as ����ƽ��
	,cast(avg (case when ���� like '%����%' then ���� else null end) as decimal(18,1))as ����ƽ��
	,cast(avg (case when ���� like '%����%' then ���� else null end) as decimal(18,1))as ����ƽ��
	,cast(avg (case when ���� like '%����%' then ���� else null end) as decimal(18,1))as ����ƽ��
	,cast(avg (case when ���� like '%�Ϻ�%' then ���� else null end) as decimal(18,1))as �Ϻ�ƽ��
	,cast(avg (case when ���� like '%�ൺ%' then ���� else null end) as decimal(18,1))as �ൺƽ��
	,cast(avg (case when ���� like '%���%' then ���� else null end) as decimal(18,1))as ���ƽ��
	,cast(avg (case when ���� like '%��������%' then ���� else null end) as decimal(18,1))as ����ƽ��
	,cast(avg (case when ���� like '%Խ��%' then ���� else null end) as decimal(18,1))as Խ��ƽ��
	,cast(avg (case when ���� like '%�Ĵ�����%' then ���� else null end) as decimal(18,1))as ����ƽ��
	,cast(avg (case when ���� like '%PACGRAN INC%' then ���� else null end) as decimal(18,1))as PACGRAN
	,cast(avg (case when ���� like '%��ɼ��˾%' then ���� else null end) as decimal(18,1))as ��ɼ�
	,cast(avg (case when ���� like '%ŦԼ��˾%' then ���� else null end) as decimal(18,1))as ŦԼ
	,cast(avg (case when ���� like '%�¸绪%' then ���� else null end) as decimal(18,1))as �¸绪
  from 
  (SELECT [FunctionName] as ����, 
	isnull(cast(cast([f6] as int) / 1000 as decimal(18,1)),0) as ����,  
	u.�û�, u.����, OperationDate as ����ʱ��
	  FROM [ICP3].[sm].[OperationLogs] o
	  inner join #TMP_USERS u on o.userid = u.id
	  where OperationDate >= @OperationDate-- dateadd(day, -7, getdate())
   ) tmp_log
  group by ����
  order by ����


  drop table #TMP_USERS