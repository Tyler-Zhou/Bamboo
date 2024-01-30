using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel;
using ICP.Framework.ClientComponents.Controls;
using System.Data;
namespace ICP.Common.UI
{
    /// <summary>
    /// 船名航次下拉选择框控件
    /// </summary>
    [ToolboxItem(true)]
    public class UCVoyageLookupEdit : MultiSearchCommonBox
    {

        private bool isFirstTimeEnter = true;
        List<VoyageList> voyageList = new List<VoyageList>();
        public delegate void VoyageSaveDelegate(VoyageList saveData);
        public delegate void VoyageSetDelegate(List<VoyageList> setData,Guid? companyid);
        public VoyageSaveDelegate saved;
        public VoyageSetDelegate set;
        private Guid? companyid = null;
        public UCVoyageLookupEdit()
        {
            this.ShowRefreshButton = true;
            saved = SaveData;
            set = SetData;
            this.RefreshButtonToolTip = LocalData.IsEnglish ? "Refresh to get matching vessel/voyage" : "刷新以获取与输入相匹配的船名/航次";
            this.RefreshEventHandler += RefreshData;
            this.Enter += OnEnter;
            this.Disposed += delegate
            {
                this.RefreshEventHandler -= RefreshData;
                this.Enter -= this.OnEnter;
                this.voyageList = null;
            };

        }
        private void OnEnter(object sender, EventArgs e)
        {
            if (isFirstTimeEnter)
            {
                string tip = LocalData.IsEnglish ? "Please Input Voyage or Vessel." : "请输入船名或航次.";
                if (tip.Equals(this.EditText))
                {
                    tip = string.Empty;
                }
                else
                {
                    tip = this.EditText;
                }
                isFirstTimeEnter = false;
                GetData(tip);
            }
        }

        private void SetData(List<VoyageList> setData,Guid? companyid)
        {
            if (voyageList == null || voyageList.Count == 0)
            {
                voyageList = setData;
            }

            this.companyid = companyid;
        }

        private void RefreshData(object sender, EventArgs e)
        {
            string vesselVoyage = string.Empty;
            if (!(string.IsNullOrEmpty(this.EditText) || string.IsNullOrEmpty(this.EditText.Trim())))
            {
                vesselVoyage = this.EditText.Trim();
            }

            GetData(vesselVoyage);

            popEdit1_TextChanged(sender, e);

            VoyageListGetter getter = new VoyageListGetter();
            string log = "User:" + LocalData.UserInfo.LoginName + ",SelectText:" + vesselVoyage + ",Date:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            getter.Savelog(log);
        }
        private void GetData(string vesselAndVoyage)
        {
            if ((voyageList == null || voyageList.Count == 0) || !string.IsNullOrEmpty(vesselAndVoyage))
            {
                VoyageListGetter getter = new VoyageListGetter();
                getter.companyid = this.companyid;
                VoyageDelegate voyageDelegate = getter.GetVoyageList;
                IAsyncResult asyncResult = voyageDelegate.BeginInvoke(vesselAndVoyage, null, null);
                List<VoyageList> curent = voyageDelegate.EndInvoke(asyncResult);
                if (curent != null)
                {
                    curent.OrderByDescending(s => s.CreateDate).ToList().ForEach(r =>
                    {
                        if (voyageList.Count(j => j.ID == r.ID) == 0)
                        {
                            voyageList.Insert(0, r);
                        }
                    });
                }
            }

            Dictionary<string, string> dicColumn = new Dictionary<string, string>();
            dicColumn.Add("VesselName", LocalData.IsEnglish ? "Vessel Name" : "船名");
            dicColumn.Add("No", LocalData.IsEnglish ? "Voyage No." : "航次");
            dicColumn.Add("UNCode", LocalData.IsEnglish ? "UNCode." : "UNCode");
            InitSource<VoyageList>(voyageList, dicColumn, "VesselAndNo", "ID");
            SetWidth(400);
        }

        private void SaveData(VoyageList saveData)
        {
            Guid selectid = (Guid)this.EditValue;
            VoyageList selectvoyage = voyageList.Find(r => r.ID == selectid);
        }


