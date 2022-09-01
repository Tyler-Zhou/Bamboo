using Client.Enums;
using Client.Models;
using System.Linq;
using System.Collections.ObjectModel;
using Client.DataAccess;

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
                new TraitModel(){ Key="CharacterRace",Value = GetRaceNameByKey(character.RaceKey) },
                new TraitModel(){ Key="CharacterClass",Value = GetClassNameByKey(character.ClassKey) },
                new TraitModel(){ Key="CharacterLevel",Value = ""+character.Level },
            };
        }

        /// <summary>
        /// 获取枚举属性集合
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

        /// <summary>
        /// 获取枚举装备集合
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static ObservableCollection<EquipmentModel> Equipments(this Character character)
        {
            return new ObservableCollection<EquipmentModel>
            {
                new EquipmentModel(){ Key="EnumEquipmentWeapon" ,EquipmentType = EnumEquipment.Weapon,Description = character.WeaponKey },
                new EquipmentModel(){ Key="EnumEquipmentShield",EquipmentType = EnumEquipment.Shield,Description = character.ShieldKey },
                new EquipmentModel(){ Key="EnumEquipmentHelm",EquipmentType = EnumEquipment.Helm,Description = character.HelmKey },
                new EquipmentModel(){ Key="EnumEquipmentHauberk",EquipmentType = EnumEquipment.Hauberk,Description = character.HauberkKey },
                new EquipmentModel(){ Key="EnumEquipmentBrassairts",EquipmentType = EnumEquipment.Brassairts,Description = character.BrassairtsKey },
                new EquipmentModel(){ Key="EnumEquipmentVambraces",EquipmentType = EnumEquipment.Vambraces,Description = character.VambracesKey },
                new EquipmentModel(){ Key="EnumEquipmentGauntlets",EquipmentType = EnumEquipment.Gauntlets,Description = character.GauntletsKey },
                new EquipmentModel(){ Key="EnumEquipmentGambeson",EquipmentType = EnumEquipment.Gambeson,Description = character.GambesonKey },
                new EquipmentModel(){ Key="EnumEquipmentCuisses",EquipmentType = EnumEquipment.Cuisses,Description = character.CuissesKey },
                new EquipmentModel(){ Key="EnumEquipmentGreaves",EquipmentType = EnumEquipment.Greaves,Description = character.GreavesKey},
                new EquipmentModel(){ Key="EnumEquipmentSollerets",EquipmentType = EnumEquipment.Sollerets,Description = character.SolleretsKey },
            };
        }

        /// <summary>
        /// 通过Key获取种族名称
        /// </summary>
        /// <param name="raceKey"></param>
        /// <returns></returns>
        private static string GetRaceNameByKey(string raceKey)
        {
            var race = Repository.Races.SingleOrDefault(item => item.Key.Equals(raceKey));
            if (race == null)
                return raceKey;
            return race.Name;
        }
        /// <summary>
        /// 通过Key获取职业名称
        /// </summary>
        /// <param name="classKey"></param>
        /// <returns></returns>
        private static string GetClassNameByKey(string classKey)
        {
            var model= Repository.Classes.SingleOrDefault(item => item.Key.Equals(classKey));
            if (model == null)
                return classKey;
            return model.Name;
        }
    }
}
