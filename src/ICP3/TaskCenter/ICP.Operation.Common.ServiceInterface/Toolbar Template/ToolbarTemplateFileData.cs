﻿using System;
using System.Collections.Generic;
using System.Linq;
using ICP.MailCenter.ServiceInterface;
using System.IO;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{
    public class ToolbarTemplateFileData : TemplateFileData
    {

        private string fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        private string tempalteFileName = "ToolbarConfig.xml";
        string fileFullPath;

        public override string GetFileFullPath()
        {
            if (!string.IsNullOrEmpty(fileFullPath))
                return fileFullPath;
            if (!Directory.Exists(fileRootDirectory))
                Directory.CreateDirectory(fileRootDirectory);
            fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
            return fileFullPath;
        }
        //读取工具栏配置文件
        public override void ReadSections()
        {
            string templateFilePath = GetFileFullPath();
            XDocument document = XDocument.Load(templateFilePath);
            var sectionElements = document.Element(XName.Get("Config")).Elements().ToList();
            if (sectionElements.Count <= 0)
                return;
            List<string> sectionNames = (from element in sectionElements
                                         select element.Name.LocalName).ToList();

            int count = sectionNames.Count;
            for (int i = 0; i < count; i++)
            {
                Add(sectionNames[i], new ToolbarTemplateSectionData { Name = sectionNames[i], FilePath = fileFullPath });
            }
            document = null;
        }
    }
}