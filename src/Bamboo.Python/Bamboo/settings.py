# Scrapy settings for Bamboo project
#
# For simplicity, this file contains only settings considered important or
# commonly used. You can find more settings consulting the documentation:
#
#     https://docs.scrapy.org/en/latest/topics/settings.html
#     https://docs.scrapy.org/en/latest/topics/downloader-middleware.html
#     https://docs.scrapy.org/en/latest/topics/spider-middleware.html
from datetime import datetime

BOT_NAME = 'Bamboo'

SPIDER_MODULES = ['Bamboo.spiders']
NEWSPIDER_MODULE = 'Bamboo.spiders'


# Crawl responsibly by identifying yourself (and your website) on the user-agent
# USER_AGENT = 'Bamboo (+http://www.yourdomain.com)'

# LOGGING
# 文件及路径，log目录需要先建好
today = datetime.now()
log_file_path = "log/{}_{}_{}_{}.log".format(today.year, today.month, today.day, today.hour)
# DEBUG   INFO   WARNING   ERROR   CRITICAL
LOG_LEVEL = 'INFO'
# 日志文件名称，启用后不在控制台显示
LOG_FILE = log_file_path

# 遵守 robots.txt 规则
ROBOTSTXT_OBEY = False

# 配置 Scrapy 执行的最大并发请求数（默认值：16）
CONCURRENT_REQUESTS = 5

# 为同一网站的请求配置延迟（默认值：0）
# 查看文档 https://docs.scrapy.org/en/latest/topics/settings.html#download-delay
# See also autothrottle settings and docs
DOWNLOAD_DELAY = 3
# The download delay setting will honor only one of:
# CONCURRENT_REQUESTS_PER_DOMAIN = 16
# CONCURRENT_REQUESTS_PER_IP = 16

# Disable cookies (enabled by default)
# COOKIES_ENABLED = False

# Disable Telnet Console (enabled by default)
# TELNETCONSOLE_ENABLED = False

# 覆盖默认请求标头：
DEFAULT_REQUEST_HEADERS = {
  'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,'
            'application/signed-exchange;v=b3;q=0.9',
  'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/'
                '537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36',
  'Accept-Encoding': 'gzip, deflate',
  'Accept-Language': 'zh-CN',
}

# Enable or disable spider middlewares
# See https://docs.scrapy.org/en/latest/topics/spider-middleware.html
# SPIDER_MIDDLEWARES = {
#    'Bamboo.middlewares.BambooSpiderMiddleware': 543,
#}

# Enable or disable downloader middlewares
# See https://docs.scrapy.org/en/latest/topics/downloader-middleware.html
# DOWNLOADER_MIDDLEWARES = {
#    'Bamboo.middlewares.BambooDownloaderMiddleware': 543,
#}

# Enable or disable extensions
# See https://docs.scrapy.org/en/latest/topics/extensions.html
#EXTENSIONS = {
#    'scrapy.extensions.telnet.TelnetConsole': None,
#}

# Configure item pipelines
# See https://docs.scrapy.org/en/latest/topics/item-pipeline.html
ITEM_PIPELINES = {
    'Bamboo.pipelines.MongoDBPipeline': 300,
}

# Enable and configure the AutoThrottle extension (disabled by default)
# See https://docs.scrapy.org/en/latest/topics/autothrottle.html
# AUTOTHROTTLE_ENABLED = True
# The initial download delay
# AUTOTHROTTLE_START_DELAY = 5
# The maximum download delay to be set in case of high latencies
# AUTOTHROTTLE_MAX_DELAY = 60
# The average number of requests Scrapy should be sending in parallel to
# each remote server
# AUTOTHROTTLE_TARGET_CONCURRENCY = 1.0
# Enable showing throttling stats for every response received:
# AUTOTHROTTLE_DEBUG = False

# Enable and configure HTTP caching (disabled by default)
# See https://docs.scrapy.org/en/latest/topics/downloader-middleware.html#httpcache-middleware-settings
# HTTPCACHE_ENABLED = True
# HTTPCACHE_EXPIRATION_SECS = 0
# HTTPCACHE_DIR = 'httpcache'
# HTTPCACHE_IGNORE_HTTP_CODES = []
# HTTPCACHE_STORAGE = 'scrapy.extensions.httpcache.FilesystemCacheStorage'

DUPEFILTER_CLASS = 'Bamboo.custom_filters.MongoDBChapterExistFilter'

# MSSQL数据库
MSSQL_SERVER = "."  # 服务器
MSSQL_PORT = "1433"  # 服务器端口
MSSQL_NAME = "Bamboo"  # 数据库名称
MSSQL_USER = "sa"  # 用户
MSSQL_PASSWORD = "DATABASE"  # 密码


# Mongo数据库
MONGO_HOST = "127.0.0.1"  # 主机IP
MONGO_PORT = 27017  # 端口号
MONGO_DB = "Bamboo"  # 库名
MONGO_COLL_FINCTIONS = "books"  # collection名
