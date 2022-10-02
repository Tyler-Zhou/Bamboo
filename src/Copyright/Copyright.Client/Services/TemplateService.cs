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
        #region 成员(Member)
        #endregion

        #region 服务(Services)
        /// <summary>
        /// Json Service
        /// </summary>
        JsonService _JsonService = null;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// Template Service
        /// </summary>
        public TemplateService()
        {
            _JsonService = new JsonService();
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="collection">集合</param>
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
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.MEMBER_NAME,Description = "成员(Member)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.SERVICE_NAME,Description = "服务(Service)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.COMMAND_NAME,Description = "命令(Command)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.CONSTRUCTOR_NAME,Description = "构造函数(Constructor)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.EVENT_NAME,Description = "事件(Event)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.PUBLICMETHOD_NAME,Description = "公用方法(Public Method)" },
                    new BaseDataModel(){IsSelected=true,Name=AppConstant.PRIVATEMETHOD_NAME,Description = "私有方法(Private Method)" },
                };
                Save(result);
            }
            return result;
        }
        #endregion
    }
}