        /// <summary>
        ///数据过滤实现
        ///船名航次的下拉选过滤：需要匹配 船名/航次 形式
        ///仅从DisplayMember列过滤，因为此列的值形式为：船名/航次
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dtFiltered"></param>
        /// <param name="columnInfos"></param>
        /// <param name="filterText"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        protected override void DoFilterDataSource(DataTable dtSource, DataTable dtFiltered, Dictionary<string, string> columnInfos, string filterText, string displayMember, string valueMember)
        {
            if (dtSource == null || dtSource.Rows.Count <= 0 || filterText.Length < 3)
            {
                return;
            }

            string[] filters = filterText.Split('/');

            foreach (DataRow dr in dtSource.Rows)
            {

                string stringValue = dr[displayMember] == null ? string.Empty : dr[displayMember].ToString();

                if (!string.IsNullOrEmpty(stringValue))
                {
                    if (filters.Length == 1)
                    {
                        if (stringValue.ToUpper().Contains(filterText))
                        {
                            DataRow newRow = dtFiltered.NewRow();

                            foreach (DataColumn dataColumn in dtFiltered.Columns)
                            {
                                newRow[dataColumn.ColumnName] = dr[dataColumn.ColumnName];
                            }
                            newRow["Index"] = stringValue.Length;
                            dtFiltered.Rows.Add(newRow);

                        }
                        else if (ICP.Framework.ClientComponents.Controls.Utility.IsContainsChinese(stringValue)
                            && ICP.Framework.ClientComponents.Controls.Utility.GetPinyinCode(stringValue.ToUpper()).Contains(filterText))
                        {
                            DataRow newRow = dtFiltered.NewRow();
                            foreach (DataColumn dataColumn in dtFiltered.Columns)
                            {
                                newRow[dataColumn.ColumnName] = dr[dataColumn.ColumnName];
                            }
                            newRow["Index"] = stringValue.Length;
                            dtFiltered.Rows.Add(newRow);

                        }
                    }
                    else
                    {
                        if (stringValue.ToUpper().Contains(filters[0]) && stringValue.ToUpper().Contains(filters[1]))
                        {
                            DataRow newRow = dtFiltered.NewRow();

                            foreach (DataColumn dataColumn in dtFiltered.Columns)
                            {
                                newRow[dataColumn.ColumnName] = dr[dataColumn.ColumnName];
                            }
                            newRow["Index"] = stringValue.Length;
                            dtFiltered.Rows.Add(newRow);

                        }
                        else if (ICP.Framework.ClientComponents.Controls.Utility.IsContainsChinese(stringValue)
                            && ICP.Framework.ClientComponents.Controls.Utility.GetPinyinCode(stringValue.ToUpper()).Contains(filterText))
                        {
                            DataRow newRow = dtFiltered.NewRow();
                            foreach (DataColumn dataColumn in dtFiltered.Columns)
                            {
                                newRow[dataColumn.ColumnName] = dr[dataColumn.ColumnName];
                            }
                            newRow["Index"] = stringValue.Length;
                            dtFiltered.Rows.Add(newRow);

                        }
                    }

                }
            }


        }

    }

    public delegate List<VoyageList> VoyageDelegate(string vesselAndVoyage);

    public class VoyageListComparer : IEqualityComparer<VoyageList>
    {

        #region IEqualityComparer<VoyageList> 成员

        public bool Equals(VoyageList x, VoyageList y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(VoyageList obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
    public sealed class VoyageListGetter
    {
        private ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public List<VoyageList> GetVoyageList(string vesselAndVoyage)
        {
            return GetData(vesselAndVoyage);

        }

        public void Savelog(string log)
        {
            TransportFoundationService.SaveLogInfo(log);
        }

        public Guid? companyid;

        private List<VoyageList> GetData(string argument)
        {
            //如果未输入任何值 则获取最近一个月内的船名/航次
            if (string.IsNullOrEmpty(argument))
            {
                List<VoyageList> temp = GetLastestVoyageList();
                // temp.Insert(0, GetEmptyVoyage());
                return temp;
            }
            //如果按照输入加载数据 则加载查找到的100条数据和最近一个月的船名航次数据
            else
            {
                string[] parameters = CommonUtility.ProcessParameter(argument);
                List<VoyageList> temp = TransportFoundationService.GetVoyageList(null, companyid, parameters[0], parameters[1], null, null, null, true, 1000);
                //List<VoyageList> lastestVoyage = GetLastestVoyageList();
                //lastestVoyage = lastestVoyage.Except<VoyageList>(temp, new VoyageListComparer()).ToList();
                //List<VoyageList> result = temp.Concat<VoyageList>(lastestVoyage).ToList();
                //result.Insert(0, GetEmptyVoyage());
                return temp;
            }
        }
        /// <summary>
        /// 获取最近一个月的船名航次信息
        /// </summary>
        /// <returns></returns>
        private List<VoyageList> GetLastestVoyageList()
        {
            DateTime dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            return TransportFoundationService.GetVoyageList(null, companyid, string.Empty, string.Empty, dt.AddMonths(-1), dt, null, true, 0);
        }
        private VoyageList GetEmptyVoyage()
        {
            //插入空行
            VoyageList voyage = new VoyageList();
            voyage.ID = Guid.Empty;
            voyage.No = voyage.VesselName = string.Empty;
            voyage.VesselAndNo = LocalData.IsEnglish ? "Please Input Voyage or Vessel." : "请输入船名或航次.";
            return voyage;
        }


    }
}

