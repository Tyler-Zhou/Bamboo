using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;
using System.IO;

namespace ICP.MailCenter.UI
{
    /// <summary> 
    /// �ṩ�Ӳ���ϵͳ��ȡ��ͬ���͵��ļ�ͼ��ķ��� 
    /// </summary> 
    public partial class SystemFileIcon
    {

        static SystemFileIcon()
        {
            Current = new SystemFileIcon();
        }

        public static SystemFileIcon Current { get; set; }        

        /// <summary> 
        /// �����ļ�����ȡͼ�꣬��ָ���ļ������ڣ���Ĭ��ͼ�ꡣ 
        /// </summary> 
        /// <param name="fileName">ָ���ļ�������·��</param> 
        /// <returns>����ָ��·�����ļ�ͼ��</returns> 
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
            //����API������ȡͼ�� 
            int[] phiconLarge = new int[1];
            int[] phiconSmall = new int[1];
            uint count = Win32.ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
            //IntPtr IconHnd = new IntPtr(true ? phiconLarge[0] : phiconSmall[0]); 
            IntPtr IconHnd = new IntPtr(phiconLarge[0]);
            Icon resultIcon = Icon.FromHandle(IconHnd);
            return resultIcon;
        }

        /// <summary> 
        /// �����ļ���չ����.*����������Ӧͼ�� 
        /// ������"."��ͷ�򷵻��ļ��е�ͼ�ꡣ 
        /// </summary> 
        /// <param name="fileType">�ļ�����</param> 
        /// <param name="isLarge"></param> 
        /// <returns>�����ļ���������Ӧ��ϵͳͼ��</returns> 
        public Icon GetIconByFileType(String fileType, bool isLarge)
        {
            if (fileType == null || fileType.Equals(String.Empty)) return null;

            RegistryKey regVersion = null;
            String regFileType = null;
            String regIconString = null;
            String systemDirectory = Environment.SystemDirectory + "\\";

            if (fileType[0] == '.')
            {
                //��ϵͳע������ļ�������Ϣ 
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
                    //û�ж�ȡ���ļ�����ע����Ϣ��ָ��Ϊδ֪�ļ����͵�ͼ�� 
                    regIconString = systemDirectory + "shell32.dll,0";
                }
            }
            else
            {
                //ֱ��ָ��Ϊ�ļ���ͼ�� 
                regIconString = systemDirectory + "shell32.dll,3";
            }
            String[] fileIcon = regIconString.Split(new char[] { ',' });
            if (fileIcon.Length != 2)
            {
                //ϵͳע�����ע��ı�ͼ����ֱ����ȡ���򷵻ؿ�ִ���ļ���ͨ��ͼ�� 
                fileIcon = new String[] { systemDirectory + "shell32.dll", "2" };
            }
            Icon resultIcon = null;
            try
            {
                //����API������ȡͼ�� 
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
    /// ������õ�API���� 
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
