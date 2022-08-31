using Client.Enums;
using System.Collections.ObjectModel;

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
        public Character()
        {
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
        public RaceModel Race
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
        public ClassModel Class
        {
            get => _summary.Class;
            set
            {
                _summary.Class = value;
            }
        }
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<StatModel> Stats { get; set; }

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
        public CharacterStats CharacterStats { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Validate();
        /// <summary>
        /// 
        /// </summary>
        private bool Validate()
        {
            return _summary.IsValid && CharacterStats.IsValid;
        }
    }
}
