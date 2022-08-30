using Client.Converters;
using System.ComponentModel;

namespace Client.Enums
{
    /// <summary>
    /// 种族
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EnumRaces
    {
        /// <summary>
        /// 半兽人
        /// </summary>
        [Description("半兽人")]
        HalfOrc = 1,
        /// <summary>
        /// 半人
        /// </summary>
        [Description("半人")]
        HalfMan = 2,
        /// <summary>
        /// 半身人
        /// </summary>
        [Description("半身人")]
        HalfHalfing = 3,
        /// <summary>
        /// 大型哈比人
        /// </summary>
        [Description("大型哈比人")]
        DoubleHobbit = 4,
        /// <summary>
        /// 哈比人
        /// </summary>
        [Description("哈比人")]
        HobHobbit = 5,
        /// <summary>
        /// 低等精灵
        /// </summary>
        [Description("低等精灵")]
        LowElf = 6,
        /// <summary>
        /// 污粪精灵
        /// </summary>
        [Description("污粪精灵")]
        DungElf = 7,
        /// <summary>
        /// 会说话的小马
        /// </summary>
        [Description("会说话的小马")]
        TalkingPony = 8,
        /// <summary>
        /// 回旋地精
        /// </summary>
        [Description("回旋地精")]
        Gyrognome = 9,
        /// <summary>
        /// 小型矮人
        /// </summary>
        [Description("小型矮人")]
        LesserDwarf = 10,
        /// <summary>
        /// 鸡冠矮人
        /// </summary>
        [Description("鸡冠矮人")]
        CrestedDwarf = 11,
        /// <summary>
        /// 鳗鱼人
        /// </summary>
        [Description("鳗鱼人")]
        EelMan = 12,
        /// <summary>
        /// 熊猫人
        /// </summary>
        [Description("熊猫人")]
        PandaMan = 13,
        /// <summary>
        /// 变形狗头人
        /// </summary>
        [Description("变形狗头人")]
        TransKobold = 14,
        /// <summary>
        /// 魔法机车
        /// </summary>
        [Description("魔法机车")]
        EnchantedMotorcycle = 15,
        /// <summary>
        /// 鬼火
        /// </summary>
        [Description("鬼火")]
        WillOTheWisp = 16,
        /// <summary>
        /// 战斗雀鸟
        /// </summary>
        [Description("战斗雀鸟")]
        BattleFinch = 17,
        /// <summary>
        /// 大型武技族
        /// </summary>
        [Description("大型武技族")]
        DoubleWookie = 18,
        /// <summary>
        /// 丑人
        /// </summary>
        [Description("丑人")]
        Skraeling = 19,
        /// <summary>
        /// 半加拿大人
        /// </summary>
        [Description("半加拿大人")]
        Demicanadian = 20,
        /// <summary>
        /// 地行乌贼
        /// </summary>
        [Description("地行乌贼")]
        LandSquid = 21
    }
}
