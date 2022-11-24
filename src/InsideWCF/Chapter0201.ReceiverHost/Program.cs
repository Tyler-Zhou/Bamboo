using Chapter0201.Contracts;
using Chapter0201.Receiver;
using System;
using System.ServiceModel;

namespace Chapter0201.ReceiverHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //定义服务绑定
            WSHttpBinding binding=new WSHttpBinding(SecurityMode.None);
            //使用文本代码
            binding.MessageEncoding = WSMessageEncoding.Text;
            //定义服务地址
            Uri addressURI = new Uri($"http://localhost:4000/Order");
            //使用ProcessOrder实例化服务宿主
            ServiceHost svc = new ServiceHost(typeof(ProcessOrder));
            //给服务添加一个终结点
            //contract，binding，and address
            svc.AddServiceEndpoint(typeof(IProcessOrder), binding, addressURI);
            //打开服务宿主开始侦听
            svc.Open();
            Console.WriteLine($"The receiver is ready");
            Console.ReadLine();

            svc.Close();
        }
    }
}
