﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Common;
using System.ComponentModel;
using ICP.Sys.ServiceInterface;
using DevExpress.XtraEditors.Repository;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface;

namespace ICP.TMS.UI
{
    public class Utility
    {

        /// <summary>
        /// 确保用户信息的DefaultCompany有值
        /// 如果没有就抛出异常
        /// </summary>
        /// <param name="userService"></param>
        static public void EnsureDefaultCompanyExists(ICP.Sys.ServiceInterface.IUserService userService)
        {
            if (string.IsNullOrEmpty(LocalData.UserInfo.DefaultCompanyName))
            {
                List<OrganizationList> list = userService.GetUserCompanyList(LocalData.UserInfo.LoginID,
                    OrganizationType.Company);

                if (list.Count > 0)
                {
                    LocalData.UserInfo.DefaultCompanyID = list[0].ID;
                    LocalData.UserInfo.DefaultCompanyName = LocalData.IsEnglish ? list[0].EShortName : list[0].CShortName;
                }
                else
                {
                    throw new Exception("Please contact administrator to assign a company for you!");
                }
            }
        }

        /// <summary>
        /// 确保用户信息的DefaultCompany有值
        /// 如果没有就抛出异常
        /// </summary>
        /// <param name="userService"></param>
        static public void EnsureDefaultDepartmentExists(ICP.Sys.ServiceInterface.IUserService userService)
        {
            if (string.IsNullOrEmpty(LocalData.UserInfo.DefaultDepartmentName))
            {
                List<OrganizationList> list = userService.GetUserCompanyList(LocalData.UserInfo.LoginID,
                    OrganizationType.Department);

                if (list.Count > 0)
                {
                    LocalData.UserInfo.DefaultDepartmentID = list[0].ID;
                    LocalData.UserInfo.DefaultDepartmentName = LocalData.IsEnglish ? list[0].EShortName : list[0].CShortName;
                }
                else
                {
                    throw new Exception("Please contact administrator to assigne a department for you!");
                }
            }
        }

        /// <summary>
        /// 弹出一个是提示"新增的数据未保存,是否保存"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByNew(string titel)
        {
            if (string.IsNullOrEmpty(titel)) titel = LocalData.IsEnglish ? "Tip" : "提示";

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "新增的数据未保存,是否保存?"
                                , titel
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        /// <summary>
        /// 弹出一个是提示"数据有更改,是否保存数据"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByUpdated(string titel)
        {
            if (string.IsNullOrEmpty(titel)) titel = LocalData.IsEnglish ? "Tip" : "提示";

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The data was changed,Sure save?" : "数据有更改是否保存?"
                                , titel
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        #region 处理对象

        /// <summary>
        /// 深拷贝,通过序列化对象再反序列化得出新的对象
        /// </summary>
        public static T Clone<T>(T t)
        {
            T clone;
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();

            System.Xml.XmlWriter w = doc.CreateWriter();
            //w.Settings.Encoding = System.Text.UnicodeEncoding.Unicode;
            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            s.Serialize(w, t);
            w.Flush();
            w.Close();
            clone = (T)s.Deserialize(doc.CreateReader());

            return clone;
        }

        #region 对象属性值的Copy
        /// <summary>
        /// 把source对象的值拷贝到一个类型为targetType的对象,然后返回产生的对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="targetType">产生新的对象的类型</param>
        /// <returns>返回新的对象</returns>
        public static object CloneValue(object source, Type targetType)
        {
            object targe = Activator.CreateInstance(targetType);
            Type sourceType = source.GetType();
            PropertyInfo[] properties = sourceType.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                PropertyInfo tp = targetType.GetProperty(p.Name);

                if (tp == null) continue;

                if (p.PropertyType.IsValueType
                    || p.PropertyType.Name == "String")//值类型
                {
                    tp.SetValue(targe, p.GetValue(source, null), null);
                }
                else if (p.PropertyType.IsGenericType
                    && p.PropertyType.GetInterface(typeof(System.Collections.IList).FullName) != null)//如果是泛集合
                {
                    //不处理

                    object pValue = p.GetValue(source, null);
                    foreach (object obj in (pValue as System.Collections.IEnumerable))
                    {
                        //如果未实例化集合属性则实例化它
                        if (tp.GetValue(targe, null) == null) tp.SetValue(targe, Activator.CreateInstance(tp.PropertyType), null);
                        //获取Item属性的类型
                        string subitemTypeName = System.Text.RegularExpressions.Regex.Match(tp.PropertyType.FullName, @"\[\[(?<val>.*?)\]\]").Groups["val"].Value;
                        Type subitemType = Type.GetType(subitemTypeName);
                        object temsubObj = null;
                        //子集属性类型为源类型-防止递归死掉
                        if (obj.GetType().FullName != sourceType.FullName)
                        {
                            temsubObj = CloneValue(obj, subitemType);
                        }
                        else
                        {
                            temsubObj = source;
                        }
                        System.Collections.IList temTarget = tp.GetValue(targe, null) as System.Collections.IList;
                        temTarget.Add(temsubObj);
                    }
                }
                else if (p.PropertyType.IsClass)
                {
                    //子集属性类型为源类型-防止递归死掉
                    if (p.PropertyType.FullName != sourceType.FullName)
                    {
                        object temSource = p.GetValue(source, null);
                        tp.SetValue(targe, CloneValue(temSource, tp.PropertyType), null);
                    }
                    else
                    {
                        tp.SetValue(targe, source, null);
                    }
                }
            }
            return targe;
        }

        /// <summary>
        /// 把source对象的值拷贝到一个类型为targetType的对象,然后返回产生的对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="targetType">需要拷贝到的对象(必须是targetType类型)</param>
        /// <param name="targetType">产生新的对象的类型</param>
        /// <returns>返回新的对象</returns>
        public static void CopyToValue(object source, object targe, Type targetType)
        {
            Type sourceType = source.GetType();
            PropertyInfo[] properties = sourceType.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                PropertyInfo tp = targetType.GetProperty(p.Name);

                if (tp == null) continue;

                if (p.PropertyType.IsValueType
                    || p.PropertyType.Name == "String")//值类型
                {
                    try
                    {
                        tp.SetValue(targe, p.GetValue(source, null), null);
                    }
                    catch
                    {
                    }
                }
                else if (p.PropertyType.IsGenericType
                    && p.PropertyType.GetInterface(typeof(System.Collections.IList).FullName) != null)//如果是泛集合
                {
                    //不处理


                    try
                    {
                        object pValue = p.GetValue(source, null);
                        foreach (object obj in (pValue as System.Collections.IEnumerable))
                        {
                            //如果未实例化集合属性则实例化它
                            if (tp.GetValue(targe, null) == null) tp.SetValue(targe, Activator.CreateInstance(tp.PropertyType), null);
                            //获取Item属性的类型
                            string subitemTypeName = System.Text.RegularExpressions.Regex.Match(tp.PropertyType.FullName, @"\[\[(?<val>.*?)\]\]").Groups["val"].Value;
                            Type subitemType = Type.GetType(subitemTypeName);
                            object temsubObj = null;
                            //子集属性类型为源类型-防止递归死掉
                            if (obj.GetType().FullName != sourceType.FullName)
                            {
                                temsubObj = CloneValue(obj, subitemType);
                            }
                            else
                            {
                                temsubObj = source;
                            }
                            System.Collections.IList temTarget = tp.GetValue(targe, null) as System.Collections.IList;
                            temTarget.Add(temsubObj);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (p.PropertyType.IsClass)
                {
                    try
                    {
                        //子集属性类型为源类型-防止递归死掉
                        if (p.PropertyType.FullName != sourceType.FullName)
                        {
                            object temSource = p.GetValue(source, null);
                            tp.SetValue(targe, CloneValue(temSource, tp.PropertyType), null);
                        }
                        else
                        {
                            tp.SetValue(targe, source, null);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            // return targe;
        }
        #endregion

        /// <summary>
        /// 获取对象某一属性值的ToString,为空的引用类型返回sting.Empty
        /// </summary>
        public static string GetObjectPropertyStringValue<T>(T t, string propertyname)
        {
            if (string.IsNullOrEmpty(propertyname)) return string.Empty;

            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }

        /// <summary>
        /// 获取对象某一属性值的ToString,为空的引用类型返回sting.Empty
        /// </summary>
        public static string GetObjectPropertyStringValue(Type type, object t, string propertyname)
        {
            if (string.IsNullOrEmpty(propertyname)) return string.Empty;

            if (t.GetType() != type) return string.Empty;
            PropertyInfo property = type.GetProperty(propertyname);
            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);
            if (o == null) return string.Empty;
            return o.ToString();
        }

        /// <summary>
        /// 比效两个对象是否相等,返回不相等的属性名
        /// </summary>
        public static string GetObjectUpdatePropertys<T>(T t1, T t2)
        {
            return GetObjectUpdatePropertys<T>(t1, t2, null);
        }

        /// <summary>
        /// 比效两个对象是否相等,返回不相等的属性名,ignores参数为不需要检测的属性
        /// </summary>
        public static string GetObjectUpdatePropertys<T>(T t1, T t2, List<string> ignores)
        {
            string info = string.Empty;

            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    //if (property.PropertyType.FullName.Contains("Guid")) continue;
                    if (ignores != null && ignores.Count > 0 && ignores.Contains(property.Name)) continue;
                    object o1 = property.GetValue(t1, null);
                    object o2 = property.GetValue(t2, null);
                    string str1 = o1 == null ? string.Empty : o1.ToString();
                    string str2 = o2 == null ? string.Empty : o2.ToString();

                    if (str1 != str2) info += property.Name + ",";
                }
            }

            if (!string.IsNullOrEmpty(info)) info = info.Substring(0, info.Length - 1);

            return info;
        }


        /// <summary>
        /// 比效两个对象是否相等
        /// </summary>
        public static bool CheckObjectIsUpdate<T>(T t1, T t2)
        {
            string info = string.Empty;

            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    object o1 = property.GetValue(t1, null);
                    object o2 = property.GetValue(t2, null);
                    string str1 = o1 == null ? string.Empty : o1.ToString();
                    string str2 = o2 == null ? string.Empty : o2.ToString();

                    if (str1 != str2) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 比效两个对象是否相等,ignores参数为不需要检测的属性
        /// </summary>
        public static bool CheckObjectIsUpdate<T>(T t1, T t2, List<string> ignores)
        {
            string info = string.Empty;

            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    if (ignores != null && ignores.Count > 0 && ignores.Contains(property.Name)) continue;

                    object o1 = property.GetValue(t1, null);
                    object o2 = property.GetValue(t2, null);
                    string str1 = o1 == null ? string.Empty : o1.ToString();
                    string str2 = o2 == null ? string.Empty : o2.ToString();

                    if (str1 != str2) return true;
                }
            }

            return false;
        }

        #endregion

        #region 处理值类型
        public static bool GuidIsNullOrEmpty(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty) return true;
            else return false;
        }

        public static bool GuidIsNullOrEmpty(object id)
        {
            try
            {
                if (id == null)
                {
                    return true;
                }
                if ((Guid)id == Guid.Empty)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return true;
            }
        }

        public static DateTime GetEndDate(DateTime date)
        {
            string dateStr = date.ToShortDateString();
            dateStr += " 23:59:59";
            return DateTime.Parse(dateStr);
        }

        /// <summary>
        /// 生成（年月日时分：201107081430）格式的字符串
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>字符串</returns>
        public static string GetDateTimeString(DateTime dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(dt.Year.ToString());
            builder.Append(dt.Month >= 10 ? dt.Month.ToString() : ("0" + dt.Month.ToString()));
            builder.Append(dt.Day >= 10 ? dt.Day.ToString() : ("0" + dt.Day.ToString()));
            builder.Append(dt.Hour >= 10 ? dt.Hour.ToString() : ("0" + dt.Hour.ToString()));
            builder.Append(dt.Minute >= 10 ? dt.Minute.ToString() : ("0" + dt.Minute.ToString()));
            return builder.ToString();
        }

        #endregion

        #region FinderHelper

        /*根据信息转换为object[]*/
        public static object[] GetSingleSearchResult<T>(T data, string[] returnFields)
        {
            object[] result = new object[returnFields.Length];
            for (int i = 0; i < returnFields.Length; i++)
            {
                result[i] = Utility.GetObjectPropertyStringValue<T>(data, returnFields[i]);
            }

            return result;
        }

        /*根据信息转换为object[]*/
        public static object[] GetMultiSearchResult<T>(List<T> datas, string[] returnFields)
        {
            object[] result = new object[datas.Count];
            for (int i = 0; i < datas.Count; i++)
            {
                result[i] = Utility.GetSingleSearchResult<T>(datas[i], returnFields);
            }
            return result;
        }

        #endregion

        #region ShowDialog
        /// <summary>
        /// 弹出一个提示"客户是无效的,是否继续"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool PopCustomerIsInvalid()
        {
            string message = LocalData.IsEnglish ? "The customer you selected is not valid. Continue anyway?" : "客户被标记为“无效”，是否继续？";

            return ShowResultMessage(message);
        }

        /// <summary>
        /// 弹出一个提示"客户尚未通过审核,是否保存数据"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool PopCustomerUnApproved()
        {
            string message = LocalData.IsEnglish ? "The customer you selected has not been approved. Continue anyway?" : "客户尚未通过审核，是否继续？";

            return ShowResultMessage(message);
        }

        /// <summary>
        /// 显示一个提示,用户选择是/否
        /// </summary>
        /// <returns></returns>
        public static bool ShowResultMessage(string message)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                               LocalData.IsEnglish ? "Tip" : "提示",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 弹出一个对话框
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessage(string message)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                              LocalData.IsEnglish ? "Tip" : "提示");
        }

