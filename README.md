VendAPI.Net
===========

.Net interface to the VendAPI

### VendHook

The vend hook web site is designed to consume web hooks from the VendHQ online store.

The RegisterSale Controller looks for products that have been purchased of type "food".  When found, it will print the food order to the configured kitchen printer.

The CashRegister Controller looks for new register sale payments and with a payment type of cash send the draw kick command to the printer.  This allows automatic opening of the Cash Drawer in environments where receipts are not used such as the hospitality industry, ie. pubs and bars.

To setup a remote printer, you will need to forward a webhook post from VendHQ to a windows computer where the printer is installed.

To setup the draw kick, you will need to install a google chrome extension that makes a web posts to your POS terminal.

### Setup the web application

* There are two versions of the VendHook included 

.NET 4.5 - [VendAPI.Net\VendHook] Works on Windows Vista/7/8/2008

.NET 4.0 - [VendAPI.Net\Vend.Net2] Works on Windows XP SP3/Windows Embedded

####Pre-requisites
* POS Printer Driver
	+ Use CD From POS vendor
* IIS installed
	+ Windows Vista/7/2008 go to Start->Control Panel->Program and Features->Turn Windows Features On or Off
	+ Windows XP go to Start->Control Panel->Add Remove Programs->Windows Features
	+ Ensure Internet Information Services is checked (The square check contains everything needed)
	+ Click OK
* Know your external IP Address
	+ Google "Whats my ip address"
	+ Your public IP address is *your external IP address*
* Known your computers internal IP address
	+ Start->Run->cmd.exe
	+ at the command prompt type: ipconfig
	+ IPv4 Address .  .  .  .  .  .  . : *your internal address*
* Know your stores external IP address
	+ Start->Run->cmd.exe
	+ at the command prompt type: ping *yourstore*.vendhq.com
	+ Reply from *your external IP address*: bytes=32 time=428ms TTL=44

#### Setup a port Forward
* Select a port to use, for example 55021
* Log on to your internet modem/router
* Click on Advanced -> Port Forwarding
* Set the server address to the computer with the printer's IP address (192.168.1.7)
* Set the External Port Start and External Port End to your port (55021)
* Set the Internal Port Start to 80
* Set the Remote IP Address to ping of *your store*.vendhq.com
* Save 

####Install web app

* Open web.config and set your Url, Username, and Password

.NET 4.5 - [VendAPI.Net\VendHook] directory

.NET 4.0 - [VendAPI.Net\Vend.Net2] directory

* Open IIS Management Studio: Start->Control Panel->Administration Tools->Internet Information Services (IIS) Manager
* Expand the Connections tree: *Your Computer*->Sites->Default Web Site
* Right click on Default Web Site and select "Add Application"
* Enter Alias: VendHook
* Enter Path: Path to 

.NET 4.5 - [VendAPI.Net\VendHook] directory

.NET 4.0 - [VendAPI.Net\Vend.Net2] directory

* Click OK
* Test web site
	+ Open browser and navigate to http://localhost/VendHook/
	+ Verify the browser does not return 404: page not found

#### Setup the web hook
* Log on to your Vend Store
* Click on setup
* Append /api to the end of the address (http://*yourstore*.vendhq.com/setup/api)
* Click Add Webhook
* enter URL:

.NET 4.0 [VendAPI.Net\Vend.Net2]  http://*your external ip address*:55021/VendHook/RegisterSaleHook.aspx

.NET 4.5 - [VendAPI.Net\VendHook] http://*your external ip address*:55021/VendHook/api/RegisterSales

* Select sale.update from the drop down
* Click Save
* Test the Webhook
	+ Click on View for the newly added web hook
	+ Click Test Web Hook
	+ Wait 10 seconds and refresh the page
	+ Verify the status is Successful and Response was 200
	+ If not, hints for Response codes
		*empty*: Verify the port has been forwarded correctly.
		404: The address entered in the web hook is incorrect.
		500: The web application crashed - revert changes and try again

#### Cash Drawer Kick Extension

This Google chrome extension will post a register sale to the address configured.  This can be used with the VendHook website to automatically open the cash register when a cash sale is made without a receipt being printed.

if using the .Net 4.5 version open VendAPI.Net\Extensions\background.js and change jQuery.get("https://localhost/VendHook/OpenCashRegister.aspx") to jQuery.get("https://localhost/VendHook/api/CashRegister")

* Open Google Chrome
* Click on Customise and control Google Chrome->Tools->Extensions
	+ Customise and control Google Chrome is the three horizontal bars button below the close windows X
* Tick the check box "Developer mode"
* Click Load unpacked extension... button
* Select the [VendAPI.Net\Extensions] directory
* Click OK
* Test: Run a sale through your vend store and do not print a receipt.  The cash register should still kick.

### Setup - To run the unit tests:

After downloading the code, copy VendAPITest/app.config.master to VendAPITest/app.config

Open the app.config and set the Url, username and password for your Vend Store

### VendAPI.Net Implemented Features
* Retrieve Products with all non deprecated filters
* Retrieve Registers
* Create Register Sale
* Saving Products

### VendAPI.Net Not yet implemented
* Requesting Stock Consignments
* Saving Stock Consignments

### Usage Retreive

	var vendApi = new VendApi(Url, Username, Password)
	var products = vendApi.GetProducts(Product.OrderBy.name, false, true);
	var registers = vendApi.GetRegisters();


### Usage Save
the products were selected from the available products returned from GetProducts

	var register = registers[0];
	var beer = products.First(p => p.handle == "tshirt");
	var parma = products.First(p => p.handle == "coffee");
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
