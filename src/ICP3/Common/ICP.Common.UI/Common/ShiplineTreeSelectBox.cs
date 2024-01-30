using System;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.Data;

namespace ICP.Common.UI
{  
    /// <summary>
    /// 航线下拉选树控件
    /// </summary>
   public class ShiplineTreeSelectBox:ICP.Framework.ClientComponents.Controls.TreeSelectBox
    {
       public IClientBaseDataService ClientBaseDataService
       {
           get
           {
               return ServiceClient.GetClientService<IClientBaseDataService>();
           }
       }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!ICP.Framework.CommonLibrary.Client.LocalData.IsDesignMode)
            {
                //this.FirstEnter += this.OnFirstEnter;
                //this.Disposed += delegate
                //{
                //    this.FirstEnter -= this.OnFirstEnter;
                //};
            }
        }
        private void OnFirstEnter(object sender, EventArgs e)
        {
            AddShiplines();
        }
        private void AddShiplines()
        {
            DataTable dt = ClientBaseDataService.GetBaseData(BaseDataType.ShipLine);
            //this.SetSource(dt, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish ? "EName" : "CName", true);
        }
    }
    
}
