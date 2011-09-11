<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>

  
  <xsl:include href="cim.xslt"></xsl:include>  
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="username">9E4n3PdG</xsl:param>
  <xsl:param name="password">8RMF6ZjL2D4z8d75</xsl:param>
  <xsl:param name="firstName">Billy</xsl:param>
  <xsl:param name="lastName">Bob</xsl:param>
  <xsl:param name="company"></xsl:param>
  <xsl:param name="address">123 Smith Street</xsl:param>
  <xsl:param name="city">Smithville</xsl:param>
  <xsl:param name="state">FL</xsl:param>
  <xsl:param name="zip">32323</xsl:param>
  <xsl:param name="country">US</xsl:param>
  <xsl:param name="phoneNumber">1112223434</xsl:param>
  <xsl:param name="faxNumber"></xsl:param>
  <xsl:param name="cardNumber">4111111111111111</xsl:param>
  <xsl:param name="expirationDate">2015-01</xsl:param>
  <xsl:param name="customerProfileId">4578864</xsl:param>
  <xsl:param name="customerPaymentProfileId">4042836</xsl:param>
  <xsl:param name="customerAddressId">4112732</xsl:param>
  <xsl:param name="itemId">1234</xsl:param>
  <xsl:param name="item.name">A big Horse</xsl:param>
  <xsl:param name="item.description">The Biggest Horse In tHe World</xsl:param>
  <xsl:param name="quantity">1</xsl:param>
  <xsl:param name="unitPrice">100.00</xsl:param>
  <xsl:param name="taxable">false</xsl:param>
  <xsl:param name="amount">100.00</xsl:param>
  <xsl:param name="tax.amount">7.00</xsl:param>
  <xsl:param name="tax.name">Florida Sales Tax</xsl:param>
  <xsl:param name="tax.description">State Sales Tax</xsl:param>
  <xsl:param name="shipping.amount">14.95</xsl:param>
  <xsl:param name="shipping.name">UPS 2 Day Ground</xsl:param>
  <xsl:param name="shipping.description">Weight 2lbs 2nd day shipping</xsl:param>
  <xsl:param name="customerShippingAddressId">4112732</xsl:param>
  <xsl:param name="order.invoiceNumber">182791209712</xsl:param>
  <xsl:param name="order.description">A Large Horse</xsl:param>
  <xsl:param name="order.purchaseOrderNumber">3244232343</xsl:param>
  <xsl:param name="taxExempt">false</xsl:param>
  <xsl:param name="recurringBilling">false</xsl:param>
  <xsl:param name="cardCode">123</xsl:param>
  <xsl:param name="splitTenderId"></xsl:param>
  <xsl:template match="/">
    <!--<xsl:call-template name="createCustomerProfileRequest">
      <xsl:with-param name="username">9E4n3PdG</xsl:with-param>
      <xsl:with-param name="password">8RMF6ZjL2D4z8d75</xsl:with-param>
      <xsl:with-param name="description">A new customer</xsl:with-param>
      <xsl:with-param name="merchantCustomerId">12345</xsl:with-param>
      <xsl:with-param name="email">joe@sdssdsd.com</xsl:with-param>      
    </xsl:call-template>-->
    <!--<xsl:call-template name="createCustomerPaymentProfileRequest">
      <xsl:with-param name="username" select="$username"></xsl:with-param>
      <xsl:with-param name="password" select="$password"></xsl:with-param>
      <xsl:with-param name="firstName" select="$firstName"></xsl:with-param>
      <xsl:with-param name="lastName" select="$lastName"></xsl:with-param>
      <xsl:with-param name="company" select="$company"></xsl:with-param>
      <xsl:with-param name="address" select="$address"></xsl:with-param>
      <xsl:with-param name="city" select="$city"></xsl:with-param>
      <xsl:with-param name="state" select="$state"></xsl:with-param>
      <xsl:with-param name="zip" select="$zip"></xsl:with-param>
      <xsl:with-param name="country" select="$country"></xsl:with-param>
      <xsl:with-param name="phoneNumber" select="$phoneNumber"></xsl:with-param>
      <xsl:with-param name="faxNumber" select="$faxNumber"></xsl:with-param>
      <xsl:with-param name="cardNumber" select="$cardNumber"></xsl:with-param>
      <xsl:with-param name="expirationDate" select="$expirationDate"></xsl:with-param>
      <xsl:with-param name="customerProfileId" select="$customerProfileId"></xsl:with-param>
    </xsl:call-template>-->
    <!--<xsl:call-template name="createCustomerShippingAddressRequest">
      <xsl:with-param name="username" select="$username"></xsl:with-param>
      <xsl:with-param name="password" select="$password"></xsl:with-param>
      <xsl:with-param name="firstName" select="$firstName"></xsl:with-param>
      <xsl:with-param name="lastName" select="$lastName"></xsl:with-param>
      <xsl:with-param name="company" select="$company"></xsl:with-param>
      <xsl:with-param name="address" select="$address"></xsl:with-param>
      <xsl:with-param name="city" select="$city"></xsl:with-param>
      <xsl:with-param name="state" select="$state"></xsl:with-param>
      <xsl:with-param name="zip" select="$zip"></xsl:with-param>
      <xsl:with-param name="country" select="$country"></xsl:with-param>
      <xsl:with-param name="phoneNumber" select="$phoneNumber"></xsl:with-param>
      <xsl:with-param name="faxNumber" select="$faxNumber"></xsl:with-param>
      <xsl:with-param name="customerProfileId" select="$customerProfileId"></xsl:with-param>
    </xsl:call-template>-->
    <xsl:call-template name="profileTransAuthOnly">
      <xsl:with-param name="username" select="$username"></xsl:with-param>
      <xsl:with-param name="password" select="$password"></xsl:with-param>
      <xsl:with-param name="amount" select="$amount"></xsl:with-param>
      <xsl:with-param name="tax.amount" select="$tax.amount"></xsl:with-param>
      <xsl:with-param name="tax.name" select="$tax.name"></xsl:with-param>
      <xsl:with-param name="tax.description" select="$tax.description"></xsl:with-param>
      <xsl:with-param name="shipping.amount" select="$shipping.amount"></xsl:with-param>
      <xsl:with-param name="shipping.name" select="$shipping.name"></xsl:with-param>
      <xsl:with-param name="shipping.description" select="$shipping.description"></xsl:with-param>
      <xsl:with-param name="itemId" select="$itemId"></xsl:with-param>
      <xsl:with-param name="item.name" select="$item.name"></xsl:with-param>
      <xsl:with-param name="item.description" select="$item.description"></xsl:with-param>
      <xsl:with-param name="quantity" select="$quantity"></xsl:with-param>
      <xsl:with-param name="unitPrice" select="$unitPrice"></xsl:with-param>
      <xsl:with-param name="taxable" select="$taxable"></xsl:with-param>
      <xsl:with-param name="customerProfileId" select="$customerProfileId"></xsl:with-param>
      <xsl:with-param name="customerPaymentProfileId" select="$customerPaymentProfileId"></xsl:with-param>
      <xsl:with-param name="customerShippingAddressId" select="$customerShippingAddressId"></xsl:with-param>
      <xsl:with-param name="order.invoiceNumber" select="$order.invoiceNumber"></xsl:with-param>
      <xsl:with-param name="order.description" select="$order.description"></xsl:with-param>
      <xsl:with-param name="order.purchaseOrderNumber" select="$order.purchaseOrderNumber"></xsl:with-param>
      <xsl:with-param name="taxExempt" select="$taxExempt"></xsl:with-param>
      <xsl:with-param name="recurringBilling" select="$recurringBilling"></xsl:with-param>
      <xsl:with-param name="cardCode" select="$cardCode"></xsl:with-param>
      <xsl:with-param name="splitTenderId" select="$splitTenderId"></xsl:with-param>
    </xsl:call-template>
  </xsl:template>
</xsl:stylesheet>
