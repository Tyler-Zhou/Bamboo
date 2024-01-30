using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.UI
{
    public abstract class BaseMiniDataFinder : IMiniDataFinder,IDisposable
    {
        #region IMiniDataFinder 成员

        public virtual event EventHandler<DataFindEventArgs> DataChoosed;

        public virtual void PickMany(string[] returnFields, Guid[] existValues, System.Windows.Forms.Control container) { }
        public virtual void PickOne(string searchValue, string property, string[] returnFields, System.Windows.Forms.Control container) { }

        public virtual void ResetCondition(IDictionary<string, object> values) { }

        #endregion

        #region IDisposable 成员

        public virtual void Dispose()
        {
            this.DataChoosed = null;
        }

        #endregion
    }
}
