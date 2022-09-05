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
            new SpellModel(){Key="SpellSlimeFinger"},
            new SpellModel(){Key="SpellRabbitPunch"},
            new SpellModel(){Key="SpellHastiness"},
            new SpellModel(){Key="SpellGoodMove"},
            new SpellModel(){Key="SpellSadness"},
            new SpellModel(){Key="SpellSeasick"},
            new SpellModel(){Key="SpellShoelaces"},
            new SpellModel(){Key="SpellInnoculate"},
            new SpellModel(){Key="SpellConeOfAnnoyance"},
            new SpellModel(){Key="SpellMagneticOrb"},
            new SpellModel(){Key="SpellInvisibleHands"},
            new SpellModel(){Key="SpellRevoltingCloud"},
            new SpellModel(){Key="SpellAqueousHumor"},
            new SpellModel(){Key="SpellSpectralMiasma"},
            new SpellModel(){Key="SpellCleverFellow"},
            new SpellModel(){Key="SpellLockjaw"},
            new SpellModel(){Key="SpellHistoryLesson"},
            new SpellModel(){Key="SpellHydrophobia"},
            new SpellModel(){Key="SpellBigSister"},
            new SpellModel(){Key="SpellConeOfPaste"},
            new SpellModel(){Key="SpellMulligan"},
            new SpellModel(){Key="SpellNestorsBrightIdea"},
            new SpellModel(){Key="SpellHolyBatpole"},
            new SpellModel(){Key="SpellTumorBenign"},
            new SpellModel(){Key="SpellBraingate"},
            new SpellModel(){Key="SpellSummonABitch"},
            new SpellModel(){Key="SpellNonplus"},
            new SpellModel(){Key="SpellAnimateNightstand"},
            new SpellModel(){Key="SpellEyeOfTheTroglodyte"},
            new SpellModel(){Key="SpellCurseName"},
            new SpellModel(){Key="SpellDropsy"},
            new SpellModel(){Key="SpellVitreousHumor"},
            new SpellModel(){Key="SpellRogersGrandIllusion"},
            new SpellModel(){Key="SpellCovet"},
            new SpellModel(){Key="SpellBlackIdaho"},
            new SpellModel(){Key="SpellAstralMiasma"},
            new SpellModel(){Key="SpellSpectralOyster"},
            new SpellModel(){Key="SpellAcridHands"},
            new SpellModel(){Key="SpellAngioplasty"},
            new SpellModel(){Key="SpellGrognorsBigDayOff"},
            new SpellModel(){Key="SpellTumorMalignant"},
            new SpellModel(){Key="SpellAnimateTunic"},
            new SpellModel(){Key="SpellUrsineArmor"},
            new SpellModel(){Key="SpellHolyRoller"},
            new SpellModel(){Key="SpellTonsilectomy"},
            new SpellModel(){Key="SpellCurseFamily"},
            new SpellModel(){Key="SpellInfiniteConfusion"},
        };
        #endregion

        #region 进攻特征(OffenseAttributes)

        /// <summary>
        /// 进攻特征(OffenseAttributes)
        /// </summary>
        public static ObservableCollection<ModifierModel> OffenseAttributes = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="ModifierPolished",Quality = 1},
            new ModifierModel(){Key="ModifierSerrated",Quality = 1},
            new ModifierModel(){Key="ModifierHeavy",Quality = 1},
            new ModifierModel(){Key="ModifierPronged",Quality = 2},
            new ModifierModel(){Key="ModifierSteely",Quality = 2},
            new ModifierModel(){Key="ModifierVicious",Quality = 3},
            new ModifierModel(){Key="ModifierVenomed",Quality = 4},
            new ModifierModel(){Key="ModifierStabbity",Quality = 4},
            new ModifierModel(){Key="ModifierDancing",Quality = 5},
            new ModifierModel(){Key="ModifierInvisible",Quality = 6},
            new ModifierModel(){Key="ModifierVorpal",Quality = 7},
        };
        #endregion

        #region 防御特征(DefenseAttributes)

        /// <summary>
        /// 防御特征(DefenseAttributes)
        /// </summary>
        public static ObservableCollection<ModifierModel> DefenseAttributes = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="ModifierStudded",Quality = 1},
            new ModifierModel(){Key="ModifierBanded",Quality = 2},
            new ModifierModel(){Key="ModifierGilded",Quality = 2},
            new ModifierModel(){Key="ModifierFestooned",Quality = 3},
            new ModifierModel(){Key="ModifierHoly",Quality = 4},
            new ModifierModel(){Key="ModifierCambric",Quality = 1},
            new ModifierModel(){Key="ModifierFine",Quality = 4},
            new ModifierModel(){Key="ModifierImpressive",Quality = 5},
            new ModifierModel(){Key="ModifierCustom",Quality = 3},
        };
        #endregion

        #region 进攻损坏(OffenseBad)
        /// <summary>
        /// 进攻损坏(OffenseBad)
        /// </summary>
        public static ObservableCollection<ModifierModel> OffenseBads = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="ModifierDull",Quality = -2},
            new ModifierModel(){Key="ModifierTarnished",Quality = -1},
            new ModifierModel(){Key="ModifierRusty",Quality = -3},
            new ModifierModel(){Key="ModifierPadded",Quality = -5},
            new ModifierModel(){Key="ModifierBent",Quality = -4},
            new ModifierModel(){Key="ModifierMini",Quality = -4},
            new ModifierModel(){Key="ModifierRubber",Quality = -6},
            new ModifierModel(){Key="ModifierNerf",Quality = -7},
            new ModifierModel(){Key="ModifierUnbalanced",Quality = -2},
        };
        #endregion

        #region 防御损坏(DefenseBads)
        /// <summary>
        /// 防御损坏(DefenseBads)
        /// </summary>
        public static ObservableCollection<ModifierModel> DefenseBads = new ObservableCollection<ModifierModel>()
        {
            new ModifierModel(){Key="ModifierHoley",Quality = -1},
            new ModifierModel(){Key="ModifierPatched",Quality = -1},
            new ModifierModel(){Key="ModifierThreadbare",Quality = -2},
            new ModifierModel(){Key="ModifierFaded",Quality = -1},
            new ModifierModel(){Key="ModifierRusty",Quality = -3},
            new ModifierModel(){Key="ModifierMotheaten",Quality = -3},
            new ModifierModel(){Key="ModifierMildewed",Quality = -2},
            new ModifierModel(){Key="ModifierTorn",Quality = -3},
            new ModifierModel(){Key="ModifierDented",Quality = -3},
            new ModifierModel(){Key="ModifierCursed",Quality = -5},
            new ModifierModel(){Key="ModifierPlastic",Quality = -4},
            new ModifierModel(){Key="ModifierCracked",Quality = -4},
            new ModifierModel(){Key="ModifierWarped",Quality = -3},
            new ModifierModel(){Key="ModifierCorroded",Quality = -3},
        };
        #endregion

        #region 盾牌(Shields)
        /// <summary>
        /// 盾牌(Shields)
        /// </summary>
        public static ObservableCollection<EquipmentPresetModel> Shields = new ObservableCollection<EquipmentPresetModel>()
        {
            new EquipmentPresetModel(){Key="EquipmentParasol",Quality = 0},
            new EquipmentPresetModel(){Key="EquipmentPiePlate",Quality = 1},
            new EquipmentPresetModel(){Key="EquipmentGarbageCanLid",Quality = 2},
            new EquipmentPresetModel(){Key="EquipmentBuckler",Quality = 3},
            new EquipmentPresetModel(){Key="EquipmentPlexiglass",Quality = 4},
            new EquipmentPresetModel(){Key="EquipmentFender",Quality = 4},
            new EquipmentPresetModel(){Key="EquipmentRoundShield",Quality = 5},
            new EquipmentPresetModel(){Key="EquipmentCarapace",Quality = 4},
            new EquipmentPresetModel(){Key="EquipmentScutum",Quality = 6},
            new EquipmentPresetModel(){Key="EquipmentPropugner",Quality = 6},
            new EquipmentPresetModel(){Key="EquipmentKiteShield",Quality = 7},
            new EquipmentPresetModel(){Key="EquipmentPavise",Quality = 8},
            new EquipmentPresetModel(){Key="EquipmentTowerShield",Quality = 9},
            new EquipmentPresetModel(){Key="EquipmentBaroqueShield",Quality = 11},
            new EquipmentPresetModel(){Key="EquipmentAegis",Quality = 12},
            new EquipmentPresetModel(){Key="EquipmentMagneticField",Quality = 18},
        };
        #endregion

        #region 盔甲(Armors)
        /// <summary>
        /// 盔甲(Armors)
        /// </summary>
        public static ObservableCollection<EquipmentPresetModel> Armors = new ObservableCollection<EquipmentPresetModel>()
        {
            new EquipmentPresetModel(){Key="EquipmentLace",Quality = 1},
            new EquipmentPresetModel(){Key="EquipmentMacrame",Quality = 2},
            new EquipmentPresetModel(){Key="EquipmentBurlap",Quality = 3},
            new EquipmentPresetModel(){Key="EquipmentCanvas",Quality = 4},
            new EquipmentPresetModel(){Key="EquipmentFlannel",Quality = 5},
            new EquipmentPresetModel(){Key="EquipmentChamois",Quality = 6},
            new EquipmentPresetModel(){Key="EquipmentPleathers",Quality = 7},
            new EquipmentPresetModel(){Key="EquipmentLeathers",Quality = 8},
            new EquipmentPresetModel(){Key="EquipmentBearskin",Quality = 9},
            new EquipmentPresetModel(){Key="EquipmentRingmail",Quality = 10},
            new EquipmentPresetModel(){Key="EquipmentScaleMail",Quality = 12},
            new EquipmentPresetModel(){Key="EquipmentChainmail",Quality = 14},
            new EquipmentPresetModel(){Key="EquipmentSplintMail",Quality = 15},
            new EquipmentPresetModel(){Key="EquipmentPlatemail",Quality = 16},
            new EquipmentPresetModel(){Key="EquipmentABS",Quality = 17},
            new EquipmentPresetModel(){Key="EquipmentKevlar",Quality = 18},
            new EquipmentPresetModel(){Key="EquipmentTitanium",Quality = 19},
            new EquipmentPresetModel(){Key="EquipmentMithrilMail",Quality = 20},
            new EquipmentPresetModel(){Key="EquipmentDiamondMail",Quality = 25},
            new EquipmentPresetModel(){Key="EquipmentPlasma",Quality = 30},
        };
        #endregion

        #region 武器(Weapons)
        /// <summary>
        /// 武器(Weapons)
        /// </summary>
        public static ObservableCollection<EquipmentPresetModel> Weapons = new ObservableCollection<EquipmentPresetModel>()
        {
            new EquipmentPresetModel(){Key="EquipmentStick",Quality = 0},
            new EquipmentPresetModel(){Key="EquipmentBroken Bottle",Quality = 1},
            new EquipmentPresetModel(){Key="EquipmentShiv",Quality = 1},
            new EquipmentPresetModel(){Key="EquipmentSprig",Quality = 1},
            new EquipmentPresetModel(){Key="EquipmentOxgoad",Quality = 1},
            new EquipmentPresetModel(){Key="EquipmentEelspear",Quality = 2},
            new EquipmentPresetModel(){Key="EquipmentBowieKnife",Quality = 2},
            new EquipmentPresetModel(){Key="EquipmentClawHammer",Quality = 2},
            new EquipmentPresetModel(){Key="EquipmentHandpeen",Quality = 2},
            new EquipmentPresetModel(){Key="EquipmentAndiron",Quality = 3},
            new EquipmentPresetModel(){Key="EquipmentHatchet",Quality = 3},
            new EquipmentPresetModel(){Key="EquipmentTomahawk",Quality = 3},
            new EquipmentPresetModel(){Key="EquipmentHackbarm",Quality = 3},
            new EquipmentPresetModel(){Key="EquipmentCrowbar",Quality = 4},
            new EquipmentPresetModel(){Key="EquipmentMace",Quality = 4},
            new EquipmentPresetModel(){Key="EquipmentBattleadze",Quality = 4},
            new EquipmentPresetModel(){Key="EquipmentLeafmace",Quality = 5},
            new EquipmentPresetModel(){Key="EquipmentShortsword",Quality = 5},
            new EquipmentPresetModel(){Key="EquipmentLongiron",Quality = 5},
            new EquipmentPresetModel(){Key="EquipmentPoachard",Quality = 5},
            new EquipmentPresetModel(){Key="EquipmentBaselard",Quality = 5},
            new EquipmentPresetModel(){Key="EquipmentWhinyard",Quality = 6},
            new EquipmentPresetModel(){Key="EquipmentBlunderbuss",Quality = 6},
            new EquipmentPresetModel(){Key="EquipmentLongsword",Quality = 6},
            new EquipmentPresetModel(){Key="EquipmentCrankbow",Quality = 6},
            new EquipmentPresetModel(){Key="EquipmentBlibo",Quality = 7},
            new EquipmentPresetModel(){Key="EquipmentBroadsword",Quality = 7},
            new EquipmentPresetModel(){Key="EquipmentKreen",Quality = 7},
            new EquipmentPresetModel(){Key="EquipmentWarhammer",Quality = 7},
            new EquipmentPresetModel(){Key="EquipmentMorningStar",Quality = 8},
            new EquipmentPresetModel(){Key="EquipmentPoleAdze",Quality = 8},
            new EquipmentPresetModel(){Key="EquipmentSpontoon",Quality = 8},
            new EquipmentPresetModel(){Key="EquipmentBastardSword",Quality = 9},
            new EquipmentPresetModel(){Key="EquipmentPeenArm",Quality = 9},
            new EquipmentPresetModel(){Key="EquipmentCulverin",Quality = 10},
            new EquipmentPresetModel(){Key="EquipmentLance",Quality = 10},
            new EquipmentPresetModel(){Key="EquipmentHalberd",Quality = 11},
            new EquipmentPresetModel(){Key="EquipmentPoleax",Quality = 12},
            new EquipmentPresetModel(){Key="EquipmentBandyclef",Quality = 15},
        };
        #endregion

        #region 特价(Specials)
        /// <summary>
        /// 特价(Specials)
        /// </summary>
        public static ObservableCollection<SpecialModel> Specials = new ObservableCollection<SpecialModel>()
        {
            new SpecialModel(){Key="SpecialDiadem"},
            new SpecialModel(){Key="SpecialFestoon"},
            new SpecialModel(){Key="SpecialGemstone"},
            new SpecialModel(){Key="SpecialPhial"},
            new SpecialModel(){Key="SpecialTiara"},
            new SpecialModel(){Key="SpecialScabbard"},
            new SpecialModel(){Key="SpecialArrow"},
            new SpecialModel(){Key="SpecialLens"},
            new SpecialModel(){Key="SpecialLamp"},
            new SpecialModel(){Key="SpecialHymnal"},
            new SpecialModel(){Key="SpecialFleece"},
            new SpecialModel(){Key="SpecialLaurel"},
            new SpecialModel(){Key="SpecialBrooch"},
            new SpecialModel(){Key="SpecialGimlet"},
            new SpecialModel(){Key="SpecialCobble"},
            new SpecialModel(){Key="SpecialAlbatross"},
            new SpecialModel(){Key="SpecialBrazier"},
            new SpecialModel(){Key="SpecialBandolier"},
            new SpecialModel(){Key="SpecialTome"},
            new SpecialModel(){Key="SpecialGarnet"},
            new SpecialModel(){Key="SpecialAmethyst"},
            new SpecialModel(){Key="SpecialCandelabra"},
            new SpecialModel(){Key="SpecialCorset"},
            new SpecialModel(){Key="SpecialSphere"},
            new SpecialModel(){Key="SpecialSceptre"},
            new SpecialModel(){Key="SpecialAnkh"},
            new SpecialModel(){Key="SpecialTalisman"},
            new SpecialModel(){Key="SpecialOrb"},
            new SpecialModel(){Key="SpecialGammel"},
            new SpecialModel(){Key="SpecialOrnament"},
            new SpecialModel(){Key="SpecialBrocade"},
            new SpecialModel(){Key="SpecialGaloon"},
            new SpecialModel(){Key="SpecialBijou"},
            new SpecialModel(){Key="SpecialSpangle"},
            new SpecialModel(){Key="SpecialGimcrack"},
            new SpecialModel(){Key="SpecialHood"},
            new SpecialModel(){Key="SpecialVulpeculum"},
        };
        #endregion

        #region 物品特征(ItemAttributes)
        /// <summary>
        /// 物品特征(ItemAttributes)
        /// </summary>
        public static ObservableCollection<ItemAttributeModel> ItemAttributes = new ObservableCollection<ItemAttributeModel>()
        {
            new ItemAttributeModel(){Key="ItemAttributeGolden"},
            new ItemAttributeModel(){Key="ItemAttributeGilded"},
            new ItemAttributeModel(){Key="ItemAttributeSpectral"},
            new ItemAttributeModel(){Key="ItemAttributeAstral"},
            new ItemAttributeModel(){Key="ItemAttributeGarlanded"},
            new ItemAttributeModel(){Key="ItemAttributePrecious"},
            new ItemAttributeModel(){Key="ItemAttributeCrafted"},
            new ItemAttributeModel(){Key="ItemAttributeDual"},
            new ItemAttributeModel(){Key="ItemAttributeFiligreed"},
            new ItemAttributeModel(){Key="ItemAttributeCruciate"},
            new ItemAttributeModel(){Key="ItemAttributeArcane"},
            new ItemAttributeModel(){Key="ItemAttributeBlessed"},
            new ItemAttributeModel(){Key="ItemAttributeReverential"},
            new ItemAttributeModel(){Key="ItemAttributeLucky"},
            new ItemAttributeModel(){Key="ItemAttributeEnchanted"},
            new ItemAttributeModel(){Key="ItemAttributeGleaming"},
            new ItemAttributeModel(){Key="ItemAttributeGrandiose"},
            new ItemAttributeModel(){Key="ItemAttributeSacred"},
            new ItemAttributeModel(){Key="ItemAttributeLegendary"},
            new ItemAttributeModel(){Key="ItemAttributeMythic"},
            new ItemAttributeModel(){Key="ItemAttributeCrystalline"},
            new ItemAttributeModel(){Key="ItemAttributeAustere"},
            new ItemAttributeModel(){Key="ItemAttributeOstentatious"},
            new ItemAttributeModel(){Key="ItemAttributeOneTrue"},
            new ItemAttributeModel(){Key="ItemAttributeProverbial"},
            new ItemAttributeModel(){Key="ItemAttributeFearsome"},
            new ItemAttributeModel(){Key="ItemAttributeDeadly"},
            new ItemAttributeModel(){Key="ItemAttributeBenevolent"},
            new ItemAttributeModel(){Key="ItemAttributeUnearthly"},
            new ItemAttributeModel(){Key="ItemAttributeMagnificent"},
            new ItemAttributeModel(){Key="ItemAttributeIron"},
            new ItemAttributeModel(){Key="ItemAttributeOrmolu"},
            new ItemAttributeModel(){Key="ItemAttributePuissant"},
        };
        #endregion

        #region 物品(ItemOfs)
        /// <summary>
        /// 物品(ItemOfs)
        /// </summary>
        public static ObservableCollection<ItemOfModel> ItemOfs = new ObservableCollection<ItemOfModel>()
        {
            new ItemOfModel(){Key="ItemOfForeboding"},
            new ItemOfModel(){Key="ItemOfForeshadowing"},
            new ItemOfModel(){Key="ItemOfNervousness"},
            new ItemOfModel(){Key="ItemOfHappiness"},
            new ItemOfModel(){Key="ItemOfTorpor"},
            new ItemOfModel(){Key="ItemOfDanger"},
            new ItemOfModel(){Key="ItemOfCraft"},
            new ItemOfModel(){Key="ItemOfSilence"},
            new ItemOfModel(){Key="ItemOfInvisibility"},
            new ItemOfModel(){Key="ItemOfRapidity"},
            new ItemOfModel(){Key="ItemOfPleasure"},
            new ItemOfModel(){Key="ItemOfPracticality"},
            new ItemOfModel(){Key="ItemOfHurting"},
            new ItemOfModel(){Key="ItemOfJoy"},
            new ItemOfModel(){Key="ItemOfPetulance"},
            new ItemOfModel(){Key="ItemOfIntrusion"},
            new ItemOfModel(){Key="ItemOfChaos"},
            new ItemOfModel(){Key="ItemOfSuffering"},
            new ItemOfModel(){Key="ItemOfExtroversion"},
            new ItemOfModel(){Key="ItemOfFrenzy"},
            new ItemOfModel(){Key="ItemOfSisu"},
            new ItemOfModel(){Key="ItemOfSolitude"},
            new ItemOfModel(){Key="ItemOfPunctuality"},
            new ItemOfModel(){Key="ItemOfEfficiency"},
            new ItemOfModel(){Key="ItemOfComfort"},
            new ItemOfModel(){Key="ItemOfPatience"},
            new ItemOfModel(){Key="ItemOfInternment"},
            new ItemOfModel(){Key="ItemOfIncarceration"},
            new ItemOfModel(){Key="ItemOfMisapprehension"},
            new ItemOfModel(){Key="ItemOfLoyalty"},
            new ItemOfModel(){Key="ItemOfEnvy"},
            new ItemOfModel(){Key="ItemOfAcrimony"},
            new ItemOfModel(){Key="ItemOfWorry"},
            new ItemOfModel(){Key="ItemOfFear"},
            new ItemOfModel(){Key="ItemOfAwe"},
            new ItemOfModel(){Key="ItemOfGuile"},
            new ItemOfModel(){Key="ItemOfPrurience"},
            new ItemOfModel(){Key="ItemOfFortune"},
            new ItemOfModel(){Key="ItemOfPerspicacity"},
            new ItemOfModel(){Key="ItemOfDomination"},
            new ItemOfModel(){Key="ItemOfSubmission"},
            new ItemOfModel(){Key="ItemOfFealty"},
            new ItemOfModel(){Key="ItemOfHunger"},
            new ItemOfModel(){Key="ItemOfDespair"},
            new ItemOfModel(){Key="ItemOfCruelty"},
            new ItemOfModel(){Key="ItemOfGrob"},
            new ItemOfModel(){Key="ItemOfDignard"},
            new ItemOfModel(){Key="ItemOfRa"},
            new ItemOfModel(){Key="ItemOfthe Bone"},
            new ItemOfModel(){Key="ItemOfDiamonique"},
            new ItemOfModel(){Key="ItemOfElectrum"},
            new ItemOfModel(){Key="ItemOfHydragyrum"},
        };
        #endregion

        #region 无聊的物品(BoringItems)
        /// <summary>
        /// 无聊的物品(BoringItems)
        /// </summary>
        public static ObservableCollection<BoringItemModel> BoringItems = new ObservableCollection<BoringItemModel>()
        {
            new BoringItemModel(){Key="BoringItemnail"},
            new BoringItemModel(){Key="BoringItemlunchpail"},
            new BoringItemModel(){Key="BoringItemsock"},
            new BoringItemModel(){Key="BoringItemI.O.U."},
            new BoringItemModel(){Key="BoringItemcookie"},
            new BoringItemModel(){Key="BoringItempint"},
            new BoringItemModel(){Key="BoringItemtoothpick"},
            new BoringItemModel(){Key="BoringItemwrit"},
            new BoringItemModel(){Key="BoringItemnewspaper"},
            new BoringItemModel(){Key="BoringItemletter"},
            new BoringItemModel(){Key="BoringItemplank"},
            new BoringItemModel(){Key="BoringItemhat"},
            new BoringItemModel(){Key="BoringItemegg"},
            new BoringItemModel(){Key="BoringItemcoin"},
            new BoringItemModel(){Key="BoringItemneedle"},
            new BoringItemModel(){Key="BoringItembucket"},
            new BoringItemModel(){Key="BoringItemladder"},
            new BoringItemModel(){Key="BoringItemchicken"},
            new BoringItemModel(){Key="BoringItemtwig"},
            new BoringItemModel(){Key="BoringItemdirtclod"},
            new BoringItemModel(){Key="BoringItemcounterpane"},
            new BoringItemModel(){Key="BoringItemvest"},
            new BoringItemModel(){Key="BoringItemteratoma"},
            new BoringItemModel(){Key="BoringItembunny"},
            new BoringItemModel(){Key="BoringItemrock"},
            new BoringItemModel(){Key="BoringItempole"},
            new BoringItemModel(){Key="BoringItemcarrot"},
            new BoringItemModel(){Key="BoringItemcanoe"},
            new BoringItemModel(){Key="BoringIteminkwell"},
            new BoringItemModel(){Key="BoringItemhoe"},
            new BoringItemModel(){Key="BoringItembandage"},
            new BoringItemModel(){Key="BoringItemtrowel"},
            new BoringItemModel(){Key="BoringItemtowel"},
            new BoringItemModel(){Key="BoringItemplanter box"},
            new BoringItemModel(){Key="BoringItemanvil"},
            new BoringItemModel(){Key="BoringItemaxle"},
            new BoringItemModel(){Key="BoringItemtuppence"},
            new BoringItemModel(){Key="BoringItemcasket"},
            new BoringItemModel(){Key="BoringItemnosegay"},
            new BoringItemModel(){Key="BoringItemtrinket"},
            new BoringItemModel(){Key="BoringItemcredenza"},
            new BoringItemModel(){Key="BoringItemwrit"},
        };
        #endregion

        #region 怪物(Monsters)
        /// <summary>
        /// 怪物(Monsters)
        /// </summary>
        public static ObservableCollection<MonsterModel> Monsters = new ObservableCollection<MonsterModel>()
        {
            new MonsterModel(){Key="MonsterAnhkheg",Level = 6 ,Item="MonsterItemchitin"},
            new MonsterModel(){Key="MonsterAnt",Level = 0 ,Item="MonsterItemantenna"},
            new MonsterModel(){Key="MonsterApe",Level = 4 ,Item="MonsterItemass"},
            new MonsterModel(){Key="MonsterBaluchitherium",Level = 14 ,Item="MonsterItemear"},
            new MonsterModel(){Key="MonsterBeholder",Level = 10 ,Item="MonsterItemeyestalk"},
            new MonsterModel(){Key="MonsterBlackPudding",Level = 10 ,Item="MonsterItemsaliva"},
            new MonsterModel(){Key="MonsterBlinkDog",Level = 4 ,Item="MonsterItemeyelid"},
            new MonsterModel(){Key="MonsterCubScout",Level = 1 ,Item="MonsterItemneckerchief"},
            new MonsterModel(){Key="MonsterGirlScout",Level = 2 ,Item="MonsterItemcookie"},
            new MonsterModel(){Key="MonsterBoyScout",Level = 3 ,Item="MonsterItemmeritbadge"},
            new MonsterModel(){Key="MonsterEagleScout",Level = 4 ,Item="MonsterItemmeritbadge"},
            new MonsterModel(){Key="MonsterBugbear",Level = 3 ,Item="MonsterItemskin"},
            new MonsterModel(){Key="MonsterBugboar",Level = 3 ,Item="MonsterItemtusk"},
            new MonsterModel(){Key="MonsterBoogie",Level = 3 ,Item="MonsterItemslime"},
            new MonsterModel(){Key="MonsterCamel",Level = 2 ,Item="MonsterItemhump"},
            new MonsterModel(){Key="MonsterCarrionCrawler",Level = 3 ,Item="MonsterItemegg"},
            new MonsterModel(){Key="MonsterCatoblepas",Level = 6 ,Item="MonsterItemneck"},
            new MonsterModel(){Key="MonsterCentaur",Level = 4 ,Item="MonsterItemrib"},
            new MonsterModel(){Key="MonsterCentipede",Level = 0 ,Item="MonsterItemleg"},
            new MonsterModel(){Key="MonsterCockatrice",Level = 5 ,Item="MonsterItemwattle"},
            new MonsterModel(){Key="MonsterCouatl",Level = 9 ,Item="MonsterItemwing"},
            new MonsterModel(){Key="MonsterCrayfish",Level = 0 ,Item="MonsterItemantenna"},
            new MonsterModel(){Key="MonsterDemogorgon",Level = 53 ,Item="MonsterItemtentacle"},
            new MonsterModel(){Key="MonsterJubilex",Level = 17 ,Item="MonsterItemgel"},
            new MonsterModel(){Key="MonsterManes",Level = 1 ,Item="MonsterItemtooth"},
            new MonsterModel(){Key="MonsterOrcus",Level = 27 ,Item="MonsterItemwand"},
            new MonsterModel(){Key="MonsterSuccubus",Level = 6 ,Item="MonsterItembra"},
            new MonsterModel(){Key="MonsterVrock",Level = 8 ,Item="MonsterItemneck"},
            new MonsterModel(){Key="MonsterHezrou",Level = 9 ,Item="MonsterItemleg"},
            new MonsterModel(){Key="MonsterGlabrezu",Level = 10 ,Item="MonsterItemcollar"},
            new MonsterModel(){Key="MonsterNalfeshnee",Level = 11 ,Item="MonsterItemtusk"},
            new MonsterModel(){Key="MonsterMarilith",Level = 7 ,Item="MonsterItemarm"},
            new MonsterModel(){Key="MonsterBalor",Level = 8 ,Item="MonsterItemwhip"},
            new MonsterModel(){Key="MonsterYeenoghu",Level = 25 ,Item="MonsterItemflail"},
            new MonsterModel(){Key="MonsterAsmodeus",Level = 52 ,Item="MonsterItemleathers"},
            new MonsterModel(){Key="MonsterBaalzebul",Level = 43 ,Item="MonsterItempants"},
            new MonsterModel(){Key="MonsterBarbedDevil",Level = 8 ,Item="MonsterItemflame"},
            new MonsterModel(){Key="MonsterBoneDevil",Level = 9 ,Item="MonsterItemhook"},
            new MonsterModel(){Key="MonsterDispater",Level = 30 ,Item="MonsterItemmatches"},
            new MonsterModel(){Key="MonsterErinyes",Level = 6 ,Item="MonsterItemthong"},
            new MonsterModel(){Key="MonsterGeryon",Level = 30 ,Item="MonsterItemcornucopia"},
            new MonsterModel(){Key="MonsterMalebranche",Level = 5 ,Item="MonsterItemfork"},
            new MonsterModel(){Key="MonsterIceDevil",Level = 11 ,Item="MonsterItemsnow"},
            new MonsterModel(){Key="MonsterLemure",Level = 3 ,Item="MonsterItemblob"},
            new MonsterModel(){Key="MonsterPitFiend",Level = 13 ,Item="MonsterItemseed"},
            new MonsterModel(){Key="MonsterAnkylosaurus",Level = 9 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterBrontosaurus",Level = 30 ,Item="MonsterItembrain"},
            new MonsterModel(){Key="MonsterDiplodocus",Level = 24 ,Item="MonsterItemfin"},
            new MonsterModel(){Key="MonsterElasmosaurus",Level = 15 ,Item="MonsterItemneck"},
            new MonsterModel(){Key="MonsterGorgosaurus",Level = 13 ,Item="MonsterItemarm"},
            new MonsterModel(){Key="MonsterIguanadon",Level = 6 ,Item="MonsterItemthumb"},
            new MonsterModel(){Key="MonsterMegalosaurus",Level = 12 ,Item="MonsterItemjaw"},
            new MonsterModel(){Key="MonsterMonoclonius",Level = 8 ,Item="MonsterItemhorn"},
            new MonsterModel(){Key="MonsterPentasaurus",Level = 12 ,Item="MonsterItemhead"},
            new MonsterModel(){Key="MonsterStegosaurus",Level = 18 ,Item="MonsterItemplate"},
            new MonsterModel(){Key="MonsterTriceratops",Level = 16 ,Item="MonsterItemhorn"},
            new MonsterModel(){Key="MonsterTyranosaurausRex",Level = 18 ,Item="MonsterItemforearm"},
            new MonsterModel(){Key="MonsterDjinn",Level = 7 ,Item="MonsterItemlamp"},
            new MonsterModel(){Key="MonsterDoppleganger",Level = 4 ,Item="MonsterItemface"},
            new MonsterModel(){Key="MonsterBlackDragon",Level = 7 ,Item=""},
            new MonsterModel(){Key="MonsterPlaidDragon",Level = 7 ,Item="MonsterItemsporrin"},
            new MonsterModel(){Key="MonsterBlueDragon",Level = 9 ,Item=""},
            new MonsterModel(){Key="MonsterBeigeDragon",Level = 9 ,Item=""},
            new MonsterModel(){Key="MonsterBrassDragon",Level = 7 ,Item="MonsterItempole"},
            new MonsterModel(){Key="MonsterTinDragon",Level = 8 ,Item=""},
            new MonsterModel(){Key="MonsterBronzeDragon",Level = 9 ,Item="MonsterItemmedal"},
            new MonsterModel(){Key="MonsterChromaticDragon",Level = 16 ,Item="MonsterItemscale"},
            new MonsterModel(){Key="MonsterCopperDragon",Level = 8 ,Item="MonsterItemloafer"},
            new MonsterModel(){Key="MonsterGoldDragon",Level = 8 ,Item="MonsterItemfilling"},
            new MonsterModel(){Key="MonsterGreenDragon",Level = 8 ,Item=""},
            new MonsterModel(){Key="MonsterPlatinumDragon",Level = 21 ,Item=""},
            new MonsterModel(){Key="MonsterRedDragon",Level = 10 ,Item="MonsterItemcocktail"},
            new MonsterModel(){Key="MonsterSilverDragon",Level = 10 ,Item=""},
            new MonsterModel(){Key="MonsterWhiteDragon",Level = 6 ,Item="MonsterItemtooth"},
            new MonsterModel(){Key="MonsterDragonTurtle",Level = 13 ,Item="MonsterItemshell"},
            new MonsterModel(){Key="MonsterDryad",Level = 2 ,Item="MonsterItemacorn"},
            new MonsterModel(){Key="MonsterDwarf",Level = 1 ,Item="MonsterItemdrawers"},
            new MonsterModel(){Key="MonsterEel",Level = 2 ,Item="MonsterItemsashimi"},
            new MonsterModel(){Key="MonsterEfreet",Level = 10 ,Item="MonsterItemcinder"},
            new MonsterModel(){Key="MonsterSandElemental",Level = 8 ,Item="MonsterItemglass"},
            new MonsterModel(){Key="MonsterBaconElemental",Level = 10 ,Item="MonsterItembit"},
            new MonsterModel(){Key="MonsterPornElemental",Level = 12 ,Item="MonsterItemlube"},
            new MonsterModel(){Key="MonsterCheeseElemental",Level = 14 ,Item="MonsterItemcurd"},
            new MonsterModel(){Key="MonsterHairElemental",Level = 16 ,Item="MonsterItemfollicle"},
            new MonsterModel(){Key="MonsterSwampElf",Level = 1 ,Item="MonsterItemlilypad"},
            new MonsterModel(){Key="MonsterBrownElf",Level = 1 ,Item="MonsterItemtusk"},
            new MonsterModel(){Key="MonsterSeaElf",Level = 1 ,Item="MonsterItemjerkin"},
            new MonsterModel(){Key="MonsterEttin",Level = 10 ,Item="MonsterItemfur"},
            new MonsterModel(){Key="MonsterFrog",Level = 0 ,Item="MonsterItemleg"},
            new MonsterModel(){Key="MonsterVioletFungi",Level = 3 ,Item="MonsterItemspore"},
            new MonsterModel(){Key="MonsterGargoyle",Level = 4 ,Item="MonsterItemgravel"},
            new MonsterModel(){Key="MonsterGelatinousCube",Level = 4 ,Item="MonsterItemjam"},
            new MonsterModel(){Key="MonsterGhast",Level = 4 ,Item="MonsterItemvomit"},
            new MonsterModel(){Key="MonsterGhost",Level = 10 ,Item=""},
            new MonsterModel(){Key="MonsterGhoul",Level = 2 ,Item="MonsterItemmuscle"},
            new MonsterModel(){Key="MonsterHumidityGiant",Level = 12 ,Item="MonsterItemdrops"},
            new MonsterModel(){Key="MonsterBeefGiant",Level = 11 ,Item="MonsterItemsteak"},
            new MonsterModel(){Key="MonsterQuartzGiant",Level = 10 ,Item="MonsterItemcrystal"},
            new MonsterModel(){Key="MonsterPorcelainGiant",Level = 9 ,Item="MonsterItemfixture"},
            new MonsterModel(){Key="MonsterRiceGiant",Level = 8 ,Item="MonsterItemgrain"},
            new MonsterModel(){Key="MonsterCloudGiant",Level = 12 ,Item="MonsterItemcondensation"},
            new MonsterModel(){Key="MonsterFireGiant",Level = 11 ,Item="MonsterItemcigarettes"},
            new MonsterModel(){Key="MonsterFrostGiant",Level = 10 ,Item="MonsterItemsnowman"},
            new MonsterModel(){Key="MonsterHillGiant",Level = 8 ,Item="MonsterItemcorpse"},
            new MonsterModel(){Key="MonsterStoneGiant",Level = 9 ,Item="MonsterItemhatchling"},
            new MonsterModel(){Key="MonsterStormGiant",Level = 15 ,Item="MonsterItembarometer"},
            new MonsterModel(){Key="MonsterMiniGiant",Level = 4 ,Item="MonsterItempompadour"},
            new MonsterModel(){Key="MonsterGnoll",Level = 2 ,Item="MonsterItemcollar"},
            new MonsterModel(){Key="MonsterGnome",Level = 1 ,Item="MonsterItemhat"},
            new MonsterModel(){Key="MonsterGoblin",Level = 1 ,Item="MonsterItemear"},
            new MonsterModel(){Key="MonsterGridBug",Level = 1 ,Item="MonsterItemcarapace"},
            new MonsterModel(){Key="MonsterJellyrock",Level = 9 ,Item="MonsterItemseedling"},
            new MonsterModel(){Key="MonsterBeerGolem",Level = 15 ,Item="MonsterItemfoam"},
            new MonsterModel(){Key="MonsterOxygenGolem",Level = 17 ,Item="MonsterItemplatelet"},
            new MonsterModel(){Key="MonsterCardboardGolem",Level = 14 ,Item="MonsterItemrecycling"},
            new MonsterModel(){Key="MonsterRubberGolem",Level = 16 ,Item="MonsterItemball"},
            new MonsterModel(){Key="MonsterLeatherGolem",Level = 15 ,Item="MonsterItemfob"},
            new MonsterModel(){Key="MonsterGorgon",Level = 8 ,Item="MonsterItemtesticle"},
            new MonsterModel(){Key="MonsterGrayOoze",Level = 3 ,Item="MonsterItemgravy"},
            new MonsterModel(){Key="MonsterGreenSlime",Level = 2 ,Item="MonsterItemsample"},
            new MonsterModel(){Key="MonsterGriffon",Level = 7 ,Item="MonsterItemnest"},
            new MonsterModel(){Key="MonsterBanshee",Level = 7 ,Item="MonsterItemlarynx"},
            new MonsterModel(){Key="MonsterHarpy",Level = 3 ,Item="MonsterItemmascara"},
            new MonsterModel(){Key="MonsterHellHound",Level = 5 ,Item="MonsterItemtongue"},
            new MonsterModel(){Key="MonsterHippocampus",Level = 4 ,Item="MonsterItemmane"},
            new MonsterModel(){Key="MonsterHippogriff",Level = 3 ,Item="MonsterItemegg"},
            new MonsterModel(){Key="MonsterHobgoblin",Level = 1 ,Item="MonsterItempatella"},
            new MonsterModel(){Key="MonsterHomonculus",Level = 2 ,Item="MonsterItemfluid"},
            new MonsterModel(){Key="MonsterHydra",Level = 8 ,Item="MonsterItemgyrum"},
            new MonsterModel(){Key="MonsterImp",Level = 2 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterInvisibleStalker",Level = 8 ,Item=""},
            new MonsterModel(){Key="MonsterIronPeasant",Level = 3 ,Item="MonsterItemchaff"},
            new MonsterModel(){Key="MonsterJumpskin",Level = 3 ,Item="MonsterItemshin"},
            new MonsterModel(){Key="MonsterKobold",Level = 1 ,Item="MonsterItempenis"},
            new MonsterModel(){Key="MonsterLeprechaun",Level = 1 ,Item="MonsterItemwallet"},
            new MonsterModel(){Key="MonsterLeucrotta",Level = 6 ,Item="MonsterItemhoof"},
            new MonsterModel(){Key="MonsterLich",Level = 11 ,Item="MonsterItemcrown"},
            new MonsterModel(){Key="MonsterLizardMan",Level = 2 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterLurker",Level = 10 ,Item="MonsterItemsac"},
            new MonsterModel(){Key="MonsterManticore",Level = 6 ,Item="MonsterItemspike"},
            new MonsterModel(){Key="MonsterMastodon",Level = 12 ,Item="MonsterItemtusk"},
            new MonsterModel(){Key="MonsterMedusa",Level = 6 ,Item="MonsterItemeye"},
            new MonsterModel(){Key="MonsterMulticell",Level = 2 ,Item="MonsterItemdendrite"},
            new MonsterModel(){Key="MonsterPirate",Level = 1 ,Item="MonsterItembooty"},
            new MonsterModel(){Key="MonsterBerserker",Level = 1 ,Item="MonsterItemshirt"},
            new MonsterModel(){Key="MonsterCaveman",Level = 2 ,Item="MonsterItemclub"},
            new MonsterModel(){Key="MonsterDervish",Level = 1 ,Item="MonsterItemrobe"},
            new MonsterModel(){Key="MonsterMerman",Level = 1 ,Item="MonsterItemtrident"},
            new MonsterModel(){Key="MonsterMermaid",Level = 1 ,Item="MonsterItemgills"},
            new MonsterModel(){Key="MonsterMimic",Level = 9 ,Item="MonsterItemhinge"},
            new MonsterModel(){Key="MonsterMindFlayer",Level = 8 ,Item="MonsterItemtentacle"},
            new MonsterModel(){Key="MonsterMinotaur",Level = 6 ,Item="MonsterItemmap"},
            new MonsterModel(){Key="MonsterYellowMold",Level = 1 ,Item="MonsterItemspore"},
            new MonsterModel(){Key="MonsterMorkoth",Level = 7 ,Item="MonsterItemteeth"},
            new MonsterModel(){Key="MonsterMummy",Level = 6 ,Item="MonsterItemgauze"},
            new MonsterModel(){Key="MonsterNaga",Level = 9 ,Item="MonsterItemrattle"},
            new MonsterModel(){Key="MonsterNebbish",Level = 1 ,Item="MonsterItembelly"},
            new MonsterModel(){Key="MonsterNeo-Otyugh",Level = 11 ,Item="MonsterItemorgan"},
            new MonsterModel(){Key="MonsterNixie",Level = 1 ,Item="MonsterItemwebbing"},
            new MonsterModel(){Key="MonsterNymph",Level = 3 ,Item="MonsterItemhanky"},
            new MonsterModel(){Key="MonsterOchreJelly",Level = 6 ,Item="MonsterItemnucleus"},
            new MonsterModel(){Key="MonsterOctopus",Level = 2 ,Item="MonsterItembeak"},
            new MonsterModel(){Key="MonsterOgre",Level = 4 ,Item="MonsterItemtalon"},
            new MonsterModel(){Key="MonsterOgreMage",Level = 5 ,Item="MonsterItemapparel"},
            new MonsterModel(){Key="MonsterOrc",Level = 1 ,Item="MonsterItemsnout"},
            new MonsterModel(){Key="MonsterOtyugh",Level = 7 ,Item="MonsterItemorgan"},
            new MonsterModel(){Key="MonsterOwlbear",Level = 5 ,Item="MonsterItemfeather"},
            new MonsterModel(){Key="MonsterPegasus",Level = 4 ,Item="MonsterItemaileron"},
            new MonsterModel(){Key="MonsterPeryton",Level = 4 ,Item="MonsterItemantler"},
            new MonsterModel(){Key="MonsterPiercer",Level = 3 ,Item="MonsterItemtip"},
            new MonsterModel(){Key="MonsterPixie",Level = 1 ,Item="MonsterItemdust"},
            new MonsterModel(){Key="MonsterManOWar",Level = 3 ,Item="MonsterItemtentacle"},
            new MonsterModel(){Key="MonsterPurpleWorm",Level = 15 ,Item="MonsterItemdung"},
            new MonsterModel(){Key="MonsterQuasit",Level = 3 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterRakshasa",Level = 7 ,Item="MonsterItempajamas"},
            new MonsterModel(){Key="MonsterRat",Level = 0 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterRemorhaz",Level = 11 ,Item="MonsterItemprotrusion"},
            new MonsterModel(){Key="MonsterRoc",Level = 18 ,Item="MonsterItemwing"},
            new MonsterModel(){Key="MonsterRoper",Level = 11 ,Item="MonsterItemtwine"},
            new MonsterModel(){Key="MonsterRotGrub",Level = 1 ,Item="MonsterItemeggsac"},
            new MonsterModel(){Key="MonsterRustMonster",Level = 5 ,Item="MonsterItemshavings"},
            new MonsterModel(){Key="MonsterSatyr",Level = 5 ,Item="MonsterItemhoof"},
            new MonsterModel(){Key="MonsterSeaHag",Level = 3 ,Item="MonsterItemwart"},
            new MonsterModel(){Key="MonsterSilkie",Level = 3 ,Item="MonsterItemfur"},
            new MonsterModel(){Key="MonsterShadow",Level = 3 ,Item="MonsterItemsilhouette"},
            new MonsterModel(){Key="MonsterShamblingMound",Level = 10 ,Item="MonsterItemmulch"},
            new MonsterModel(){Key="MonsterShedu",Level = 9 ,Item="MonsterItemhoof"},
            new MonsterModel(){Key="MonsterShrieker",Level = 3 ,Item="MonsterItemstalk"},
            new MonsterModel(){Key="MonsterSkeleton",Level = 1 ,Item="MonsterItemclavicle"},
            new MonsterModel(){Key="MonsterSpectre",Level = 7 ,Item="MonsterItemvestige"},
            new MonsterModel(){Key="MonsterSphinx",Level = 10 ,Item="MonsterItempaw"},
            new MonsterModel(){Key="MonsterSpider",Level = 0 ,Item="MonsterItemweb"},
            new MonsterModel(){Key="MonsterSprite",Level = 1 ,Item="MonsterItemcan"},
            new MonsterModel(){Key="MonsterStirge",Level = 1 ,Item="MonsterItemproboscis"},
            new MonsterModel(){Key="MonsterStunBear",Level = 5 ,Item="MonsterItemtooth"},
            new MonsterModel(){Key="MonsterStunWorm",Level = 2 ,Item="MonsterItemtrode"},
            new MonsterModel(){Key="MonsterSu-monster",Level = 5 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterSylph",Level = 3 ,Item="MonsterItemthigh"},
            new MonsterModel(){Key="MonsterTitan",Level = 20 ,Item="MonsterItemsandal"},
            new MonsterModel(){Key="MonsterTrapper",Level = 12 ,Item="MonsterItemshag"},
            new MonsterModel(){Key="MonsterTreant",Level = 10 ,Item="MonsterItemacorn"},
            new MonsterModel(){Key="MonsterTriton",Level = 3 ,Item="MonsterItemscale"},
            new MonsterModel(){Key="MonsterTroglodyte",Level = 2 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterTroll",Level = 6 ,Item="MonsterItemhide"},
            new MonsterModel(){Key="MonsterUmberHulk",Level = 8 ,Item="MonsterItemclaw"},
            new MonsterModel(){Key="MonsterUnicorn",Level = 4 ,Item="MonsterItemblood"},
            new MonsterModel(){Key="MonsterVampire",Level = 8 ,Item="MonsterItempancreas"},
            new MonsterModel(){Key="MonsterWight",Level = 4 ,Item="MonsterItemlung"},
            new MonsterModel(){Key="MonsterWillOTheWisp",Level = 9 ,Item="MonsterItemwisp"},
            new MonsterModel(){Key="MonsterWraith",Level = 5 ,Item="MonsterItemfinger"},
            new MonsterModel(){Key="MonsterWyvern",Level = 7 ,Item="MonsterItemwing"},
            new MonsterModel(){Key="MonsterXorn",Level = 7 ,Item="MonsterItemjaw"},
            new MonsterModel(){Key="MonsterYeti",Level = 4 ,Item="MonsterItemfur"},
            new MonsterModel(){Key="MonsterZombie",Level = 2 ,Item="MonsterItemforehead"},
            new MonsterModel(){Key="MonsterWasp",Level = 0 ,Item="MonsterItemstinger"},
            new MonsterModel(){Key="MonsterRat",Level = 1 ,Item="MonsterItemtail"},
            new MonsterModel(){Key="MonsterBunny",Level = 0 ,Item="MonsterItemear"},
            new MonsterModel(){Key="MonsterMoth",Level = 0 ,Item="MonsterItemdust"},
            new MonsterModel(){Key="MonsterBeagle",Level = 0 ,Item="MonsterItemcollar"},
            new MonsterModel(){Key="MonsterMidge",Level = 0 ,Item="MonsterItemcorpse"},
            new MonsterModel(){Key="MonsterOstrich",Level = 1 ,Item="MonsterItembeak"},
            new MonsterModel(){Key="MonsterBillyGoat",Level = 1 ,Item="MonsterItembeard"},
            new MonsterModel(){Key="MonsterBat",Level = 1 ,Item="MonsterItemwing"},
            new MonsterModel(){Key="MonsterKoala",Level = 2 ,Item="MonsterItemheart"},
            new MonsterModel(){Key="MonsterWolf",Level = 2 ,Item="MonsterItempaw"},
            new MonsterModel(){Key="MonsterWhippet",Level = 2 ,Item="MonsterItemcollar"},
            new MonsterModel(){Key="MonsterUruk",Level = 2 ,Item="MonsterItemboot"},
            new MonsterModel(){Key="MonsterPoroid",Level = 4 ,Item="MonsterItemnode"},
            new MonsterModel(){Key="MonsterMoakum",Level = 8 ,Item="MonsterItemfrenum"},
            new MonsterModel(){Key="MonsterFly",Level = 0 ,Item=""},
            new MonsterModel(){Key="MonsterHogbird",Level = 3 ,Item="MonsterItemcurl"},
            new MonsterModel(){Key="MonsterWolog",Level = 4 ,Item="MonsterItemlemma"},
        };
        #endregion

        #region 标题(Titles)
        /// <summary>
        /// 标题(Titles)
        /// </summary>
        public static ObservableCollection<TitleModel> Titles = new ObservableCollection<TitleModel>()
        {
            new TitleModel(){Key="TitleMr"},
            new TitleModel(){Key="TitleMrs"},
            new TitleModel(){Key="TitleSir"},
            new TitleModel(){Key="TitleSgt"},
            new TitleModel(){Key="TitleMs"},
            new TitleModel(){Key="TitleCaptain"},
            new TitleModel(){Key="TitleChief"},
            new TitleModel(){Key="TitleAdmiral"},
            new TitleModel(){Key="TitleSaint"},
        };
        #endregion

        #region 令人印象深刻的标题(ImpressiveTitles)
        /// <summary>
        /// 令人印象深刻的标题(ImpressiveTitles)
        /// </summary>
        public static ObservableCollection<ImpressiveTitleModel> ImpressiveTitles = new ObservableCollection<ImpressiveTitleModel>()
        {
            new ImpressiveTitleModel(){Key="TitleKing"},
            new ImpressiveTitleModel(){Key="TitleQueen"},
            new ImpressiveTitleModel(){Key="TitleLord"},
            new ImpressiveTitleModel(){Key="TitleLady"},
            new ImpressiveTitleModel(){Key="TitleViceroy"},
            new ImpressiveTitleModel(){Key="TitleMayor"},
            new ImpressiveTitleModel(){Key="TitlePrince"},
            new ImpressiveTitleModel(){Key="TitlePrincess"},
            new ImpressiveTitleModel(){Key="TitleChief"},
            new ImpressiveTitleModel(){Key="TitleBoss"},
            new ImpressiveTitleModel(){Key="TitleArchbishop"},
        };
        #endregion
    }
}
