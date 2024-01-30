using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FRM.UI
{
    /*
     * 该代码为制作原型专用。
     */
    
    #region General
    class PrototypeHelper
    {
        public static List<string> GetCNPorts()
        {
            var ports = new List<string>();
            ports.Add("BANGKOK");
            ports.Add("DALIAN");
            ports.Add("HAIPHONG");
            ports.Add("HOCHIMINH");
            ports.Add("HONGKONG");
            ports.Add("LAEM CHABANG");
            ports.Add("LIANYUNGANG");
            ports.Add("NINGBO");
            ports.Add("PASIR GUDANG");
            ports.Add("PENANG");
            ports.Add("PORT KLANG");
            ports.Add("QINGDAO");
            ports.Add("SHANGHAI");
            ports.Add("XIAMEN");
            ports.Add("XINGANG");
            ports.Add("YANTIAN");
            return ports;
        }
        public static List<string> GetUSAPorts()
        {
            var ports = new List<string>();
            ports.Add("AGOURA HILLS,CA");
            ports.Add("ALHAMBRA,CA");
            ports.Add("ALTADENA,CA");
            ports.Add("ANAHEIM,CA");
            ports.Add("ANTHEM,AZ");
            ports.Add("APACHE JUNCTION,AZ");
            ports.Add("APPLE VALLEY,CA");
            ports.Add("ARCADIA,CA");
            ports.Add("ARLINGTON,CA");
            ports.Add("ARTESIA,CA");
            ports.Add("AZUSA,CA");
            ports.Add("BAKERSFIELD,CA");
            ports.Add("BALDWIN PARK,CA");
            ports.Add("BEL AIR,CA");
            ports.Add("BELL GARDEN,CA");
            ports.Add("BELL,CA");
            ports.Add("BELLFLOWER,CA");
            ports.Add("BEVERLY HILLS,CA");
            ports.Add("BLOOMINGTON,CA");
            ports.Add("BRANDBURY,CA");
            ports.Add("BREA,CA");
            ports.Add("BUENA PARK,CA");
            ports.Add("BURBANK,CA");
            ports.Add("CALABASSAS,CA");
            ports.Add("CALEXICO,CA");
            ports.Add("CALIFORNIA CITY,CA");
            ports.Add("CAMARILLO,CA");
            ports.Add("CANOGA PARK,CA");
            ports.Add("CAPISTRANO BEACH,CA");
            ports.Add("CARLSBAD,CA");
            ports.Add("CARSON,CA");
            ports.Add("CERRITOS,CA");
            ports.Add("CHANDLER,AZ");
            ports.Add("CHARTSWORTH,CA");
            ports.Add("CHINO HILLS,CA");
            ports.Add("CHINO,CA");
            ports.Add("CHULA VISTA,CA");
            ports.Add("CITY OF COMMERCE,CA");
            ports.Add("CITY OF INDUSTRY,CA");
            ports.Add("CLAREMONT,CA");
            ports.Add("CLOVIS,CA");
            ports.Add("COLTON,CA");
            ports.Add("COMPTON,CA");
            ports.Add("CORONA,CA");
            ports.Add("COSTA MESA,CA");
            ports.Add("COVINA,CA");
            ports.Add("CUDAHY,CA");
            ports.Add("CULVER CITY,CA");
            ports.Add("CYPRESS,CA");
            ports.Add("DELANO,CA");
            ports.Add("DELMAR,CA");
            ports.Add("DIAMOND BAR,CA");
            ports.Add("DINUBA,CA");
            ports.Add("DOMINGUEZ,CA");
            ports.Add("DOWNEY,CA");
            ports.Add("DUARTE,CA");
            ports.Add("EAGLE ROCK,CA");
            ports.Add("EARLIMART,CA");
            ports.Add("EAST LOS ANGELES,CA");
            ports.Add("EL CENTRO,CA");
            ports.Add("EL MIRAGE,AZ");
            ports.Add("EL MONTE,CA");
            ports.Add("EL SEGUNDO,CA");
            ports.Add("EL TORO,CA");
            ports.Add("ENCINITAS,CA");
            ports.Add("ENCINO,CA");
            ports.Add("ESCONDIDO,CA");
            ports.Add("EXETER,CA");
            ports.Add("FALLBROOK,CA");
            ports.Add("FIRESTONE PARK,CA");
            ports.Add("FLINTRIDGE,CA");
            ports.Add("FLORENCE,CA");
            ports.Add("FONTANA,CA");
            ports.Add("FOOTHILL RANCH,CA");
            ports.Add("FOUNTAIN VALLEY,CA");
            ports.Add("FRESNO,CA");
            ports.Add("FULLERTON,CA");
            ports.Add("GARDEN GROVE,CA");
            ports.Add("GARDENA,CA");
            ports.Add("GILBERT,AZ");
            ports.Add("GLENDALE,AZ");
            ports.Add("GLENDALE,CA");
            ports.Add("GLENDORA,CA");
            ports.Add("GOLETA,CA");
            ports.Add("GOODYEAR,AZ");
            ports.Add("GRANADA HILLS,CA");
            ports.Add("GREEN VALLEY,CA");
            ports.Add("HACIENDA HEIGHTS,CA");
            ports.Add("HARBOR CITY,CA");
            ports.Add("HAWAIIAN GARDENS,CA");
            ports.Add("HAWTHORNE,CA");
            ports.Add("HAYWARD,CA");
            ports.Add("HENDERSON,NV");
            ports.Add("HERMOSA BEACH,CA");
            ports.Add("HIGHLAND PARK,CA");
            ports.Add("HIGHLAND,CA");
            ports.Add("HOLLYWOOD,CA");
            ports.Add("HUENEME,CA");
            ports.Add("HUNTINGTON BEACH,CA");
            ports.Add("HUNTINGTON PARK,CA");
            ports.Add("INGLEWOOD,CA");
            ports.Add("IRVINE,CA");
            ports.Add("IRWINDALE,CA");
            ports.Add("LA CANADA,CA");
            ports.Add("LA CRESCENTA,CA");
            ports.Add("LA HABRA,CA");
            ports.Add("LA MIRADA,CA");
            ports.Add("LA PALMA,CA");
            ports.Add("LA PUENTE,CA");
            ports.Add("LA VERNE,CA");
            ports.Add("LAGUNA BEACH,CA");
            ports.Add("LAGUNA HILLS,CA");
            ports.Add("LAKE FOREST,CA");
            ports.Add("LAKEWOOD,CA");
            ports.Add("LAS VEGAS,NV");
            ports.Add("LAWNDALE,CA");
            ports.Add("LENNOX,CA");
            ports.Add("LINDSAY,CA");
            ports.Add("LOMA LINDA,CA");
            ports.Add("LOMITA,CA");
            ports.Add("LOMPOC,CA");
            ports.Add("LONG BEACH,CA");
            ports.Add("LOS ALAMITOS,CA");
            ports.Add("LOS ANGELES,CA");
            ports.Add("LYNWOOD,CA");
            ports.Add("MALIBU,CA");
            ports.Add("MANHATTAN BEACH,CA");
            ports.Add("MARGARITA,CA");
            ports.Add("MARICOPA,AZ");
            ports.Add("MAYWOOD,CA");
            ports.Add("MENTONE,CA");
            ports.Add("MERCED,CA");
            ports.Add("MESA,AZ");
            ports.Add("MESQUITE,NV");
            ports.Add("MIRA LOMA,CA");
            ports.Add("MIRADA,CA");
            ports.Add("MODESTO,CA");
            ports.Add("MOJAVE,CA");
            ports.Add("MONROVIA,CA");
            ports.Add("MONTCLAIR,CA");
            ports.Add("MONTEBELLO,CA");
            ports.Add("MONTEREY PARK,CA");
            ports.Add("MONTROSE,CA");
            ports.Add("MOORPARK,CA");
            ports.Add("MORENO VALLEY,CA");
            ports.Add("MURRIETA,CA");
            ports.Add("NATIONAL CITY,CA");
            ports.Add("NEWBURY PARK,CA");
            ports.Add("NEWPORT BEACH,CA");
            ports.Add("NILAND,CA");
            ports.Add("NOGALES,AZ");
            ports.Add("NORTH HOLLYWOOD,CA");
            ports.Add("NORTH LAS VEGAS,NV");
            ports.Add("NORTHRIDGE,CA");
            ports.Add("NORWALK,CA");
            ports.Add("OCEANSIDE,CA");
            ports.Add("ONTARIO,CA");
            ports.Add("ORANGE,CA");
            ports.Add("OTAY MESA,CA");
            ports.Add("OXNARD,CA");
            ports.Add("PACOIMA,CA");
            ports.Add("PALM DESERT,CA");
            ports.Add("PALM SPRINGS,CA");
            ports.Add("PANORAMA CITY,CA");
            ports.Add("PARAMOUNT,CA");
            ports.Add("PASADENA,CA");
            ports.Add("PASO ROBLES,CA");
            ports.Add("PEORIA,AZ");
            ports.Add("PERRIS,CA");
            ports.Add("PHILLIPS RANCH,CA");
            ports.Add("PHOENIX,AZ");
            ports.Add("PICO RIVERA,CA");
            ports.Add("PLACENTIA,CA");
            ports.Add("POMONA,CA");
            ports.Add("PORT HUENEME,CA");
            ports.Add("PORTERVILLE,CA");
            ports.Add("POWAY,CA");
            ports.Add("PRESCOTT,AZ");
            ports.Add("RANCHO CUCAMONGA,CA");
            ports.Add("RANCHO DOMINGUEZ,CA");
            ports.Add("RANCHO MIRAGE,CA");
            ports.Add("REDLANDS,CA");
            ports.Add("REDONDO BEACH,CA");
            ports.Add("RENO,NV");
            ports.Add("RESEDA,CA");
            ports.Add("RIALTO,CA");
            ports.Add("RIVERSIDE,CA");
            ports.Add("ROSEMEAD,CA");
            ports.Add("ROWLAND HEIGHTS,CA");
            ports.Add("SAN BERNARDINO,CA");
            ports.Add("SAN CLEMENTE,CA");
            ports.Add("SAN DIEGO,CA");
            ports.Add("SAN DIMAS,CA");
            ports.Add("SAN FERNANDO,CA");
            ports.Add("SAN FRANCISCO,CA");
            ports.Add("SAN GABRIEL,CA");
            ports.Add("SAN JUAN CAPISTRANO,CA");
            ports.Add("SAN LUIS OBISPO,CA");
            ports.Add("SAN MARCOS,CA");
            ports.Add("SAN MARINO,CA");
            ports.Add("SAN PEDRO,CA");
            ports.Add("SAN YSIDRO,CA");
            ports.Add("SANTA ANA,CA");
            ports.Add("SANTA BARBARA,CA");
            ports.Add("SANTA CLARITA,CA");
            ports.Add("SANTA FE SPRINGS,CA");
            ports.Add("SANTA MARIA,CA");
            ports.Add("SANTA MONICA,CA");
            ports.Add("SCOTTSDALE,AZ");
            ports.Add("SEAL BEACH,CA");
            ports.Add("SEPULVEDA,CA");
            ports.Add("SHAFTER,CA");
            ports.Add("SHERMAN OAKS,CA");
            ports.Add("SIGNAL HILL,CA");
            ports.Add("SILVER LAKE,CA");
            ports.Add("SIMI VALLEY,CA");
            ports.Add("SOUTH EL MONTE,CA");
            ports.Add("SOUTH GATE,CA");
            ports.Add("STANTON,CA");
            ports.Add("STUDIO CITY,CA");
            ports.Add("SUN VALLEY,CA");
            ports.Add("SUNLAND,CA");
            ports.Add("SURPRISE,AZ");
            ports.Add("SYLMAR,CA");
            ports.Add("TARZANA,CA");
            ports.Add("TECATE,CA");
            ports.Add("TEHACHAPI,CA");
            ports.Add("TEMECULA,CA");
            ports.Add("TEMPE,AZ");
            ports.Add("TEMPLE CITY,CA");
            ports.Add("TERRA BELLA,CA");
            ports.Add("THERMAL,CA");
            ports.Add("THOUSAND OAKS,CA");
            ports.Add("TOLLESON,AZ");
            ports.Add("TOPANGA,CA");
            ports.Add("TORRANCE,CA");
            ports.Add("TUCSON,AZ");
            ports.Add("TUJUNGA,CA");
            ports.Add("TULARE,CA");
            ports.Add("TUSTIN,CA");
            ports.Add("UPLAND,CA");
            ports.Add("VALENCIA,CA");
            ports.Add("VALLEY CENTER,CA");
            ports.Add("VAN NUYS,CA");
            ports.Add("VENICE,CA");
            ports.Add("VENTURA,CA");
            ports.Add("VERNON,CA");
            ports.Add("VICTORVILLE,CA");
            ports.Add("VISALIA,CA");
            ports.Add("VISTA,CA");
            ports.Add("WALNUT,CA");
            ports.Add("WASCO,CA");
            ports.Add("WEST COVINA,CA");
            ports.Add("WEST LAKE VILLAGE,CA");
            ports.Add("WEST LAKE,CA");
            ports.Add("WESTCHESTER,CA");
            ports.Add("WESTMINSTER,CA");
            ports.Add("WHITTIER,CA");
            ports.Add("WILMINGTON,CA");
            ports.Add("WOODLAND HILLS,CA");
            ports.Add("YORBA LINDA,CA");
            ports.Add("YUMA,AZ");

            return ports;
        }
    }
    #endregion

    #region For OPRelatedItems
    class Generator
    {
        public static List<PrototypeOceanAdditionalFeeList> GenerateAdditionalFees()
        {
            var additionalFees = new List<PrototypeOceanAdditionalFeeList>();
            additionalFees.Add(new PrototypeOceanAdditionalFeeList()
            {
                IsSpecialFee = true,
                ChargingCode = "燃油附加费",
                CustomerName = "",
                Percent = 0,
                CurrencyName = "USD",
                P20GP = 200,
                P40GP = 300,
                P45HQ = 500
            });

            additionalFees.Add(new PrototypeOceanAdditionalFeeList()
            {
                IsSpecialFee = false,
                ChargingCode = "RSS",
                CustomerName = "",
                Percent = 0,
                CurrencyName = "USD",
                P20GP = 200,
                P40GP = 300,
                P45HQ = 500
            });

            additionalFees.Add(new PrototypeOceanAdditionalFeeList()
            {
                IsSpecialFee = false,
                ChargingCode = "PRO",
                CustomerName = "",
                Percent = 0,
                CurrencyName = "USD",
                P20GP = 200,
                P40GP = 300,
                P45HQ = 500
            });

            return additionalFees;
        }

        public static List<PrototypeOceanFeederList> GenerateOriginFeeders()
        {
            var feeders = new List<PrototypeOceanFeederList>();

            feeders.Add(new PrototypeOceanFeederList()
            {
                Comm = "",
                POLName = "Zhuhai",
                PODName = "Yantian",
                TransportClauseName = "pp",
                Type = "Original",
                IsQuotation = false,
                P20GP = 800,
                P40GP = 900,
                P45HQ = 1100
            });

            feeders.Add(new PrototypeOceanFeederList()
            {
                Comm = "",
                POLName = "Huizhou",
                PODName = "Yantian",
                TransportClauseName = "pp",
                Type = "Original",
                IsQuotation = false,
                P20GP = 800,
                P40GP = 900,
                P45HQ = 1100
            });

            feeders.Add(new PrototypeOceanFeederList()
            {
                Comm = "",
                POLName = "Fuzhou",
                PODName = "Yantian",
                TransportClauseName = "pp",
                Type = "Original",
                IsQuotation = true,
                P20GP = 1800,
                P40GP = 1900,
                P45HQ = 2100
            });
            return feeders;
        }

        public static List<PrototypeOceanFeederList> GenerateDestFeeders()
        {
            var feeders = new List<PrototypeOceanFeederList>();

            feeders.Add(new PrototypeOceanFeederList()
            {
                Comm = "",
                POLName = "Los Angels",
                PODName = "Chicago",
                TransportClauseName = "pp",
                Type = "Destination",
                IsQuotation = false,
                P20GP = 800,
                P40GP = 900,
                P45HQ = 1100
            });

            feeders.Add(new PrototypeOceanFeederList()
            {
                Comm = "",
                POLName = "Los Angels",
                PODName = "Denver",
                TransportClauseName = "pp",
                Type = "Destination",
                IsQuotation = false,
                P20GP = 800,
                P40GP = 900,
                P45HQ = 1100
            });

            feeders.Add(new PrototypeOceanFeederList()
            {
                Comm = "",
                POLName = "Los Angels",
                PODName = "Philadelphia",
                TransportClauseName = "pp",
                Type = "Destination",
                IsQuotation = true,
                P20GP = 1800,
                P40GP = 1900,
                P45HQ = 2100
            });
            return feeders;
        }
    }

    ////[XmlType]
    [Serializable]
    public class PrototypeOceanFeederList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }



        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }



        Guid _oceanid;
        /// <summary>
        /// 海运运价ID
        /// </summary>
        public Guid OceanID
        {
            get
            {
                return _oceanid;
            }
            set
            {
                if (_oceanid != value)
                {
                    _oceanid = value;
                    base.OnPropertyChanged("OceanID", value);
                }
            }
        }


        Guid _polid;
        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid POLID
        {
            get
            {
                return _polid;
            }
            set
            {
                if (_polid != value)
                {
                    _polid = value;
                    base.OnPropertyChanged("POLID", value);
                }
            }
        }


        string _polname;
        /// <summary>
        /// 装货港名称
        /// </summary>
        public string POLName
        {
            get
            {
                return _polname;
            }
            set
            {
                if (_polname != value)
                {
                    _polname = value;
                    base.OnPropertyChanged("POLName", value);
                }
            }
        }

        string _polcode;
        /// <summary>
        /// 装货港代码
        /// </summary>
        public string POLCode
        {
            get
            {
                return _polcode;
            }
            set
            {
                if (_polcode != value)
                {
                    _polcode = value;
                    base.OnPropertyChanged("POLCode", value);
                }
            }
        }


        Guid _podid;
        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid PODID
        {
            get
            {
                return _podid;
            }
            set
            {
                if (_podid != value)
                {
                    _podid = value;
                    base.OnPropertyChanged("PODID", value);
                }
            }
        }


        string _podname;
        /// <summary>
        /// 卸货港名称
        /// </summary>
        public string PODName
        {
            get
            {
                return _podname;
            }
            set
            {
                if (_podname != value)
                {
                    _podname = value;
                    base.OnPropertyChanged("PODName", value);
                }
            }
        }

        string _podcode;
        /// <summary>
        /// 卸货港代码
        /// </summary>
        public string PODCode
        {
            get
            {
                return _podcode;
            }
            set
            {
                if (_podcode != value)
                {
                    _podcode = value;
                    base.OnPropertyChanged("PODCode", value);
                }
            }
        }

        string _type;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get
            {
                if (_type == null) return string.Empty;
                else return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    base.OnPropertyChanged("Type", value);
                }
            }
        }

        bool? _isQuotation;
        /// <summary>
        /// 类型
        /// </summary>
        public bool? IsQuotation
        {
            get
            {
                if (_isQuotation == null) return false;
                else return _isQuotation;
            }
            set
            {
                if (_isQuotation != value)
                {
                    _isQuotation = value;
                    base.OnPropertyChanged("IsQuotation", value);
                }
            }
        }

        string _comm;
        /// <summary>
        /// 品名
        /// </summary>
        public string Comm
        {
            get
            {
                if (_comm == null) return string.Empty;
                else return _comm;
            }
            set
            {
                if (_comm != value)
                {
                    _comm = value;
                    base.OnPropertyChanged("Comm", value);
                }
            }
        }


        Guid _transportclauseid;
        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid TransportClauseID
        {
            get
            {
                return _transportclauseid;
            }
            set
            {
                if (_transportclauseid != value)
                {
                    _transportclauseid = value;
                    base.OnPropertyChanged("TransportClauseID", value);
                }
            }
        }

        string _transportclausename;
        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClauseName
        {
            get
            {
                return _transportclausename;
            }
            set
            {
                if (_transportclausename != value)
                {
                    _transportclausename = value;
                    base.OnPropertyChanged("TransportClauseName", value);
                }
            }
        }

        List<Guid> _oceanitemids;
        /// <summary>
        /// 海运运价项目ID
        /// </summary>
        public List<Guid> OceanItemIDs
        {
            get
            {
                return _oceanitemids;
            }
            set
            {
                if (_oceanitemids != value)
                {
                    _oceanitemids = value;
                    base.OnPropertyChanged("OceanItemIDs", value);
                }
            }
        }

        public decimal P20GP
        {
            get;
            set;
        }

        public decimal P40GP
        {
            get;
            set;
        }

        public decimal P45HQ
        {
            get;
            set;
        }

        bool _isspecialfee;
        /// <summary>
        /// 是否特殊费用（如果是特殊费用，那么必须与运价项目关联才有效）
        /// </summary>
        public bool IsSpecialFee
        {
            get
            {
                return _isspecialfee;
            }
            set
            {
                if (_isspecialfee != value)
                {
                    _isspecialfee = value;
                    base.OnPropertyChanged("IsSpecialFee", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 行版本
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        public int RowIndex { get; set; }

        public bool Selected { get; set; }


        public override bool Equals(object obj)
        {
            PrototypeOceanFeederList newObj = obj as PrototypeOceanFeederList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }

    [Serializable]
    public class PrototypeOceanAdditionalFeeList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        bool _isexistsoceanitem;
        /// <summary>
        /// 是否关联运价
        /// </summary>
        public bool IsExistsOceanItem
        {
            get
            {
                return _isexistsoceanitem;
            }
            set
            {
                if (_isexistsoceanitem != value)
                {
                    _isexistsoceanitem = value;
                    base.OnPropertyChanged("IsExistsOceanItem", value);
                }
            }
        }


        Guid _oceanid;
        /// <summary>
        /// 海运运价ID
        /// </summary>
        public Guid OceanID
        {
            get
            {
                return _oceanid;
            }
            set
            {
                if (_oceanid != value)
                {
                    _oceanid = value;
                    base.OnPropertyChanged("OceanID", value);
                }
            }
        }


        List<Guid> _oceanitemids;
        /// <summary>
        /// 海运运价项目ID
        /// </summary>
        public List<Guid> OceanItemIDs
        {
            get
            {
                return _oceanitemids;
            }
            set
            {
                if (_oceanitemids != value)
                {
                    _oceanitemids = value;
                    base.OnPropertyChanged("OceanItemIDs", value);
                }
            }
        }


        Guid _chargingcodeid;
        /// <summary>
        /// 费用项目ID
        /// </summary>
        public Guid ChargingCodeID
        {
            get
            {
                return _chargingcodeid;
            }
            set
            {
                if (_chargingcodeid != value)
                {
                    _chargingcodeid = value;
                    base.OnPropertyChanged("ChargingCodeID", value);
                }
            }
        }


        string _chargingcode;
        /// <summary>
        /// 费用项目代码
        /// </summary>
        public string ChargingCode
        {
            get
            {
                return _chargingcode;
            }
            set
            {
                if (_chargingcode != value)
                {
                    _chargingcode = value;
                    base.OnPropertyChanged("ChargingCode", value);
                }
            }
        }


        string _chargingcodedescription;
        /// <summary>
        /// 费用项目描述
        /// </summary>
        public string ChargingCodeDescription
        {
            get
            {
                return _chargingcodedescription;
            }
            set
            {
                if (_chargingcodedescription != value)
                {
                    _chargingcodedescription = value;
                    base.OnPropertyChanged("ChargingCodeDescription", value);
                }
            }
        }


        Guid? _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID
        {
            get
            {
                return _customerid;
            }
            set
            {
                if (_customerid != value)
                {
                    _customerid = value;
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }


        string _customername;
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _customername;
            }
            set
            {
                if (_customername != value)
                {
                    _customername = value;
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }


        Guid _currencyid;
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID
        {
            get
            {
                return _currencyid;
            }
            set
            {
                if (_currencyid != value)
                {
                    _currencyid = value;
                    base.OnPropertyChanged("CurrencyID", value);
                }
            }
        }


        string _currencyname;
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName
        {
            get
            {
                return _currencyname;
            }
            set
            {
                if (_currencyname != value)
                {
                    _currencyname = value;
                    base.OnPropertyChanged("CurrencyName", value);
                }
            }
        }



        short _upercent;
        /// <summary>
        /// 百分比
        /// </summary>
        public short Percent
        {
            get
            {
                return _upercent;
            }
            set
            {
                if (_upercent != value)
                {
                    _upercent = value;
                    base.OnPropertyChanged("Percent", value);
                }
            }
        }

        public decimal P20GP
        {
            get;
            set;
        }
        public decimal P40GP
        {
            get;
            set;
        }
        public decimal P45HQ
        {
            get;
            set;
        }


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }


        bool _isspecialfee;
        /// <summary>
        /// 是否特殊费用（如果是特殊费用，那么必须与运价项目关联才有效）
        /// </summary>
        public bool IsSpecialFee
        {
            get
            {
                return _isspecialfee;
            }
            set
            {
                if (_isspecialfee != value)
                {
                    _isspecialfee = value;
                    base.OnPropertyChanged("IsSpecialFee", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 行版本
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        public bool Selected { get; set; }


        public override bool Equals(object obj)
        {
            PrototypeOceanAdditionalFeeList newObj = obj as PrototypeOceanAdditionalFeeList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }
    #endregion

    

}
