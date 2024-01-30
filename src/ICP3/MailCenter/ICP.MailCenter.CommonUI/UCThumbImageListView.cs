using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Wintellect.Threading.AsyncProgModel;
namespace ICP.MailCenter.CommonUI
{
    public partial class UCThumbImageListView : XtraUserControl
    {
        #region 私有字段
        private List<string> thumbImagePaths = new List<string>();
        private string sourceFilePath;
        private List<string> sourceFileNames = new List<string>();
        private bool isEnglish = LocalData.IsEnglish;

        #endregion
        #region 属性
        /// <summary>
        /// 列表中所选缩略图改变事件
        /// </summary>
        public EventHandler<Microsoft.Practices.CompositeUI.Utility.DataEventArgs<int>> CurrentIndexChanged;

        /// <summary>
        /// 缩略图存放路径
        /// </summary>
        public string SourceFilePath
        {
            get { return this.sourceFilePath; }
            set
            {
                if (string.IsNullOrEmpty(sourceFilePath))
                {
                    value = AppDomain.CurrentDomain.BaseDirectory;
                }
                this.sourceFilePath = value;



            }
        }
        /// <summary>
        /// 原文件名称,即缩略图来源的文件名称
        /// </summary>
        public List<string> SourceFileNames
        {
            get
            {
                return sourceFileNames;

            }
            set
            {
                if (value == null)
                {
                    value = new List<string>();
                }
                sourceFileNames = value;

                AddChildControl();
            }
        }
        #endregion

        private void AddChildControl()
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(InnerAddChildControl(), async.EndExecute);
        }
        private IEnumerator<Int32> InnerAddChildControl()
        {
            GetThumbImagePaths();
            List<Image> images = GenerateImages();
            this.imageList.Images.Clear();

            this.imageList.Images.AddRange(images.ToArray());
            this.listViewThumbView.SuspendLayout();
            this.listViewThumbView.Items.Clear();
            AddListViewItems();

            this.listViewThumbView.ResumeLayout();
            yield return 1;
        }

        private void AddListViewItems()
        {
            for (int i = 0; i < this.imageList.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Name = "item" + i.ToString();
                item.ImageIndex = i;
                item.ToolTipText = i.ToString();
                this.listViewThumbView.Items.Add(item);
            }

        }

        private List<Image> GenerateImages()
        {
            List<Image> images = new List<Image>();
            foreach (string thumbImageFile in this.thumbImagePaths)
            {
                Image image = Image.FromFile(thumbImageFile);
                images.Add(image);
            }
            return images;
        }

        private void GetThumbImagePaths()
        {
            foreach (string fileName in sourceFileNames)
            {

                List<string> thumbImageFiles = Directory.GetFiles(this.sourceFilePath, string.Format("{0}*{1}", Path.GetFileNameWithoutExtension(fileName), ".jpg"), SearchOption.TopDirectoryOnly).ToList();
                thumbImageFiles.Sort(CompareThumbImageFileNameByLength);
                this.thumbImagePaths.AddRange(thumbImageFiles);

            }
        }
        private static int CompareThumbImageFileNameByLength(string x, string y)
        {
            int lengthOfx = x.Length, lengthOfy = y.Length;
            if (lengthOfx > lengthOfy)
                return 1;
            else if (lengthOfx == lengthOfy)
            {
                return x.CompareTo(y);

            }
            else
            {
                return -1;
            }
        }


        public UCThumbImageListView()
        {
            InitializeComponent();
        }

        private void listViewThumbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageIndex = this.listViewThumbView.SelectedIndices[0];
            if (this.CurrentIndexChanged != null)
            {
                this.CurrentIndexChanged(this.listViewThumbView.SelectedItems[0], new Microsoft.Practices.CompositeUI.Utility.DataEventArgs<int>(pageIndex));
            }


        }
    }
}
