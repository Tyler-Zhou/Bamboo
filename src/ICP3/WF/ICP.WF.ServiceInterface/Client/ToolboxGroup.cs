


//-----------------------------------------------------------------------
// <copyright file="ToolboxTab.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceInterface.Client
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

	/// <summary>
	/// ��������
	/// </summary>
	public class ToolboxGroup
    {
        #region ���캯��

        public ToolboxGroup()
		{
        }


        #endregion

        #region ��������
        private string ctitle;
        /// <summary>
        /// �����ı���
        /// </summary>
        [XmlAttribute("CTitle")]
        public string CTitle
		{
			get
			{
				return ctitle;
			}
			set
			{
				ctitle = value;
			}
		}

        private string etitle;
        /// <summary>
        /// ��Ӣ�ı���
        /// </summary>
        [XmlAttribute("ETitle")]
        public string ETitle
        {
            get
            {
                return etitle;
            }
            set
            {
                etitle = value;
            }
        }

        private ToolboxGroupType type;
        /// <summary>
        /// �����ı���
        /// </summary>
        [XmlAttribute("Type")]
        public ToolboxGroupType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

       

        private List<ToolboxItem> items = new List<ToolboxItem>();

        /// <summary>
        /// �������б�
        /// </summary>
        [System.Xml.Serialization.XmlElement("Item")]
        public List<ToolboxItem> Items
		{
			get
			{
				return items;
			}
			set
			{
				items = value;
			}
        }

        #endregion
    }

    /// <summary>
    /// ����������
    /// </summary>
    public enum ToolboxGroupType
    {
        /// <summary>
        /// ��
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "Form")]
        Form = 1,

        /// <summary>
        /// ������
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "Workflow")]
        Workflow = 2,
    }
}
