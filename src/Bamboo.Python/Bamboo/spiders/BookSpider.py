# -*- coding: utf-8 -*-
import re
from datetime import datetime
import string

from scrapy import Spider
from Bamboo.items import BookItem, ChapterItem
from scrapy.http import Request
from scrapy.selector import Selector
from Bamboo.websiteconfig.Factory import Factory


class BookSpider(Spider):
    """
    小说爬虫
    """
    name = 'Book'
    allowed_domains = ['www.shuquge.com', 'www.quge66.com', 'www.81zw.com', 'www.shunong.com']

    def __init__(self, website_name='', starturl='', *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.website_name = website_name
        self.starturl = starturl
        self.logger.info('网站名称：%s', self.website_name)
        self.logger.info('启动URL：%s', self.starturl)

        factory = Factory()
        self.bookKey = ""
        self.website_config = factory.get_website(self.website_name)
        self.logger.info('创建网站配置实例：%s', self.website_config.config_name())
        self.start_urls = [self.starturl]

    def parse(self, response):
        """
        解析章节目录页面
        :param response:
        :return:
        """
        try:
            response_selector = Selector(response)
            self.logger.info('实例化Selector')
            main_div = response_selector.xpath(self.website_config.main_xpath())
            self.logger.info('主要容器')
            if self.website_config.info_xpath() == "":
                book_info = main_div
            else:
                book_info = main_div.xpath(self.website_config.info_xpath())
            self.logger.info('获取书籍信息容器')
            item = BookItem()
            self.bookKey = re.findall(self.website_config.parse_url_regex(), response.url, re.S)[0]
            self.logger.info('获取书籍Key：%s', self.bookKey)
            item['sKey'] = self.bookKey
            item['sName'] = book_info.xpath(self.website_config.name_xpath()).extract_first()
            self.logger.info('书籍名称：%s', item['sName'])
            item['sLink'] = response.url
            self.logger.info('书籍链接：%s', item['sLink'])
            item['sAuthor'] = book_info.xpath(self.website_config.author_xpath()).extract_first()
            self.logger.info('书籍作者：%s', item['sAuthor'])
            if self.website_config.tag_xpath() == "":
                item['sTag'] = "玄幻"
            else:
                item['sTag'] = book_info.xpath(self.website_config.tag_xpath()).extract_first()
            self.logger.info('书籍标签(分类)：%s', item['sTag'])
            if self.website_config.introduction_xpath() == "":
                item['sIntroduction'] = item['sName']
            else:
                item['sIntroduction'] = book_info.xpath(self.website_config.introduction_xpath()).extract_first()
            self.logger.info('书籍简介：%s', item['sIntroduction'])
            if self.website_config.status_xpath() == "":
                item['tStatus'] = "1"
            else:
                sstatus = book_info.xpath(self.website_config.status_xpath()).extract_first()
                if "完本" in sstatus:
                    item['tStatus'] = "1"
                else:
                    item['tStatus'] = "0"
            self.logger.info('书籍状态：%s', item['tStatus'])
            item['tCreateDate'] = datetime.now()
            yield item
        except Exception as ex:
            self.logger.exception("获取书籍信息失败：%s", ex)
            raise ex
        self.logger.info("开始获取章节信息")
        try:
            chapter_urls = response_selector.xpath(self.website_config.chapterurls_xpath()).extract()
            for chapter_url in chapter_urls:
                yield Request(response.urljoin(chapter_url), callback=self.parse_chapter)
        except Exception as ex:
            self.logger.exception("遍历章节信息失败：%s", ex)
            raise ex

    def parse_chapter(self, response):
        """
        解析章节明细
        :param response:解析网页响应对象
        :return:
        """
        try:
            result = response.text
            chapter_url = response.url
            # 小说章节内容
            # 1.小说内容正则表达式获取
            chapter_content = re.findall(self.website_config.content_regex(), result, re.S)[0]
            self.logger.info('章节内容')

            item = ChapterItem()
            item["sBookKey"] = self.bookKey
            # 移除html后缀
            item['sKey'] = chapter_url.split('/')[-1].replace('.html', '')
            self.logger.info('章节Key：%s', item['sKey'])
            item['sName'] = response.xpath(self.website_config.chapter_name_xpath()).extract_first()
            self.logger.info('章节名称：%s', item['sName'])
            item['sContent'] = chapter_content
            item['sLink'] = chapter_url
            self.logger.info('章节链接：%s', item['sLink'])
            item["bIsError"] = False
            try:
                iorderindex = int(item['sKey'])
                item["iOrderIndex"] = iorderindex
            except Exception as ex:
                item["iOrderIndex"] = 0
                self.logger.exception("Key转换Int失败%s", ex)

            item['tCreateDate'] = datetime.now()
            yield item
        except Exception as ex:
            self.logger.exception("获取章节信息失败：%s", ex)
            raise ex