        /// <summary>
        /// 弹出一个是提示"是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentData()
        {


            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Srue Delete Current Data?" : "是否删除当前数据?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 弹出一个是提示"数据有更改,是否保存数据"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByUpdated()
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The data was changed,Sure save?" : "数据有更改是否保存?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        /// <summary>
        /// 弹出一个是提示"新增的数据未保存,是否保存"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByNew()
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "新增的数据未保存,是否保存?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }


        /// <summary>
        /// 功能描述：打开一个模态窗口,返回该模态窗口的DialogReasult的值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static DialogResult ShowDialog(System.Windows.Forms.Control control, string title)
        {
            return Utility.ShowDialog(control, title, System.Windows.Forms.FormBorderStyle.FixedSingle);
        }

        /// <summary>
        /// 功能描述：打开一个模态窗口,返回该模态窗口的DialogReasult的值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static DialogResult ShowDialog(System.Windows.Forms.Control control, string title, System.Windows.Forms.FormBorderStyle formBorderStyle)
        {
            DevExpress.XtraEditors.XtraForm form = new DevExpress.XtraEditors.XtraForm();
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Text = title;
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            if (control.Height >= System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            {
                form.Size = new System.Drawing.Size(control.Size.Width + 10, control.Size.Height + 30);
            }
            else
            {
                form.Size = new System.Drawing.Size(control.Size.Width, control.Size.Height + 30);
            }
            form.FormBorderStyle = formBorderStyle;
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            return form.ShowDialog();
        }

        #endregion

        #region 中文字支持

        /// <summary>
        /// 把字符串转换为拼音码
        /// </summary>
        public static string GetPinyinCode(string str)
        {
            string pyCode = "";
            string[] PY = new string[26];
            bool IsFound;
            //A
            PY[0] = "啊阿呵吖嗄腌锕錒厑爱矮挨哎碍癌艾唉哀蔼隘埃皑呆嗌嫒瑷暧捱砹嗳锿霭乂伌僾儗凒叆呝啀嘊噯堨塧壒娭娾嬡愛懓懝敱敳昹曖欸毐溰溾濭烠焥璦皚皧瞹硋磑絠薆藹譪躷鎄鑀阸靄靉餲馤騃鱫鴱按安暗岸俺案鞍氨胺庵揞犴铵桉谙鹌埯黯侒儑匼厈咹唵啽垵垾堓婩媕峖晻洝玵痷盦盫罯腤荌菴萻葊蓭裺誝諳豻貋銨錌隌雸鞌韽頞馣鮟鴳鵪鶕昂肮盎卬岇昻枊醃醠骯袄凹傲奥熬懊敖翱澳拗媪廒骜嗷坳遨聱螯獒鏊鳌鏖岙厫嗸噢嚻垇墺奡奧媼嫯岰嶅嶴慠扷抝摮擙柪梎泑滶澚熝爊獓璈眑磝磽礉翶翺芺蔜蝹襖謷謸軪鏕镺隞驁鰲鴁鴢鷔鼇";
            //B
            PY[1] = "把八吧爸拔罢跋巴芭扒坝霸叭靶笆疤耙捌粑茇岜鲅钯魃菝灞仈伯叐哱哵坺垻墢壩夿妭峇弝抜抪捭朳柭欛炦犮玐癹皅矲笩紦罷羓胈萆蚆覇詙豝跁軷釛釟鈀颰魞鮁鮊鲃鲌鼥百白摆败柏拜佰稗呗掰唄庍拝挀擺敗栢猈竡粨粺絔薜薭襬贁鞁鞴韛兡瓸半办班般拌搬版斑板伴扳扮瓣颁绊癍坂钣舨阪瘢並坢埿姅岅彬怑搫攽斒昄朌柈湴瓪秚籓粄絆肦蝂螁螌褩覂豳跘辦辧辨辩辬辯鈑鉡闆靽頒魬鳻帮棒绑磅镑邦榜蚌傍梆膀谤浜蒡嗙垹埲塝嫎峀幇幚幫徬挷捠搒棓牓玤硥稖綁縍艕蚄蛖蜯螃謗邫鎊鞤騯髈包抱报饱保暴薄宝爆剥豹刨雹褒堡苞胞鲍龅孢煲褓鸨趵葆佨儤剝勹勽嘐嚗堢報媬嫑寚寳寶忁怉曓枹珤砲窇笣簿緥菢蕔藵虣蚫袌裒裦襃賲鉋鑤铇闁靌靤飹飽駂骲髱鮑鳵鴇齙萡被北倍杯背悲备碑卑贝辈钡焙狈惫臂褙悖蓓鹎鐾邶孛陂碚俻俾偝偹備僃喺埤愂憊揹昁杮柸桮梖椑波牬犕狽珼痺盃禙箃糒苝萯藣蛽誖諀貝軰輩鄁鉳鋇錍鐴骳鵯本奔苯笨夯锛贲畚坌倴喯夲奙捹撪桳楍泍渀燌犇獖翉蟦賁輽逩錛鐼蹦绷甭崩迸泵甏嘣伻傰唪埄堋塴奟嵭漨琣琫痭祊絣綳繃菶跰逬錋鏰镚閍鞛比笔闭鼻碧必避逼毕彼鄙壁蓖币弊蔽毙庇敝陛毖痹泌秕荸芘匕裨畀嬖狴筚箅篦舭荜襞庳铋跸吡愎滗濞璧哔髀弼妣婢仳佊佖偪匂咇啚嗶坒堛夶奰妼娝媲嬶屄崥幣幤庀廦弻彃怭怶悂愊斃旇朼枈柀柲梐楅檗殍毴沘湢滭潷煏熚獘獙珌畁畢疕疪皀皕瞥禆稫笓筆箄箆篳粃粊紕紴綼縪繴纰罼翍聛肶肸胇脾腗腷苾萞蓽蘗蜌螕袐襅襣觱詖诐豍貏貱費贔赑跛踾蹕躃躄邲鄨鄪鈚鉍鎞鏎閇閈閉閟闬陴鞞鞸韠飶饆馝馥駜驆髲魓魮鮅鮩鰏鲾鵖鷝鷩鸊鼊边变便遍编扁贬鞭卞辫忭砭匾汴碥蝙褊鳊笾苄窆弁髟缏煸変峅徧惼抃揙昪汳炞牑猵獱甂稨稹箯籩糄編緶臱艑萹藊覍覵變貶辡辮邉邊釆鍽鞕鯾鯿鴘鶣表标彪膘杓婊飑飙鳔瘭飚镳裱骠镖俵僄儦墂幖徱摽標檦淲滮漂瀌灬熛爂猋穮篻脿膔臕蔈藨褾諘謤贆錶鏢鑣颮颷飆飇飈飊驃驫骉鰾麃别憋鳖瘪蹩別彆徶撆撇猰癟穪莂虌蛂蟞襒鱉鼈龞宾濒摈斌滨膑殡缤髌傧槟鬓镔玢儐擯椕殯氞汃濱濵瀕瑸璸砏繽臏虨蠙訜豩賓賔贇赟邠鑌霦顮髕髩鬂鬢并病兵冰丙饼秉柄炳摒禀邴仌併倂偋傡冫垪寎幷庰怲抦掤昞昺枋栟栤梹棅檳氷燷燹琕癛癝眪稟窉竝綆绠苪蛃誁鈵鉼鋲陃靐鞆餅餠拨播泊博驳玻勃菠钵搏脖帛舶渤铂箔膊魄卜礴亳鹁踣啵蕃簸钹饽擘仢侼僠僰噃壆孹嶓帗彴愽懪挬撥桲榑檘欂殕泼浡淿湐潘煿牔犦犻狛猼瓝瓟癶皪盋砵碆磻礡秡穛箥簙糪缽肑胉艊艴苩葧蒲蔔蔢蘖蚾袚袯袹襎襏襮譒豰蹳郣鈸鉑鉢鋍鎛鑮镈餑餺馎馛馞駁駮驋髆髉鱍鵓不步补布部捕哺埠怖埔瓿逋晡钸钚醭卟佈勏吥咘埗婄尃峬庯廍悑拊捗擈柨歨歩獛秿箁篰荹蔀補誧踄輹轐郶鈈鈽陠餔餢鯆鳪鵏鸔";
            //C
            PY[2] = "厂擦拆礤嚓傪囃攃橴磣礸蔡遪才菜采材财裁猜踩睬彩倸偲啋埰婇寀戝扐採揌棌綵縩纔財跴乲蚕残掺参惨惭餐灿骖璨孱黪粲儏參叄叅喰嘇噆囋嬠嬱嵾慘慙慚憯戔摲朁殘湌澯燦爘穇篸薒蝅蠶蠺謲鏒飡飱驂鯵鰺鲹黲藏仓沧舱苍伧仺倉傖凔匨嵢欌滄濸獊瑲篬罉臧艙蒼蔵螥賶鑶鶬鸧草操曹槽糙嘈艚螬漕傮屮嶆愺慅慒懆撡曺肏艸艹蓸褿襙鄵鏪鐰騲鼜册侧策测厕恻側冊厠墄嫧帻幘廁惻憡拺敇測畟笧筞筴箣簎粣荝萗萴蓛齰岑涔梣汵硶笒膥曾层蹭噌增層嶒橧竲繒缯驓硛硳岾猠乽查插叉茶差岔搽察茬碴诧槎镲衩汊馇檫姹杈锸仛侘偛嗏垞奼岎嵖扠扱挿捈揷摖査梌猹疀秅紁肞臿艖芆苴荖荼褨訍詧詫蹅釵銟鍤鎈鑔钗靫餷柴豺瘥虿侪儕勑喍囆搓犲祡茈茝蔕蠆袃齜龇产缠搀阐颤铲谗蝉馋觇婵蒇谄冁廛蟾羼镡忏潺禅骣躔澶丳亶佔僝僤儃儳刬剗剷劖単啴嘽嚵囅團墠壥嬋嬗嵼嶃嶄巉幝幨憚懴懺摌摻撣攙斺旵梴棎榐欃毚浐湹滻潹瀍瀺灛煘燀獑產産硟磛禪簅緂緾繟繵纏纒脠艬苫蕆螹蟬蟺袩裣裧襌襜襝覘誗諂譂讇讒讖谶蹍辿鄽酁醦鉆鋋鋓鏟鑱镵閳闡韂顫饞长唱常场尝肠畅昌敞倡偿猖鲳氅菖惝嫦徜鬯阊怅伥昶苌娼仧倀倘僘償儻兏厰嘗嚐場塲廠悵晿暢棖椙淌淐焻玚琩瑒瑺瓺甞畼腸膓萇蟐裮誯鋹鋿錩鏛锠長镸閶闛韔鯧鱨鲿鼚朝抄超吵潮巢炒嘲绰钞怊焯耖晁仦仯劋勦巐巣弨摷槱樔欩漅焣煼牊眧窲粆綽繛绉绍罺觘訬謅謿趠轈鄛鈔麨鼂鼌车撤扯掣彻尺澈坼砗伡俥偖勶呫唓喢夛奲徹揊摰撦斥池烢烲焎爡瞮硨硩聅莗蛼詀謵車迠頙趁称辰臣尘晨沉陈衬橙忱郴榇抻谌碜宸龀嗔琛侲儭嚫塡塵墋夦愖捵揨敐曟桭棽樄櫬湛瀋烥煁疢疹瘎瘨眈瞋稱綝縝肜胂茞莀莐蔯薼螴襯訦諃諶謓賝贂趂趻跈踸軙迧鈂鍖闖闯陳霃鷐麎齓齔成乘撑城程呈诚秤惩逞骋澄承塍柽埕铖铛酲晟裎枨蛏丞瞠乗侱偁僜埩堘塖娍宬峸嵊庱徎徵悜憆憕懲挰掁摚摤撐撜朾棦椉橕檉檙氶泟洆浧浾溗澂瀓瀞爯牚珵珹琤畻睈矃碀窚竀筬絾緽脀脭荿蟶觕誠赪赬踜郕郢鋮鏳鏿阷靗頳饓騁騬鯎吃迟翅痴赤齿耻持侈弛驰炽踟坻茌墀饬媸豉褫敕哧瘛蚩啻鸱眵螭篪魑叱彳笞嗤傺佁侙俿剟勅卙卶叺呎呬呮喜喫噄噭坘垑奓妛岻彨彲徲恜恥慗慸憏懘扡抶拕拸捇搋摛摴攡杘柅樆欼歗歯汖沶治泜淔湁漦灻烾熾狋瓻痓痸瘈癡眙瞝竾筂箈箎粚糦絺翄翤翨耛肔胝胣胵脪腟芪荎莉菭蚇蚳蝭袲袳裭訵誀誃誺謘謻貾赿趍趐趩跅跢跮踅踶軧迡迣遅遟遫遲邌鉓鉹銐鍉雴饎馳騺驪鳷鴟鵄鵣鶒鶗鶙鷘麶黐齒齝冲重虫充宠崇艟忡舂铳憧茺偅傭僮喠嘃埫寵崈徸憃揰摏樁沖浺漴潼爞珫痋祌緟罿翀蝩蟲衝褈蹖蹱酮銃隀抽愁臭仇丑稠绸酬筹踌畴瞅惆俦帱瘳雠丒侴偢儔吜嚋婤媿嬦幬懤掫揄搊擣杽栦椆檮殠溴燽牰犨犫畤疇皗盩眣矁篘籌紬絒綢臰菗薵裯詶譸讎讐诪跾躊遚酧醔醜醻鈕钮雔魗鮘鯈鲋出处初锄除触橱楚础储畜滁矗搐躇厨雏楮杵刍怵绌亍憷蹰黜蜍樗俶傗儊儲処助嘼埱媰岀幮廚慉懨拀摢敊斶椘榋槒橻檚櫉櫖櫥欪歜滀濋炪犓珿琡璴礎禇竌竐篨絀耝耡臅芻菆蒢蒭蓫蕏藸處蟵蠩褚觸諔諸豖豠貙趎踀躕鄐鉏鋤閦雛鶵鸀齣齭齼撮欻歘揣膪啜嘬踹腄膗穿船传串川喘椽氚遄钏舡舛巛傳僢剶圌堾惴掾暷歂汌猭玔瑏甎篅膞舩荈賗踳輲釧镩鶨窗床创疮怆傸刅刱剏剙創噇囪囱愴戧摐朣橦漺牀牎牕瘡磢窓窻膧葱蔥吹垂炊锤捶椎槌棰陲倕埀惙搥桘箠菙郵錘鎚顀鬌魋龡春唇纯蠢醇淳椿蝽莼鹑偆媋惷旾暙朐杶楯槆橁櫄沌浱湻滣漘犉瑃睶箺純肫脣芚萅萶蒓蓴賰輇輴辁醕錞陙鯙鰆鶉鶞戳踔龊辍促吷嚽娕娖婥婼孎擉斫歠涰淖磭箹簇蔟輟辵辶逴酫醛鋜鏃鑡镞齪齱次此词瓷慈雌磁辞刺茨伺疵赐兹呲鹚祠糍佌佽偨刾呰啙垐堲姕嬨嵯嵳庛措朿柌栜栨泚滋澬濨玼珁甆皉礠絘縒胔茦茲荠莿萕薋薺蚝蛓螅螆蠀詞賜赼趀趑跐辝辤辭鈶飺餈骴髊鮆鴜鶿鷀齹嗭从丛匆聪琮枞淙璁骢苁偬叢婃孮従徖從忩怱悤悰憁暰棇楤樅樬樷欉漎漗潀潈潨灇焧熜燪爜瑽瞛碂篵総緫縦縱總繱纵聡聦聰茐蓯藂蟌誴謥賨賩鏦騘驄凑楱辏腠湊薮藪趋輳粗醋徂猝蹙酢殂蹴卆噈媨怚憱捽瘄瘯皻縬脨蔍蔖誎趗趥踓踤踧蹵錯错顣麁麄麆麤鼀窜蹿篡攒汆爨撺僔巑攅攛攢昕櫕欑殩濽灒熶穳窾竄篹簒襸躥鋑鑹催脆摧翠崔淬瘁粹璀啐悴萃毳榱乼伜倅凗啛墔崒崪嶉忰慛椊槯漼濢焠熣獕琗疩皠磪竁粋紣綷縗繀缞翆脃脺膬膵臎襊趡鏙隹顇村寸存蹲忖皴侟刌吋墫拵洊浚澊竴籿踆邨挫磋厝鹾脞痤蹉锉矬剉剒夎庴棤澨營瑳睉莝莡蒫蓌虘諎躜躦逪遳酂酇醝銼鹺宷";
            //D
            PY[3] = "单單掸多盯虰鐺黨哆柢踱大答达打搭瘩笪耷哒褡疸怛靼妲嗒鞑亣剳匒呾咑噠垯塌墶搨撘橽毼汏溚炟燵畗畣眔矺笚繨羍胆荅荙薘蟽觰詚跶躂迏迖迭逹達鎉鎝鐽韃龖龘带代戴待袋逮歹贷怠傣殆呔玳迨岱甙黛骀绐埭侢叇嘚垈帒帯帶廗懛曃柋棣毒瀻獃瑇箉簤紿緿艜蚮蝳螮襶詒诒貸蹛軑軚軩轪逯遞遰隶霴靆馱駄駘驮鴏黱但蛋担弹淡丹耽旦氮诞郸惮澹瘅萏殚聃箪赕儋啖丼伔倓冄冉刐勯匰唌啗啿嘾噉噡嚪壇妉娊媅帎弾彈惔憺抌撢擔柦檐欿殫沊泹澸狚玬瓭甔疍癉癚皽砃禫窞簞紞耼聸腅膻膽蜑蜒衴褝觛訑詹誕贉贍赡躭鄲酖醈霮頕餤饏馾駳髧鴠黕黮黵当党挡档荡谠宕菪凼裆砀偒儅噹圵垱壋婸崵嵣愓擋攩檔欓氹潒澢灙珰璗璫瓽當瘍盪瞊碭礑筜簜簹艡蕩蘯蟷襠譡讜趤逿闣雼到道倒刀岛盗稻捣悼导蹈祷纛忉焘氘叨刂啁嘄噵壔宲導屶島嶋嶌嶹捯搗朷椡槝檤洮燾瓙盜禂禱稲箌絩翢翿舠菿虭衜衟軇釖陦陶隝隯魛鱽的地得德底锝徳恴悳惪棏淂登鍀陟哋揼扥扽等灯邓瞪凳蹬磴镫噔嶝戥簦墱嬁櫈燈璒竳艠覴豋鄧鐙隥哣第低敌抵滴帝递嫡弟缔堤涤笛迪狄翟蒂觌邸谛诋嘀骶羝氐睇娣荻碲镝籴砥仾俤偙僀儥勺厎呧唙啇啲嚁坔埅埊埞墆墑墬奃媂嵽嶳廸弔弚弤彽怟扚拞掋揥摕敵旳杕枤梊梑楴樀浟渧滌焍牴玓珶甋疐眱碮磾祶禘篴糴締聜腣芍苐苖莜菂菧蓧蔋蔐藋藡蝃袛覿觝詆諟諦豴趆蹄蹏蹢逓適釱鉪鏑阺隄靮鞮頔題馰髢鬄魡鯳鸐嗲点电店殿淀掂颠垫碘惦奠典佃靛滇甸踮钿坫阽癫簟玷巅癜傎厧唸埝墊壂奌婝婰嵮巓巔扂拈攧敁敟椣槇槙橂橝湺澱琔痶癲磹腍蒧蕇蜓蜔蹎鈿電顚顛驔點齻掉钓叼吊雕调刁碉凋铞铫鲷貂伄佻刟奝嬥屌弴彫殦汈淍琱瘹瞗矵窎窵竨簓粜糶蛁蜩訋誂調軺轺釣鈟銱鋽錭鑃雿颩鮉鯛鳭鵃鵰鸼鼦爹跌叠碟蝶谍牒堞瓞揲蹀耋鲽垤喋佚咥啑峌崼幉怢恎惵戜挃挕昳曡柣楪槢殜氎泆渉渫牃畳疂疉疊眰絰绖耊胅臷艓苵蜨褋褶褺詄諜趃跕軼轶镻鞢鮙鰈鰨鳎嚸顶定订叮丁钉鼎锭町玎铤腚碇疔仃耵酊啶奵嵿帄忊掟椗汀濎甼矴碠磸聢艼萣葶薡訂釘鋌錠鐤靪頂顁飣饤鼑丢铥丟銩动东懂洞冻冬董栋侗恫峒鸫垌胨胴硐氡岽咚倲働凍動勭埬墥姛娻嬞峝崠崬戙挏昸東桐棟氭涷湩烔燑狫笗筩箽絧腖苳菄蕫蝀衕詷諌迵霘駧騆鮗鯟鶇鶫鼕都斗豆逗陡抖痘兜读蚪窦篼蔸乧侸兠凟剅吺唗投斣枓梪橷毭氀浢渎瀆窬竇脰艔荳讀郖酘酡鈄鋀钭閗闘阧餖饾鬥鬦鬪鬬鬭度渡堵独肚镀赌睹杜督犊妒顿蠹笃嘟椟牍黩髑芏剢剫匵厾噣塗妬嬻帾斁晵暏樚樞橐櫝殬殰涜牘犢獨琽瓄皾睪秺笁篤荰螙蠧裻襡襩覩読讟豄賭贕醏錖鍍鍺鑟锗闍阇陼靯韇韣韥頓騳黷段短断端锻缎椴煅簖偳剬塅媏彖斷毈瑖碫籪緞耑腶葮褍躖鍛鍴叾对队堆兑敦镦碓怼憝兊兌垖埻塠夺奪対對嵟憞懟杸濧濻瀢瀩痽磓祋綐膭薱謉譈譵鈗銳鋭鐓鐜锐陮隊頧鴭吨墩钝盾囤遁趸盹礅炖砘伅俊噸墪壿庉忳惇撉撴橔潡燉犜獤碷腞腯蜳踲蹾躉逇遯鈍驐朵舵剁垛跺惰堕掇躲沲咄铎裰哚缍亸凙刴喥嚉嚲垜埵墮墯媠嫷尮崜嶞憜挅挆敓敚敠敪朶柁柂柮桗椯橢毲沰澤痥硾綞茤詑貀趓跥躱軃鈬鐸陊隓飿饳鮵鵽";
            //E
            PY[4] = "呃阨饿额鹅蛾扼俄讹遏峨娥恶厄鄂锇谔垩锷阏萼苊轭婀莪鳄颚腭愕噩鹗屙亞佮侉偔偽僞僫匎卾吪咢哑唖啈啞噁囐囮垭埡堊堮妸妿姶娿屵岋峉峩崿廅悪惡戹搕搤搹擜曷枙椏櫮歞歺涐湂玀珴琧皒睋砈砐砨硆硪磀礘蒍蕚蘁蚅蝁覨訛詻誐諤譌讍豟軛軶輵迗遌遻邑鈋鋨鍔鑩閜閼頟額顎餓餩騀鬲魤魥鰐鰪鱷鵈鵝鵞鶚齃齶齾诶恩摁蒽嗯奀峎煾饐鞥仒乻欕旕而二耳儿饵尔贰洱珥鲕鸸佴迩铒侕児兒刵咡唲嬭尒尓峏弍弐杒栭栮樲毦洏渪濡爾粫耏聏胹臑荋薾衈袻貮貳趰輀輭轜邇鉺陑陾隭餌駬髵髶鮞鴯";
            //F
            PY[5] = "茷分紡纺怫琲茀蜚炃俸熢佛复幅拂服畐费鶝封疺份番發捬附发法罚伐乏筏阀珐垡砝佱傠姂廢彂栰橃汎沷泛灋琺発瞂罰罸蕟藅醗醱鍅閥髪髮反饭翻犯凡帆返繁烦贩范樊藩矾钒燔蘩畈蹯梵幡仮伋凢凣勫匥墦奿婏嬎嬏嬔忛憣払旙旛杋柉棥楓橎氾渢滼瀪瀿煩犿璠畨盕礬笲笵範籵緐繙羳膰舤舧薠蟠蠜袢訉販軓軬轓辺釩鐇颿飜飯飰鱕鷭放房防芳方访仿坊妨肪钫彷邡舫鲂倣匚堏旊昉昘汸淓牥瓬眆訪趽鈁錺雱髣魴鰟鳑鴋鶭非飞肥肺废匪吠沸菲诽啡篚腓扉妃斐狒芾悱镄霏翡榧淝鲱绯痱俷剕厞奜婓婔屝廃昲暃曊朏柹棐橨櫠渄濷猆疿癈砩祓笰紼緋绋胏胐萉蕜蕡蜰裴裵裶誹鐨陫靅靟飛飝餥馡騑騛髴鯡鼣芬粉坟奋愤纷忿粪酚焚吩氛汾棼瀵鲼偾鼢僨喷噴坆坋墳奮妢帉幩弅愍憤敃昐朆枌梤棻歕濆燓盼瞓秎竕糞紛羒羵翂膹葐蒶蚠蚡衯豮豶躮轒鈖錀隫雰餴饙馚馩魵鱝黂黺鼖风逢缝蜂丰枫疯冯奉讽凤峰锋烽砜酆葑沣仹偑僼凨凬凮堸夆妦寷峯崶捀摓桻檒沨浲湗溄灃炐焨煈犎猦琒甮瘋盽碸篈綘縫肨舽艂莑蘕蘴諷豊豐賵赗逄鄷鋒鎽鏠靊風飌馮鳯鳳鴌麷瓰覅仏仸坲梻否缶垺妚炰紑缹缻芣衃雬鴀副扶浮富福负伏付俯斧赴缚夫父符孵敷赋辅府腐腹妇抚覆辐肤氟俘傅讣弗涪袱甫釜脯腑阜咐黼苻趺跗蚨幞茯滏蜉菔蝠鳆蝮绂赙罘稃匐麸凫桴莩孚驸呋郛芙黻乀伕俌俛偩冨冹刜呒咈哹嘸坿垘妋姇娐婦媍宓岪峊巿弣彿復怤懯抙捊撫旉枎柎柫栿棴椨椱汱沕泭洑澓炥烰焤玞玸琈璷甶畉癁盙砆祔禣稪竎筟箙簠粰糐紨紱絥綍綒緮縛纀罦翇胕膚艀荂荴莆葍蓲蕧虙蚥蚹蛗蜅蝜衭袝複褔襆襥覄訃詂諨豧負賦賻軵輔輻邚邞郍郙鄜酜酻釡鈇鉘鉜錇鍑鍢锫阝陚韍韨頫颫駙鬴鮄鮒鮲鰒鳧鳬鳺鴔鵩麩麬麱猤";
            //G
            PY[6] = "閡阂隑广干棍崗閞噶胳嘎轧钆伽旮尬尕尜呷嘠玍軋釓錷魀甴该改盖概钙芥溉戤垓丐陔赅乢侅匃匄咳姟峐忋摡晐杚槩槪漑瓂畡祴絯胲荄葢蓋該豥賅賌郂鈣鎅阣骸赶感敢竿甘肝柑杆赣秆旰酐矸疳泔苷擀绀橄澉淦尴坩个乹乾亁仠佄倝凎凲咁尲尶尷幹忓扞攼桿榦檊浛漧灨玕皯盰稈笴筸篢簳粓紺芉虷衦詌諴豃贑贛趕迀釬錎飦骭魐鰔鱤鳡鳱刚钢纲港缸岗杠冈肛筻罡戆冮剛堈堽岡戅戇掆棡槓溝焵牨犅犺疘矼碙綱罁罓釭鋼鎠阬頏颃高搞告稿膏篙羔糕镐皋郜诰杲缟睾槔锆槁藁勂吿咎夰峼暠槀槹橰檺櫜浩滜澔獋獔皐祮祰禞稁稾筶縞羙臯菒蒿藳誥鋯鎬韟餻髙鷎鷱鼛各歌割哥搁格阁隔革咯葛蛤戈鸽疙屹铬硌骼颌袼塥虼圪镉仡舸嗝膈搿纥哿佫個匌可吤呄嘅嘢彁愅戓戨扢挌擱敋槅櫊滆滒牫牱犵猲獦秴箇紇肐臈臵茖菏蛒裓觡詥諽謌轕鉀鉻鉿鎑鎘鎶钾铪閘閣閤闸鞈鞷韐韚頜騔髂魺鮥鮯鲄鴐鴚鴿鵅给給跟根哏茛亘艮揯搄痕更耕颈梗耿庚羹埂赓鲠哽亙刯堩峺恆挭掶暅椩浭焿畊絙絚緪縆羮莄菮賡郉郠頸骾鯁鶊鹒嚿啹喼嗰工公功共弓攻宫供恭拱贡躬巩汞龚肱觥珙蚣匑匔厷咣唝嗊塨宮幊廾愩慐拲杛栱渱熕碽糼羾虹蛩觵貢躳輁銾鞏髸魟龏龔兝兣够沟狗钩勾购构苟垢岣彀枸鞲觏缑笱诟遘媾篝佝傋冓呴坸夠姤抅搆撀構泃煹玽簼緱耇耈耉茩蚼袧褠覯訽詬豿購軥鈎鉤雊韝鮈鴝鸜鸲古股鼓谷故孤箍姑顾固雇估咕骨辜沽蛊贾菇梏鸪汩轱崮菰鹄鹘钴臌酤呱鲴诂牯瞽毂锢牿痼觚蛄罟嘏傦僱凅劷哌唂唃啒嗀嗗堌夃嫴尳峠崓怘愲抇柧棝榖榾橭櫎泒淈濲瀔焸瓠皷盬硲磆祻稒穀笟箛篐糓縎罛羖胍脵苽蓇薣蛌蠱詁軱軲轂逧鈲鈷錮頋顧餶馉骰鮕鯝鴣鵠鶻鼔挂刮瓜寡剐褂卦鸹栝诖冎剮劀叧咶咼啩坬掛歄煱絓緺罣罫詿諣趏踻銛銽铦颪颳騧鴰怪拐乖掴叏哙噲夬恠枴柺关管官观馆惯罐灌冠贯棺盥掼涫鳏鹳倌丱卝婠悹悺慣懽摜斡果桄樌權毌泴淉淪潅爟琯瓘痯瘝癏矔礶祼窤筦綸罆舘菅萖蒄覌観觀貫躀輨遦錧鏆鑵関闗關雚館鰥鱞鱹鳤鵍鸛光逛犷胱侊俇僙垙姯広廣挄撗横欟洸潢灮炗炚炛烡獷珖硄臦臩茪趪輄迋銧黆归贵鬼跪轨规硅桂柜龟诡闺瑰圭刽癸炔庋宄桧刿鳜鲑皈匦妫晷簋炅亀佹劊劌匭匮匱厬哇垝姽媯嫢嬀嶡嶲巂帰庪廆恑摫撌攰攱昋朹桅椝椢槣槶槻槼檜櫃櫰櫷歸氿沩湀溎潙珪璝瓌癐瞆瞡瞶硊祪禬窐筀簂胿茥蓕蘬蛫螝蟡袿襘規觖觤詭貴赽趹蹶軌邽郌閨陒隗雟鞼騩鬶鬹鮭鱖鱥鴂鴃龜滚辊鲧衮磙绲丨惃棞滾璭睔睴緄緷蓘蔉袞裷謴輥錕锟鮌鯀过国裹锅郭涡埚椁聒馘猓崞帼呙虢蜾蝈唬啯嘓囗囯囶囻圀國堝墎幗彉彍惈慖摑楇槨活渦漍濄瘑矌粿綶聝腂腘膕菓蔮蜮蝸蟈蠃褁輠過鈛錁鍋鐹锞餜馃";
            //H
            PY[7] = "欬屽盒頇顸憾怀还核捍汗合红紅滑恍絵繪绘浑混渾划哈丷奤妎為獬蝦海害氦孩骇亥嗨醢咍咴嗐嚡塰烸還酼頦颏餀饚駭駴乤喊含寒汉旱酣韩焊涵函憨翰罕撼悍邯邗菡撖瀚阚蚶焓颔晗鼾傼兯凾哻唅嚂圅娢嫨崡嵅嵌晘晥暵梒椷欦歛浫涆淊滩漢澏澣灘熯爳猂琀甝皔睅筨糮肣莟蔊蘫蛿蜬蜭螒譀谽銲鋎鋡闞雗靬韓頷顄顩馠馯駻鬫魽鶾航杭吭沆绗珩垳妔忼斻桁炕笐筕絎苀蚢貥迒邟魧好号嚎壕郝毫豪耗貉昊颢灏嚆嗥皓濠薅傐儫呺哠嘷噑妞恏悎昦晧暤暭曍椃淏滈灝獆皜皞皡皥秏竓籇翯聕膠茠薃薧藃號虠蠔諕譹鄗鎒鐞顥鰝兞和喝河禾何荷贺赫褐鹤涸嗬劾盍翮阖壑诃呼咊哬啝喛嗃嗑嚇垎姀寉峆惒愒抲揭敆柇楁欱渮湼澕焃煂熆熇燺爀狢癋皬盇盉碋篕籺粭繳缴萂藿蚵蝎螛蠚袔覈訶訸謞貈賀輅轄辂辖郃鉌鑉闔阋隺霍靍靎靏鞨餄饸鬩鶡鶮鶴鸖鹖麧齕龁龢黑嘿嬒潶黒冚很狠恨佷噷拫掀詪鞎恒哼衡亨蘅堼姮悙橫涥烆狟胻脝訇鑅鴴鵆鸻囍乊乥轰哄洪宏烘鸿弘讧蕻闳薨黉荭泓仜叿吰吽哅嚝垬妅娂宖屸巆彋揈撔晎汯浤渹潂澋澒灴焢玒玜瓨硔硡竑竤篊粠紘紭綋纮翃翝耾舼苰葒葓訌謍谹谼谾軣輷轟鈜鉷鋐鍧閎閧闀闂霐霟鞃鬨鴻黌后厚吼喉侯候猴鲎篌堠後逅糇骺瘊垕帿洉犼睺矦翭翵葔豞郈鄇銗鍭餱鮜鯸鱟鲘齁湖户虎壶互胡护糊弧忽狐蝴葫沪乎瑚鹕冱怙鹱笏戽扈浒祜醐琥囫烀轷煳斛猢惚岵滹觳唿槲乕冴匢匫喖嘑嘝嚛垀壷壺姱婟媩嫭嫮寣帍幠弖恗戯戱戲戶戸搰擭昈昒曶枑楛楜槴歑殻汻沍泘洿淴滬滸濩瀫焀熩瓡瓳礐穫箶簄粐絗綔縏縠膴舗芐芔芴苸萀蔛蔰虍虖虝螜衚觷謼護軤鄠鋘錿鍙鍸雐雽韄頀頶餬鬍魱鯱鰗鱯鳠鳸鴩鶘鶦鸌话花化画华哗猾豁铧桦骅砉劃劐嘩埖姡婲婳嫿嬅崋搳摦撶杹椛槬樺檴浍澅澮獪璍畫畵硴稞糀繣舙芲華蕐蘤蘳螖觟話誮諙譁譮釪釫錵鏵驊鷨黊坏淮槐徊踝佪喟嘳坯壊壞懐懷瀤耲蘹蘾褢褱换唤环患缓欢幻宦涣焕豢桓痪漶獾擐逭鲩郇鬟寰奂锾圜洹萑缳浣喚嚾圂垸堚奐孉寏峘嵈巜愌懁換援攌梙槵欥歓歡汍渙澴烉煥瑍環瓛瘓眩睆瞏瞣糫緩繯羦肒荁萈蒝藧蠸讙豲貆貛輐轘酄鉮鍰鐶镮闤阛雈驩鯇鯶鰀鴅鹮黄慌晃荒簧凰皇谎惶蝗磺煌幌隍肓篁徨鳇遑癀湟蟥璜偟兤喤堭塃墴奛媓宺崲巟怳愰揘晄曂朚楻榥滉炾熀熿爌獚瑝皝皩穔縨艎茫葟衁詤諻謊鍠鎤鐄锽韹餭騜鰉鱑鷬黃回会灰挥汇辉毁悔惠晦徽恢秽慧贿蛔讳卉烩诲彗珲蕙喙恚哕晖隳麾诙蟪茴洄虺荟缋僡儶匯叀嘒噅噕噦嚖囘囬圚婎媈孈寭屷幑廻廽彙彚徻恛恵憓懳拻揮撝暉暳會椲楎槥橞檅檓櫘毀毇泋洃湏滙潓濊瀈灳烜烣煇煒燬燴獩琿璤璯痐瘣眭睢睳瞺禈穢篲繢翙翚翬翽蒐蔧薈薉藱蚘蛕蜖袆褘詯詼誨諱譓譭譿豗賄輝迴逥違銊鏸鐬闠阓靧韢韦頮顪餯鮰鰴鼿齀昏荤婚魂阍馄溷诨俒倱婫忶惛惽慁捆掍昬梡梱棔殙涽湣湷焄焝眃睧睯緍緡繉葷蔒觨諢轋閽顐餛餫鼲或火伙货获祸惑嚯镬耠攉锪蠖钬夥佸俰剨吙咟嚄奯掝旤曤沎湱漷瀖獲癨眓矆矐礊禍秮秳篧耯臛艧萿蒦謋貨邩鈥鍃鑊閄雘靃騞魊夻遤";
            //I
            PY[8] = "乁";
            //J
            PY[9] = "鯦浇澆捷接渐漸剿净淨茧粢仅夹咖奸介句拘角賈矜几及急既即机鸡积记级极计挤己季寄纪基激吉脊际汲肌嫉姬绩缉饥迹棘蓟技冀辑伎祭剂悸济籍寂其忌齐妓继集击圾箕讥畸稽疾墼洎鲚屐齑戟鲫嵇矶稷戢虮诘笈暨笄剞叽蒺跻嵴掎跽霁唧畿瘠玑羁丌偈芨佶赍楫髻咭蕺觊麂骥殛岌亟犄乩芰哜丮亼倚偮僟兾刉刏剤劑勣卽叝吇喞嗘嘰嚌坖垍塉墍妀姞姼尐居峜嵆嶯帺幾廭彐彑彶徛忣惎愱憿懠懻揖揤撃撠撽擊擠攲敧旡旣暩曁枅梞楖極槉樭機橶檕檝檵櫅櫭毄汥泲洁淁済湒漃漈潗濈濟瀱焏犱狤猗璂璣璾痵瘵癠癪皍睽瞉瞿磯禝禨秸稘稩穄穊積穖穧筓箿簊糭紀紒級結継緝縘績繋繫繼结罽羇羈耤耭脔膌臮艥艻芶茍莋萁葪蒩蔇蕀蕲薊藉蘎蘮蘻虀蝍螏蟣蟻蟿裚襀襋覉覊覬觙觭計訐記誋諅譏譤讦谻賫賷趌趞跂跡踑踖踦蹐蹟躋躤躸輯轚郅郆鄿銈銡錤鍓鏶鐖鑇鑙際隮雞雦雧霵霽鞊鞿韲飢饑驥鬾魝魢鯚鯯鯽鰶鰿鱀鱭鱾鳮鵋鶏鶺鷄鷑鸄鹡齌齍齎齏家加假价架甲佳嘉驾嫁枷荚颊稼茄铗葭迦戛浃镓痂恝岬跏胛笳珈郏袈蛱傢價叚唊圿埉夓夾婽宊幏徦忦戞扴抸押拁拮挈挟揩擖斚斝暇梜椵榎榢槚檟毠泇浹犌猳玾糘耞脥腵莢蛺袷裌豭貑跲郟鉫鋏鎵頡頬頰颉駕骆骱鴶鵊麚嗧见件减尖间键贱肩兼建检箭煎简剪歼监坚健艰荐剑溅涧鉴践捡柬笺俭碱硷拣舰缄饯翦鞯戋谏牮枧腱趼缣搛戬毽鲣笕谫楗蹇裥踺睑謇鹣蒹僭锏湔俴倹偂傔僣儉冿剣剱劍劎劒劔囏堅堑堿塹墹姦姧寋帴幵弿彅徤惤戩挸揀揃揵撿擶攕旔暕柙栫梘検椾榗樫橌橏橺檢檻櫼殱殲涀減湕澗濺瀐瀳瀸瀽熞熸牋犍猏玪珔瑊瑐監睷瞯瞷瞼碊磵礀礆礛稴筧箋箴篯簡籈籛糋絸緘縑繝繭纎纖聻臶艦艱菺葌葏蔪蕑蕳薦藆虃蠒袸襇襉襺見覸詃諓諫謭譖譼譾谮豜豣賎賤趝踐轞醎醶釰釼鈃銒銭鋄鋻錢錬錽鍊鍳鍵鎫鏩鐗鐧鐱鑑鑒鑬鑯鑳閒間鞬韀韉餞餰馢騫骞鬋鰎鰜鰹鳒鳽鵳鶼鹸鹹鹻鹼麉黚黬将讲江奖降浆僵姜酱蒋疆匠桨豇礓缰犟耩绛茳糨洚勥匞塂壃夅奨奬將嵹弜弶彊摪摾杢槳橿櫤殭滰漿獎畕畺疅糡絳繮翞膙葁蔃蔣薑螀螿袶講謽醤醬韁顜鱂鳉叫脚交教较觉焦胶娇绞校搅骄狡矫郊嚼蕉轿窖椒礁饺铰酵侥徼艽僬蛟敫峤跤姣皎茭鹪噍醮佼湫鲛挢侨僑僥儌勪呌咬嘂嘦嫶嬌嬓孂峧嵺嶕嶠嶣恔憍憢挍捁撟撹攪敎敥敽敿斠晈暞曒櫵湬滘漖潐灂灚烄焳煍燋獥珓璬皦皭矯穚窌笅筊簥糾絞纐纠腳膲臫芁茮菽萩蕎藠蟜蟭覐覚覺訆譑譥賋趭踋蹻較轇轎釂釥鉸鐎餃驕骹鮫鱎鵁鵤鷦鷮节街借皆截解界届姐戒阶劫竭疥桔杰诫睫桀喈羯蚧嗟鲒婕碣孑疖丯倢偼傑刦刧刼劼卩卪唶堦堺媎媘媫嫅屆岊岕崨嵑嵥嶰嶻巀幯庎徣悈掲搩擑擮擳昅桝椄楐楬楶榤檞毑洯湝滐潔煯犗玠琾畍疌痎癤砎礍稭節絜繲脻艐莭菨蓵蛣蛶蜐蝔蠘蠞蠽衱衸袓袺褯觧詰誡誱謯踕迼鉣鍇鍻锴階雃鞂飷髫魪鮚鶛进近今紧金斤尽劲禁浸锦晋筋津谨巾襟烬靳廑瑾馑槿衿堇荩噤缙卺妗赆觐伒侭僅僸儘兓凚劤勁厪唫嚍埐堻墐壗婜嫤嬐嬧寖嶜巹惍慬搢斳晉枃歏殣浕溍漌濅濜煡燼珒琎琻瑨璡璶盡砛祲竻紟紾緊縉臸荕菫菳蓳藎覲觔謹賮贐進釒釿鋟錦钅锓饉馸鹶黅齽竟静井惊经镜京敬精景警竞境径荆晶鲸粳兢茎睛痉靖肼獍阱腈弪刭憬婧胫菁儆旌迳靓泾亰仱俓倞傹儬凈剄坓坕坙妌婙婛宑巠幜弳徑憼擏旍暻曔桱梷橸檠殑汫汬浄涇濪烃烴燝猄璄璟璥痙秔稉穽竧竫競竸箐粇経經聙脛荊莖葝蟼誩踁逕鋞鏡陉陘靘靚靜頚頴颕驚鯨鵛鶁鶄麖麠鼱窘炯扃迥侰僒冂冋冏囧坰垧埛宭扄泂浻澃烱煚煛熲燛絅綗蘏蘔褧逈銄鎣顈颎駉駫就九酒旧久揪救舅究韭厩臼玖灸疚赳鹫僦柩桕鬏鸠阄啾丩乆乣倃剹勼匓匛匶奺廄廏廐慦捄揂揫摎朻杦柾樛橚殧汣牞畂稵穋糺紤繆缪舊舏萛蝤镹韮鬮鳩鷲麔齨欍举巨局具距锯剧聚菊矩沮拒惧鞠狙驹据俱咀疽踞炬倨醵裾屦犋窭飓锔椐苣琚掬榘龃趄莒雎遽橘踽榉鞫钜讵侷倶僪冣凥劇勮匊圧坥埧埾壉姖娵婅婮寠屨岠岨崌巈弆忂怇怐愳懅懼拠挙挶揟據擧昛梮椇椈檋櫸欅歫毩毱泦洰涺淗渠湨澽焗爠犑狊珇痀眗砠秬窶筥篓簍簴籧粔粷罝耟聥腒臄舉艍菹萭葅蒌蒟蓻蔞蘜蘧虡蚷蛆蜛螶襷詎諊豦貗趉趜跔跙跼踘蹫躆躹輂邭郥郰郹鄒鄹鉅鋦鋸鐻閰陱颶駏駒駶驧鬻鮍鮔鲏鴡鵙鵴鶋鶪鼰鼳齟卷倦鹃捐娟眷绢鄄锩蠲镌狷桊涓隽劵勌勬呟埍埢奆姢帣悁惓慻捲朘梋棬泫淃焆獧瓹甄睃睊睠絭絹縳罥羂脧腃臇菤萒蔨蕋蜷裐襈讂踡鋗錈鎸鐫雋鞙韏飬餋鵑决绝爵掘诀撅倔抉攫桷噱橛劂爝矍镢獗珏崛蕨噘谲孓厥亅傕刔匷啳埆妜孒屩屫崫嶥弡彏憠憰戄挗捔撧斍橜欔欮殌氒決泬潏灍熦爑爴狂玃玦玨瑴璚疦瘚矞矡砄穱絕絶繑繘芵蕝蕞虳蚗蛙蟨蟩蠼袦觼訣誳譎貜蹷躩逫鈌鐍鐝钁镼闋闕阕阙鞒鞽駃騤骙鶌鷢龣军君均菌峻竣骏钧郡筠麇皲捃儁呁埈姰寯懏攈晙桾汮濬焌焞燇狻珺畯皸皹碅箘箟莙葰蚐蜠袀覠軍鈞銁銞鋆鍕陖餕馂駿鮶鲪鵔鵕鵘麏麕";
            //K
            PY[10] = "剀剴嵦碪堪扛亢伉抗枯苦扩擴傀渇渴苛昆槛楷卡喀胩咔佧呿垰衉裃鉲开凯慨垲锎铠忾恺蒈凱勓塏奒幆愷愾暟欯溘炌炏烗豈輆鎎鎧鐦開闓闿雉颽乫看砍刊坎勘龛戡侃瞰莰偘凵埳塪墈崁嵁惂扻栞歁矙磡竷莶薟衎輡輱轁轗顑餡馅龕糠康慷钪闶匟囥坑嫝嵻摃槺漮砊穅躿鈧鏮閌鱇靠考烤拷栲犒尻铐丂嵪攷洘焅訄銬髛鮳鯌鲓克棵科颗刻课客壳柯磕坷恪岢蝌缂轲窠钶氪瞌珂髁疴骒剋勀勊堁娔尅嵙嶱愘愙揢敤榼樖炣牁犐痾砢硞碦礚窼簻緙翗胢萪薖課趷軻醘鈳顆騍乪蠄肯啃恳垦裉垠墾懇掯珢硍肎肻褃豤貇錹齦龈铿劥坈忐挳揁摼殸牼硁硎硜硻誙銵鍞鏗巪乬迲唟厼怾空孔控恐倥崆箜埪宆悾椌涳硿躻錓鞚鵼廤口扣抠寇蔻芤眍筘叩冦剾劶宼彄挎摳敂滱瞘窛竘簆蔲釦鏂鷇哭库裤窟酷刳骷喾堀绔俈嚳圐庫扝桍泏狜瘔矻秙窋絝袴褲趶跍跨郀鮬垮夸胯咵晇舿誇銙顝骻快块筷侩蒯郐狯脍儈凷圦塊墤廥擓旝糩膾蒉蕢鄶鬠魁鱠鲙宽款髋寛寬欵歀窽鑧髖矿筐框况旷匡眶诳邝纩夼诓圹贶哐儣劻匩壙岲忹恇懬懭抂昿曠枉況洭狅眖砿礦穬筺絋絖纊誆誑貺軖軠軦軭邼鄺鉱鋛鑛鵟黋亏愧奎窥溃葵馈盔岿愦揆跬聩篑喹逵暌悝馗蝰夔刲卼嬇尯巋巙憒戣晆楏楑樻櫆欳殨潰煃磈窺簣籄聧聭聵臾藈蘷虁虧蹞躨鄈鍨鍷鐀鑎闚頄頍頯餽饋困坤鲲髡琨醌阃悃堃堒壸壼尡崐崑晜涃潉焜熴猑瑻睏硱祵稇稛綑罤臗菎蜫裈裍裩褌閫閸騉髠髨鯤鵾鶤鹍阔括廓适蛞噋懖拡桰濶筈葀闊霩鞟鞹韕頢髺穒";
            //L
            PY[11] = "厘唠嘮帘离離骊硫纶沦狼芦苙蜡駱滥濫肋卵拉啦辣腊喇垃蓝落瘌邋砬剌旯儠嚹揦揧搚摺擸攋柆楋櫴溂爉瓎癩磖翋臘菈藞蝋蝲蠟辢鑞镴鞡鬎鯻鱲来赖莱濑赉崃涞铼籁徕癞睐來俫倈厲唻婡崍庲徠懶梾棶淶瀨瀬猍琜睞筙箂籟萊藾襰賚賴逨郲釐錸頼顂騋鯠鶆麳黧兰烂拦篮懒栏揽缆阑谰婪澜览榄岚褴镧斓罱漤僋儖厱啉囒壈壏嬾孄孏嵐幱廩廪惏懔懢擥攔攬斕欄欖欗浨涟湅漣瀾灆灠灡炼煉燗燣爁爛爤爦璼瓓礷籃籣糷繿纜葻藍蘭襕襤襴襽覧覽譋讕躝醂鑭钄闌韊顲浪廊郎朗榔琅稂螂莨啷锒阆蒗俍勆哴唥埌塱嫏崀悢朖朤桹樃樠欴烺瑯硠筤脼艆蓈蓢蜋誏踉躴郒郞鋃鎯閬駺老捞牢劳烙涝姥酪络佬潦耢铹醪铑栳崂痨僗僚労勞咾哰嗠嫪嶗恅憥憦撈撩朥橑橯浶澇獠珯癆硓磱窂簩粩絡耮蓼蛯蟧軂轑銠鐒顟髝鮱了乐勒鳓仂叻泐嘞忇楽樂氻玏砳簕阞韷餎饹鰳类累泪雷垒擂蕾镭儡磊缧诔耒酹羸嫘檑傫儽卢厽咧塁壘壨攂樏櫐櫑欙洡涙淚漯灅瓃畾癗盧矋磥礌礧礨祱禷絫縲纇纍纝罍脷蔂蕌藟蘱蘲蘽虆蠝誄讄轠銇錑鐳鑘鑸靁頛頪類颣鱩鸓鼺冧冷棱楞愣塄倰堎睖碐稜薐輘里力立李例哩理利梨礼历丽吏砾漓傈荔俐痢狸粒沥栗璃鲤厉励犁黎篱郦鹂笠坜苈鳢缡跞蜊锂澧粝蓠枥蠡呖砺嫠篥疠疬猁藜溧鲡戾栎唳醴轹詈罹逦俪喱雳莅俚蛎娌位儮儷凓刕列剓剺劙勵厤厯叓叕唎嚟嚦囄囇塛壢娳婯孋孷屴岦峛峲巁廲悡悧悷慄捩搮擽攊攦攭斄暦曆曞朸栃栛栵梸棃棙檪櫔櫟櫪欐欚歴歷氂沴沵浬涖濼濿瀝灑灕爄爏犂犛犡珕珞琍瑮瓅瓈瓑瓥癘癧盠盭睝矖砅磿礪礫礰禮禲秝穲竰筣籬粴糎糲綟縭纅纚艃茘荲菞蒚蒞蔾藶蘺蚸蛠蜧蝕蝷蟍蟸蠇蠣蠫裏裡褵觻謧讈貍赲躒轢轣邐酈醨釃鉝鋫鋰錅鏫鑗鑠铄隷隸霾靂靋鬁鯉鯏鯬鱧鱱鱳鱺鳨鴗鵹鷅鸝麗麜俩倆连联练莲恋脸链敛怜廉镰蠊琏殓蔹鲢奁潋臁裢濂楝亷令僆劆匲匳嗹噒堜奩娈媡嫾嬚孌慩憐戀挛摙攣斂梿槏槤櫣殮浰溓澰濓瀲熑燫瑓璉瞵磏簾籢籨練縺纞羷翴聨聫聮聯膦臉苓萰蓮薕蘝蘞螊褳覝謰譧蹥連鄻鎌鏈鐮零鬑鰊鰱鱄两亮辆凉粮梁量良晾谅粱墚椋魉両兩唡啢喨掚樑涼湸煷簗糧綡緉蜽裲諒輌輛輬辌鍄魎料聊撂疗廖燎辽寥镣钌尥寮缭鹩嘹僇嫽尞尦屪嶚嶛廫憀憭敹暸漻炓爎爒璙療瞭窷竂簝繚膋膫蟉蟟豂賿蹘蹽遼鄝釕鏐鐐镠镽飂飉髎鷯裂猎劣烈埒鬣趔躐冽洌劦劽哷埓奊姴峢巤忚挒挘毟浖烮煭燤犣猟獵睙聗脟茢蛚迾颲鬛鮤鴷林临淋邻磷鳞赁吝拎琳霖凛遴嶙蔺粼麟躏辚檩亃僯凜厸壣崊恡悋懍撛斴晽暽橉檁潾澟瀶焛燐獜璘甐疄痳碄箖粦繗翷臨菻藺賃蹸躙躪轔轥鄰鏻閵隣驎魿鱗麐另领铃玲灵岭龄凌陵菱伶羚翎蛉绫瓴酃呤泠棂柃鲮聆囹刢坽夌姈婈孁岺崚嶺彾掕昤朎櫺欞淩澪瀮炩燯爧狑琌皊砱祾秢竛笭紷綾舲蓤蔆蕶蘦衑袊裬詅跉軨醽鈴錂閝阾霊霗霛霝靇靈領駖鯪鴒鸰鹷麢齡齢龗六流留刘柳溜瘤榴琉馏碌陆绺锍鎏镏浏骝旒鹨熘遛偻僂劉嚠塯媹嬼嵧廇懰抡斿旈栁桞桺橊橮沠泖澑瀏熮珋瑠瑬璢畄畱疁癅磂磟綹罶羀翏膢蒥蓅藰裗蹓鋶鎦鐂陸雡霤飀飅飗餾駠駵騮驑鬸鰡鶹鷚鹠麍瓼甅囖龙拢笼聋隆垄弄咙窿陇垅胧珑茏泷栊癃砻儱厐哢嚨壟壠嶐巃巄徿攏昽曨朧梇槞櫳湰滝漋瀧爖瓏眬矓硦礱礲竉竜篭籠聾蘢蠪蠬衖襱豅贚躘鏧鑨隴霳驡鸗龍龐龒龓楼搂漏陋露娄蝼镂耧髅喽瘘嵝嘍塿婁屚嶁廔慺摟樓溇漊熡甊瘺瘻瞜耬艛螻謱軁遱鏤鞻髏路录鹿炉鲁卤颅庐掳绿虏赂戮潞禄麓鲈栌渌泸轳氇簏橹垆胪噜镥辘漉撸璐鸬鹭舻侓勎勠嚕嚧圥坴塶塷壚娽峍廘廬彔挔捋捛摅摝擄擼攄攎枦椂樐櫓櫨氌淕淥滤滷澛濾瀂瀘爐獹玈琭瓐甪盝睩矑硉硵磠祿稑箓簬簵簶籙籚粶緑纑罏膟臚舮艣艪艫菉蓾蕗蘆虂虜螰蠦觮賂趢踛蹗轆轤醁鈩錄録錴鏀鏴鐪鑥鑪顱騄騼髗魯魲鯥鱸鴼鵦鵱鷺鸕鹵黸乱滦峦孪栾銮鸾乿亂圝圞奱孿巒曫欒灓灤癴癵羉臠臡薍虊覶釠鑾鵉鸞略掠锊圙寽畧稤鋝鋢论轮伦仑囵侖倫圇埨婨崘崙惀掄棆溣碖磮稐耣腀菕蜦論踚輪陯鯩罗锣裸骡箩螺萝洛逻荦雒倮椤脶瘰摞泺镙猡儸剆啰囉峈挼捰攞曪欏烁犖癳笿籮纙羅腡臝蓏蘿覼躶邏鏍鑼頱饠騾驘鸁铝驴旅屡吕律氯缕侣履膂榈闾褛稆侶儢勴卛呂垏屢嵂慮曥梠櫚焒爈祣穞穭箻絽綠縷繂膐葎藘褸郘鋁鑢閭馿驢鷜";
            //M
            PY[12] = "秘鴓募芒沬缗鉚铆呣吗妈马嘛麻骂抹码玛蚂摩唛蟆犸嬷杩么亇傌嗎嘜媽嫲嬤孖尛榪溤犘獁瑪痲睰碼礣祃禡罵蓦蔴螞蟇貊鎷閁靡馬駡驀鬕鰢鷌麼麽买卖迈埋麦脉劢荬佅勱咪嘪売脈蕒薶衇買賣邁霡霢鷶麥满慢瞒漫蛮蔓曼馒谩幔鳗墁螨镘颟鞔缦熳僈姏嫚屘幕悗慲摱槾満滿澫澷獌睌瞞矕絻縵蔄蘰蟃蟎蠻襔謾鄤鏋鏝顢饅鬗鬘鰻忙盲莽氓硭邙蟒漭厖吂哤壾娏尨庬恾朦杗杧汒浝牤牻狵甿痝盳瞢笀茻莾蘉蠎釯鋩铓駹鸏鹲匁毛冒帽猫矛卯貌茂贸锚茅耄茆瑁蝥髦懋昴牦瞀峁袤蟊旄侔冃冇冐勖務堥夘媢嵍愗戼描暓枆楙毣毷渵牟獏皃眊笷緢罞芼萺蓩蛑蝐覒貓貿軞鄚鄮酕鉾錨霿髳鶜唜嚒嚜嚰孭庅濹癦没每煤镁美酶妹枚霉玫眉梅寐昧媒糜媚谜沫嵋猸袂湄浼鹛莓魅镅楣凂呅坶堳塺墨媄媺嬍嵄徾抺挴攗攟某栂楳槑櫗毎氼沒渼湈溦煝燘珻瑂痗眛睂睸矀祙禖篃脄脢腜苺葿蘪蝞跊躾郿鋂鎂鎇韎鬽鶥黣黴门们闷懑扪钔焖亹們怋悶懣捫暪椚汶燜玟玣玧璊穈菛虋鍆門閅猛梦蒙锰孟盟檬萌礞蜢勐懵甍蠓虻艋艨儚冡夢夣嫇幪懜懞掹擝明曚橗氋溕濛獴瓾瞑矇矒莔萠蕄蝱鄳鄸錳雺霚霥霧靀顭饛鯍鯭鱦黽黾鼆踎米密迷眯蜜觅弥幂醚蘼縻汨麋祢猕弭谧芈脒敉嘧糸侎冖冞冪劘塓孊宻峚幎幦幺彌戂摵擟擵攠榓樒檷櫁洣淧渳溟滵漞濔濗瀰灖熐爢獯獼瓕眫眽瞇瞴祕禰簚籋粎罙羃羋葞蒾蓂蔝蔤藌蝆袮覓覔覛詸謎謐醾醿釄銤鑖镾鸍麊麛鼏面棉免绵眠缅勉冕娩腼湎眄沔渑丏偭冥勔喕婂媔嬵宀愐檰櫋汅泯澠矈矊矏糆綿緜緬芇莬葂蝒蠠靣靦鮸麪麫麵麺秒苗庙妙瞄藐渺眇缈淼喵杪鹋邈媌嫹庿廟玅竗篎緲蜱鱙鶓灭蔑咩篾蠛乜吀哶幭懱搣滅瀎眜薎衊覕谂鱴瓱民抿敏闽皿悯珉闵苠鳘岷僶冺刡勄呡垊姄崏忞慜憫捪敯旻旼暋渂潣琘琝瑉痻盿砇碈笢笽簢繩罠賯鈱錉鍲閔閩鰵鴖名命鸣铭螟暝茗酩佲凕姳慏掵朙榠洺猽眀眳覭詺鄍銘鳴谬謬摸磨末膜莫默魔模摹漠陌蘑寞秣瘼殁镆嫫谟貘茉馍耱劰勿嗼嚤嚩圽塻妺嫼帓帞怽懡昩暯枺橅歾歿爅皌眿瞐瞙砞礳粖糢絈縸纆艒莈藦蛨蟔謨謩譕貃銆鏌靺饃饝髍魩魹麿黙乮谋眸鍪哞劺厶婺恈敄桙毋洠蟱謀鞪鴾麰木母亩目墓牧穆暮牡拇慕睦姆钼毪沐仫苜凩墲娒峔幙慔楘樢氁炑牳狇畆畒畝畞畮砪胟莯蚞踇鉧鉬雮霂旀丆椧";
            //N
            PY[13] = "乃譺扭杻柠鳥鸟屰囝呐那拿哪纳钠娜南衲捺镎肭乸內内吶呶嗱妠抐拏挐淰笝箬篛納蒘蒳訤詉誽豽蹃軜鈉鎿雫靹魶耐奶奈氖萘艿柰鼐倷妳孻廼掜摨渿熋疓能腉螚褦迺釢錼难男赧囡蝻楠喃腩侽娚婻弇戁抩揇摊攤暔枏枬柟湳煵畘莮萳諵遖難囊馕曩囔攮儾哝噥嚢憹搑擃欜瀼灢蘘蠰譨饢鬞齉闹脑恼挠孬铙瑙垴蛲猱硇匘堖夒嫐峱嶩巎怓悩惱撓橈檂獶獿碯脳腝腦蝚蟯譊鐃閙鬧呢讷疒眲訥馁娞氝浽脮腇錗餒餧鮾鯘焾嫩恁媆嫰枘莻鈪銰啱你泥拟腻逆溺倪尼匿妮霓铌昵坭猊伲怩鲵睨旎伱儞堄婗嫟嬺孨孴屔嶷惄愵慝抳擬晲暱棿淣滠濘灄狔痆眤秜籾縌胒膩苨薿蚭蛪蜺觬貎跜輗郳鈮鉨鑈隬馜鯢麑齯年念捻撵碾粘廿黏辇鲇鲶卄哖姩撚攆涊秊秥簐艌蹨躎輦鮎鯰鵇娘酿嬢孃醸釀尿袅茑脲嬲嫋嬝蔦裊褭捏镍聂孽涅镊啮陧嗫臬蹑颞噛嚙囁囓圼孼嵲嶭巕帇惗揑敜枿槷櫱篞糱糵聶肀臲苶菍蠥褹諗讘踂踗踙躡鉩銸鋷錜鎳鑷钀闑隉顳齧您囜拰脌拧凝宁狞泞佞甯咛聍侫儜嚀嬣寍寕寗寜寧擰橣檸澝獰聹苧薴鑏鬡鬤鸋牛纽狃忸汼沑炄牜紐莥蚴靵浓农脓侬儂挊挵欁濃癑禯秾穠繷膿蕽襛農辳醲齈耨啂嬬搙擩槈檽獳羺譳怒努奴孥胬驽弩伖伮傉帑砮笯褥駑暖奻渜湪煖煗餪虐疟谑硸黁燶挪诺懦糯喏傩锘搦儺堧愞懧掿搻梛榒橠稬穤糑糥耎諾逽鍩女衄钕恧朒籹衂釹瘧";
            //O
            PY[14] = "哦毆喔筽夞乯昷鞰偶呕欧藕鸥沤殴怄瓯讴耦吘嘔塸慪握敺櫙歐渥漚熰甌紆纡腢膒蕅藲謳醧鴎鷗齵";
            //P
            PY[15] = "杷湃排派彭旁炮瀑袍棑菩葡平抨辟枇拚剽嫖頻频屏拼怕拍擗潑跑僕撲溥捧鵬鹏掊蹒蹣庞彯帕爬趴啪琶筢葩妑帊掱潖舥袙牌迫徘俳蒎犤猅簰輫鎃磗盘判攀畔叛磐胖襻泮爿乑冸媻幋槃沜洀溿瀊炍片牉皤盤眅蒰詊踫鄱鋬鎜鑻鞶頖鵥耪乓滂沗篣胮膖蠭覫霶龎抛泡咆狍匏庖疱脬垉奅拋爮犥皰礟礮萢褜謈軳鞄麅麭陪配赔呸胚佩培沛旆帔醅霈辔伂俖姵媐嶏怌抷斾昢毰浿淠珮肧蓜賠轡阫馷駍盆湓呠瓫翸碰棚砰蓬朋烹硼膨澎篷怦蟛嘭倗剻匉塜塳弸恲憉掽梈椖椪槰樥泙淎淜漰皏硑磞稝竼纄胓芃苹荓蟚軯軿輣輧鑝閛韸韼髼鬅鬔浌巼闏乶喸批皮披匹劈屁僻疲痞霹琵毗啤譬砒貔丕圮癖郫甓睥鼙邳铍罴噼蚍伓伾噽嚊嚭壀嫓岯憵扑朇榌毘毞渒潎澼炋焷狉狓疈疋痦睤磇礔礕秛秠篺簲羆耚脴膍苉苤蚽螷蠯豼豾釽鈹鉟銔銢錃闢阰隦頗顖颇駓髬魾鴄鵧鷿篇骗偏翩犏骈胼蹁谝囨媥楄楩腁覑諚諞貵賆駢騈騗騙骿魸攵丿票飘瓢朴螵瞟缥嘌勡慓旚皫磦縹翲薸醥闝顠飃飄魒氕嫳暼鐅品贫聘嫔榀姘牝颦嚬娉嬪朩玭矉礗穦薲蘋貧顰馪驞凭瓶评乒萍坪鲆枰俜凴呯塀娦屛岼帡帲幈慿憑洴涄焩玶甁甹砯竮箳簈缾聠艵蓱蚲蛢評郱頩鮃破坡婆粕笸钋攴叵珀钷嘙尀岥岶廹敀椺櫇洦溌烞皛砶蒪酦釙鉕鏺駊剖咅堷抔犃颒兺哛铺谱仆曝圃浦普镨噗匍濮氆蹼璞镤圑圤墣巬巭暜樸檏潽炇烳痡瞨穙舖菐蒱諩譜贌酺鋪鏷鐠駇";
            //Q
            PY[16] = "奇鉗钳墽淺趫呛嗆抢搶槍鎗詘诎胊荃趣趨且戚汽区區祈鈐钤洽搉期棋蘄褀騎骑齊浅前纤葥钱强強乔却卻喬橋契蜻靑青娶圈圏弮屈頎颀穹羫腔缺頃顷泣剠犭起七气器妻欺漆启柒岂砌弃祁凄企乞歧栖畦脐崎迄沏讫旗祺骐屺岐蹊桤憩萋芑汔鳍俟槭嘁蛴綦亓欹琪麒琦蜞圻杞葺碛淇耆绮亝倛傶僛切剘勤吱呇咠唘唭啓啔啟噐埼夡娸婍宿岓嵜忔忮忯恓悽愭慼慽憇捿掑斉斊旂暣朞栔桼梩棄棊棨棲榿檱櫀欫気氣洓淒渍渏湆湇滊漬濝炁猉玂玘甈疧盀盵碁碕碶磎磜磧磩禥竒簯簱籏粸紪綥綨綮綺緀緕纃缼罊肵臍艩芞藄蚑蚔蚚蜝螇螧蠐衹裿褄訖諆諬諿趿軝迉邔郪釮錡鏚锜闙霋騏騹鬐鬿魌魕鮨鯕鰭鲯鵸鶀鶈麡齮恰掐葜佉冾圶帢拤殎硈磍跒酠鞐千牵签欠铅钎迁谴谦潜歉扦遣黔仟岍褰箝掮搴倩慊悭愆虔芡荨缱佥芊阡肷茜椠伣俔僉儙刋嗛圱圲墘壍奷媊孅孯寨岒嵰廞忴悓慳扲拑拪掔撁撍攐攑攓杄杴棈榩槧橬檶櫏歬汘汧潛濳灊炶煔燂牽皘竏箞篏篟簽籖籤粁綪縴繾羟羥羬膁臤苂茾葴蒨蔳蕁藖蚈蚙蜸諐謙譴谸軡輤遷釺鈆鉛鍼鎆鏲鑓韆顅騚騝鬜鬝鰬鵮鹐枪墙羌蔷蜣跄戗襁戕炝镪锖锵樯嫱唴啌嗴墏墻嬙嶈庆廧慶斨檣溬漒熗牄牆猐獇玱琷繈繦羗羻艢薔蘠親謒跫蹌蹡錆鏘鏹兛瓩桥瞧敲巧翘锹鞘撬悄俏窍雀峭橇樵荞跷硗憔谯愀缲诮劁僺喿嘺塙墝墧帩幓幧槗橾殼毃燆犞癄睄硚硝碻礄窯竅箾繰翹荍菬藮誚譙趬踃踍蹺躈郻鄡鄥銚鍫鍬鐈陗鞩韒頝顤顦髚髜怯窃郄惬锲妾箧倿匧厒唼帹悏愜朅癿稧穕竊笡篋籡緁聺苆藒踥鍥鐑鯜亲琴侵擒寝秦芹沁禽钦吣覃衾芩溱嗪螓噙揿檎吢唚坅埁媇嫀寑寢寴嵚嶔庈懃懄抋捦搇撳昑梫欽澿瀙珡琹瘽矝笉綅耹菣菦蓁藽螼誛赺赾鈊鈙雂靲顉駸骎鬵鮼鳹请轻清情晴氢倾擎卿氰圊謦苘黥罄鲭磬傾凊勍啨埥夝寈庼廎掅暒棾樈檾櫦氫淸漀甠硘碃請軽輕郬鑋鯖穷琼邛茕銎筇儝卭嬛惸憌桏橩焪焭煢熍瓊瓗睘窮竆笻藑藭蛬赹求球秋丘泅邱囚酋楸蚯裘糗巯逑俅虬赇鳅犰鼽遒丠厹叴唒坵媝崷巰恘扏搝梂櫹殏毬氽汓浗渞湭煪玌璆皳盚秌穐篍紌絿緧肍莍蘒虯蛷蝵蟗蠤觓觩訅賕逎釚釻銶鞦鞧鮂鯄鰌鰍鰽鱃鵭鶖鹙龝去取曲驱躯龋戌祛蕖磲劬阒麴癯衢黢璩氍觑蛐岖伹佢刞匤厺岴嶇憈戵抾斪欋浀淭灈璖竬筁粬紶組絇组翑胠臞菃葋蝺蟝蠷衐袪覰覷覻詓躣軀鑺閴闃阹駆駈騶驅驺髷魼鰸鱋麮麯麹鼁鼩齲全权劝拳犬泉券颧痊铨筌绻诠畎鬈悛佺勧勸姾婘峑巏巻恮拴搼栒椦楾槫権洤湶灥烇牶牷犈瑔甽硂絟綣縓葲虇觠詮謜譔跧銓鐉闎顴駩騡鰁鳈齤确瘸鹊榷悫崅愨慤汋燩獡皵碏確礭舃舄蒛鵲群裙逡囷夋峮帬羣裠輑";
            //R
            PY[17] = "蕊任攘亻亽罖囕染燃然髯苒蚺呥嘫姌媣橪珃繎肰舑蒅蚦衻袇袡髥让嚷瓤壤穰禳儴勷壌忀懹欀爙獽穣纕譲讓躟鑲镶饶绕扰荛桡娆嬈擾犪穘繞蕘襓遶隢饒热若惹偌捼渃熱人忍认刃仁韧妊纫壬饪轫仞荏葚衽稔仭刄姙屻忈忎扨朲栠栣梕棯牣秂秹紉紝絍綛纴肕芢荵袵訒認讱躵軔鈓銋靭靱韌飪餁魜鵀仍扔戎礽芿辸日囸氜鈤馹驲容绒融溶熔荣蓉冗茸榕狨嵘蝾傇傛媶嫆嬫宂峵嵤嶸搈摉曧栄榮榵毧氄瀜烿爃瑢穁穃絨縙縟缛羢茙螎蠑褣鎔镕駥肉揉柔糅蹂鞣媃宍楺渘煣瑈瓇禸粈腬葇輮鍒鑐韖騥鰇鶔如入汝儒茹乳辱蠕孺蓐襦铷嚅薷颥溽洳侞偄咮嗕媷嶿帤扖曘杁桇燸筎繻肗蕠袽込鄏醹銣顬鱬鳰鴑鴽软阮朊壖撋瑌瓀碝礝緛蝡軟瑞睿芮蚋蕤叡壡惢桵棁橤汭甤笍綏緌繠绥蘂蘃蜹踒润闰橍潤閏閠弱叒嵶楉焫爇蒻鄀鰙鰯鶸";
            //S
            PY[18] = "蛇諰柵栅赦僧刹剎裳尙尚紹綤沈盛匙飭飾餝饰淑司衰石受涉陏隋市舌拾氏身圣颯飒涁渗滲蕯率爍紗纱绳摂摄攝姍姗审審纟扌礻饣彡氵示捎声胜頌颂苼撒洒萨挲仨卅脎摋桬櫒殺泧潵薩訯躠鈒鏾钑隡霅靸馺栍塞腮鳃思赛噻僿嗮嘥愢毢毸簑簺賽顋鰓乷虄三散伞叁馓糁毵霰俕傘厁壭弎橵毶毿犙糂糝糣糤繖蔘閐饊鬖桑丧嗓颡磉搡喪桒槡褬鎟顙扫嫂搔骚梢埽鳋臊缫瘙哨掃掻氉溞煰燥矂縿繅颾騒騷髞鰠鱢色涩瑟啬铯穑嗇愬懎擌歮歰渋溹澀澁濇濏瀒璱瘷穡穯繬虩譅轖銫鎍鎩鏼铩闟雭飋裇聓森槮襂鬙閪縇杀沙啥傻砂莎厦煞杉鲨霎痧裟歃倽儍唦噎廈挱榝樧猀硰箑繌繺翜翣萐蔱賒賖赊閯閷魦鯊鯋晒筛酾曬篩簁簛籭術山闪衫善扇删煽珊膳陕汕擅缮蟮芟跚鄯潸鳝剡骟疝讪钐舢埏傓僐刪剼圸墡嶦挻掞搧敾晱曏曑杣椫樿檀檆潬澘灗烻熌狦痁睒磰笘繕羴羶葠襳覢訕謆譱赸軕邖釤銏鐥閃閊陝顃饍騸鯅鱓鱔鱣鳣上伤商赏晌墒熵觞绱殇丄仩傷恦慯殤滳禓緔蔏螪觴謪賞踼鑜鞝鬺少烧稍邵韶苕劭潲艄蛸筲佋卲娋弰搜旓柖溲焼燒燿玿笤綃绡莦萔萷蕱袑輎颵髾鮹社射设舍慑奢厍畲猞麝佘厙弽慴懾捨檨欇涻畬磼舎葉蔎虵蛥蠂設輋韘騇谁伸深婶神甚肾申绅呻砷什娠慎莘诜矧椹渖蜃哂侁侺兟堔妽姺嬸孞屾峷弞愼扟搷昚曋柛椮榊氠燊珅甡甧瘆瘮眒眘瞫矤祳穼籶籸紳罧脤腎蓡薓蜄裑覾訠訷詵讅谉邥鋠頣駪魫鯓鰰鵢省剩生升甥牲眚笙偗剰勝呏墭憴斘昇晠曻枡榺橳殅泩渻湦焺狌珄琞竔箵縄聖聲蕂譝貹賸鉎鍟阩陞陹鵿鼪是使十时事室师试史式识虱矢屎驶始似士世柿拭誓逝势嗜噬失仕侍释狮食恃蚀视实施湿诗尸豕莳埘铈舐鲥鲺贳轼蓍筮炻谥弑螫丗乨亊佀佦兘冟勢卋厔呞呩噓埶塒奭媞嬕実宩寔實寺屍峕嵵師弒徥忕恀惿戺揓斯旹昰時枾栻榁榯檡洂浉湜湤溡溮溼濕烒煶狧狶獅瑡痑眂眎眡睗祏禵秲竍笶笹箷篒簭籂絁胑舓葹蒒蒔蝨褆褷襫襹視觢試詩諡謚識貰跩軾辻遈遾邿醳釈釋釶鈰鉂鉃鉇鉈鉐鉽銴鍦飠餙馶駛魳鮖鯴鰘鰣鰤鳲鳾鶳鸤鼫鼭齛齥兙瓧手收首守瘦授兽售熟寿艏狩绶収垨壽夀涭獣獸痩綬膄鏉书树数输梳叔属束术述蜀黍鼠赎孰蔬疏戍竖墅庶薯漱恕枢暑殊抒曙署舒姝秫纾沭毹腧塾殳澍倏俆倐儵咰嗽婌尌尗屬庻忬怷悆捒掓數書朮杼柕樹毺涑潄潻濖瀭焂瑹璹疎癙稌竪籔糬紓紵絉綀翛荗蒁蒣薥藷虪蠴蠾裋謶豎豫贖踈軗輸鄃鉥錰鏣陎隃鮛鱪鱰鵨鶐鷸鹬鼡刷耍唰唆涮誜摔甩帅蟀帥栓闩腨閂双霜爽孀傱塽孇慡樉欆灀礵縔艭鏯雙騻驦骦鷞鸘鹴水睡税说娷帨挩捝氺涗涚稅脽裞說説誰閖顺吮瞬舜恂橓眴瞚瞤蕣順鬊硕朔搠妁槊蒴哾嗍欶洬溯濯矟碩鎙四死丝撕私嘶肆饲嗣巳耜驷兕蛳厮汜锶泗笥咝鸶姒缌祀澌亖価俬傂儩凘噝娰媤孠廝恖杫柶楒榹泀泤洍涘瀃燍牭磃禗禠禩竢糹絲緦罒罳肂肄菥蕬蕼虒蜤螄螔蟖蟴覗謕貄逘釲鈻鉰銉銯鋖鍶鐁颸飔飤飴飼駟騦鷉鷥鼶螦乺送松耸宋诵怂讼竦菘淞悚嵩凇崧忪倯吅娀嵷庺愯慫憽捴揔摗枀枩柗梥檧濍硹聳蜙訟誦鎹餸駷鬆艘擞嗾嗖飕叟锼馊瞍螋傁凁叜廀廋捜撨擻棷櫢潚獀瘶蓃謏鄋醙鎪颼餿騪素速诉塑俗苏肃粟酥缩僳愫簌觫稣夙嗉谡蔌傃僁卹囌埣塐嫊憟梀榡樎樕櫯殐泝溸潥玊珟璛甦碿稡穌窣粛縤縮肅膆莤藗蘇蘓訴謖趚蹜遡遬鋉餗驌骕鯂鱐鷫鹔酸算蒜匴痠祘笇筭岁随碎虽穗遂髓隧祟谇濉邃燧荽亗倠哸嗺夊嬘嵗旞檖歲歳滖澻瀡煫璲瓍睟砕禭穂穟繐繸膸芕荾蓑襚誶譢賥遀鐆鐩隨雖鞖髄孙损笋榫荪飧狲隼孫巺損搎槂潠猻畃筍箰簨蓀蕵薞跣鎨鶽所锁琐索梭唢桫嗦娑羧傞嗩摍暛溑琑瑣璅簔莏蜶趖逤鎖鎻鏁髿鮻";
            //T
            PY[19] = "体土兎兔涛濤填樘橖趟她抬拖提沱涂跿枱塔沓坛潭烫燙忑题倜挑跳踢鐡鐵筒豚陀亠团汤湯漡铊透台他它踏拓獭挞蹋溻榻遢闼侤傝咜嚃嚺墖太崉撻榙毾涾澾濌牠獺祂禢褟誻譶蹹躢遝錔闒闥阘鞜鞳襨态胎苔泰酞汰炱肽跆鲐钛薹邰儓冭囼坮夳嬯孡忲態擡旲檯溙炲珆籉粏臺舦鈦颱鮐谈叹探碳贪痰毯坦炭瘫谭坍袒钽郯锬昙嗿嘆埮墰墵壜婒怹憛憳憻擹暺曇榃歎湠璮癱罈罎舔舕菼蕈藫襢談譚譠貚貪賧醓醰鉭錟鷤躺堂糖塘唐搪棠膛螳羰醣瑭镗傥饧溏耥铴螗傏劏啺嘡坣戃摥曭榶漟煻爣矘磄禟篖糃糛膅蓎薚蝪赯蹚鄌鎕鎲鏜鐋钂镋隚鞺餳餹饄鶶鼞套掏逃桃讨淘滔绦萄鼗啕饕韬仐匋咷夵嫍幍弢慆抭搯梼槄瑫祹絛綯縚縧绹蜪裪討詜謟迯醄鋾鞀鞉鞱韜飸饀駣騊畓特铽忒脦螣蟘貣鋱疼腾藤誊滕儯幐漛籐籘縢膯虅謄邆霯駦騰驣鰧鼟唞朰替剃剔梯锑啼涕嚏惕屉醍鹈绨缇裼逖悌偍厗嗁嚔屜崹悐惖戻挮掦擿桋歒殢洟漽瑅瓋稊穉籊綈緹蕛薙褅趧趯躰軆逷遆銻鍗騠骵體髰鬀鮧鮷鯷鳀鴺鵜鷈天田添甜恬腆掭阗忝殄畋倎兲吞呑唺娗婖寘屇悿晪沺淟湉琠瑱璳甛畑畠睓睼碵磌窴胋舚菾覥觍賟酟錪闐靔靝餂鴫鷆鷏黇条迢眺龆祧窕鲦宨岧岹庣恌斢旫晀朓條樤祒窱聎脁脩艞芀蓚蓨螩覜趒鋚鎥鞗鰷齠铁贴帖萜餮聑蛈貼鉄銕飻驖听停挺厅亭艇庭廷莛婷梃霆侹厛圢嵉庁廰廳桯楟榳涏渟烶珽筳綎耓聤聴聼聽脡蝏誔諪邒閮鞓頲颋鼮乭同通痛铜桶捅统童彤瞳茼仝砼恸佟嗵哃囲峂庝慟憅晍曈樋氃浵炵熥犝狪獞痌眮硧秱穜粡統綂蓪蚒蜼赨鉖鉵銅餇鮦鲖头偷偸妵婾媮敨紏緰蘣鍮頭黈图吐秃突徒凸途屠酴钍菟堍凃唋図圖圗圡堗峹嵞嶀庩廜悇捸揬汢涋湥潳痜瘏禿筡莵葖蒤迌釷鈯鋵鍎馟駼鵌鵚鵵鶟鷋鷵鼵湍疃抟剸団圕塼嫥慱摶檲漙煓猯畽磚篿糰褖貒鏄鷒鷻腿推退褪颓蜕煺侻俀僓啍娧尵穨聉脫脱蓷藬蘈蛻讉蹆蹪隤頹頺頽駾骽屯臀饨暾坉旽朜涒窀臋豘軘霕飩魨鲀黗托妥驼椭唾鸵柝跎乇坨佗庹鼍箨砣侂咃堶岮彵撱杔杝楕槖毤毻汑涶狏砤碢籜紽莌萚蘀袉袥託讬飥饦馲駝駞騨驒驝魠鮀鰖鴕鵎鼉鼧";
            //U
            PY[20] = "屗徚斏曢朑桛歚毜毝毮洜烪焑焽燞癷皼祍稥耂聁聣艈茒蒊蓞藔虲蝊袰贘躼辪鍂鎼鐢闧霻鶑";
            //V
            PY[21] = "";
            //W
            PY[22] = "伪莞娃洼汪皖綄脘违韋完頑顽味万无無袜婑尢吴挖瓦佤娲腽劸咓啘嗢媧屲徍搲攨汙汚污溛漥瓲畖砙穵窊窪膃襪邷韈韤黳鼃外歪崴喎竵顡晚碗玩弯挽湾丸腕宛婉烷豌惋蜿绾芄琬纨剜畹菀乛倇刓卍卐唍埦塆壪夗妧岏帵彎忨惌抏捖捥晩晼杤梚椀涴潫灣琓盌睕笂箢紈綩綰翫脕苋莧萬蚖貦贃贎踠輓邜鋔鋺鍐骩骪骫魭望忘王往网亡旺妄辋魍惘罔亾仼兦尣尩尪尫彺徃暀朢棢瀇焹網莣菵蚟蛧蝄誷輞迬为未围喂胃微尾威伟卫危委魏唯维畏惟巍蔚谓尉潍纬慰萎苇渭葳帏艉鲔娓逶闱隈玮涠帷诿洧偎猥猬嵬軎韪炜煨圩薇痿倭偉儰厃叞唩喡喴圍墛壝媁媙媦寪峗峞崣嵔嶶幃徫愄愇懀捤揋揻斖暐梶椳楲欈沇洈浘渨湋溈潿濰烓煀煟熨熭燰爲犚犩猚琟瑋璏痏癓硙碨維緭緯縅罻腲芛苿茟荱菋葦葨蓶蔿薳藯蘶蜲蝛蝟螱衛衞褽覣覹觹觽觿詴諉謂讆讏躗躛轊鄬醀鍏鍡鏏闈阢隇霨霺韑韙韡頠颹餵饖鮇鮠鮪鰃鰄鳂鳚问文闻稳温吻蚊纹瘟紊阌刎雯璺呚問塭妏彣忟愠慍抆揾搵桽榅榲殟溫炆煴珳瑥瘒穏穩紋緼縕缊聞肳脗芠蕰薀蘊蚉螡蟁褞豱輼轀辒鈫鎾閺閿闅闦韞韫饂馼魰鰛鰮鳁鳼鴍鼤翁嗡瓮蕹蓊勜塕壅奣嵡暡滃甕瞈罋聬螉鎓鶲鹟齆我窝卧挝沃蜗幄龌肟莴仴偓婐媉捾撾杌枂楃涹濣猧瓁瞃窩腛臒臥萵馧齷五屋物舞雾误捂悟钨武戊务呜伍午吾侮乌诬芜巫晤梧坞妩蜈牾寤兀怃邬唔忤骛鋈仵鹜迕焐庑鹉鼯浯圬乄伆俉倵儛剭吳呉啎嗚塢奦娪娬嫵屼岉峿嵨廡弙御忢悞悮憮扜扤摀旿杅杇橆歍洖溩潕烏熃熓玝珷珸瑦璑甒矹碔祦禑窏窹箼粅茣莁蕪螐誈誣誤躌逜郚鄔鋙錻鎢铻隖靰騖鯃鰞鴮鵐鵡鶩鷡鹀齬龉";
            //X
            PY[23] = "嚣囂郩虑斜醒啸嘨嘯屟絮昔涎邢虾吓軒轩行巷戏許许学學系谿瘕夏挾揳钘险險穴旬兄伈盷幸需盻忄削琁歙信冼姓嘘选選巡廵徙析些牺犧獻錫锡綉绣鞋限西洗细吸席稀溪熄膝息袭惜习嘻夕悉矽熙希檄晰媳硒铣烯隙汐犀蜥奚浠葸饩屣玺嬉禊兮翕穸禧僖淅蓰舾醯郗欷皙蟋羲隰唏曦樨粞熹觋鼷係俙傒凞匸卌卥厀唽噏嚱塈壐嬆屃屖屭嵠嶍巇徆徯忥怬怸恄悕惁慀憘憙扸晞晳暿枲桸椞榽橀橲歖氥渓漇漝潝潟澙焁焈焟焬煕熂熈熺熻燨爔犔犠琋璽瘜睎瞦磶礂窸細綌緆縰繥绤羛習翖肹胁脅脇脋莃葈蒠蒵蓆蕮薂蜴蟢蠵衋襲覀覡覤訢誒謑譆豀豨豯貕赥赩趇趘蹝躧郋郤鄎酅釳釸鈢銑鎴鏭鑴隟隵霫霼飁餏餼饻騱騽驨鯑鰼鱚鳛鵗鸂黖下峡瞎霞狭匣侠狎黠硖罅遐瑕丅俠傄圷峽懗敮梺溊炠烚煆狹珨疜睱硤碬祫筪縀縖翈舝舺蕸谺赮鍜鎋鏬閕陜陿颬騢魻鰕鶷先线县现显闲献嫌陷鲜弦衔咸锨仙腺贤宪舷羡藓岘痫籼娴蚬猃祆跹酰暹氙鹇筅仚伭佡僊僩僲僴咞哯啣嘕垷奾妶姭娨娹婱嫺嫻尟尠屳峴崄嶮幰廯忺憪憲憸挦揱搟撊撏攇晛枮櫶毨澖瀗灦烍狝獫獮玁玹珗現甉癇癎県睍礥禒秈箲粯絃絤綖綫線縣繊缐羨胘臔臽苮蘚蚿蛝蜆衘褼誢誸譣豏賢贒赻蹮躚軐銜鋧鍁鍌鑦閑陥険韅韯韱顕顯馦鮮鱻鶱鷳鷴鷼麙麲黹鼸想向象项响香乡相像箱享厢翔祥橡详湘襄飨鲞骧蟓庠芗饷缃葙亯佭勨嚮姠嶑廂晑栙楿珦瓖絴緗缿羏膷萫薌蚃蠁襐詳跭郷鄉鄊鄕鐌響項餉饗饟驤鮝鯗鱌鱜鱶鴹麘小笑消销萧效宵晓肖孝淆霄哮魈骁枵哓崤筱潇逍枭箫侾俲傚効咲咻嘋嘵婋宯庨彇恷敩斅斆暁曉梟歊殽毊洨涍滧澩瀟灱灲烋焇熽爻猇獟獢痚痟皢硣窙筿篠簘簫膮蕭虈虓蟂蟏蟰蠨詨誟誵銷驍髇髐鴞鴵鷍鷕鸮写歇血谢卸屑蟹泻懈泄楔邪协械谐携绁缬榭廨撷偕瀣亵榍邂薤躞燮勰伳偞偰冩協卨嗋噧垥塮夑娎媟寫屓屧峫徢恊愶拹擕擷攜旪暬枻洩澥瀉灺炧炨熁燲爕瑎祄禼糏紲絏絬綊緤緳纈缷翓膎薢藛蝑蝢蠍蠏衺褉褻襭諧謝讗鍱鐷鞵韰齂齘龤新心欣芯薪锌辛寻衅忻歆囟馨鑫伩俽兴噺妡嬜尋惞憖杺枔潃炘焮盺興舋襑訫邤釁鋅鐔阠馫性型形星腥刑杏猩惺悻荥擤荇侀倖哘垶娙婞嫈嬹曐洐涬滎煋瑆皨睲篂緈臖莕蛵裄觪觲謃鉶铏騂骍鮏鯹胸雄凶熊汹匈芎兇夐忷恟敻昫洶胷訩詗詾讻诇修锈休羞嗅袖秀朽貅馐髹鸺庥岫俢樇滫烌煦珛琇璓糔綇繍繡脙臹苬莠螑褎褏銝銹鎀鏅鏥鏽飍饈髤鮴鵂齅须虚蓄续序叙婿徐旭绪吁酗恤墟糈栩蓿顼洫胥醑诩溆盱伵侐偦冔勗喣垿壻姁媭嬃幁怴惐敍敘旴暊朂楈欨欰歔殈汿沀淢湑漵潊烅烼獝珝珬疞盢盨瞁瞲稰稸窢籲続緒緖縃續聟芌芧蕦藇藚虗虛蛡訏訹詡諝謣譃谞賉鄦雩須頊驉鬚魆魖魣鱮悬旋玄宣喧绚癣暄楦儇渲漩铉璇煊碹镟炫揎萱痃谖咺塇媗嫙弲怰愃愋懸昍昡晅暶楥檈洵琄瑄璿癬睻矎禤箮絢縼繏翧翾萲蓒蔙蕿藼蘐蜁蜎蝖蠉衒袨諠諼譞贙鉉鍹鏇颴駨駽鰚雪靴薛鳕泶乴坹岤峃嶨斈桖樰瀥烕燢狘疶膤艝茓蒆袕謔轌辥雤鞾鱈鷽鸴讯熏训循殉迅驯汛逊勋询巽鲟浔埙醺峋薰荀窨曛徇伨侚偱勛勲勳卂噀噚嚑坃塤壎壦奞愻揗攳杊桪樳殾毥潯燅燖燻爋爓狥珣璕矄稄筼篔紃纁臐薫蘍蟫蟳訊訓訙詢賐迿遜鄩鑂顨馴鱏鱘";
            //Y
            PY[24] = "噫礙賹醷遃闇陰隂仰頨呹移涌怞踰約约甬逾亚亜硬羽瑗越妖吟訡瀅熒乙勻匀妪嫗阴游药藥蔫喦嵒疑遇彦廴衤讠邺月抴吲媵液繹绎俞兪野以饴遗遺炎姚荑跃躍畇恿愉褕諭谕余墿袘迆迤迱阤陁夭园有眼蕴藴於义吚咦義呀岈疨矣洋叶耶頁页研硏于亐伃芋压牙芽鸭崖涯丫雅衙鸦讶蚜砑琊桠睚娅痖氩伢迓揠俹倻劜厊厌厓厭圔圠堐壓婭孲崕庌庘挜掗枒椻氬浥漄犽玡瑘瘂稏窫笌聐蕥襾訝釾錏鐚铔顔颜鴉鴨鵶鼼齖烟沿盐言演严咽淹掩宴岩延堰验艳殷阉砚雁唁焰衍谚燕阎焉奄芫厣菸魇琰滟焱赝筵兖餍恹罨湮偃谳胭晏闫俨郾酽鄢妍鼹崦嫣乵偐偣傿儼兗円剦匽厳厴喭噞嚥嚴塩墕壛壧妟姲姸娫娮嬊嬮嬿孍嵃嵓嶖巌巖巗巘巚彥愝懕戭扊抁揅揜昖暥曕曮棪椼楌樮檿櫩殗氤洇淫渰渷漹灎灔灧灩焔煙熖燄牪狿猒珚琂甗硯硽碞礹篶簷縯臙艶艷莚葕蔅虤蝘褗覎觃觾訁訮詽諺讌讞豓豔贋贗躽軅郔酀酓醼釅閆閹閻阭隁隒靥靨顏饜騐験騴驗驠鬳魘鰋鳫鴈鶠鷃鷰鹽麣黡黤黫黭黰黶鼴齗齞齴龂龑样养羊扬秧氧痒杨漾阳殃央鸯佯疡炀恙徉鞅泱蛘烊怏佒傟咉坱垟奍姎岟崸慃懩抰揚攁敭旸昜映暘柍楊楧様樣氱瀁炴煬珜癢眏眻礢紻羕羘羪胦英詇諹軮輰鉠鍈鍚鐊钖阦陽雵霙霷颺飏養駚鰑鴦鸉要摇腰窑舀邀谣遥瑶耀尧钥珧鳐鹞吆崾肴曜徭杳窈倄偠傜喓嗂垚堯婹媱宎尭岆峣嶢嶤幼徺怮愮揺搖摿暚曣枖柼楆榚榣殀溔滛瀹烑熎狕猶猺瑤由矅磘祅穾窅窔窰筄繇苭葯葽蓔薬蘨袎覞訞詏謠謡讑遙鎐鑰闄靿颻飖餆餚騕鰩鷂齩也夜业爷掖腋冶曳椰谒晔烨揶铘亪亱僷吔嚈埜墷壄嶪嶫拽捓捙擛擨擪擫暍曄曅曗枼枽業歋漜潱澲煠燁爗爺皣瞱瞸礏蠮謁鄓鄴鋣鎁餘餣饁馀馌驜鵺鸈黦岃膶一已亿衣依易医仪亦椅益姨翼译伊胰沂宜异彝壹蚁谊铱翌艺抑役臆逸疫颐裔意毅忆夷溢诣议怿痍镒癔怡驿旖熠酏翊峄圯殪懿劓漪咿瘗羿弈苡佾贻钇缢刈悒黟翳弋奕埸挹薏呓镱舣亄伇伿侇俋偯儀億兿冝劮勚勩匇匜印呭唈囈圛坄垼壱夁嫕嫛嬄嬑嬟宐宧寱寲峓崺嶧嶬巸帟帠庡廙弌弬彛彜彞怈恞悘悥憶懌扅扆撎擇攺敡敼旑晹暆曀曎曵杙枍栘栧栺棭椬椸榏槸樴檍檥檹櫂欭歝殔殹毉洢浂浳湙潩澺瀷炈焲熤熪熼燚燡燱獈玴瑿瓵畩異痬瘞瘱瞖礒祎禕秇稦穓竩笖簃籎縊繄繶羠耴肊膉艗艤芅苅苢萓萟蓺藙藝蘙虉蛜蛦螘螠衪衵袣裛襗襼觺訲訳詍詣誼譩譯議讛豙豛豷貖貤貽贀跇跠輢轙辷迻郼醫釔釴鈘鈠鉯銥鎰鏔鐿陭隿霬靾頉頤頥顊顗駅驛骮鯣鳦鶂鶃鶍鷁鷊鷖鷧鷾鸃鹝鹢鹥黓黝齸因引银音饮隐荫尹寅茵姻堙鄞喑夤胤狺霪蚓铟瘾茚乚侌冘凐噖噾嚚囙圁垔垽婣婬峾崟崯嶾廕愔慇慭憗懚斦朄栶檃檭檼櫽歅殥泿洕淾湚溵濥濦烎犾猌璌瘖癊癮碒磤禋秵筃粌絪緸荶蒑蔩蔭蘟螾裀訔訚誾諲讔趛酳鈏鈝銀銦闉阥陻隠隱霒霠靷鞇韾飮飲駰骃鮣鷣应影营迎蝇赢鹰颖莹盈婴樱缨荧萤萦楹蓥瘿茔鹦莺璎嘤撄瑛滢潆嬴罂瀛膺颍偀僌営噟嚶塋媖嬰孆孾巊廮応愥應摬攍攖攚暎朠桜梬櫻櫿渶溁溋潁濙濚濴瀠瀯瀴灐灜煐珱瑩瓔甇甖癭盁矨碤礯穎籝籯緓縈纓绬罃罌耺膡萾藀蘡蛍蝧蝿螢蠅蠳褮覮譍譻賏贏軈鐛鑍锳韺鴬鶧鶯鷪鷹鸎鸚哟育唷喲用永拥蛹勇雍咏泳佣踊痈庸臃慵俑墉鳙邕喁饔镛勈嗈噰埇塎嫞嵱廱彮怺悀惥愑愹慂擁柡栐槦湧滽澭灉牅癕癰砽禜苚蒏詠踴郺鄘醟銿鏞雝顒颙鯒鰫鱅鲬鷛又右油优友铀忧尤犹诱悠邮酉佑釉幽疣攸蚰鱿卣莸猷宥牖囿柚蝣鼬铕呦侑丣亴偤優哊唀嚘姷孧峟峳庮怣憂懮栯梄楢櫌櫾沋泈湵滺瀀牗狖祐禉秞糿纋羐羑耰聈肬脜苃蕕蜏訧誘貁輏輶迶逌逰遊邎鄾酭鈾銪駀魷鮋鲉麀与欲鱼雨语愈狱玉渔予誉愚虞娱淤舆屿禹宇迂域郁盂喻峪粥渝榆隅浴寓裕预驭嵛阈鹆妤窳觎舁蓣煜钰谀竽瑜禺聿欤俣伛圄庾昱萸瘐圉瘀饫毓燠腴狳蝓俁俼偊傴匬唹喅喐喩噊噳圫堉堣堬娛娯媀嬩寙崳嵎嶎嶼庽彧慾懙戫扵挧敔斔斞旟棛棜棫楀楡楰櫲欎欝歈歟歶淯湡滪漁澞澦灪灹焴燏爩牏獄玗玙琙瑀璵畭瘉癒盓睮砡硢礇礖礜祤禦秗稢稶穥穻箊篽籅籞緎罭羭與艅苑茰荢萮蒮蓹蕍蕷薁蘌蘛虶蜟螸衧袬覦語諛譽貐軉輍輿轝迃逳遹邘鄅酑鈺鋊錥鍝鐭閾陓隩雓霱預飫饇馭騟驈骬髃鬰鬱魚鮽鯲鰅鱊鳿鴥鴧鴪鵒鷠鸆鸒麌远员元院圆原愿猿怨冤源缘袁渊垣鸳辕鼋橼媛爰眢鸢沅螈塬傆允剈厡厵員噮囦圎園圓妴媴嫄嬽寃杬棩榞榬櫞淵渁渆渕湲溒灁猨獂盶禐笎緣縁羱肙葾蒬薗蜵蝝蝯衏裫褑褤貟贠轅逺遠邍邧酛鈨鎱陨隕願駌騵鳶鴛鵷鶢鶰鹓黿鼘鼝阅岳悦曰粤钺刖龠樾嬳岄嶽彟彠恱悅戉抈捳曱爚玥矱礿禴篗籆籥籰粵蘥蚎蚏跀軏鈅鉞閱閲鸑鸙龥云运晕韵孕耘酝郧氲恽郓芸昀狁殒纭伝傊呍喗囩夽奫妘惲愪抎抣暈枟橒殞氳沄涢溳澐熅熉磒秐紜縜繧腪荺蒀蒕蒷蕓賱運鄆鄖醖醞雲霣韗韻頵馻齫齳";
            //Z
            PY[25] = "蹔澡造楂崭沾胀脹縐诌宅枕棧缜徴浈歭滞滯祇种烛盅種圳妯祝著诸跦幢綴缀腏錣奏揍族卒最逐赵趙窒至螲竺追杂找隻祖租足邹燭爥总搱輾辗庄丶夂丬祗扺支枝招召折震殖峙唑汁肢朱鐲镯專撰犆職鐟鎭鎮镇占怗飳玆饌馔札这這妷择众砸咋匝扎咱咂拶喒帀桚沞沯紥紮臜臢襍鉔雑雜雥韴在再灾载栽宰哉甾崽仔傤儎扗洅渽溨災烖睵縡菑賳載酨暂赞簪趱糌瓒昝錾偺儧儹兂寁揝暫瓉瓚禶簮讃讚賛贊趲鄼鏨鐕饡脏葬赃奘驵塟弉牂臓臟賍賘贓贜銺駔髒早遭糟灶枣凿躁藻皂噪蚤唣唕慥栆梍棗璪皁竃竈簉艁薻譟趮蹧醩鑿则责泽箦舴迮啧仄昃笮赜伬則啫嘖夨崱庂択捑昗柞樍歵汄沢泎溭皟瞔矠礋簀耫葃蔶蠌謫謮讁谪責賾飵鸅齚贼蠈賊鰂鱡鲗怎囎譛赠憎综罾甑锃増曽熷璔矰磳綜譄贈鄫鋥鬷鱛炸渣眨榨乍诈铡喳砟痄吒哳蚱揸咤齄偧劄厏宱怍抯拃挓搾摣柤樝溠牐皶箚苲蚻詐譇譗踷迊醡鍘鮓鮺鲊鲝齇摘窄债斋砦債厇夈抧捚斎榸粂鉙齋站战盏毡展栈蘸绽斩瞻谵搌旃偡嫸嶘惉戦戰斬旜栴桟氈氊琖盞綻菚薝虥虦蛅覱譫讝趈輚轏邅醆閚霑颭飐饘驏驙魙鸇鹯张章帐仗丈掌涨账樟杖彰漳瘴障仉嫜幛鄣璋嶂獐蟑傽墇帳幥張慞扙暲涱漲痮瘬瞕礃粀粻蔁賬遧鏱鐣餦騿鱆麞着照罩爪兆昭沼肇棹钊笊诏啅垗妱巶旐曌枛炤燳爫狣瑵盄瞾罀羄肁肈詔釗鉊鍣駋鮡者遮蛰哲蔗辙浙柘辄赭鹧磔蜇啠喆嗻嚞埑嫬悊慹晢晣樜歽淛潪砓籷粍虴蟄蟅襵詟謺讋輒輙轍陬鮿鷓鷙鸷真阵针振斟珍诊砧臻贞侦祯轸榛赈朕鸩胗桢畛偵塦姫嫃寊屒帪弫抮挋揕搸敒敶昣栕栚楨樼殝湞潧澵獉珎瑧眕眞眹禎禛紖絼縥纼聄萙蒖薽袗裖覙診誫貞賑軫轃辴遉酙針鉁鋴錱陣靕駗鬒鱵鴆正整睁争挣征怔证帧症郑拯蒸狰政峥钲铮筝诤佂凧塣姃媜崝崢幀徰愸抍掙晸止炡烝爭猙癥眐睜箏篜糽聇証諍證踭鄭鉦錚鬇鴊只之直知制指纸芝稚蜘质脂炙织职痔植执值侄址趾旨志挚掷致置帜智秩帙摭桎枳轵祉蛭膣觯栀彘芷咫絷踬骘轾痣踯埴贽卮酯豸跖栉俧倁値偫儨凪劕劧坁坧垁執墌姪娡嬂崻巵帋幟庢庤廌徏徔徝恉憄懥懫戠搘摯擲旘晊梔梽椥榰槜櫍櫛汦沚洔洷淽滍漐潌瀄熫狾猘瓆疷疻砋礩祑祬禃禔秓秖秪秷稙稺筫紙紩綕緻縶織翐聀膱芖茋藢蘵蟙衼袟袠製襧覟觗觶訨誌謢豑豒貭質贄跱蹠躑躓軄軹輊釞銍鋕鑕铚锧阯隲馽駤騭驇鴙鴲鼅中钟肿终忠仲衷踵舯螽锺冢伀刣堹塚妐妕媑尰幒彸柊歱汷炂煄狆瘇眾筗籦終腫茽蔠蚛螤衆衳衶諥迚鈡鍾鐘鴤鼨周洲皱州轴舟昼骤宙肘帚咒胄纣荮碡籀酎伷侏侜僽冑呪啄喌徟晝晭注炿烐珘甃疛皺睭矪箒籒籕粙紂舳菷葤詋諏诹賙赒軸輈輖辀週郮銂霌駎駲驟鯞住主猪竹株煮筑贮铸嘱拄驻珠瞩蛛柱诛蛀潴洙伫瘃翥茱苎橥箸炷铢疰渚躅麈邾槠佇劅劚劯囑坾墸壴宔嵀斀斸曯柷樦櫡櫧櫫欘殶濐瀦灟炢煑眝矚砫硃祩秼竚笜筯築篫紸絑纻罜羜茁茿莇蝫蠋袾註詝誅豬貯跓軴鉒銖鋳鑄钃阻霔馵駐駯鮢鯺鱁鴸鼄抓檛膼髽转专砖赚篆颛啭僎囀堟専灷瑑瑼竱籑蒃蟤諯賺転轉鄟顓装撞壮桩状妆壯壵妝娤庒梉焋狀粧糚荘莊裝坠锥赘骓缒墜沝甀畷礈縋膇諈贅轛醊錐鑆餟騅鵻准谆凖宒準稕綧衠訰諄迍捉桌拙灼浊卓琢酌擢诼浞涿倬禚丵圴妰娺撯擆斮斱斲斵晫梲棳椓槕濁烵犳琸硺窡窧籗籱罬蠗蠿諁諑鐯鵫鷟字自子紫籽资姿滓咨孜淄笫秭恣谘缁梓鲻锱孳耔觜髭赀訾嵫眦姊辎倳剚嗞姉孶崰杍栥椔榟湽牸眥矷禌秄秶紎緇胾芓茊茡葘虸訿諮貲資趦輜輺鄑釨鈭錙鍿鎡镃頾頿鯔鰦鶅鼒咗唨宗棕踪鬃粽腙倊倧傯堫嵏嵕嵸惣惾愡搃摠昮朡椶熧猔猣疭瘲磫稯糉緃緵縂翪葼蓗蝬豵踨蹤錝鍯鏓鑁騌騣骔鬉鯮鯼走鲰棸緅赱鯐鯫黀齺诅俎哫爼箤詛踿錊鎺靻钻纂缵攥劗籫繤纉纘鑚鑽嘴醉罪厜噿嶊嶵晬枠栬樶檇檌璻祽穝絊纗蟕辠酔尊遵鳟撙樽噂嶟捘繜罇譐銌鐏鱒鶎鷷做作坐左座昨佐祚胙阼侳岝岞秨稓筰糳繓葄蓙袏鈼";

            str = str.Trim();
            if (str.Length > 10)
            {
                str = str.Substring(0, 10);
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (Regex.IsMatch(str[i].ToString(), @"[a-zA-Z0-9]"))
                {
                    pyCode += str[i].ToString().ToUpper();
                }
                else
                {
                    IsFound = false;
                    for (int j = 0; j < 26; j++)
                    {
                        if (PY[j].IndexOf(str[i]) >= 0)
                        {
                            IsFound = true;
                            pyCode += ((char)(j + 65)).ToString();
                            break;
                        }
                    }
                    if (!IsFound)
                    {
                        pyCode += str[i].ToString();
                    }
                }
            }

            return pyCode;
        }

        /// <summary>
        /// 通过对字符的unicode编码进行判断来确定字符是否为中文.
        /// 在unicode 字符串中，中文的范围是在4E00..9FFF:
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>返回是否含有中文</returns>
        public static bool IsContainsChinese(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);


            for (int i = 0; i < input.Length; i++)
            {
                int code = Char.ConvertToUtf32(input, i);
                if (code >= chfrom && code <= chend)
                    return true;
            }

            return false;
        }

        #endregion

        #region 设置控件方法只执行一次

        public delegate void SetDelegateSource();

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(Control control, SetDelegateSource method)
        {
            bool isInit = false;
            control.Enter += delegate
            {
                if (isInit) return;
                isInit = true;
                method();
                
            };
        }

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(RepositoryItemImageComboBox control, SetDelegateSource method)
        {
            bool isInit = false;
            control.Enter += delegate
            {
                if (isInit) return;
                isInit = true;
                method();
            };
        }

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(List<Control> controls, SetDelegateSource method)
        {
            bool isInit = false;

            foreach (var item in controls)
            {
                item.Enter += delegate
                {
                    if (isInit) return;
                    isInit = true;
                    method();
                };
            }
        }

        #endregion

        #region 设置TextEdit的水印
        /// <summary>
        /// 设置CustoemrTextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        public static void SetCustomerTextEditNullValuePrompt(List<DevExpress.XtraEditors.TextEdit> textEdits)
        {
            string tip = LocalData.IsEnglish ? "Please Input Code or EName or CName." : "请输入代码、中文名称或英文名称.";
            Utility.SetTextEditNullValuePrompt(textEdits, tip);
        }

        /// <summary>
        /// 设置PortTextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        public static void SetPortTextEditNullValuePrompt(List<DevExpress.XtraEditors.TextEdit> textEdits)
        {
            string tip = LocalData.IsEnglish ? "Please Input EName or CName." : "请输入中文名称或英文名称.";
            Utility.SetTextEditNullValuePrompt(textEdits, tip);
        }

        /// <summary>
        /// 设置VoyageTextTextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        public static void SetVoyageTextEditNullValuePrompt(List<DevExpress.XtraEditors.TextEdit> textEdits)
        {
            string tip = LocalData.IsEnglish ? "Please Input Voyage or Vessel." : "请输入船名或航次.";
            Utility.SetTextEditNullValuePrompt(textEdits, tip);
        }

        /// <summary>
        /// 设置TextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        /// <param name="tip">水印字串,传空默认为"请输入代码、中文名称或英文名称."</param>
        public static void SetTextEditNullValuePrompt(List<DevExpress.XtraEditors.TextEdit> textEdits, string tip)
        {
            if (textEdits == null || textEdits.Count == 0) return;
            if (string.IsNullOrEmpty(tip)) tip = LocalData.IsEnglish ? "Please Input Code or EName or CName." : "请输入代码、中文名称或英文名称.";
            foreach (var item in textEdits)
            {
                item.Properties.NullValuePrompt = tip;
                item.Properties.NullValuePromptShowForEmptyValue = true;
            }
        }
        #endregion

        #region Grid行号
        /// <summary>
        /// 显示Grid行号
        /// </summary>
        public static void ShowGridRowNo(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            gridView.IndicatorWidth = 35;
            gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gridView_CustomDrawRowIndicator);
        }

