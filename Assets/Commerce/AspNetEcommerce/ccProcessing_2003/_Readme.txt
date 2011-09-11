---------------------------------
PayPal Integration Article Sample
---------------------------------

Rick Strahl
West Wind Technologies
http://www.west-wind.com/


Installation
------------
To configure the sample application as a Web Application, run the 
_WebConfiguration.exe file and create the Virtual directory and
set the directory permissions for the anonymous user. You can also
update the VS.NET Solution reference.


What's included
---------------
This is  a very simplistic and stripped down example of the concepts
described in the accompanying article: 

http://www.west-wind.com/presentations/PayPalIntegration/PayPalIntegration.asp

This sample is purposefully simple simple so you can use it as a 
skeleton if you wish - what little page specific code there is 
easy to remove and replace with  real business logic. 

The sample consists of:

OrderForm.aspx				-   a very, very simple form with a price box to process
Confirmation.aspx			-   an optional confirmation page 
PayPalIPNConfirmation.aspx  -  A sample IPN Confirmation page. The page is jsut a skeleton
							   and only sends the IPN message and retrieves the result back.
							   you'll need to implement logic here to handle the appropriate
							   action (ie. emailing or ...). If you test this make sure you
							   configure the PayPal IPN URL for your account. See file for details

PayPalHelper.cs				- Small helper class that provides the logic for creating the 
							  the URL to call the PayPal gateway pages with your account and
							  the order amount preselected. Class also handles the IPN confirmation
							  forwarding.

Configuration.cs			-  a file containing some configurable settings (2 actually
							   Make sure you edit this file and change the Account Email
							   Normally I would use a configuraiton class or config settings
							   for this:
							   http://www.west-wind.com/presentations/configurationclass/configurationclass.asp



wwHTTP.cs					-  An HTTP client helper class that's used for POSTing 
								data to PayPal for IPN confirmations. Note you can
								replace the wwHTTP code with the .NET WebClient class
			                    if you choose

Problems? Questions?
--------------------
Please use our message board to post questions:
http://www.west-wind.com/wwThreads/default.asp?Forum=White+Papers




Rick Strahl
West Wind Technologies
http://www.west-wind.com/
rstrahl@west-wind.com
