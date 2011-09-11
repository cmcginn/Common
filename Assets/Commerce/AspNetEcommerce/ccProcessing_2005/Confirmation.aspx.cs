using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Text;


namespace PayPalIntegration
{
	/// <summary>
	/// Confirmation form that shows the user the final invoice information and emails the customer.
	/// For simplicity this page uses ASP style syntax to display most of its information, 
	/// rather than using Web Controls.
	/// </summary>
	public partial class Confirmation : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm frmConfirmation;

		public object InvPk;

		/// <summary>
		/// Our ever so complicated ORDER DATA. Hey this is supposed to be 
		/// a quick demo and skeleton, so I kept it as simple as possible.
		/// The article shows a more complex environment!
		/// </summary>
		protected decimal OrderAmount 
		{
			get 
			{ 
				if (this._Amount  != -1.11M)
					return this._Amount;
				
				try 
				{
					this._Amount = (decimal) Session["OrderAmount"];
				}
				catch { this._Amount = 0.00M; }

				return this._Amount;
			}
		}
		private decimal _Amount = -1.11M;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion			protected System.Web.UI.HtmlControls.HtmlForm frmConfirmation;


		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			// *** TODO: Put your order finalization code here. up invoice or whatever
		    //			 Display a full invoice, Email Confirmations etc. etc.
			// *** We'll simulate with a Session variable in the OrderAmoutn Property above
			//     the ASPX page simply displays the final order amount.
			
			// *** If Order Amount was never set we can't confirm
			if (this.OrderAmount == 0.00M )
				Response.Redirect("OrderForm.aspx");


			// *** Throw away the key
			Session.Remove("OrderAmount");

		}

			}
}