using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using Aspose.Pdf.Generator;
using Aspose.Slides.Pptx;
using Aspose.Words.Saving;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemImage = System.Drawing.Image;
using TextSharpDocument = iTextSharp.text.Document;
using HtmlAgilityHtmlDocument = HtmlAgilityPack.HtmlDocument;
using AWDocument = Aspose.Words.Document;
using APImage= Aspose.Pdf.Generator.Image;
using APPageSize = Aspose.Pdf.Generator.PageSize;
using APSection = Aspose.Pdf.Generator.Section;
using AWPdfSaveOptions = Aspose.Words.Saving.PdfSaveOptions;
using ACPdfSaveOptions = Aspose.Cells.PdfSaveOptions;
using APRectangle = Aspose.Pdf.Generator.Rectangle;
using AWSaveFormat = Aspose.Words.SaveFormat;
using ACSaveFormat = Aspose.Cells.SaveFormat;
using ASSaveFormat = Aspose.Slides.Export.SaveFormat;
using APText=Aspose.Pdf.Generator.Text;

namespace ICP.Common.UI.Common
{
    /// <summary>
    /// 文件转换服务实现类
    /// </summary>
    public class FileConvertService : IFileConvertService
    {
        object readOnly = (object)false;   // const 
        object m = Missing.Value; // const 
        String tempDIR = String.Empty;// ICP.TaskCenter.ServiceInterface.DataObjects.AppCache.LocalFilePath;  //file local path

