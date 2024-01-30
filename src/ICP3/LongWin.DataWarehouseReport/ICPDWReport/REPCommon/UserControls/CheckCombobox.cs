using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LongWin.DataWarehouseReport.WinUI
{
	/// <summary>
	/// 组合复选框
	/// </summary>
	public class CheckComboBox : System.Windows.Forms.UserControl
	{
		//复选列表框(单击时才被加载)
		private CheckedListBox checkedListBox1;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			this.ItemsChanaged +=new EventHandler(CheckComboBox_ItemsChanaged);
		}

		#region Code by builder
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBox1;


		public CheckComboBox()
		{
			InitializeComponent();

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Menu;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Webdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(108, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(16, 18);
            this.button1.TabIndex = 0;
            this.button1.Text = "6";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(124, 14);
            this.textBox1.TabIndex = 1;
            this.textBox1.Resize += new System.EventHandler(this.textBox1_Resize);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 22);
            this.panel1.TabIndex = 2;
            // 
            // CheckComboBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "CheckComboBox";
            this.Size = new System.Drawing.Size(128, 22);
            this.Resize += new System.EventHandler(this.CheckComboBox_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region 自定义属性的代码
		public event System.EventHandler  ItemsChanaged;
		private string[] _Items = new string[0];
		/// <summary>
		/// 项列表
		/// </summary>
		public string[] Items
		{
			get
			{
				return this._Items;
			}
			set
			{
				
				if(this._Items!= value)
				{
					_Items = value;
					if(this.ItemsChanaged !=null)
					{
						this.ItemsChanaged (this,new System .EventArgs ());
					}
				}
			}
		}

		

		private int _MaxChecked;
		/// <summary>
		/// 选择最大上限
		/// </summary>
		public int MaxChecked
		{
			get
			{
				return this._MaxChecked;
			}
			set
			{
				this._MaxChecked = value;
			}
		}

        public event System.EventHandler  TextChanaged;
		/// <summary>
		/// 获取或设置文本
		/// </summary>
		public override string Text
		{
			get
			{
				return this.textBox1.Text;
			}
			set
			{
				this.textBox1.Text = value;

                if (this.checkedListBox1 == null)
                {
                    this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
                    this.checkedListBox1.Name = "checkedListBox1";

                    this.checkedListBox1.TabIndex = 3;
                    this.checkedListBox1.BorderStyle = BorderStyle.FixedSingle;
                    this.checkedListBox1.CheckOnClick = true;
                    this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(checkedListBox1_ItemCheck);
                    this.checkedListBox1.SelectedValueChanged += new EventHandler(checkedListBox1_SelectedValueChanged);
                    this.checkedListBox1.Leave += new EventHandler(checkedListBox1_Leave);
                    if (_Items != null)
                    {
                        foreach (object item in _Items)
                        {
                            if (item != null )
                                this.checkedListBox1.Items.Add(item);

                        }

                        string[] values = this.textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (this.checkedListBox1 == null) return;
                        for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                        {
                            foreach (string str in values)
                            {
                                if (this.checkedListBox1.Items[i].ToString().ToUpper() ==
                                     str.ToUpper())
                                {
                                    this.checkedListBox1.SetItemChecked(i, true);
                                }
                            }
                        }

                    }
                }
                else
                {
                    this.checkedListBox1.ItemCheck -= new ItemCheckEventHandler(checkedListBox1_ItemCheck);
              
                    string[] values = this.textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (this.checkedListBox1 == null) return;
                    for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                    {
                        foreach (string str in values)
                        {
                            if (this.checkedListBox1.Items[i].ToString().ToUpper() ==
                                 str.ToUpper())
                            {
                                this.checkedListBox1.SetItemChecked(i, true);

                                if (!this._selectedText.Contains(this.checkedListBox1.Items[i]))
                                {
                                    this._selectedText.Add(this.checkedListBox1.Items[i]);
                                }
                            }
                        }
                    }
                    this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(checkedListBox1_ItemCheck);
                }
            }
		}
		#endregion

		#region 显示或加载复选列表框的代码
		private void ShowCheckListbox()
		{
			if (this.checkedListBox1 == null)
			{
				this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
				this.checkedListBox1.Name = "checkedListBox1";
              
				this.checkedListBox1.TabIndex = 3;
				this.checkedListBox1.BorderStyle = BorderStyle.FixedSingle;
				this.checkedListBox1.CheckOnClick = true;
                this.checkedListBox1.ItemCheck -= new ItemCheckEventHandler(checkedListBox1_ItemCheck);
				this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(checkedListBox1_ItemCheck);
				this.checkedListBox1.SelectedValueChanged += new EventHandler(checkedListBox1_SelectedValueChanged);
				this.checkedListBox1.Leave += new EventHandler(checkedListBox1_Leave);
				if (_Items != null)
				{
					foreach(object item in _Items)
					{
						this.checkedListBox1.Items.Add(item);       
                        
					}  
                 
                    string[] values = this.textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (this.checkedListBox1 == null) return;
                    for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                    {
                        foreach (string str in values)
                        {
                            if (this.checkedListBox1.Items[i].ToString().ToUpper() ==
                                 str.ToUpper())
                            {
                                this.checkedListBox1.SetItemChecked(i, true);
                            }
                        }
                    }

				}
			}
			this.RegMouseDownEvent(this);
			this.checkedListBox1.Size = new System.Drawing.Size(this.Width, 70);
			this.checkedListBox1.Location = GetPointToForm(this, new Point(0,0));
			this.checkedListBox1.Top += this.Height ;
			this.FindForm().Controls.Add(this.checkedListBox1);
			

			this.checkedListBox1.BringToFront();
			this.checkedListBox1.Show();
			this.checkedListBox1.Focus();
			
		}

		private void ShowText()
		{
			this.textBox1.Text = string.Empty;
			foreach(object value in this._selectedText)
			{
				if (this.textBox1.Text == string.Empty)
					this.textBox1.Text = value.ToString();
				else
					this.textBox1.Text += "," + value.ToString();
			}
			if(this.TextChanaged!=null)
			{
				this.TextChanaged(this,new EventArgs());
               
			}
		}
		
		
		private Point GetPointToForm(Control control, Point point)
		{
			
			if (control.Parent is Form)
			{
				return new Point(control.Left + point.X  - 1, control.Top  + point.Y - 1);
			}
			else
			{
				return GetPointToForm(control.Parent, new Point(point.X + control.Left, point.Y + control.Top ));
			}
		}
		#endregion

		#region 鼠标事件的代码
		private void RegMouseDownEvent(Control control)
		{
			control.Parent.MouseDown += new MouseEventHandler(Parent_MouseDown);
			
			if (control.Parent is Form) 
				return;
			else
				this.RegMouseDownEvent(control.Parent);
		}

		private void UnRegMouseDownEvent(Control control)
		{
			control.Parent.MouseDown -= new MouseEventHandler(Parent_MouseDown);
			
			if (control.Parent is Form) 
				return;
			else
				this.UnRegMouseDownEvent(control.Parent);
		}
		#endregion


		//--------------------------------------------------------------------
		/*
		 控件的事件
		 */
		private void button1_Click(object sender, System.EventArgs e)
		{
			this.ShowCheckListbox();
		}

		private void textBox1_Enter(object sender, System.EventArgs e)
		{
			this.HideCheckedListBox1();
		}

		private void HideCheckedListBox1()
		{
			if (this.checkedListBox1 != null)
			{
				this.checkedListBox1.Hide();
				this.UnRegMouseDownEvent(this);
			}
		}

		private void Parent_MouseDown(object sender, MouseEventArgs e)
		{
			this.HideCheckedListBox1();
		}

		private void checkedListBox1_Leave(object sender, EventArgs e)
		{
			this.HideCheckedListBox1();
		}

		private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
		{
            //检查是否有没有选择上的项            
            this.checkedListBox1.ItemCheck -= new ItemCheckEventHandler(checkedListBox1_ItemCheck);
            string[] values = this.textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (this.checkedListBox1 == null) return;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                foreach (string str in values)
                {
                    if (this.checkedListBox1.Items[i].ToString().ToUpper() ==
                         str.ToUpper())
                    {
                         this.checkedListBox1.SetItemChecked(i, true);

                        if (!this._selectedText.Contains(this.checkedListBox1.Items[i]))
                        {
                            this._selectedText.Add(this.checkedListBox1.Items[i]);
                        }
                    }
                }
            }
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(checkedListBox1_ItemCheck);

		}

		private ArrayList _selectedText = new ArrayList();
		private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Checked)
			{
				if (this._selectedText.Count >= _MaxChecked && _MaxChecked > 0)
				{
                    if (this._selectedText.Contains(this.checkedListBox1.Items[e.Index]))
                    {
                        return;
                    }
                    else
                    {
                        MessageBox.Show(" MAX Items is  :" + _MaxChecked.ToString());
                        e.NewValue = e.CurrentValue;
                        return;
                    }
				}
                if (!this._selectedText.Contains(this.checkedListBox1.Items[e.Index]))
                {
                    this._selectedText.Add(this.checkedListBox1.Items[e.Index]);
                }
			}
			else if (e.NewValue == CheckState.Unchecked)
				this._selectedText.Remove(this.checkedListBox1.Items[e.Index].ToString());
			
			this.ShowText();
		}

		private void textBox1_Resize(object sender, System.EventArgs e)
		{
			
		}

		private void CheckComboBox_Resize(object sender, System.EventArgs e)
		{
			
		}

		private void CheckComboBox_ItemsChanaged(object sender, EventArgs e)
		{
			if (_Items != null && this.checkedListBox1 != null)
			{
				this.checkedListBox1.Items.Clear();
				foreach(object item in _Items)
				{
					this.checkedListBox1.Items.Add(item);
				}
				this.MaxChecked = 0;
				this._selectedText.Clear();

                this.checkedListBox1.Items.Clear();
                if (_Items != null)
                {
                    foreach (object item in _Items)
                    {
                        this.checkedListBox1.Items.Add(item);

                    }
                }
			}
		}
	}

	
}
