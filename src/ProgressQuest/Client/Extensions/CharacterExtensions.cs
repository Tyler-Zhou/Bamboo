using Client.Enums;
using Client.Models;
using System.Collections.ObjectModel;

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
                new TraitModel(){ Key="CharacterName",Value = character.Name },
                new TraitModel(){ Key="CharacterRace",Value = character.Race.Name },
                new TraitModel(){ Key="CharacterClass",Value = character.Class.Name },
                new TraitModel(){ Key="CharacterLevel",Value = ""+character.Level },
            };
        }

        /// <summary>
        /// 获取属性集合
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static ObservableCollection<StatModel> Stats(this Character character)
        {
            return new ObservableCollection<StatModel>
            {
                new StatModel(){ Key="EnumStatStrength",StatType = EnumStat.Strength,Value = character.Strength },
                new StatModel(){ Key="EnumStatConstitution",StatType = EnumStat.Constitution,Value = character.Constitution },
                new StatModel(){ Key="EnumStatDexterity",StatType = EnumStat.Dexterity,Value = character.Dexterity },
                new StatModel(){ Key="EnumStatIntelligence",StatType = EnumStat.Intelligence,Value = character.Intelligence },
                new StatModel(){ Key="EnumStatWisdom",StatType = EnumStat.Wisdom,Value = character.Wisdom },
                new StatModel(){ Key="EnumStatCharisma",StatType = EnumStat.Charisma,Value = character.Charisma },
                new StatModel(){ Key="EnumStatHPMax",StatType = EnumStat.HPMax,Value = character.HPMax },
                new StatModel(){ Key="EnumStatMPMax",StatType = EnumStat.MPMax,Value = character.MPMax },
            };
        }
    }
}
