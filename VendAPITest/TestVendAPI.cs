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
            var products = new VendApi(this.Url, this.Username, this.Password).GetProducts(Product.OrderBy.name, false, true);
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
            var products = new VendApi(this.Url, this.Username, this.Password).GetProducts(Product.OrderBy.name, true, true);
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
            var registers = new VendApi(this.Url, this.Username, this.Password).GetRegisters();

            Assert.IsNotNull(registers);
            Assert.IsTrue(registers.Length > 0);
        }

        [TestMethod]
        public void ShouldBeAbleToCreateRegisterSale()
        {
            var register = new VendApi(this.Url, this.Username, this.Password).GetRegisters().First(r => r.Name == "Main Register");
            var products = new VendApi(this.Url, this.Username, this.Password).GetProducts(Product.OrderBy.name, false, true);
            var product1 = products.First(p => p.Handle == this.Product1);
            var product2 = products.First(p => p.Handle == this.Product2);

            var registerSale = new RegisterSale
                                   {
                                       RegisterId = register.Id,
                                       CustomerId = "null",
                                       SaleDate = DateTime.UtcNow.ToString("u"),
                                       UserName = "test",
                                       TotalPrice = product1.Price + (product2.Price * 2),
                                       TotalTax = product1.Tax + (product2.Tax * 2),
                                       TaxName = "GST",
                                       Status = "SAVED",
                                       InvoiceNumber = "102",
                                       InvoiceSequence = 102,
                                       Note = null,
                                       RegisterSaleProducts =
                                           new[]
                                               {
                                                   new RegisterSaleProduct
                                                       {
                                                           ProductId = product1.Id,
                                                           Quantity = 1,
                                                           Price = product1.Price,
                                                           Tax = product1.Tax,
                                                           TaxId = product1.TaxId,
                                                           TaxTotal = product1.Tax
                                                       },
                                                   new RegisterSaleProduct
                                                       {
                                                           ProductId = product2.Id,
                                                           Quantity = 2,
                                                           Price = product2.Price,
                                                           Tax = product2.Tax,
                                                           TaxTotal = product2.Tax * 2
                                                       }
                                               }
                                   };

            var savedRegisterSale = new VendApi(this.Url, this.Username, this.Password).SaveRegisterSale(registerSale);

            Assert.IsNotNull(savedRegisterSale);
            Assert.AreNotEqual(Guid.Empty, savedRegisterSale.Id);
        }

        [TestMethod]
        public void ShouldBeAbleToRetrieveExistingRegisterSales()
        {
            var tag = string.Empty;
            var status = new[] { "SAVED" };

            var registerSales = new VendApi(this.Url, this.Username, this.Password).GetRegisterSales(null, tag, status);
            
            Assert.IsNotNull(registerSales);
        }

        [TestMethod]
        public void ShouldBeAbleToUpdateProducts()
        {
            var vendApi = new VendApi(this.Url, this.Username, this.Password);
            var products = vendApi.GetProducts(Product.OrderBy.updasted_at, false, null);
            var tshirt = products.First(p => p.Handle == "tshirt");
            string previousValue = tshirt.Description;
            tshirt.Description = tshirt.Description == "My Company" ? "Someone Else" : "My Company";

            var savedProduct = vendApi.SaveProduct(tshirt);
            Assert.IsNotNull(savedProduct);

            Product tshirtFromWebService = vendApi.GetProduct(tshirt.Id);
            Assert.AreNotEqual(previousValue, tshirtFromWebService.Description);
        }
    }
}
