using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            byte[] data = new byte[] { 27, 112, 0, 25, 250 };
            VendHook.Controllers.PrintThroughDriver.SendStringToPrinter("POS58", Encoding.ASCII.GetString(data));
            //// return new string[] { "value1", "sucess" };
        }
        catch (Exception)
        {
            //// return new string[] { "value1", "fail" };
        }
    }
}
