using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Data;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.Controls;

namespace ICP.WF.UI
{
    public class HtmlPageBuilder
    {
        #region 本地变量

        //文档标题
        string _title;

        //工作名称
        string _workInfoName;

        //打印TableLayoutPanel控件
        TableLayoutPanel _panel;

        //打印宽度
        int _width;
        string _height;

        //HTML头

        string documentHead = @" <html>
                                   <head>
                                          <title>{0}</title>
                                          <meta http-equiv='Content-Type' content='text/html; charset=gb2312'>
                                   </head>
                                   <body >";

        //HTML底

        string documentBottom = @"  </body>
                                  </html>";


        //表格样式
        string tableStyle = "cellspacing = '0' cellpadding = '0'  style = 'WORD-BREAK: break-all;empty-cells: show;border-width: 0px;font-size: 12px; border-style: groove;'";


        //表格样式
        string childTableStyle = "cellspacing = '0' cellpadding = '0' style = 'WORD-BREAK: break-all;border-width: 0px;border-style: none;empty-cells: show;font-size: 12px;'";

        //行样式

        string trStyle = "bgcolor='#FFFFFF'";

        //单元格样式

        //string tdStyle = "style='vertical-align:top;text-align:left;padding: 1px;font-size: 12px; border-style: groove; border-width: 0px'";
        string tdNoneStyle = "style='vertical-align:center;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 0px none; BORDER-bottom: rgb(0,0,0) 0px none; BORDER-left: rgb(0,0,0) 0px none; BORDER-right: rgb(0,0,0) 0px none'";

        string tdLeftTopStyle = "style='vertical-align:center;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 0px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 0px groove'";


        string tdLeftTopRightStyle = "style='vertical-align:center;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 0px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 1px groove'";

        string tdLeftTopBottomStyle = "style='vertical-align:center;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 1px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 0px none'";

        string tdLeftTopRightBottomStyle = "style='vertical-align:center;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 1px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 1px groove'";

        //单元格样式

        string multLineStyle = "style='vertical-align:top;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 0px groove; BORDER-bottom: rgb(0,0,0) 0px groove; BORDER-left: rgb(0,0,0) 0px groove; BORDER-right: rgb(0,0,0) 0px groove'";

        string multLineLeftTopStyle = "style='vertical-align:top;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 0px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 0px groove'";

        string multLineLeftTopBottomStyle = "style='vertical-align:top;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 1px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 0px groove'";


        string multLineLeftTopRightStyle = "style='vertical-align:top;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 0px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 1px groove'";

        string multLineLeftTopRightBottomStyle = "style='vertical-align:top;font-size: 12px;text-align:left;BORDER-top: rgb(0,0,0) 1px groove; BORDER-bottom: rgb(0,0,0) 1px groove; BORDER-left: rgb(0,0,0) 1px groove; BORDER-right: rgb(0,0,0) 1px groove'";

        //表头样式
        string tableHeadStyle = "style='vertical-align:middle;text-align:center;font-size: 24px; font-weight: bold; font-style: normal;border:none;'";

        #endregion


        #region 构造函数


        public HtmlPageBuilder(TableLayoutPanel panel, string title, string workInfoName, int width, string height)
        {
            _title = title;
            _workInfoName = workInfoName;
            _panel = panel;
            _width = width;
            _height = height;

            //修改打印设置
            PageSetup(string.Empty, string.Empty);
        }

        #endregion


        #region 本地方法

