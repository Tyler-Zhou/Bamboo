using Client.Extensions;
using Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Client.Models
{
    /// <summary>
    /// 人物模型
    /// </summary>
    public class Character
    {
        #region 成员(Member)
        #region 基本信息
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 种族
        /// </summary>
        public string RaceKey { get; set; }
        /// <summary>
        /// 种族名称,显示在存档选择界面
        /// </summary>
        [JsonIgnore]
        public string RaceName
        {
            get { return RaceKey.FindResourceDictionary(); }
        }
        /// <summary>
        /// 职业Key
        /// </summary>
        public string ClassKey { get; set; }
        /// <summary>
        /// 职业名称,显示在存档选择界面
        /// </summary>
        [JsonIgnore]
        public string ClassName
        {
            get { return ClassKey.FindResourceDictionary(); }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnLine { get; set; } = false;
        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime BirthDay { get; set; }
        #endregion

        #region 任务书
        /// <summary>
        /// 任务书
        /// </summary>
        public CharacterQuestBook QuestBook { get; set; }
        #endregion

        #region 集合
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<CharacterStat> Stats { get; set; }
        /// <summary>
        /// 装备集合集合
        /// </summary>
        public ObservableCollection<CharacterEquipment> Equipments { get; set; }
        /// <summary>
        /// 法术书集合
        /// </summary>
        public ObservableCollection<CharacterSpellBook> SpellBooks { get; set; }
        /// <summary>
        /// 货物集合
        /// </summary>
        public ObservableCollection<CharacterItem> Items { get; set; }
        #endregion

        #region 进度条
        /// <summary>
        /// 经验进度条
        /// </summary>
        public ExperienceBarModel ExperienceBar { get; set; }
        /// <summary>
        /// 货物进度条
        /// </summary>
        public ItemBarModel ItemBar { get; set; }
        /// <summary>
        /// 剧情进度条
        /// </summary>
        public PlotBarModel PlotBar { get; set; }
        /// <summary>
        /// 任务进度条
        /// </summary>
        public QuestBarModel QuestBar { get; set; }
        /// <summary>
        /// 当前执行任务进度条
        /// </summary>
        public CurrentBarModel CurrentBar { get; set; }
        #endregion

        #region 当前待执行任务队列
        /// <summary>
        /// 当前待执行任务队列
        /// </summary>
        public Queue<BaseTask> TaskQueue { get; set; }
        #endregion

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public Character()
        {
            
        }
        #endregion

        #region 公共方法(Public Method)
        #endregion

        #region 私有方法(Private Method)

        #endregion
    }
}
