VendHook
===========

.Net MVC Web Api to utilize the Vend Hooks


### Setup 

Assumes the application will be run on a windows computer with IIS installed located inside the clients private domain.

1. Open application in Visual Studio
2. Right click on VendHook and select properties
3. Click on Web and ensure "Use Visual Studio Development Server is checked and note the specific port (38001)
4. Right click on VendHook and start a debugging session
5. Open a web browser and go to google and search for "what my ip"
6. Google will response with Your public IP address is XXX.XXX.XXX.XXX copy this number to notepad
7. Go to your vend store setup page and add /api to the end http://yourstore.vendhp.com/setup/api
8. Click the button Add Web Hook
9. Enter the URL http://your_public_ip_address:38001/api/customer
10. Ensure the type is set to customer.update
11. Click Save
12. Click on the view link for the customer.update web hook
13. Click the test web hook button
14. Refresh the page to check on the status of the request. 

If the request does not make it you are most like on a private network and we will need to perform a port forward.

If you are on a corporate network, ask your network administrator to add a port forward for port 38001 from yourstore.vendhq.com to your internal network address machine.

If you have not IT team and are connected to the internet through an ADSL router perform the following
1. Open a command prompt (Start->Run-> cmd.exe)
2. Type ipconfig and note your default gateway (Default Gateway . . . . . . 192.168.1.1) and private ip address (IPv4 Address. . . . . . 192.168.1.3)
3. Open a web browser and navigate to your Default Gateway (http://192.168.1.1)
4. Log on to your router.  If you don't know the password or never set one, Google your routers model/product number and " default credentials"  You can reset your router back to factory defaults to use the default credentials, but you will need to resetup your ISP connection details.
5. Go to your advanced settings->Port Forward
6. Add a port forward for External Port Start 38001 to External Port End 38001 - Protocol TCP - Internal Port Start 38001 to Internal Port End 38001
7. Click Add
8. Google your routers model number and " port forward" for specific instructions for your router


### Implemented Features
* Hook for Customers
* Hook for Products
* Hook for Inventory
* Hook for RegisterSales
	