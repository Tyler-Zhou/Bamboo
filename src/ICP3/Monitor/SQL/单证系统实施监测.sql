

--获取业务列表
SELECT oi.id as OperationID, oi.no as [NO], O.CShortName AS 分公司, oi.FETA as FETA
    , u1.EName as CS, U2.EName AS DocBy
         INTO #TMP_OI
         FROM fcm.OIBookings  oi
                   INNER JOIN SM.Organizations O ON oi.CompanyID = O.ID 
                   LEFT JOIN sm.users u1 ON u1.ID = oi.CustomerServiceID
                   LEFT JOIN sm.users u2 ON u2.ID = oi.FilerId
         WHERE oi.FETA >= '2014-12-27' --AND oi.FETA <= '2015-1-10'

         --SELECT * FROM #TMP_OI

--获取按业务统计的文档数量
SELECT oi.OperationID, count(*) as 上传文档总数 
         INTO #TMP_DOC_Count
         FROM fcm.OperationFiles f
                   INNER JOIN #TMP_OI oi ON f.OperationID = oi.OperationID
         GROUP BY oi.OperationID

--获取按业务统计的邮件数量(总数，服务端备份，客户端备份)
SELECT oi.OperationID
         , count(*) as 关联邮件总数
         , sum(CASE WHEN m.StreamID IS NULL THEN 0 ELSE 1 END) AS 上传邮件总数
         INTO #TMP_Mail_Count
         FROM fcm.OperationMessages om
                   INNER JOIN oa.IMessages m ON om.IMessageID = m.ID
                   INNER JOIN #TMP_OI oi ON om.OperationID = oi.OperationID
         GROUP BY oi.OperationID

--获取按业务统计的事件记录()
SELECT oi.OperationID
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
                   INNER JOIN #TMP_OI oi ON oe.OperationID = oi.OperationID
         GROUP BY oi.OperationID

SELECT oi.分公司
         , SUM(1) AS 业务总数
         , sum(isnull(doc.上传文档总数,0)) AS 上传文档总数
         , sum(isnull(mail.关联邮件总数,0)) as 关联邮件总数
         , sum(isnull(mail.上传邮件总数,0)) as 上传邮件总数
         , sum(isnull(ev.ANRC,0)) as ANRC
         , sum(isnull(ev.ANSC,0)) as ANSC
         , sum(isnull(ev.PayNt,0)) as PayNt
         , sum(isnull(ev.ARA,0)) as ARA
         FROM #TMP_OI oi
                   LEFT JOIN #TMP_DOC_Count doc ON oi.OperationID = doc.OperationID
                   LEFT JOIN #TMP_Mail_Count mail ON oi.OperationID = mail.OperationID
                   LEFT JOIN #TMP_Event_Count ev ON oi.OperationID = ev.OperationID
         GROUP BY oi.分公司

SELECT oi.NO, oi.分公司, oi.cs, oi.docby, oi.FETA
         , doc.上传文档总数
         , mail.关联邮件总数, mail.上传邮件总数
         , ev.ANRC, ev.ANSC, ev.[PayNt], ev.[ARA]
         FROM #TMP_OI oi
                   LEFT JOIN #TMP_DOC_Count doc ON oi.OperationID = doc.OperationID
                   LEFT JOIN #TMP_Mail_Count mail ON oi.OperationID = mail.OperationID
                   LEFT JOIN #TMP_Event_Count ev ON oi.OperationID = ev.OperationID
         WHERE oi.分公司 like '%洛杉矶%' 
         ORDER BY NO DESC

DROP TABLE #TMP_OI
DROP TABLE #TMP_DOC_Count
DROP TABLE #TMP_Mail_Count
DROP TABLE #TMP_Event_Count


