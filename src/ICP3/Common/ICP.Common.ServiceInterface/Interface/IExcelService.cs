#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/27 星期三 11:38:59
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Collections;
using System.Data;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.Common.ServiceInterface
{
    /// <summary>
    /// Excel 服务
    /// </summary>
    [ServiceContract]
    [ICPServiceHost]
    public interface IExcelService
    {
        /// <summary>
        /// DataTable 导出到 Excel
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="filepath"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OperationContract]
        bool DataTableToExcelFile(DataTable datatable, string filepath, out string error);
        /// <summary>
        /// DataTable 导出 Excel
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="filepath"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OperationContract]
        bool DataTableToExcelFile2(DataTable datatable, string filepath, out string error);

        /// <summary>
        /// List 导出到 Excel
        /// </summary>
        /// <param name="lists"></param>
        /// <param name="filepath"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OperationContract]
        bool ListsToExcelFile(IList[] lists, string filepath, out string error);
        /// <summary>
        /// Excel 导出到 DataTable
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="datatable"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OperationContract]
        bool ExcelFileToDataTable(string filepath, out DataTable datatable, out string error);

        /// <summary>
        /// Excel 导出到 IList
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="lists"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OperationContract]
        bool ExcelFileToLists(string filepath, out IList[] lists, out string error);
    }
}
