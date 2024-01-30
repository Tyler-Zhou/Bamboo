using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using System.Collections;
using Microsoft.Office.Core;
using Aspose.Pdf;
using Aspose.Pdf.Generator;
using Microsoft.Office.Interop.Outlook;
using System.Threading;

namespace ICP.DataCache.ServiceInterface1
{
    /// <summary>
    /// 文件转换帮助类
    /// </summary>
    public class ConvertFileHelper
    {
        static ConvertFileHelper()
        {
            Current = new ConvertFileHelper();
        }
        public static ConvertFileHelper Current
        {
            get;
            set;

        }
        object readOnly = (object)false;   // const 
        object m = System.Reflection.Missing.Value; // const 
        String tempDIR = String.Empty;// ICP.TaskCenter.ServiceInterface.DataObjects.AppCache.LocalFilePath;  //file local path

        bool isSave = false;
        /// <summary>
        /// 多个pdf合并成一个pdf文件
        /// </summary>
        /// <param name="fileList">pdfs</param>
        /// <param name="outMergeFile">pdf</param>
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
            catch (System.Exception ex)
            {
                throw ex;
            }

            return resultFilePath;
        }

        /// <summary>
        /// 将image转换问pdf文件
        /// </summary>
        /// <param name="inputFile">image文件</param>
        /// <param name="outputFile">pdf文件</param>
        public String ExportImage2PDF(String inputFile)
        {
            CheckFileExists(inputFile);

            String targetFile = GetTargetFilePath(inputFile);
            System.Drawing.Image sourceImg = null;
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            try
            {
                using (FileStream stream = new FileStream(targetFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    sourceImg = System.Drawing.Image.FromFile(inputFile);
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


        private static String GetTargetFilePath(String inputFile)
        {
            string rootPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, DataCacheUtility.HtmlTempPath);
            return Path.Combine(rootPath, Path.GetFileNameWithoutExtension(inputFile) + ".pdf");

        }

        /// <summary>
        /// 将Text文件生成pdf文件
        /// </summary>
        /// <param name="inputFile">text文件</param>
        public String ExportText2PDF(String inputFile)
        {
            CheckFileExists(inputFile);

            String targetFile = GetTargetFilePath(inputFile);

            using (StreamReader reader = new StreamReader(inputFile, true))
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

                // pdf.IsAutoFontAdjusted = true;
                pdf.SetUnicode();

                pdf.Save(targetFile);
                reader.Close();

            }
            return targetFile;
        }
        /// <summary>
        /// 将word文件转换pdf文件
        /// </summary>
        /// <param name="infile">word文件</param>
        public String ExportWord2PDF(String inputFile)
        {
            CheckFileExists(inputFile);
            //Open an Doc file
            string outputFile = GetTargetFilePath(inputFile);
            Aspose.Words.Document doc = new Aspose.Words.Document(inputFile);
            //Save the doc in PDF format
            doc.Save(outputFile, Aspose.Words.SaveFormat.Pdf);
            doc = null;

            return outputFile;
        }
        private void CheckFileExists(string fileFullPath)
        {
            if (!System.IO.File.Exists(fileFullPath))
            {
                throw new System.Exception(String.Format("{0} does not exist!", fileFullPath));
            }
        }

        public String ExportPPTtoPDF(String inputFile)
        {
            CheckFileExists(inputFile);
            string outputFile = GetTargetFilePath(inputFile);
            //Instantiate a PresentationEx object that represents a PPTX file
            Aspose.Slides.Pptx.PresentationEx pres = new Aspose.Slides.Pptx.PresentationEx(inputFile);
            //Saving the PPTX presentation to PDF document
            pres.Save(outputFile, Aspose.Slides.Export.SaveFormat.Pdf);
            pres = null;

            return outputFile;
        }

        /// <summary>
        /// 将excel转换为PDF
        /// </summary>
        /// <param name="infile">excel文件</param>
        /// <param name="outfile">pdf文件</param>
        public String ExportExcel2PDF(String inputFile)
        {
            CheckFileExists(inputFile);
            string outputFile = GetTargetFilePath(inputFile);
            //Open an Excel file
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(inputFile);
            //Save the document in PDF format
            workbook.Save(outputFile, Aspose.Cells.SaveFormat.Pdf);
            workbook = null;

            return outputFile;
        }

        /// <summary>
        /// 将文本文件写入另一文本文件中 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public String CopyNewFile(String inputFile, String outputFile)
        {
            String targetFile = outputFile;

            byte[] data = GetFileStream(inputFile);

            FileStream stream;
            if (System.IO.File.Exists(targetFile))
                stream = new FileStream(targetFile, FileMode.Truncate);
            else
                stream = new FileStream(targetFile, FileMode.CreateNew);

            BinaryWriter bw = new BinaryWriter(stream);
            bw.Write(data, 0, data.Length);
            bw.Flush();
            stream.Close();
            stream.Dispose();
            bw.Close();

            return targetFile;
        }

        /// <summary>
        /// 读取文件流
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public byte[] GetFileStream(String filePath)
        {
            using (FileStream sFile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[sFile.Length];
                sFile.Read(data, 0, (int)sFile.Length);
                return data;
            }
        }

        /// <summary>
        /// 将html转换为pdf文件
        /// </summary>
        /// <param name="inputFile">html文件</param>
        /// <param name="outPutFile">转换后文件的地址</param>
        /// <returns></returns>
        public string ExportHtml2PDF(string inputFile)
        {
            CheckFileExists(inputFile);
            string outputFilePath = string.Empty;
            try
            {
                // Instantiate an object PDF class
                Aspose.Pdf.Generator.Pdf pdf = new Aspose.Pdf.Generator.Pdf();
                // add the section to PDF document sections collection
                Aspose.Pdf.Generator.Section section = pdf.Sections.Add();
                // Read the contents of HTML file into StreamReader object
                using (StreamReader reader = System.IO.File.OpenText(inputFile))
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
                    outputFilePath = GetTargetFilePath(inputFile);
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

        /// <summary>
        /// 将邮件生成pdf
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        public string ExportMail2PDF(string inputFile)
        {
            CheckFileExists(inputFile);
            string outputFilePath = string.Empty;
            Microsoft.Office.Interop.Outlook.Application olApp = new Microsoft.Office.Interop.Outlook.Application();
            MailItem mail = olApp.Session.OpenSharedItem(inputFile) as MailItem;
            string htmlBody = mail.HTMLBody;
            // write html to file      
            outputFilePath = GetTargetFilePath(inputFile);
            using (FileStream fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(htmlBody);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }

            string pdfFile = this.ExportHtml2PDF(outputFilePath);
            return pdfFile;
        }

    }
}
