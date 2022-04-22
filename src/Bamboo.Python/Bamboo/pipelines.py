# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://docs.scrapy.org/en/latest/topics/item-pipeline.html


# useful for handling different item types with a single interface
from itemadapter import ItemAdapter
import pymssql
from Bamboo.items import BookItem, ChapterItem
import logging


class BambooPipeline:
    def process_item(self, item, spider):
        return item


class MSSQLPipeline(object):
    logger = logging.getLogger(__name__)
    """
    Sql Server
    """

    def __init__(self, db_server, db_port, db_name, db_user, db_password):
        self.db_server = db_server
        self.db_port = db_port
        self.db_name = db_name
        self.db_user = db_user
        self.db_password = db_password

        self.cur = None
        self.conn = None

    @classmethod
    def from_crawler(cls, crawler):
        """
        获取MSSQL配置
        :param crawler:
        :return:
        """
        return cls(
            db_server=crawler.settings.get('MSSQL_SERVER'),
            db_port=crawler.settings.get('MSSQL_PORT'),
            db_name=crawler.settings.get('MSSQL_NAME'),
            db_user=crawler.settings.get('MSSQL_USER'),
            db_password=crawler.settings.get('MSSQL_PASSWORD')
        )

    def open_spider(self, spider):
        """
        爬虫启动时候的运行
        打开MSSQL连接
        :param spider:
        :return:
        """
        self.conn = pymssql.connect(server=self.db_server, port=self.db_port
                                    , user=self.db_user, password=self.db_password,
                                    database=self.db_name, charset='utf8', autocommit=True)
        self.cur = self.conn.cursor()

    def process_item(self, item, spider):
        if isinstance(item, BookItem):
            self._process_book_item(item)
        else:
            self._process_chapter_item(item)
        return item

    def _process_book_item(self, item):
        try:
            sql = "SELECT [iId] FROM [dbo].[tb_Book] WHERE [sKey] ='%s'" % item["sKey"]
            self.cur.execute(sql)
            iid = self.cur.fetchone()
            if iid is None or iid[0] <= 0:
                sql = "BEGIN TRAN INSERT INTO [tb_Book]" \
                      "(sKey,sName,sAuthor,sLink,sTag,sIntroduction,tStatus,tCreateDate) VALUES " \
                      "(%s,%s,%s,%s,%s,%s,%s,%s);COMMIT TRAN"
                param = (
                    item["sKey"],
                    item["sName"],
                    item["sAuthor"],
                    item["sLink"],
                    item["sTag"],
                    item["sIntroduction"],
                    item["tStatus"],
                    item["tCreateDate"]
                )
                self.cur.execute(sql, param)  # 执行insert
                self.logger.info("新增书籍成功")
            else:
                self.logger.info("书籍已存在 iId：%d", iid[0])
        except Exception as ex:
            self.conn.rollback()
            self.logger.exception("新增书籍失败：%s", ex)
            raise ex

    def _process_chapter_item(self, item):
        try:
            sql = "SELECT [iId] FROM [dbo].[tb_Book_Chapter] WHERE [sKey] ='%s'" % item["sKey"]
            self.cur.execute(sql)
            iid = self.cur.fetchone()
            if iid is None or iid[0] <= 0:
                sql = "BEGIN TRAN INSERT INTO [tb_Book_Chapter]" \
                      "(sBookKey,sKey,sName,sContent,sLink,tCreateDate) VALUES " \
                      "(%s,%s,%s,%s,%s,%s);COMMIT TRAN"
                param = (
                    item["sBookKey"],
                    item["sKey"],
                    item["sName"],
                    item["sContent"],
                    item["sLink"],
                    item["tCreateDate"]
                )
                self.cur.execute(sql, param)  # 执行insert
                self.logger.info("新增章节成功")
            else:
                self.logger.info("章节已存在 iId：%d", iid[0])
        except Exception as ex:
            self.conn.rollback()
            self.logger.exception("新增章节失败：%s", ex)
            raise ex

    def close_spider(self, spider):
        """
        爬虫结束的时候运行
        关闭SQL连接
        :param spider:
        :return:
        """
        self.cur.close()
        self.conn.close()
