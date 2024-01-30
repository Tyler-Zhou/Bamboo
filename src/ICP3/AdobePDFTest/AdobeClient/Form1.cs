using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Castle.Windsor;
using Castle.Facilities.WcfIntegration;
using ICP.FilePreviewServiceLibrary;
using System.Threading;
using System.IO;
namespace AdobeClient
{
    public partial class Form1 : Form
    {
        WindsorContainer container;
        private EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, "FilePreviewWaitHandle");
        string filePreviewAppName = "ICP.FilePreviewService.exe";
        Type type = typeof(IFilePreviewService);
        public Form1()
        {
            InitializeComponent();
            InitContainer();
        }

        private void InitContainer()
        {
            container = new WindsorContainer();
            container.AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero);
            var items = Castle.MicroKernel.Registration.Types.From(type);
            container.Register(items.Pick().Configure(c => c.Named(c.Implementation.Name).AsWcfClient(Get(type))));
        }
        public WcfClientModelBase Get(Type type)
        {
            WcfClientModelBase clientModel;

            clientModel = new DefaultClientModel();
            clientModel.Endpoint = WcfEndpoint.BoundTo(FilePreviewHelper.GetBinding()).At(string.Format("{0}/{1}", FilePreviewHelper.GetServiceBaseAddress(), type.Name.Substring(1)));
            return clientModel;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Screen scr = Screen.FromPoint(this.Location);
            //this.Location = new Point((int)(this.ClientSize), LocalData.Height);
            //this.Height = scr.WorkingArea.Height - LocalData.Height;
            //this.Width = (int)(scr.WorkingArea.Width * 0.6);
            Point location = this.Location;
            Size size = this.ClientSize;
            Preview(location, size,true);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Point location = PointToScreen(this.panel1.Location);
            Size size = this.panel1.Size;
            Preview(location, size,false);
        }
        private void Preview(Point location, Size size,bool isAutoHide)
        {
            EnsureFilePreviewAppExists();
            IFilePreviewService filePreviewService = container.Resolve<IFilePreviewService>();
            filePreviewService.Preview("rabbitmq-dotnet-client-3.1.1-wcf-service-model.pdf", location, size, isAutoHide);
        }
        private void EnsureFilePreviewAppExists()
        {
            

            System.Diagnostics.Process[] processes = GetEmailCenterProcesses();
            if (processes.Length <= 0)
            {

                waitHandle.Close();
                waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, "FilePreviewWaitHandle");
                string appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePreviewAppName);
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(appPath);
                waitHandle.WaitOne(5000);
            }
        }
        public System.Diagnostics.Process[] GetEmailCenterProcesses()
        {
            
            string processName = System.IO.Path.GetFileNameWithoutExtension(filePreviewAppName);
            return System.Diagnostics.Process.GetProcessesByName(processName);
        }
    }
}
