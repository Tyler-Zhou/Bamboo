using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.DataCache.ServiceInterface
{     
    [ICPServiceHost]
   [ServiceContract]
   public interface IClientFrameworkInitializeService
    {   
       [OperationContract]
        List<MultiLanguageList> GetMultiLanguageList(string[] fullNames, string[] formNames,bool isEnglish);

        /// <summary>
        /// 保存多语言资源
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="fullNames"></param>
        /// <param name="formNames"></param>
        /// <param name="controlNames"></param>
        /// <param name="chineseValues"></param>
        /// <param name="englishValues"></param>
        /// <param name="updateDates"></param>
        /// <param name="languageTypes"></param>
        /// <param name="saveByID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
       [OperationContract]
        DataTable SaveMultiLanguageList(Guid[] ids,string[] fullNames,string[] formNames,string[] controlNames,string[] chineseValues,string[] englishValues,string[] chineseToolTips, string[] englishToolTips, DateTime?[] updateDates,LanguageType[] languageTypes,Guid saveByID,bool isEnglish);


        /// <summary>
        /// 删除多语言资源
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="updateDates">更新时间</param>
        /// <param name="removeID">删除人</param>
        /// <param name="isEnglish">是否英文版本</param>
       [OperationContract]
        void DeleteMultiLanguageList(Guid[] ids, DateTime?[] updateDates, Guid removeID, bool isEnglish);

        
    }
}
