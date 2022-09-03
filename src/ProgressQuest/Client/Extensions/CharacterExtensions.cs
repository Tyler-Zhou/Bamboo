using Client.DataAccess;
using Client.Enums;
using Client.Helpers;
using Client.Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Client.Extensions
{
    /// <summary>
    /// 人物扩展方法
    /// </summary>
    public static class CharacterExtensions
    {
        /// <summary>
        /// 获取特征集合
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static ObservableCollection<TraitModel> Traits(this Character character)
        {
            return new ObservableCollection<TraitModel>
            {
                new TraitModel(){ Key="CharacterName",Value = character?.Name },
                new TraitModel(){ Key="CharacterRace",Value = character?.GetRaceName() },
                new TraitModel(){ Key="CharacterClass",Value = character?.GetClassName()},
                new TraitModel(){ Key="CharacterLevel",Value = ""+character?.Level },
            };
        }

        /// <summary>
        /// 获取种族名称
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string GetRaceName(this Character character)
        {
            if (character == null)
                return string.Empty;
            var race = Repository.Races.SingleOrDefault(item => item.Key.Equals(character.RaceKey));
            if (race == null)
                return string.Empty;
            return race.Name;
        }
        /// <summary>
        /// 通过Key获取职业名称
        /// </summary>
        /// <returns></returns>
        public static string GetClassName(this Character character)
        {
            if (character == null)
                return string.Empty;
            var model = Repository.Classes.SingleOrDefault(item => item.Key.Equals(character.ClassKey));
            if (model == null)
                return string.Empty;
            return model.Name;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="character">人物实体</param>
        public static void InitData(this Character character)
        {
            #region 属性集合
            character.Stats.Clear();
            character.Stats.AddRange(new List<CharacterStat>
            {
                new CharacterStat(){ Key="EnumStatStrength",StatType = EnumStat.Strength,Value = 0 },
                new CharacterStat(){ Key="EnumStatConstitution",StatType = EnumStat.Constitution,Value = 0 },
                new CharacterStat(){ Key="EnumStatDexterity",StatType = EnumStat.Dexterity,Value = 0 },
                new CharacterStat(){ Key="EnumStatIntelligence",StatType = EnumStat.Intelligence,Value = 0 },
                new CharacterStat(){ Key="EnumStatWisdom",StatType = EnumStat.Wisdom,Value = 0 },
                new CharacterStat(){ Key="EnumStatCharisma",StatType = EnumStat.Charisma,Value = 0 },
                new CharacterStat(){ Key="EnumStatHPMax",StatType = EnumStat.HPMax,Value = 0 },
                new CharacterStat(){ Key="EnumStatMPMax",StatType = EnumStat.MPMax,Value = 0 },
            });
            #endregion

            #region 装备集合
            character.Equipments.Clear();
            if (character.Equipments == null || character.Equipments.Count <= 0)
            {
                character.Equipments.AddRange(new ObservableCollection<CharacterEquipment>
                {
                    new CharacterEquipment(){ Key="EnumEquipmentWeapon" ,EquipmentType = EnumEquipment.Weapon },
                    new CharacterEquipment(){ Key="EnumEquipmentShield",EquipmentType = EnumEquipment.Shield },
                    new CharacterEquipment(){ Key="EnumEquipmentHelm",EquipmentType = EnumEquipment.Helm },
                    new CharacterEquipment(){ Key="EnumEquipmentHauberk",EquipmentType = EnumEquipment.Hauberk },
                    new CharacterEquipment(){ Key="EnumEquipmentBrassairts",EquipmentType = EnumEquipment.Brassairts },
                    new CharacterEquipment(){ Key="EnumEquipmentVambraces",EquipmentType = EnumEquipment.Vambraces},
                    new CharacterEquipment(){ Key="EnumEquipmentGauntlets",EquipmentType = EnumEquipment.Gauntlets },
                    new CharacterEquipment(){ Key="EnumEquipmentGambeson",EquipmentType = EnumEquipment.Gambeson },
                    new CharacterEquipment(){ Key="EnumEquipmentCuisses",EquipmentType = EnumEquipment.Cuisses },
                    new CharacterEquipment(){ Key="EnumEquipmentGreaves",EquipmentType = EnumEquipment.Greaves},
                    new CharacterEquipment(){ Key="EnumEquipmentSollerets",EquipmentType = EnumEquipment.Sollerets},
                });
            }
            #endregion

            #region 货物集合
            //货物默认添加金币
            character.Items.Clear();
            character.Items.AddRange(new ObservableCollection<CharacterItem>()
            {
                new CharacterItem(){Key="CharacterGold",Quality = 0 },
            });
            #endregion
        }

        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <param name="character">人物实体</param>
        public static void InitTask(this Character character)
        {
            character.ItemTask = new ProgressRateItem()
            {
                Key = "ProgressBarToolTipInventory",
                Position = 0,
                MaxValue = CharacterHelper.GetCapacity(GetStatValue(character, EnumStat.Strength))
            };
            character.ExpTask = new ProgressRateExperience()
            {
                Key = "ProgressBarToolTipExperience",
                Position = 0,
                MaxValue = CharacterHelper.GetMaxExperienceByLevel(character.Level)
            };
            character.PlotTask = new ProgressRatePlot()
            {
                Key = "ProgressBarToolTipPlot",
                Position = 0,
                MaxValue = 26,
                CommpleteNeedTime = CharacterHelper.ActTime(0)
            };
            character.QuestTask = new ProgressRateQuest()
            {
                Key = "ProgressBarToolTipQuest",
                Position = 0,
                MaxValue = 0
            };
            character.CurrentTask = new ProgressRateCurrent()
            {
                Key="",
                Position = 0,
                MaxValue = 30,
            };
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="character">人物实体</param>
        /// <param name="statType">属性类型</param>
        /// <returns>属性值</returns>
        /// <exception cref="Exception"></exception>
        public static int GetStatValue(this Character character, EnumStat statType)
        {
            return character.Stats.SingleOrDefault(item => item.StatType == statType).Value;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="character">人物实体</param>
        /// <param name="statType">属性类型</param>
        /// <param name="statValue"></param>
        /// <param name="isInit">是否初始化</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception"></exception>
        public static bool SetStatValue(this Character character, EnumStat statType, int statValue, bool isInit = false)
        {
            var stat = character.Stats.SingleOrDefault(item => item.StatType == statType);
            if (isInit)
                stat.Value = statValue;
            else
            {
                stat.Value += statValue;
                if (stat.StatType == EnumStat.Strength)
                    SetCapacity(character, CharacterHelper.GetCapacity(stat.Value));
            }
            return true;
        }

        /// <summary>
        /// 设置容量
        /// </summary>
        /// <param name="character">人物实体</param>
        /// <param name="capacity">容量</param>
        public static void SetCapacity(this Character character, int capacity)
        {
            character.ItemTask.MaxValue = capacity;
        }

        /// <summary>
        /// 更新装备
        /// </summary>
        /// <param name="character">人物实体</param>
        /// <param name="equipmentType">装备类型</param>
        /// <param name="equipmentKey">装备 Key </param>
        /// <param name="modifierKey1">修饰符 Key 1 </param>
        /// <param name="modifierKey2">修饰符 Key 2 </param>
        /// <param name="plus">加成</param>
        /// <returns>是否更新成功</returns>
        /// <exception cref="Exception"></exception>
        public static bool UpdateEquipment(this Character character, EnumEquipment equipmentType, string equipmentKey, string modifierKey1, string modifierKey2, int plus)
        {
            var equipment = character.Equipments.SingleOrDefault(item => item.EquipmentType == equipmentType);
            if (equipment == null)
                throw new Exception("装备未初始化");
            equipment.EquipmentKey = equipmentKey;
            equipment.ModifierKey1 = modifierKey1;
            equipment.ModifierKey2 = modifierKey2;
            equipment.Plus = plus;
            return true;
        }
        /// <summary>
        /// 添加货物
        /// </summary>
        /// <param name="character">人物实体</param>
        /// <param name="model">待添加货物实体</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool AddItem(this Character character, CharacterItem model)
        {
            var singItem = character.Items.SingleOrDefault(
                item => item.Key.Equals(model.Key)
                && item.ItemAttributeKey.Equals(model.ItemAttributeKey)
                && item.SpecialKey.Equals(model.SpecialKey)
                );
            if (singItem == null)
            {
                character.Items.Add(model);
            }
            else
            {
                singItem.Quality += 1;
            }
            return true;
        }
        /// <summary>
        /// 移除货物
        /// </summary>
        /// <param name="character">人物实体</param>
        /// <param name="model">待移除货物实体</param>
        /// <returns></returns>
        public static bool RemoveItem(this Character character, CharacterItem model)
        {
            if (character.Items.Count == 1)
                return true;
            character.Items.Remove(model);
            return true;
        }

        /// <summary>
        /// 添加法术书
        /// </summary>
        /// <param name="character">人物实体</param>
        /// <param name="model">法术书实体</param>
        /// <returns>是否新增成功</returns>
        /// <exception cref="Exception"></exception>
        public static bool AddSpellBook(this Character character, CharacterSpellBook model)
        {
            var spellBook = character.SpellBooks.SingleOrDefault(item => item.Key.Equals(model.Key));
            if (spellBook == null)
            {
                character.SpellBooks.Add(model);
            }
            else
            {
                spellBook.Level += 1;
            }
            return true;
        }

        /// <summary>
        /// 人物升级
        /// </summary>
        /// <param name="character">人物实体</param>
        public static void LevelUp(this Character character)
        {
            character.Level += 1;
            SetStatValue(character, EnumStat.HPMax, CharacterHelper.LevelUpMaxHPOrMP(GetStatValue(character, EnumStat.Constitution)));
            SetStatValue(character, EnumStat.MPMax, CharacterHelper.LevelUpMaxHPOrMP(GetStatValue(character, EnumStat.Intelligence)));

            WinStat(character);
            WinStat(character);
            WinSpell(character);
            //设置经验进程最大值
            character.ExpTask.MaxValue = CharacterHelper.GetMaxExperienceByLevel(character.Level);
        }

        /// <summary>
        /// 赢得属性
        /// </summary>
        /// <param name="character">人物实体</param>
        public static void WinStat(this Character character)
        {
            EnumStat chosenType = EnumStat.UnKnown;
            if (RandomHelper.Odds(1, 2))
            {
                chosenType = (EnumStat)RandomHelper.Value(CharacterHelper.EnumStatScope);
            }
            else
            {
                //favor the best stat so it will tend to clump
                int sumValue = 0;
                for (int i = 0; i < CharacterHelper.EnumStatScope; i++)
                {
                    sumValue += character.Stats[i].Value * character.Stats[i].Value;
                }
                sumValue = RandomHelper.Value(sumValue);
                for (int i = 0; i < CharacterHelper.EnumStatScope; i++)
                {
                    chosenType = character.Stats[i].StatType;
                    sumValue -= character.Stats[i].Value * character.Stats[i].Value;
                    if (sumValue < 0)
                        break;
                }
            }
            SetStatValue(character, chosenType, 1);
        }

        /// <summary>
        /// 赢得法术书
        /// </summary>
        /// <param name="character">人物实体</param>
        public static void WinSpell(this Character character)
        {
            int maxValue = Math.Min(GetStatValue(character, EnumStat.Wisdom) + character.Level, Repository.Spells.Count);
            SpellModel spell = Repository.Spells.Pick(maxValue);
            if (spell != null)
            {
                AddSpellBook(character, new CharacterSpellBook { Key = spell.Key, Level = 1 });
            }
        }
        /// <summary>
        /// 赢得装备
        /// </summary>
        /// <param name="character">人物实体</param>
        public static void WinEquipment(this Character character)
        {
            EnumEquipment equipmentType = (EnumEquipment)RandomHelper.Value(CharacterHelper.EnumEquipmentScope);
            ObservableCollection<EquipmentPresetModel> stuff;
            ObservableCollection<ModifierModel> better;
            ObservableCollection<ModifierModel> worse;
            ObservableCollection<ModifierModel> modifier_pool;
            if (equipmentType == EnumEquipment.Weapon)
            {
                stuff = Repository.Weapons;
                better = Repository.OffenseAttributes;
                worse = Repository.OffenseBads;
            }
            else
            {
                if (equipmentType == EnumEquipment.Shield)
                    stuff = Repository.Shields;
                else
                    stuff = Repository.Armors;
                better = Repository.DefenseAttributes;
                worse = Repository.DefenseBads;
            }
            EquipmentPresetModel equipment = PickEquipment(character.Level, stuff);
            int plus = character.Level - equipment.Quality;
            if (plus < 0)
                modifier_pool = worse;
            else
                modifier_pool = better;
            int count = 0;
            string modifierKey1 = "";
            string modifierKey2 = "";
            while (count < 2 && count < plus)
            {
                ModifierModel modifier = modifier_pool.Pick();

                if (modifier.Key.Equals(modifierKey1)) //已选择修饰符
                    break;
                if (Math.Abs(plus) < Math.Abs(modifier.Quality)) //加成太多
                    break;

                if (count == 0)
                    modifierKey1 = modifier.Key;
                else
                    modifierKey2 = modifier.Key;
                plus -= modifier.Quality;
                count += 1;
            }
            UpdateEquipment(character, equipmentType, equipment.Key, modifierKey1, modifierKey2, plus);
        }
        /// <summary>
        /// 赢得物品
        /// </summary>
        /// <param name="character">人物实体</param>
        public static void WinItem(this Character character)
        {
            CharacterItem model = new CharacterItem();
            model.Key = Repository.ItemOfs.Pick().Key;
            model.ItemAttributeKey = Repository.ItemAttributes.Pick().Key;
            model.SpecialKey = Repository.Specials.Pick().Key;
            model.Quality = 1;
            AddItem(character, model);
        }

        /// <summary>
        /// 挑选装备
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="stuff"></param>
        /// <returns></returns>
        static EquipmentPresetModel PickEquipment(this int level, ObservableCollection<EquipmentPresetModel> stuff)
        {
            EquipmentPresetModel result = stuff.Pick();
            for (int i = 0; i < 5; i++)
            {
                EquipmentPresetModel alternative = stuff.Pick();
                if (Math.Abs(level - alternative.Quality) < Math.Abs(level - result.Quality))
                {
                    result = alternative;
                }
            }
            return result;
        }
    }
}
