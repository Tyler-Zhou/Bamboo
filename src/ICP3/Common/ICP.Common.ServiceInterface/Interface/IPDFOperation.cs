
namespace ICP.Common.ServiceInterface
{  
    /// <summary>
    /// PDF操作接口
    /// </summary>
   public interface IPDFOperation
    {  
       /// <summary>
        /// 载入单个文档(文档可以是非PDF类型，将在内部转换)
       /// </summary>
       /// <param name="filePath">文档的绝对路径</param>
       void Load(string filePath);
       /// <summary>
       /// 载入多个文档，文档将在内部转换合并成单个文档然后显示(文档可以是非PDF类型，将在内部转换)
       /// </summary>
       /// <param name="filePaths">文档的绝对路径</param>
       void Load(string[] filePaths);
       void Print();

    }
}
