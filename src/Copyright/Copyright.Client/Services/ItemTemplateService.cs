/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 10:04:00
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using Copyright.Client.Common;
using Copyright.Client.Helpers;
using Copyright.Client.Models;
using Copyright.Client.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Copyright.Client.Services
{
    /// <summary>
    /// Item Template Service
    /// </summary>
    public class ItemTemplateService
    {
        #region Services
        /// <summary>
        /// Config Service
        /// </summary>
        JsonService _ConfigService = null;
        /// <summary>
        /// Document Service
        /// </summary>
        TemplateService _DocumentService = null;
        #endregion

        #region Constructor
        public ItemTemplateService()
        {
            _ConfigService = new JsonService();
            _DocumentService = new TemplateService();
        }
        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ItemTemplateModel> GetCollection()
        {
            ObservableCollection<ItemTemplateModel> results = _ConfigService.Get<ObservableCollection<ItemTemplateModel>>(AppConstant.ITEMTEMPLATE_NAME);
            if (results == null)
            {
                string path = CopyrightHelper.GetItemTemplatePath();
                results = new ObservableCollection<ItemTemplateModel>()
                {
                    //Code
                    new ItemTemplateModel(){ Key="代码-类",FullPath = $"{path}\\Code\\2052\\Class\\Class.cs" },
                    new ItemTemplateModel(){ Key="代码-接口",FullPath = $"{path}\\Code\\2052\\Interface\\Interface.cs" },
                    //General
                    new ItemTemplateModel(){Key="常规-组件类", FullPath = $"{path}\\General\\2052\\Component\\component.cs" },
                    new ItemTemplateModel(){Key="常规-Windows 服务", FullPath = $"{path}\\General\\2052\\Service\\service.cs" },
                    //Windows Forms
                    new ItemTemplateModel(){Key="Windows窗体-关于Box", FullPath = $"{path}\\Windows Forms\\2052\\AboutBox\\AboutBox.cs" },
                    new ItemTemplateModel(){Key="Windows窗体-自定义控件", FullPath = $"{path}\\Windows Forms\\2052\\CustomControl\\customcontrol.cs" },
                    new ItemTemplateModel(){Key="Windows窗体-窗体", FullPath = $"{path}\\Windows Forms\\2052\\Form\\form.cs" },
                    new ItemTemplateModel(){Key="Windows窗体-MDI父窗体", FullPath = $"{path}\\Windows Forms\\2052\\MDIParent\\mdiparent.cs" },
                    new ItemTemplateModel(){Key="Windows窗体-用户控件", FullPath = $"{path}\\Windows Forms\\2052\\UserControl\\usercontrol.cs" },
                    //WPF
                    new ItemTemplateModel(){Key="WPF-用户控件", FullPath = $"{path}\\WPF\\2052\\WPFUserControl\\UserControl1.xaml.cs" },
                };
            }
            foreach (ItemTemplateModel model in results)
            {
                model.OriginalContent = CopyrightHelper.GetContent(model.FullPath);
                model.CustomizeContent = CopyrightHelper.CustomizeTemplate(model.OriginalContent, _DocumentService.Get());
            }
            _ConfigService.Save(AppConstant.ITEMTEMPLATE_NAME, results);
            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public bool SaveCollection(ObservableCollection<ItemTemplateModel> settings)
        {
            return _ConfigService.Save(AppConstant.ITEMTEMPLATE_NAME, settings);
        }
        #endregion

        #region Private Method
        
        #endregion
    }
}
