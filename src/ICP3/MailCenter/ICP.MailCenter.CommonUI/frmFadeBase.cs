using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace ICP.MailCenter.CommonUI
{
    public partial class frmFadeBase : XtraForm
    {
        #region 私有字段
 
        private Timer DetectTimer = new Timer();
        private bool IsInBound = false;
        
		#endregion

        public Action<Boolean> MouseBoundChanged;


        public frmFadeBase()
        {  
           
            InitializeComponent();
            this.VisibleChanged += new EventHandler(frmFadeBase_VisibleChanged);
            
        }

        void frmFadeBase_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!this.DetectTimer.Enabled)
                {
                    this.DetectTimer.Enabled = true;
                    this.DetectTimer.Start();
                }
            }
            else
            {
                this.DetectTimer.Stop();
                this.DetectTimer.Enabled = false;
            }
        }

        void DetectTimer_Tick(object sender, EventArgs e)
        {
            bool inBound = this.Bounds.Contains(Cursor.Position);
            if (inBound != IsInBound)
            {
                IsInBound = inBound;
                if (this.MouseBoundChanged != null)
                {
                    MouseBoundChanged(inBound);
                }
            }
        }


        #region Form Overrides



        protected override void OnLoad(EventArgs e)
        {

            DetectTimer.Enabled = true;
            DetectTimer.Interval = 300;
            DetectTimer.Tick += new EventHandler(DetectTimer_Tick);
            base.OnLoad(e);
        }

     
        #endregion

      

        
    }
    
}
