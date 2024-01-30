using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// TabControlʵ�����
    /// </summary>
    public class TabControl
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
        public string Cname { get; set; }
        /// <summary>
        /// Ӣ������
        /// </summary>
        public string Ename { get; set; }
        /// <summary>
        /// �ؼ�����
        /// </summary>
        public UserControl Control { get; set; }
        /// <summary>
        /// �ؼ��Ƿ����
        /// </summary>
        public Dictionary<int, bool> Dictionary { get; set; }
    }
}
