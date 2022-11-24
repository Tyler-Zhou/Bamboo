using Chapter0201.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chapter0201.Sender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Press ENTER when the receiver is ready");
            Console.ReadLine();

            //接收程序的地址
            EndpointAddress address = new EndpointAddress($"http://localhost:4000/Order");
            //定义一个通讯的服务
            //在这个例子里，可以使用WS的HTTP绑定
            WSHttpBinding binding=new WSHttpBinding(SecurityMode.None);
            binding.MessageEncoding = WSMessageEncoding.Text;
            //创建通道
            ChannelFactory<IProcessOrder> channel=new ChannelFactory<IProcessOrder>(binding,address);
            //使用通道工厂创建代理
            IProcessOrder proxy = channel.CreateChannel();
            //创建一些消息
            Message msg = null;
            for (int i = 0; i < 10; i++)
            {
                //调用Helper方法创建消息
                //注意使用在IprocessOrder契约里定义的Action
                //the IProcessOrder contract...
                msg = GenerateMessage(i,i);

                //SOAP消息头里添加MessageID
                UniqueId uniqueId=new UniqueId(i.ToString());

                msg.Headers.MessageId = uniqueId;

                Console.WriteLine($"Sending Message # {uniqueId}");

                //SOAP消息头里添加Action
                msg.Headers.Action= "urn:SubmitOrder";

                //发送消息
                proxy.SubmitOrder(msg);
            }
        }
        /// <summary>
        /// 创建Message
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <param name="qty">数量</param>
        /// <returns></returns>
        static Message GenerateMessage(int productID,int qty)
        {
            MemoryStream stream=new MemoryStream();
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream,Encoding.UTF8,false);
            writer.WriteStartElement("Order");
            writer.WriteElementString("ProdID",productID.ToString());
            writer.WriteElementString("Qty", qty.ToString());
            writer.WriteEndElement();

            writer.Flush();
            stream.Position = 0;

            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream,XmlDictionaryReaderQuotas.Max);

            //使用Action 和body 创建消息
            return Message.CreateMessage(MessageVersion.Soap12WSAddressing10,string.Empty,reader);
        }
    }
}
