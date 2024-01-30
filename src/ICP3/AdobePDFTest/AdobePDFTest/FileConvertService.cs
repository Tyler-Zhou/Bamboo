using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.pdf;
using System.IO;
using Aspose.Pdf.Generator;
using ICP.FilePreviewServiceLibrary;
namespace ICP.FilePreviewService
{
    /// <summary>
    /// 文件转换服务实现类
    /// </summary>
    public class FileConvertService : IFileConvertService
    {
        object readOnly = (object)false;   // const 
        object m = System.Reflection.Missing.Value; // const 
        String tempDIR = String.Empty;// ICP.TaskCenter.ServiceInterface.DataObjects.AppCache.LocalFilePath;  //file local path

        bool isSave = false;
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

        /// <summary>
        /// 将image转换问pdf文件
        /// </summary>
        /// <param name="imageFilePath">image文件</param>
        public String ConvertImage2PDF(String imageFilePath)
        {
            IOHelper.CheckFileExists(imageFilePath, true);

            String targetFile = GetTargetFilePath(imageFilePath);
            System.Drawing.Image sourceImg = null;
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            try
            {
                using (FileStream stream = new FileStream(targetFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();
                    sourceImg = System.Drawing.Image.FromFile(imageFilePath);
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
            string rootPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, IOHelper.HtmlTempPath);
            return Path.Combine(rootPath, Path.GetFileNameWithoutExtension(inputFile) + ".pdf");

        }

        /// <summary>
        /// 将Text文件生成pdf文件
        /// </summary>
        /// <param name="inputFile">text文件</param>
        public String ConvertText2PDF(String txtFilePath)
        {
            IOHelper.CheckFileExists(txtFilePath, true);

            String targetFile = GetTargetFilePath(txtFilePath);

            using (StreamReader reader = new StreamReader(txtFilePath, true))
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
        public String ConvertWord2PDF(String wordFilePath)
        {
            IOHelper.CheckFileExists(wordFilePath, true);
            //Open an Doc file
            string outputFile = GetTargetFilePath(wordFilePath);
            Aspose.Words.Document doc = new Aspose.Words.Document(wordFilePath);
            //Save the doc in PDF format
            doc.Save(outputFile, Aspose.Words.SaveFormat.Pdf);
            doc = null;

            return outputFile;
        }


        public String ConvertPPT2PDF(String pptFilePath)
        {
            IOHelper.CheckFileExists(pptFilePath, true);
            string outputFile = GetTargetFilePath(pptFilePath);
            //Instantiate a PresentationEx object that represents a PPTX file
            Aspose.Slides.Pptx.PresentationEx pres = new Aspose.Slides.Pptx.PresentationEx(pptFilePath);
            //Saving the PPTX presentation to PDF document
            pres.Save(outputFile, Aspose.Slides.Export.SaveFormat.Pdf);
            pres = null;

            return outputFile;
        }

        /// <summary>
        /// 将excel转换为PDF
        /// </summary>
        /// <param name="excelFilePath">excel文件</param>
        public String ConvertExcel2PDF(String excelFilePath)
        {
            IOHelper.CheckFileExists(excelFilePath, true);
            string outputFile = GetTargetFilePath(excelFilePath);
            //Open an Excel file
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(excelFilePath);
            //Save the document in PDF format
            workbook.Save(outputFile, Aspose.Cells.SaveFormat.Pdf);
            workbook = null;

            return outputFile;
        }

        /// <summary>
        /// 将html转换为pdf文件
        /// </summary>
        /// <param name="inputFile">html文件</param>
        /// <param name="outPutFile">转换后文件的地址</param>
        /// <returns></returns>
        public string ConvertHtml2PDF(string htmlFilePath)
        {
            IOHelper.CheckFileExists(htmlFilePath, true);
            string outputFilePath = string.Empty;
            try
            {
                // Instantiate an object PDF class
                Aspose.Pdf.Generator.Pdf pdf = new Aspose.Pdf.Generator.Pdf();
                // add the section to PDF document sections collection
                Aspose.Pdf.Generator.Section section = pdf.Sections.Add();
                // Read the contents of HTML file into StreamReader object
                using (StreamReader reader = System.IO.File.OpenText(htmlFilePath))
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
                    outputFilePath = GetTargetFilePath(htmlFilePath);
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

    
    }
}
