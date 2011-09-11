<?xml version="1.0" encoding="UTF-8" ?>

<!-- New document created with EditiX at Sat Sep 10 10:31:50 EDT 2011 -->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:variable name="targetNS">AnetApi/xml/v1/schema/AnetApiSchema.xsd</xsl:variable>
	<xsl:param name="username"/>
	<xsl:param name="password"/>
	<xsl:output method="xml" indent="yes"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template name="merchantAuthentication">
	<xsl:element name="merchantAuthentication" namespace="">
		<xsl:value-of select="$username"/>
	</xsl:element>
	</xsl:template>
</xsl:stylesheet>
