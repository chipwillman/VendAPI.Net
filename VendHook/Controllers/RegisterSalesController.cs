namespace VendHook.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;
    using VendAPI.Extentions;

    using VendAPI.Models;

    public class RegisterSalesController : ApiController
    {
        private static Product[] products;

        private static SortedList<Guid, Guid> printedRegisterSales = new SortedList<Guid, Guid>();

        private static byte[] normalPrintMode = new byte[] { 27, 33, 0 };
        
        private static byte[] headerPrintMode = new byte[] { 27, 33, 57 };

        private static byte[] menuPrintMode = new byte[] { 27, 33, 25  };

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

        public HttpResponseMessage Post([FromBody]VendHookPost value)
        {
            lock (this)
            {
                var registerSale = value.payload.FromJson<RegisterSale>();

                if (registerSale.RegisterSalePayments != null)
                {
                    var lastPayment = registerSale.RegisterSalePayments.Last();
                    if (lastPayment != null && lastPayment.PaymentTypeId == "1")
                    {
                        System.Diagnostics.Debug.WriteLine("Cash sale found. Opening Cash Drawer");
                    }
                }

                var api = new VendAPI.VendApi(
                    ConfigurationManager.AppSettings["Url"],
                    ConfigurationManager.AppSettings["Username"],
                    ConfigurationManager.AppSettings["Password"]);
                var printList = new List<Product>();
                var products = api.GetProducts(Product.OrderBy.id, false, true);

                foreach (var registerSaleProduct in registerSale.RegisterSaleProducts)
                {
                    var product = products.FirstOrDefault(p => p.Id == registerSaleProduct.ProductId);
                    if (product != null && product.Type == "Food" && !this.PrintedRegisterSales.ContainsKey(registerSaleProduct.Id))
                    {
                        printList.Add(product);
                        PrintedRegisterSales.Add(registerSaleProduct.Id, registerSaleProduct.Id);
                    }
                }

                if (printList.Count > 0)
                {
                    var tableName = "Counter Sale";
                    if (!string.IsNullOrEmpty(registerSale.CustomerName))
                    {
                        tableName = registerSale.CustomerName;
                    }
                    else if (!string.IsNullOrEmpty(registerSale.CustomerId) && registerSale.CustomerId != Guid.Empty.ToString())
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

            return this.Request.CreateResponse(HttpStatusCode.OK);
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
                printString.Append("\t" + product.Name + "\n");
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
            PrintThroughDriver.SendStringToPrinter("EPSON TM-T88IV Receipt", printString.ToString());
        }
    }
}
