# Define here the models for your scraped items
#
# See documentation in:
# https://docs.scrapy.org/en/latest/topics/items.html

import scrapy


class BookItem(scrapy.Item):
    # define the fields for your item here like:
    # name = scrapy.Field()
    sKey = scrapy.Field()
    sName = scrapy.Field()
    sAuthor = scrapy.Field()
    sTag = scrapy.Field()
    sLink = scrapy.Field()
    sIntroduction = scrapy.Field()
    tStatus = scrapy.Field()
    tStatus = scrapy.Field()
    tCreateDate = scrapy.Field()
    pass


class ChapterItem(scrapy.Item):
    sBookKey = scrapy.Field()
    sKey = scrapy.Field()
    sName = scrapy.Field()
    sContent = scrapy.Field()
    sLink = scrapy.Field()
    tCreateDate = scrapy.Field()
    iOrderIndex = scrapy.Field()
    pass
