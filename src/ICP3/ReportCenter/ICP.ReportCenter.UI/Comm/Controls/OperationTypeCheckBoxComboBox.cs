using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// 业务类型下拉勾选框
    /// </summary>
    public class OperationTypeCheckBoxComboBox : CheckBoxComboBox
    {
        public OperationTypeCheckBoxComboBox():base()
        {
           this.checkedTypes=this.GetOperationTypes();
        }
        private List<OperationType> checkedTypes;
        /// <summary>
        /// 默认需要勾选上的业务类型,默认为全部勾选
        /// </summary>
        public List<OperationType> CheckedTypes
        {
            get
            {
                return this.checkedTypes;
            }
            set
            {
                this.checkedTypes = value;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                AddOperationTypeItems();
            }
        }

        private void AddOperationTypeItems()
        {
            List<EnumHelper.ListItem<OperationType>> types = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            types.RemoveAll(type => type.Value == OperationType.Unknown);
            foreach (var item in types)
            {
                bool isChecked = false;
                if (checkedTypes != null && checkedTypes.Contains(item.Value))
                {
                    isChecked = true;
                }
                else
                {
                    isChecked = false;
                }
                this.AddItem(item.Value, item.Name, isChecked);
            }
            this.RefreshText();
        }
        /// <summary>
        /// 返回选择的业务类型
        /// </summary>
        public List<OperationType> SelectedOperationTypes
        {
            get
            {
                List<OperationType> operationTypes = new List<OperationType>();
                foreach (var item in this.EditValue)
                {
                    operationTypes.Add((OperationType)Enum.Parse(typeof(OperationType), item.ToString()));
                }
                return operationTypes;

            }
        }
        private List<OperationType> GetOperationTypes()
        {
          var types= EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            List<OperationType> list=new List<OperationType>();
            foreach(var item in types)
            {
              list.Add(item.Value);
            }
            return list;
        }
                 
           
    }
}
