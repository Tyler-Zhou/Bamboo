# -*- coding: utf-8 -*-

from Bamboo.websiteconfig.EightOne import EightOne
from Bamboo.websiteconfig.QuGeSix import QuGeSix
from Bamboo.websiteconfig.ShuQuGe import ShuQuGe
from Bamboo.websiteconfig.ShuNong import ShuNong


class Factory:
    """
    网站工厂
    """
    def get_website(self, website_name):
        """
        获取网站对象
        :param website_name:站点名称
        :return:
        """
        if website_name == 'EithuOne':
            return EightOne()
        elif website_name == 'QuGeSix':
            return QuGeSix()
        elif website_name == 'ShuNong':
            return ShuNong()
        else:
            return ShuQuGe()
        pass
