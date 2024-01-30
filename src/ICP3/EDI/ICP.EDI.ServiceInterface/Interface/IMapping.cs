
using Altova.IO;

namespace ICP.EDI.ServiceInterface
{
    /// <summary>
    /// 所有插件必须实现该接口
    /// </summary>
    public interface IMapping
    {
        void Run(string sourceFilename, string targetFilename);


        void Run(Input source, Output target);
    }
}
