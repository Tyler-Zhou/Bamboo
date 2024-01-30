using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.Configure.Solution
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SolutionGLGroupEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        //public override string Title { get { return LocalData.IsEnglish ? "GLGroup Edit" : "编辑GL目录"; } }

        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        public SolutionGLGroupEditPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.popParent.QueryPopUp -= this.popParent_QueryPopUp;
                this.dxErrorProvider1.DataSource = null;
                this.treeParent.DataSource = null;
                this.bsParent.DataSource = null;
                this.bsParent.Dispose();
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.DataChanged = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

 

        private void InitControls()
        {
            this.popParent.QueryPopUp += new CancelEventHandler(popParent_QueryPopUp);
            if (LocalData.IsEnglish)
                colEName.Visible = true;
            else
                colCName.Visible = true;
        }

        #region Pop
        void popParent_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.popParent.QueryPopUp -= new CancelEventHandler(popParent_QueryPopUp);
            List<SolutionGLGroupList> data = ConfigureService.GetSolutionGLGroupList(string.Empty, string.Empty,0);
            SolutionGLGroupInfo currentData = bindingSource1.DataSource as SolutionGLGroupInfo;
            if (currentData.ID != Guid.Empty)
            {
                List<Guid> needRemoveIds = GetNeedRemoveNodeById(data, currentData.ID);
                List<SolutionGLGroupList> needRemoveChilds = data.FindAll(delegate(SolutionGLGroupList item) { return needRemoveIds.Contains(item.ID); });
                foreach (var item in needRemoveChilds)
                {
                    data.Remove(item);
                }
            }

            bsParent.DataSource = data;
        }

        List<Guid> GetNeedRemoveNodeById(List<SolutionGLGroupList> data, Guid currentId)
        {
            List<Guid> needRemoveIds = new List<Guid>();
            needRemoveIds.Add(currentId);

            while (true)
            {
                List<SolutionGLGroupList> childs = data.FindAll(delegate(SolutionGLGroupList item)
                { return item.ParentID.HasValue && needRemoveIds.Contains(item.ParentID.Value) && needRemoveIds.Contains(item.ID) == false; });

                if (childs == null || childs.Count == 0)
                    break;
                else
                {
                    foreach (SolutionGLGroupList item in childs)
                    {
                        needRemoveIds.Add(item.ID);
                    }
                }
            }
            return needRemoveIds;
        }


        SolutionGLGroupList CurrentSolutionGLGroupList
        {
            get { return bsParent.Current as SolutionGLGroupList; }
        }

        private void treeParent_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentSolutionGLGroupList != null)
            {
                SolutionGLGroupInfo currentData = bindingSource1.DataSource as SolutionGLGroupInfo;
                currentData.ParentID = CurrentSolutionGLGroupList.ID;
                currentData.ParentName = LocalData.IsEnglish ? CurrentSolutionGLGroupList.EName : CurrentSolutionGLGroupList.CName;
            }
            popParent.ClosePopup();
        }

        private void btnClearPop_Click(object sender, EventArgs e)
        {
            SolutionGLGroupInfo currentData = bindingSource1.DataSource as SolutionGLGroupInfo;
            currentData.ParentID =null;
            currentData.ParentName = string.Empty;
            popParent.ClosePopup();
        }

        #endregion

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labParent.Text = "分类";
            labEName.Text = "英文名";
            btnClearPop.Text = "清空";
        }

        public void BindingData(object data)
        {
            this.bindingSource1.DataSource = data;

            InitControls();
        }
        #region IDataContentPart 成员
        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion
    }
}