        /// <summary>
        /// 生成Table
        /// </summary>
        /// <param name="panel">TableLayoutPanel控件</param>
        /// <returns></returns>
        string BuildHtmlTable(TableLayoutPanel panel)
        {

            StringBuilder temp = new StringBuilder();

            temp.AppendLine("<div align='center'>");
            temp.Append("<table>");
            temp.Append("<tr height='3px'>");
            temp.AppendLine();
            temp.AppendFormat("<td {0}>", tableHeadStyle);
            temp.AppendLine("</td>");
            temp.Append("</tr>");

            temp.Append("<tr>");
            temp.AppendLine("<td>");

            //插入table
            temp.AppendFormat("<table Id={0} {1} width={2} height={3}>", panel.Name, tableStyle, _width, _height);
            temp.AppendLine();

            //插入标题
            temp.AppendFormat("<tr height='40px' {0}>", trStyle);
            temp.AppendFormat("<td {0} colspan={1}> ", tableHeadStyle, panel.ColumnStyles.Count);
            temp.AppendLine("<strong fontsize='24px'>");
            temp.AppendLine(_title);
            temp.AppendLine("</strong>");
            temp.AppendLine("</td>");
            temp.AppendLine("</tr>");

            //插入工作名称
            temp.AppendFormat("<tr height='20px' {0}>", trStyle);
            temp.AppendFormat("<td {0} colspan={1}> ", tdNoneStyle, panel.ColumnStyles.Count);
            temp.AppendLine("<strong fontsize='24px'>");
            temp.AppendFormat("工作名称：{0}",_workInfoName);
            temp.AppendLine("</strong>");
            temp.AppendLine("</td>");
            temp.AppendLine("</tr>");


            Dictionary<int, object> vs = new Dictionary<int, object>(panel.ColumnCount);
            for (int j = 0; j < panel.ColumnCount; j++)
            {
                if (panel.ColumnStyles[j].SizeType == SizeType.Percent)
                {
                    vs.Add(j, panel.ColumnStyles[j].Width);
                }
                else
                {
                    vs.Add(j, (panel.ColumnStyles[j].Width / panel.Width) * 100);
                }
            }

            for (int k = 0; k < panel.RowStyles.Count; k++)
            {
                temp.AppendFormat("<tr {0} height={1}>", trStyle, panel.RowStyles[k].Height);
                temp.AppendLine();
                int colIndex = 0;
                for (int j = 0; j < panel.ColumnCount; j++)
                {
                    if (j < colIndex) continue;

                    Control control = panel.GetControlFromPosition(j, k);
                    if (control == null) continue;

                    int span = panel.GetColumnSpan(control);


                    LWMultiTextBox tb = control as LWMultiTextBox;
                    bool multline = false;
                    if (tb != null)
                    {
                        multline = true;
                    }

                      if (k == panel.RowCount - 1 && span + j == panel.ColumnCount)
                    {
                        temp.AppendFormat("<td {0} colspan={1} width='{2}%'> ", (multline ? multLineLeftTopRightBottomStyle : tdLeftTopRightBottomStyle), span, vs[j]);
                    }
                    else if (k == panel.RowCount-1)
                    {
                        temp.AppendFormat("<td {0} colspan={1} width='{2}%'> ", (multline ? multLineLeftTopBottomStyle : tdLeftTopBottomStyle), span, vs[j]);
                    }
                    else if (control is DataGridView)
                    {
                        temp.AppendFormat("<td {0} colspan={1} width='{2}%'> ", (multline ? multLineStyle : tdNoneStyle), span, vs[j]);
                    }
                    else if (span + j == panel.ColumnCount)
                    {
                        temp.AppendFormat("<td {0} colspan={1} width='{2}%'> ", (multline ? multLineLeftTopRightStyle : tdLeftTopRightStyle), span, vs[j]);
                    }
                    else
                    {
                        temp.AppendFormat("<td {0} colspan={1} width='{2}%'> ", (multline ? multLineLeftTopStyle : tdLeftTopStyle), span, vs[j]);
                    }

                    temp.AppendLine();


                    GetControlHTML(control,temp);

                    temp.AppendLine("</td>");

                    colIndex = j + span;
                }

                temp.AppendLine("</tr>");
            }

            temp.AppendLine("</table>");
            temp.AppendLine("</td>");
            temp.Append("</tr>");
            temp.AppendLine("</div>");
            return temp.ToString();
        }

        /// <summary>
        /// 生成控件
        /// </summary>
        /// <returns></returns>
        private void GetControlHTML(Control control, StringBuilder temp)
        {

            if (control is DataGridView)
            {
                temp.AppendLine(BuildHtmlTable(control as DataGridView));
            }
            else if (control is ICP.WF.Controls.LWRadioGroup)
            {
                temp.AppendLine(BuildHtmlRadioGroup(control as ICP.WF.Controls.LWRadioGroup));
            }
            else if (control is RadioButton)
            {
                temp.Append(BuildHtmlRadio(control as RadioButton));
            }
            else if (control is CheckBox)
            {
                temp.Append(BuildHtmlCheckbox(control as CheckBox));
            }
            else if (control is LWCheckBox) 
            {
                temp.Append(BuildHtmlLWCheckBox(control as LWCheckBox));
            }
            else
            {
                string vText = control.Text;
                if (string.IsNullOrEmpty(vText))
                {
                    vText = "&nbsp";
                }
                else
                {
                    vText = HttpUtility.HtmlEncode(vText).Replace("\r\n", "<br/>");
                }


                temp.AppendLine("<p Font-Size='12px'>");
                temp.AppendLine(vText);
                temp.AppendLine("</p>");
            }

        }

        /// <summary>
        /// 生成CheckBox
        /// </summary>
        /// <param name="rbButton"></param>
        /// <returns></returns>
        string BuildHtmlCheckbox(CheckBox rbButton)
        {
            StringBuilder temp = new StringBuilder();
            temp.AppendLine();
            temp.AppendFormat("<input type='checkbox' checked='{0}' Font-Size='12px'>", rbButton.Checked);
            temp.AppendLine(rbButton.Text);
            temp.AppendLine();
            temp.Append("</input>");

            return temp.ToString();
        }


