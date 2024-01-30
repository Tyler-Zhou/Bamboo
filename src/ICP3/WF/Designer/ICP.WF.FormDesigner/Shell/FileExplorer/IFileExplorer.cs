
//-----------------------------------------------------------------------
// <copyright file="ISolutionExplorer.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;

    public interface IFileExplorerPart : IBasePart
    {
        /// <summary>
        /// 接点双击触发
        /// </summary>
        event EventHandler OpentDesignFormEvent;

        /// <summary>
        /// 添加一个文件夹节点
        /// </summary>
        /// <param name="nodeText">节点名称</param>
        void AddFolderFileNode(string nodeText);

        /// <summary>
        /// 添加一个文件夹节点
        /// </summary>
        /// <param name="nodeText">节点名称</param>
        /// <param name="fileName">文件夹路径</param>
        void AddFolderFileNode(string nodeText, string fileName);



        /// <summary>
        /// 添加一个表单节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        void AddFormFileNode(string nodeText);



        /// <summary>
        /// 添加一个表单节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        /// <param name="filePath">表单文件路径</param>
        void AddFormFileNode(string nodeText, string filePath);


        /// <summary>
        /// 添加一个数据源文件
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        void AddDataDesignFileNode(string nodeText);


        /// <summary>
        /// 添加一个数据源文件节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        /// <param name="fileName">文件名称</param>
        void AddDataDesignFileNode(string nodeText, string fileName);

        /// <summary>
        /// 删除指定文本节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        void RemoveFileNode(string nodeText);

        /// <summary>
        /// 删除所有节点

        /// </summary>
        void RemoveAllNode();
    }


    /// <summary>
    /// 节点类型
    /// </summary>
    public class TreeNodeType
    {
        private int id;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get 
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        private int parentID = 0;
        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentID
        {
            get
            {
                return parentID;
            }
            set
            {
                parentID = value;
            }
        }
        string fileName;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        string nodeText;
        /// <summary>
        /// 节点标题
        /// </summary>
        public string NodeText
        {
            get { return nodeText; }
            set { nodeText = value; }
        }

        TreeNodeTypeEnum type;
        /// <summary>
        /// 节点类型
        /// </summary>
        public TreeNodeTypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }
    }

    public enum TreeNodeTypeEnum
    {
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder,

        /// <summary>
        /// 表单文件
        /// </summary>
        FormFile,

        /// <summary>
        /// 数据源文件
        /// </summary>
        DataDesign
    }
}
