using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;

namespace Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class GameViewModel : BaseViewModel
    {
        #region 成员(Member)
        private Character _character;

        public string CharacterName
        {
            get => _character.Name;
        }

        public RaceModel CharacterRace
        {
            get => _character.Race;
        }

        public ClassModel CharacterClass
        {
            get => _character.Class;
        }

        public int CharacterLevel
        {
            get => _character.Level;
        }

        public int CharacterStrength
        {
            get => _character.CharacterStats.Strength;
        }

        public int CharacterConstitution
        {
            get => _character.CharacterStats.Constitution;
        }

        public int CharacterDexterity
        {
            get => _character.CharacterStats.Dexterity;
        }

        public int CharacterIntelligence
        {
            get => _character.CharacterStats.Intelligence;
        }

        public int CharacterWisdom
        {
            get => _character.CharacterStats.Wisdom;
        }

        public int CharacterCharisma
        {
            get => _character.CharacterStats.Charisma;
        }

        public int CharacterMaxHp
        {
            get => _character.CharacterStats.HpMax;
        }

        public int CharacterMaxMp
        {
            get => _character.CharacterStats.MpMax;
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
        public GameViewModel(IContainerProvider provider) : base(provider)
        {
            if (_character == null)
                _character = new Character();
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext != null)
            {
                if (navigationContext.Parameters.ContainsKey("NewCharacter"))
                    _character = (Character)navigationContext.Parameters["NewCharacter"];
            }
        }
        #endregion

        #region 方法(Method)
        #endregion
    }
}
