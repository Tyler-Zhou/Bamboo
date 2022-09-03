using Client.Enums;
using Client.Models;
using System.Collections.ObjectModel;

namespace Client.DataAccess
{
    /// <summary>
    /// 仓储
    /// </summary>
    public class Repository
    {
        #region 种族(Race)
        /// <summary>
        /// 种族(Race)
        /// </summary>
        public static ObservableCollection<RaceModel> Races = new ObservableCollection<RaceModel>()
        {
            new RaceModel(){Key="RaceHalfOrc",Stats=new ObservableCollection<EnumStat>{EnumStat.HPMax } },
            new RaceModel(){Key="RaceHalfMan",Stats=new ObservableCollection<EnumStat>{EnumStat.Charisma } },
            new RaceModel(){Key="RaceHalfHalfing",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity } },
            new RaceModel(){Key="RaceDoubleHobbit",Stats=new ObservableCollection<EnumStat>{EnumStat.Strength } },
            new RaceModel(){Key="RaceHobHobbit",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity,EnumStat.Constitution } },
            new RaceModel(){Key="RaceLowElf",Stats=new ObservableCollection<EnumStat>{EnumStat.Constitution } },
            new RaceModel(){Key="RaceDungElf",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom } },
            new RaceModel(){Key="RaceTalkingPony",Stats=new ObservableCollection<EnumStat>{EnumStat.MPMax,EnumStat.Intelligence } },
            new RaceModel(){Key="RaceGyrognome",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity } },
            new RaceModel(){Key="RaceLesserDwarf",Stats=new ObservableCollection<EnumStat>{EnumStat.Constitution } },
            new RaceModel(){Key="RaceCrestedDwarf",Stats=new ObservableCollection<EnumStat>{EnumStat.Charisma } },
            new RaceModel(){Key="RaceEelMan",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity } },
            new RaceModel(){Key="RacePandaMan",Stats=new ObservableCollection<EnumStat>{EnumStat.Constitution,EnumStat.Strength } },
            new RaceModel(){Key="RaceTransKobold",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom } },
            new RaceModel(){Key="RaceEnchantedMotorcycle",Stats=new ObservableCollection<EnumStat>{EnumStat.MPMax } },
            new RaceModel(){Key="RaceWillOTheWisp",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom } },
            new RaceModel(){Key="RaceBattleFinch",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity,EnumStat.Intelligence } },
            new RaceModel(){Key="RaceDoubleWookie",Stats=new ObservableCollection<EnumStat>{EnumStat.Strength } },
            new RaceModel(){Key="RaceSkraeling",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom } },
            new RaceModel(){Key="RaceDemicanadian",Stats=new ObservableCollection<EnumStat>{EnumStat.Constitution } },
            new RaceModel(){Key="RaceLandSquid",Stats=new ObservableCollection<EnumStat>{EnumStat.Strength,EnumStat.HPMax } },
        };
        #endregion

        #region 职业(Class)
        /// <summary>
        /// 职业(Class)
        /// </summary>
        public static ObservableCollection<ClassModel> Classes = new ObservableCollection<ClassModel>()
        {
            new ClassModel(){Key="ClassUrPaladin",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom,EnumStat.Constitution } },
            new ClassModel(){Key="ClassVoodooPrincess",Stats=new ObservableCollection<EnumStat>{EnumStat.Intelligence,EnumStat.Charisma } },
            new ClassModel(){Key="ClassRobotMonk",Stats=new ObservableCollection<EnumStat>{EnumStat.Strength } },
            new ClassModel(){Key="ClassMuFuMonk",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity } },
            new ClassModel(){Key="ClassMageIllusioner",Stats=new ObservableCollection<EnumStat>{EnumStat.Intelligence,EnumStat.MPMax } },
            new ClassModel(){Key="ClassShivKnight",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity } },
            new ClassModel(){Key="ClassInnerMason",Stats=new ObservableCollection<EnumStat>{EnumStat.Constitution } },
            new ClassModel(){Key="ClassFighterOrganist",Stats=new ObservableCollection<EnumStat>{EnumStat.Charisma,EnumStat.Strength } },
            new ClassModel(){Key="ClassPumaBurgular",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity } },
            new ClassModel(){Key="ClassRuneloremaster",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom } },
            new ClassModel(){Key="ClassHunterStrangler",Stats=new ObservableCollection<EnumStat>{EnumStat.Dexterity,EnumStat.Intelligence } },
            new ClassModel(){Key="ClassBattleFelon",Stats=new ObservableCollection<EnumStat>{EnumStat.Strength } },
            new ClassModel(){Key="ClassTickleMimic",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom,EnumStat.Intelligence } },
            new ClassModel(){Key="ClassSlowPoisoner",Stats=new ObservableCollection<EnumStat>{EnumStat.Constitution } },
            new ClassModel(){Key="ClassBastardLunatic",Stats=new ObservableCollection<EnumStat>{EnumStat.Constitution } },
            new ClassModel(){Key="ClassLowling",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom } },
            new ClassModel(){Key="ClassBirdrider",Stats=new ObservableCollection<EnumStat>{EnumStat.Wisdom } },
            new ClassModel(){Key="ClassVermineer",Stats=new ObservableCollection<EnumStat>{EnumStat.Intelligence } },
        };
        #endregion

        #region 法术(Spells)
        /// <summary>
        /// 法术(Spells)
        /// </summary>
        public static ObservableCollection<SpellModel> Spells = new ObservableCollection<SpellModel>()
        {
            new SpellModel(){Key="Slime Finger"},
            new SpellModel(){Key="Rabbit Punch"},
            new SpellModel(){Key="Hastiness"},
            new SpellModel(){Key="Good Move"},
            new SpellModel(){Key="Sadness"},
            new SpellModel(){Key="Seasick"},
            new SpellModel(){Key="Shoelaces"},
            new SpellModel(){Key="Innoculate"},
            new SpellModel(){Key="Cone of Annoyance"},
            new SpellModel(){Key="Magnetic Orb"},
            new SpellModel(){Key="Invisible Hands"},
            new SpellModel(){Key="Revolting Cloud"},
            new SpellModel(){Key="Aqueous Humor"},
            new SpellModel(){Key="Spectral Miasma"},
            new SpellModel(){Key="Clever Fellow"},
            new SpellModel(){Key="Lockjaw"},
            new SpellModel(){Key="History Lesson"},
            new SpellModel(){Key="Hydrophobia"},
            new SpellModel(){Key="Big Sister"},
            new SpellModel(){Key="Cone of Paste"},
            new SpellModel(){Key="Mulligan"},
            new SpellModel(){Key="Nestor's Bright Idea"},
            new SpellModel(){Key="Holy Batpole"},
            new SpellModel(){Key="Tumor (Benign)"},
            new SpellModel(){Key="Braingate"},
            new SpellModel(){Key="Summon a Bitch"},
            new SpellModel(){Key="Nonplus"},
            new SpellModel(){Key="Animate Nightstand"},
            new SpellModel(){Key="Eye of the Troglodyte"},
            new SpellModel(){Key="Curse Name"},
            new SpellModel(){Key="Dropsy"},
            new SpellModel(){Key="Vitreous Humor"},
            new SpellModel(){Key="Roger's Grand Illusion"},
            new SpellModel(){Key="Covet"},
            new SpellModel(){Key="Black Idaho"},
            new SpellModel(){Key="Astral Miasma"},
            new SpellModel(){Key="Spectral Oyster"},
            new SpellModel(){Key="Acrid Hands"},
            new SpellModel(){Key="Angioplasty"},
            new SpellModel(){Key="Grognor's Big Day Off"},
            new SpellModel(){Key="Tumor (Malignant)"},
            new SpellModel(){Key="Animate Tunic"},
            new SpellModel(){Key="Ursine Armor"},
            new SpellModel(){Key="Holy Roller"},
            new SpellModel(){Key="Tonsilectomy"},
            new SpellModel(){Key="Curse Family"},
            new SpellModel(){Key="Infinite Confusion"},
        };
        #endregion

        #region 进攻特征(OffenseAttributes)

        /// <summary>
        /// 进攻特征(OffenseAttributes)
        /// </summary>
        public static ObservableCollection<ModifierModel> OffenseAttributes = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="Polished",Quality = 1},
            new ModifierModel(){Key="Serrated",Quality = 1},
            new ModifierModel(){Key="Heavy",Quality = 1},
            new ModifierModel(){Key="Pronged",Quality = 2},
            new ModifierModel(){Key="Steely",Quality = 2},
            new ModifierModel(){Key="Vicious",Quality = 3},
            new ModifierModel(){Key="Venomed",Quality = 4},
            new ModifierModel(){Key="Stabbity",Quality = 4},
            new ModifierModel(){Key="Dancing",Quality = 5},
            new ModifierModel(){Key="Invisible",Quality = 6},
            new ModifierModel(){Key="Vorpal",Quality = 7},
        };
        #endregion

        #region 防御特征(DefenseAttributes)

        /// <summary>
        /// 防御特征(DefenseAttributes)
        /// </summary>
        public static ObservableCollection<ModifierModel> DefenseAttributes = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="Studded",Quality = 1},
            new ModifierModel(){Key="Banded",Quality = 2},
            new ModifierModel(){Key="Gilded",Quality = 2},
            new ModifierModel(){Key="Festooned",Quality = 3},
            new ModifierModel(){Key="Holy",Quality = 4},
            new ModifierModel(){Key="Cambric",Quality = 1},
            new ModifierModel(){Key="Fine",Quality = 4},
            new ModifierModel(){Key="Impressive",Quality = 5},
            new ModifierModel(){Key="Custom",Quality = 3},
        };
        #endregion

        #region 进攻损坏(OffenseBad)
        /// <summary>
        /// 进攻损坏(OffenseBad)
        /// </summary>
        public static ObservableCollection<ModifierModel> OffenseBads = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="Dull",Quality = -2},
            new ModifierModel(){Key="Tarnished",Quality = -1},
            new ModifierModel(){Key="Rusty",Quality = -3},
            new ModifierModel(){Key="Padded",Quality = -5},
            new ModifierModel(){Key="Bent",Quality = -4},
            new ModifierModel(){Key="Mini",Quality = -4},
            new ModifierModel(){Key="Rubber",Quality = -6},
            new ModifierModel(){Key="Nerf",Quality = -7},
            new ModifierModel(){Key="Unbalanced",Quality = -2},
        };
        #endregion

        #region 防御损坏(DefenseBads)
        /// <summary>
        /// 防御损坏(DefenseBads)
        /// </summary>
        public static ObservableCollection<ModifierModel> DefenseBads = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="Holey",Quality = -1},
            new ModifierModel(){Key="Patched",Quality = -1},
            new ModifierModel(){Key="Threadbare",Quality = -2},
            new ModifierModel(){Key="Faded",Quality = -1},
            new ModifierModel(){Key="Rusty",Quality = -3},
            new ModifierModel(){Key="Motheaten",Quality = -3},
            new ModifierModel(){Key="Mildewed",Quality = -2},
            new ModifierModel(){Key="Torn",Quality = -3},
            new ModifierModel(){Key="Dented",Quality = -3},
            new ModifierModel(){Key="Cursed",Quality = -5},
            new ModifierModel(){Key="Plastic",Quality = -4},
            new ModifierModel(){Key="Cracked",Quality = -4},
            new ModifierModel(){Key="Warped",Quality = -3},
            new ModifierModel(){Key="Corroded",Quality = -3},
        };
        #endregion

        #region 盾牌(Shields)
        /// <summary>
        /// 盾牌(Shields)
        /// </summary>
        public static ObservableCollection<EquipmentPresetModel> Shields = new ObservableCollection<EquipmentPresetModel>()
        {
            new EquipmentPresetModel(){Key="Parasol",Quality = 0},
            new EquipmentPresetModel(){Key="Pie Plate",Quality = 1},
            new EquipmentPresetModel(){Key="Garbage Can Lid",Quality = 2},
            new EquipmentPresetModel(){Key="Buckler",Quality = 3},
            new EquipmentPresetModel(){Key="Plexiglass",Quality = 4},
            new EquipmentPresetModel(){Key="Fender",Quality = 4},
            new EquipmentPresetModel(){Key="Round Shield",Quality = 5},
            new EquipmentPresetModel(){Key="Carapace",Quality = 4},
            new EquipmentPresetModel(){Key="Scutum",Quality = 6},
            new EquipmentPresetModel(){Key="Propugner",Quality = 6},
            new EquipmentPresetModel(){Key="Kite Shield",Quality = 7},
            new EquipmentPresetModel(){Key="Pavise",Quality = 8},
            new EquipmentPresetModel(){Key="Tower Shield",Quality = 9},
            new EquipmentPresetModel(){Key="Baroque Shield",Quality = 11},
            new EquipmentPresetModel(){Key="Aegis",Quality = 12},
            new EquipmentPresetModel(){Key="Magnetic Field",Quality = 18},
        };
        #endregion

        #region 盔甲(Armors)
        /// <summary>
        /// 盔甲(Armors)
        /// </summary>
        public static ObservableCollection<EquipmentPresetModel> Armors = new ObservableCollection<EquipmentPresetModel>()
        {
            new EquipmentPresetModel(){Key="Lace",Quality = 1},
            new EquipmentPresetModel(){Key="Macrame",Quality = 2},
            new EquipmentPresetModel(){Key="Burlap",Quality = 3},
            new EquipmentPresetModel(){Key="Canvas",Quality = 4},
            new EquipmentPresetModel(){Key="Flannel",Quality = 5},
            new EquipmentPresetModel(){Key="Chamois",Quality = 6},
            new EquipmentPresetModel(){Key="Pleathers",Quality = 7},
            new EquipmentPresetModel(){Key="Leathers",Quality = 8},
            new EquipmentPresetModel(){Key="Bearskin",Quality = 9},
            new EquipmentPresetModel(){Key="Ringmail",Quality = 10},
            new EquipmentPresetModel(){Key="Scale Mail",Quality = 12},
            new EquipmentPresetModel(){Key="Chainmail",Quality = 14},
            new EquipmentPresetModel(){Key="Splint Mail",Quality = 15},
            new EquipmentPresetModel(){Key="Platemail",Quality = 16},
            new EquipmentPresetModel(){Key="ABS",Quality = 17},
            new EquipmentPresetModel(){Key="Kevlar",Quality = 18},
            new EquipmentPresetModel(){Key="Titanium",Quality = 19},
            new EquipmentPresetModel(){Key="Mithril Mail",Quality = 20},
            new EquipmentPresetModel(){Key="Diamond Mail",Quality = 25},
            new EquipmentPresetModel(){Key="Plasma",Quality = 30},
        };
        #endregion

        #region 武器(Weapons)
        /// <summary>
        /// 武器(Weapons)
        /// </summary>
        public static ObservableCollection<EquipmentPresetModel> Weapons = new ObservableCollection<EquipmentPresetModel>()
        {
            new EquipmentPresetModel(){Key="Stick",Quality = 0},
            new EquipmentPresetModel(){Key="Broken Bottle",Quality = 1},
            new EquipmentPresetModel(){Key="Shiv",Quality = 1},
            new EquipmentPresetModel(){Key="Sprig",Quality = 1},
            new EquipmentPresetModel(){Key="Oxgoad",Quality = 1},
            new EquipmentPresetModel(){Key="Eelspear",Quality = 2},
            new EquipmentPresetModel(){Key="Bowie Knife",Quality = 2},
            new EquipmentPresetModel(){Key="Claw Hammer",Quality = 2},
            new EquipmentPresetModel(){Key="Handpeen",Quality = 2},
            new EquipmentPresetModel(){Key="Andiron",Quality = 3},
            new EquipmentPresetModel(){Key="Hatchet",Quality = 3},
            new EquipmentPresetModel(){Key="Tomahawk",Quality = 3},
            new EquipmentPresetModel(){Key="Hackbarm",Quality = 3},
            new EquipmentPresetModel(){Key="Crowbar",Quality = 4},
            new EquipmentPresetModel(){Key="Mace",Quality = 4},
            new EquipmentPresetModel(){Key="Battleadze",Quality = 4},
            new EquipmentPresetModel(){Key="Leafmace",Quality = 5},
            new EquipmentPresetModel(){Key="Shortsword",Quality = 5},
            new EquipmentPresetModel(){Key="Longiron",Quality = 5},
            new EquipmentPresetModel(){Key="Poachard",Quality = 5},
            new EquipmentPresetModel(){Key="Baselard",Quality = 5},
            new EquipmentPresetModel(){Key="Whinyard",Quality = 6},
            new EquipmentPresetModel(){Key="Blunderbuss",Quality = 6},
            new EquipmentPresetModel(){Key="Longsword",Quality = 6},
            new EquipmentPresetModel(){Key="Crankbow",Quality = 6},
            new EquipmentPresetModel(){Key="Blibo",Quality = 7},
            new EquipmentPresetModel(){Key="Broadsword",Quality = 7},
            new EquipmentPresetModel(){Key="Kreen",Quality = 7},
            new EquipmentPresetModel(){Key="Warhammer",Quality = 7},
            new EquipmentPresetModel(){Key="Morning Star",Quality = 8},
            new EquipmentPresetModel(){Key="Pole-adze",Quality = 8},
            new EquipmentPresetModel(){Key="Spontoon",Quality = 8},
            new EquipmentPresetModel(){Key="Bastard Sword",Quality = 9},
            new EquipmentPresetModel(){Key="Peen-arm",Quality = 9},
            new EquipmentPresetModel(){Key="Culverin",Quality = 10},
            new EquipmentPresetModel(){Key="Lance",Quality = 10},
            new EquipmentPresetModel(){Key="Halberd",Quality = 11},
            new EquipmentPresetModel(){Key="Poleax",Quality = 12},
            new EquipmentPresetModel(){Key="Bandyclef",Quality = 15},
        };
        #endregion

        #region 特价(Specials)
        /// <summary>
        /// 特价(Specials)
        /// </summary>
        public static ObservableCollection<SpecialModel> Specials = new ObservableCollection<SpecialModel>()
        {
            new SpecialModel(){Key="Diadem"},
            new SpecialModel(){Key="Festoon"},
            new SpecialModel(){Key="Gemstone"},
            new SpecialModel(){Key="Phial"},
            new SpecialModel(){Key="Tiara"},
            new SpecialModel(){Key="Scabbard"},
            new SpecialModel(){Key="Arrow"},
            new SpecialModel(){Key="Lens"},
            new SpecialModel(){Key="Lamp"},
            new SpecialModel(){Key="Hymnal"},
            new SpecialModel(){Key="Fleece"},
            new SpecialModel(){Key="Laurel"},
            new SpecialModel(){Key="Brooch"},
            new SpecialModel(){Key="Gimlet"},
            new SpecialModel(){Key="Cobble"},
            new SpecialModel(){Key="Albatross"},
            new SpecialModel(){Key="Brazier"},
            new SpecialModel(){Key="Bandolier"},
            new SpecialModel(){Key="Tome"},
            new SpecialModel(){Key="Garnet"},
            new SpecialModel(){Key="Amethyst"},
            new SpecialModel(){Key="Candelabra"},
            new SpecialModel(){Key="Corset"},
            new SpecialModel(){Key="Sphere"},
            new SpecialModel(){Key="Sceptre"},
            new SpecialModel(){Key="Ankh"},
            new SpecialModel(){Key="Talisman"},
            new SpecialModel(){Key="Orb"},
            new SpecialModel(){Key="Gammel"},
            new SpecialModel(){Key="Ornament"},
            new SpecialModel(){Key="Brocade"},
            new SpecialModel(){Key="Galoon"},
            new SpecialModel(){Key="Bijou"},
            new SpecialModel(){Key="Spangle"},
            new SpecialModel(){Key="Gimcrack"},
            new SpecialModel(){Key="Hood"},
            new SpecialModel(){Key="Vulpeculum"},
        };
        #endregion

        #region 物品特征(ItemAttributes)
        /// <summary>
        /// 物品特征(ItemAttributes)
        /// </summary>
        public static ObservableCollection<ItemAttributeModel> ItemAttributes = new ObservableCollection<ItemAttributeModel>()
        {
            new ItemAttributeModel(){Key="Golden"},
            new ItemAttributeModel(){Key="Gilded"},
            new ItemAttributeModel(){Key="Spectral"},
            new ItemAttributeModel(){Key="Astral"},
            new ItemAttributeModel(){Key="Garlanded"},
            new ItemAttributeModel(){Key="Precious"},
            new ItemAttributeModel(){Key="Crafted"},
            new ItemAttributeModel(){Key="Dual"},
            new ItemAttributeModel(){Key="Filigreed"},
            new ItemAttributeModel(){Key="Cruciate"},
            new ItemAttributeModel(){Key="Arcane"},
            new ItemAttributeModel(){Key="Blessed"},
            new ItemAttributeModel(){Key="Reverential"},
            new ItemAttributeModel(){Key="Lucky"},
            new ItemAttributeModel(){Key="Enchanted"},
            new ItemAttributeModel(){Key="Gleaming"},
            new ItemAttributeModel(){Key="Grandiose"},
            new ItemAttributeModel(){Key="Sacred"},
            new ItemAttributeModel(){Key="Legendary"},
            new ItemAttributeModel(){Key="Mythic"},
            new ItemAttributeModel(){Key="Crystalline"},
            new ItemAttributeModel(){Key="Austere"},
            new ItemAttributeModel(){Key="Ostentatious"},
            new ItemAttributeModel(){Key="One True"},
            new ItemAttributeModel(){Key="Proverbial"},
            new ItemAttributeModel(){Key="Fearsome"},
            new ItemAttributeModel(){Key="Deadly"},
            new ItemAttributeModel(){Key="Benevolent"},
            new ItemAttributeModel(){Key="Unearthly"},
            new ItemAttributeModel(){Key="Magnificent"},
            new ItemAttributeModel(){Key="Iron"},
            new ItemAttributeModel(){Key="Ormolu"},
            new ItemAttributeModel(){Key="Puissant"},
        };
        #endregion

        #region 物品(ItemOfs)
        /// <summary>
        /// 物品(ItemOfs)
        /// </summary>
        public static ObservableCollection<ItemOfModel> ItemOfs = new ObservableCollection<ItemOfModel>()
        {
            new ItemOfModel(){Key="Foreboding"},
            new ItemOfModel(){Key="Foreshadowing"},
            new ItemOfModel(){Key="Nervousness"},
            new ItemOfModel(){Key="Happiness"},
            new ItemOfModel(){Key="Torpor"},
            new ItemOfModel(){Key="Danger"},
            new ItemOfModel(){Key="Craft"},
            new ItemOfModel(){Key="Silence"},
            new ItemOfModel(){Key="Invisibility"},
            new ItemOfModel(){Key="Rapidity"},
            new ItemOfModel(){Key="Pleasure"},
            new ItemOfModel(){Key="Practicality"},
            new ItemOfModel(){Key="Hurting"},
            new ItemOfModel(){Key="Joy"},
            new ItemOfModel(){Key="Petulance"},
            new ItemOfModel(){Key="Intrusion"},
            new ItemOfModel(){Key="Chaos"},
            new ItemOfModel(){Key="Suffering"},
            new ItemOfModel(){Key="Extroversion"},
            new ItemOfModel(){Key="Frenzy"},
            new ItemOfModel(){Key="Sisu"},
            new ItemOfModel(){Key="Solitude"},
            new ItemOfModel(){Key="Punctuality"},
            new ItemOfModel(){Key="Efficiency"},
            new ItemOfModel(){Key="Comfort"},
            new ItemOfModel(){Key="Patience"},
            new ItemOfModel(){Key="Internment"},
            new ItemOfModel(){Key="Incarceration"},
            new ItemOfModel(){Key="Misapprehension"},
            new ItemOfModel(){Key="Loyalty"},
            new ItemOfModel(){Key="Envy"},
            new ItemOfModel(){Key="Acrimony"},
            new ItemOfModel(){Key="Worry"},
            new ItemOfModel(){Key="Fear"},
            new ItemOfModel(){Key="Awe"},
            new ItemOfModel(){Key="Guile"},
            new ItemOfModel(){Key="Prurience"},
            new ItemOfModel(){Key="Fortune"},
            new ItemOfModel(){Key="Perspicacity"},
            new ItemOfModel(){Key="Domination"},
            new ItemOfModel(){Key="Submission"},
            new ItemOfModel(){Key="Fealty"},
            new ItemOfModel(){Key="Hunger"},
            new ItemOfModel(){Key="Despair"},
            new ItemOfModel(){Key="Cruelty"},
            new ItemOfModel(){Key="Grob"},
            new ItemOfModel(){Key="Dignard"},
            new ItemOfModel(){Key="Ra"},
            new ItemOfModel(){Key="the Bone"},
            new ItemOfModel(){Key="Diamonique"},
            new ItemOfModel(){Key="Electrum"},
            new ItemOfModel(){Key="Hydragyrum"},
        };
        #endregion

        #region 无聊的物品(BoringItems)
        /// <summary>
        /// 无聊的物品(BoringItems)
        /// </summary>
        public ObservableCollection<BoringItemModel> BoringItems { get => _BoringItems; }


        ObservableCollection<BoringItemModel> _BoringItems = new ObservableCollection<BoringItemModel>()
        {
            new BoringItemModel(){Key="nail"},
            new BoringItemModel(){Key="lunchpail"},
            new BoringItemModel(){Key="sock"},
            new BoringItemModel(){Key="I.O.U."},
            new BoringItemModel(){Key="cookie"},
            new BoringItemModel(){Key="pint"},
            new BoringItemModel(){Key="toothpick"},
            new BoringItemModel(){Key="writ"},
            new BoringItemModel(){Key="newspaper"},
            new BoringItemModel(){Key="letter"},
            new BoringItemModel(){Key="plank"},
            new BoringItemModel(){Key="hat"},
            new BoringItemModel(){Key="egg"},
            new BoringItemModel(){Key="coin"},
            new BoringItemModel(){Key="needle"},
            new BoringItemModel(){Key="bucket"},
            new BoringItemModel(){Key="ladder"},
            new BoringItemModel(){Key="chicken"},
            new BoringItemModel(){Key="twig"},
            new BoringItemModel(){Key="dirtclod"},
            new BoringItemModel(){Key="counterpane"},
            new BoringItemModel(){Key="vest"},
            new BoringItemModel(){Key="teratoma"},
            new BoringItemModel(){Key="bunny"},
            new BoringItemModel(){Key="rock"},
            new BoringItemModel(){Key="pole"},
            new BoringItemModel(){Key="carrot"},
            new BoringItemModel(){Key="canoe"},
            new BoringItemModel(){Key="inkwell"},
            new BoringItemModel(){Key="hoe"},
            new BoringItemModel(){Key="bandage"},
            new BoringItemModel(){Key="trowel"},
            new BoringItemModel(){Key="towel"},
            new BoringItemModel(){Key="planter box"},
            new BoringItemModel(){Key="anvil"},
            new BoringItemModel(){Key="axle"},
            new BoringItemModel(){Key="tuppence"},
            new BoringItemModel(){Key="casket"},
            new BoringItemModel(){Key="nosegay"},
            new BoringItemModel(){Key="trinket"},
            new BoringItemModel(){Key="credenza"},
            new BoringItemModel(){Key="writ"},
        };
        #endregion

        #region 怪物(Monsters)
        /// <summary>
        /// 怪物(Monsters)
        /// </summary>
        public ObservableCollection<MonsterModel> Monsters { get => _Monsters; }


        ObservableCollection<MonsterModel> _Monsters = new ObservableCollection<MonsterModel>()
        {
            new MonsterModel(){Key="Anhkheg",Level = 6,Item="chitin"},
            new MonsterModel(){Key="Ant",Level = 0,Item="antenna"},
            new MonsterModel(){Key="Ape",Level = 4,Item="ass"},
            new MonsterModel(){Key="Baluchitherium",Level = 14,Item="ear"},
            new MonsterModel(){Key="Beholder",Level = 10,Item="eyestalk"},
            new MonsterModel(){Key="Black Pudding",Level = 10,Item="saliva"},
            new MonsterModel(){Key="Blink Dog",Level = 4,Item="eyelid"},
            new MonsterModel(){Key="Cub Scout",Level = 1,Item="neckerchief"},
            new MonsterModel(){Key="Girl Scout",Level = 2,Item="cookie"},
            new MonsterModel(){Key="Boy Scout",Level = 3,Item="merit badge"},
            new MonsterModel(){Key="Eagle Scout",Level = 4,Item="merit badge"},
            new MonsterModel(){Key="Bugbear",Level = 3,Item="skin"},
            new MonsterModel(){Key="Bugboar",Level = 3,Item="tusk"},
            new MonsterModel(){Key="Boogie",Level = 3,Item="slime"},
            new MonsterModel(){Key="Camel",Level = 2,Item="hump"},
            new MonsterModel(){Key="Carrion Crawler",Level = 3,Item="egg"},
            new MonsterModel(){Key="Catoblepas",Level = 6,Item="neck"},
            new MonsterModel(){Key="Centaur",Level = 4,Item="rib"},
            new MonsterModel(){Key="Centipede",Level = 0,Item="leg"},
            new MonsterModel(){Key="Cockatrice",Level = 5,Item="wattle"},
            new MonsterModel(){Key="Couatl",Level = 9,Item="wing"},
            new MonsterModel(){Key="Crayfish",Level = 0,Item="antenna"},
            new MonsterModel(){Key="Demogorgon",Level = 53,Item="tentacle"},
            new MonsterModel(){Key="Jubilex",Level = 17,Item="gel"},
            new MonsterModel(){Key="Manes",Level = 1,Item="tooth"},
            new MonsterModel(){Key="Orcus",Level = 27,Item="wand"},
            new MonsterModel(){Key="Succubus",Level = 6,Item="bra"},
            new MonsterModel(){Key="Vrock",Level = 8,Item="neck"},
            new MonsterModel(){Key="Hezrou",Level = 9,Item="leg"},
            new MonsterModel(){Key="Glabrezu",Level = 10,Item="collar"},
            new MonsterModel(){Key="Nalfeshnee",Level = 11,Item="tusk"},
            new MonsterModel(){Key="Marilith",Level = 7,Item="arm"},
            new MonsterModel(){Key="Balor",Level = 8,Item="whip"},
            new MonsterModel(){Key="Yeenoghu",Level = 25,Item="flail"},
            new MonsterModel(){Key="Asmodeus",Level = 52,Item="leathers"},
            new MonsterModel(){Key="Baalzebul",Level = 43,Item="pants"},
            new MonsterModel(){Key="Barbed Devil",Level = 8,Item="flame"},
            new MonsterModel(){Key="Bone Devil",Level = 9,Item="hook"},
            new MonsterModel(){Key="Dispater",Level = 30,Item="matches"},
            new MonsterModel(){Key="Erinyes",Level = 6,Item="thong"},
            new MonsterModel(){Key="Geryon",Level = 30,Item="cornucopia"},
            new MonsterModel(){Key="Malebranche",Level = 5,Item="fork"},
            new MonsterModel(){Key="Ice Devil",Level = 11,Item="snow"},
            new MonsterModel(){Key="Lemure",Level = 3,Item="blob"},
            new MonsterModel(){Key="Pit Fiend",Level = 13,Item="seed"},
            new MonsterModel(){Key="Ankylosaurus",Level = 9,Item="tail"},
            new MonsterModel(){Key="Brontosaurus",Level = 30,Item="brain"},
            new MonsterModel(){Key="Diplodocus",Level = 24,Item="fin"},
            new MonsterModel(){Key="Elasmosaurus",Level = 15,Item="neck"},
            new MonsterModel(){Key="Gorgosaurus",Level = 13,Item="arm"},
            new MonsterModel(){Key="Iguanadon",Level = 6,Item="thumb"},
            new MonsterModel(){Key="Megalosaurus",Level = 12,Item="jaw"},
            new MonsterModel(){Key="Monoclonius",Level = 8,Item="horn"},
            new MonsterModel(){Key="Pentasaurus",Level = 12,Item="head"},
            new MonsterModel(){Key="Stegosaurus",Level = 18,Item="plate"},
            new MonsterModel(){Key="Triceratops",Level = 16,Item="horn"},
            new MonsterModel(){Key="Tyranosauraus Rex",Level = 18,Item="forearm"},
            new MonsterModel(){Key="Djinn",Level = 7,Item="lamp"},
            new MonsterModel(){Key="Doppleganger",Level = 4,Item="face"},
            new MonsterModel(){Key="Black Dragon",Level = 7, Item="" },
            new MonsterModel(){Key="Plaid Dragon",Level = 7,Item="sporrin"},
            new MonsterModel(){Key="Blue Dragon",Level = 9, Item="" },
            new MonsterModel(){Key="Beige Dragon",Level = 9, Item="" },
            new MonsterModel(){Key="Brass Dragon",Level = 7,Item="pole"},
            new MonsterModel(){Key="Tin Dragon",Level = 8, Item="" },
            new MonsterModel(){Key="Bronze Dragon",Level = 9,Item="medal"},
            new MonsterModel(){Key="Chromatic Dragon",Level = 16,Item="scale"},
            new MonsterModel(){Key="Copper Dragon",Level = 8,Item="loafer"},
            new MonsterModel(){Key="Gold Dragon",Level = 8,Item="filling"},
            new MonsterModel(){Key="Green Dragon",Level = 8, Item="" },
            new MonsterModel(){Key="Platinum Dragon",Level = 21, Item="" },
            new MonsterModel(){Key="Red Dragon",Level = 10,Item="cocktail"},
            new MonsterModel(){Key="Silver Dragon",Level = 10, Item="" },
            new MonsterModel(){Key="White Dragon",Level = 6,Item="tooth"},
            new MonsterModel(){Key="Dragon Turtle",Level = 13,Item="shell"},
            new MonsterModel(){Key="Dryad",Level = 2,Item="acorn"},
            new MonsterModel(){Key="Dwarf",Level = 1,Item="drawers"},
            new MonsterModel(){Key="Eel",Level = 2,Item="sashimi"},
            new MonsterModel(){Key="Efreet",Level = 10,Item="cinder"},
            new MonsterModel(){Key="Sand Elemental",Level = 8,Item="glass"},
            new MonsterModel(){Key="Bacon Elemental",Level = 10,Item="bit"},
            new MonsterModel(){Key="Porn Elemental",Level = 12,Item="lube"},
            new MonsterModel(){Key="Cheese Elemental",Level = 14,Item="curd"},
            new MonsterModel(){Key="Hair Elemental",Level = 16,Item="follicle"},
            new MonsterModel(){Key="Swamp Elf",Level = 1,Item="lilypad"},
            new MonsterModel(){Key="Brown Elf",Level = 1,Item="tusk"},
            new MonsterModel(){Key="Sea Elf",Level = 1,Item="jerkin"},
            new MonsterModel(){Key="Ettin",Level = 10,Item="fur"},
            new MonsterModel(){Key="Frog",Level = 0,Item="leg"},
            new MonsterModel(){Key="Violet Fungi",Level = 3,Item="spore"},
            new MonsterModel(){Key="Gargoyle",Level = 4,Item="gravel"},
            new MonsterModel(){Key="Gelatinous Cube",Level = 4,Item="jam"},
            new MonsterModel(){Key="Ghast",Level = 4,Item="vomit"},
            new MonsterModel(){Key="Ghost",Level = 10, Item="" },
            new MonsterModel(){Key="Ghoul",Level = 2,Item="muscle"},
            new MonsterModel(){Key="Humidity Giant",Level = 12,Item="drops"},
            new MonsterModel(){Key="Beef Giant",Level = 11,Item="steak"},
            new MonsterModel(){Key="Quartz Giant",Level = 10,Item="crystal"},
            new MonsterModel(){Key="Porcelain Giant",Level = 9,Item="fixture"},
            new MonsterModel(){Key="Rice Giant",Level = 8,Item="grain"},
            new MonsterModel(){Key="Cloud Giant",Level = 12,Item="condensation"},
            new MonsterModel(){Key="Fire Giant",Level = 11,Item="cigarettes"},
            new MonsterModel(){Key="Frost Giant",Level = 10,Item="snowman"},
            new MonsterModel(){Key="Hill Giant",Level = 8,Item="corpse"},
            new MonsterModel(){Key="Stone Giant",Level = 9,Item="hatchling"},
            new MonsterModel(){Key="Storm Giant",Level = 15,Item="barometer"},
            new MonsterModel(){Key="Mini Giant",Level = 4,Item="pompadour"},
            new MonsterModel(){Key="Gnoll",Level = 2,Item="collar"},
            new MonsterModel(){Key="Gnome",Level = 1,Item="hat"},
            new MonsterModel(){Key="Goblin",Level = 1,Item="ear"},
            new MonsterModel(){Key="Grid Bug",Level = 1,Item="carapace"},
            new MonsterModel(){Key="Jellyrock",Level = 9,Item="seedling"},
            new MonsterModel(){Key="Beer Golem",Level = 15,Item="foam"},
            new MonsterModel(){Key="Oxygen Golem",Level = 17,Item="platelet"},
            new MonsterModel(){Key="Cardboard Golem",Level = 14,Item="recycling"},
            new MonsterModel(){Key="Rubber Golem",Level = 16,Item="ball"},
            new MonsterModel(){Key="Leather Golem",Level = 15,Item="fob"},
            new MonsterModel(){Key="Gorgon",Level = 8,Item="testicle"},
            new MonsterModel(){Key="Gray Ooze",Level = 3,Item="gravy"},
            new MonsterModel(){Key="Green Slime",Level = 2,Item="sample"},
            new MonsterModel(){Key="Griffon",Level = 7,Item="nest"},
            new MonsterModel(){Key="Banshee",Level = 7,Item="larynx"},
            new MonsterModel(){Key="Harpy",Level = 3,Item="mascara"},
            new MonsterModel(){Key="Hell Hound",Level = 5,Item="tongue"},
            new MonsterModel(){Key="Hippocampus",Level = 4,Item="mane"},
            new MonsterModel(){Key="Hippogriff",Level = 3,Item="egg"},
            new MonsterModel(){Key="Hobgoblin",Level = 1,Item="patella"},
            new MonsterModel(){Key="Homonculus",Level = 2,Item="fluid"},
            new MonsterModel(){Key="Hydra",Level = 8,Item="gyrum"},
            new MonsterModel(){Key="Imp",Level = 2,Item="tail"},
            new MonsterModel(){Key="Invisible Stalker",Level = 8, Item="" },
            new MonsterModel(){Key="Iron Peasant",Level = 3,Item="chaff"},
            new MonsterModel(){Key="Jumpskin",Level = 3,Item="shin"},
            new MonsterModel(){Key="Kobold",Level = 1,Item="penis"},
            new MonsterModel(){Key="Leprechaun",Level = 1,Item="wallet"},
            new MonsterModel(){Key="Leucrotta",Level = 6,Item="hoof"},
            new MonsterModel(){Key="Lich",Level = 11,Item="crown"},
            new MonsterModel(){Key="Lizard Man",Level = 2,Item="tail"},
            new MonsterModel(){Key="Lurker",Level = 10,Item="sac"},
            new MonsterModel(){Key="Manticore",Level = 6,Item="spike"},
            new MonsterModel(){Key="Mastodon",Level = 12,Item="tusk"},
            new MonsterModel(){Key="Medusa",Level = 6,Item="eye"},
            new MonsterModel(){Key="Multicell",Level = 2,Item="dendrite"},
            new MonsterModel(){Key="Pirate",Level = 1,Item="booty"},
            new MonsterModel(){Key="Berserker",Level = 1,Item="shirt"},
            new MonsterModel(){Key="Caveman",Level = 2,Item="club"},
            new MonsterModel(){Key="Dervish",Level = 1,Item="robe"},
            new MonsterModel(){Key="Merman",Level = 1,Item="trident"},
            new MonsterModel(){Key="Mermaid",Level = 1,Item="gills"},
            new MonsterModel(){Key="Mimic",Level = 9,Item="hinge"},
            new MonsterModel(){Key="Mind Flayer",Level = 8,Item="tentacle"},
            new MonsterModel(){Key="Minotaur",Level = 6,Item="map"},
            new MonsterModel(){Key="Yellow Mold",Level = 1,Item="spore"},
            new MonsterModel(){Key="Morkoth",Level = 7,Item="teeth"},
            new MonsterModel(){Key="Mummy",Level = 6,Item="gauze"},
            new MonsterModel(){Key="Naga",Level = 9,Item="rattle"},
            new MonsterModel(){Key="Nebbish",Level = 1,Item="belly"},
            new MonsterModel(){Key="Neo-Otyugh",Level = 11,Item="organ "},
            new MonsterModel(){Key="Nixie",Level = 1,Item="webbing"},
            new MonsterModel(){Key="Nymph",Level = 3,Item="hanky"},
            new MonsterModel(){Key="Ochre Jelly",Level = 6,Item="nucleus"},
            new MonsterModel(){Key="Octopus",Level = 2,Item="beak"},
            new MonsterModel(){Key="Ogre",Level = 4,Item="talon"},
            new MonsterModel(){Key="Ogre Mage",Level = 5,Item="apparel"},
            new MonsterModel(){Key="Orc",Level = 1,Item="snout"},
            new MonsterModel(){Key="Otyugh",Level = 7,Item="organ"},
            new MonsterModel(){Key="Owlbear",Level = 5,Item="feather"},
            new MonsterModel(){Key="Pegasus",Level = 4,Item="aileron"},
            new MonsterModel(){Key="Peryton",Level = 4,Item="antler"},
            new MonsterModel(){Key="Piercer",Level = 3,Item="tip"},
            new MonsterModel(){Key="Pixie",Level = 1,Item="dust"},
            new MonsterModel(){Key="Man-o-war",Level = 3,Item="tentacle"},
            new MonsterModel(){Key="Purple Worm",Level = 15,Item="dung"},
            new MonsterModel(){Key="Quasit",Level = 3,Item="tail"},
            new MonsterModel(){Key="Rakshasa",Level = 7,Item="pajamas"},
            new MonsterModel(){Key="Rat",Level = 0,Item="tail"},
            new MonsterModel(){Key="Remorhaz",Level = 11,Item="protrusion"},
            new MonsterModel(){Key="Roc",Level = 18,Item="wing"},
            new MonsterModel(){Key="Roper",Level = 11,Item="twine"},
            new MonsterModel(){Key="Rot Grub",Level = 1,Item="eggsac"},
            new MonsterModel(){Key="Rust Monster",Level = 5,Item="shavings"},
            new MonsterModel(){Key="Satyr",Level = 5,Item="hoof"},
            new MonsterModel(){Key="Sea Hag",Level = 3,Item="wart"},
            new MonsterModel(){Key="Silkie",Level = 3,Item="fur"},
            new MonsterModel(){Key="Shadow",Level = 3,Item="silhouette"},
            new MonsterModel(){Key="Shambling Mound",Level = 10,Item="mulch"},
            new MonsterModel(){Key="Shedu",Level = 9,Item="hoof"},
            new MonsterModel(){Key="Shrieker",Level = 3,Item="stalk"},
            new MonsterModel(){Key="Skeleton",Level = 1,Item="clavicle"},
            new MonsterModel(){Key="Spectre",Level = 7,Item="vestige"},
            new MonsterModel(){Key="Sphinx",Level = 10,Item="paw"},
            new MonsterModel(){Key="Spider",Level = 0,Item="web"},
            new MonsterModel(){Key="Sprite",Level = 1,Item="can"},
            new MonsterModel(){Key="Stirge",Level = 1,Item="proboscis"},
            new MonsterModel(){Key="Stun Bear",Level = 5,Item="tooth"},
            new MonsterModel(){Key="Stun Worm",Level = 2,Item="trode"},
            new MonsterModel(){Key="Su-monster",Level = 5,Item="tail"},
            new MonsterModel(){Key="Sylph",Level = 3,Item="thigh"},
            new MonsterModel(){Key="Titan",Level = 20,Item="sandal"},
            new MonsterModel(){Key="Trapper",Level = 12,Item="shag"},
            new MonsterModel(){Key="Treant",Level = 10,Item="acorn"},
            new MonsterModel(){Key="Triton",Level = 3,Item="scale"},
            new MonsterModel(){Key="Troglodyte",Level = 2,Item="tail"},
            new MonsterModel(){Key="Troll",Level = 6,Item="hide"},
            new MonsterModel(){Key="Umber Hulk",Level = 8,Item="claw"},
            new MonsterModel(){Key="Unicorn",Level = 4,Item="blood"},
            new MonsterModel(){Key="Vampire",Level = 8,Item="pancreas"},
            new MonsterModel(){Key="Wight",Level = 4,Item="lung"},
            new MonsterModel(){Key="Will-o'-the-Wisp",Level = 9,Item="wisp"},
            new MonsterModel(){Key="Wraith",Level = 5,Item="finger"},
            new MonsterModel(){Key="Wyvern",Level = 7,Item="wing"},
            new MonsterModel(){Key="Xorn",Level = 7,Item="jaw"},
            new MonsterModel(){Key="Yeti",Level = 4,Item="fur"},
            new MonsterModel(){Key="Zombie",Level = 2,Item="forehead"},
            new MonsterModel(){Key="Wasp",Level = 0,Item="stinger"},
            new MonsterModel(){Key="Rat",Level = 1,Item="tail"},
            new MonsterModel(){Key="Bunny",Level = 0,Item="ear"},
            new MonsterModel(){Key="Moth",Level = 0,Item="dust"},
            new MonsterModel(){Key="Beagle",Level = 0,Item="collar"},
            new MonsterModel(){Key="Midge",Level = 0,Item="corpse"},
            new MonsterModel(){Key="Ostrich",Level = 1,Item="beak"},
            new MonsterModel(){Key="Billy Goat",Level = 1,Item="beard"},
            new MonsterModel(){Key="Bat",Level = 1,Item="wing"},
            new MonsterModel(){Key="Koala",Level = 2,Item="heart"},
            new MonsterModel(){Key="Wolf",Level = 2,Item="paw"},
            new MonsterModel(){Key="Whippet",Level = 2,Item="collar"},
            new MonsterModel(){Key="Uruk",Level = 2,Item="boot"},
            new MonsterModel(){Key="Poroid",Level = 4,Item="node"},
            new MonsterModel(){Key="Moakum",Level = 8,Item="frenum"},
            new MonsterModel(){Key="Fly",Level = 0, Item="" },
            new MonsterModel(){Key="Hogbird",Level = 3,Item="curl"},
            new MonsterModel(){Key="Wolog",Level = 4,Item="lemma"},

        };
        #endregion

        #region 标题(Titles)
        /// <summary>
        /// 标题(Titles)
        /// </summary>
        public ObservableCollection<TitleModel> Titles { get => _Titles; }


        ObservableCollection<TitleModel> _Titles = new ObservableCollection<TitleModel>()
        {
            new TitleModel(){Key="Mr."},
            new TitleModel(){Key="Mrs."},
            new TitleModel(){Key="Sir"},
            new TitleModel(){Key="Sgt."},
            new TitleModel(){Key="Ms."},
            new TitleModel(){Key="Captain"},
            new TitleModel(){Key="Chief"},
            new TitleModel(){Key="Admiral"},
            new TitleModel(){Key="Saint"},
        };
        #endregion

        #region 令人印象深刻的标题(ImpressiveTitles)
        /// <summary>
        /// 令人印象深刻的标题(ImpressiveTitles)
        /// </summary>
        public ObservableCollection<ImpressiveTitleModel> ImpressiveTitles { get => _ImpressiveTitles; }


        ObservableCollection<ImpressiveTitleModel> _ImpressiveTitles = new ObservableCollection<ImpressiveTitleModel>()
        {
            new ImpressiveTitleModel(){Key="King"},
            new ImpressiveTitleModel(){Key="Queen"},
            new ImpressiveTitleModel(){Key="Lord"},
            new ImpressiveTitleModel(){Key="Lady"},
            new ImpressiveTitleModel(){Key="Viceroy"},
            new ImpressiveTitleModel(){Key="Mayor"},
            new ImpressiveTitleModel(){Key="Prince"},
            new ImpressiveTitleModel(){Key="Princess"},
            new ImpressiveTitleModel(){Key="Chief"},
            new ImpressiveTitleModel(){Key="Boss"},
            new ImpressiveTitleModel(){Key="Archbishop"},
        };
        #endregion
    }
}
