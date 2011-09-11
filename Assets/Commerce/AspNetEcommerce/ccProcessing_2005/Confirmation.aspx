<%@ Page language="c#" enableViewState="False" inherits="PayPalIntegration.Confirmation" Trace="False" CodeFile="Confirmation.aspx.cs" %>
<html>
   <head>
      <title>Order Confirmation</title>
      <meta name="progid" content="SharePoint.SmartPage.Document">
      <meta name="SmartPageExpansion" content="full">
      <meta http-equiv="content-type" content="text/html;charset=iso-8859-1">
      <link href="wwWebstore.css" type="text/css" rel="stylesheet">
   </head>
   <body text="black" vlink="gray" alink="red" link="#000055" leftmargin="0" topmargin="0"
      marginwidth="0" marginheight="0">
     <h1>Thank you for your order</h1>
     
     <blockquote>
     <p>
     Thank you for your generous order of <b>$<%= this.OrderAmount %></b>.
     <p>
 We all had a wonderful celebration afterwards and the whole party 
marched down the street to the post office where the entire town of 
Paia   waved 'Bon Voyage!' to your package, on its way to you.
<p>
Please come back and visit us again.
<p>
Pretty please?
</blockquote>
<hr>
<small> Best Buddy Software, &copy; HappiSmileyPeople, Inc &trade;</small>
   </body>
</html>