        /// <summary>
        /// 生成LWCheckBox文本
        /// </summary>
        /// <param name="lwckbox"></param>
        /// <returns></returns>
        string BuildHtmlLWCheckBox(LWCheckBox  lwckbox) 
        {
            StringBuilder temp = new StringBuilder();
            temp.AppendLine();
            temp.AppendFormat("<input type='checkbox' checked='{0}' Font-Size='12px'>", lwckbox.Checked);
            temp.AppendLine(lwckbox.Text);
            temp.AppendLine();
            temp.Append("</input>");

            return temp.ToString();
        }

        /// <summary>
        /// 生成LWRadioGroup
        /// </summary>
        /// <param name="rbButton"></param>
        /// <returns></returns>
        string BuildHtmlRadioGroup(ICP.WF.Controls.LWRadioGroup rbButton)
        {
            StringBuilder temp = new StringBuilder();
            temp.AppendLine();
            temp.AppendFormat("<div>");
            foreach (ICP.WF.Controls.LWRadioButton rb in rbButton.Items)
            {
                temp.AppendLine(BuildHtmlRadio(rb));
                temp.AppendLine();
            }
            temp.Append("</div>");

            return temp.ToString();
        }

        /// <summary>
        /// 生成Radiochecked='checked'
        /// </summary>
        /// <param name="rbButton"></param>
        /// <returns></returns>
        string BuildHtmlRadio(RadioButton rbButton)
        {
            if (rbButton.Checked == false) return string.Empty;

            StringBuilder temp = new StringBuilder();
            temp.AppendLine();
            string text = string.Empty;
            if (rbButton.Checked)
            {
                temp.AppendFormat("<label  Font-Size='12px'>");
                text = HttpUtility.HtmlEncode(rbButton.Text);
            }
            temp.AppendLine(text);
            temp.AppendLine();
            temp.Append("</label>");

            return temp.ToString();
        }



        /// <summary>
        /// 生成CheckBox
        /// </summary>
        /// <param name="rbButton"></param>
        /// <returns></returns>
        string BuildHtmlCheckbox(LWCheckBox rbButton)
        {
            StringBuilder temp = new StringBuilder();
            temp.AppendLine();
            if (rbButton.Checked)
            {
                temp.AppendFormat("<input type='checkbox' checked='{0}' Font-Size='12px'>", rbButton.Checked);
            }
            else
            {
                temp.AppendFormat("<input type='checkbox' Font-Size='12px'>");
            }
            temp.AppendLine(rbButton.Text);
            temp.AppendLine();
         

            return temp.ToString();
        }


