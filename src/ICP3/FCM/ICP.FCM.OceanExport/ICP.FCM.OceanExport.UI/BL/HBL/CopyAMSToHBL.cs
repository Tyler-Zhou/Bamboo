using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using System.Collections.Generic;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.UI.MBL
{
    /// <summary>
    /// 从提单列表中提取AMS导入到新提单
    /// </summary>
    [ToolboxItem(false)]
    public partial class CopyAMSToHBL : BaseEditPart
    {
        #region service

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        #endregion

        #region init

        public CopyAMSToHBL()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.Saved = null;

            };
        }

        #endregion


        #region btn

        private void btnOK_Click(object sender, EventArgs e)
        {
            //查询AMS是否存在
            List<OceanHBL2AmsAciIsf> amsList = OceanExportService.GetAmsAciIsfOjbectsList(HBLInfo.ID, true);

            if (amsList == null || amsList.Count < 1)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Not exist of AMS info!" : "不存在AMS信息！");
                return;
            }
            //查询新提单信息
            string hblno = txtHBLNo.Text.Trim();
            Guid[] g = new Guid[] { HBLInfo.CompanyID };//
            List<OceanBLList> oceanHBLList = OceanExportService.GetOceanBLListForFaster(g, NoSearchType.BL, hblno, CustomerSearchType.All, string.Empty, PortSearchType.All, string.Empty, DateSearchType.All, null, null, 100);

            if (oceanHBLList == null || oceanHBLList.Count < 1)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Not exist of '" + hblno + "'!" : "不存在" + hblno + "！");
                return;
            }
            OceanBLList oceanHBL = oceanHBLList.Find(m => m.BLType == ICP.FCM.Common.ServiceInterface.DataObjects.FCMBLType.HBL && m.No == hblno);
            foreach (OceanHBL2AmsAciIsf ams in amsList)
            {
                ams.Container = new List<ContainerForAMS>();
                ams.ContainerDetails = new List<ContainerDetailsForAMS>();
                ams.CreateBy = LocalData.UserInfo.LoginID;
                ams.FirstPorOtfCall = string.Empty;
                ams.Flag = Guid.Empty;
                ams.FlagName = string.Empty;
                ams.IMO = string.Empty;
                ams.LastPortOfCall = string.Empty;
                ams.OceanHBLID = oceanHBL.ID;
                string vv = oceanHBL.VesselVoyage;
                if (!string.IsNullOrEmpty(vv))
                {
                    ams.VesselName = vv.Substring(0, vv.IndexOf('/'));
                    string voyage = vv.Substring(vv.IndexOf("/") + 1).Replace("V.", "");
                    if (voyage.Length > 5)
                        if (voyage.IndexOf('-') > 0)
                            ams.VoyageNumber = voyage.Substring(0, 4) + voyage.Substring(voyage.Length - 1);
                        else
                            ams.VoyageNumber = voyage;
                    else
                        ams.VoyageNumber = voyage;
                }
            }
            //保存新AMS信息
            OceanExportService.SaveAmsAciIsfOjbects(amsList, oceanHBL.ID, LocalData.UserInfo.LoginID, AMSEntryType.Unknown);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功.");
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }

        #endregion

        #region IEditPart 成员

        public OceanBLList HBLInfo { get; set; }

        public override object DataSource
        {
            get { return HBLInfo; }
            set { HBLInfo = value as OceanBLList; }
        }


        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion


    }
}
