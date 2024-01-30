using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using ICP.FilePreviewServiceLibrary;

namespace ICP.FilePreviewService
{
    public partial class UCPDFControl : UserControl, IPDFOperation
    {
        IFileConvertService fileConvertService = new FileConvertService();
        public UCPDFControl()
        {
            InitializeComponent();
        }

        public new void Load(string filePath)
        {
            string targetFile = ConvertFileToPdf(new string[] { filePath });

            this.axAcroPDF1.LoadFile(targetFile);
        }

        void IPDFOperation.Load(string[] filePaths)
        {
            string targetFile = ConvertFileToPdf(filePaths);
            Load(targetFile);

        }

        public void Print()
        {
                this.axAcroPDF1.Print();
            
        
        }
        public string ConvertFileToPdf(String[] files)
        {
         List<string> pdfFiles = new List<string>();
            foreach (String file in files)
            {
                if (!String.IsNullOrEmpty(file))
                {
                    string pdfFile = string.Empty;
                    String fileExtension = Path.GetExtension(file).ToLower();
                    switch (fileExtension)
                    {
                        case ".doc":
                        case ".docx":
                        case ".docm":
                            pdfFile = fileConvertService.ConvertWord2PDF(file);
                            break;
                        case ".xls":
                        case ".xlsx":
                        case ".xlsm":
                        case ".xltx":
                        case ".xlam":
                        case ".xlsb":
                        case ".xltm":
                            pdfFile = fileConvertService.ConvertExcel2PDF(file);
                            break;
                        case ".pdf":
                        case ".pdfxml":
                            pdfFile = file;
                            break;
                        case ".jpg":
                        case ".png":
                        case ".bmp":
                        case ".tif":
                        case ".jpe":
                        case ".jfif":
                        case ".gif":
                        case ".dib":
                        case ".jpeg":
                            pdfFile = fileConvertService.ConvertImage2PDF(file);
                            break;
                        case ".txt":
                        case ".log":
                        case ".scp":
                        case ".ps1":
                        case ".ps1xml":
                            pdfFile = fileConvertService.ConvertText2PDF(file);
                            break;
                        case ".pptx":
                        case ".ppt":
                            pdfFile = fileConvertService.ConvertPPT2PDF(file);
                            break;
                        default:
                            MessageBox.Show("Don't support the current file type.");
                            break;
                    }
                    pdfFiles.Add(pdfFile);


                }
            }
            string targetFilePath = fileConvertService.MergePDFFiles(pdfFiles);
            return targetFilePath;

        }
    }
}
