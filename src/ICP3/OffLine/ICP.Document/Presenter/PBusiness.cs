#region Comment

/*
 * 
 * FileName:    PBusiness.cs
 * CreatedOn:   2014/5/14 星期三 17:35:57
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->业务数据逻辑处理类
 *      ->1.Load:窗体加载时查询所有业务数据
 *      ->2.Search_ItemClick:查询所有业务数据
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System.Collections.Generic;

namespace ICP.Document
{
    /// <summary>
    /// 业务数据逻辑处理类
    /// </summary>
    public class PBusiness : Presenter<IVBusiness>
    {
        /// <summary>
        /// 业务数据DB处理对象
        /// </summary>
        public BusinessModel Model { get; private set; }

        public PBusiness(IVBusiness view)
            : base(view)
        {
            this.Model = new BusinessModel();
        }
  
        protected override void OnViewSet()
        {

            #region 查询业务数据
            this.View.Search_ItemClick += (sender, args) =>
                    {
                        string strWhere = "";
                        if (!string.IsNullOrEmpty(args.SearchParam1))
                            strWhere = " WHERE 1=1 AND ([NO] LIKE '%" + args.SearchParam1
                            + "%' OR [Description] LIKE '%" + args.SearchParam1 + "%')";
                        else
                            strWhere = " WHERE [opd]=0 ";
                        List<BusinessInfo> busList = this.Model.GetBusinessList(strWhere);
                        this.View.FillBusinessInfo(busList);
                    }; 
            #endregion
        }   
    }
}
