using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.ServiceInterface;

using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OPFileEditPart : BasePart
    {

        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }

        #endregion

        #region init

        public OPFileEditPart()
        {
            InitializeComponent();
            Disposed += delegate {
                ChangedFileHandler = null;
                _file = null;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!DesignMode) { InitMessage(); }

        }

        private void InitMessage()
        {
            RegisterMessage("SaveSuccessfully", "Save Successfully");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #endregion

        #region 接口
        OceanFileList _file = null;
        public void SetSource(OceanFileList file)
        {
            _file = file;
            txtFileName.Text = _file.FileName;
            txtRemark.Text = _file.Remark;
        }

        #endregion

        public event EventHandler ChangedFileHandler;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Save()) FindForm().Close();
        }

        private bool Save()
        {
            try
            {
                _file.Remark = txtRemark.Text.Trim();

                ManyResult result = OceanPriceService.SaveOceanFileInfo(_file.OceanID
                                            , new Guid?[] { _file.ID }
                                            , new string[] { _file.FileName }
                                            , new string[] { _file.Remark }
                                            , LocalData.UserInfo.LoginID
                                            , new DateTime?[] { _file.UpdateDate }
                                            );
                _file.ID = result.Items[0].GetValue<Guid>("ID");
                _file.UpdateDate = result.Items[0].GetValue<DateTime?>("UpdateDate");
                _file.BeginEdit();

                if (ChangedFileHandler != null)
                    ChangedFileHandler(_file, null);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); return false; }
        }
    }
}
