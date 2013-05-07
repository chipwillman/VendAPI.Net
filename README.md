VendAPI.Net
===========

.Net interface to the VendAPI


### Setup - To run the unit tests:

After downloading the code, copy VendAPITest/config.app.master to VendAPITest/condig.app

Open the config.app and set the Url, username and password for your Vend Store

### Implemented Features
* Retrieve Products with all non deprecated filters
* Retrieve Registers
* Create Register Sale

### Not yet implemented
* Saving Products
* Requesting Stock Consignments
* Saving Stock Consignments

### Usage Retreive

	var vendApi = new VendApi(Url, Username, Password)
	var products = vendApi.GetProducts(Product.OrderBy.name, false, true);
	var registers = vendApi.GetRegisters();


### Usage Save
the products where selected from the available products returned from GetProducts

	var register = registers[0];
	var beer = products.First(p => p.handle == "tshirt");
	var parma = products.First(p => p.handle == "coffee");
        var registerSale = new RegisterSale
                          {
                                       RegisterId = register.Id,
                                       CustomerId = "null",
                                       SaleDate = DateTime.UtcNow.ToString("u"),
                                       UserName = "test",
                                       TotalPrice = parma.Price + (parma.Price * 2),
                                       TotalTax = beer.Tax + (beer.Tax * 2),
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



	