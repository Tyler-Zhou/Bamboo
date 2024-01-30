using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace ICP.FilePreviewService
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
    /// <summary>
    /// PDF操作接口
    /// </summary>
    public interface IPDFOperation
    {
        /// <summary>
        /// 载入单个文档(文档可以是非PDF类型，将在内部转换)
        /// </summary>
        /// <param name="filePath">文档的绝对路径</param>
        void Load(string filePath);
        /// <summary>
        /// 载入多个文档，文档将在内部转换合并成单个文档然后显示(文档可以是非PDF类型，将在内部转换)
        /// </summary>
        /// <param name="filePaths">文档的绝对路径</param>
        void Load(string[] filePaths);
        void Print();

    }
    
}
