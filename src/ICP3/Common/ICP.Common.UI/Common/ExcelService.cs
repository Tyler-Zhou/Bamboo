#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/27 星期三 11:46:14
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Linq;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using ICP.Common.ServiceInterface;
using System;
using System.Collections;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using AsposeCells = Aspose.Cells.Cells;
using AsposeColor = System.Drawing.Color;
using AsposeImage = System.Drawing.Image;

namespace ICP.Common.UI.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelService : IExcelService
    {
        /// <summary>
        /// DataTable 导出到 Excel
        /// </summary>
        /// <param name="datatable">数据源</param>
        /// <param name="filepath">文件路径</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public bool DataTableToExcelFile(DataTable datatable, string filepath, out string error)
        {
            error = "";
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                AsposeCells cells = sheet.Cells;

                int nRow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    nRow++;
                    try
                    {
                        for (int i = 0; i < datatable.Columns.Count; i++)
                        {
                            if (row[i].GetType().ToString() == "System.Drawing.Bitmap")
                            {
                                //------插入图片数据-------
                                AsposeImage image = (AsposeImage)row[i];
                                MemoryStream mstream = new MemoryStream();
                                image.Save(mstream, ImageFormat.Jpeg);
                                sheet.Pictures.Add(nRow, i, mstream);
                            }
                            else
                            {
                                cells[nRow, i].PutValue(row[i]);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        error = error + " DataTableToExcel: " + e.Message;
                    }
                }

                workbook.Save(filepath);
                return true;
            }
            catch (Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }
        }

        /// <summary>
        /// DataTable 导出 Excel
        /// </summary>
        /// <param name="datatable">数据源</param>
        /// <param name="filepath">文件路径</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public bool DataTableToExcelFile2(DataTable datatable, string filepath, out string error)
        {
            error = "";
            Workbook wb = new Workbook();

            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }

                //为单元格添加样式    
                Style style = wb.Styles[wb.Styles.Add()];
                //设置居中
                style.HorizontalAlignment = TextAlignmentType.Center;
                //设置背景颜色
                style.ForegroundColor = AsposeColor.FromArgb(153, 204, 0);
                style.Pattern = BackgroundType.Solid;
                style.Font.IsBold = true;

                int rowIndex = 0;
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    DataColumn col = datatable.Columns[i];
                    string columnName = col.Caption ?? col.ColumnName;
                    wb.Worksheets[0].Cells[rowIndex, i].PutValue(columnName);
                    wb.Worksheets[0].Cells[rowIndex, i].SetStyle(style);
                }
                rowIndex++;

                foreach (DataRow row in datatable.Rows)
                {
                    for (int i = 0; i < datatable.Columns.Count; i++)
                    {
                        wb.Worksheets[0].Cells[rowIndex, i].PutValue(row[i].ToString());
                    }
                    rowIndex++;
                }

                for (int k = 0; k < datatable.Columns.Count; k++)
                {
                    wb.Worksheets[0].AutoFitColumn(k, 0, 150);
                }
                wb.Worksheets[0].FreezePanes(1, 0, 1, datatable.Columns.Count);
                wb.Save(filepath);
                return true;
            }
            catch (Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }
        }

        /// <summary>
        /// List 导出到 Excel
        /// </summary>
        /// <param name="lists">数据源</param>
        /// <param name="filepath">文件路径</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public bool ListsToExcelFile(IList[] lists, string filepath, out string error)
        {
            error = "";
            //----------Aspose变量初始化----------------
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            AsposeCells cells = sheet.Cells;
            //-------------输入数据-------------
            int nRow = 0;
            sheet.Pictures.Clear();
            cells.Clear();
            foreach (IList list in lists)
            {

                for (int i = 0; i <= list.Count - 1; i++)
                {
                    try
                    {
                        Console.WriteLine(i + "  " + list[i].GetType());
                        if (list[i].GetType().ToString() == "System.Drawing.Bitmap")
                        {
                            //插入图片数据
                            AsposeImage image = (AsposeImage)list[i];

                            MemoryStream mstream = new MemoryStream();

                            image.Save(mstream, ImageFormat.Jpeg);

                            sheet.Pictures.Add(nRow, i, mstream);
                        }
                        else
                        {
                            cells[nRow, i].PutValue(list[i]);
                        }
                    }
                    catch (Exception e)
                    {
                        error = error + e.Message;
                    }

                }

                nRow++;
            }
            //-------------保存-------------
            workbook.Save(filepath);

            return true;
        }

        /// <summary>
        /// Excel 导出到 DataTable
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="datatable">结果集</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public bool ExcelFileToDataTable(string filepath, out DataTable datatable, out string error)
        {
            error = "";
            datatable = null;
            try
            {
                if (File.Exists(filepath) == false)
                {
                    error = "文件不存在";
                    datatable = null;
                    return false;
                }
                Workbook workbook = new Workbook(filepath);
                Worksheet worksheet = workbook.Worksheets[0];
                datatable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxRow + 1, worksheet.Cells.MaxColumn + 1);
                //-------------图片处理-------------
                PictureCollection pictures = worksheet.Pictures;
                if (pictures.Count > 0)
                {
                    string error2 = "";
                    if (InsertPicturesIntoDataTable(pictures, datatable, out datatable, out error2) == false)
                    {
                        error = error + error2;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Excel 导出到 IList
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="lists">结果集</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public bool ExcelFileToLists(string filepath, out IList[] lists, out string error)
        {
            error = "";
            lists = null;
            DataTable datatable = new DataTable();
            PictureCollection[] pictures;
            if (ExcelFileToDataTable(filepath, out datatable, out error) && GetPicturesFromExcelFile(filepath, out pictures, out error))
            {
                lists = new IList[datatable.Rows.Count];
                //------------DataTable转换成IList[]--------------
                //数据
                int nRow = 0;
                foreach (DataRow row in datatable.Rows)
                {
                    lists[nRow] = new ArrayList(datatable.Columns.Count);
                    for (int i = 0; i <= datatable.Columns.Count - 1; i++)
                    {
                        lists[nRow].Add(row[i]);
                    }
                    nRow++;
                }
                //图片
                foreach (Picture picture in pictures.SelectMany(t => t.Cast<Picture>()))
                {
                    try
                    {
                        //图片有可能越界
                        if (picture.UpperLeftRow <= datatable.Rows.Count && picture.UpperLeftColumn <= datatable.Columns.Count)
                        {
                            lists[picture.UpperLeftRow][picture.UpperLeftColumn] = picture.Data;
                        }
                    }
                    catch (Exception e)
                    {
                        error = error + e.Message;
                    }
                }
            }
            else
            {

                return false;
            }
            return true;
        }

        /// <summary>
        /// 从Excel中获取所有图片
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="pictures">图片集合</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        bool GetPicturesFromExcelFile(string filepath, out PictureCollection[] pictures, out string error)
        {
            error = "";
            pictures = null;
            bool result;
            try
            {
                if (!File.Exists(filepath))
                {
                    error = "文件不存在";
                    pictures = null;
                    result = false;
                }
                else
                {
                    Workbook workbook = new Workbook(filepath);
                    pictures = new PictureCollection[workbook.Worksheets.Count];
                    for (int i = 0; i < workbook.Worksheets.Count; i++)
                    {
                        pictures[i] = workbook.Worksheets[i].Pictures;
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 将图片插入DataTable中
        /// </summary>
        /// <param name="pictures">图片集合</param>
        /// <param name="fromdatatable">待新增图片数据源</param>
        /// <param name="datatable">新增图片后的结果集</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        bool InsertPicturesIntoDataTable(IEnumerable pictures, DataTable fromdatatable, out DataTable datatable, out string error)
        {
            error = "";
            datatable = fromdatatable;
            DataRow[] array = datatable.Select();
            foreach (Picture picture in pictures)
            {
                try
                {
                    Console.WriteLine(picture.GetType().ToString());
                    MemoryStream memoryStream = new MemoryStream();
                    memoryStream.Write(picture.Data, 0, picture.Data.Length);
                    AsposeImage value = AsposeImage.FromStream(memoryStream);
                    array[picture.UpperLeftRow][picture.UpperLeftColumn] = value;
                }
                catch (Exception ex)
                {
                    error = error + " InsertPicturesIntoDataTable: " + ex.Message;
                }
            }
            return true;
        }
    }
}
