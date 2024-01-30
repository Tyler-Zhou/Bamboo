using System;
using System.ComponentModel;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI
{
    [ToolboxItem(false)]
    public partial class MovieProjectEdit : BaseEditPart
    {
        public MovieProjectEdit()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.Saved = null;
                this.bsList = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }            
            };

        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }



        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        #endregion

      
        #region IEditPart 成员
        bool isDirty;

        private bool IsDirty
        {
            get
            {
                return false;
            }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;



        #endregion

        [CommandHandler(MovieProjectCommandConstants.Command_MovieProjectAdd)]
        public void Command_MovieProjectAdd(object sender, EventArgs e)
        {
            DataDictionaryList item = new DataDictionaryList();
            item.IsValid = true;
            this.bsList.DataSource = item;
            this.bsList.ResetBindings(false);
        }
        private bool Save()
        {
            if (!this.ValidateData())
            {
                return false;
            }

            SingleResultData rusult = TransportFoundationService.SaveDataDictionaryInfo(CurrentData.ID,
                                                              CurrentData.Code,
                                                              CurrentData.CName,
                                                              CurrentData.EName,
                                                              this.txtRemark.Text,
                                                              DataDictionaryType.MovieProjects,
                                                              LocalData.UserInfo.LoginID,
                                                              CurrentData.UpdateDate);
            CurrentData.ID = rusult.ID;
            CurrentData.UpdateDate = rusult.UpdateDate;

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            if (Saved != null)
            {
                Saved(new object[] { CurrentData });
            }
            return true;
        }

        private bool ValidateData()
        {
            this.CurrentData.EndEdit();
            this.EndEdit();
            this.bsList.EndEdit();

            return true;

        }
        public DataDictionaryList CurrentData
        {
            get
            {
                return bsList.Current as DataDictionaryList;
            }
        }
        public void BindDataList(DataDictionaryList data)
        {
            this.bsList.DataSource = data;
            this.bsList.ResetBindings(false);
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
        }
    }
}
