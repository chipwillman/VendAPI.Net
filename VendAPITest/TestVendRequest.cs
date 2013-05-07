namespace VendAPITest
{
    using System.Linq;
    
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VendAPI;
    using VendAPI.Extentions;
    using VendAPI.Models;

    /// <summary>
    /// The test vend request.
    /// </summary>
    [TestClass]
    public class TestVendRequest : BaseVendTest
    {
        [TestMethod]
        public void ShouldBeAbleToConnectToTheApiAndRequestRecentSales()
        {
            var vendRequest = new VendRequest(this.Url, this.Username, this.Password);
            var response = vendRequest.Get("/api/products");
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void ShouldBeAbleToConvertProductRequestsIntoProductObjects()
        {
            var vendGetRequest = new VendRequest(this.Url, this.Username, this.Password);
            var getResponse = vendGetRequest.Get("/api/products");
            var products = getResponse.FromJson<ProductList>();
            Assert.IsNotNull(products);
            Assert.IsNotNull(products.Products);
            var tshirt = products.Products.First(p => p.Handle == "tshirt");
            Assert.IsNotNull(tshirt);
            Assert.AreEqual(0m, tshirt.Inventory[0].Count);
            Assert.AreEqual(4.5m, tshirt.PriceBookEntries[0].Price);
        }

        [TestMethod]
        public void ShouldBeAbleToConnectToTheApiAndPostChanges()
        {
            var vendGetRequest = new VendRequest(this.Url, this.Username, this.Password);
            var getResponse = vendGetRequest.Get("/api/products");
            var products = getResponse.FromJson<ProductList>();
            Assert.IsNotNull(products);

            var vendRequest = new VendRequest(this.Url, this.Username, this.Password);
            var tshirt = products.Products.First(p => p.Handle == "tshirt");
            string previousValue = tshirt.Description;
            tshirt.Description = tshirt.Description == "My Company" ? "Someone Else" : "My Company";
            var response = vendRequest.Post("/api/products", tshirt.ToJson());
            Assert.IsNotNull(response);
            var responseProducts = response.FromJson<ProductWrapper>();
            Assert.IsNotNull(responseProducts);
            Assert.IsNotNull(responseProducts.Product);

            var tshirtResponse = vendRequest.Get("/api/products/" + tshirt.Id);
            var tshirtFromWebService = tshirtResponse.FromJson<ProductList>();
            Assert.AreNotEqual(previousValue, tshirtFromWebService.Products[0].Description);
        }
    }
}