        /// <summary>
        /// 根据DataGridView生成Html表格
        /// </summary>
        /// <param name="dataGridView">DataGridView控件</param>
        /// <returns></returns>
        string BuildHtmlTable(DataGridView dataGridView)
        {
            StringBuilder temp = new StringBuilder();

            //插入table
            temp.AppendFormat("<table Id='{0}' {1} width='100%' height='100%'>", dataGridView.Name, childTableStyle);
            temp.AppendLine();


            //插入标题
            temp.AppendFormat("<tr {0} height='{1}'>", trStyle, dataGridView.ColumnHeadersHeight);
            foreach (DataGridViewColumn dc in dataGridView.Columns)
            {
                if (dc.Index == dataGridView.ColumnCount - 1)
                {
                    temp.AppendFormat("<td {0} width='{1}' Font-Size='12px'>", tdLeftTopRightStyle, dc.Width);
                }
                else
                {
                    temp.AppendFormat("<td {0} width='{1}' Font-Size='12px'>", tdLeftTopStyle, dc.Width);
                }
                temp.AppendLine(dc.HeaderText);
                temp.AppendLine("</td>");

            }
            temp.AppendLine("</tr>");

            DataTable dt = dataGridView.DataSource as DataTable;
            if (dt != null&&dt.Rows.Count>0)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(decimal) || col.DataType == typeof(int) || col.DataType == typeof(short))
                    {
                        dr[col] = dt.Compute(string.Format("SUM({0})", col.ColumnName), string.Empty);
                    }
                }
                dt.Rows.Add(dr);
                dt.AcceptChanges();    
 
            }

            foreach (DataGridViewRow rs in dataGridView.Rows)
            {
                if (rs.IsNewRow) continue;

                //插入行

                temp.AppendFormat("<tr {0} height='{1}'>", trStyle, rs.Height);
                temp.AppendLine();

                Dictionary<int, string> colindexs = new Dictionary<int, string>();
                foreach (DataGridViewCell cs in rs.Cells)
                {

                    string vText = string.Empty;

                    //获取单元格里面的文本

                    if (cs is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell coCell = (DataGridViewComboBoxCell)cs;
                        vText = this.GetText(coCell);
                    }
                    else
                    {
                        vText = cs.FormattedValue.ToString();
                    }


                    if (rs.Index == dataGridView.RowCount - 1 && cs.ColumnIndex == 0)
                    {
                        vText = LocalData.IsEnglish ? "Total:" : "合计:";
                    }


                    if (string.IsNullOrEmpty(vText))
                    {
                        vText = "&nbsp";
                    }
                    else
                    {
                        vText = HttpUtility.HtmlEncode(vText).Replace("\r\n", "<br/>");
                        colindexs.Add(cs.ColumnIndex, vText);
                    }


                    if (rs.Index < dataGridView.RowCount - 1)
                    {
                        if (cs.ColumnIndex == dataGridView.ColumnCount - 1)
                        {
                            temp.AppendFormat("<td {0} width='{1}' Font-Size='12px'>", tdLeftTopRightStyle, cs.Size.Width);
                        }
                        else
                        {
                            temp.AppendFormat("<td {0} width='{1}' Font-Size='12px'>", tdLeftTopStyle, cs.Size.Width);
                        }
                        temp.AppendLine();
                        temp.AppendLine(vText);
                        temp.AppendLine("</td>");
                    }
                }


                if (rs.Index == dataGridView.RowCount - 1)
                {
                    colindexs.Add(dataGridView.ColumnCount, string.Empty);

                    int lastIndex = 0;
                    int count = 1;
                    foreach (int ind in colindexs.Keys)
                    {
                        if (ind - lastIndex > 0)
                        {
                            if (colindexs.Keys.Count == count)
                            {
                                temp.AppendFormat("<td {0} colspan={1}  Font-Size='12px'>", tdLeftTopRightStyle, ind - lastIndex);
                                temp.AppendLine();
                                temp.AppendLine(colindexs[lastIndex]);
                                temp.AppendLine("</td>");
                            }
                            else
                            {
                                temp.AppendFormat("<td {0} colspan={1}  Font-Size='12px'>", tdLeftTopStyle, ind - lastIndex);
                                temp.AppendLine();
                                temp.AppendLine(colindexs[lastIndex]);
                                temp.AppendLine("</td>");
                            }
                        }

                        lastIndex = ind;
                        count++;
                    }
                }

                temp.AppendLine("</tr>");
            }

            temp.AppendLine("</table>");

            return temp.ToString();
        }


        private string GetText(DataGridViewComboBoxCell comboCell)
        {
            try
            {
                DataTable dt = null;
                string text = "";
                if (comboCell.DataSource is DataTable)
                {
                    dt = (DataTable)comboCell.DataSource;
                }
                else if (comboCell.DataSource is DataSet)
                {

                    dt = ((DataSet)comboCell.DataSource).Tables[0];

                }
                else if (comboCell.DataSource is DataView)
                {
                    dt = ((DataView)comboCell.DataSource).Table;
                }
                if (comboCell.Value == null || string.IsNullOrEmpty(comboCell.Value.ToString()))
                {
                    text = "";
                }
                else
                {
                    text = dt.Rows.Find(comboCell.Value)[comboCell.DisplayMember].ToString();
                }
                return text;
            }
            catch
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 生成HTML文档
        /// </summary>
        /// <returns></returns>
        string BuildHtml()
        {
            StringBuilder documentContent = new StringBuilder();

            //创建Html头

            string head = string.Format(documentHead, _title);
            documentContent.Insert(0, head);

            //创建Table类容
            string tablecontent = BuildHtmlTable(_panel);
            documentContent.AppendLine(tablecontent);

            //加入Html结束标记
            documentContent.AppendLine(documentBottom);

            return documentContent.ToString();

        }



        /// <summary>
        /// 写文件

        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Strings">文件内容</param>
        void WriteFile(string path, string strings)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding("gb2312"));
            f2.WriteLine(strings);
            f2.Close();
            f2.Dispose();
        }

        #endregion


        #region 外部接口

        /// <summary>
        /// 保存到指定文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void SaveAsHtml(string fileName)
        {
            string htmlContent = BuildHtml();

            WriteFile(fileName, htmlContent);
        }

        private void PageSetup(string header, string footer)
        {
            try
            {
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup\\", true);
                string head = rk.GetValue("header").ToString();
                if (string.IsNullOrEmpty(head) == false)
                {
                    rk.SetValue("header", header);
                    rk.SetValue("footer", footer);
                    rk.SetValue("margin_bottom", 0.06);
                    rk.SetValue("margin_left", 0.05);
                    rk.SetValue("margin_right", 0.05);
                    rk.SetValue("margin_top", 0.06);
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
