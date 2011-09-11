/*
 
This is  a very simplistic and stripped down example of the concepts
described in the accompanying article. This sample is purpose fully
simple so you can use it as a skeleton if you wish - what little
page specific code there is easy to remove and replace with 
real business logic. 

Rick Strahl
West Wind Technologies
http://www.west-wind.com/

Article: 
http://www.west-wind.com/presentations/paypalintegration/paypalintegration.asp

*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

// *** Add the ccProcessing namespace
using Westwind.WebStore;
using Westwind.Tools;


namespace PayPalIntegration
{
	/// <summary>
	/// Summary description for Default.
	/// </summary>
	public partial class OrderForm: Page
	{
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{

			InitializeComponent();

			this.txtCCType.SelectedValue = "PP";
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion	

		
		/// <summary>
		/// Required internal variable that lets us know if
		/// we are returning from PayPal. This flag can be used
		/// to bypass other processing that might be happening
		/// for Credit Cards or whatever.
		/// 
		/// This gets set by HandlePayPalReturn. Not used in this
		/// demo. Refer to article to see how it's used in a more
		/// complex environment.
		/// </summary>
		private bool PayPalReturnRequest = false;

		/// <summary>
		/// Our ever so complicated ORDER DATA. Hey this is supposed to be 
		/// a quick demo and skeleton, so I kept it as simple as possible.
		/// The article shows a more complex environment!
		/// </summary>
		protected decimal OrderAmount = 0.00M;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// *** Check for PayPal responses - 
			// *** if we have no CC selection and PayPal QueryString we need to handle it
			if (Request.QueryString["PayPal"] != null )
					this.HandlePayPalReturn();  

			this.txtCCType.Items[1].Text = "Credit Card (" + App.Configuration.CCProcessor.ToString() + ")";
		

			// *** Display the invoice and get final user input to process card
		
		}

		/// <summary>
		/// Saves the invoice if all goes well!
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
		
			// *** Our simplistic 'order validation'
			try 
			{
				this.OrderAmount = decimal.Parse(this.txtOrderAmount.Text);
			}
			catch 
			{
				this.ShowError("Invalid Order Amount. Get a grip.");
				return;
			}

			
			// *** Dumb ass data simulation - this should only be set once the order is Validated!
			this.Session["OrderAmount"] = this.OrderAmount;


			// *** Handle PayPal Processing seperately from ProcessCard() since it requires
			// *** passing off to another page on the PayPal Site.
			// *** This request will return to this page Cancel or Success querystring
			if (this.txtCCType.SelectedValue == "PP" && !this.PayPalReturnRequest) 
				this.HandlePayPalRedirection(); // this will end this request!
			else 
			{
				// *** CC Processing
				if (!this.ProcessCreditCard()) 
					return;    // failure - display error

				// *** Write the order amount (and enything else you might need into session)
				// *** Normally you'd probably write a PK for the final invoice so you 
				// *** can reload it on the Confirmation.aspx page
				Session["PayPal_OrderAmount"] = this.OrderAmount;  


			}


			// *** TODO:  Save your order etc.
 	


			// *** Show the confirmation page - don't transfer so they can refresh without error
			Response.Redirect("Confirmation.aspx");
		}


		/// <summary>
		/// Redirects the current request to the PayPal site by passing a querystring.
		/// PayPal then should return to this page with ?PayPal=Cancel or ?PayPal=Success
		/// This routine stores all the form vars so they can be restored later
		/// </summary>
		private void HandlePayPalRedirection() 
		{

			// *** Set a flag so we know we redirected
			Session["PayPal_Redirected"] = "True";

			// *** Save any values you might need when you return here
			Session["PayPal_OrderAmount"] = this.OrderAmount;  // already saved above

//			Session["PayPal_HeardFrom"] = this.txtHeardFrom.Text;
//			Session["PayPal_ToolUsed"] = this.txtToolUsed.Text;

			PayPalHelper PayPal = new PayPalHelper();
			PayPal.PayPalBaseUrl = App.Configuration.PayPalUrl;
			PayPal.AccountEmail = App.Configuration.AccountEmail;
			PayPal.Amount = this.OrderAmount;


			//PayPal.LogoUrl = "https://www.west-wind.com/images/wwtoollogo_text.gif";
			
			PayPal.ItemName = "West Wind Web Store Invoice #" + new Guid().GetHashCode().ToString("x");

			// *** Have paypal return back to this URL
			PayPal.SuccessUrl =Request.Url + "?PayPal=Success";
			PayPal.CancelUrl = Request.Url + "?PayPal=Cancel";
	
			Response.Redirect( PayPal.GetSubmitUrl());

			return;
		}
		
		/// <summary>
		/// Handles the return processing from a PayPal Request.
		/// Looks at the PayPal Querystring variable
		/// </summary>
		private void HandlePayPalReturn() 
		{
			string Result = Request.QueryString["PayPal"];
			string Redir = (string) Session["PayPal_Redirected"];
			
			// *** Only do this if we are redirected!
			if (Redir == "True") 
			{
				Session.Remove("PayPal_Redirected");
				
				// *** Set flag so we know not to go TO PayPal again
				this.PayPalReturnRequest = true;
	
				// *** Retrieve saved Page content
				this.txtOrderAmount.Text = ((decimal) Session["PayPal_OrderAmount"]).ToString();
				this.txtCCType.SelectedValue = "PP";

				//				this.txtNotes.Text = (string) Session["PayPal_Notes"];
				//				this.txtHeardFrom.Text = (string) Session["PayPal_HeardFrom"];
				//				this.txtToolUsed.Text = (string) Session["PayPal_ToolUsed"];

				if (Result == "Cancel") 
				{
					this.ShowError("PayPal Payment Processing Failed");
				}
				else 
				{
					// *** We returned successfully - simulate button click to save the order
					this.btnSubmit_Click(this,EventArgs.Empty);
				}
			}


		}
			
		public bool ProcessCreditCard() 
		{
			bool Result = false;

			ccProcessing CC = null;
			ccProcessors CCType = App.Configuration.CCProcessor;

			try 
			{
				// *** Figure out which type to use
				if (CCType == ccProcessors.AccessPoint)
				{
					CC = new ccAccessPoint();
				}
				else if(CCType == ccProcessors.AuthorizeNet) 
				{
					CC = new ccAuthorizeNet();
					CC.MerchantPassword = App.Configuration.CCMerchantPassword;
				}
				else if (CCType == ccProcessors.PayFlowPro) 
				{
					CC = new ccPayFlowPro();
					CC.MerchantPassword = App.Configuration.CCMerchantPassword;
				}
				else if(CCType == ccProcessors.LinkPoint) 
				{
					CC = new ccLinkPoint();
					CC.MerchantPassword = App.Configuration.CCMerchantId;
					CC.CertificatePath = App.Configuration.CCCertificatePath;   // "d:\app\MyCert.pem"
				}


				//CC.UseTestTransaction = true;

				// *** Tell whether we do SALE or Pre-Auth
				CC.ProcessType = App.Configuration.CCProcessType;

				// *** Disable this for testing to get provider response
				CC.UseMod10Check = true;

				CC.Timeout = App.Configuration.CCConnectionTimeout;  // In Seconds
				CC.HttpLink = App.Configuration.CCHostUrl;			 // The host Url format will vary with provider
				CC.MerchantId = App.Configuration.CCMerchantId;

				CC.LogFile = App.Configuration.CCLogFile;
				CC.ReferringUrl = App.Configuration.CCReferingOrderUrl;

				// *** Name can be provided as a single string or as firstname and lastname
				CC.Name = this.txtName.Text;
				//CC.Firstname = Cust.Firstname.TrimEnd();
				//CC.Lastname = Cust.Lastname.TrimEnd();
				// CC.Company = Cust.Company.TrimEnd();

				CC.Address = this.txtAddress.Text;
				CC.State = this.txtState.Text;
				CC.City = this.txtCity.Text;
				CC.Zip = this.txtZip.Text;
				CC.Country = this.txtCountryId.SelectedValue;	// 2 Character Country ID
				CC.Phone = this.txtPhone.Text;
				CC.Email = this.txtEmail.Text;

				CC.OrderAmount = decimal.Parse( this.txtOrderAmount.Text ); 
				
				//CC.TaxAmount = Inv.Tax;					// Optional
				
				CC.CreditCardNumber = this.txtCC.Text;
				CC.CreditCardExpiration = this.txtCCMonth.SelectedValue  + "/" + this.txtCCYear.SelectedValue;

				CC.SecurityCode = this.txtSecurity.Text;
	
				// *** Make this Unique
				//CC.OrderId = Inv.Invno.TrimEnd() + "_" + DateTime.Now.ToString();
				CC.Comment = "Test Order # " + new Guid().GetHashCode().ToString("x");

				Result = CC.ValidateCard();
		
				if (!Result) 
				{
					this.lblErrorMessage.Text = CC.ValidatedMessage +
						"<hr>" + 
						CC.ErrorMessage;
				}
				else
				{
					// *** Should be APPROVED
					this.lblErrorMessage.Text = CC.ValidatedMessage;
				}


				// *** Always write out the raw response
				if (wwUtils.Empty(CC.RawProcessorResult)) 
				{
					this.lblErrorMessage.Text += "<hr>" + "Raw Results:<br>" +
												 CC.RawProcessorResult;
				}
			}
			catch(Exception ex) 
			{
				
				this.lblErrorMessage.Text = "FAILED<hr>" + 
											"Processing Error: " + ex.Message;

				return false;
			}

			return Result;
		}		
		




		void ShowError(string ErrorMessage) 
		{
			this.lblErrorMessage.Text = ErrorMessage + "<p>";
		}

		protected void TextBox8_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		
	}
}
