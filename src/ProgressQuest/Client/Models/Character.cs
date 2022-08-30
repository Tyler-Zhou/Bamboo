using Client.Enums;
using Client.Interfaces;

namespace Client.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Character
    {
        /// <summary>
        /// 
        /// </summary>
        private CharacterSummary _summary = new CharacterSummary();
        /// <summary>
        /// 
        /// </summary>
        private ICharacterService _CharacterService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="characterService"></param>
        public Character(ICharacterService characterService)
        {
            _CharacterService = characterService;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => _summary.Name;
            set
            {
                _summary.Name = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public EnumRaces Race
        {
            get => _summary.Race;
            set
            {
                _summary.Race = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public EnumClasses Class
        {
            get => _summary.Class;
            set
            {
                _summary.Class = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Level
        {
            get => _summary.Level;
            set
            {
                _summary.Level = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public CharacterProperty Stats { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Validate();
        /// <summary>
        /// 
        /// </summary>
        public Character()
        {
            Stats = new CharacterProperty();
        }
        /// <summary>
        /// 
        /// </summary>
        private bool Validate()
        {
            return _summary.IsValid && Stats.IsValid;
        }
        /// <summary>
        /// 
        /// </summary>
        internal void Save()
        {
            _CharacterService.Save(this);
        }
    }
}
