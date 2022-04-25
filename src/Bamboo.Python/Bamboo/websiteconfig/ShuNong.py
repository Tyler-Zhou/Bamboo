# -*- coding: utf-8 -*-
from Bamboo.websiteconfig.BaseConfig import BaseConfig


# 书农
class ShuNong(BaseConfig):
    """
    书农
    https://www.shunong.com/
    """

    def config_name(self):
        """
            配置名称
            :return: 配置名称
            """
        return 'ShuNong'
        pass

    def main_xpath(self):
        """
            主要容器
            :return: XPath路径
            """
        return '//div[@class="booktitle clearfix"]'
        pass

    def info_xpath(self):
        """
            书籍信息容器
            :return: XPath路径
            """
        return ''
        pass

    def parse_url_regex(self):
        """
            提取BookKey
            :return: 正则表达式
            """
        return '.*?://.*?/.*?/.*?/(.*?)/$'
        pass

    def name_xpath(self):
        """
          书籍名称
          :return: XPath路径
          """
        return './/a/h1/text()'
        pass

    def author_xpath(self):
        """
           作者
        :return: XPath路径
        """
        return './/span[@class="author"]/b/text()'
        pass

    def tag_xpath(self):
        """
           标签(分类)
        :return: XPath路径
        """
        return ''
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
        return ''
        pass

    def chapterurls_xpath(self):
        """
           章节列表
        :return: XPath路径
        """
        return './/*[@class="booklist clearfix"][2]/span/a/@href'
        pass

    def content_regex(self):
        """
           章节内容
        :return: 正则表达式
        """
        return '</strong>(.*?)<div'
        pass

    def chapter_name_xpath(self):
        """
           章节名称
        :return: XPath路径
        """
        return './/div[@class="booktitle clearfix"]/span[@class="author"]/text()'
        pass
