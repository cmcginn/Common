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
  <xsl:param name="transactionType">profileTransAuthCapture</xsl:param>
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
  <xsl:param name="invoiceNumber">182791209712</xsl:param>
  <xsl:param name="order.description">A Large Horse</xsl:param>
  <xsl:param name="purchaseOrderNumber">3244232343</xsl:param>
  <xsl:param name="taxExempt">false</xsl:param>
  <xsl:param name="recurringBilling">false</xsl:param>
  <xsl:param name="cardCode">123</xsl:param>
  <xsl:param name="splitTenderId"></xsl:param>
  <xsl:template match="/"> 
    <xsl:variable name="merchantAuthentication">
      <xsl:call-template name="merchantAuthentication">
        <xsl:with-param name="username" select="$username"></xsl:with-param>
        <xsl:with-param name="password" select="$password"></xsl:with-param>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="tax.charge">
      <xsl:call-template name="charge">
        <xsl:with-param name="amount" select="$tax.amount"></xsl:with-param>
        <xsl:with-param name="description" select="$tax.description"></xsl:with-param>
        <xsl:with-param name="name" select="$tax.name"></xsl:with-param>
      </xsl:call-template>      
    </xsl:variable>
    <xsl:variable name="shipping.charge">
      <xsl:call-template name="charge">
        <xsl:with-param name="amount" select="$shipping.amount"></xsl:with-param>
        <xsl:with-param name="description" select="$shipping.description"></xsl:with-param>
        <xsl:with-param name="name" select="$shipping.name"></xsl:with-param>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="order">
      <xsl:call-template name="order">
        <xsl:with-param name="description" select="$order.description"></xsl:with-param>
        <xsl:with-param name="invoiceNumber" select="$invoiceNumber"></xsl:with-param>
        <xsl:with-param name="purchaseOrderNumber" select="$purchaseOrderNumber"></xsl:with-param>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="sale">
      <xsl:call-template name="sale">
        <xsl:with-param name="amount" select="$amount"></xsl:with-param>
        <xsl:with-param name="tax.charge" select="$tax.charge"></xsl:with-param>
        <xsl:with-param name="shipping.charge" select="$shipping.charge"></xsl:with-param>
        <xsl:with-param name="order" select="$order"></xsl:with-param>
        <xsl:with-param name="customerProfileId" select="$customerProfileId"></xsl:with-param>
        <xsl:with-param name="customerPaymentProfileId" select="$customerPaymentProfileId"></xsl:with-param>
        <xsl:with-param name="customerShippingAddressId" select ="$customerAddressId"></xsl:with-param>
        <xsl:with-param name="taxExempt" select="$taxExempt"></xsl:with-param>
        <xsl:with-param name="recurringBilling" select="$recurringBilling"></xsl:with-param>
        <xsl:with-param name="cardCode" select ="$cardCode"></xsl:with-param>
        <xsl:with-param name="splitTenderId" select="$splitTenderId"></xsl:with-param>
      </xsl:call-template>
    </xsl:variable>
    <xsl:call-template name="profileTransAuthOnly">
      <xsl:with-param name="merchantAuthentication" select="$merchantAuthentication"></xsl:with-param>
      <xsl:with-param name="sale" select="$sale"></xsl:with-param>
    </xsl:call-template>
  </xsl:template>
</xsl:stylesheet>
