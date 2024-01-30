using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.ServiceComponent.Common
{
    public class DCDataContext : DataContext, IDisposable
    {
        private static MappingSource mappingSource = new AttributeMappingSource();
        public DCDataContext(string connection) :
            base(connection, mappingSource)
        {
        }

        public void Save()
        {
            base.SubmitChanges(ConflictMode.FailOnFirstConflict);
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            base.Dispose();
        }

        #endregion

        #region 对象转为特定类型

        public Table<LedgerMasters> LedgerMasters
        {
            get
            {
                return this.GetTable<LedgerMasters>();
            }
        }

        public Table<Ledgers> Ledgers
        {
            get
            {
                return this.GetTable<Ledgers>();
            }
        }

        #endregion
    }
}
