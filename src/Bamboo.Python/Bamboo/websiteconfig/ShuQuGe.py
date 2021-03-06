# -*- coding: utf-8 -*-
from Bamboo.websiteconfig.BaseConfig import BaseConfig


# 书趣阁
class ShuQuGe(BaseConfig):
    """
    书趣阁
    https://www.shuquge.com/
    """

    def config_name(self):
        """
            配置名称
            :return: 配置名称
            """
        return 'ShuQuGe'
        pass

    def main_xpath(self):
        """
            主要容器
            :return: XPath路径
            """
        return '//div[@class="book"]'
        pass

    def info_xpath(self):
        """
            书籍信息容器
            :return: XPath路径
            """
        return './/div[@class="info"]'
        pass

    def parse_url_regex(self):
        """
            提取BookKey
            :return: 正则表达式
            """
        return '.*?://.*?/txt/(.*?)/'
        pass

    def name_xpath(self):
        """
          书籍名称
          :return: XPath路径
          """
        return './/h2/text()'
        pass

    def author_xpath(self):
        """
           作者
        :return: XPath路径
        """
        return './/div[@class="small"]/span[1]/text()'
        pass

    def tag_xpath(self):
        """
           标签(分类)
        :return: XPath路径
        """
        return './/div[@class="small"]/span[2]/text()'
        pass

    def introduction_xpath(self):
        """
           简介
        :return: XPath路径
        """
        return ''
        pass

    def status_xpath(self):
        """
        状态(是否完本)
        :return: XPath路径
        """
        return './/div[@class="small"]/span[3]/text()'
        pass

    def chapterurls_xpath(self):
        """
           章节列表
        :return: XPath路径
        """
        return './/*[@class="listmain"]/dl/dd/a/@href'
        pass

    def content_regex(self):
        """
           章节内容
        :return: 正则表达式
        """
        return '<div id="content" class="showtxt">(.*?)</div>'
        pass

    def chapter_name_xpath(self):
        """
           章节名称
        :return: XPath路径
        """
        return './/div[@class="book reader"]/div[@class="content"]/h1/text()'
        pass
