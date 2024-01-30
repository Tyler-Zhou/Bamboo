using ICP.DataCache.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.Communication
{
    public partial class CommunicationHistoryStage : BasePart
    {
        public ICommunicationHistoryService HistoryService
        {
            get
            {
                return ServiceClient.GetService<ICommunicationHistoryService>();
            }
        }

        public CommunicationHistoryStage()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
        }

        public CommunicationHistoryListPresenter ListPresenter { get; set; }
         
        public string ContactStages
        {
            get
            {
                StringBuilder stageTypes = new StringBuilder();
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chklStage.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        stageTypes.Append(((int)item.Value).ToString()+','); 
                    }
                }
                return stageTypes.ToString().Substring(0, stageTypes.Length > 0 ? stageTypes.Length - 1 : stageTypes.Length);
            }
        }

        private void InitControls()
        {
            this.chklStage.Items.BeginUpdate();
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ContactStage>> stagePrograms = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<ContactStage>(LocalData.IsEnglish);
            this.chklStage.Items.Clear();
            foreach (var item in stagePrograms)
            {
                if (item.Value == ContactStage.Unknown) continue;
                this.chklStage.Items.Add(item.Value, false);
            }
            this.chklStage.Items.EndUpdate();
            InnerData();
        }

        void InnerData()
        {
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chklStage.Items)
            {
                item.CheckState = CheckState.Unchecked;
                string[] contactStages = ListPresenter.ucList.Current.ContactStage.Split(',');
                foreach (string contactStage in contactStages)
                {
                    if (item.Value.ToString() == contactStage)
                    {
                        item.CheckState = CheckState.Checked;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
         
        private void btnOK_Click(object sender, EventArgs e)
        {
            HistoryService.SetCommunicationHistoryStage(ListPresenter.ucList.Current.Id, ContactStages);
            ListPresenter.ucList.Current.ContactStage = ContactStages.ToStageNames();
            BusinessOperationContext context=new BusinessOperationContext
            {
                OperationID = ListPresenter.ucList.Current.OperationId
            };
            ListPresenter.LoadData(context);
            this.FindForm().Close(); 
        }
         
    }
}
