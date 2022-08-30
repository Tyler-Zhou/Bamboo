using Client.Enums;
using System;

namespace Client.Models
{
    /// <summary>
    /// 人物概要
    /// </summary>
    public class CharacterSummary
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EnumRaces Race { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EnumClasses Class { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Validate();
        /// <summary>
        /// 
        /// </summary>
        public CharacterSummary()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            EnumRaces tempRace;
            EnumClasses tempClass;
            return !string.IsNullOrWhiteSpace(Name) && Enum.TryParse(Race.ToString(), out tempRace) && Enum.TryParse(Class.ToString(), out tempClass);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void Save()
        {
            throw new NotImplementedException();
        }
    }
}
