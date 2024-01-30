using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Common.UI.Authcodes
{
    public partial class AuthcodeSearch : BaseSearchPart
    {

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }


        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        #endregion

        public AuthcodeSearch()
        {
            InitializeComponent();
        }

        #region 重写

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<AuthcodeInfo> DataList = TransportFoundationService.GetAuthcodeList(this.txtCode.Text, true, 100);
                if (OnSearched != null)
                {
                    OnSearched(sender, DataList);
                }
            }
        }
    }
}
