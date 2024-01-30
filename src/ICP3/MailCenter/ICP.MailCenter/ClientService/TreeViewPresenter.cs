using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Office.Interop.Outlook;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using System.Threading;
using Microsoft.Practices.CompositeUI;
using Exception = System.Exception;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件文件夹列表呈现类
    /// </summary>
    public partial class TreeViewPresenter
    {
        static TreeViewPresenter()
        {
            DicFolderPathAndNodes = new Dictionary<string, TreeNode>();
        }

        #region 常量

        /// <summary>
        /// 记录刚打开邮件中心时，某个文件夹的子文件夹收到新邮件，
        /// 但这个时候正在构造树，就找不到该节点，
        /// 记录下来，等构造树完成后，再将其展开
        /// </summary>
        public static List<FolderInfo> noFoundFolders = new List<FolderInfo>();
        /// <summary>
        /// 根文件夹路径
        /// </summary>
        public static string RootFolderPath = string.Empty;
        /// <summary>
        /// 追加树节点后将其展开
        /// </summary>
        /// <param name="rootNode"></param>
        private delegate void GeneralTreeNodesDelegate(TreeNode rootNode);
        /// <summary>
        /// 记录父节点树集合
        /// </summary>
        private static List<TreeNode> parentTreeNodes = new List<TreeNode>();
        /// <summary>
        /// 记录所有节点集合
        /// </summary>
        private static TreeNode[] arrTreeNodes = null;
        /// <summary>
        /// 记录当前节点集合
        /// </summary>
        private static TreeNode[] currentTreeNodes = { };

        /// <summary>
        /// 记录子节点集合
        /// </summary>
        private static List<TreeNode> subTreeNodes = new List<TreeNode>();
        #endregion

        #region  outlook默认使用的账户根目录文件夹名称
        /// <summary>
        /// 获取账户发送邮件的文件夹根目录
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultUserFolderName()
        {
            string folderName = string.Empty;
            MAPIFolder olfolder = DefaultUseFolder();
            if (olfolder != null)
            {
                folderName = olfolder.Name;
                MailUtility.ReleaseComObject(olfolder);
            }
            return folderName;
        }

        /// <summary>
        /// outlook账户默认使用的文件夹EntryID
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultUserFolderEntryID()
        {
            string entryID = string.Empty;
            MAPIFolder olfolder = DefaultUseFolder();
            if (olfolder != null)
            {
                entryID = olfolder.EntryID;
                MailUtility.ReleaseComObject(olfolder);
            }

            return entryID;
        }
        /// <summary>
        ///  outlook 账户默认使用的文件夹
        /// </summary>
        /// <returns></returns>
        public static MAPIFolder DefaultUseFolder()
        {

            MAPIFolder defaultUseFolder = null;
            try
            {
                defaultUseFolder = ClientUtility.OlNS.DefaultStore.GetRootFolder();
            }
            catch (Exception ex)
            {
                try
                {
                    defaultUseFolder = ClientUtility.CreateOutlookNameSpaceInstance().DefaultStore.GetRootFolder();
                }
                catch (System.Exception e)
                {
                }
            }
            return defaultUseFolder;
        }

        /// <summary>
        /// 默认定位收件箱完整路径
        /// </summary>
        /// <returns></returns>
        public static string DefaultUseFolderFullPath()
        {
            MAPIFolder olFolder = TreeViewPresenter.DefaultUseFolder();
            if (olFolder != null)
            {
                string fullPath = string.Format("{0}{1}", olFolder.FullFolderPath, (ClientUtility.olCultrue == "zh-cn" ? @"\收件箱" : @"\Inbox"));
                MailUtility.ReleaseComObject(olFolder);

                return fullPath;
            }
            else
            {
                return ClientUtility.olCultrue == "zh-cn" ? @"\\收件箱" : @"\\Inbox";
            }
        }

        /// <summary>
        /// 用户outlook客户端默认使用的pst文件夹
        /// </summary>
        private static string _defaultUseFolder;
        public static string DefaultUseFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultUseFolder))
                {
                    _defaultUseFolder = GetDefaultUserFolderName();
                }
                return _defaultUseFolder;
            }
        }
        #endregion

        /// <summary>
        /// TreeView默认选中文件夹Full Path
        /// </summary>
        private static string _defaultSelectedFolderPath;
        public static string DefaultSelectedFolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultSelectedFolderPath))
                {
                    _defaultSelectedFolderPath = string.Format(@"\\{0}\{1}", RootFolderPath,
                                                               ClientUtility.olCultrue == "zh-cn" ? "收件箱" : "Inbox");
                }
                return _defaultSelectedFolderPath;
            }
        }


        private static Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public static Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = ServiceClient.GetClientService<WorkItem>().SmartParts;
                return _SmartParts;
            }
        }
        static EmailFolderPart _folderPart;
        public static EmailFolderPart folderPart
        {
            get
            {
                if (_folderPart == null)
                    _folderPart = SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);

                return _folderPart;
            }
            set
            {
                _folderPart = value;
            }
        }

        /// <summary>
        /// 文件夹路径和对应节点键值对缓存
        /// </summary>
        public static Dictionary<string, TreeNode> DicFolderPathAndNodes
        {
            get;
            set;
        }

        #region Invalidate
        ////记录有新邮件来需要刷新的节点名称
        //public static List<TreeNode> _list_UpdateNodes;
        //public static List<TreeNode> list_UpdateNodes
        //{
        //    get
        //    {
        //        return _list_UpdateNodes ?? (_list_UpdateNodes = new List<TreeNode>());
        //    }
        //    set
        //    {
        //        _list_UpdateNodes = value;
        //    }
        //}
        ///// <summary>
        ///// 记录在outlook中添加之前没有子节点的文件夹
        ///// </summary>
        //public static List<TreeNode> _lstAddNewNodes;
        //public static List<TreeNode> lstAddNewNodes
        //{
        //    get
        //    {
        //        return _lstAddNewNodes ?? (_lstAddNewNodes = new List<TreeNode>());
        //    }
        //    set
        //    {
        //        _lstAddNewNodes = value;
        //    }
        //}
        ///// <summary>
        ///// 记录需要删除的文件夹
        ///// </summary>
        //public static List<TreeNode> _lstRemoveNodes;
        //public static List<TreeNode> lstRemoveNodes
        //{
        //    get
        //    {
        //        return _lstRemoveNodes ?? (_lstRemoveNodes = new List<TreeNode>());
        //    }
        //    set
        //    {
        //        _lstRemoveNodes = value;
        //    }
        //}
        ///// <summary>
        ///// 记录TreeView中节点EntryID
        ///// </summary>
        //public static List<string> _lstTreeNode;
        //public static List<string> lstTreeNode
        //{
        //    get
        //    {
        //        return _lstTreeNode ?? (_lstTreeNode = new List<string>());
        //    }
        //    set { _lstTreeNode = value; }
        //}
        ///// <summary>
        ///// 记录Outlook中节点EntryID
        ///// </summary>
        //static List<string> _lstOLNode;
        //public static List<string> lstOLNode
        //{
        //    get
        //    {
        //        return _lstOLNode ?? (_lstOLNode = new List<string>());
        //    }
        //    set { _lstOLNode = value; }
        //}
        ///// <summary>
        ///// Ojbect表示Node名称,string表示outlook对应的节点的名称
        ///// </summary>
        //public static List<NodePacked> _lstUpdateNodes;
        //public static List<NodePacked> lstUpdateNodes
        //{
        //    get
        //    {
        //        return _lstUpdateNodes ?? (_lstUpdateNodes = new List<NodePacked>());
        //    }
        //    set
        //    {
        //        _lstUpdateNodes = value;
        //    }
        //}

        //public static void ClearData()
        //{
        //    newRecord = originalRecord = string.Empty;
        //    lstTreeNode.Clear();
        //    lstOLNode.Clear();
        //}
        //static void RemoveTreeViewNode(TreeNode node)
        //{
        //    lstTreeNode.Remove(node.Tag.ToString());
        //    lstRemoveNodes.Add(node);
        //}
        #endregion


        /// <summary>
        /// 判断Outlook文件夹和树节点是否相同
        /// </summary>
        /// <param name="currentSelectedNode"></param>
        /// <param name="targetFolder"></param>
        /// <returns></returns>
        public static bool IsSameFolder(TreeNode currentSelectedNode, MAPIFolder targetFolder)
        {
            bool sameFolder = false;
            if (currentSelectedNode == null)
            {
                if (DefaultSelectedFolderPath.Equals(string.Format(@"\\{0}", targetFolder.FullFolderPath)))
                    sameFolder = true;
            }

            else
            {
                if (currentSelectedNode.Tag != null && currentSelectedNode.Tag != "")
                {
                    if (currentSelectedNode.Tag.ToString().Equals(targetFolder.EntryID))
                        sameFolder = true;
                }
            }
            return sameFolder;
        }
        /// <summary>
        /// 获取关联邮件
        /// </summary>
        /// <param name="inboxFolder">收件箱对象</param>
        /// <param name="items">返回集合对象：保存关联的邮件</param>
        public static void GetRelationMails(MAPIFolder inboxFolder, ref List<Object> items)
        {
            if (inboxFolder == null)
                return;
            //当前文件夹下邮件集合对象
            Items olItems = inboxFolder.Items;
            //邮件对象
            _MailItem olItem = null;
            //邮件回执
            ReportItem olReportItem = null;
            #region 遍历邮件
            //遍历邮件
            if (olItems != null)
            {
                for (int index1 = 1; index1 <= olItems.Count; index1++)
                {
                    //防止窗体假死
                    System.Windows.Forms.Application.DoEvents();
                    olItem = olItems[index1] as MailItem;
                    //转换邮件对象不为空且邮件为绿色标记
                    if (olItem != null && (olItem.Categories == ClientUtility.GreenCategory))
                    {
                        if (items == null)
                            return;
                        items.Add(olItem);
                    }
                    else
                    {
                        olReportItem = olItems[index1] as ReportItem;
                        //转换回执对象不为空且回执为绿色标记
                        if (olReportItem != null && (olReportItem.Categories == ClientUtility.GreenCategory))
                        {
                            if (items == null)
                                return;
                            items.Add(olReportItem);
                        }
                    }
                }
            }
            #region 设置对象为空
            if (olItem != null)
                olItem = null;
            if (olReportItem != null)
                olReportItem = null;
            #endregion
            #endregion
        }

        /// <summary>
        /// 将项添加到集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddItemToDictionary(string key, TreeNode value)
        {
            key = key.ToLower();
            //得到最新的TreeNode
            if (DicFolderPathAndNodes.ContainsKey(key))
                DicFolderPathAndNodes.Remove(key);

            DicFolderPathAndNodes.Add(key, value);
        }

        #region 更新所有文件夹

        /// <summary>
        /// 将树保持和outlook文件夹一致
        /// </summary>
        /// <param name="tvFolder"></param>
        /// <param name="isRegisterEvents"></param>
        public static void RefershTreeView(TreeView tvFolder, bool isRegisterEvents)
        {
            WaitCallback callback = (obj) => InnerRefershed(obj, isRegisterEvents);
            ThreadPool.QueueUserWorkItem(callback, tvFolder);
        }

        public static void InnerRefershed(object objTreeView, bool isRegisterEvents)
        {
            TreeView tvFolder = objTreeView as TreeView;
            //StopwatchHelper.StartStopwatch();
            ClientProperties.RootFolders.Clear();
            if (tvFolder == null || tvFolder.IsDisposed)
                return;
            if (isRegisterEvents)
            {
                folderPart.UseWaitCursor = true;
            }
            ClientProperties.IsSynchronizingFolders = true;
            tvFolder.BeginUpdate();
            int count = tvFolder.Nodes.Count;
            try
            {
                //更新节点
                for (int i = 0; i < count; i++)
                {
                    arrTreeNodes = null;
                    TreeNode rootNode = tvFolder.Nodes[i];
                    MailListPresenter.AddRootFolderToList(rootNode.Text);
                    LoopRecordTreeNodes(rootNode, new TreeNode[0], isRegisterEvents, true, true);//循环遍历记录所有展开的节点，并且注册FolderChange事件
                    var generalTreeNodes = new GeneralTreeNodesDelegate(AddRangeTreeNodes);
                    tvFolder.Invoke(generalTreeNodes, rootNode);
                }
                ReleaseMailCenterHandle();
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                tvFolder.EndUpdate();
                ClientProperties.IsSynchronizingFolders = false;
                if (isRegisterEvents)
                {
                    folderPart.UseWaitCursor = false;
                    folderPart.StopAnimate();
                }
                // 记录刚打开邮件中心时，某个文件夹的子文件夹收到新邮件，
                // 但这个时候正在构造树，就找不到该节点，
                // 记录下来，等构造树完成后，再将其展开
                if (noFoundFolders != null && noFoundFolders.Count > 0)
                {
                    for (int i = 0; i < noFoundFolders.Count; i++)
                        ExpandByMAPIFolder(folderPart.trvFolder, noFoundFolders[i]);

                    noFoundFolders.Clear();
                }

                //回调通知要刷新界面UI
                folderPart.OnRefershed();
                tvFolder = null;
                //MailUtility.EndStopwatch("ICPMailCenter.exe", (LocalData.IsEnglish ? "Open Mail Center" : "打开邮件中心"));
            }
        }

        public static void AddRangeTreeNodes(TreeNode rootNode)
        {
            if (arrTreeNodes != null && arrTreeNodes.Length > 0)
            {
                var expandedTreeNodes = arrTreeNodes.Where(node => node.IsExpanded);
                if (expandedTreeNodes != null)
                {
                    rootNode.Nodes.Clear();
                    rootNode.Nodes.AddRange(arrTreeNodes);
                    rootNode.Expand();
                    //当把节点AddRange后，然来展开的节点全部都会变成未展开，需要重新将其展开
                    if (expandedTreeNodes.Count() > 0)
                    {
                        foreach (var item in expandedTreeNodes)
                        {
                            Expand(item);
                        }
                        expandedTreeNodes = null;
                        arrTreeNodes = null;
                    }
                }
            }
        }

        /// <summary>
        /// 比如当前TreeNode 的Level是3，则 Level分别为2，1都需要将其展开
        /// </summary>
        /// <param name="_treeNode"></param>
        private static void Expand(TreeNode _treeNode)
        {
            if (_treeNode.Tag != null && _treeNode.Tag != "")
            {
                TreeNodeCollection treeNodeCollection = _treeNode.Nodes;
                int count = treeNodeCollection.Count;
                if (count > 0)
                {
                    if (_treeNode.FirstNode.Tag != null && _treeNode.LastNode.Tag != null)
                        _treeNode.Expand();
                    for (int i = 0; i < count; i++)
                    {
                        Expand(treeNodeCollection[i]);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 释放延迟等待
        /// </summary>
        public static void ReleaseMailCenterHandle()
        {
            //如果是从ICP主程序打开邮件中心，则需要释放等待事件句柄；
            //如果从菜单直接打开邮件中心，此时等待句柄不存在，需要捕获句柄不存在的异常
            EventWaitHandle waitHandle = EventWaitHandle.OpenExisting("ICPMailCenter.exe");
            waitHandle.Set();
        }

        /// <summary>
        /// 新增树节点，并添加一个Loading的子文件夹
        /// </summary>
        /// <param name="node"></param>
        /// <param name="folder"></param>
        /// <param name="isRegister"></param>
        public static void AddShamAndRealNode(TreeNode node, MAPIFolder folder, bool isRegister)
        {
            if (node != null)
            {
                TreeNode newSubNode = ConvertMAPIFolderToTreeNode(folder);
                if (isRegister)
                {
                    RegisterFolderEvents(folder, !MailUtility.IsInDOJFolders(folder.Name));
                }
                AddShamNode(newSubNode, folder);
                AddToTreeNode(node, newSubNode);
            }
        }
        /// <summary>
        /// 新增一个节点
        /// </summary>
        /// <param name="olFolder"></param>
        /// <returns></returns>
        public static TreeNode CreateNewTreeNode(MAPIFolder olFolder)
        {
            TreeNode newNode = ConvertMAPIFolderToTreeNode(olFolder);
            AddShamNode(newNode, olFolder);
            return newNode;
        }
        /// <summary>
        /// 生成一个默认的节点和伪子节点
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="entryID"></param>
        /// <returns></returns>
        public static TreeNode CreateShamAndRealNode(string folderName, string entryID)
        {
            int index = GetImageIndex(folderName.ToLower());
            TreeNode rootNode = new TreeNode() { Text = folderName, Tag = entryID, ImageIndex = index, SelectedImageIndex = index };
            AddShamNode(rootNode);

            return rootNode;
        }
        /// <summary>
        /// 添加一个伪节点（Loading TreeNode）
        /// </summary>
        /// <param name="node"></param>
        public static void AddShamNode(TreeNode node)
        {
            if (node != null)
            {
                node.Nodes.Add(new TreeNode() { Text = MailUtility.LoadingText, ForeColor = MailUtility.ShamNodeColor });
            }
        }
        /// <summary>
        /// 添加一个伪节点
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="olFolder"></param>
        public static void AddShamNode(TreeNode newNode, MAPIFolder olFolder)
        {
            Folders _olFolders = olFolder.Folders;
            if (_olFolders.Count > 0)
                AddShamNode(newNode);

            MailUtility.ReleaseComObject(_olFolders);
        }

        /// <summary>
        /// 追加TreeNode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="subNode"></param>
        public static void AddToTreeNode(TreeNode node, TreeNode subNode)
        {
            if (node.FirstNode != null)
            {
                if (!node.FirstNode.Text.Equals(MailUtility.LoadingText))
                {
                    node.Nodes.Add(subNode);
                }
            }
            else
            {
                AddShamNode(node);
            }
        }

        public static void RegisterAllExpandedNodes(TreeView tvFolder, bool isRegisterEvents)
        {
            WaitCallback callback = (obj) => InnerRegister(obj, isRegisterEvents);
            ThreadPool.QueueUserWorkItem(callback, tvFolder);
        }

        public static void InnerRegister(object objTreeView, bool isRegisterEvents)
        {
            if (!isRegisterEvents)
                return;
            TreeView _treeView = objTreeView as TreeView;
            if (_treeView == null || _treeView.IsDisposed)
                return;
            try
            {
                int count = _treeView.Nodes.Count;
                for (int i = 0; i < count; i++)
                {
                    RegisterTreeViewEvents(_treeView.Nodes[i]);
                }
            }
            catch { }
            finally
            {
                _treeView = null;
            }
        }

        /// <summary>
        ///递归注册所有MAPIFolder事件
        /// </summary>
        /// <param name="node"></param>
        private static void RegisterTreeViewEvents(TreeNode node)
        {
            if (node.Tag != null && node.Tag != "" && node.IsExpanded)
            {
                int count = node.Nodes.Count;
                if (count > 0)
                {
                    MAPIFolder olFolder = MailListPresenter.GetFolderByEntryID(node.Tag.ToString());
                    if (olFolder == null)
                        return;
                    RegisterFolderEvents(olFolder);
                    //将遍历TreeNode所有子文件夹，对比Outlook所有文件夹
                    for (int t = 0; t < count; t++)
                    {
                        RegisterTreeViewEvents(node.Nodes[t]);
                    }
                }
            }
        }

        /// <summary>
        /// 循环遍历记录所有展开的节点，并且注册Folder事件
        /// </summary>
        /// <param name="node"></param>
        /// <param name="isRegisterEvents"></param>
        /// <param name="isRegisterSubNodeEvents"></param>
        /// <param name="isRefershUI"></param>
        public static void LoopRecordTreeNodes(TreeNode node, TreeNode[] treeNodes, bool isRegisterEvents, bool isRegisterSubNodeEvents,
                                        bool isRefershUI)
        {
            if (node.Tag != null && node.IsExpanded)//只对展开的节点更新
            {
                int count = node.Nodes.Count;
                if (count > 0)
                {
                    MAPIFolder olFolder = MailListPresenter.GetFolderByEntryID(node.Tag.ToString());//获取outlook文件夹
                    if (olFolder == null)
                        return;

                    //将文件夹注册FolderChange事件
                    if (isRegisterEvents && isRegisterSubNodeEvents)
                    {
                        RegisterFolderEvents(olFolder);
                    }
                    subTreeNodes.Clear();
                    parentTreeNodes.Clear();
                    bool hasParentNode; TreeNode _parentNode = null;
                    _parentNode = HasParentTreeNode(treeNodes, node.Text, node.Tag, out hasParentNode);//是否存在父节点，返回hasParentNode标识
                    Folders olFolders = olFolder.Folders;
                    int olCount = olFolders.Count;
                    for (int ol = 1; ol <= olCount; ol++)
                    {
                        MAPIFolder _olFolder = olFolders[ol];
                        if (!FilterOutlookJunkFolder(_olFolder))//过滤是否是outlook垃圾邮件
                        {
                            if (!hasParentNode)
                                parentTreeNodes.Add(CreateNewTreeNode(_olFolder));//将文件夹转化为节点赋给TreeView
                            else
                                subTreeNodes.Add(CreateNewTreeNode(_olFolder));
                        }
                        _olFolder = null;
                    }
                    olFolders = null;
                    olFolder = null;

                    if (!hasParentNode)
                    {
                        arrTreeNodes = parentTreeNodes.ToArray();//记录父节点集合AddRangeTreeNodes方法中用到
                        //if (arrTreeNodes.Length > 1)
                        //    Array.Sort(arrTreeNodes, new TreeNodeComparer());                               
                        treeNodes = arrTreeNodes;
                    }
                    else
                    {
                        currentTreeNodes = subTreeNodes.ToArray();
                        // if (currentTreeNodes.Length > 1)
                        //    Array.Sort(currentTreeNodes, new TreeNodeComparer());
                        _parentNode.Nodes.Clear();
                        _parentNode.Nodes.AddRange(currentTreeNodes);
                        _parentNode.Expand();

                        treeNodes = currentTreeNodes;
                    }

                    //将遍历TreeNode所有子文件夹，对比Outlook所有文件夹
                    for (int t = 0; t < count; t++)
                    {
                        LoopRecordTreeNodes(node.Nodes[t], treeNodes, isRegisterEvents, isRegisterSubNodeEvents, isRefershUI);
                    }
                }
            }
        }

        #region 过滤垃圾文件夹
        private static string[] _FilterFolders;
        public static string[] FilterFolders
        {
            get
            {
                if (_FilterFolders == null)
                {
                    if (ClientUtility.olCultrue.Equals("zh-cn"))
                    {
                        _FilterFolders = MailUtility.CN_JunFolders;
                    }
                    else
                    {
                        _FilterFolders = MailUtility.EN_JunFolders;
                    }
                }
                return _FilterFolders;
            }
        }


        public static bool FilterOutlookJunkFolder(MAPIFolder olFolder)
        {
            bool isFilter = false;
            if (olFolder.DefaultItemType != OlItemType.olMailItem)
            {
                isFilter = true;
            }
            else
            {
                if (FilterFolders.Any(item => item.Equals(olFolder.Name.ToLower())))//此邮件是否在垃圾邮件数组中
                {
                    isFilter = true;
                }
            }
            return isFilter;
        }

        #endregion

        /// <summary>
        /// 是否存在父节点
        /// </summary>
        /// <param name="treeNodes"></param>
        /// <param name="nodeText"></param>
        /// <param name="tag"></param>
        /// <param name="hasParentNode"></param>
        /// <returns></returns>
        private static TreeNode HasParentTreeNode(TreeNode[] treeNodes, string nodeText, object tag, out bool hasParentNode)
        {
            TreeNode _parentNode = null;
            hasParentNode = true;
            if (treeNodes.Length == 0)
                hasParentNode = false;
            else
            {
                _parentNode = treeNodes.FirstOrDefault(item => item.Tag.Equals(tag));
                if (_parentNode == null)
                    hasParentNode = false;
            }
            return _parentNode;
        }


        /// <summary>
        /// 将MAPIFolder 转换成TreeNode
        /// </summary>
        /// <param name="currentFolder"></param>
        /// <returns></returns>
        public static TreeNode ConvertMAPIFolderToTreeNode(MAPIFolder currentFolder)
        {
            string folderName = currentFolder.Name;
            TreeNode newNode = new TreeNode(folderName) { Tag = currentFolder.EntryID };
            int count = 0;
            bool isContains = IsContainsTotalCountFolder(currentFolder, out count);
            NodeInfo nodeInfo = NodeInfo.GetNodeInfo(isContains, count, folderName);
            SetNodeAppearance(newNode, nodeInfo);//将从文件夹获取的样式赋给节点
            int index = GetImageIndex(folderName.ToLower());
            newNode.SelectedImageIndex = newNode.ImageIndex = index;

            return newNode;
        }


        #region  添加在OutLook中新增的节点到对应的TreeView节点中


        //public static void GetterNode(string olFullPath, TreeView treeView, ref TreeNode needAddNode)
        //{
        //    if (treeView.IsDisposed == false)
        //    {
        //        foreach (TreeNode rootNode in treeView.Nodes)
        //        {
        //            LoopTreeView(rootNode, olFullPath, ref needAddNode);
        //        }
        //    }
        //}

        //static void LoopTreeView(TreeNode node, string olFullPath, ref TreeNode needAddNode)
        //{
        //    if (node != null)
        //    {
        //        if (IsSameTreeViewNode(olFullPath, node.FullPath))
        //        {
        //            needAddNode = node;
        //            return;
        //        }

        //        foreach (TreeNode childNode in node.Nodes)
        //        {
        //            LoopTreeView(childNode, olFullPath, ref needAddNode);
        //        }
        //    }
        //}

        public static bool IsSameTreeViewNode(string olFullPath, string fullPath)
        {
            bool isSame = false;
            if (!string.IsNullOrEmpty(olFullPath) && !string.IsNullOrEmpty(fullPath))
            {

                if (olFullPath.Length > 3)
                {
                    string tempOLFullPath = string.Format("\\{0}", olFullPath);
                    if (fullPath.Equals(tempOLFullPath))
                        return true;
                }
            }
            return isSame;
        }

        #endregion


        static string SplitNodeText(List<NodePacked> list, char charText, TreeNode node, MAPIFolder folder, string folderName)
        {
            string nodeText = string.Empty, nodeNumber = string.Empty;

            string[] arrText = node.Text.Split(charText);
            if (arrText.Length > 0)
            {
                nodeText = arrText[0];
                //判断文件夹名称是否相同
                if (!nodeText.Equals(folderName))
                    list.Add(new NodePacked() { TreeeNode = node, FolderName = folder.Name, Count = charText == '(' ? folder.UnReadItemCount : folder.Items.Count });
            }
            //判断文件夹TotalCount|UnReadItemCount是否相同
            if (arrText.Length > 1)
            {
                nodeNumber = arrText[1].Substring(0, arrText[1].Length - 1).Trim();
                int count = 0;
                _Items olItems = folder.Items;
                if (charText == '[')
                    count = olItems.Count;
                else
                    count = folder.UnReadItemCount;
                if (!nodeNumber.Equals(count.ToString()))
                    list.Add(new NodePacked() { TreeeNode = node, FolderName = folder.Name, Count = count });

                olItems = null;
            }


            return nodeText;
        }

        /// <summary>
        /// show all folders
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="folders"></param>
        public static TreeNode[] ShowALLFolders(TreeView treeView, MAPIFolder olFolder, bool registerEvent)
        {
            List<TreeNode> _treeNodes = new List<TreeNode>();
            MAPIFolder refFolder = null;
            Folders _olFolders = olFolder.Folders;
            int count = _olFolders.Count;
            if (count > 0)
            {
                for (int i = 1; i <= count; i++)
                {
                    refFolder = _olFolders[i];
                    if (!FilterOutlookJunkFolder(refFolder))//过滤outlook垃圾邮件
                    {
                        TreeNode newNode = ConvertMAPIFolderToTreeNode(refFolder);//将MAPIFolder转化成TreeNode

                        #region 获取收件箱文件夹以及子文件夹事件
                        if (registerEvent)
                            RegisterAllFoldersEvents(refFolder);
                        #endregion

                        Folders _oFolders = refFolder.Folders;
                        if (_oFolders.Count > 0)//如果文件夹下存在子文件夹就给创建一个TreeNode节点
                            AddShamNode(newNode);

                        _treeNodes.Add(newNode);
                        _oFolders = null;
                    }
                    refFolder = null;
                }
            }
            _olFolders = null;

            return _treeNodes.ToArray();
        }

        public static bool ExsitsSameRootFolderName(List<string> folders)
        {
            bool isExsits = false;
            List<string> temp = new List<string>();
            foreach (string name in folders)
            {
                if (temp.Contains(name))
                {
                    isExsits = true;
                    break;
                }
                else
                    temp.Add(name);

            }
            return isExsits;
        }

        /// <summary>
        /// 找到根目录相同文件夹名称
        /// </summary>
        /// <returns></returns>
        public static string GetSameRootFolderName(List<string> folders)
        {
            string rootFolderName = string.Empty;
            List<string> temp = new List<string>();
            foreach (string name in folders)
            {
                if (temp.Contains(name))
                {
                    rootFolderName = name;
                    break;
                }
                else
                    temp.Add(name);
            }

            return rootFolderName;
        }

        public static void RegisterFolderEvents(MAPIFolder refFolder)
        {
            //找到草稿, 发件箱, 垃圾邮件文件夹
            //bool isInDOJfolders = MailUtility.IsInDOJFolders(refFolder.Name);
            RegisterFolderEvents(refFolder, true);
        }


        public static void RegisterAllFoldersEvents(MAPIFolder refFolder)
        {

            //找到草稿, 发件箱, 垃圾邮件文件夹
            //bool isTotalCount = MailUtility.IsInDOJFolders(refFolder.Name);
            //if (isTotalCount)
            //{
            //     RegisterFolderEvents(refFolder, false);
            //    return;
            //}
            //其他新增邮件文件夹
            //else
            // {
            RegisterFolderEvents(refFolder, true);
            // }
        }

        /// <summary>
        /// 将MAPIFolder 注册监听事件
        /// </summary>
        /// <param name="olFolder"></param>
        /// <param name="isChanged"></param>
        public static void RegisterFolderEvents(MAPIFolder olFolder, bool isChanged)
        {
            FolderWrapper.Wrapper(olFolder, isChanged);
        }

        /// <summary>
        /// 根据文件夹名称和文件夹的完整路径查找对应的文件夹节点
        /// </summary>
        /// <param name="tcNodes"></param>
        /// <param name="folderName"></param>
        /// <param name="fullFolderPath"></param>
        /// <returns></returns>
        private static bool gotIt = false;
        public static TreeNode FindNodeByFolderFullPath(TreeNodeCollection treeNodeCollection, string folderName, string fullFolderPath, string entryID)
        {
            string lowerFolderPath = fullFolderPath.ToLower();
            if (DicFolderPathAndNodes.ContainsKey(lowerFolderPath))
            {
                return DicFolderPathAndNodes[lowerFolderPath];//从缓存中取
            }
            else
            {
                nodes.Clear();
                gotIt = false;
                foreach (TreeNode node in treeNodeCollection)
                {
                    if (gotIt)
                        continue;
                    InnerSearchSingleNode(fullFolderPath, entryID, node);//找到文件夹后缓存到集合中
                }

                if (nodes.Count > 0)
                {
                    if (nodes.Count == 1)
                        return nodes[0];
                    else
                    {
                        return GetSameLevelTreeNode(nodes, fullFolderPath);//获取同一层的树节点
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private static TreeNode GetSameLevelTreeNode(List<TreeNode> treeNodes, string fullFolderPath)
        {
            foreach (TreeNode node in treeNodes)
            {
                string fullPath = node.FullPath;
                List<string> node_FullPath = MailUtility.SplitStringToList(fullPath, new char[1] { '\\' });

                string treeNodeFullPath = string.Empty;
                Array.ForEach(node_FullPath.ToArray(), item =>
                {
                    treeNodeFullPath += GetRealTreeNodeName(item);
                });

                string olFolderFullPath = string.Empty;
                List<string> folder_FullPath = MailUtility.SplitStringToList(fullFolderPath, new char[1] { '\\' });
                Array.ForEach(folder_FullPath.ToArray(), item =>
                {
                    olFolderFullPath += GetRealTreeNodeName(item);
                });

                if (treeNodeFullPath.Equals(olFolderFullPath))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// 通过MAPIFolder文件夹找到TreeNode，再将TreeNode展开
        /// </summary>
        /// <param name="olFolder"></param>
        public static void ExpandByMAPIFolder(TreeView treeView, FolderInfo folderInfo)
        {
            if (treeView.IsDisposed)
                return;

            if (treeView.InvokeRequired)
            {
                treeView.Invoke((System.Windows.Forms.MethodInvoker)
                    (() => InnerExpandByParentFullPath(treeView, folderInfo)));
            }
            else
                InnerExpandByParentFullPath(treeView, folderInfo);
        }

        /// <summary>
        /// 找到父节点后，去展开子节点
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="olFolder"></param>
        private static void InnerExpandByParentFullPath(TreeView treeView, FolderInfo folderInfo)
        {

            TreeNode targetNode = FindNodeByFolderFullPath(treeView.Nodes, folderInfo.Name,
                                                                             folderInfo.FullPath,
                                                                             folderInfo.EntryID);
            if (targetNode != null)
            {
                if (!targetNode.IsExpanded)
                    targetNode.Expand();
            }
            else
            {
                //这种情况只会在刚打开邮件中心时发生
                if (!noFoundFolders.Contains(folderInfo))
                    noFoundFolders.Add(folderInfo);
            }
        }
        /// <summary>
        /// 根据文件夹名称搜索过程中缓存搜索到的节点
        /// </summary>
        static List<TreeNode> nodes = new List<TreeNode>();
        private static void AddTreeNodeToList(TreeNode node)
        {
            if (!nodes.Contains(node))
                nodes.Add(node);
        }

        /// <summary>
        /// 找到文件夹后，缓存到集合中
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="fullFolderPath"></param>
        /// <param name="entityID"></param>
        /// <param name="startNode"></param>
        private static void InnerSearchSingleNode(string folderFullPath, string entryID, TreeNode startNode)
        {
            if (startNode != null)
            {
                TreeNodeCollection tnCollection = startNode.Nodes;
                int count = tnCollection.Count;
                for (int i = 0; i < count; i++)
                {
                    if (gotIt)
                        return;
                    TreeNode _node = tnCollection[i];
                    if (_node.Tag != null && String.Compare(_node.Tag.ToString(), entryID) == 0)
                    {
                        AddItemToDictionary(folderFullPath, _node);
                        AddTreeNodeToList(_node);
                        gotIt = true;
                        break;
                    }

                    InnerSearchSingleNode(folderFullPath, entryID, _node);
                }
            }
        }
        /// <summary>
        /// 根据文件夹名称和文件夹所包含的未读项数量设置文件夹节点的名称
        /// </summary>
        /// <param name="tcNodes"></param>
        /// <param name="unreadItemCount"></param>
        /// <param name="folderName"></param>
        /// <param name="fullPathName"></param>
        public static TreeNode SetFolderNodeText(TreeNodeCollection nodes, int count, string folderName, String fullPathName, string entityID)
        {
            TreeNode targetNode = FindNodeByFolderFullPath(nodes, folderName, fullPathName, entityID);//根据文件夹名称和文件夹的路径查找对应的文件夹节点
            if (targetNode == null)
            {
                Framework.CommonLibrary.Logger.Log.Error(string.Format("The '{0}' not found!", fullPathName));
            }
            else
            {
                if (!NodeInfo.HasSameTreeNodeCount(targetNode.ToolTipText, count))//两次文件夹的数量对比，如果相同，就不需要在去设置文件夹了
                    InnerSetTreeNodeText(targetNode, count, folderName);
            }
            //把正在同步outlook文件夹时修改文件夹数量的文件夹给记录下来，当同步完后，就不需要再去同步该文件夹了，以免造成与outlook数量不一致
            if (ClientProperties.IsSynchronizingFolders)
            {
                ClientProperties.CurrentSelectedNode = targetNode;
            }
            //else
            //{
            //    throw new NullReferenceException(string.Format("在方法FindNodeByFolderFullPath中,查找目标文件夹（名称:{0},完整路径:{1})失败", folderName, fullPathName));
            //}           
            return targetNode;
        }

        /// <summary>
        /// 获取真正的TreeNode文本
        /// </summary>
        /// <param name="arrNode"></param>
        /// <returns></returns>
        public static string GetRealTreeNodeName(string arrNode)
        {
            if (!string.IsNullOrEmpty(arrNode))
            {
                string node = arrNode;
                if (node.Contains("[") && node.EndsWith("]"))
                {
                    node = SplitTreeNodeName('[', node);
                }

                if (node.Contains("(") && node.EndsWith(")"))
                {
                    node = SplitTreeNodeName('(', node);
                }

                return ConvertSpecialCharacter(node);
            }

            return string.Empty;
        }

        /// <summary>
        /// 转换特殊字符
        /// </summary>
        public static string ConvertSpecialCharacter(string arrNode)
        {
            string node = arrNode;
            if (node.Contains("%2F"))
            {
                node = node.Replace("%2F", "/");
            }
            if (node.Contains("%5C"))
            {
                node = node.Replace("%5C", @"\");
            }
            //if (node.Contains("%"))
            //{
            //    node = node.Replace("%", "/");
            //}


            return node;
        }
        /// <summary>
        /// 将树节点名称分割
        /// </summary>
        /// <param name="charText"></param>
        /// <param name="arrNode"></param>
        /// <returns></returns>
        private static string SplitTreeNodeName(char charText, string arrNode)
        {
            string node = string.Empty;
            string[] splitNodes = arrNode.Split(charText);
            if (splitNodes != null)
            {
                if (splitNodes.Length == 2)
                    node = splitNodes[0];

                if (splitNodes.Length > 2)
                {
                    for (int i = 0; i < splitNodes.Length - 1; i++)
                        node += splitNodes[i];
                }
            }
            return node;
        }

        /// <summary>
        /// 根据文件夹名称和文件夹所包含的未读项数量设置文件夹节点的名称
        /// </summary>
        /// <param name="node"></param>
        /// <param name="count"></param>
        /// <param name="folderName"></param>
        public static void InnerSetTreeNodeText(TreeNode node, int count, string folderName)
        {
            bool isContains = MailUtility.IsInDOJFolders(folderName);//判断是否是草稿箱、发件箱、垃圾邮件
            NodeInfo nodeInfo = NodeInfo.GetNodeInfo(isContains, count, folderName);//获取单个节点信息
            SetNodeAppearance(node, nodeInfo);//设置节点信息
        }


        /// <summary>
        /// 将outlook新节点的EntryID替换对应TreeView节点的EntryID
        /// </summary>
        /// <param name="oFullFolderPath"></param>
        /// <param name="targetFolder"></param>
        /// <param name="_oNode"></param>
        public static void ReplaceEntryID(string oFullFolderPath, MAPIFolder targetFolder, string folderName, ref  string entryID)
        {
            if (IsSameAccount(oFullFolderPath, targetFolder.FullFolderPath))
            {
                MailUtility.ReleaseComObject(targetFolder);
                return;
            }

            Folders _oFolders = targetFolder.Folders;
            MAPIFolder olFolder = _oFolders.OfType<MAPIFolder>().SingleOrDefault(item => folderName.Contains(item.Name));

            if (olFolder != null)
            {
                entryID = olFolder.EntryID;
                MailUtility.ReleaseComObject(olFolder);
            }

            MailUtility.ReleaseComObject(_oFolders);
        }

        /// <summary>
        /// 实现在链两个不同账户之间文件夹拖拽后，entryID需要改变
        /// </summary>
        /// <param name="oFullFolderPath"></param>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static bool IsSameAccount(string oFullFolderPath, string fullPath)
        {
            bool isSame = false;
            if (oFullFolderPath != null && oFullFolderPath.StartsWith("\\"))
            {
                List<string> list = MailUtility.SplitStringToList(oFullFolderPath, new char[1] { '\\' });
                if (list != null && list.Count > 0)
                {
                    if (fullPath.Contains(list[0]))
                    {
                        isSame = true;
                    }
                }
            }

            return isSame;
        }

        public static void UpdateNodeText(MAPIFolder currentFolder, TreeNode node, string folderName)
        {
            int count = 0;
            bool isContains = IsContainsTotalCountFolder(currentFolder, out count);
            NodeInfo nodeInfo = NodeInfo.GetNodeInfo(isContains, count, folderName);
            SetNodeAppearance(node, nodeInfo);
        }

        private static void SetNodeAppearance(TreeNode node, NodeInfo nodeInfo)
        {
            node.Text = nodeInfo.Text;
            node.ToolTipText = nodeInfo.ToolTip;
            //为了减少TreeView加载过多节点后，渲染会闪烁（未读邮件从1变为0或0变为1时，样式会发生变化）
            if (nodeInfo.NodeFont != node.NodeFont)
            {
                node.NodeFont = nodeInfo.NodeFont;
                node.ForeColor = nodeInfo.ForeColor;
            }
        }


        private static bool IsContainsTotalCountFolder(MAPIFolder olFolder, out int count)
        {
            bool isContains = MailUtility.IsInDOJFolders(olFolder.Name);
            count = isContains ? olFolder.Items.Count : olFolder.UnReadItemCount;


            return isContains;
        }

        /// <summary>
        /// 设置文件夹显示图标
        /// </summary>
        /// <param name="lower_FolderName"></param>
        /// <returns></returns>
        public static int GetImageIndex(string lower_FolderName)
        {
            if (MailUtility.FixedFolders.Any(item => item.Equals(lower_FolderName)))
            {
                if (MailUtility.InBoxFolder.Any(item => item.Equals(lower_FolderName))) //收件箱
                {
                    return 1;
                }
                else if (MailUtility.DraftsFolders.Any(item => item.Equals(lower_FolderName))) //草稿箱
                {
                    return 2;
                }
                else if (MailUtility.OutBoxFolder.Any(item => item.Equals(lower_FolderName))) //已发送邮件
                {
                    return 3;
                }
                else if (MailUtility.DeleteItemsFolders.Any(item => item.Equals(lower_FolderName))) //已删除邮件
                {
                    return 4;
                }

                else if (MailUtility.SentFolders.Any(item => item.Equals(lower_FolderName))) //发件箱
                {
                    return 5;
                }
                else if (MailUtility.JunE_MailFolder.Any(item => item.Equals(lower_FolderName))) //垃圾邮件
                {
                    return 6;
                }

                #region Invalidate
                //else if (MailUtility.RSSFolder.Contains(lower_FolderName))  //RSS阅读
                //{
                //    return 5;
                //}
                //else if (MailUtility.Calendars.Contains(lower_FolderName))  //日历
                //{
                //    index = 10;
                //}
                //else if (MailUtility.Journals.Contains(lower_FolderName))  //日记
                //{
                //    index = 11;
                //}
                //else if (MailUtility.Tasks.Contains(lower_FolderName))   //任务
                //{
                //    index = 12;
                //}
                //else if (MailUtility.Notes.Contains(lower_FolderName))  //便笺
                //{
                //    index = 13;
                //}
                //else if (MailUtility.Contacts.Contains(lower_FolderName))  //联系人
                //{
                //    index = 14;
                //}

                #endregion

                else if (MailUtility.SearchFolders.Any(item => item.Equals(lower_FolderName))) //搜索文件夹以及搜索文件夹下子文件夹
                {
                    return 9;
                }
            }
            else
            {
                //根目录文件夹
                if (MailListPresenter.IsContainsRootFolder(lower_FolderName))
                {
                    if (GetDefaultUserFolderName().ToLower().Equals(lower_FolderName))
                        return 0;
                    else
                        return 8;
                }
                else //用户新增的文件夹
                    return 7;
            }

            return 7;
        }

        /// <summary>
        /// 将FolderName更改成FolderName1, FolderName[1]=>FolderName1[1], FolderName(1)=>FolderName1(1)
        /// </summary>
        /// <param name="node"></param>
        public static void RenameTreeNode(TreeNode node)
        {
            if (node == null) return;

            string nodeText = node.Text;
            if (nodeText.Contains("(") && nodeText.EndsWith(")"))
            {
                SetNodeText(node, nodeText, '(');
            }
            else if (nodeText.Contains("[") && nodeText.EndsWith("]"))
            {
                SetNodeText(node, nodeText, '[');
            }
            else
            {
                node.Text = string.Format("{0}_1", nodeText);
            }
        }

        static void SetNodeText(TreeNode node, string nodeText, char arrChar)
        {
            string[] arrText = MailUtility.SplitStringToArray(nodeText, new char[1] { arrChar });
            if (arrText != null && arrText.Length == 2)
            {
                node.Text = string.Format("{0}_1{1}{2}", arrText[0], arrChar.ToString(), arrText[1]);
            }
        }

        #region 搜索文件夹
        /// <summary>
        /// 遍历所有文件夹
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public static SearchFolderInfo SearchAllTreeNodes(TreeView treeView, string keyWord)
        {
            SearchFolderInfo folderInfo = new SearchFolderInfo();
            folderInfo.HasFoundTreeNode = false;
            int count = treeView.Nodes.Count;

            for (int i = 0; i < count; i++)
            {
                if (folderInfo.HasFoundTreeNode)
                    return folderInfo;
                //根目录没有展开文件夹
                if (!treeView.Nodes[i].IsExpanded)
                {
                    bool hasFoundTreeNode = GetMatchMAPIFolderByKeyWord(keyWord, treeView.Nodes[i], ref folderInfo);
                    if (hasFoundTreeNode)
                        break;
                    continue;
                }

                SearchSingleTreeNode(treeView.Nodes[i], keyWord, ref folderInfo);
            }
            return folderInfo;
        }
        /// <summary>
        /// 查找匹配的文件夹
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="keyWord"></param>
        /// <param name="dataList"></param>
        public static void SearchSingleTreeNode(TreeNode treeNode, string keyWord, ref SearchFolderInfo folderInfo)
        {
            if (treeNode != null)
            {
                TreeNodeCollection tcNodes = treeNode.Nodes;
                int count = tcNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    if (folderInfo.HasFoundTreeNode)
                        return;

                    TreeNode _node = tcNodes[i];
                    //只要有找到文件夹就直接返回
                    if (_node.Text.ToLower().Contains(keyWord))
                    {
                        folderInfo.HasFoundTreeNode = true;
                        folderInfo.TreeNode = _node;
                        folderInfo.FoundInOutlookFolder = false;
                        folderInfo.Tag = _node.Tag;
                        folderInfo.Level = -1;
                        break;
                    }

                    //查找Outlook中文件夹
                    if (!_node.IsExpanded)
                    {
                        bool hasFoundTreeNode = GetMatchMAPIFolderByKeyWord(keyWord, _node, ref folderInfo);
                        if (hasFoundTreeNode)
                        {
                            break;
                        }
                    }

                    SearchSingleTreeNode(_node, keyWord, ref folderInfo);
                }
            }
        }

        /// <summary>
        /// 根据输入的关键字去查找Outlook中的文件夹和该文件夹的所有子文件夹
        /// </summary>
        /// <returns></returns>
        public static bool GetMatchMAPIFolderByKeyWord(string keyWord, TreeNode parentNode, ref SearchFolderInfo folderInfo)
        {
            if (parentNode.Tag != null && parentNode.Nodes.Count > 0)
            {
                MAPIFolder olFolder = MailListPresenter.GetFolderByEntryID(parentNode.Tag.ToString());
                if (olFolder != null)
                {
                    bool foundMAPIFolder = false;
                    GetMAPIFolderByAllSubMAPIFolder(keyWord, olFolder, ref foundMAPIFolder, ref folderInfo);

                    MailUtility.ReleaseComObject(olFolder);
                    if (foundMAPIFolder)
                    {
                        folderInfo.TreeNode = parentNode;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 如果子节点没有展开，将其展开
        /// </summary>
        /// <param name="node"></param>
        public static void ExpandAllSubTreeNodes(TreeNode node)
        {
            try
            {
                if (!string.IsNullOrEmpty(node.FullPath))
                {
                    string[] level = MailUtility.SplitStringToArray(node.FullPath, @"\".ToCharArray());
                    if (level != null && level.Length > 0)
                    {
                        int length = level.Length;
                        EnsureTreeNodeExpand(node, length);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 确保所有父节点关系节点展开
        /// </summary>
        /// <param name="currentNode">父节点</param>
        /// <param name="level">指要展开节点的父节点的层次</param>
        private static void EnsureTreeNodeExpand(TreeNode currentNode, int level)
        {
            if (currentNode != null && level > 0)
            {
                if (!currentNode.IsExpanded && currentNode.Tag != null)
                    currentNode.Expand();

                EnsureTreeNodeExpand(currentNode.Parent, level--);
            }
        }


        /// <summary>
        /// 查找文件夹下所有子文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="parentFolder"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static void GetMAPIFolderByAllSubMAPIFolder(string folderName, MAPIFolder parentFolder, ref bool foundMAPIFolder, ref SearchFolderInfo folderInfo)
        {
            if (parentFolder != null)
            {
                if (parentFolder.Name.ToLower().Contains(folderName))
                {
                    List<string> folder_FullPath = MailUtility.SplitStringToList(parentFolder.FullFolderPath, new char[1] { '\\' });
                    folderInfo.FolderPaths = folder_FullPath;
                    folderInfo.HasFoundTreeNode = true;

                    folderInfo.Level = folder_FullPath.Count;
                    folderInfo.Tag = parentFolder.EntryID;
                    folderInfo.FoundInOutlookFolder = true;
                    foundMAPIFolder = true;

                    MailUtility.ReleaseComObject(parentFolder);
                }
                else
                {
                    foreach (MAPIFolder subFolder in parentFolder.Folders)
                    {
                        if (foundMAPIFolder)
                            break;

                        GetMAPIFolderByAllSubMAPIFolder(folderName, subFolder, ref foundMAPIFolder, ref folderInfo);
                    }

                    MailUtility.ReleaseComObject(parentFolder);
                }
            }
        }

        #endregion

        #region 添加一个树节点
        public static void AddTreeNode(TreeNode selectedNode, Folders olFolders, string newFolderName, bool isRegister)
        {

            MAPIFolder olFolder = olFolders.OfType<MAPIFolder>().SingleOrDefault(item => item.Name.Equals(newFolderName));
            if (olFolder != null)
            {
                AddShamAndRealNode(selectedNode, olFolder, isRegister);
                MailUtility.ReleaseComObject(olFolder);
            }
            //foreach (Folder olF in olFolders)
            //{
            //    if (olF.Name == newFolderName)
            //    {
            //        AddShamAndRealNode(selectedNode, olF, isRegister);
            //        break;
            //    }

            //    MailUtility.ReleaseComObject(olF);
            //}
        }

        public static void MoveFolderToFolder(TreeNodeCollection tnCollection, MAPIFolder tFolder, string arrOriginalNode, string arrTargetNode)
        {
            TreeNode tNode = null;
            FindTreeNode(tnCollection, arrTargetNode, ref tNode);
            if (tNode != null)
            {
                TreeNode newNode = (TreeNode)_folderPart.trvFolder.SelectedNode.Clone();
                _folderPart.trvFolder.SelectedNode.Remove();
                AddTreeNode(tNode, tFolder.Folders, arrOriginalNode, true);
            }
        }

        /// <summary>
        /// 根据文件夹名称找到TreeNode
        /// </summary>
        /// <param name="tnCollection"></param>
        /// <param name="nodeName"></param>
        /// <param name="findNode"></param>
        public static void FindTreeNode(TreeNodeCollection tnCollection, string nodeName, ref TreeNode targetNode)
        {
            foreach (TreeNode rootNode in tnCollection)
            {
                if (rootNode.Text == nodeName)
                {
                    targetNode = rootNode;
                    break;
                }
                FindTreeNode(rootNode.Nodes, nodeName, ref targetNode);
            }
        }

        /// <summary>
        /// 根据文件夹名称查找TreeView节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="nodeName"></param>
        /// <param name="targetNode"></param>
        public static void FindTreeNodeByName(TreeNode treeNode, string nodeName, ref TreeNode targetNode)
        {
            FindTreeNode(treeNode.Nodes, nodeName, ref treeNode);
        }
        /// <summary>
        /// 通过Tag值获取树形节点
        /// </summary>
        /// <param name="tnCollection">节点集合</param>
        /// <param name="tagValue">Tag值</param>
        /// <param name="findNode">查找的TreeNode</param>
        public static void FindTreeNodeByTag(TreeNodeCollection tnCollection, string tagValue, ref TreeNode findNode)
        {
            foreach (TreeNode rootNode in tnCollection)
            {
                if (rootNode.Tag == null)
                    continue;
                if (rootNode.Tag.ToString().Equals(tagValue))
                {
                    findNode = rootNode;
                    break;
                }
                FindTreeNodeByTag(rootNode.Nodes, tagValue, ref findNode);
            }
        }

        /// <summary>
        /// 注册一个指定的MAPIFolder
        /// </summary>
        /// <param name="olFolder"></param>
        /// <param name="newFolderName"></param>
        public static void RegisterTreeNodeEvent(MAPIFolder olFolder, string newFolderName)
        {
            MAPIFolder _folder = olFolder.Folders[newFolderName];
            if (_folder != null)
            {
                RegisterFolderEvents(_folder, true);
                _folder = null;
            }
        }

        #endregion

        #region IDisposable 成员

        public static void Dispose()
        {
            _folderPart = null;
            _SmartParts = null;
            parentTreeNodes = null;
            subTreeNodes = null;
            currentTreeNodes = null;
            arrTreeNodes = null;
            DicFolderPathAndNodes.Clear();
        }

        #endregion
    }
}
