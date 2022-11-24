using Chapter0201.Contracts;
using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Xml;

namespace Chapter0201.Receiver
{
    /// <summary>
    /// 实现处理订单服务
    /// </summary>
    public sealed class ProcessOrder :IProcessOrder
    {
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="order"></param>
        public void SubmitOrder(Message order)
        {
            //根据MessageID创建文件名
            string fileName = $"Order{order.Headers.MessageId}.xml";

            //提示消息到达
            Console.Write($"Message ID {order.Headers.MessageId} received");

            //创建一个xmlDictionaryWriter去写文件
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(new FileStream(fileName,FileMode.Create));
            //写消息到文件里
            order.WriteMessage(writer);
            writer.Close();
        }
    }
}
