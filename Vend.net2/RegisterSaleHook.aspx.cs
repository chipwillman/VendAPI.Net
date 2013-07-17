using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using VendAPI.Extentions;
using VendAPI.Models;

using VendHook.Controllers;

public partial class RegisterSaleHook : System.Web.UI.Page
{
    private static Product[] products;

    private static SortedList<Guid, Guid> printedRegisterSales = new SortedList<Guid, Guid>();

    private static byte[] normalPrintMode = new byte[] { 27, 33, 0 };

    private static byte[] headerPrintMode = new byte[] { 27, 33, 57 };

    private static byte[] menuPrintMode = new byte[] { 27, 33, 25 };

    private static byte[] underlinePrintModeOn = new byte[] { 27, 45, 2 };

    private static byte[] underlinePrintModeOff = new byte[] { 27, 45, 0 };

    public Product[] Products
    {
        get
        {
            lock (this)
            {
                if (products == null)
                {
                    var api = new VendAPI.VendApi(
                        ConfigurationManager.AppSettings["Url"],
                        ConfigurationManager.AppSettings["Username"],
                        ConfigurationManager.AppSettings["Password"]);
                    products = api.GetProducts(Product.OrderBy.id, false, true);
                }

                return products;
            }
        }
    }

    public SortedList<Guid, Guid> PrintedRegisterSales
    {
        get
        {
            return printedRegisterSales;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Form.AllKeys.Length > 0)
            {
                string data = Request.Form.Get("payload");
                
                var registerSale = data.FromJson<RegisterSale>();
                if (registerSale != null)
                {
                    this.ProcessRegisterSale(registerSale);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    private void ProcessRegisterSale(RegisterSale registerSale)
    {
        lock (this)
        {
            var api = new VendAPI.VendApi(
                ConfigurationManager.AppSettings["Url"],
                ConfigurationManager.AppSettings["Username"],
                ConfigurationManager.AppSettings["Password"]);
            var printList = new List<Product>();

            foreach (var registerSaleProduct in registerSale.RegisterSaleProducts)
            {
                var product = this.Products.FirstOrDefault(p => p.Id == registerSaleProduct.ProductId);
                if (product != null && product.Type == "Food"
                    && !this.PrintedRegisterSales.ContainsKey(registerSaleProduct.Id))
                {
                    printList.Add(product);
                    product.PrintQuantity = registerSaleProduct.Quantity;
                    this.PrintedRegisterSales.Add(registerSaleProduct.Id, registerSaleProduct.Id);
                }
            }

            if (printList.Count > 0)
            {
                var tableName = "Counter Sale";
                if (!string.IsNullOrEmpty(registerSale.CustomerName))
                {
                    tableName = registerSale.CustomerName;
                }
                else if (!string.IsNullOrEmpty(registerSale.CustomerId)
                         && registerSale.CustomerId != Guid.Empty.ToString())
                {
                    var customer = api.GetCustomer(registerSale.CustomerId);
                    if (customer != null && customer.ContactFirstName != null && customer.ContactLastName != null)
                    {
                        tableName = customer.ContactFirstName + " " + customer.ContactLastName;
                    }
                    else
                    {
                        tableName = "WALKIN";
                    }
                }

                this.PrintToKitchen(registerSale, tableName, printList);
            }
        }
    }

    private void PrintToKitchen(RegisterSale registerSale, string tableName, List<Product> printList)
    {
        var printString = new StringBuilder();
        printString.Append(Encoding.ASCII.GetString(headerPrintMode));
        printString.Append(tableName + "\n\n");
        printString.Append(Encoding.ASCII.GetString(normalPrintMode));
        printString.Append("   Created Date: " + registerSale.SaleDate + "\n");

        printString.Append(Encoding.ASCII.GetString(menuPrintMode));
        printString.Append("    Printed Date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\n\n");

        printString.Append(Encoding.ASCII.GetString(underlinePrintModeOn));
        printString.Append("Foor Order\n\n");
        printString.Append(Encoding.ASCII.GetString(underlinePrintModeOff));
   
        printString.Append(Encoding.ASCII.GetString(menuPrintMode));
        foreach (var product in printList)
        {
            if (product.PrintQuantity > 1)
            {
                printString.Append("\t   " + product.Name + "\n");
            }
            else
            {
                printString.Append("\t" + product.PrintQuantity + " X " + product.PrintQuantity + product.Name + "\n");
            }
        }

        printString.Append("\n\n");
        printString.Append(Encoding.ASCII.GetString(normalPrintMode));
        if (!string.IsNullOrEmpty(registerSale.Note))
        {
            printString.Append(Encoding.ASCII.GetString(underlinePrintModeOn));
            printString.Append("Notes:\n\n");
            printString.Append(Encoding.ASCII.GetString(underlinePrintModeOff));
            printString.Append(Encoding.ASCII.GetString(normalPrintMode));
            printString.Append(registerSale.Note + "\n");                
        }

        if (!string.IsNullOrEmpty(registerSale.UserName))
        {
            printString.Append("User: " + registerSale.UserName);
        }

        printString.Append("\n\n");
        var performCut = new byte[] { 29, 86, 66, 240 };
        printString.Append(Encoding.ASCII.GetString(performCut));
        PrintThroughDriver.SendStringToPrinter(ConfigurationManager.AppSettings["KitchenPrinter"], printString.ToString());
    }
}
