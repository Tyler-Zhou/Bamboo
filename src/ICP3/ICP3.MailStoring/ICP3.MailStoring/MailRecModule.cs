using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICP3.MailStoring
{
    

    /// <summary>
    /// 邮件记录实体
    /// </summary>
    [Serializable]
    public class MailRecModule //: IEquatable<MailRecModule>
    {
        /// <summary>
        /// 邮件的messageID
        /// </summary>
        public string MsgID
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件副本文件的路径
        /// </summary>
        public string FilePath
        {
            get;
            set;
        }

        /// <summary>
        /// IMessageID
        /// </summary>
        public string IMessageID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }

        //public bool Equals(MailRecModule other)
        //{
        //    // Check whether the compared object is null. 
        //    if (Object.ReferenceEquals(other, null)) return false;

        //    // Check whether the compared object references the same data. 
        //    if (Object.ReferenceEquals(this, other)) return true;

        //    // Check whether the MailRecModules' properties are equal. 
        //    if (MsgID.Equals(other.MsgID))
        //    {
        //        this.IMessageID = other.IMessageID;
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //// If Equals returns true for a pair of objects, 
        //// GetHashCode must return the same value for these objects. 
        //public override int GetHashCode()
        //{
        //    // Get the hash code for the Code field. 
        //    int hashMailRecModuleCode = MsgID.GetHashCode();

        //    // Calculate the hash code for the MailRecModule. 
        //    return hashMailRecModuleCode;
        //}
    }

    class MailRecModuleComparer : IEqualityComparer<MailRecModule>
    {
        // Products are equal if their names and product numbers are equal. 
        public bool Equals(MailRecModule x, MailRecModule y)
        {

            // Check whether the compared objects reference the same data. 
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null. 
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the products' properties are equal. 
            if (x.MsgID == y.MsgID)
            {
                x.IMessageID = y.IMessageID;
                return true;
            }
            else
                return false;
        }

        // If Equals() returns true for a pair of objects, 
        // GetHashCode must return the same value for these objects. 

        public int GetHashCode(MailRecModule mail)
        {
            // Check whether the object is null. 
            if (Object.ReferenceEquals(mail, null)) return 0;


            // Get the hash code for the Code field. 
            int hashMsgID = mail.MsgID.GetHashCode();

            // Calculate the hash code for the product. 
            return hashMsgID;
        }

    }

}