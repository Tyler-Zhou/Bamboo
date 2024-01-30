using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
//using ICP.MailCenterFramework.UI;
namespace ICP_OutlookAddIn
{
    public partial class ICP_Ribbon : OfficeRibbon
    {
        public ICP_Ribbon()
        {
            InitializeComponent();
        }
        //OutLookToolBarPart outLookTool = new OutLookToolBarPart();
        private void ICP_Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
          
            //FlowLayoutPanel pan1 = new FlowLayoutPanel();
            //FlowLayoutPanel.Controls.Add(outLookTool);
            //group1.Items.Add(outLookTool);
            //Button btn2=new Button();
            //RibbonControl rc = btn2 as RibbonControl;
            //this.group1.Items.Add(btn2);
            //System.ComponentModel.IComponent a = outLookTool as System.ComponentModel.IComponent;
            //group1.Container.Add(a, "outLookTool");
            //this.group1.Container.Add(outLookTool);
            //outLookTool.Visible = true;
            //outLookTool.Show();
            
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            //outLookTool.Show();
        }
   }
}
