/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 14:41:56
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using Copyright.Client.Common;
using Copyright.Client.Models;
using System.Collections.ObjectModel;

namespace Copyright.Client.Services
{
    /// <summary>
    /// Template Service
    /// </summary>
    public class TemplateService
    {
        #region ��Ա(Member)
        #endregion

        #region ����(Services)
        /// <summary>
        /// Json Service
        /// </summary>
        JsonService _JsonService = null;
        #endregion

        #region ���캯��(Constructor)
        /// <summary>
        /// Template Service
        /// </summary>
        public TemplateService()
        {
            _JsonService = new JsonService();
        }
        #endregion

        #region ����(Method)
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="collection">����</param>
        /// <returns></returns>
        public bool Save(ObservableCollection<BaseDataModel> collection)
        {
            return _JsonService.Save(AppConstant.TEMPLATE_NAME, collection);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<BaseDataModel> Get()
        {
            ObservableCollection<BaseDataModel> result = _JsonService.Get<ObservableCollection<BaseDataModel>>(AppConstant.TEMPLATE_NAME);
            if(result==null)
            {
                result = new ObservableCollection<BaseDataModel>()
                {
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.HEADER_NAME,Description = @"/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:$time$
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.MEMBER_NAME,Description = "��Ա(Member)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.SERVICE_NAME,Description = "����(Service)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.COMMAND_NAME,Description = "����(Command)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.CONSTRUCTOR_NAME,Description = "���캯��(Constructor)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.EVENT_NAME,Description = "�¼�(Event)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.PUBLICMETHOD_NAME,Description = "���÷���(Public Method)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.PRIVATEMETHOD_NAME,Description = "˽�з���(Private Method)" },
                };
                Save(result);
            }
            return result;
        }
        #endregion
    }
}
