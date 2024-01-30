using System;
using System.Windows.Forms;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.UI.SystemHelp.FeedbackSample
{
    public partial class FeedbackSamplePart : ICP.Framework.ClientComponents.UIFramework.BasePart
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public string email;
        public string EMail
        {
            get
            {
                if (string.IsNullOrEmpty(email))
                {
                    email = LocalData.UserInfo.EmailAddress;
                }

                return email;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                int SH = Screen.PrimaryScreen.Bounds.Height;
                int SW = Screen.PrimaryScreen.Bounds.Width;

                this.panel1.Width = SW-18;
                this.panel1.Height = SH-160;
                pictureBox1.Height=2050;
            }

        }


        public FeedbackSamplePart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }


   

 
    }
}
