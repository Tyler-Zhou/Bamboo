using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.ReportCenter.UI.Comm.Controls
{ 
    /// <summary>
    /// 航线下拉勾选框
    /// </summary>
  public  class ShipLineCheckBoxComboBox : CheckBoxComboBox
    {
      protected override void OnLoad(EventArgs e)
      {
          base.OnLoad(e);
          if (!LocalData.IsDesignMode)
          {
              this.FirstTimeEnter += new EventHandler(ShipLineCheckBoxComboBox_FirstTimeEnter);
              this.Disposed += delegate
              {
                  this.FirstTimeEnter -= this.ShipLineCheckBoxComboBox_FirstTimeEnter;
               };
          }
      }

      void ShipLineCheckBoxComboBox_FirstTimeEnter(object sender, EventArgs e)
      {
          AddShipLineItems();
      }

      public ReportCenterHelper ReportCenterHelper
      {
          get
          {
              return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
          }
      }

      private void AddShipLineItems()
      {
          this.BeginUpdate();
          List<ICP.Common.ServiceInterface.DataObjects.ShippingLineList> shipLineList = ReportCenterHelper.ShipLines;
          foreach (var item in shipLineList)
          {
              this.AddItem(item.ID, LocalData.IsEnglish ? item.EName : item.CName);
          }
          this.EndUpdate();
      }
    }
}
