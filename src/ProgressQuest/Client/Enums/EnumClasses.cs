using Client.Converters;
using System.ComponentModel;

namespace Client.Enums
{
    /// <summary>
    /// 职业
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EnumClasses
    {
        /// <summary>
        /// 圣骑士
        /// </summary>
        [Description("圣骑士")]
        UrPaladin = 1,
        /// <summary>
        /// 巫毒公主
        /// </summary>
        [Description("巫毒公主")]
        VoodooPrincess = 2,
        /// <summary>
        /// 机器僧侣
        /// </summary>
        [Description("机器僧侣")]
        RobotMonk = 3,
        /// <summary>
        /// 幕府僧侣(Mu-Fu Monk)
        /// </summary>
        [Description("幕府僧侣")]
        MuFuMonk = 4,
        /// <summary>
        /// 幻影法师
        /// </summary>
        [Description("幻影法师")]
        MageIllusioner = 5,
        /// <summary>
        /// 剃刀骑士(Shiv-Knight)
        /// </summary>
        [Description("剃刀骑士")]
        ShivKnight = 6,
        /// <summary>
        /// 精神马尾松
        /// </summary>
        [Description("精神马尾松")]
        InnerMason = 7,
        /// <summary>
        /// 战士/风琴手(Fighter/Organist)
        /// </summary>
        [Description("战士/风琴手")]
        FighterOrganist = 8,
        /// <summary>
        /// 美洲狮盗贼
        /// </summary>
        [Description("美洲狮盗贼")]
        PumaBurgular = 9,
        /// <summary>
        /// 符文专家
        /// </summary>
        [Description("符文专家")]
        Runeloremaster = 10,
        /// <summary>
        /// 猎人扼杀者
        /// </summary>
        [Description("猎人扼杀者")]
        HunterStrangler = 11,
        /// <summary>
        /// 战斗恶棍(Battle-Felon)
        /// </summary>
        [Description("战斗恶棍")]
        BattleFelon = 12,
        /// <summary>
        /// 搞笑小丑(Tickle-Mimic)
        /// </summary>
        [Description("搞笑小丑")]
        TickleMimic = 13,
        /// <summary>
        /// 搞笑小丑
        /// </summary>
        [Description("搞笑小丑")]
        SlowPoisoner = 14,
        /// <summary>
        /// 杂种狂人
        /// </summary>
        [Description("杂种狂人")]
        BastardLunatic = 15,
        /// <summary>
        /// 卑下者
        /// </summary>
        [Description("卑下者")]
        Lowling = 16,
        /// <summary>
        /// 鸟骑士
        /// </summary>
        [Description("鸟骑士")]
        Birdrider = 17,
        /// <summary>
        /// 操舌剑者
        /// </summary>
        [Description("操舌剑者")]
        Vermineer = 18
    }
}