        static void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        #endregion

        #region 绑定枚举
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combox"></param>
        /// <param name="ensureFirstEmpty">是否确保下拉列表第一项为空值</param>
        /// <param name="keepFirstOriginal">当第一个枚举值的int为0时，true表示文字描述用值的描述，false表示文字描述用string.Empty代替</param>
        public static void SetComboxByEnum<T>(ImageComboBoxEdit combox, bool ensureFirstEmpty, bool keepFirstOriginal)
        {
            
            List<EnumHelper.ListItem<T>> list = EnumHelper.GetEnumValues<T>(LocalData.IsEnglish);
            combox.Properties.BeginUpdate();
            combox.Properties.Items.Clear();
            foreach (var item in list)
            {
                if (Convert.ToInt32(item.Value) == 0)
                {
                    if (keepFirstOriginal)
                    {
                        combox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                    }
                    else if (ensureFirstEmpty)
                    {
                        combox.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, item.Value));
                    }
                    else
                    {
                    }

                    continue;
                }

                combox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            if (ensureFirstEmpty)
            {
                if (combox.Properties.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(combox.Properties.Items[0].Description))
                    {
                        combox.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));
                    }
                }
                else
                {
                    combox.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));
                }
            }
            combox.Properties.EndUpdate();
        }

        /// <summary>
        /// 将枚举类型的值和对应语言的描述绑定到下拉列表   
        /// </summary>
        /// <typeparam name="T">必须是枚举类型</typeparam>
        /// <param name="combox">下拉列表控件</param>
        /// <param name="ensureFirstEmpty">是否保留第一项空白</param>
        public static void SetComboxByEnum<T>(ImageComboBoxEdit combox, bool ensureFirstEmpty)
        {
            SetComboxByEnum<T>(combox, ensureFirstEmpty, false);
        }
        #endregion

        #region 获得公司、部门、用户信息
        /// <summary>
        /// 绑定当前用户所有的公司(多选下拉框)
        /// </summary>
        public static void BindCheckComboBoxByCompany(CheckedComboBoxEdit checkComboBox)
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> list = GetCompanyList();

            checkComboBox.Properties.BeginUpdate();
            checkComboBox.Properties.Items.Clear();
            foreach (var item in list)
            {
                string name = LocalData.IsEnglish ? item.EShortName : item.CShortName;
                checkComboBox.Properties.Items.Add(item.ID, name, CheckState.Checked, true);
            }

            checkComboBox.Properties.SelectAllItemCaption = LocalData.IsEnglish ? "All" : "全部";
            checkComboBox.Properties.EndUpdate();

        }
        /// <summary>
        /// 绑定当前用户所有的公司(下拉框)
        /// </summary>
        /// <param name="comBox"></param>
        /// <param name="userService"></param>
        public static void BindComboBoxByCompany(ImageComboBoxEdit comBox)
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> list = GetCompanyList();
            comBox.Properties.BeginUpdate();
            comBox.Properties.Items.Clear();
            comBox.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", null));
            foreach (var item in list)
            {
                string name = LocalData.IsEnglish ? item.EShortName : item.CShortName;
                comBox.Properties.Items.Add(new ImageComboBoxItem(name, item.ID));
            }
            comBox.Properties.EndUpdate();
        }
        /// <summary>
        ///  获得当前用户的公司列表
        /// </summary>
        public static List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> GetCompanyList()
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);

            return userCompanyList;
        }

        /// <summary>
        /// 获得当前用户所有公司的ID集合
        /// </summary>
        /// <returns></returns>
        public static List<Guid> GetCompanyIDList()
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> list = GetCompanyList();
            List<Guid> companyIDs = new List<Guid>();

            companyIDs = (from l in list select l.ID).ToList();

            return companyIDs;
        }

        /// <summary>
        /// 填充“揽货人”下拉列表(可搜索)
        /// </summary>
        /// <param name="mcmbUsers">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public static List<UserList> SetMcmbUsers(ICP.Sys.ServiceInterface.IUserService userService, MultiSearchCommonBox mcmbUsers, List<Guid> companyIDs, string roleName, string jobName)
        {
            if (companyIDs == null || companyIDs.Count == 0)
            {
                companyIDs = new List<Guid>();
            }
            List<UserList> users = userService.GetUnderlingUserList(companyIDs.ToArray(), new string[] { jobName }, new string[] { roleName }, true);
            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser == null)
            {
                currentUser = new ModuleUserList();

                users.Insert(0, currentUser);
            }


            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbUsers.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");

            return users;
        }
        /// <summary>
        /// 填充“揽货人”下拉列表(下拉框)
        /// </summary>
        /// <param name="mcmbUsers">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public static void SetCmbBoxUsers(ICP.Sys.ServiceInterface.IUserService userService, ImageComboBoxEdit comBox, string roleName, string jobName)
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> orgList = GetCompanyList();

            Guid[] companyID = (from o in orgList select o.ID).ToArray();

            comBox.Properties.BeginUpdate();
            List<UserList> users = userService.GetUnderlingUserList(companyID, new string[] { jobName }, new string[] { roleName }, true);
            users.Insert(0, new UserList());
            foreach (UserList user in users)
            {
                string name = LocalData.IsEnglish ? user.EName : user.CName;
                comBox.Properties.Items.Add(new ImageComboBoxItem(name, user.ID));
            }
            comBox.Properties.EndUpdate();
        }

        /// <summary>
        /// 绑定所有公司(下拉框)
        /// </summary>
        /// <param name="comBox"></param>
        /// <param name="orgService"></param>
        public static void BindComboBoxByAllCompany(ImageComboBoxEdit comBox, ICP.Sys.ServiceInterface.IOrganizationService orgService)
        {
            List<OrganizationList> list = orgService.GetOfficeList();

            comBox.Properties.BeginUpdate();
            comBox.Properties.Items.Clear();

            comBox.Properties.Items.Add(new ImageComboBoxItem(null, null));

            foreach (OrganizationList item in list)
            {
                string name = LocalData.IsEnglish ? item.EShortName : item.CShortName;
                comBox.Properties.Items.Add(new ImageComboBoxItem(name, item.ID));
            }

            comBox.Properties.EndUpdate();

        }

        /// <summary>
        /// 绑定指定部门的所有用户
        /// </summary>
        public static void BindCmbBoxUserByOrg(IUserService userService, MultiSearchCommonBox mcmbUsers, Guid orgID)
        {
            List<UserList> users = userService.GetUserListBySearch(orgID, string.Empty, string.Empty, true, true, 0);

            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser == null)
            {
                currentUser = new ModuleUserList();

                users.Insert(0, currentUser);
            }

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbUsers.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");

        }


        #endregion

        #region 获得币种信息

        /// <summary>
        /// 获得指定公司下的币种列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="configureService"></param>
        /// <returns></returns>
        public static List<SolutionCurrencyList> GetCompanyCurrencyList(Guid companyID, ICP.Common.ServiceInterface.IConfigureService configureService)
        {
            List<SolutionCurrencyList> currencyList = new List<SolutionCurrencyList>();

            ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(companyID);
            if (configureInfo != null)
            {
                currencyList = configureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
            }

            return currencyList;

        }

        #endregion

        #region 获得会计科目列表
        /// <summary>
        /// 获得指定公司的会计科目
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="configureService"></param>
        public static List<SolutionGLCodeList> GetGLCodeList(Guid companyID, ICP.Common.ServiceInterface.IConfigureService configureService)
        {
            ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(companyID);
            List<SolutionGLCodeList> list = configureService.GetSolutionGLCodeList(configureInfo.SolutionID, true);
            return list;
        }
        #endregion

        #region 将数字类型转化为中文大写(打印用)
        /// <summary>
        /// 将数字类型转化为中文大写
        /// </summary>
        /// <param name="source">需要转换的数字内容</param>
        /// <returns>转换后的中文内容</returns>
        public static string MoneyToString(decimal source)
        {
            string returnVal = "";
            string CNChar = "";
            string bit = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string num = "壹贰叁肆伍陆柒捌玖";
            int moneyMax = bit.Length + 1;

            string moneyString = source.ToString("###########.00");
            int length = moneyString.Length - 1;

            moneyString = moneyString.Replace(".", "");//去除小数点


            for (int i = moneyString.Length; i > 0; i--)
            {
                moneyMax = moneyMax - 1;
                CNChar = bit.Substring(moneyMax - 1, 1);
                int number = Convert.ToInt16(moneyString.Substring(i - 1, 1));
                if (number == 0)
                {
                    switch (CNChar)
                    {
                        case "元":
                            returnVal = CNChar + returnVal;
                            break;
                        case "万":
                            returnVal = CNChar + returnVal;
                            break;
                        case "亿":
                            returnVal = CNChar + returnVal;
                            break;
                        case "分":
                            returnVal = "整";
                            break;
                        case "角":
                            if (returnVal != "整") returnVal = "零" + returnVal;
                            break;
                        default:
                            if ((returnVal.Substring(0, 1) != "万") && (returnVal.Substring(0, 1) != "亿") && (returnVal.Substring(0, 1) != "元") && (returnVal.Substring(0, 1) != "零")) returnVal = "零" + returnVal;
                            break;
                    }
                }
                else
                {
                    returnVal = num.Substring(number - 1, 1) + CNChar + returnVal;
                }
            }
            return returnVal;
        }
        #endregion

        #region UIHelper

        /// <summary>
        /// SetGridViewColumnAllowEditColor
        /// </summary>
        /// <param name="gvMain">GridView</param>
        public static void SetGridViewColumnAllowEditColor(DevExpress.XtraGrid.Views.Grid.GridView gvMain)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn item in gvMain.Columns)
            {
                if (item.OptionsColumn.AllowEdit == false)
                {
                    item.AppearanceCell.ForeColor = System.Drawing.SystemColors.ControlDark;
                    item.AppearanceCell.Options.UseForeColor = true;
                }
            }
        }

        /// <summary>
        /// 绑定字典
        /// </summary>
        /// <param name="TFService"></param>
        /// <param name="cmbBox"></param>
        /// <param name="dicType"></param>
        public static void BindaDictionary(ITransportFoundationService TFService, ImageComboBoxEdit cmbBox, DataDictionaryType dicType)
        {
            List<DataDictionaryList> dicList = TFService.GetDataDictionaryList(null, null, dicType, true, 0);

            cmbBox.Properties.BeginUpdate();
            cmbBox.Properties.Items.Clear();

            foreach (DataDictionaryList item in dicList)
            {
                cmbBox.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            cmbBox.Properties.EndUpdate();
        }

        #endregion

    }

    public class RateHelper
    {
        #region Services

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        #endregion

        List<CurrencyList> _currencys;
        public List<CurrencyList> Currencys
        {
            get
            {
                if (_currencys == null) _currencys = ConfigureService.GetCurrencyList(string.Empty, string.Empty, null, true, 0);
                return _currencys;
            }
        }

        #region RateHelper

        public decimal GetRate(string sourceCurrency, string targetCurrency, DateTime billdate, List<ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList> rateList)
        {
            decimal rate = 1m;

            if (sourceCurrency == targetCurrency) return rate;

            var rateobj = rateList.Find(delegate(ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList r)
            {
                return r.SourceCurrency == sourceCurrency &&
                        r.TargetCurrency == targetCurrency &&
                        billdate.Date >= r.FromDate &&
                        billdate.Date <= r.ToDate;
            });

            if (rateobj == null)
            {
                rateobj = rateList.Find(delegate(ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList r)
                {
                    return r.TargetCurrency == sourceCurrency &&
                            r.SourceCurrency == targetCurrency &&
                            billdate.Date >= r.FromDate &&
                            billdate.Date <= r.ToDate;
                });

                if (rateobj != null) rate = 1 / rateobj.Rate;
            }
            else
            {
                rate = rateobj.Rate;
            }

            if (rateobj == null)
            {
                if (LocalData.IsEnglish)
                    throw new ApplicationException("have no rate between " + sourceCurrency + " to " + targetCurrency + ".(Date:" + billdate.ToShortDateString() + ")");
                else
                    throw new ApplicationException("没有" + sourceCurrency + "和" + targetCurrency + "的汇率.(时间:" + billdate.ToShortDateString() + ")");
            }

            return rate;
        }

        /// <summary>
        /// 查找汇率
        /// </summary>
        /// <param name="sourceCurrencyID">源币种</param>
        /// <param name="targetCurrencyID">目标币种</param>
        /// <param name="billdate">日期</param>
        /// <param name="rateList">汇率列表</param>
        /// <returns></returns>
        public decimal GetRate(Guid sourceCurrencyID, Guid targetCurrencyID, DateTime billdate, List<ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList> rateList)
        {

            if (sourceCurrencyID == Guid.Empty || targetCurrencyID == Guid.Empty) return 0m;

            if (sourceCurrencyID == targetCurrencyID) return 1;

            SolutionExchangeRateList inRate = rateList.Find(delegate(SolutionExchangeRateList item)
            {
                return
                  item.SourceCurrencyID == sourceCurrencyID && item.TargetCurrencyID == targetCurrencyID
                  && billdate.Date >= item.FromDate && billdate.Date <= item.ToDate;
            });

            if (inRate != null)
            {
                return inRate.Rate;
            }

            SolutionExchangeRateList outRate = rateList.Find(delegate(SolutionExchangeRateList item)
            {
                return
                  item.SourceCurrencyID == targetCurrencyID && item.TargetCurrencyID == sourceCurrencyID
                        && billdate.Date >= item.FromDate && billdate.Date <= item.ToDate;
            });

            if (outRate != null)
            {
                return (1 / outRate.Rate);
            }

            string sourceCurrencyName = GetCurrencyNameByCurrencyID(sourceCurrencyID);
            string tagerCurrencyName = GetCurrencyNameByCurrencyID(targetCurrencyID);

            DevExpress.XtraEditors.XtraMessageBox.Show(sourceCurrencyName + "=>" + tagerCurrencyName + (LocalData.IsEnglish ? " Rate Not Find." : " 找不到汇率."), LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //throw new ApplicationException(sourceCurrencyName + "=>" + tagerCurrencyName + (LocalData.IsEnglish ? " Rate Not Find." : " 找不到汇率."));
            return 0m;
        }

        public decimal GetAmountByRate(decimal amount, Guid sourceCurrencyID, Guid targetCurrencyID, List<SolutionExchangeRateList> rateList)
        {
            if (sourceCurrencyID == Guid.Empty || targetCurrencyID == Guid.Empty) return 0m;

            if (sourceCurrencyID == targetCurrencyID) return amount;

            SolutionExchangeRateList inRate = rateList.Find(delegate(SolutionExchangeRateList item)
            { return item.SourceCurrencyID == sourceCurrencyID && item.TargetCurrencyID == targetCurrencyID; });
            if (inRate != null)
            {
                return (amount * inRate.Rate);
            }

            SolutionExchangeRateList outRate = rateList.Find(delegate(SolutionExchangeRateList item)
            { return item.SourceCurrencyID == targetCurrencyID && item.TargetCurrencyID == sourceCurrencyID; });
            if (outRate != null)
            {
                return (amount / outRate.Rate);
            }

            string sourceCurrencyName = GetCurrencyNameByCurrencyID(sourceCurrencyID);
            string tagerCurrencyName = GetCurrencyNameByCurrencyID(targetCurrencyID);

            throw new ApplicationException(sourceCurrencyName + "=>" + tagerCurrencyName + (LocalData.IsEnglish ? " Rate Not Find." : " 找不到汇率."));
        }

        public string GetCurrencyNameByCurrencyID(Guid guid)
        {
            CurrencyList tager = Currencys.Find(delegate(CurrencyList item) { return item.ID == guid; });
            if (tager == null) return string.Empty;
            else return tager.Code;
        }


        #endregion
    }

    public class TMSUIHelper : Controller
    {
        #region Services
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }


        #endregion

        /// <summary>
        /// 设置客户的描述类
        /// </summary>
        /// <param name="customerID">客户的ID</param>
        /// <param name="customerDescription">客户的描述,不可为空</param>
        public CustomerInfo SetCustomerDesByID(Guid? customerID, CustomerDescription customerDescription)
        {
            if (customerID.HasValue == false || customerID.Value == Guid.Empty)
            {
                customerDescription.Address = customerDescription.City = customerDescription.Contact = customerDescription.Country
                    = customerDescription.Fax = customerDescription.Name = customerDescription.Tel = string.Empty;
                return null;
            }
            else
            {
                CustomerInfo info = CustomerService.GetCustomerInfo(customerID.Value);
                customerDescription.Address = info.EAddress ?? string.Empty;
                customerDescription.City = info.CityName ?? string.Empty;
                customerDescription.Contact = string.Empty ?? string.Empty;
                customerDescription.Country = info.CountryName ?? string.Empty;
                customerDescription.Fax = info.Fax ?? string.Empty;
                customerDescription.Name = info.EName ?? string.Empty;
                customerDescription.Tel = info.Tel1 ?? string.Empty;

                return info;
            }
        }
    }

    public class ReportHelper
    {
        public static string GetFAMReportPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\TMS\\";
        }

        public static string GetReportLOGOPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\LOGO\\";
        }
    }




    public class UIConnectionHelper
    {
        public delegate bool SaveDataDelegate();

        public static void ParentChangingForEditPart(IListPart listPart, SaveDataDelegate saveData, BaseDataObject childDataSource, CancelEventArgs e, string partName)
        {
            ParentChangingForEditPart(listPart, saveData, childDataSource, e, partName, true);
        }

        public static void ParentChangingForEditPart(IListPart listPart, SaveDataDelegate saveData, BaseDataObject childDataSource, CancelEventArgs e, string partName, bool removeMainList)
        {
            if (childDataSource == null) return;

            if (childDataSource.IsNew)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByNew(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No && childDataSource.IsNew && removeMainList)
                {
                    listPart.RemoveItem(listPart.Current);
                }
            }
            else if (childDataSource.IsDirty)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No && childDataSource.IsNew && removeMainList)
                {
                    listPart.RemoveItem(listPart.Current);
                }
                else if (dlg == DialogResult.No)
                {
                    childDataSource.IsDirty = false;
                }
            }
        }

        public static void ParentChangingForListPart<T>(SaveDataDelegate saveData, List<T> childDataSource, CancelEventArgs e, string partName) where T : BaseDataObject
        {
            if (childDataSource == null || childDataSource.Count == 0) return;

            bool isNew = false, isDirty = false;
            foreach (var item in childDataSource)
            {
                if (item.IsNew) { isNew = true; break; }
                if (item.IsDirty) { isDirty = true; break; }
            }

            if (isNew)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByNew(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No) { return; }
            }
            else if (isDirty)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No) { return; }
            }
        }

        public static void ParentChangingForBaseListPart<T>(SaveDataDelegate saveData, BaseList<T> childDataSource, CancelEventArgs e, string partName) where T : BaseDataObject
        {
            if (childDataSource.IsDirty)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No) { return; }
            }
        }

    }
}

