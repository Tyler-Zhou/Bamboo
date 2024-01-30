using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace ICP.FAM.ServiceComponent.Common
{
    public class DBFactory
    {
        public static DCDataContext DCC
        {
            get
            {
                Database db = DatabaseFactory.CreateDatabase();
                DCDataContext obj = new DCDataContext(db.CreateConnection().ConnectionString);
                obj.DeferredLoadingEnabled = false;
                return obj;
            }
        }

        private static Hashtable m_Hashtable = null;

        public static T Create<T>() where T : class, new()
        {
            if (m_Hashtable == null)
            {
                m_Hashtable = new Hashtable();
            }
            T oT;
            object obj = m_Hashtable[typeof(T).Name];
            if (obj == null)
            {
                oT = new T();
                m_Hashtable.Add(typeof(T).Name, oT);
            }
            else
            {
                oT = (T)obj;
            }
            return oT;
        }
    }
}
