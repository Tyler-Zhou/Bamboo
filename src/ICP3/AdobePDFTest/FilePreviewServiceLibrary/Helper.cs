using System;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace ICP.FilePreviewServiceLibrary
{
    public class FilePreviewHelper
    {
      public static Binding GetBinding()
      {
          NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
          binding.SendTimeout = binding.OpenTimeout = TimeSpan.FromMinutes(4);
          binding.ReceiveTimeout = TimeSpan.MaxValue;
          return binding;
      }
      public static string GetServiceBaseAddress()
      {
          return "net.pipe://localhost/FilePreview";
      }
     
    }
}
