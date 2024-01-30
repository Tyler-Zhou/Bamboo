namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    /// <summary>
    /// 控件类
    /// </summary>
   public  class ControlClass
    {
       /// <summary>
       /// 控件序列ID
       /// </summary>
       public int ID { get; set; }

       /// <summary>
       /// 控件名称
       /// </summary>
       public string ControlName { get; set; }

       /// <summary>
       /// 控件值类型
       /// </summary>
       public string ControlValueType { get; set; }

       /// <summary>
       /// 控件值
       /// </summary>
       public string ControlValue { get; set; }
    }
}
