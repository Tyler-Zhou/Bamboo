# -*- coding: utf-8 -*-
from abc import ABC, abstractmethod


# 网站XPath，Regex配置
class BaseConfig(ABC):
    """
    网站XPath，Regex配置
    """

    @abstractmethod
    def config_name(self):
        """
            配置名称
            :return: 配置名称
            """
        pass

    @abstractmethod
    def main_xpath(self):
        """
            主要容器
            :return: XPath路径
            """
        pass

    @abstractmethod
    def info_xpath(self):
        """
            书籍信息容器
            :return: XPath路径
            """
        pass

    @abstractmethod
    def parse_url_regex(self):
        """
            提取BookKey
            :return: 正则表达式
            """
        pass

    @abstractmethod
    def name_xpath(self):
        """
          书籍名称
          :return: XPath路径
          """
        pass

    @abstractmethod
    def author_xpath(self):
        """
           作者
        :return: XPath路径
        """
        pass

    @abstractmethod
    def tag_xpath(self):
        """
           标签(分类)
        :return: XPath路径
        """
        pass

    @abstractmethod
    def introduction_xpath(self):
        """
           简介
        :return: XPath路径
        """
        pass

    @abstractmethod
    def status_xpath(self):
        """
        状态(是否完本)
        :return: XPath路径
        """
        pass

    @abstractmethod
    def chapterurls_xpath(self):
        """
           章节列表
        :return: XPath路径
        """
        pass

    @abstractmethod
    def content_regex(self):
        """
           章节内容
        :return: 正则表达式
        """
        pass

    @abstractmethod
    def chapter_name_xpath(self):
        """
           章节名称
        :return: XPath路径
        """
        pass
