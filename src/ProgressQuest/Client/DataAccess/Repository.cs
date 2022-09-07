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
        public static ObservableCollection<BaseModel> Spells = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="SpellSlimeFinger"},
            new BaseModel(){Key="SpellRabbitPunch"},
            new BaseModel(){Key="SpellHastiness"},
            new BaseModel(){Key="SpellGoodMove"},
            new BaseModel(){Key="SpellSadness"},
            new BaseModel(){Key="SpellSeasick"},
            new BaseModel(){Key="SpellShoelaces"},
            new BaseModel(){Key="SpellInnoculate"},
            new BaseModel(){Key="SpellConeOfAnnoyance"},
            new BaseModel(){Key="SpellMagneticOrb"},
            new BaseModel(){Key="SpellInvisibleHands"},
            new BaseModel(){Key="SpellRevoltingCloud"},
            new BaseModel(){Key="SpellAqueousHumor"},
            new BaseModel(){Key="SpellSpectralMiasma"},
            new BaseModel(){Key="SpellCleverFellow"},
            new BaseModel(){Key="SpellLockjaw"},
            new BaseModel(){Key="SpellHistoryLesson"},
            new BaseModel(){Key="SpellHydrophobia"},
            new BaseModel(){Key="SpellBigSister"},
            new BaseModel(){Key="SpellConeOfPaste"},
            new BaseModel(){Key="SpellMulligan"},
            new BaseModel(){Key="SpellNestorsBrightIdea"},
            new BaseModel(){Key="SpellHolyBatpole"},
            new BaseModel(){Key="SpellTumorBenign"},
            new BaseModel(){Key="SpellBraingate"},
            new BaseModel(){Key="SpellSummonABitch"},
            new BaseModel(){Key="SpellNonplus"},
            new BaseModel(){Key="SpellAnimateNightstand"},
            new BaseModel(){Key="SpellEyeOfTheTroglodyte"},
            new BaseModel(){Key="SpellCurseName"},
            new BaseModel(){Key="SpellDropsy"},
            new BaseModel(){Key="SpellVitreousHumor"},
            new BaseModel(){Key="SpellRogersGrandIllusion"},
            new BaseModel(){Key="SpellCovet"},
            new BaseModel(){Key="SpellBlackIdaho"},
            new BaseModel(){Key="SpellAstralMiasma"},
            new BaseModel(){Key="SpellSpectralOyster"},
            new BaseModel(){Key="SpellAcridHands"},
            new BaseModel(){Key="SpellAngioplasty"},
            new BaseModel(){Key="SpellGrognorsBigDayOff"},
            new BaseModel(){Key="SpellTumorMalignant"},
            new BaseModel(){Key="SpellAnimateTunic"},
            new BaseModel(){Key="SpellUrsineArmor"},
            new BaseModel(){Key="SpellHolyRoller"},
            new BaseModel(){Key="SpellTonsilectomy"},
            new BaseModel(){Key="SpellCurseFamily"},
            new BaseModel(){Key="SpellInfiniteConfusion"},
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
        public static ObservableCollection<BaseModel> Specials = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="SpecialDiadem"},
            new BaseModel(){Key="SpecialFestoon"},
            new BaseModel(){Key="SpecialGemstone"},
            new BaseModel(){Key="SpecialPhial"},
            new BaseModel(){Key="SpecialTiara"},
            new BaseModel(){Key="SpecialScabbard"},
            new BaseModel(){Key="SpecialArrow"},
            new BaseModel(){Key="SpecialLens"},
            new BaseModel(){Key="SpecialLamp"},
            new BaseModel(){Key="SpecialHymnal"},
            new BaseModel(){Key="SpecialFleece"},
            new BaseModel(){Key="SpecialLaurel"},
            new BaseModel(){Key="SpecialBrooch"},
            new BaseModel(){Key="SpecialGimlet"},
            new BaseModel(){Key="SpecialCobble"},
            new BaseModel(){Key="SpecialAlbatross"},
            new BaseModel(){Key="SpecialBrazier"},
            new BaseModel(){Key="SpecialBandolier"},
            new BaseModel(){Key="SpecialTome"},
            new BaseModel(){Key="SpecialGarnet"},
            new BaseModel(){Key="SpecialAmethyst"},
            new BaseModel(){Key="SpecialCandelabra"},
            new BaseModel(){Key="SpecialCorset"},
            new BaseModel(){Key="SpecialSphere"},
            new BaseModel(){Key="SpecialSceptre"},
            new BaseModel(){Key="SpecialAnkh"},
            new BaseModel(){Key="SpecialTalisman"},
            new BaseModel(){Key="SpecialOrb"},
            new BaseModel(){Key="SpecialGammel"},
            new BaseModel(){Key="SpecialOrnament"},
            new BaseModel(){Key="SpecialBrocade"},
            new BaseModel(){Key="SpecialGaloon"},
            new BaseModel(){Key="SpecialBijou"},
            new BaseModel(){Key="SpecialSpangle"},
            new BaseModel(){Key="SpecialGimcrack"},
            new BaseModel(){Key="SpecialHood"},
            new BaseModel(){Key="SpecialVulpeculum"},
        };
        #endregion

        #region 物品特征(ItemAttributes)
        /// <summary>
        /// 物品特征(ItemAttributes)
        /// </summary>
        public static ObservableCollection<BaseModel> ItemAttributes = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="ItemAttributeGolden"},
            new BaseModel(){Key="ItemAttributeGilded"},
            new BaseModel(){Key="ItemAttributeSpectral"},
            new BaseModel(){Key="ItemAttributeAstral"},
            new BaseModel(){Key="ItemAttributeGarlanded"},
            new BaseModel(){Key="ItemAttributePrecious"},
            new BaseModel(){Key="ItemAttributeCrafted"},
            new BaseModel(){Key="ItemAttributeDual"},
            new BaseModel(){Key="ItemAttributeFiligreed"},
            new BaseModel(){Key="ItemAttributeCruciate"},
            new BaseModel(){Key="ItemAttributeArcane"},
            new BaseModel(){Key="ItemAttributeBlessed"},
            new BaseModel(){Key="ItemAttributeReverential"},
            new BaseModel(){Key="ItemAttributeLucky"},
            new BaseModel(){Key="ItemAttributeEnchanted"},
            new BaseModel(){Key="ItemAttributeGleaming"},
            new BaseModel(){Key="ItemAttributeGrandiose"},
            new BaseModel(){Key="ItemAttributeSacred"},
            new BaseModel(){Key="ItemAttributeLegendary"},
            new BaseModel(){Key="ItemAttributeMythic"},
            new BaseModel(){Key="ItemAttributeCrystalline"},
            new BaseModel(){Key="ItemAttributeAustere"},
            new BaseModel(){Key="ItemAttributeOstentatious"},
            new BaseModel(){Key="ItemAttributeOneTrue"},
            new BaseModel(){Key="ItemAttributeProverbial"},
            new BaseModel(){Key="ItemAttributeFearsome"},
            new BaseModel(){Key="ItemAttributeDeadly"},
            new BaseModel(){Key="ItemAttributeBenevolent"},
            new BaseModel(){Key="ItemAttributeUnearthly"},
            new BaseModel(){Key="ItemAttributeMagnificent"},
            new BaseModel(){Key="ItemAttributeIron"},
            new BaseModel(){Key="ItemAttributeOrmolu"},
            new BaseModel(){Key="ItemAttributePuissant"},
        };
        #endregion

        #region 物品(ItemOfs)
        /// <summary>
        /// 物品(ItemOfs)
        /// </summary>
        public static ObservableCollection<BaseModel> ItemOfs = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="ItemOfForeboding"},
            new BaseModel(){Key="ItemOfForeshadowing"},
            new BaseModel(){Key="ItemOfNervousness"},
            new BaseModel(){Key="ItemOfHappiness"},
            new BaseModel(){Key="ItemOfTorpor"},
            new BaseModel(){Key="ItemOfDanger"},
            new BaseModel(){Key="ItemOfCraft"},
            new BaseModel(){Key="ItemOfSilence"},
            new BaseModel(){Key="ItemOfInvisibility"},
            new BaseModel(){Key="ItemOfRapidity"},
            new BaseModel(){Key="ItemOfPleasure"},
            new BaseModel(){Key="ItemOfPracticality"},
            new BaseModel(){Key="ItemOfHurting"},
            new BaseModel(){Key="ItemOfJoy"},
            new BaseModel(){Key="ItemOfPetulance"},
            new BaseModel(){Key="ItemOfIntrusion"},
            new BaseModel(){Key="ItemOfChaos"},
            new BaseModel(){Key="ItemOfSuffering"},
            new BaseModel(){Key="ItemOfExtroversion"},
            new BaseModel(){Key="ItemOfFrenzy"},
            new BaseModel(){Key="ItemOfSisu"},
            new BaseModel(){Key="ItemOfSolitude"},
            new BaseModel(){Key="ItemOfPunctuality"},
            new BaseModel(){Key="ItemOfEfficiency"},
            new BaseModel(){Key="ItemOfComfort"},
            new BaseModel(){Key="ItemOfPatience"},
            new BaseModel(){Key="ItemOfInternment"},
            new BaseModel(){Key="ItemOfIncarceration"},
            new BaseModel(){Key="ItemOfMisapprehension"},
            new BaseModel(){Key="ItemOfLoyalty"},
            new BaseModel(){Key="ItemOfEnvy"},
            new BaseModel(){Key="ItemOfAcrimony"},
            new BaseModel(){Key="ItemOfWorry"},
            new BaseModel(){Key="ItemOfFear"},
            new BaseModel(){Key="ItemOfAwe"},
            new BaseModel(){Key="ItemOfGuile"},
            new BaseModel(){Key="ItemOfPrurience"},
            new BaseModel(){Key="ItemOfFortune"},
            new BaseModel(){Key="ItemOfPerspicacity"},
            new BaseModel(){Key="ItemOfDomination"},
            new BaseModel(){Key="ItemOfSubmission"},
            new BaseModel(){Key="ItemOfFealty"},
            new BaseModel(){Key="ItemOfHunger"},
            new BaseModel(){Key="ItemOfDespair"},
            new BaseModel(){Key="ItemOfCruelty"},
            new BaseModel(){Key="ItemOfGrob"},
            new BaseModel(){Key="ItemOfDignard"},
            new BaseModel(){Key="ItemOfRa"},
            new BaseModel(){Key="ItemOfthe Bone"},
            new BaseModel(){Key="ItemOfDiamonique"},
            new BaseModel(){Key="ItemOfElectrum"},
            new BaseModel(){Key="ItemOfHydragyrum"},
        };
        #endregion

        #region 无聊的物品(BoringItems)
        /// <summary>
        /// 无聊的物品(BoringItems)
        /// </summary>
        public static ObservableCollection<BaseModel> BoringItems = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="BoringItemnail"},
            new BaseModel(){Key="BoringItemlunchpail"},
            new BaseModel(){Key="BoringItemsock"},
            new BaseModel(){Key="BoringItemIOU"},
            new BaseModel(){Key="BoringItemcookie"},
            new BaseModel(){Key="BoringItempint"},
            new BaseModel(){Key="BoringItemtoothpick"},
            new BaseModel(){Key="BoringItemwrit"},
            new BaseModel(){Key="BoringItemnewspaper"},
            new BaseModel(){Key="BoringItemletter"},
            new BaseModel(){Key="BoringItemplank"},
            new BaseModel(){Key="BoringItemhat"},
            new BaseModel(){Key="BoringItemegg"},
            new BaseModel(){Key="BoringItemcoin"},
            new BaseModel(){Key="BoringItemneedle"},
            new BaseModel(){Key="BoringItembucket"},
            new BaseModel(){Key="BoringItemladder"},
            new BaseModel(){Key="BoringItemchicken"},
            new BaseModel(){Key="BoringItemtwig"},
            new BaseModel(){Key="BoringItemdirtclod"},
            new BaseModel(){Key="BoringItemcounterpane"},
            new BaseModel(){Key="BoringItemvest"},
            new BaseModel(){Key="BoringItemteratoma"},
            new BaseModel(){Key="BoringItembunny"},
            new BaseModel(){Key="BoringItemrock"},
            new BaseModel(){Key="BoringItempole"},
            new BaseModel(){Key="BoringItemcarrot"},
            new BaseModel(){Key="BoringItemcanoe"},
            new BaseModel(){Key="BoringIteminkwell"},
            new BaseModel(){Key="BoringItemhoe"},
            new BaseModel(){Key="BoringItembandage"},
            new BaseModel(){Key="BoringItemtrowel"},
            new BaseModel(){Key="BoringItemtowel"},
            new BaseModel(){Key="BoringItemplanterbox"},
            new BaseModel(){Key="BoringItemanvil"},
            new BaseModel(){Key="BoringItemaxle"},
            new BaseModel(){Key="BoringItemtuppence"},
            new BaseModel(){Key="BoringItemcasket"},
            new BaseModel(){Key="BoringItemnosegay"},
            new BaseModel(){Key="BoringItemtrinket"},
            new BaseModel(){Key="BoringItemcredenza"},
            new BaseModel(){Key="BoringItemwrit"},
        };
        #endregion

        #region 生病的前缀(SickPrefixs)
        /// <summary>
        /// 生病的前缀(SickPrefixs)
        /// </summary>
        public static ObservableCollection<BaseModel> SickPrefixs = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="PrefixDead"},
            new BaseModel(){Key="PrefixComatose"},
            new BaseModel(){Key="PrefixCrippled"},
            new BaseModel(){Key="PrefixSick"},
            new BaseModel(){Key="PrefixUndernourished"},
        };
        #endregion

        #region 年轻的前缀(YoungPrefixs)
        /// <summary>
        /// 年轻的前缀(YoungPrefixs)
        /// </summary>
        public static ObservableCollection<BaseModel> YoungPrefixs = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="PrefixFoetal"},
            new BaseModel(){Key="PrefixBaby"},
            new BaseModel(){Key="PrefixPreadolescent"},
            new BaseModel(){Key="PrefixTeenage"},
            new BaseModel(){Key="PrefixUnderage"},
        };
        #endregion

        #region 巨大的前缀(BigPrefixs)
        /// <summary>
        /// 巨大的前缀(BigPrefixs)
        /// </summary>
        public static ObservableCollection<BaseModel> BigPrefixs = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="PrefixGreater"},
            new BaseModel(){Key="PrefixMassive"},
            new BaseModel(){Key="PrefixEnormous"},
            new BaseModel(){Key="PrefixGiant"},
            new BaseModel(){Key="PrefixTitanic"},
        };
        #endregion

        #region 特别的前缀1(SpecialOnePrefixs)
        /// <summary>
        /// 特别的前缀1(SpecialOnePrefixs)
        /// </summary>
        public static ObservableCollection<BaseModel> SpecialOnePrefixs = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="PrefixVeteran"},
            new BaseModel(){Key="PrefixCursed"},
            new BaseModel(){Key="PrefixWarrior"},
            new BaseModel(){Key="PrefixUndead"},
            new BaseModel(){Key="PrefixDemon"},
        };
        #endregion

        #region 特别的前缀2(SpecialTwoPrefixs)
        /// <summary>
        /// 特别的前缀2(SpecialTwoPrefixs)
        /// </summary>
        public static ObservableCollection<BaseModel> SpecialTwoPrefixs = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="PrefixBattle"},
            new BaseModel(){Key="PrefixCursed"},
            new BaseModel(){Key="PrefixWere"},
            new BaseModel(){Key="PrefixUndead"},
            new BaseModel(){Key="PrefixDemon"},
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
        public static ObservableCollection<BaseModel> Titles = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="TitleMr"},
            new BaseModel(){Key="TitleMrs"},
            new BaseModel(){Key="TitleSir"},
            new BaseModel(){Key="TitleSgt"},
            new BaseModel(){Key="TitleMs"},
            new BaseModel(){Key="TitleCaptain"},
            new BaseModel(){Key="TitleChief"},
            new BaseModel(){Key="TitleAdmiral"},
            new BaseModel(){Key="TitleSaint"},
        };
        #endregion

        #region 令人印象深刻的标题(ImpressiveTitles)
        /// <summary>
        /// 令人印象深刻的标题(ImpressiveTitles)
        /// </summary>
        public static ObservableCollection<BaseModel> ImpressiveTitles = new ObservableCollection<BaseModel>()
        {
            new BaseModel(){Key="TitleKing"},
            new BaseModel(){Key="TitleQueen"},
            new BaseModel(){Key="TitleLord"},
            new BaseModel(){Key="TitleLady"},
            new BaseModel(){Key="TitleViceroy"},
            new BaseModel(){Key="TitleMayor"},
            new BaseModel(){Key="TitlePrince"},
            new BaseModel(){Key="TitlePrincess"},
            new BaseModel(){Key="TitleChief"},
            new BaseModel(){Key="TitleBoss"},
            new BaseModel(){Key="TitleArchbishop"},
        };
        #endregion
    }
}
