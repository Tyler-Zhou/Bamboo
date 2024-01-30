using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using System.Drawing;
using System.ComponentModel;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.Comm.OperationTypeSearchParts
{
    /// <summary>
    /// 搜索面板工厂
    /// </summary>
    public class OperationTypeSearchPartFactory
    {
        /// <summary>
        /// 返回对应的搜索面板的实例
        /// </summary>
        public static OperationTypeSearchPart GetSearchPart(OperationType? operationType)
        {
            if (operationType == null) return new NoneSearchPart();


            if (operationType == OperationType.OceanExport )
                return new OESearchPart();
            else if(operationType == OperationType.OceanImport)
                return new OISearchPart();
            else if (operationType == OperationType.AirExport)
                return new AirSearchPart();
            else if (operationType == OperationType.AirImport)
                return new AirSearchPart();
            else if (operationType == OperationType.Customs)
                return new CustomsSearchPart();
            else if (operationType == OperationType.Other)
                return new OtherSearchPart();
            else if (operationType == OperationType.Truck)
                return new NoneSearchPart();
            else
                return new NoneSearchPart();

        }
    }

    [ToolboxItem(false)]
    public  class OperationTypeSearchPart : BasePart
    {
        public string BLNo { get; set; }
        public string CtnNo{get;set;}
        public string ChargeCodeIDs{get;set; }

        public string FilterXmlHeader = "<BillSearchFilter>";

        public string FilterXmlFoot = "</BillSearchFilter>";

        public virtual OperationParameter GetOperationParameter()
        {
            OperationParameter paramenter = new OperationParameter();

            paramenter.BlNo = BLNo;
            paramenter.CtnNo = CtnNo;
            paramenter.ChargeCodeIDs = ChargeCodeIDs;
            return paramenter;
        
        }

        public virtual void SetTextBoxLocation(int labX, int txtBoxX, int txtBoxWidth)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].Name == "chkIsARC")
                {
                    continue;
                }


                if (Controls[i] is LabelControl || Controls[i] is CheckEdit)
                {
                    Controls[i].Location = new Point(labX, Controls[i].Location.Y);
                }
                else
                {
                    Controls[i].Location = new Point(txtBoxX, Controls[i].Location.Y);
                    Controls[i].Width = txtBoxWidth;
                }
            }
        }

        public virtual void Clear() { }
    }
}
