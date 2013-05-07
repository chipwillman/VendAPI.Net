namespace VendAPI
{
    using System;
    using System.Linq;
    using System.Text;

    using VendAPI.Extentions;
    using VendAPI.Models;

    public class VendApi
    {
        public VendApi(string url, string username, string password)
        {
            this.Url = url;
            this.Username = username;
            this.Password = password;
        }

        public string Url { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public Product[] GetProducts(Product.OrderBy orderBy, bool reverseOrder, bool? active)
        {
            var result = new Product[0];
            var sb = new StringBuilder();
            if (orderBy != Product.OrderBy.updasted_at)
            {
                sb.Append("order_by=" + orderBy + "&");
            }

            if (reverseOrder)
            {
                sb.Append("order_direction=DESC&");
            }

            if (active.HasValue)
            {
                sb.Append("active=" + (active.Value ? "1" : "0") + "&");
            }

            var paramString = sb.ToString().TrimEnd('&');
            var response = new VendRequest(this.Url, this.Username, this.Password).Get("/api/products?" + paramString);
            if (!string.IsNullOrEmpty(response))
            {
                var productList = response.FromJson<ProductList>();
                result = productList.Products;
            }

            return result;
        }

        public RegisterSale[] GetRegisterSales(Guid? outletId, string tag, string[] status)
        {
            var result = new RegisterSale[0];

            var sb = new StringBuilder();
            if (outletId.HasValue)
            {
                sb.Append("outlet_id=" + outletId + "&");
            }

            if (!string.IsNullOrEmpty(tag))
            {
                sb.Append("tag=" + tag + "&");
            }

            if (status != null && status.Length > 0)
            {
                sb.Append("status=" + status.Join(',') + "&");
            }

            var paramString = sb.ToString().TrimEnd('&');
            var response = new VendRequest(this.Url, this.Username, this.Password).Get("/api/register_sales?" + paramString);

            if (!string.IsNullOrEmpty(response))
            {
                var registerSaleList = response.FromJson<RegisterSaleList>();
                result = registerSaleList.RegisterSales;
            }

            return result;
        }

        public Register[] GetRegisters()
        {
            var result = new Register[0];
            var response = new VendRequest(this.Url, this.Username, this.Password).Get("/api/registers");
            if (!string.IsNullOrEmpty(response))
            {
                var registerSaleList = response.FromJson<RegisterList>();
                result = registerSaleList.Registers;
            }
            return result;
        }

        public RegisterSale SaveRegisterSale(RegisterSale registerSale)
        {
            RegisterSale result = null;

            var vendRequest = new VendRequest(this.Url, this.Username, this.Password);
            var response = vendRequest.Post("/api/register_sales", registerSale.ToJson());
            if (!string.IsNullOrEmpty(response))
            {
                var responseRegisterSale = response.FromJson<RegisterSaleWrapper>();
                result = responseRegisterSale.RegisterSale;
            }

            return result;
        }
    }
}
