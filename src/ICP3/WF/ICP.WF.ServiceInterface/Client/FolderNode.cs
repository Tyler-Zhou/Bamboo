

namespace ICP.WF.ServiceInterface.Client
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 文件夹节点
    /// </summary>
    public class DocumentNode
    {
        public DocumentNode()
        {
            this.Childs = new FolderCollection();
        }

        List<DocumentNode> documentList { get; set; }

        public List<DocumentNode> GetList()
        {
            return documentList;
        }

        string rootPath;
       
        public DocumentNode(string rtPath,ToolboxGroupType boxType): this()
        {

            this.documentList = new List<DocumentNode>();
            System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(rtPath);
            
            DocumentNode node = new DocumentNode();
            node.Name = rootDir.Name;
            node.path = rtPath;
            node.ParentID =0 ;
            node.ID = 1;
            node.DocumentNodeType = DocumentNodeType.Folder;
            documentList.Add(node);
            Init(rootDir, node, this.documentList, boxType);
        }

      
        
        void Init(System.IO.DirectoryInfo parentDirInfo,DocumentNode node, List<DocumentNode> returnDocuments,ToolboxGroupType boxType)
        {
            //构造文件夹下文件对象
            System.IO.FileInfo[] files = null;
            if (boxType == ToolboxGroupType.Form)
            {
                files=parentDirInfo.GetFiles("*.xml", System.IO.SearchOption.TopDirectoryOnly);
            }
            else
            {
                files = parentDirInfo.GetFiles("*.xoml", System.IO.SearchOption.TopDirectoryOnly);
            }

            foreach (System.IO.FileInfo file in files)
            {
                DocumentNode fileNode = new DocumentNode();
                fileNode.Name = file.Name;
               
                fileNode.ID = GetList().Count + 2;
                fileNode.ParentID = node.ID;
                fileNode.Parent = node;
                fileNode.DocumentNodeType = DocumentNodeType.File;
                returnDocuments.Add(fileNode);

  
            }

            System.IO.DirectoryInfo[] dirs = parentDirInfo.GetDirectories();
            foreach (System.IO.DirectoryInfo dir in dirs)
            {
                //构造文件夹对象
                DocumentNode folderNode = new DocumentNode();
                folderNode.Name = dir.Name;
                folderNode.ID = GetList().Count+2;
                folderNode.ParentID = node.ID;
                folderNode.Parent = node;
                folderNode.DocumentNodeType = DocumentNodeType.Folder;
                returnDocuments.Add(folderNode);

                Init(dir, folderNode, returnDocuments, boxType);
            }
        }


        public int ID { get; set; }

        public int ParentID { get; set; }

        DocumentNode parent;
        /// <summary>
        /// 父文件夹
        /// </summary>
        public DocumentNode Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;

                if (parent != null)
                {
                    parent.Childs.Add(this);
                }
            }
        }

        /// <summary>
        /// 子文件夹
        /// </summary>
        public FolderCollection Childs { get; set; }

        /// <summary>
        /// 文件夹名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        public void Create()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(this.Path);
            if (!dir.Exists)
            {
                dir.Create();
            }
            foreach (DocumentNode folder in this.Childs)
            {
                folder.Create();
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        public System.IO.DirectoryInfo FindDirectory(string name)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(name);
            if (dir.Exists)
            {
                return dir;
            }

            foreach (DocumentNode folder in this.Childs)
            {
                dir = folder.FindDirectory(name);
                if (dir != null)
                {
                    break;
                }
            }

            return dir;
        }


        string path;
        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get
            {
                if (this.Parent != null)
                {
                    path = this.Parent.Path + "\\" + this.Name;
                }
                else
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        path = this.Name;
                    }
                }

                return path;
            }
            set
            {
                path = value;
            }
        }

        /// <summary>
        /// 文档类型
        /// </summary>
        public DocumentNodeType DocumentNodeType { get; set; }
    }

  
    public class FolderCollection : CollectionBase
    {

        public FolderCollection()
        {
        }


        public FolderCollection(FolderCollection value)
        {
            this.AddRange(value);
        }


        public FolderCollection(DocumentNode[] value)
        {
            this.AddRange(value);
        }

        public DocumentNode this[string name]
        {
            get
            {
                foreach (DocumentNode node in this.List)
                {
                    if (node.Name.Equals(name))
                    {
                        return node;
                    }
                }

                return null;
            }
        }

        public DocumentNode this[int index]
        {
            get
            {
                return ((DocumentNode)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }


        public int Add(DocumentNode value)
        {
            return List.Add(value);
        }


        public void AddRange(DocumentNode[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                this.Add(value[i]);
            }
        }


        public void AddRange(FolderCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                this.Add(value[i]);
            }
        }


        public bool Contains(DocumentNode value)
        {
            return List.Contains(value);
        }


        public void CopyTo(DocumentNode[] array, int index)
        {
            List.CopyTo(array, index);
        }


        public int IndexOf(DocumentNode value)
        {
            return List.IndexOf(value);
        }


        public void Insert(int index, DocumentNode value)
        {
            List.Insert(index, value);
        }

        public new FolderEnumerator GetEnumerator()
        {
            return new FolderEnumerator(this);
        }


        public void Remove(DocumentNode value)
        {
            List.Remove(value);
        }

        public class FolderEnumerator : object, IEnumerator
        {

            private IEnumerator baseEnumerator;

            private IEnumerable temp;

            public FolderEnumerator(FolderCollection mappings)
            {
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

            public DocumentNode Current
            {
                get
                {
                    return ((DocumentNode)(baseEnumerator.Current));
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return baseEnumerator.Current;
                }
            }

            public bool MoveNext()
            {
                return baseEnumerator.MoveNext();
            }

            bool IEnumerator.MoveNext()
            {
                return baseEnumerator.MoveNext();
            }

            public void Reset()
            {
                baseEnumerator.Reset();
            }

            void IEnumerator.Reset()
            {
                baseEnumerator.Reset();
            }
        }
    }


}
