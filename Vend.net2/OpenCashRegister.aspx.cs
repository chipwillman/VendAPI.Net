using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OpenCashRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        resultLiteral.Text = "Page_load";
        try
        {
            byte[] data = new byte[] { 27, 112, 0, 25, 250 };
            VendHook.Controllers.PrintThroughDriver.SendStringToPrinter(ConfigurationManager.AppSettings["ReceiptPrinter"], Encoding.ASCII.GetString(data));
            resultLiteral.Text = string.Format("sent string %i %i %i %i %i", data[0], data[1], data[2], data[3], data[4]);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
}