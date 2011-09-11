<%@ Page language="c#" enableViewState="False" validateRequest="False" 
	 inherits="PayPalIntegration.OrderForm" CodeFile="orderform.aspx.cs" %>

<HTML>
  <HEAD>
		<title>Order Form</title>
		<meta http-equiv="content-type" content="text/html;charset=iso-8859-1">
		<link href="wwWebstore.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body text="black" vlink="gray" alink="red" link="#000055" leftmargin="0" topmargin="0"
		marginheight="0" marginwidth="0">
		<form id="frmOrderForm" runat="server">
			<h1>
				Simpified Order Processing Demo</h1>
			<br>
			<table align="center" width="500" cellpadding="10" id="Table2" border="1" class="blackborder">
				<tr>
					<td>
						<p>
							Process with:
							<asp:DropDownList id="txtCCType" runat="server" Width="224px">
								<asp:ListItem Value="PP">PayPal</asp:ListItem>
								<asp:ListItem Value="Credit Card">Credit Card</asp:ListItem>
							</asp:DropDownList>
						</p>
						<p>
							<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300">
								<TR>
									<TD width="125">Name:</TD>
									<TD>
										<asp:TextBox id="txtName" runat="server" Width="360px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="125" height="27">Address:</TD>
									<TD height="27">
										<asp:TextBox id="txtAddress" runat="server" Width="360px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="125">City:</TD>
									<TD>
										<asp:TextBox id="txtCity" runat="server" Width="360px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="125" height="19">State/Province:</TD>
									<TD height="19">
										<asp:TextBox id="txtState" runat="server" Width="40px"></asp:TextBox>&nbsp;&nbsp; 
										Zip/Postal Code:
										<asp:TextBox id="txtZip" runat="server" Width="80px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="125" height="19">Country:</TD>
									<TD height="19">
										<asp:dropdownlist id="txtCountryId" runat="Server" width="360px" bindingsourcemember="CountryId" bindingsource="Customer.DataRow">
													<asp:listitem value="US" selected="True">United States</asp:listitem>
													<asp:listitem value="AL">Albania</asp:listitem>
													<asp:listitem value="AR">Argentina</asp:listitem>
													<asp:listitem value="AM">Armenia</asp:listitem>
													<asp:listitem value="AU">Australia</asp:listitem>
													<asp:listitem value="AT">Austria</asp:listitem>
													<asp:listitem value="BE">Belgium</asp:listitem>
													<asp:listitem value="BO">Bolivia</asp:listitem>
													<asp:listitem value="BR">Brazil</asp:listitem>
													<asp:listitem value="BG">Bulgaria</asp:listitem>
													<asp:listitem value="KH">Cambodia</asp:listitem>
													<asp:listitem value="CA">Canada</asp:listitem>
													<asp:listitem value="CL">Chile</asp:listitem>
													<asp:listitem value="CN">China</asp:listitem>
													<asp:listitem value="CO">Colombia</asp:listitem>
													<asp:listitem value="CR">Costa Rica</asp:listitem>
													<asp:listitem value="CU">Cuba</asp:listitem>
													<asp:listitem value="CZ">Czech Republic</asp:listitem>
													<asp:listitem value="DK">Denmark</asp:listitem>
													<asp:listitem value="EC">Ecuador</asp:listitem>
													<asp:listitem value="EG">Egypt</asp:listitem>
													<asp:listitem value="EE">Estonia</asp:listitem>
													<asp:listitem value="ET">Ethiopia</asp:listitem>
													<asp:listitem value="FI">Finland</asp:listitem>
													<asp:listitem value="FR">France</asp:listitem>
													<asp:listitem value="DE">Germany</asp:listitem>
													<asp:listitem value="GR">Greece</asp:listitem>
													<asp:listitem value="GL">Greenland</asp:listitem>
													<asp:listitem value="GD">Grenada</asp:listitem>
													<asp:listitem value="GU">Guam</asp:listitem>
													<asp:listitem value="GT">Guatemala</asp:listitem>
													<asp:listitem value="HT">Haiti</asp:listitem>
													<asp:listitem value="HN">Honduras</asp:listitem>
													<asp:listitem value="HK">Hong Kong</asp:listitem>
													<asp:listitem value="HU">Hungary</asp:listitem>
													<asp:listitem value="IS">Iceland</asp:listitem>
													<asp:listitem value="IN">India</asp:listitem>
													<asp:listitem value="ID">Indonesia</asp:listitem>
													<asp:listitem value="IE">Ireland</asp:listitem>
													<asp:listitem value="IL">Israel</asp:listitem>
													<asp:listitem value="IT">Italy</asp:listitem>
													<asp:listitem value="JM">Jamaica</asp:listitem>
													<asp:listitem value="JP">Japan</asp:listitem>
													<asp:listitem value="LB">Lebanon</asp:listitem>
													<asp:listitem value="LI">Liechtenstein</asp:listitem>
													<asp:listitem value="LU">Luxembourg</asp:listitem>
													<asp:listitem value="MG">Madagascar</asp:listitem>
													<asp:listitem value="MY">Malaysia</asp:listitem>
													<asp:listitem value="MX">Mexico</asp:listitem>
													<asp:listitem value="MC">Monaco</asp:listitem>
													<asp:listitem value="MA">Morocco</asp:listitem>
													<asp:listitem value="NL">Netherlands</asp:listitem>
													<asp:listitem value="NZ">New Zealand</asp:listitem>
													<asp:listitem value="NI">Nicaragua</asp:listitem>
													<asp:listitem value="KP">North Korea</asp:listitem>
													<asp:listitem value="NO">Norway</asp:listitem>
													<asp:listitem value="PK">Pakistan</asp:listitem>
													<asp:listitem value="PA">Panama</asp:listitem>
													<asp:listitem value="PE">Peru</asp:listitem>
													<asp:listitem value="PH">Philippines</asp:listitem>
													<asp:listitem value="PL">Poland</asp:listitem>
													<asp:listitem value="PT">Portugal</asp:listitem>
													<asp:listitem value="PR">Puerto Rico</asp:listitem>
													<asp:listitem value="RO">Romania</asp:listitem>
													<asp:listitem value="RU">Russian Federation</asp:listitem>
													<asp:listitem value="SA">Saudi Arabia</asp:listitem>
													<asp:listitem value="SG">Singapore</asp:listitem>
													<asp:listitem value="ZA">South Africa</asp:listitem>
													<asp:listitem value="KR">South Korea</asp:listitem>
													<asp:listitem value="ES">Spain</asp:listitem>
													<asp:listitem value="SE">Sweden</asp:listitem>
													<asp:listitem value="CH">Switzerland</asp:listitem>
													<asp:listitem value="TW">Taiwan</asp:listitem>
													<asp:listitem value="TH">Thailand</asp:listitem>
													<asp:listitem value="TR">Turkey</asp:listitem>
													<asp:listitem value="UA">Ukraine</asp:listitem>
													<asp:listitem value="AE">United Arab Emirates</asp:listitem>
													<asp:listitem value="GB">United Kingdom</asp:listitem>
													<asp:listitem value="US">United States</asp:listitem>
													<asp:listitem value="VE">Venezuela</asp:listitem>
													<asp:listitem value="VN">Vietnam</asp:listitem>
													<asp:listitem value="VG">Virgin Islands (British)</asp:listitem>
													<asp:listitem value="VI">Virgin Islands (U.S.)</asp:listitem>
													<asp:listitem value="ZZ">Other-Not Shown</asp:listitem>
												
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD width="125">Email:</TD>
									<TD>
										<asp:TextBox id="txtEmail" runat="server" Width="360px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="125">Phone:</TD>
									<TD>
										<asp:TextBox id="txtPhone" runat="server" Width="360px"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="125">Card Number:</TD>
									<TD>
										<asp:TextBox id="txtCC" runat="server" Width="360px" ontextchanged="TextBox8_TextChanged"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD width="125">Expiration:</TD>
									<TD>
										<asp:dropdownlist id="txtCCMonth" runat="server" enableviewstate="False">
											<ASP:LISTITEM value="1">January - 
            01</ASP:LISTITEM>
											<ASP:LISTITEM value="2">February - 
            02</ASP:LISTITEM>
											<ASP:LISTITEM value="3">March - 03</ASP:LISTITEM>
											<ASP:LISTITEM value="4">April - 04</ASP:LISTITEM>
											<ASP:LISTITEM value="5">May - 05</ASP:LISTITEM>
											<ASP:LISTITEM value="6">June - 
            06</ASP:LISTITEM>
											<ASP:LISTITEM value="7">July - 07</ASP:LISTITEM>
											<ASP:LISTITEM value="8">August - 08</ASP:LISTITEM>
											<ASP:LISTITEM value="9">September - 09</ASP:LISTITEM>
											<ASP:LISTITEM value="10">October - 10</ASP:LISTITEM>
											<ASP:LISTITEM value="11">November -11</ASP:LISTITEM>
											<ASP:LISTITEM value="12">December - 12</ASP:LISTITEM>
										</asp:dropdownlist>
										<asp:dropdownlist id="txtCCYear" runat="server" enableviewstate="False">
											<ASP:LISTITEM value="2005">2005</ASP:LISTITEM>
											<ASP:LISTITEM value="2006">2006</ASP:LISTITEM>
											<ASP:LISTITEM value="2007">2007</ASP:LISTITEM>
											<ASP:LISTITEM value="2008">2008</ASP:LISTITEM>
											<ASP:LISTITEM value="2009">2009</ASP:LISTITEM>
											<ASP:LISTITEM value="2010">2010</ASP:LISTITEM>
											<ASP:LISTITEM value="2011">2011</ASP:LISTITEM>
											<ASP:LISTITEM value="2012">2012</ASP:LISTITEM>
										</asp:dropdownlist>&nbsp;&nbsp;&nbsp; Security Code:
										<asp:TextBox id="txtSecurity" runat="server" Width="40px"></asp:TextBox>
									</TD>
								</TR>
							</TABLE>
						</p>
						<P>Enter Order Amount:&nbsp;
							<asp:textbox id="txtOrderAmount" runat="server" width="112px"></asp:textbox>
							<asp:button id="btnSubmit" runat="server" text="Go" onclick="btnSubmit_Click"></asp:button></P>
						<p>
							<asp:label id="lblErrorMessage" runat="server" width="487px" cssclass="ErrorMessage"></asp:label></p>
					</td>
				</tr>
			</table>
		</form>
<P>&nbsp;</P>
<blockquote>
<h3>Configuration:</h3>
<hr>
  In order to configure this demo application you have to modify the 
  Configuration.cs file and fill in your PayPal and CC processor information. 
  The default demo is configured for Authorize.NET and PayPal with payments 
  directed at West Wind Technologies. Feel free to make a donation, preferably 
  large &lt;g&gt;. You can change the values in this file.
  <P>For more information on the classes and configuration requirements and URLs 
  for each please see the following documentation topics:</P>
  <P><A 
  href="http://www.west-wind.com/westwindwebstore/docs/index.htm?page=_13U0XJX0K.htm">Credit 
  Card Processing Classes</A><BR>
  <A 
  href="http://www.west-wind.com/westwindwebstore/docs/index.htm?page=_13Z13TG18.htm">Using 
  the Credit Card Processing Classes</A></P>
  <P>&nbsp;</P>
</blockquote>
	</body>
</HTML>