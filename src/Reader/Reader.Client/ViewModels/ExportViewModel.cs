using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 导出视图模型
    /// </summary>
    public class ExportViewModel : BaseViewModel
    {
        /// <summary>
        /// 导出视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public ExportViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
        }
    }
}
