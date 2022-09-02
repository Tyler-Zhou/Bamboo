using Client.DataAccess;
using Client.Enums;
using Client.Models;
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
            var model= Repository.Classes.SingleOrDefault(item => item.Key.Equals(character.ClassKey));
            if (model == null)
                return string.Empty;
            return model.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int GetCapacity(this Character character)
        {
            return character.GetStatValue(EnumStat.Strength) + 10;
        }
    }
}
