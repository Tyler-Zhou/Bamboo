using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace ICP.MailCenter.CommonUI
{  
    
    public partial class ContractPanel:FlowLayoutPanel
    {
        private List<string> _contractAddresses;
        private int childIndex = 1;
        /// <summary>
        /// 联系人地址集合
        /// </summary>
        public List<string> ContractAddresses
        {
            get
            {
                return this._contractAddresses;
            }
            set
            {
                this._contractAddresses = value;
                this.SuspendLayout();
                AddChildControls();
                this.ResumeLayout();
            }

        }

        private void AddChildControls()
        {
            this.Controls.Clear();
            this.Visible = false;
            if (this._contractAddresses == null || this._contractAddresses.Count <= 0)
            {

                return;
            }
            this.Visible = true;
            string[] addresses = this._contractAddresses.Distinct().ToArray();
            addresses = addresses.Where(address => !string.IsNullOrEmpty(address)).ToArray();
            ThreadPool.QueueUserWorkItem(data =>
            {
                Array.ForEach(addresses, address =>
                {

                    if (this.InvokeRequired)
                    {
                        AddDelegate addDelegate = new AddDelegate(InnerAddChildControl);
                        this.Invoke(addDelegate, address);
                    }
                    else

                        InnerAddChildControl(address);
                });
                if (this.InvokeRequired)
                {
                    RemoveLastChildDelegate removeDelegate = new RemoveLastChildDelegate(RemoveLastChild);
                    this.Invoke(removeDelegate);
                }
                else

                    RemoveLastChild();


            });

        }
        delegate void AddDelegate(string address);
        delegate void RemoveLastChildDelegate();
        private void RemoveLastChild()
        {
            int count = this.Controls.Count;
            int lastChildIndex = count - 1;
            if (count > 1 && this.Controls[lastChildIndex].GetType() == typeof(Label))
            {
                this.Controls.RemoveAt(lastChildIndex);
            }
        }
        private void InnerAddChildControl(string address)
        {
            ContractLabel lblContract = new ContractLabel();
            lblContract.Name = "lblContract" + (childIndex++).ToString();
            lblContract.Text = address;
            lblContract.ContractAddress = address;
            Label lblSemicolon = new Label();
            lblSemicolon.Name = "lblSemicolon" + (childIndex++).ToString();
            lblSemicolon.AutoSize = true;
            lblSemicolon.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            lblSemicolon.Text = ";";
            this.Controls.Add(lblContract);
            this.Controls.Add(lblSemicolon);

        }
        public ContractPanel()
        {
            InitializeComponent();
        }

        public ContractPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
