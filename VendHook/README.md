VendHook
===========

.Net MVC Web Api to utilize the Vend Hooks

This is the default MCV web applications
The only files changes are in /Controllers


### Setup 

Assumes the application will be run on a windows computer with IIS installed located inside the clients private domain.

1. Open application in Visual Studio
2. Right click on VendHook and select properties
3. Click on Web and ensure "Use Visual Studio Development Server is checked and note the specific port (38001)
4. Right click on VendHook and start a debugging session
5. Open a web browser and go to google and search for "what my ip" (240.48.215.213)
6. Google will response with Your public IP address is XXX.XXX.XXX.XXX (192.168.1.3) copy this number to notepad
7. Go to your vend store setup page and add /api to the end http://yourstore.vendhp.com/setup/api
8. Click the button Add Web Hook
9. Enter the URL http://your_public_ip_address:port/api/customer (http://240.48.215.213:38001/api/customer)
10. Ensure the type is set to customer.update
11. Click Save
12. Click on the view link for the customer.update web hook
13. Click the test web hook button
14. Refresh the page to check on the status of the request. 

If the request does not make it you are most like on a private network and we will need to perform a port forward.

If you are on a corporate network, ask your network administrator to add a port forward for port 38001 from yourstore.vendhq.com to your internal network address machine.

If you do not have an IT team and are connected to the internet through an ADSL router perform the following

1. Open a command prompt (Start->Run-> cmd.exe)
2. Type ipconfig and note your default gateway (Default Gateway . . . . . . 192.168.1.1) and private ip address (IPv4 Address. . . . . . 192.168.1.3)
3. Open a web browser and navigate to your Default Gateway (http://192.168.1.1)
4. Log on to your router.  If you don't know the password or never set one, Google your routers model/product number and " default credentials"  You can reset your router back to factory defaults to use the default credentials, but you will need to resetup your ISP connection details.
5. Go to your advanced settings->Port Forward
6. Add a port forward for External Port Start 38001 to External Port End 38001 - Protocol TCP - Internal Port Start 38001 to Internal Port End 38001 to your Private IPv4 address (38001 - 38001 - TCP - 38001 - 38001 - 192.168.1.3)
7. Click Add
8. Google your routers model number and " port forward" for specific instructions for your router
9. Rerun the steps from the above steps 12. - 14.

If it is working you can map the other web hooks
sale.update		http://240.48.215.213:3801/api/registersales	
product.update		http://240.48.215.213:3801/api/product	
inventory.update	http://240.48.215.213:3801/api/inventory


### Implemented Features
* Hook for Customers
* Hook for Products
* Hook for Inventory
* Hook for RegisterSales
	