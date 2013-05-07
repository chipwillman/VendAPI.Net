namespace VendAPITest
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VendAPI;
    using VendAPI.Models;

    [TestClass] 
    public class TestVendApi : BaseVendTest
    {
        [TestMethod]
        public void ShouldBeAbleToRetrieveProductsOrderedByName()
        {
            var products = new VendApi(Url, Username, Password).GetProducts(Product.OrderBy.name, false, true);
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Length > 5);
            for (int i = 0; i < products.Length - 1; i++)
            {
                var p1 = products[i].Name.ToUpper();
                var p2 = products[i + 1].Name.ToUpper();
                Assert.IsTrue(string.Compare(p1, p2) < 0, "string compare failed " + string.Compare(p1, p2) + " " + p1 + " " + p2);
            }
        }

        [TestMethod]
        public void ShouldBeAbleToRetrieveProductsReverseOrderedByName()
        {
            var products = new VendApi(Url, Username, Password).GetProducts(Product.OrderBy.name, true, true);
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Length > 5);
            for (int i = 0; i < products.Length - 1; i++)
            {
                var p1 = products[i].Name.ToUpper();
                var p2 = products[i + 1].Name.ToUpper();
                Assert.IsTrue(string.Compare(p1, p2) > 0, "string compare failed " + string.Compare(p1, p2) + " " + p1 + " " + p2);
            }
        }

        [TestMethod]
        public void ShouldBeAbleToRetriveRegisters()
        {
            var registers = new VendApi(Url, Username, Password).GetRegisters();

            Assert.IsNotNull(registers);
            Assert.IsTrue(registers.Length > 0);
        }

        [TestMethod]
        public void ShouldBeAbleToCreateRegisterSale()
        {
            var register = new VendApi(Url, Username, Password).GetRegisters().First(r => r.Name == "Main Register");
            var products = new VendApi(Url, Username, Password).GetProducts(Product.OrderBy.name, false, true);
            var parma = products.First(p => p.Handle == "HardisParma");
            var beer = products.First(p => p.Handle == "TooheyOld");
                
            var registerSale = new RegisterSale
                                   {
                                       RegisterId = register.Id,
                                       CustomerId = "null",
                                       SaleDate = DateTime.UtcNow.ToString("u"),
                                       UserName = "test",
                                       TotalPrice = parma.Price + (beer.Price * 2),
                                       TotalTax = parma.Tax + (beer.Tax * 2),
                                       TaxName = "GST",
                                       Status = "SAVED",
                                       InvoiceNumber = "102",
                                       InvoiceSequence = 102,
                                       Note = null,
                                       RegisterSaleProducts = new[]
                                                                  {
                                                                      new RegisterSaleProduct
                                                                          {
                                                                              ProductId = parma.Id,
                                                                              Quantity = 1,
                                                                              Price = parma.Price,
                                                                              Tax = parma.Tax,
                                                                              TaxId = parma.TaxId,
                                                                              TaxTotal = parma.Tax
                                                                          },
                                                                      new RegisterSaleProduct
                                                                          {
                                                                              ProductId = beer.Id,
                                                                              Quantity = 2,
                                                                              Price = beer.Price,
                                                                              Tax = beer.Tax,
                                                                              TaxTotal = beer.Tax * 2
                                                                          }

                                                                  }
                                   };

            var savedRegisterSale = new VendApi(Url, Username, Password).SaveRegisterSale(registerSale);

            Assert.IsNotNull(savedRegisterSale);
            Assert.AreNotEqual(Guid.Empty, savedRegisterSale.Id);
        }

        [TestMethod]
        public void ShouldBeAbleToRetrieveExistingRegisterSales()
        {
            var tag = string.Empty;
            var status = new[] { "SAVED" };

            var registerSales = new VendApi(Url, Username, Password).GetRegisterSales(null, tag, status);
            
            Assert.IsNotNull(registerSales);
        }
    }
}
