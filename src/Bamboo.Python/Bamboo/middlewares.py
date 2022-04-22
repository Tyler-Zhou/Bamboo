# Define here the models for your spider middleware
#
# See documentation in:
# https://docs.scrapy.org/en/latest/topics/spider-middleware.html

from scrapy import signals

# useful for handling different item types with a single interface
from itemadapter import is_item, ItemAdapter


class BambooSpiderMiddleware:
    """
    并非所有方法都需要定义。
    如果没有定义一个方法，scrapy 就像蜘蛛中间件没有修改传递的对象一样。
    """

    @classmethod
    def from_crawler(cls, crawler):
        """
        Scrapy 使用此方法来创建您的蜘蛛。
        :param crawler:
        :return:
        """
        s = cls()
        crawler.signals.connect(s.spider_opened, signal=signals.spider_opened)
        return s

    def process_spider_input(self, response, spider):
        """
        为通过蜘蛛中间件并进入蜘蛛的每个响应调用。
        应该返回 None 或引发异常。
        :param response:
        :param spider:
        :return:
        """
        return None

    def process_spider_output(self, response, result, spider):
        """
        在处理完响应后，使用从 Spider 返回的结果调用。
        必须返回一个可迭代的 Request 或 item 对象。
        :param response:
        :param result:
        :param spider:
        :return:
        """
        for i in result:
            yield i

    def process_spider_exception(self, response, exception, spider):
        """
        当蜘蛛或 process_spider_input() 方法（来自其他蜘蛛中间件）引发异常时调用。
        应该返回 None 或一个可迭代的 Request 或 item 对象。
        :param response:
        :param exception:
        :param spider:
        :return:
        """
        pass

    def process_start_requests(self, start_requests, spider):
        """
        与蜘蛛的启动请求一起调用，与 process_spider_output() 方法类似，只是它没有关联的响应。
        必须只返回请求（而不是项目）。
        :param start_requests:
        :param spider:
        :return:
        """
        for r in start_requests:
            yield r

    def spider_opened(self, spider):
        spider.logger.info('打开爬虫: %s' % spider.name)


class BambooDownloaderMiddleware:
    """
    并非所有方法都需要定义。
    如果没有定义方法，scrapy 就好像下载器中间件不修改传递的对象一样。
    """

    @classmethod
    def from_crawler(cls, crawler):
        """
        Scrapy 使用此方法来创建您的蜘蛛。
        :param crawler:
        :return:
        """
        s = cls()
        crawler.signals.connect(s.spider_opened, signal=signals.spider_opened)
        return s

    def process_request(self, request, spider):
        """
        为通过下载器中间件的每个请求调用。
        :param request:
        :param spider:
        :return: None: 继续处理这个请求
                    返回一个响应对象
                    返回一个请求对象
                    引发 IgnoreRequest: process_exception() 方法
                    已安装的下载器中间件将被调用
        """

        # 必须：
        # - return None: 继续处理这个请求
        # - 或者返回一个响应对象
        # - 或者返回一个请求对象
        # - 或引发 IgnoreRequest: process_exception() 方法
        # 已安装的下载器中间件将被调用
        return None

    def process_response(self, request, response, spider):
        """
        使用从下载器返回的响应调用。
        :param request:
        :param response:
        :param spider:
        :return:
        """

        # 必须；
        # - 返回一个响应对象
        # - 返回一个请求对象
        # - 或提出 IgnoreRequest
        return response

    def process_exception(self, request, exception, spider):
        """
        当下载处理程序或 process_request()（来自其他下载器中间件）引发异常时调用。
        :param request:
        :param exception:
        :param spider:
        :return:
        """

        # 必须：
        # - return None: 继续处理这个异常
        # - 返回一个响应对象：停止 process_exception() 链
        # - 返回一个请求对象：停止 process_exception() 链
        pass

    def spider_opened(self, spider):
        spider.logger.info('爬虫已关闭: %s' % spider.name)
