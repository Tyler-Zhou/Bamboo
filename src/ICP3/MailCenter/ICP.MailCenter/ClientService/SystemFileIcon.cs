using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;
using System.IO;

namespace ICP.MailCenter.UI
{
    /// <summary> 
    /// 提供从操作系统获取不同类型的文件图标的方法 
    /// </summary> 
    public partial class SystemFileIcon
    {

        static SystemFileIcon()
        {
            Current = new SystemFileIcon();
        }

        public static SystemFileIcon Current { get; set; }        

        /// <summary> 
        /// 依据文件名读取图标，若指定文件不存在，则默认图标。 
        /// </summary> 
        /// <param name="fileName">指定文件的完整路径</param> 
        /// <returns>返回指定路径的文件图标</returns> 
        public Icon GetIconByFileName(String fileName)
        {
            if (String.IsNullOrEmpty(fileName)) return null;
            if (!File.Exists(fileName))
            {
                return GetIconForFileExtension(Path.GetExtension(fileName), false, false);
            }
            else
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                //Use this to get the small Icon 
                Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
                //The icon is returned in the hIcon member of the shinfo struct 
                System.Drawing.Icon myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
                return myIcon;
            }
        }

        public Icon GetIconForFileExtension(String extension, Boolean largeIcon, Boolean linkOverlay)
        {
            if (string.IsNullOrEmpty(extension)) return null;
            if (!extension.StartsWith(".")) extension = "." + extension;

            SHFILEINFO shellFileInfo = new SHFILEINFO();
            try
            {
                uint flags = Win32.SHGFI_ICON | Win32.SHGFI_USEFILEATTRIBUTES;
                flags |= linkOverlay ? Win32.SHGFI_LINKOVERLAY : 0;
                flags |= largeIcon ? Win32.SHGFI_LARGEICON : Win32.SHGFI_SMALLICON;

                Win32.SHGetFileInfo(extension, Win32.FILE_ATTRIBUTE_NORMAL, ref shellFileInfo,
                    (uint)Marshal.SizeOf(shellFileInfo), flags);
                return (Icon)Icon.FromHandle(shellFileInfo.hIcon).Clone();
            }
            catch
            {
                return null;
            }
            finally
            {
                Win32.DestroyIcon(shellFileInfo.hIcon);
            }
        }

        public Icon GetIconByAPI()
        {
            String[] fileIcon = (Environment.SystemDirectory + "\\" + "shell32.dll,0").Split(new char[] { ',' });
            //调用API方法读取图标 
            int[] phiconLarge = new int[1];
            int[] phiconSmall = new int[1];
            uint count = Win32.ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
            //IntPtr IconHnd = new IntPtr(true ? phiconLarge[0] : phiconSmall[0]); 
            IntPtr IconHnd = new IntPtr(phiconLarge[0]);
            Icon resultIcon = Icon.FromHandle(IconHnd);
            return resultIcon;
        }

        /// <summary> 
        /// 给出文件扩展名（.*），返回相应图标 
        /// 若不以"."开头则返回文件夹的图标。 
        /// </summary> 
        /// <param name="fileType">文件类型</param> 
        /// <param name="isLarge"></param> 
        /// <returns>返回文件类型所对应的系统图标</returns> 
        public Icon GetIconByFileType(String fileType, bool isLarge)
        {
            if (fileType == null || fileType.Equals(String.Empty)) return null;

            RegistryKey regVersion = null;
            String regFileType = null;
            String regIconString = null;
            String systemDirectory = Environment.SystemDirectory + "\\";

            if (fileType[0] == '.')
            {
                //读系统注册表中文件类型信息 
                regVersion = Registry.ClassesRoot.OpenSubKey(fileType, true);
                if (regVersion != null)
                {
                    //regFileType = regVersion.Getvalue("") as String; 
                    regFileType = regVersion.GetValue("") as String;
                    regVersion.Close();
                    regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon", true);
                    if (regVersion != null)
                    {
                        //regIconString = regVersion.Getvalue("") as String; 
                        regIconString = regVersion.GetValue("") as String;
                        regVersion.Close();
                    }
                }
                if (regIconString == null)
                {
                    //没有读取到文件类型注册信息，指定为未知文件类型的图标 
                    regIconString = systemDirectory + "shell32.dll,0";
                }
            }
            else
            {
                //直接指定为文件夹图标 
                regIconString = systemDirectory + "shell32.dll,3";
            }
            String[] fileIcon = regIconString.Split(new char[] { ',' });
            if (fileIcon.Length != 2)
            {
                //系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标 
                fileIcon = new String[] { systemDirectory + "shell32.dll", "2" };
            }
            Icon resultIcon = null;
            try
            {
                //调用API方法读取图标 
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = Win32.ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
                resultIcon = Icon.FromHandle(IconHnd);
            }
            catch { }
            return resultIcon;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public String szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public String szTypeName;
    }

    /// <summary> 
    /// 定义调用的API方法 
    /// </summary> 
    class Win32
    {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LINKOVERLAY = 0x000008000;
        public const uint SHGFI_LARGEICON = 0x000000000;
        public const uint SHGFI_SMALLICON = 0x000000001;
        public const uint SHGFI_OPENICON = 0x000000002;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

        //[DllImport("shell32.dll")]
        //public static extern IntPtr SHGetFileInfo(String pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        [DllImport("shell32.dll")]
        public static extern uint ExtractIconEx(String lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags
            );

        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);
    }
}
