using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICP.DataCache.BusinessOperation
{
    public class ClientFrameworkInitializeService : IClientFrameworkInitializeService
    {

        public ILocalBusinessCacheDataOperation LocalCacheDataOperation
        {
            get
            {
                return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>();
            }
        }

        public IFrameworkInitializeService FrameworkInitializeService
        {
            get
            {
                return ServiceClient.GetService<IFrameworkInitializeService>();
            }
        }

        public List<MultiLanguageList> GetMultiLanguageList(string[] fullNames, string[] formNames, bool isEnglish)
        {
            List<MultiLanguageList> list = null;
            
            try
            {
                DataTable dt = LocalCacheDataOperation.GetMultiLanguageList(fullNames, formNames);
                list = (from b in dt.AsEnumerable()
                                                select new MultiLanguageList
                                                {
                                                    _ID = b.Field<Guid>("ID"),
                                                    fullName = b.Field<String>("FullName"),
                                                    formName = b.Field<String>("FormName"),
                                                    controlName = b.Field<String>("ControlName"),
                                                    chineseValue = b.Field<String>("ChineseValue"),
                                                    englishValue = b.Field<String>("EnglishValue"),
                                                    chineseToolTip = b.Field<String>("ChineseToolTip"),
                                                    englishToolTip = b.Field<String>("EnglishToolTip"),
                                                    _UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                    languagetype = (LanguageType)b.Field<Byte>("Type")
                                                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public DataTable SaveMultiLanguageList(Guid[] ids, string[] fullNames, string[] formNames, string[] controlNames, string[] chineseValues, string[] englishValues, string[] chineseToolTips, string[] englishToolTips, DateTime?[] updateDates, LanguageType[] languageTypes, Guid saveByID, bool isEnglish)
        {
             DataTable dt= FrameworkInitializeService.SaveMultiLanguageList(ids, fullNames, formNames, controlNames, chineseValues, englishValues, chineseToolTips, englishToolTips, updateDates, languageTypes, saveByID, isEnglish);
             try
             {
                 LocalCacheDataOperation.SaveMultiLanguageList(dt);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dt;
        }

        public void DeleteMultiLanguageList(Guid[] ids, DateTime?[] updateDates, Guid removeID, bool isEnglish)
        {
            FrameworkInitializeService.DeleteMultiLanguageList(ids, updateDates, removeID, isEnglish);
            try
            {
                LocalCacheDataOperation.DeleteMultiLanguageList(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
     
    }
}
