// Textfile_typeinfo.cs
// This file contains generated code and will be overwritten when you rerun code generation.

//using <some stuff>

using Altova.TypeInfo;

namespace PIL.Booking
{

    public class Textfile_TypeInfo
    {
        // namespaces indices
        public static readonly int _altova_ni = 0;


        // typeinfo indices
        public static readonly int _altova_tif = 0;
        public static readonly int _altova_ti_altova_any = _altova_tif;
        public static readonly int _altova_ti_altova_string2 = 1;
        public static readonly int _altova_ti_altova_boolean = 2;
        public static readonly int _altova_ti_altova_decimal2 = 3;
        public static readonly int _altova_ti_altova_double2 = 4;
        public static readonly int _altova_ti_altova_duration = 5;
        public static readonly int _altova_ti_altova_time = 6;
        public static readonly int _altova_ti_altova_date = 7;
        public static readonly int _altova_ti_altova_dateTime = 8;
        public static readonly int _altova_ti_altova_integer = 9;
        public static readonly int _altova_ti_altova_hexBinary = 10;
        public static readonly int _altova_ti_altova_anyURI = 11;
        public static readonly int _altova_ti_altova_unnamedType = 12;
        public static readonly int _altova_ti_altova_RowsType = 13;
        public static readonly int _altova_ti_altova_stringType = 14;
        public static readonly int _altova_til = 15;



        // memberinfo indices
        public static readonly int _altova_mif_altova_any = 0;
        public static readonly int _altova_mil_altova_any = _altova_mif_altova_any;

        public static readonly int _altova_mif_altova_string2 = _altova_mil_altova_any;
        public static readonly int _altova_mil_altova_string2 = _altova_mif_altova_string2;

        public static readonly int _altova_mif_altova_boolean = _altova_mil_altova_string2;
        public static readonly int _altova_mil_altova_boolean = _altova_mif_altova_boolean;

        public static readonly int _altova_mif_altova_decimal2 = _altova_mil_altova_boolean;
        public static readonly int _altova_mil_altova_decimal2 = _altova_mif_altova_decimal2;

        public static readonly int _altova_mif_altova_double2 = _altova_mil_altova_decimal2;
        public static readonly int _altova_mil_altova_double2 = _altova_mif_altova_double2;

        public static readonly int _altova_mif_altova_duration = _altova_mil_altova_double2;
        public static readonly int _altova_mil_altova_duration = _altova_mif_altova_duration;

        public static readonly int _altova_mif_altova_time = _altova_mil_altova_duration;
        public static readonly int _altova_mil_altova_time = _altova_mif_altova_time;

        public static readonly int _altova_mif_altova_date = _altova_mil_altova_time;
        public static readonly int _altova_mil_altova_date = _altova_mif_altova_date;

        public static readonly int _altova_mif_altova_dateTime = _altova_mil_altova_date;
        public static readonly int _altova_mil_altova_dateTime = _altova_mif_altova_dateTime;

        public static readonly int _altova_mif_altova_integer = _altova_mil_altova_dateTime;
        public static readonly int _altova_mil_altova_integer = _altova_mif_altova_integer;

        public static readonly int _altova_mif_altova_hexBinary = _altova_mil_altova_integer;
        public static readonly int _altova_mil_altova_hexBinary = _altova_mif_altova_hexBinary;

        public static readonly int _altova_mif_altova_anyURI = _altova_mil_altova_hexBinary;
        public static readonly int _altova_mil_altova_anyURI = _altova_mif_altova_anyURI;

        public static readonly int _altova_mif_altova_unnamedType = _altova_mil_altova_anyURI;
        public static readonly int _altova_mi_altova_unnamedType_altova_Rows = _altova_mif_altova_unnamedType;
        public static readonly int _altova_mil_altova_unnamedType = 1;

