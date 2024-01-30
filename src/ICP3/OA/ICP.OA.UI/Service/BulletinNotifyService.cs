using System;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using DevExpress.XtraBars.Alerter;
using ICP.Framework.CommonLibrary.Client;
using ICP.OA.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
namespace ICP.OA.UI.Service
{  
    /// <summary>
    /// 公告回调服务类
    /// </summary>
    [CallbackBehavior(ConcurrencyMode=ConcurrencyMode.Multiple)]
    public class BulletinNotifyService : IBulletinNotifyService
    {



        public IMainForm MainForm
        {
            get
            {
                return ServiceClient.GetClientService<IMainForm>();
            }
        }

       ImageList listImage;
       BulletinData _bulletinData;
       private ImageList ListImage
       {
            get {
                if (listImage == null)
                {
                    listImage = new ImageList();
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                    listImage.ImageSize = new Size(32, 32);
                    listImage.Images.Add("Normal", Image.FromFile(Path.Combine(filePath,"normal.jpeg")));
                    listImage.Images.Add("Low", Image.FromFile(Path.Combine(filePath,"low.gif")));
                    listImage.Images.Add("High",Image.FromFile(Path.Combine(filePath,"high.gif")));
                    
                }
                return listImage;
            }
       }
  

       private void control_AlertClick(object sender, AlertClickEventArgs e)
       {
           e.AlertForm.Close();
           ShowBulletinContent();
           
       }
        /// <summary>
        /// 弹出窗体显示公告内容
        /// </summary>
       private void ShowBulletinContent()
       {   

           ICP.OA.UI.Bulletin.frmBulletinList frmBulletin = new ICP.OA.UI.Bulletin.frmBulletinList(_bulletinData);
           frmBulletin.ShowDialog();
       }
          
        
        /// <summary>
        /// 显示公告
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="content">公告内容</param>
        /// <param name="priority">优先级</param>
        /// <param name="cPublisherName">发布人中文名</param>
        /// <param name="ePublisherName">发布人英文名</param>
        private void Show(string caption, string content, BulletinPriority priority, string cPublisherName, string ePublisherName,Guid bulletinType)
        {
            
             AlertControl control = new AlertControl();
             control.FormLoad += new AlertFormLoadEventHandler(control_FormLoad);
            control.AlertClick += new AlertClickEventHandler(control_AlertClick);
                Form form = MainForm as Form;
                form.Show();
                control.Show(MainForm as Form, caption, content, content, ListImage.Images[Enum.GetName(typeof(BulletinPriority), priority)],LocalData.IsEnglish?ePublisherName:cPublisherName);

                
            
            
        }

  

     

        void control_FormLoad(object sender, AlertFormLoadEventArgs e)
        {
            e.Buttons.PinButton.SetDown(true);
        }
      
        public void Notify(BulletinData data)
        {
            _bulletinData = data;
            Show(data.Subject, data.Content, data.Priority, data.PublisherCName, data.PublisherEName,data.BulletinType);
        }

    }
}
