using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using LongWin.DataWarehouseReport.ServiceInterface;

namespace LongWin.DataWarehouseReport.WinUI.Common
{
    public partial class TestService : UserControl
    {
    //    IREPPUBDataService _pubDataService;
    //    [ServiceDependency]
    //    public IREPPUBDataService PublicDataService
    //    {
    //        set { this._pubDataService = value; }
    //    }

        public TestService()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //JobInfoSet.JobInfoDataTable ds = this._pubDataService.GetJobInformation(new DateTime(1900, 1, 1), DateTime.Now, new Guid("1337179B-6A3A-44C2-AD7D-C74E3A6328BC"), new Guid("894F6B65-147E-4355-9DE3-8BD1160920D4"));
            //this.dataGridView1.DataSource = ds;
        }
    }
}
