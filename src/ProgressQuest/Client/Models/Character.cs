using Client.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;

namespace Client.Models
{
    /// <summary>
    /// 人物
    /// </summary>
    public class Character
    {
        #region 成员(Member)
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RaceModel Race { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ClassModel Class { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Level { get; set; } = 1;
        /// <summary>
        /// 力量
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// 体质
        /// </summary>
        public int Constitution { get; set; }

        /// <summary>
        /// 敏捷
        /// </summary>
        public int Dexterity { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        public int Intelligence { get; set; }

        /// <summary>
        /// 智慧
        /// </summary>
        public int Wisdom { get; set; }

        /// <summary>
        /// 魅力
        /// </summary>
        public int Charisma { get; set; }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int HPMax { get; set; }

        /// <summary>
        /// 最大魔法值
        /// </summary>
        public int MPMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Validate();

        /// <summary>
        /// 经验
        /// </summary>
        public int Experience { get; set; } = 0;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public Character()
        {
            Race = new RaceModel() { Key = "UnKnown" };
            Class = new ClassModel() { Key = "UnKnown" };
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 
        /// </summary>
        private bool Validate()
        {
            return ValueFallsInRange(Strength)
                && ValueFallsInRange(Constitution)
                && ValueFallsInRange(Dexterity)
                && ValueFallsInRange(Intelligence)
                && ValueFallsInRange(Wisdom)
                && ValueFallsInRange(Charisma)
                && HPMax > 0
                && MPMax > 0;
        }

        /// <summary>
        /// 值在范围内
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValueFallsInRange(int value)
        {
            return value > 0 && value < 16;
        } 
        #endregion
    }
}
