using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// TabControl��Ҷ�ؼ�������
    /// </summary>
    public class TabItemConfigInfo
    {
        /// <summary>
        /// ��ʾ��λ��
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// Ӣ������
        /// </summary>
        public string EName { get; set; }
        /// <summary>
        /// ��Ҷ����ʾ�Ŀؼ��ؼ�
        /// </summary>
        public UserControl Control { get; set; }
        /// <summary>
        /// �ؼ�����ȫ��������
        /// </summary>
        public string ControlFullName { get; set; }
        /// <summary>
        /// �ؼ��Ƿ����
        /// </summary>
        public bool IsControlInit
        {
            get
            {
                return Control != null;
            }
        }
        /// <summary>
        /// �ؼ��Ƿ�ֻ��
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}
