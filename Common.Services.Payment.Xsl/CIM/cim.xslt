<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes"/>
<!--Types-->
  <!--Merchant Authentication Template-->
  <xsl:template name="merchantAuthentication">
    <xsl:param name="username"/>
    <xsl:param name="password"/>
    <xsl:element name="merchantAuthentication" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:element name="name" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$username"/>
      </xsl:element>
      <xsl:element name="transactionKey" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$password"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--Address Type-->
  <xsl:template name="addressType">
    <xsl:param name="firstName"/>
    <xsl:param name="lastName"/>
    <xsl:param name="company"/>
    <xsl:param name="address"/>
    <xsl:param name="city"/>
    <xsl:param name="state"/>
    <xsl:param name="zip"/>
    <xsl:param name="country"/>
    <xsl:param name="phoneNumber"/>
    <xsl:param name="faxNumber"/>
    <xsl:element name="firstName" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$firstName"/>
    </xsl:element>
    <xsl:element name="lastName" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$lastName"/>
    </xsl:element>
    <xsl:element name="company" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$company"/>
    </xsl:element>
    <xsl:element name="address" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$address"/>
    </xsl:element>
    <xsl:element name="city" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$city"/>
    </xsl:element>
    <xsl:element name="state" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$state"/>
    </xsl:element>
    <xsl:element name="zip" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$zip"/>
    </xsl:element>
    <xsl:element name="country" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$country"/>
    </xsl:element>
    <xsl:element name="phoneNumber" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$phoneNumber"/>
    </xsl:element>
    <xsl:element name="faxNumber" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$faxNumber"/>
    </xsl:element>

  </xsl:template>
  <!--CustomerProfileType-->
  <xsl:template name="customerProfileType">
    <xsl:param name="merchantCustomerId"/>
    <xsl:param name="description"/>
    <xsl:param name="email"/>
    <xsl:element name="merchantCustomerId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$merchantCustomerId"/>
    </xsl:element>
    <xsl:element name="description" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$description"/>
    </xsl:element>
    <xsl:element name="email" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$email"/>
    </xsl:element>
  </xsl:template>
  <!--creditCardType-->
  <xsl:template name="creditCardTypeType">
    <xsl:param name="cardNumber"/>
    <xsl:param name="expirationDate"/>
    <xsl:element name="creditCard" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:element name="cardNumber" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$cardNumber"/>
      </xsl:element>
      <xsl:element name="expirationDate" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$expirationDate"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--lineitems-->
  <xsl:template name="lineItems">
    <xsl:param name="itemId"/>
    <xsl:param name="name"/>
    <xsl:param name="description"/>
    <xsl:param name="quantity"/>
    <xsl:param name="unitPrice"/>
    <xsl:param name="taxable"/>
    <xsl:element name="lineItems" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:element name="itemId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$itemId"/>
      </xsl:element>
      <xsl:element name="name" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$name"/>
      </xsl:element>
      <xsl:element name="description" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$description"/>
      </xsl:element>
      <xsl:element name="quantity" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$quantity"/>
      </xsl:element>
      <xsl:element name="unitPrice" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$unitPrice"/>
      </xsl:element>
      <xsl:element name="taxable" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$taxable"/>
      </xsl:element>
    </xsl:element>
  </xsl:template> 
  <!--charge-->
  <xsl:template name="charge">
    <xsl:param name="amount"/>
    <xsl:param name="name"/>
    <xsl:param name="description"/>   
      <xsl:element name="amount" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$amount"/>
      </xsl:element>
      <xsl:element name="name" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$name"/>
      </xsl:element>
      <xsl:element name="description" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$description"/>
      </xsl:element>   
  </xsl:template>
  <!--order-->
  <xsl:template name="order">
    <xsl:param name="invoiceNumber"/>
    <xsl:param name="description"/>
    <xsl:param name="purchaseOrderNumber"/>
    <xsl:element name="order" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:element name="invoiceNumber" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$invoiceNumber"/>
      </xsl:element>
      <xsl:element name="description" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$description"/>
      </xsl:element>
      <xsl:element name="purchaseOrderNumber" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$purchaseOrderNumber"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--sale-->
  <xsl:template name="sale">
    <xsl:param name="amount"/>
    <xsl:param name="tax.charge"/>
    <xsl:param name="shipping.charge"/>
    <xsl:param name="order"></xsl:param>
    <xsl:param name="customerProfileId"/>
    <xsl:param name="customerPaymentProfileId"/>
    <xsl:param name="customerShippingAddressId"/>
    <xsl:param name="taxExempt"/>
    <xsl:param name="recurringBilling"/>
    <xsl:param name="cardCode"/>
    <xsl:param name="splitTenderId"/>
    <!--prototype needs loop-->
    <xsl:param name="lineItems"></xsl:param>
    <xsl:element name="amount" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$amount"/>
    </xsl:element>
    <!--Tax-->
    <xsl:element name="tax" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:copy-of select="$tax.charge"/>
    </xsl:element>   
    <!--Shipping-->
    <xsl:element name="shipping" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:copy-of select="$shipping.charge"/>
    </xsl:element>
    <!--Line Items 1..*-->
    <xsl:copy-of select="$lineItems"/>
    <!--Customer Profile Id-->
    <xsl:element name="customerProfileId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$customerProfileId"/>
    </xsl:element>
    <!--Customer Payment Profile Id-->
    <xsl:element name="customerPaymentProfileId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$customerPaymentProfileId"/>
    </xsl:element>
    <!--Customer Shipping Address Id-->
    <xsl:element name="customerShippingAddressId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$customerShippingAddressId"/>
    </xsl:element>
    <!--Order-->
    <xsl:copy-of select="$order"/>
    <!--Tax Exempt-->
    <xsl:element name="taxExempt" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$taxExempt"/>
    </xsl:element>
    <!--Recurring Billing-->
    <xsl:element name="recurringBilling" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$recurringBilling"/>
    </xsl:element>
    <!--Card Code-->
    <xsl:element name="cardCode" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$cardCode"/>
    </xsl:element>
    <!--Split Tender Id-->
    <xsl:if test="$splitTenderId">
    <xsl:element name="splitTenderId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:value-of select="$splitTenderId"/>
    </xsl:element>
    </xsl:if>
  </xsl:template>

  <!--Messages-->
  <!--createCustomerProfileRequest-->
  <xsl:template name="createCustomerProfileRequest">
    <xsl:param name="merchantAuthentication"></xsl:param>
    <xsl:param name="merchantCustomerId"/>
    <xsl:param name="description"/>
    <xsl:param name="email"/>
    <xsl:element name="createCustomerProfileRequest" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:copy-of select="$merchantAuthentication"/>
      <xsl:element name="profile" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:call-template name="customerProfileType">
          <xsl:with-param name="merchantCustomerId" select="$merchantCustomerId"></xsl:with-param>
          <xsl:with-param name="description" select="$description"></xsl:with-param>
          <xsl:with-param name="email" select="$email"></xsl:with-param>
        </xsl:call-template>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--createCustomerShippingAddressRequest-->
  <xsl:template name="createCustomerShippingAddressRequest">
    <xsl:param name="merchantAuthentication"/>
    <xsl:param name="shipping.address"/>
    <xsl:param name="customerProfileId"/>
    <xsl:element name="createCustomerShippingAddressRequest" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:copy-of select="$merchantAuthentication"/>
      <xsl:element name="customerProfileId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$customerProfileId"/>
      </xsl:element>
      <xsl:element name="address" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:copy-of select="$shipping.address"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--createCustomerPaymentProfileRequest-->
  <xsl:template name="createCustomerPaymentProfileRequest">
    <xsl:param name="merchantAuthentication"/>
    <xsl:param name="billto.address"></xsl:param>
    <xsl:param name="customerProfileId"></xsl:param>
    <xsl:param name="payment"></xsl:param>
    <xsl:element name="createCustomerPaymentProfileRequest" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:copy-of select="$merchantAuthentication"></xsl:copy-of>
      <xsl:element name="customerProfileId" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:value-of select="$customerProfileId"/>
      </xsl:element>
      <xsl:element name="paymentProfile" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:element name="billTo" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
          <xsl:copy-of select="$billto.address"/>
        </xsl:element>
        <xsl:element name="payment" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
          <xsl:copy-of select="$payment"/>
        </xsl:element>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--profileTransaAuthOnly-->
  <xsl:template name="profileTransAuthOnly">
    <xsl:param name="merchantAuthentication"></xsl:param>
    <xsl:param name="sale"></xsl:param>   
    <xsl:element name="createCustomerProfileTransactionRequest" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:copy-of select="$merchantAuthentication"/>
      <xsl:element name="transaction" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">       
        <xsl:element name="profileTransAuthOnly" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
          <xsl:copy-of select="$sale"/>
        </xsl:element>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  <!--profileTransaAuthCapture-->
  <xsl:template name="profileTransAuthCapture">
    <xsl:param name="merchantAuthentication"></xsl:param>
    <xsl:param name="sale"></xsl:param>
    <xsl:element name="createCustomerProfileTransactionRequest" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
      <xsl:copy-of select="$merchantAuthentication"/>
      <xsl:element name="transaction" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
        <xsl:element name="profileTransAuthCapture" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
          <xsl:copy-of select="$sale"/>
        </xsl:element>
      </xsl:element>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
