
--获取操作员列表
SELECT SUBSTRING(O.FullCName,26,100) AS 分公司, op.UserEname as Operator, OP.UserID AS OperatiorID
         INTO #TMP_OP
         FROM sm.VOperator op
			INNER JOIN SM.Organizations O ON op.DeptID = O.ID 

DECLARE @Beginning_CreateDate DATETIME
SET @Beginning_CreateDate = DATEADD(DAY,-3, GETDATE())

         --SELECT * FROM #TMP_OP


--获取按业务统计的文档数量
SELECT CreateBy, CONVERT(varchar(40), CreateDate, 102) AS CreateDate, count(*) as 上传文档总数 
         INTO #TMP_DOC_Count
         FROM fcm.OperationFiles f
         WHERE FileSource = 1
			AND CreateDate >= @Beginning_CreateDate
         GROUP BY CreateBy, CONVERT(varchar(40), CreateDate, 102)


--获取按业务统计的邮件数量(总数，服务端备份，客户端备份)
SELECT om.CreateBy, CONVERT(varchar(40), om.CreateDate, 102) AS CreateDate
         , count(*) as 关联邮件总数
         , sum(CASE WHEN m.StreamID IS NULL THEN 0 ELSE 1 END) AS 上传邮件总数
         INTO #TMP_Mail_Count
         FROM fcm.OperationMessages om
                   INNER JOIN oa.IMessages m ON om.IMessageID = m.ID
		 WHERE om.CreateDate >= @Beginning_CreateDate
         GROUP BY om.CreateBy, CONVERT(varchar(40), om.CreateDate, 102)


--获取按业务统计的事件记录()
SELECT m.CreateBy, CONVERT(varchar(40), m.CreateDate, 102) AS CreateDate
         , MAX(CASE WHEN 
                                               (NOT oe.MessageID IS NULL OR NOT oe.MailMsgID IS NULL) 
                                               AND oev.Code = 'ANRC'
                                     THEN 1 ELSE 0 END ) as ANRC
         , MAX(CASE WHEN 
                                               (NOT oe.MessageID IS NULL OR NOT oe.MailMsgID IS NULL) 
                                               AND oev.Code = 'ANSC'
                                     THEN 1 ELSE 0 END ) as ANSC
         , MAX(CASE WHEN 
                                               (NOT oe.MessageID IS NULL OR NOT oe.MailMsgID IS NULL) 
                                               AND oev.Code = 'DOS'
                                     THEN 1 ELSE 0 END ) as [DOS]
         , MAX(CASE WHEN 
                                               (NOT oe.MessageID IS NULL OR NOT oe.MailMsgID IS NULL) 
                                               AND oev.Code = 'PayNt'
                                     THEN 1 ELSE 0 END ) as [PayNt]
         , MAX(CASE WHEN 
                                               (NOT oe.MessageID IS NULL OR NOT oe.MailMsgID IS NULL) 
                                               AND oev.Code = 'ARA'
                                     THEN 1 ELSE 0 END ) as [ARA]
         INTO #TMP_Event_Count
         FROM fcm.OperationMemos oe
                   INNER JOIN fcm.OperationEvents oev ON oe.OperationEventID = oev.ID
				   INNER JOIN pub.Memos m ON m.ID = oe.MemoID
		 WHERE m.CreateDate >= @Beginning_CreateDate
         GROUP BY m.CreateBy, CONVERT(varchar(40), m.CreateDate, 102)

SELECT op.分公司
         , sum(isnull(doc.上传文档总数,0)) AS 上传文档总数
         , sum(isnull(mail.关联邮件总数,0)) as 关联邮件总数
         , sum(isnull(mail.上传邮件总数,0)) as 上传邮件总数
         , sum(isnull(ev.ANRC,0)) as ANRC
         , sum(isnull(ev.ANSC,0)) as ANSC
         , sum(isnull(ev.PayNt,0)) as PayNt
         , sum(isnull(ev.ARA,0)) as ARA
         FROM #TMP_OP op
                   LEFT JOIN #TMP_DOC_Count doc ON op.OperatiorID = doc.CreateBy
                   LEFT JOIN #TMP_Mail_Count mail ON op.OperatiorID = mail.CreateBy
                   LEFT JOIN #TMP_Event_Count ev ON op.OperatiorID = ev.CreateBy
         GROUP BY op.分公司
		 ORDER BY op.分公司
		          
SELECT op.分公司, Operator
         , sum(isnull(doc.上传文档总数,0)) AS 上传文档总数
         , sum(isnull(mail.关联邮件总数,0)) as 关联邮件总数
         , sum(isnull(mail.上传邮件总数,0)) as 上传邮件总数
         , sum(isnull(ev.ANRC,0)) as ANRC
         , sum(isnull(ev.ANSC,0)) as ANSC
         , sum(isnull(ev.PayNt,0)) as PayNt
         , sum(isnull(ev.ARA,0)) as ARA
         FROM #TMP_OP op
                   LEFT JOIN #TMP_DOC_Count doc ON op.OperatiorID = doc.CreateBy
                   LEFT JOIN #TMP_Mail_Count mail ON op.OperatiorID = mail.CreateBy
                   LEFT JOIN #TMP_Event_Count ev ON op.OperatiorID = ev.CreateBy
		 WHERE op.分公司 like '%洛杉矶%' 
         GROUP BY op.分公司, Operator
		 ORDER BY Operator

--SELECT op.Operator, CreateDate
--         , doc.上传文档总数
--         , mail.关联邮件总数, mail.上传邮件总数
--         , ev.ANRC, ev.ANSC, ev.[PayNt], ev.[ARA]
--         FROM #TMP_OP op
--                   LEFT JOIN #TMP_DOC_Count doc ON op.OperatiorID = doc.CreateBy
--                   LEFT JOIN #TMP_Mail_Count mail ON op.OperatiorID = mail.CreateBy
--                   LEFT JOIN #TMP_Event_Count ev ON op.OperatiorID = ev.CreateBy
--         WHERE op.分公司 like '%洛杉矶%' 

DROP TABLE #TMP_OP
DROP TABLE #TMP_DOC_Count
DROP TABLE #TMP_Mail_Count
DROP TABLE #TMP_Event_Count


