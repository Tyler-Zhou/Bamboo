using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Helpers
{
    /// <summary>
    /// 人物帮助类
    /// 1. 名字生成
    /// 2. 属性值设置逻辑规则
    /// 3. 各项进度计算规则
    /// </summary>
    public class CharacterHelper
    {
        #region 生成名字
        #region 中国姓氏(ChineseSurnameList)
        /// <summary>
        /// 中国姓氏
        /// </summary>
        static List<string> ChineseSurnameList = new List<string>()
        {
            "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯", "陈", "楮", "卫", "蒋", "沈", "韩", "杨",
            "朱", "秦", "尤", "许", "何", "吕", "施", "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜",
            "戚", "谢", "邹", "喻", "柏", "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎",
            "鲁", "韦", "昌", "马", "苗", "凤", "花", "方", "俞", "任", "袁", "柳", "酆", "鲍", "史", "唐",
            "费", "廉", "岑", "薛", "雷", "贺", "倪", "汤", "滕", "殷", "罗", "毕", "郝", "邬", "安", "常",
            "乐", "于", "时", "傅", "皮", "卞", "齐", "康", "伍", "余", "元", "卜", "顾", "孟", "平", "黄",
            "和", "穆", "萧", "尹", "姚", "邵", "湛", "汪", "祁", "毛", "禹", "狄", "米", "贝", "明", "臧",
            "计", "伏", "成", "戴", "谈", "宋", "茅", "庞", "熊", "纪", "舒", "屈", "项", "祝", "董", "梁",
            "杜", "阮", "蓝", "闽", "席", "季", "麻", "强", "贾", "路", "娄", "危", "江", "童", "颜", "郭",
            "梅", "盛", "林", "刁", "锺", "徐", "丘", "骆", "高", "夏", "蔡", "田", "樊", "胡", "凌", "霍",
            "虞", "万", "支", "柯", "昝", "管", "卢", "莫", "经", "房", "裘", "缪", "干", "解", "应", "宗",
            "丁", "宣", "贲", "邓", "郁", "单", "杭", "洪", "包", "诸", "左", "石", "崔", "吉", "钮", "龚",
            "程", "嵇", "邢", "滑", "裴", "陆", "荣", "翁", "荀", "羊", "於", "惠", "甄", "麹", "家", "封",
            "芮", "羿", "储", "靳", "汲", "邴", "糜", "松", "井", "段", "富", "巫", "乌", "焦", "巴", "弓",
            "牧", "隗", "山", "谷", "车", "侯", "宓", "蓬", "全", "郗", "班", "仰", "秋", "仲", "伊", "宫",
            "宁", "仇", "栾", "暴", "甘", "斜", "厉", "戎", "祖", "武", "符", "刘", "景", "詹", "束", "龙",
            "叶", "幸", "司", "韶", "郜", "黎", "蓟", "薄", "印", "宿", "白", "怀", "蒲", "邰", "从", "鄂",
            "索", "咸", "籍", "赖", "卓", "蔺", "屠", "蒙", "池", "乔", "阴", "郁", "胥", "能", "苍", "双",
            "闻", "莘", "党", "翟", "谭", "贡", "劳", "逄", "姬", "申", "扶", "堵", "冉", "宰", "郦", "雍",
            "郤", "璩", "桑", "桂", "濮", "牛", "寿", "通", "边", "扈", "燕", "冀", "郏", "浦", "尚", "农",
            "温", "别", "庄", "晏", "柴", "瞿", "阎", "充", "慕", "连", "茹", "习", "宦", "艾", "鱼", "容",
            "向", "古", "易", "慎", "戈", "廖", "庾", "终", "暨", "居", "衡", "步", "都", "耿", "满", "弘",
            "匡", "国", "文", "寇", "广", "禄", "阙", "东", "欧", "殳", "沃", "利", "蔚", "越", "夔", "隆",
            "师", "巩", "厍", "聂", "晁", "勾", "敖", "融", "冷", "訾", "辛", "阚", "那", "简", "饶", "空",
            "曾", "毋", "沙", "乜", "养", "鞠", "须", "丰", "巢", "关", "蒯", "相", "查", "后", "荆", "红",
            "游", "竺", "权", "逑", "盖", "益", "桓", "公", "仉", "督", "晋", "楚", "阎", "法", "汝", "鄢",
            "涂", "钦", "岳", "帅", "缑", "亢", "况", "后", "有", "琴", "归", "海", "墨", "哈", "谯", "笪",
            "年", "爱", "阳", "佟", "商", "牟", "佘", "佴", "伯", "赏",
            "万俟", "司马", "上官", "欧阳", "夏侯", "诸葛", "闻人", "东方", "赫连", "皇甫", "尉迟", "公羊",
            "澹台", "公冶", "宗政", "濮阳", "淳于", "单于", "太叔", "申屠", "公孙", "仲孙", "轩辕", "令狐",
            "锺离", "宇文", "长孙", "慕容", "鲜于", "闾丘", "司徒", "司空", "丌官", "司寇", "子车", "微生",
            "颛孙", "端木", "巫马", "公西", "漆雕", "乐正", "壤驷", "公良", "拓拔", "夹谷", "宰父", "谷梁",
            "段干", "百里", "东郭", "南门", "呼延", "羊舌", "梁丘", "左丘", "东门", "西门", "南宫"
        };
        #endregion

        /// <summary>
        /// 创建区域代码
        /// </summary>
        /// <param name="strlength"></param>
        /// <param name="isRandomCount"></param>
        /// <returns></returns>
        static object[] CreateRegionCode(int strlength, bool isRandomCount = false)
        {
            if (isRandomCount)
            {
                strlength = RandomHelper.Value(strlength + 1);
            }

            //定义一个字符串数组储存汉字编码的组成元素
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            //定义一个object数组用来
            object[] bytes = new object[strlength];
            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中
             每个汉字有四个区位码组成
             区位码第1位和区位码第2位作为字节数组第一个元素
             区位码第3位和区位码第4位作为字节数组第二个元素
            */

            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位
                int r1 = RandomHelper.Value(11, 14);
                string str_r1 = rBase[r1].Trim();
                //区位码第2位
                int r2;
                if (r1 == 13)
                {
                    r2 = RandomHelper.Value(0, 7);
                }
                else
                {
                    r2 = RandomHelper.Value(0, 16);
                }
                string str_r2 = rBase[r2].Trim();
                //区位码第3位
                int r3 = RandomHelper.Value(10, 16);
                string str_r3 = rBase[r3].Trim();
                //区位码第4位
                int r4;
                if (r3 == 10)
                {
                    r4 = RandomHelper.Value(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = RandomHelper.Value(0, 15);
                }
                else
                {
                    r4 = RandomHelper.Value(0, 16);
                }
                string str_r4 = rBase[r4].Trim();
                //定义两个字节变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中
                byte[] str_r = new byte[] { byte1, byte2 };
                //将产生的一个汉字的字节数组放入object数组中
                bytes.SetValue(str_r, i);
            }
            return bytes;
        }

        static string GetChineseSurname()
        {
            int index = RandomHelper.Value(0, ChineseSurnameList.Count());
            return ChineseSurnameList[index];
        }

        /// <summary>
        /// 随机挑选
        /// </summary>
        /// <param name="pickableSet">可挑选集合</param>
        /// <returns></returns>
        static string RandomPick(string[] pickableSet)
        {
            return pickableSet[RandomHelper.Value(pickableSet.Length)];
        }
        /// <summary>
        /// 生成中文名称
        /// </summary>
        /// <returns></returns>
        public static string GenerateChineseName()
        {
            //姓
            string surname = GetChineseSurname();
            //获取GB2312编码页（表）
            Encoding gb = Encoding.GetEncoding("gb2312");
            //调用函数产生4个随机中文汉字编码
            object[] bytes = CreateRegionCode(2, true);
            //根据汉字编码的字节数组解码出中文汉字
            string name = string.Empty;
            for (int i = 0; i < bytes.Length; i++)
            {
                name += gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[])));
            }
            return $"{surname}{name}";
        }
        /// <summary>
        /// 随机生成名称
        /// </summary>
        /// <returns></returns>
        public static string GenerateEnglishName()
        {
            string[][] KParts = { "br|cr|dr|fr|gr|j|kr|l|m|n|pr||||r|sh|tr|v|wh|x|y|z".Split('|'), "a|a|e|e|i|i|o|o|u|u|ae|ie|oo|ou".Split('|'), "b|ck|d|g|k|m|n|p|t|v|x|z".Split('|') };
            string name = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                if (name != string.Empty)
                {
                    name += RandomPick(KParts[i % 3]);
                }
                else
                {
                    name += RandomPick(KParts[i % 3]).ToUpper();
                }
            }

            return name;
        }

        /// <summary>
        /// 生成名字
        /// </summary>
        /// <param name="culture">文化</param>
        /// <returns>生成的名字</returns>
        public static string GenerateName(string culture)
        {
            switch (culture)
            {
                case "zh-CN":
                    return GenerateChineseName();
                case "en-US":
                    return GenerateEnglishName();
                default:
                    return GenerateChineseName();
            }
        }
        #endregion

        #region 设定属性值
        /// <summary>
        /// 枚举属性范围
        /// 主要属性,不包括 HP MAx , MP Max
        /// </summary>
        public static int EnumStatScope { get; set; } = 6;
        /// <summary>
        /// 枚举装备筛选范围(所有装备)
        /// </summary>
        public static int EnumEquipmentScope { get; set; } = 12;

        /// <summary>
        /// 初始化一般属性
        /// </summary>
        /// <returns></returns>
        /// <remarks>初始值在 1-16 之间</remarks>
        public static int InitGeneralStat()
        {
            return RandomHelper.Value(16);
        }

        /// <summary>
        /// 初始化最大生命值或最大魔法值
        /// 初始值为 小于 9 的随机数 + 传入属性值除以 6 取整
        /// </summary>
        /// <param name="statValue">属性值:生命值对应体质;魔法值对应智力;</param>
        /// <returns></returns>
        public static int InitMaxHPOrMP(int statValue)
        {
            return RandomHelper.Value(8) + statValue / 6;
        }

        /// <summary>
        /// 等级提升更新一般属性
        /// 小于 17 的随机数
        /// </summary>
        /// <returns>属性值</returns>
        public static int LevelUpGeneralStat()
        {
            return RandomHelper.Value(16);
        }

        /// <summary>
        /// 等级提升更新最大生命值或最大魔法值
        /// 传入属性值除以 3 + 1 + 小于5 的随机数
        /// </summary>
        /// <param name="statValue">属性值:生命值对应体质;魔法值对应智力;</param>
        /// <returns></returns>
        public static int LevelUpMaxHPOrMP(int statValue)
        {
            return statValue / 3 + 1 + RandomHelper.Value(4);
        }
        #endregion

        #region 根据等级获取当前等级经验最大值
        /// <summary>
        /// 根据等级获取当前等级经验最大值
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetMaxExperienceByLevel(int level)
        {
            //1级20分钟,之后指数增长
            return (int)Math.Round((20 + Math.Pow(1.15, level)) * 60);
        }
        #endregion

        #region 容量
        /// <summary>
        /// 容量
        /// </summary>
        /// <param name="strength">力量</param>
        /// <returns>容量</returns>
        public static int GetCapacity(int strength)
        {
            return strength + 10;
        }
        #endregion

        #region 获取完成剧幕所需时间
        /// <summary>
        /// 获取完成剧幕所需时间
        /// </summary>
        /// <param name="actIndex">剧幕索引</param>
        /// <returns>完成剧幕所需时间</returns>
        /// <remarks>每个剧幕(1+5)个小时</remarks>
        public static int ActTime(int actIndex)
        {
            return 60 * 60 * (1 + 5 * actIndex);
        }
        #endregion

        #region 计算百分比
        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="numA"></param>
        /// <param name="numB"></param>
        /// <returns></returns>
        public static string Percent(double numA, double numB)
        {
            if (numB <= 0)
                return "100 %";
            var percent = Math.Floor(Math.Round(decimal.Parse((numA / numB).ToString("0.000")), 2) * 100);
            return percent.ToString() + "%";
        }
        #endregion

        #region 杀怪任务区间值
        /// <summary>
        /// 杀怪任务区间值
        /// </summary>
        /// <param name="monsterLevel">怪物等级</param>
        /// <param name="characterLevel">人物等级</param>
        /// <returns>杀怪任务区间值</returns>
        /// <remarks>每级</remarks>
        public static int KillTaskDuration(int monsterLevel, int characterLevel)
        {
            return (2 * 3 * monsterLevel * 1) / characterLevel;
        }
        #endregion

        #region 获取完成剧幕所需时间
        /// <summary>
        /// 装备价格
        /// </summary>
        /// <param name="characterLevel">人物等级</param>
        public static int EquipmentPrice(int characterLevel)
        {
            return 5 * (characterLevel * characterLevel) + 10 * characterLevel + 20;
        }
        #endregion

    }
}