        public static readonly int _altova_mif_altova_RowsType = _altova_mil_altova_unnamedType;
        public static readonly int _altova_mi_altova_RowsType_altova_DATA = _altova_mif_altova_RowsType;
        public static readonly int _altova_mil_altova_RowsType = 2;

        public static readonly int _altova_mif_altova_stringType = _altova_mil_altova_RowsType;
        public static readonly int _altova_mi_altova_stringType_altova_unnamed = _altova_mif_altova_stringType;
        public static readonly int _altova_mil_altova_stringType = 3;



        public class InfoBinder : InfoBinderInterface
        {
            public NamespaceInfo[] Namespaces { get { return namespaces; } }
            public TypeInfo[] Types { get { return types; } }
            public MemberInfo[] Members { get { return members; } }
        }

        public static InfoBinderInterface binder = new InfoBinder();

        // array of all namespaces with poin... references to types
        public static NamespaceInfo[] namespaces = 
		{
			new NamespaceInfo(binder, "", "", _altova_tif, _altova_til),
		};

        // array of all types with references to members
        public static TypeInfo[] types = 
		{
			new TypeInfo( binder, _altova_ni, "any", 0, _altova_mif_altova_any, _altova_mil_altova_any,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "string", _altova_ti_altova_any, _altova_mif_altova_string2, _altova_mil_altova_string2,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "boolean", _altova_ti_altova_any, _altova_mif_altova_boolean, _altova_mil_altova_boolean,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "decimal", _altova_ti_altova_any, _altova_mif_altova_decimal2, _altova_mil_altova_decimal2,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "double", _altova_ti_altova_any, _altova_mif_altova_double2, _altova_mil_altova_double2,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "duration", _altova_ti_altova_any, _altova_mif_altova_duration, _altova_mil_altova_duration,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "time", _altova_ti_altova_any, _altova_mif_altova_time, _altova_mil_altova_time,  null, 				WhitespaceType.Unknown,  Altova.Xml.Xs.TimeFormatter ),
			new TypeInfo( binder, _altova_ni, "date", _altova_ti_altova_any, _altova_mif_altova_date, _altova_mil_altova_date,  null, 				WhitespaceType.Unknown,  Altova.Xml.Xs.DateFormatter ),
			new TypeInfo( binder, _altova_ni, "dateTime", _altova_ti_altova_any, _altova_mif_altova_dateTime, _altova_mil_altova_dateTime,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "integer", _altova_ti_altova_any, _altova_mif_altova_integer, _altova_mil_altova_integer,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "hexBinary", _altova_ti_altova_any, _altova_mif_altova_hexBinary, _altova_mil_altova_hexBinary,  null, 				WhitespaceType.Unknown, Altova.Xml.Xs.HexBinaryFormatter ),
			new TypeInfo( binder, _altova_ni, "anyURI", _altova_ti_altova_any, _altova_mif_altova_anyURI, _altova_mil_altova_anyURI,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "", 0, _altova_mif_altova_unnamedType, _altova_mil_altova_unnamedType,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "", 0, _altova_mif_altova_RowsType, _altova_mil_altova_RowsType,  null, 				WhitespaceType.Unknown ),
			new TypeInfo( binder, _altova_ni, "", 0, _altova_mif_altova_stringType, _altova_mil_altova_stringType,  null, 				WhitespaceType.Unknown ),

			
		};

        // array of all members
        public static MemberInfo[] members = 
		{
			new MemberInfo (binder, "", "Rows", _altova_ti_altova_unnamedType, _altova_ti_altova_RowsType, MemberFlags.None, 0, -1),

			new MemberInfo (binder, "", "DATA", _altova_ti_altova_RowsType, _altova_ti_altova_stringType, MemberFlags.None, 0, 1),

			new MemberInfo (binder, "", "", _altova_ti_altova_stringType, _altova_ti_altova_string2, MemberFlags.None|MemberFlags.IsAttribute, 1, 1),

		};
    }
}
