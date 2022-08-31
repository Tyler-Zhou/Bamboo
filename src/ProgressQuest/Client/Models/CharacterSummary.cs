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
        public RaceModel Race { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ClassModel Class { get; set; }
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
            return !string.IsNullOrWhiteSpace(Name) && (Race!=null) && (Class!=null);
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