        bool isSave = false;
        /// <summary>
        /// 合并多个PDF文件产生单个PDF文件
        /// </summary>
        /// <param name="fileList">pdfs</param>
        /// <returns>返回产生的PDF文件路径</returns>
        public String MergePDFFiles(List<string> fileList)
        {
            if (fileList.Count == 0)
            {
                return string.Empty;
            }
            fileList = fileList.FindAll(fileFullPath => !string.IsNullOrEmpty(fileFullPath)).ToList();
            string resultFilePath = string.Empty;
            PdfReader reader = null;
            string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), ".pdf");
            resultFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
            TextSharpDocument document = new TextSharpDocument();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(resultFilePath, FileMode.Create));
            document.Open();
            PdfContentByte contentByte = writer.DirectContent;
            PdfImportedPage newPage;
            int rotation = 0;
            string message = string.Empty;
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
                            string filename = string.Empty;
                            try
                            {
                                reader = new PdfReader(file);
                                filename = Path.GetFileName(file);
                                int totalPages = reader.NumberOfPages;
                                for (int i = 1; i <= totalPages; i++)
                                {
                                    document.NewPage();
                                    newPage = writer.GetImportedPage(reader, i);

                                    rotation = reader.GetPageRotation(i);
                                    if (rotation == 90 || rotation == 270)
                                        contentByte.AddTemplate(newPage, 0, -1f, 1f, 0, 0,
                                            reader.GetPageSizeWithRotation(i).Height);
                                    else
                                        contentByte.AddTemplate(newPage, 1f, 0, 0, 1f, 0, 0);
                                }
                            }
                            catch (Exception ex)
                            {
                                message += ("文件\"" + filename + "\"PDF无法识别，请单独打印！" + Environment.NewLine);
                            }
                            //finally
                            //{
                            //    if (reader != null) reader.Close();
                            //}
                        }
                        document.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(message))
                    MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    throw new Exception(string.Format("{0}{1}", "MergePDFFiles Failure", ex.Message));
            }

            return resultFilePath;
        }
        private APRectangle GetImagePageSize(String imageFilePath)
        {
            SystemImage sourceImg = SystemImage.FromFile(imageFilePath);

            float originalAvailDocumentHeight = APPageSize.A4Height;
            float verticalMargin = 72;
            float documentHeight = originalAvailDocumentHeight;
            if (sourceImg.Height > (originalAvailDocumentHeight - (verticalMargin)))
            {
                documentHeight = sourceImg.Height + verticalMargin;
            }
            float originalAvailDocumentWidth = APPageSize.A4Width;
            float horizontalMargin = 72;
            float documentWidth = originalAvailDocumentWidth;
            if (sourceImg.Width > horizontalMargin)
            {
                documentWidth = sourceImg.Width + horizontalMargin;
            }
            sourceImg.Dispose();
            sourceImg = null;
            return new APRectangle(0, 0, documentWidth, documentHeight);
        }
        private Section CreateSection(Pdf document, string imageFilePath)
        {
            Section section = new Section(document);

            // Set margins so image will fit, etc.
            section.PageInfo.Margin.Top = 36;
            section.PageInfo.Margin.Bottom = 36;
            section.PageInfo.Margin.Left = 36;
            section.PageInfo.Margin.Right = 36;

            APRectangle rectangle = GetImagePageSize(imageFilePath);
            section.PageInfo.PageWidth = rectangle.Width;
            section.PageInfo.PageHeight = rectangle.Height;
            document.Sections.Add(section);
            return section;
        }
        /// <summary>
        /// 将image转换问pdf文件
        /// </summary>
        /// <param name="imageFilePath">image文件</param>
        public String ConvertImage2PDF(String imageFilePath)
        {
            IOHelper.CheckFileExists(imageFilePath, true);

            String targetFile = GetTargetFilePath(imageFilePath);
            SystemImage sourceImg = SystemImage.FromFile(imageFilePath);


            Pdf document = new Pdf();
            Section section = CreateSection(document, imageFilePath);

            try
            {
                using (FileStream fs = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
                {
                    //Create an image object
                    APImage image = new APImage();
                    section.Paragraphs.Add(image);
                    string extension = Path.GetExtension(imageFilePath);
                    if (!string.IsNullOrEmpty(extension) && (extension.ToLower().Equals(".tif") || extension.ToLower().Equals(".tiff")))
                    {
                        image.ImageInfo.ImageFileType = ImageFileType.Tiff;
                        // set IsBlackWhite property to true for performance improvement
                        image.ImageInfo.IsBlackWhite = true;
                    }
                    //Set the ImageStream to a MemoryStream object
                    image.ImageInfo.ImageStream = fs;
                    //Set desired image scale
                    image.ImageScale = 1;

                    image.ImageInfo.Alignment = AlignmentType.Center;
                    image.ImageWidth = sourceImg.Width;
                    image.ImageHeight = sourceImg.Height;
                    document.Save(targetFile);
                    document = null;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}{1}", "ConvertImage2PDF Failure", ex.Message));
            }
            finally
            {
                sourceImg = null;
            }
            return targetFile;
        }


        private static String GetTargetFilePath(String inputFile)
        {
            string rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IOHelper.HtmlTempPath);
            return Path.Combine(rootPath, Path.GetFileNameWithoutExtension(inputFile) + ".pdf");

        }

        /// <summary>
        /// 将Text文件生成pdf文件
        /// </summary>
        /// <param name="txtFilePath">text文件路径</param>
        public String ConvertText2PDF(String txtFilePath)
        {
            IOHelper.CheckFileExists(txtFilePath, true);

            String targetFile = GetTargetFilePath(txtFilePath);

            using (StreamReader reader = new StreamReader(txtFilePath, true))
            {

                Pdf pdf = new Pdf();
                Section section = pdf.Sections.Add();
                section.PageInfo.Margin.Left = 110;
                section.PageInfo.Margin.Right = 120;
                string content = reader.ReadToEnd();
                APText text = new APText();
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
        /// <param name="wordFilePath">word文件路径</param>
        public String ConvertWord2PDF(String wordFilePath)
        {
            IOHelper.CheckFileExists(wordFilePath, true);
            //Open an Doc file
            string outputFile = GetTargetFilePath(wordFilePath);
            AWDocument doc = new AWDocument(wordFilePath);
            //doc.GetPageInfo(0).PaperSize = Aspose.Words.PaperSize.
            AWPdfSaveOptions saveOptions = new AWPdfSaveOptions
            {
                SaveFormat = AWSaveFormat.Pdf,
                PrettyFormat = true,
                TextCompression = PdfTextCompression.None,
                PreserveFormFields = true,
                UseCoreFonts = true,
                ZoomBehavior = PdfZoomBehavior.FitBox,
                ZoomFactor = 100
            };

            saveOptions.PrettyFormat = true;
            saveOptions.UseHighQualityRendering = true;
            //Save the doc in PDF format
            doc.Save(outputFile, saveOptions);
            doc = null;


            return outputFile;
        }


        public String ConvertPPT2PDF(String pptFilePath)
        {
            IOHelper.CheckFileExists(pptFilePath, true);
            string outputFile = GetTargetFilePath(pptFilePath);
            //Instantiate a PresentationEx object that represents a PPTX file
            PresentationEx pres = new PresentationEx(pptFilePath);

            //Saving the PPTX presentation to PDF document
            pres.Save(outputFile, ASSaveFormat.Pdf);
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
            Workbook workbook = new Workbook(excelFilePath);

            //Save the document in PDF format
            workbook.Save(outputFile, ACSaveFormat.Pdf);


            workbook = null;

            return outputFile;
        }

        /// <summary>
        /// 将html转换为pdf文件
        /// </summary>
        /// <param name="htmlFilePath">html文件路径</param>
        /// <returns></returns>
        public string ConvertHtml2PDF(string htmlFilePath)
        {
            IOHelper.CheckFileExists(htmlFilePath, true);
            string outputFilePath = string.Empty;
            try
            {

                Pdf pdf = new Pdf();
                // add the section to PDF document sections collection
                Section section = pdf.Sections.Add();


                HtmlAgilityHtmlDocument doc = new HtmlAgilityHtmlDocument();
                doc.LoadHtml(htmlFilePath);
                Encoding encoding = doc.DetectEncoding(htmlFilePath);

                using (StreamReader reader = new StreamReader(htmlFilePath, encoding))
                {
                    //Create text paragraphs containing HTML text
                    APText text2 = new APText(section, reader.ReadToEnd());
                    // enable the property to display HTML contents within their own formatting
                    text2.IsHtmlTagSupported = true;
                    //Add the text paragraphs containing HTML text to the section
                    section.Paragraphs.Add(text2);
                    pdf.IsAutoFontAdjusted = true;


                    outputFilePath = GetTargetFilePath(htmlFilePath);

                    pdf.Save(outputFilePath);
                    doc = null;
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}{1}", "ConvertHtml2PDF Failure", ex.Message));
            }

            return outputFilePath;
        }

        /// <summary>
        /// 将邮件生成pdf
        /// </summary>
        /// <param name="mailFilePath">邮件路径</param>
        /// <returns></returns>
        public string ConvertMail2PDF(string mailFilePath)
        {
            IClientMessageService clientMessageService = ServiceClient.GetClientService<IClientMessageService>();
            return clientMessageService.ConvertMailToPDF(mailFilePath);
        }
        /// <summary>
        /// 获取文件编码格式
        /// </summary>
        /// <param name="srcFile">文件目录</param>
        /// <returns></returns>
        public Encoding GetFileEncoding(string srcFile)
        {

            // *** Use Default of Encoding.Default (Ansi CodePage)

            Encoding enc = Encoding.Default;



            // *** Detect byte order mark if any - otherwise assume default

            byte[] buffer = new byte[5];

            using (FileStream file = new FileStream(srcFile, FileMode.Open))
            {

                file.Read(buffer, 0, 5);

                file.Close();



                if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)

                    enc = Encoding.UTF8;

                else if (buffer[0] == 0xfe && buffer[1] == 0xff)

                    enc = Encoding.Unicode;

                else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)

                    enc = Encoding.UTF32;

                else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)

                    enc = Encoding.UTF7;

            }

            return enc;

        }

    }
}
