
namespace ICP.FRM.ServiceInterface.DataObjects
{
    public static class WeeklyAppCache
    {
        static string _WeeklyItem = string.Empty;
        public static string WeeklyItem
        {
            get { return _WeeklyItem; }
            set { _WeeklyItem = value; }
        }


        static string _WeeklyDate = string.Empty;
        public static string WeeklyDate
        {
            get { return _WeeklyDate; }
            set { _WeeklyDate = value; }
        }
    }
}
