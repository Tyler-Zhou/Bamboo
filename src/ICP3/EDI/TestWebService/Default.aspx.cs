using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CityOcean;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Button1.Visible = false;
        Label1.Text = string.Empty;

        ICP3EDIService ser = new ICP3EDIService();
        string[] str = ser.SplitString(@"YUEMA INTERNATIONAL TRADE LIMITEDRM501,DONGLE BLDG,2019#SHENNAN EASTD,SHENZHEN,CHINA*", 35, 5);
        foreach (string s in str)
            Label1.Text += s + "<br/>";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
//        ICP3EDIService ser = new ICP3EDIService();

//        string[] str = ser.ICP3ParseCustomerToCSCL(@"<CustomerDescription>
//  <Name></Name>
//  <Address>YUEMA INTERNATIONAL TRADE LIMITED
//
//
//RM501,DONGLE BLDG,2019#SHENNAN EAST
//RD,SHENZHEN,CHINA*</Address>
//  <Country />
//  <City />
//  <Tel />
//  <Fax />
//  <Contact />
//  <Remark />
//</CustomerDescription>", 35, 15, "");
//        foreach (string s in str)
//            Label1.Text += s+ "<br/>";

    }
}
