<?xml version="1.0" encoding="utf-8"?><!-- New document created with EditiX at Thu Aug 18 03:45:04 EDT 2011 -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:person="http://schema.org/Person">
	<xsl:output indent="yes" method="xml"/>
	<xsl:variable name="email" select="Order/Person/person:Email"/>
	<xsl:variable name="description" select="Order/Person/person:Description"/>
	<xsl:param name="username"></xsl:param>
	<xsl:param name="password"></xsl:param>
	<xsl:template match="/">
		<xsl:call-template name="createProfile">
			<xsl:with-param name="Person" select="Order/Person"/>			
		</xsl:call-template>
	</xsl:template>
	<xsl:template name="credentials">
		<xsl:element name="merchantAuthentication" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
			<xsl:element name="name" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
				<xsl:value-of select="$username"/>
			</xsl:element>
			<xsl:element name="transactionKey" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
				<xsl:value-of select="$password"/>
			</xsl:element>
		</xsl:element>
	</xsl:template>
	<xsl:template name="createProfile">
		<xsl:param name="Person"/>
		<xsl:element name="createCustomerProfileRequest" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
			<xsl:call-template name="credentials"/>
			<xsl:element name="profile" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
				<xsl:element name="description" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
					<xsl:value-of select="$description"/>
				</xsl:element>
				<xsl:element name="email" namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
					<xsl:value-of select="$email"/>
				</xsl:element>
			</xsl:element>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>
