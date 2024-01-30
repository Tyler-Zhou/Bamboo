using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class NewOIBusinessReceived : UserControl
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }


        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }


        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID
        {
            get;
            set;
        }

        public NewOIBusinessReceived()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Load += (sender, e) =>
                {
                    if (LocalData.IsEnglish) SetCnText();
                    this.dtpReleaseDate.EditValue = DateTime.Now;
                };
            }
        }

        public void SetCnText()
        {
            labOrderDate.Text = "OHBLRcved";
            btnOK.Text = "OK";
            btnClose.Text = "Cancel";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dtpReleaseDate.EditValue == null)
                {
                    string message = LocalData.IsEnglish ? "Please Select OHBL Rcved Date" : "请选择收到正本日期";
                    dtpReleaseDate.Focus();
                    MessageBox.Show(message);
                }
                else
                {
                    List<OceanBusinessHBLList> list = OceanImportService.GetOIBookingHBLList(BusinessID);
                    if (list.Any())
                    {
                        OceanImportService.SaveOIBookingHBLInfo(GetHBLSaveInfo(list,true)[0]);
                        this.FindForm().DialogResult = DialogResult.OK;
                        this.FindForm().Close();
                    }
                }

            }
            catch (Exception ex)
            {
                string message = (LocalData.IsEnglish ? "Received Faily" : "设置失败.") + ex.Message;
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
            }

        }
        /// <summary>
        /// 取消正本状态
        /// </summary>
        public bool CancelRcved()
        {
            bool returnValue = false;
            try
            {
                DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Do you want to un-mark [OHBL RCVED]?" : "是否取消标识[OHBL RCVED]?"
                                        , LocalData.IsEnglish ? "Tip" : "提示"
                                        , MessageBoxButtons.YesNo
                                        , MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    List<OceanBusinessHBLList> list = OceanImportService.GetOIBookingHBLList(BusinessID);
                    if (list.Any())
                    {
                        OceanImportService.SaveOIBookingHBLInfo(GetHBLSaveInfo(list, false)[0]);
                        returnValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = false;
                string message = (LocalData.IsEnglish ? "Received Faily" : "设置失败.") + ex.Message;
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
            }
            return returnValue;
        }
        
        /// <summary>
        /// 获得数据信息
        /// </summary>
        /// <returns></returns>
        public List<HBLInfoSaveRequest> GetHBLSaveInfo(List<OceanBusinessHBLList> oceanBusinessHblLists,bool OBLRcved)
        {
            if (oceanBusinessHblLists.Count > 0)
            {
                List<HBLInfoSaveRequest> commands = new List<HBLInfoSaveRequest>();
                List<Guid?> idList = new List<Guid?>();
                List<string> noList = new List<string>();
                List<Guid?> shipperIDList = new List<Guid?>();
                List<string> amsNoList = new List<string>();
                List<string> isfNoList = new List<string>();
                List<DateTime?> receiveOBLDateList = new List<DateTime?>();
                List<DateTime?> updateDateList = new List<DateTime?>();
                List<string> customerDescList = new List<string>();
                List<string> goodsList = new List<string>();
                List<Int32?> qtyList = new List<int?>();
                List<decimal?> weightList = new List<decimal?>();
                List<decimal?> measurementList = new List<decimal?>();

                foreach (OceanBusinessHBLList hbl in oceanBusinessHblLists)
                {
                    idList.Add(hbl.ID);
                    noList.Add(hbl.HBLNo);
                    shipperIDList.Add(hbl.ShipperID);
                    amsNoList.Add(hbl.AMSNo);
                    isfNoList.Add(hbl.ISFNo);
                    receiveOBLDateList.Add(hbl.ReceiveOBLDate);
                    updateDateList.Add(hbl.UpdateDate);
                    string customsDescription = SerializerHelper.SerializeToString<CustomerDescription>(new CustomerDescription(), true, false);
                    customerDescList.Add(customsDescription);

                    goodsList.Add(hbl.GoodsInfo);
                    qtyList.Add(hbl.Qty);
                    weightList.Add(hbl.Weight);
                    measurementList.Add(hbl.Measurement);

                }

                HBLInfoSaveRequest saveRequest = new HBLInfoSaveRequest();
                saveRequest.IDs = idList.ToArray();
                saveRequest.BLNos = noList.ToArray();
                saveRequest.ShipperIDs = shipperIDList.ToArray();
                saveRequest.AMSNos = amsNoList.ToArray();
                saveRequest.ISFNos = isfNoList.ToArray();
                saveRequest.ReceiveOBLDates = receiveOBLDateList.ToArray();
                saveRequest.UpdateDates = updateDateList.ToArray();
                saveRequest.ShipperDescriptions = customerDescList.ToArray();
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.OIBookingID = BusinessID;
                saveRequest.GoodsInfos = goodsList.ToArray();
                saveRequest.Qtys = qtyList.ToArray();
                saveRequest.Weights = weightList.ToArray();
                saveRequest.Measurements = measurementList.ToArray();
                if (!OBLRcved)
                    dtpReleaseDate.EditValue = DateTime.MinValue;
                saveRequest.ReceiveOBLDates = new DateTime?[] { DateTime.Parse(dtpReleaseDate.EditValue.ToString())};

                commands.Add(saveRequest);

                return commands;
            }
            return new List<HBLInfoSaveRequest>();
        }
    }

}

