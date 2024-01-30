using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchOceanBaseInfoPart : BaseEditPart
    {
        public SearchOceanBaseInfoPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                baseInfo = null;
                bsList.DataSource = null;
                bsList.Dispose();
                EventBroker_ShowAttachment = null;
                EventBroker_ShowRemark = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    if (ContainerRateListPart != null)
                    {
                        Workitem.Items.Remove(ContainerRateListPart);
                        ContainerRateListPart = null;

                    }
                    Workitem = null;

                }
            
            };
        }
        #region 属性
        private SearchOceanBaseInfo baseInfo;
        #endregion

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }



        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public UCContainerRateListPart ContainerRateListPart { get; set; }

        #endregion

        #region 绑定数据
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        private void BindingData(object data)
        {
            SearchOceanBaseInfo info = data as SearchOceanBaseInfo;
            if (info == null)
            {
                baseInfo = new SearchOceanBaseInfo();
                bsList.DataSource = baseInfo;
                bsList.ResetBindings(false);

                ContainerRateListPart.DataSource = new List<FrmUnitRateList>();

                labRemarkDetails.Enabled = false;
                if (_IsShowRemark) ShowRemarkPart();

                labAttachments.Enabled = false;
                if (_IsShowFile) ShowRemarkPart();

            }
            else
            {
                baseInfo = info;
                if (!string.IsNullOrEmpty(baseInfo.Commodity))
                {
                    baseInfo.Commodity = baseInfo.Commodity.Replace(GlobalConstants.DividedSymbol, Environment.NewLine);
                }

                bsList.DataSource = baseInfo;
                bsList.ResetBindings(false);

                ContainerRateListPart.DataSource = baseInfo.UnitList;


                #region 设置备注、文件按钮的可用性与里面的内容

                if (baseInfo.RemarkDetails.IsNullOrEmpty())
                {
                    labRemarkDetails.Enabled = false;
                    if (_IsShowRemark) ShowRemarkPart();
                }
                else
                    labRemarkDetails.Enabled = true;


                if (baseInfo.FilesCount <= 0)
                {
                    labAttachments.Enabled = false;
                    if (_IsShowFile) ShowRemarkPart();
                }
                else
                    labAttachments.Enabled = true;

                #endregion

            }
            ContainerRateListPart.InitGridControl(false,false);
        }
        #endregion

        #region 显示关闭Remark&FilterList

        bool _IsShowRemark = false;
        // 显示关闭Remark
        private void labRemarkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowRemarkPart();
        }

        //[EventPublication(SearchOceanEventBrokerConstants.EventBroker_ShowRemark, PublicationScope.Global )]
        public event EventHandler EventBroker_ShowRemark;
        private void ShowRemarkPart()
        {
            if (EventBroker_ShowRemark != null)
            {
                EventBroker_ShowRemark(this, EventArgs.Empty);
            }
            //this.Workitem.Commands[SearchOceanCommandConstants.Command_ShowRemark].Execute();

            _IsShowRemark = !_IsShowRemark;

            if (_IsShowRemark)
                labRemarkDetails.Text = "Close Remark Details";
            else
                labRemarkDetails.Text = "Remark Details";

            _IsShowFile = false;
            labAttachments.Text = "Attachments";

        }

        //显示/关闭Files
        bool _IsShowFile = false;
        private void labAttachments_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowFilesPart();
        }

        //[EventPublication(SearchOceanEventBrokerConstants.EventBroker_ShowAttachment, PublicationScope.Global)]
        public event EventHandler EventBroker_ShowAttachment;
        private void ShowFilesPart()
        {
            if (EventBroker_ShowAttachment != null)
            {
                EventBroker_ShowAttachment(this, EventArgs.Empty);
            }
            //this.Workitem.Commands[SearchOceanCommandConstants.Command_ShowAttachment].Execute();


            _IsShowFile = !_IsShowFile;

            if (_IsShowFile)
                labAttachments.Text = "Close Attachments";
            else
            {
                labAttachments.Text = "Attachments";
            }



            _IsShowRemark = false;
            labRemarkDetails.Text = "Remark Details";
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                pnlScrollable.BackColor = pnlMain.BackColor;
                AddContainsRateListToGroupBoxPanel();
            }
        }

        private void AddContainsRateListToGroupBoxPanel()
        {
            ContainerRateListPart.Dock = DockStyle.Fill;
            gbxContainerRate.Controls.Add(ContainerRateListPart);
        }

        #endregion

     
    }
}
