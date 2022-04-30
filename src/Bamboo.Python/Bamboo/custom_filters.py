# -*- coding: utf-8 -*-

import logging
import pymssql

from scrapy.dupefilters import BaseDupeFilter
from scrapy.utils.request import referer_str


class ChapterExistFilter(BaseDupeFilter):
    """
    章节在数据库是否已经存在
    """

    def __init__(self, db_server, db_port, db_name, db_user, db_password, debug=False):
        self.logdupes = True
        self.debug = debug
        self.logger = logging.getLogger(__name__)

        self.db_server = db_server
        self.db_port = db_port
        self.db_name = db_name
        self.db_user = db_user
        self.db_password = db_password

        self.cur = None
        self.conn = None

    @classmethod
    def from_settings(cls, settings):
        return cls(db_server=settings.get('MSSQL_SERVER'),
                   db_port=settings.get('MSSQL_PORT'),
                   db_name=settings.get('MSSQL_NAME'),
                   db_user=settings.get('MSSQL_USER'),
                   db_password=settings.get('MSSQL_PASSWORD'),
                   debug=settings.getbool('DUPEFILTER_DEBUG'))

    def request_seen(self, request):
        self.conn = pymssql.connect(server=self.db_server, port=self.db_port
                                    , user=self.db_user, password=self.db_password,
                                    database=self.db_name, charset='utf8', autocommit=True)
        self.cur = self.conn.cursor()
        keys = request.url.split('/')
        bookkey = keys[-2]
        chapterkey = keys[-1].replace('.html', '')
        sql = "SELECT [iId] FROM [dbo].[tb_Book_Chapter] WHERE [sBookKey] ='%s' AND [sKey] = '%s'" %(bookkey, chapterkey)
        self.cur.execute(sql)
        iid = self.cur.fetchone()
        self.cur.close()
        self.conn.close()
        if iid is None or iid[0] <= 0:
            self.logger.info("开始抓取网站地址：%s", request.url)
            return False
        self.logger.info("章节已存在 iId：%d", iid[0])
        return True

    def log(self, request, spider):
        if self.debug:
            msg = "Filtered duplicate request: %(request)s (referer: %(referer)s)"
            args = {'request': request, 'referer': referer_str(request)}
            self.logger.debug(msg, args, extra={'spider': spider})
        elif self.logdupes:
            msg = ("Filtered duplicate request: %(request)s"
                   " - no more duplicates will be shown"
                   " (see DUPEFILTER_DEBUG to show all duplicates)")
            self.logger.debug(msg, {'request': request}, extra={'spider': spider})
            self.logdupes = False
        spider.crawler.stats.inc_value('dupefilter/filtered', spider=spider)
