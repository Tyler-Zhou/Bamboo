using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ICP3.MailStoring.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = "";

            if (_list.Count == 0)
                LoadData();

            var found = _list.Find(a => a.MsgID == txtMsgID.Text.ToLower().Trim());

            if (found != null)
            {
                txtFilePath.Text = found.FilePath;
                txtFilePath.BackColor = Color.Yellow;
            }
            else
                txtFilePath.BackColor = Color.White;
            
        }

        List<MailStoring.MailRecModule> _list = new List<MailRecModule>();

        private void LoadData()
        {
            string[] files = Directory.GetFiles(txtDir.Text, "*.imap", SearchOption.AllDirectories);
            string mailBody;
            string msgID;
            int specialIDCount = 0;
            foreach (var file in files)
            {
                mailBody = File.ReadAllText(file);
                msgID = Regex.Match(mailBody, "Message-ID: .*", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture).Value.ToLower().Replace("message-id: ", "").Trim();

                if (msgID == "")
                {
                    //针对换行定义的Message-ID
                    int startMsgIDIndex;
                    int endMsgIDIndex;
                    //Environment.NewLine
                    startMsgIDIndex = mailBody.IndexOf("Message-ID:", StringComparison.OrdinalIgnoreCase);
                    endMsgIDIndex = mailBody.IndexOf(Environment.NewLine, startMsgIDIndex + 15);
                    msgID = mailBody.Substring(startMsgIDIndex, endMsgIDIndex - startMsgIDIndex).ToLower().Replace("message-id:","").Replace(Environment.NewLine,"").Trim();

                    specialIDCount++;
                    if (msgID == "")
                        throw new Exception("异常，获取的MSGID为空，文件：" + file);
                }

                _list.Add(new MailRecModule() { FilePath = file, MsgID = msgID, CreateTime = File.GetCreationTime(file) });
            }


            this.Text = specialIDCount.ToString();
            dataGridView1.DataSource = _list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSet1TableAdapters.uspGetUnDocumentedOperationMailListTableAdapter d = new DataSet1TableAdapters.uspGetUnDocumentedOperationMailListTableAdapter();
            var result = d.GetData();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"c:\unsavedMail" , FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, result);
            stream.Close();
        }
    }
}
