VendAPI.Net
===========

.Net interface to the VendAPI


### Setup - To run the unit tests:

After downloading the code, copy VendAPITest/config.app.master to VendAPITest/condig.app

Open the config.app and set the Url, username and password for your Vend Store

### Usage

	var vendApi = new VendApi(Url, Username, Password)
	var products = vendApi.GetProducts(Product.OrderBy.name, false, true);

Examples of posting can be found in the Unit Tests

	