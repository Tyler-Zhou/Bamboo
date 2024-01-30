using System;
using System.Data.Linq.Mapping;

namespace LongWin.ReportCenter.Model
{
    public partial class OperationResult
    {

        private Guid _Id;

        private string _ReferenceNO;


        public OperationResult()
        {
        }

        [Column(Storage = "_ReferenceNO", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string ReferenceNO
        {
            get
            {
                return this._ReferenceNO;
            }
            set
            {
                if ((this._ReferenceNO != value))
                {
                    this._ReferenceNO = value;
                }
            }
        }

        [Column(Storage = "_Id", DbType = "UniqueIdentifier NOT NULL")]
        public System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

    }
}
