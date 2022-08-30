using Client.Enums;
using Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class GameViewModel : BindableBase
    {
        #region 成员(Member)
        private Character _character;

        public string CharacterName
        {
            get => _character.Name;
        }

        public EnumRaces CharacterRace
        {
            get => _character.Race;
        }

        public EnumClasses CharacterClass
        {
            get => _character.Class;
        }

        public int CharacterLevel
        {
            get => _character.Level;
        }

        public int CharacterStrength
        {
            get => _character.Stats.Strength;
        }

        public int CharacterConstitution
        {
            get => _character.Stats.Constitution;
        }

        public int CharacterDexterity
        {
            get => _character.Stats.Dexterity;
        }

        public int CharacterIntelligence
        {
            get => _character.Stats.Intelligence;
        }

        public int CharacterWisdom
        {
            get => _character.Stats.Wisdom;
        }

        public int CharacterCharisma
        {
            get => _character.Stats.Charisma;
        }

        public int CharacterMaxHp
        {
            get => _character.Stats.HpMax;
        }

        public int CharacterMaxMp
        {
            get => _character.Stats.MpMax;
        }
        #endregion

        #region 服务(Service)

        #endregion

        #region 命令
        /// <summary>
        /// 执行所有命令，根据类型参数选择不同操作
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public GameViewModel()
        {
            _character = new Character();
        }
        #endregion

        #region 重写方法(Override)

        #endregion

        #region 方法(Method)
        #endregion
    }
}
