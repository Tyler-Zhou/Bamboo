#region Comment

/*
 * 
 * FileName:    PDFConvert.cs
 * CreatedOn:   2014/5/19 星期一 17:23:44
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->PDF转换帮助类：将Text、Image、Word、PPT、Excel、HTML等文件转换为PDF文件以便在窗体预览
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Generator;
using iTextSharp.text.pdf;

namespace ICP.Document
{
    /// <summary>
    /// PDF转换
    /// </summary>
    public class PDFConvert
    {
        #region 合并多个PDF文件产生单个PDF文件
        /// <summary>
        /// 合并多个PDF文件产生单个PDF文件
        /// </summary>
        /// <param name="fileList">pdfs</param>
        /// <returns>返回产生的PDF文件路径</returns>
        public String MergePDFFiles(List<string> fileList)
        {
            fileList = fileList.FindAll(fileFullPath => !string.IsNullOrEmpty(fileFullPath)).ToList();
            string resultFilePath = string.Empty;
            iTextSharp.text.pdf.PdfReader reader;
            string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), ".pdf");
            resultFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(resultFilePath, FileMode.Create));
            document.Open();
            PdfContentByte contentByte = writer.DirectContent;
            PdfImportedPage newPage;
            int rotation = 0;
            try
            {
                if (fileList != null)
                {
                    if (fileList.Count == 1)
                    {
                        resultFilePath = fileList[0];
                    }
                    else
                    {
                        foreach (String file in fileList)
                        {
                            reader = new iTextSharp.text.pdf.PdfReader(file);
                            int totalPages = reader.NumberOfPages;
                            for (int i = 1; i <= totalPages; i++)
                            {
                                document.NewPage();
                                newPage = writer.GetImportedPage(reader, i);

                                rotation = reader.GetPageRotation(i);
                                if (rotation == 90 || rotation == 270)
                                    contentByte.AddTemplate(newPage, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                                else
                                    contentByte.AddTemplate(newPage, 1f, 0, 0, 1f, 0, 0);
                            }
                            reader.Close();

                        }

                        document.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return resultFilePath;
        } 
        #endregion

        #region 从Text文件转换
        /// <summary>
        /// 从Text文件转换
        /// </summary>
        /// <param name="paramTxtPath">Text 文件路径</param>
        /// <returns></returns>
        public String FromText(String paramTxtPath)
        {
            IOHelper.CheckFileExists(paramTxtPath, true);

            String targetFile = GetTargetFilePath(paramTxtPath);

            using (StreamReader reader = new StreamReader(paramTxtPath, true))
            {

                Pdf pdf = new Pdf();
                Aspose.Pdf.Generator.Section section = pdf.Sections.Add();
                section.PageInfo.Margin.Left = 110;
                section.PageInfo.Margin.Right = 120;
                string content = reader.ReadToEnd();
                Text text = new Text();
                section.Paragraphs.Add(text);
                text.Segments.Add(content);
                text.TextInfo.FontName = "SimSun";

                pdf.SetUnicode();

                pdf.Save(targetFile);
                reader.Close();

            }
            return targetFile;
        } 
        #endregion

        #region 将Image转换问PDF文件
        /// <summary>
        /// 将Image转换问PDF文件
        /// </summary>
        /// <param name="imageFilePath">Image 文件路径</param>
        public String FromImage(String paramImgPath)
        {
            IOHelper.CheckFileExists(paramImgPath, true);

            String targetFile = GetTargetFilePath(paramImgPath);
            System.Drawing.Image sourceImg = null;
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            try
            {
                using (FileStream stream = new FileStream(targetFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    sourceImg = System.Drawing.Image.FromFile(paramImgPath);
                    var image = iTextSharp.text.Image.GetInstance(sourceImg, sourceImg.RawFormat);
                    image.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    float height = document.Top - document.TopMargin;
                    //图片原始大小
                    image.ScaleToFit(sourceImg.Width > document.Right ? document.Right : sourceImg.Width, sourceImg.Height > height ? height : sourceImg.Height);

                    document.Add(image);

                    document.Close();
                }
            }

            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                sourceImg = null;
                document.Close();
            }

            return targetFile;
        } 
        #endregion

        #region 将Word文件转换PDF文件
        /// <summary>
        /// 将Word文件转换PDF文件
        /// </summary>
        /// <param name="infile">Word 文件路径</param>
        public String FromWord(String paramWordPath)
        {
            IOHelper.CheckFileExists(paramWordPath, true);
            //Open an Doc file
            string outputFile = GetTargetFilePath(paramWordPath);
            Aspose.Words.Document doc = new Aspose.Words.Document(paramWordPath);
            //Save the doc in PDF format
            doc.Save(outputFile, Aspose.Words.SaveFormat.Pdf);
            doc = null;

            return outputFile;
        } 
        #endregion

        #region 将PPT文件转换PDF文件
        /// <summary>
        /// 将PPT文件转换PDF文件
        /// </summary>
        /// <param name="paramPPTPath">PPT 文件路径</param>
        /// <returns></returns>
        public String FromPPT(String paramPPTPath)
        {
            IOHelper.CheckFileExists(paramPPTPath, true);
            string outputFile = GetTargetFilePath(paramPPTPath);
            //Instantiate a PresentationEx object that represents a PPTX file
            Aspose.Slides.Pptx.PresentationEx pres = new Aspose.Slides.Pptx.PresentationEx(paramPPTPath);
            //Saving the PPTX presentation to PDF document
            pres.Save(outputFile, Aspose.Slides.Export.SaveFormat.Pdf);
            pres = null;

            return outputFile;
        } 
        #endregion

        #region 将Excel转换为PDF文件
        /// <summary>
        /// 将Excel转换为PDF文件
        /// </summary>
        /// <param name="paramExcelPath">Excel 文件路径</param>
        /// <returns></returns>
        public String FromExcel(String paramExcelPath)
        {
            IOHelper.CheckFileExists(paramExcelPath, true);
            string outputFile = GetTargetFilePath(paramExcelPath);
            //Open an Excel file
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(paramExcelPath);
            //Save the document in PDF format
            workbook.Save(outputFile, Aspose.Cells.SaveFormat.Pdf);
            workbook = null;

            return outputFile;
        } 
        #endregion

        #region 将HTML转换为PDF文件
        /// <summary>
        /// 将HTML转换为PDF文件
        /// </summary>
        /// <param name="paramHTMLPath">HTML 文件路径</param>
        /// <returns></returns>
        public String FromHTML(String paramHTMLPath)
        {
            IOHelper.CheckFileExists(paramHTMLPath, true);
            string outputFilePath = string.Empty;
            try
            {
                // Instantiate an object PDF class
                Aspose.Pdf.Generator.Pdf pdf = new Aspose.Pdf.Generator.Pdf();
                // add the section to PDF document sections collection
                Aspose.Pdf.Generator.Section section = pdf.Sections.Add();
                // Read the contents of HTML file into StreamReader object
                using (StreamReader reader = System.IO.File.OpenText(paramHTMLPath))
                {
                    //Create text paragraphs containing HTML text
                    Aspose.Pdf.Generator.Text text2 = new Aspose.Pdf.Generator.Text(section, reader.ReadToEnd());
                    // enable the property to display HTML contents within their own formatting
                    text2.IsHtmlTagSupported = true;
                    //Add the text paragraphs containing HTML text to the section
                    section.Paragraphs.Add(text2);
                    pdf.IsAutoFontAdjusted = true;
                    // embed the font subset in resultant PDF
                    pdf.SetUnicode();
                    // Specify the URL which serves as images database
                    outputFilePath = GetTargetFilePath(paramHTMLPath);
                    //pdf.HtmlInfo.ImgUrl = string.Format(@"{0}\", Path.GetDirectoryName(outputFilePath));
                    //Save the pdf document
                    pdf.Save(outputFilePath);

                    reader.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return outputFilePath;
        } 
        #endregion

        #region 获取文件路径
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        private static String GetTargetFilePath(String inputFile)
        {
            string rootPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, IOHelper.HtmlTempPath);
            return Path.Combine(rootPath, Path.GetFileNameWithoutExtension(inputFile) + ".pdf");
        }
        #endregion
    }
}
