using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Microsoft.Win32;

using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Web;

namespace Westwind.Tools
{

	/// <summary>
	/// wwUtils class which contains a set of common utility classes for 
	/// Formatting strings
	/// Reflection Helpers
	/// Object Serialization
	/// </summary>
	public class wwUtils
	{

		#region String Helper Functions

		/// <summary>
		/// Replaces and  and Quote characters to HTML safe equivalents.
		/// </summary>
		/// <param name="Html">HTML to convert</param>
		/// <returns>Returns an HTML string of the converted text</returns>
		public static string FixHTMLForDisplay( string Html )
		{
			Html = Html.Replace("<","&lt;");
			Html = Html.Replace(">","&gt;");
			Html = Html.Replace("\"","&quot;");
			return Html;
		}

		/// <summary>
		/// Strips HTML tags out of an HTML string and returns just the text.
		/// </summary>
		/// <param name="Html">Html String</param>
		/// <returns></returns>
		public static string StripHtml(string Html)
		{
			Html = Regex.Replace(Html, @"<(.|\n)*?>", string.Empty);
			Html = Html.Replace("\t", " ");
			Html = Html.Replace("\r\n", "");
			Html = Html.Replace("   ", " ");
			return Html.Replace("  ", " ");
		}

		/// <summary>
		/// Fixes a plain text field for display as HTML by replacing carriage returns 
		/// with the appropriate br and p tags for breaks.
		/// </summary>
		/// <param name="String Text">Input string</param>
		/// <returns>Fixed up string</returns>
		public static string DisplayMemo(string HtmlText) 
		{				
			HtmlText = HtmlText.Replace("\r\n","\r");
			HtmlText = HtmlText.Replace("\n","\r");
			HtmlText = HtmlText.Replace("\r\r","<p>");
			HtmlText = HtmlText.Replace("\r","<br>");
			return HtmlText;
		}
		/// <summary>
		/// Method that handles handles display of text by breaking text.
		/// Unlike the non-encoded version it encodes any embedded HTML text
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		public static string DisplayMemoEncoded(string Text) 
		{
			bool PreTag = false;
			if (Text.IndexOf("<pre>") > -1) 
			{
				Text = Text.Replace("<pre>","__pre__");
				Text = Text.Replace("</pre>","__/pre__");
				PreTag = true;
			}

			// *** fix up line breaks into <br><p>
			Text = Westwind.Tools.wwUtils.DisplayMemo( HttpUtility.HtmlEncode(Text) );

			if (PreTag) 
			{
				Text = Text.Replace("__pre__","<pre>");
				Text = Text.Replace("__/pre__","</pre>");
			}

			return Text;
		}

		/// <summary>
		/// Expands links into HTML hyperlinks inside of text or HTML.
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		public static string ExpandUrls(string Text) 
		{
			// *** Expand embedded hyperlinks
			string regex = @"\b(((ftp|https?)://)?[-\w]+(\.\w[-\w]*)+|\w+\@|mailto:|[a-z0-9](?:[-a-z0-9]*[a-z0-9])?\.)+(com\b|edu\b|biz\b|gov\b|in(?:t|fo)\b|mil\b|net\b|org\b|[a-z][a-z]\b)(:\d+)?(/[-a-z0-9_:\@&?=+,.!/~*'%\$]*)*(?<![.,?!])(?!((?!(?:<a )).)*?(?:</a>))(?!((?!(?:<!--)).)*?(?:-->))";
			System.Text.RegularExpressions.RegexOptions options = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.Multiline) 
				| System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(regex, options);
            
			MatchEvaluator MatchEval = new MatchEvaluator( ExpandUrlsRegExEvaluator);
			return Regex.Replace(Text,regex,MatchEval);
		}

		/// <summary>
		/// Internal RegExEvaluator callback
		/// </summary>
		/// <param name="M"></param>
		/// <returns></returns>
		private static string ExpandUrlsRegExEvaluator(System.Text.RegularExpressions.Match M) 
		{
			string Href = M.Groups[0].Value;
			string Text = Href;
        	
			if ( Href.IndexOf("://") < 0 ) 
			{
				if ( Href.StartsWith("www.") )
					Href="http://" + Href;
				else if (Href.StartsWith("ftp") )
					Href="ftp://" + Href;
				else if (Href.IndexOf("@") > -1 )
					Href="mailto://" + Href;
			}
			return "<a href='" + Href + "'>" + Text + "</a>";
		}




		/// <summary>
		/// Extracts a string from between a pair of delimiters. Only the first 
		/// instance is found.
		/// </summary>
		/// <param name="Source">Input String to work on</param>
		/// <param name="StartDelim">Beginning delimiter</param>
		/// <param name="EndDelim">ending delimiter</param>
		/// <param name="CaseInsensitive">Determines whether the search for delimiters is case sensitive</param>
		/// <returns>Extracted string or ""</returns>
		public static string ExtractString(string Source, string BeginDelim, string EndDelim, bool CaseInSensitive, bool AllowMissingEndDelimiter) 
		{
			int At1, At2;

			if (Source == null || Source.Length < 1)
				return "";

			if (CaseInSensitive) 
			{
				At1 = Source.IndexOf(BeginDelim);
				At2 = Source.IndexOf(EndDelim,At1+ BeginDelim.Length );
			}
			else 
			{
				string Lower = Source.ToLower();
				At1 =Lower.IndexOf( BeginDelim.ToLower() );
				At2 = Lower.IndexOf( EndDelim.ToLower(),At1+ BeginDelim.Length);
			}

			if (AllowMissingEndDelimiter && At2 == -1)
				return Source.Substring(At1 + BeginDelim.Length);

			if (At1 > -1 && At2 > 1) 
				return Source.Substring(At1 + BeginDelim.Length,At2-At1 - BeginDelim.Length);

			return "";
		}

		/// <summary>
		/// Extracts a string from between a pair of delimiters. Only the first
		/// instance is found.
		/// <seealso>Class wwUtils</seealso>
		/// </summary>
		/// <param name="Source">
		/// Input String to work on
		/// </param>
		/// <param name="BeginDelim"></param>
		/// <param name="EndDelim">
		/// ending delimiter
		/// </param>
		/// <param name="CaseInSensitive"></param>
		/// <returns>String</returns>
		public static string ExtractString(string Source, string BeginDelim, string EndDelim, bool CaseInSensitive)
		{
			return ExtractString(Source, BeginDelim, EndDelim, false, false);
		}

		/// <summary>
		/// Extracts a string from between a pair of delimiters. Only the first 
		/// instance is found. Search is case insensitive.
		/// </summary>
		/// <param name="Source">
		/// Input String to work on
		/// </param>
		/// <param name="StartDelim">
		/// Beginning delimiter
		/// </param>
		/// <param name="EndDelim">
		/// ending delimiter
		/// </param>
		/// <returns>Extracted string or ""</returns>
		public static string ExtractString(string Source, string BeginDelim, string EndDelim) 
		{
			return wwUtils.ExtractString(Source,BeginDelim,EndDelim,false,false);
		}
	

		/// <summary>
		/// Determines whether a string is empty (null or zero length)
		/// </summary>
		/// <param name="String">Input string</param>
		/// <returns>true or false</returns>
		public static bool Empty(string String) 
		{
			if (String == null || String.Trim().Length == 0)
				return true;

			return false;
		}
		
		/// <summary>
		/// Determines wheter a string is empty (null or zero length)
		/// </summary>
		/// <param name="StringValue">Input string (in object format)</param>
		/// <returns>true or false/returns>
		public static bool Empty(object StringValue) 
		{
			string String = (string) StringValue;
			if ( String == null || String.Trim().Length == 0)
				return true;
			
			return false;
		}

		/// <summary>
		/// Return a string in proper Case format
		/// </summary>
		/// <param name="Input"></param>
		/// <returns></returns>
		public static string ProperCase(string Input) 
		{
			return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Input);
		}

		/// <summary>
		/// Returns an abstract of the provided text by returning up to Length characters
		/// of a text string. If the text is truncated a ... is appended.
		/// </summary>
		/// <param name="Text">Text to abstract</param>
		/// <param name="Length">Number of characters to abstract to</param>
		/// <returns>string</returns>
		public static string TextAbstract(string Text, int Length) 
		{
			if (Text.Length <= Length)
				return Text;

			Text = Text.Substring(0,Length);
            
			Text = Text.Substring(0,Text.LastIndexOf(" "));
			return Text + "..."; 
		}

		/// <summary>
		/// Creates an Abstract from an HTML document. Strips the 
		/// HTML into plain text, then creates an abstract.
		/// </summary>
		/// <param name="Html"></param>
		/// <returns></returns>
		public static string HtmlAbstract(string Html,int Length)
		{
			return TextAbstract(StripHtml(Html),Length);
		}


		/// <summary>
		/// Expands URLs into Href links
		/// </summary>
		/// <param name="Text"></param>
		/// <returns></returns>
		public static string ExpandUrls(string TextToExpand,string Target) 
		{
			if (Target == null)
				Target = "";
			else
				Target = "target=\"" + Target + "\"";

			string pattern = @"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])";
			MatchCollection Matches;
                        
			Matches = Regex.Matches(TextToExpand,pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			foreach (Match m in Matches) 
			{
				string Url = m.ToString();
				
				TextToExpand = TextToExpand.Replace(Url,
					"<a " + Target + "href=\"" + Url + "\">" + Url + "</a>");
			}
			return TextToExpand;			
		}
		#endregion

		#region UrlEncoding and UrlDecoding without System.Web
		/// <summary>
		/// UrlEncodes a string without the requirement for System.Web
		/// </summary>
		/// <param name="String"></param>
		/// <returns></returns>
		public static string UrlEncode(string InputString) 
		{
			StringReader sr = new StringReader( InputString);
			StringBuilder sb = new StringBuilder(  InputString.Length );

			while (true) 
			{
				int Value = sr.Read();
				if (Value == -1)
					break;
				char CharValue = (char) Value;

				if (CharValue >= 'a' && CharValue <= 'z' || 
					CharValue >= 'A' && CharValue <= 'Z' || 
					CharValue >= '0' && CharValue <= '9')
					sb.Append(CharValue);
				else if (CharValue == ' ') 
					sb.Append("+");
				else
					sb.AppendFormat("%{0:X2}",Value);
			}

			return sb.ToString();
		}

		/// <summary>
		/// UrlDecodes a string without requiring System.Web
		/// </summary>
		/// <param name="InputString">String to decode.</param>
		/// <returns>decoded string</returns>
		public static string UrlDecode(string InputString)
		{
			char temp = ' ';
			StringReader sr = new StringReader(InputString);
			StringBuilder sb = new StringBuilder( InputString.Length );

			while (true) 
			{
				int lnVal = sr.Read();
				if (lnVal == -1)
					break;
				char TChar = (char) lnVal;
				if (TChar == '+')
					sb.Append(' ');
				else if(TChar == '%') 
				{
					// *** read the next 2 chars and parse into a char
					temp = (char) Int32.Parse(((char) sr.Read()).ToString() +  ((char) sr.Read()).ToString(),
						System.Globalization.NumberStyles.HexNumber);
					sb.Append(temp);
				}
				else
					sb.Append(TChar);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Retrieves a value by key from a UrlEncoded string.
		/// </summary>
		/// <param name="UrlEncodedString">UrlEncoded String</param>
		/// <param name="Key">Key to retrieve value for</param>
		/// <returns>returns the value or "" if the key is not found or the value is blank</returns>
		public static string GetUrlEncodedKey(string UrlEncodedString, string Key) 
		{
			UrlEncodedString = "&" + UrlEncodedString + "&";

			int Index = UrlEncodedString.ToLower().IndexOf("&" + Key.ToLower() + "=");
			if (Index < 0)
				return "";
	
			int lnStart = Index + 2 + Key.Length;

			int Index2 = UrlEncodedString.IndexOf("&",lnStart);
			if (Index2 < 0)
				return "";

			return UrlDecode(  UrlEncodedString.Substring(lnStart,Index2 - lnStart) );
		}
		#endregion

		#region Reflection Helper Code
		/// <summary>
		/// Binding Flags constant to be reused for all Reflection access methods.
		/// </summary>
		public const BindingFlags MemberAccess = 
			BindingFlags.Public | BindingFlags.NonPublic | 
			BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase ;



		/// <summary>
		/// Retrieve a dynamic 'non-typelib' property
		/// </summary>
		/// <param name="Object">Object to make the call on</param>
		/// <param name="Property">Property to retrieve</param>
		/// <returns>Object - cast to proper type</returns>
		public static object GetProperty(object Object,string Property)
		{
			return Object.GetType().GetProperty(Property,wwUtils.MemberAccess).GetValue(Object,null);
		}

		/// <summary>
		/// Retrieve a dynamic 'non-typelib' field
		/// </summary>
		/// <param name="Object">Object to retreve Field from</param>
		/// <param name="Property">name of the field to retrieve</param>
		/// <returns></returns>
		public static object GetField(object Object,string Property)
		{
			return Object.GetType().GetField(Property,wwUtils.MemberAccess).GetValue(Object);
		}

		/// <summary>
		/// Returns a property or field value using a base object and sub members including . syntax.
		/// For example, you can access: this.oCustomer.oData.Company with (this,"oCustomer.oData.Company")
		/// </summary>
		/// <param name="Parent">Parent object to 'start' parsing from.</param>
		/// <param name="Property">The property to retrieve. Example: 'oBus.oData.Company'</param>
		/// <returns></returns>
		public static object GetPropertyEx(object Parent, string Property) 
		{
			MemberInfo Member = null;

			Type Type = Parent.GetType();

			int lnAt = Property.IndexOf(".");
			if ( lnAt < 0) 
			{
				if (Property == "this" || Property == "me")
					return Parent;

				// *** Get the member
				Member = Type.GetMember(Property,wwUtils.MemberAccess)[0];
				if (Member.MemberType == MemberTypes.Property )
					return ((PropertyInfo) Member).GetValue(Parent,null);
				else
					return ((FieldInfo) Member).GetValue(Parent);
			}

			// *** Walk the . syntax - split into current object (Main) and further parsed objects (Subs)
			string Main = Property.Substring(0,lnAt);
			string Subs = Property.Substring(lnAt+1);

			// *** Retrieve the current property
			Member = Type.GetMember(Main,wwUtils.MemberAccess)[0];

			object Sub;
			if (Member.MemberType == MemberTypes.Property )
			{
				// *** Get its value
				Sub = ((PropertyInfo) Member).GetValue(Parent,null);

			}
			else
			{
				Sub = ( (FieldInfo) Member).GetValue(Parent);

			}

			// *** Recurse further into the sub-properties (Subs)
			return wwUtils.GetPropertyEx(Sub,Subs);
		}

		/// <summary>
		/// Sets the property on an object.
		/// </summary>
		/// <param name="Object">Object to set property on</param>
		/// <param name="Property">Name of the property to set</param>
		/// <param name="Value">value to set it to</param>
		public static void SetProperty(object Object,string Property,object Value)
		{
			Object.GetType().GetProperty(Property,wwUtils.MemberAccess).SetValue(Object,Value,null);
		}

		/// <summary>
		/// Sets the field on an object.
		/// </summary>
		/// <param name="Object">Object to set property on</param>
		/// <param name="Property">Name of the field to set</param>
		/// <param name="Value">value to set it to</param>
		public static void SetField(object Object,string Property,object Value)
		{
			Object.GetType().GetField(Property,wwUtils.MemberAccess).SetValue(Object,Value);
		}

		/// <summary>
		/// Sets the value of a field or property via Reflection. This method alws 
		/// for using '.' syntax to specify objects multiple levels down.
		/// 
		/// wwUtils.SetPropertyEx(this,"Invoice.LineItemsCount",10)
		/// 
		/// which would be equivalent of:
		/// 
		/// this.Invoice.LineItemsCount = 10;
		/// </summary>
		/// <param name="Object Parent">
		/// Object to set the property on.
		/// </param>
		/// <param name="String Property">
		/// Property to set. Can be an object hierarchy with . syntax.
		/// </param>
		/// <param name="Object Value">
		/// Value to set the property to
		/// </param>
		public static object SetPropertyEx(object Parent, string Property,object Value) 
		{
			Type Type = Parent.GetType();
			MemberInfo Member = null;

			// *** no more .s - we got our final object
			int lnAt = Property.IndexOf(".");
			if ( lnAt < 0) 
			{
				Member = Type.GetMember(Property,wwUtils.MemberAccess)[0];
				if ( Member.MemberType == MemberTypes.Property ) 
				{

					((PropertyInfo) Member).SetValue(Parent,Value,null);
					return null;
				}
				else 
				{
					((FieldInfo) Member).SetValue(Parent,Value);
					return null;				   
				}
			}	

			// *** Walk the . syntax
			string Main = Property.Substring(0,lnAt);
			string Subs = Property.Substring(lnAt+1);
			Member = Type.GetMember(Main,wwUtils.MemberAccess)[0];

			object Sub;
			if (Member.MemberType == MemberTypes.Property)
				Sub = ((PropertyInfo) Member).GetValue(Parent,null);
			else
				Sub = ((FieldInfo) Member).GetValue(Parent);

			// *** Recurse until we get the lowest ref
			SetPropertyEx(Sub,Subs,Value);
			return null;
		}

		/// <summary>
		/// Wrapper method to call a 'dynamic' (non-typelib) method
		/// on a COM object
		/// </summary>
		/// <param name="Params"></param>
		/// 1st - Method name, 2nd - 1st parameter, 3rd - 2nd parm etc.
		/// <returns></returns>
		public static object CallMethod(object Object,string Method, params object[] Params)
		{
			return Object.GetType().InvokeMember(Method,wwUtils.MemberAccess | BindingFlags.InvokeMethod,null,Object,Params);
			//return Object.GetType().GetMethod(Method,wwUtils.MemberAccess | BindingFlags.InvokeMethod).Invoke(Object,Params);
		}

		/// <summary>
		/// Creates an instance from a type by calling the parameterless constructor.
		/// 
		/// Note this will not work with COM objects - continue to use the Activator.CreateInstance
		/// for COM objects.
		/// <seealso>Class wwUtils</seealso>
		/// </summary>
		/// <param name="TypeToCreate">
		/// The type from which to create an instance.
		/// </param>
		/// <returns>object</returns>
		public object CreateInstanceFromType( Type TypeToCreate ) 
		{
			Type[]  Parms = Type.EmptyTypes;
			return TypeToCreate.GetConstructor(Parms).Invoke(null);
		}

		/// <summary>
		/// Converts a type to string if possible. This method supports an optional culture generically on any value.
		/// It calls the ToString() method on common types and uses a type converter on all other objects
		/// if available
		/// </summary>
		/// <param name="RawValue">The Value or Object to convert to a string</param>
		/// <param name="Culture">Culture for numeric and DateTime values</param>
		/// <returns>string</returns>
		public static string TypedValueToString(object RawValue,CultureInfo Culture) 
		{
			Type ValueType = RawValue.GetType();
			string Return = null;

			if (ValueType == typeof(string) ) 
				Return = RawValue.ToString();
			else if ( ValueType == typeof(int) || ValueType == typeof(decimal) || 
				ValueType == typeof(double) || ValueType == typeof(float))
				Return = string.Format(Culture.NumberFormat,"{0}",RawValue);
			else if(ValueType == typeof(DateTime))
				Return =  string.Format(Culture.DateTimeFormat,"{0}",RawValue);
			else if(ValueType == typeof(bool))
				Return = RawValue.ToString();
			else if(ValueType == typeof(byte))
				Return = RawValue.ToString();
			else if(ValueType.IsEnum)
				Return = RawValue.ToString();
			else 
			{
				// Any type that supports a type converter
				System.ComponentModel.TypeConverter converter = 
					System.ComponentModel.TypeDescriptor.GetConverter( ValueType );
				if (converter != null && converter.CanConvertTo(typeof(string)) )
					Return = converter.ConvertToString(null,Culture,RawValue );
				else
					// Last resort - just call ToString() on unknown type
					Return = RawValue.ToString();

			}
		
			return Return;
		}

		/// <summary>
		/// Converts a type to string if possible. This method uses the current culture for numeric and DateTime values.
		/// It calls the ToString() method on common types and uses a type converter on all other objects
		/// if available.
		/// </summary>
		/// <param name="RawValue">The Value or Object to convert to a string</param>
		/// <param name="Culture">Culture for numeric and DateTime values</param>
		/// <returns>string</returns>
		public static string TypedValueToString(object RawValue) 
		{
			return TypedValueToString(RawValue,CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Turns a string into a typed value. Useful for auto-conversion routines
		/// like form variable or XML parsers.
		/// <seealso>Class wwUtils</seealso>
		/// </summary>
		/// <param name="SourceString">
		/// The string to convert from
		/// </param>
		/// <param name="TargetType">
		/// The type to convert to
		/// </param>
		/// <param name="Culture">
		/// Culture used for numeric and datetime values.
		/// </param>
		/// <returns>object. Throws exception if it cannot be converted.</returns>
		public static object StringToTypedValue(string SourceString, Type TargetType, CultureInfo Culture ) 
		{
			object Result = null;

			if ( TargetType == typeof(string) )
				Result = SourceString;
			else if (TargetType == typeof(int))  
				Result = int.Parse( SourceString, NumberStyles.Integer, Culture.NumberFormat );			
			else if (TargetType  == typeof(byte) )  
				Result = Convert.ToByte(SourceString);				
			else if (TargetType  == typeof(decimal))  
				Result = Decimal.Parse(SourceString,NumberStyles.Any, Culture.NumberFormat);
			else if (TargetType  == typeof(double))    
				Result = Double.Parse( SourceString,NumberStyles.Any, Culture.NumberFormat);				
			else if (TargetType == typeof(bool)) 
			{
				if (SourceString.ToLower() == "true" || SourceString.ToLower() == "on" || SourceString == "1")
					Result = true;
				else
					Result = false;
			}
			else if (TargetType == typeof(DateTime))  
				Result = Convert.ToDateTime(SourceString,Culture.DateTimeFormat);	
			else if (TargetType.IsEnum)
				Result = Enum.Parse(TargetType,SourceString);
			else   
			{
				System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(TargetType);
				if (converter != null && converter.CanConvertFrom(typeof(string)) )
					Result = converter.ConvertFromString( null, Culture, SourceString );
				else  
				{
					System.Diagnostics.Debug.Assert(false,"Type Conversion not handled in StringToTypedValue for " + 
						TargetType.Name + " " + SourceString );
					throw(new ApplicationException("Type Conversion not handled in StringToTypedValue"));
				}
			}

			return Result;
		}

		/// <summary>
		/// Turns a string into a typed value. Useful for auto-conversion routines
		/// like form variable or XML parsers.
		/// </summary>
		/// <param name="SourceString">The input string to convert</param>
		/// <param name="TargetType">The Type to convert it to</param>
		/// <returns>object reference. Throws Exception if type can not be converted</returns>
		public static object StringToTypedValue(string SourceString, Type TargetType ) 
		{
			return StringToTypedValue(SourceString,TargetType,CultureInfo.CurrentCulture);
		}

		#endregion

		#region COM Reflection Helper Code
		
		/// <summary>
		/// Retrieve a dynamic 'non-typelib' property
		/// </summary>
		/// <param name="Object">Object to make the call on</param>
		/// <param name="Property">Property to retrieve</param>
		/// <returns></returns>
		public static object GetPropertyCom(object Object,string Property)
		{
			return Object.GetType().InvokeMember(Property,wwUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField,null,
				Object,null);
		}

		
		/// <summary>
		/// Returns a property or field value using a base object and sub members including . syntax.
		/// For example, you can access: this.oCustomer.oData.Company with (this,"oCustomer.oData.Company")
		/// </summary>
		/// <param name="Parent">Parent object to 'start' parsing from.</param>
		/// <param name="Property">The property to retrieve. Example: 'oBus.oData.Company'</param>
		/// <returns></returns>
		public static object GetPropertyExCom(object Parent, string Property) 
		{

			Type Type = Parent.GetType();

			int lnAt = Property.IndexOf(".");
			if ( lnAt < 0) 
			{
				if (Property == "this" || Property == "me")
					return Parent;

				// *** Get the member
				return Parent.GetType().InvokeMember(Property,wwUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField,null,
					Parent,null);
			}

			// *** Walk the . syntax - split into current object (Main) and further parsed objects (Subs)
			string Main = Property.Substring(0,lnAt);
			string Subs = Property.Substring(lnAt+1);

			object Sub = Parent.GetType().InvokeMember(Main,wwUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField,null,
				Parent,null);

			// *** Recurse further into the sub-properties (Subs)
			return wwUtils.GetPropertyExCom(Sub,Subs);
		}

		/// <summary>
		/// Sets the property on an object.
		/// </summary>
		/// <param name="Object">Object to set property on</param>
		/// <param name="Property">Name of the property to set</param>
		/// <param name="Value">value to set it to</param>
		public static void SetPropertyCom(object Object,string Property,object Value)
		{
			Object.GetType().InvokeMember(Property,wwUtils.MemberAccess | BindingFlags.SetProperty | BindingFlags.SetField,null,Object,new object[1] { Value } );
			//GetProperty(Property,wwUtils.MemberAccess).SetValue(Object,Value,null);
		}

		/// <summary>
		/// Sets the value of a field or property via Reflection. This method alws 
		/// for using '.' syntax to specify objects multiple levels down.
		/// 
		/// wwUtils.SetPropertyEx(this,"Invoice.LineItemsCount",10)
		/// 
		/// which would be equivalent of:
		/// 
		/// this.Invoice.LineItemsCount = 10;
		/// </summary>
		/// <param name="Object Parent">
		/// Object to set the property on.
		/// </param>
		/// <param name="String Property">
		/// Property to set. Can be an object hierarchy with . syntax.
		/// </param>
		/// <param name="Object Value">
		/// Value to set the property to
		/// </param>
		public static object SetPropertyExCom(object Parent, string Property,object Value) 
		{
			Type Type = Parent.GetType();

			int lnAt = Property.IndexOf(".");
			if ( lnAt < 0) 
			{
				// *** Set the member
				Parent.GetType().InvokeMember(Property,wwUtils.MemberAccess | BindingFlags.SetProperty | BindingFlags.SetField,null,
					Parent,new object[1] { Value } );

				return null;
			}

			// *** Walk the . syntax - split into current object (Main) and further parsed objects (Subs)
			string Main = Property.Substring(0,lnAt);
			string Subs = Property.Substring(lnAt+1);


			object Sub = Parent.GetType().InvokeMember(Main,wwUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField,null,
				Parent,null);

			return SetPropertyExCom(Sub,Subs,Value);
		}


		/// <summary>
		/// Wrapper method to call a 'dynamic' (non-typelib) method
		/// on a COM object
		/// </summary>
		/// <param name="Params"></param>
		/// 1st - Method name, 2nd - 1st parameter, 3rd - 2nd parm etc.
		/// <returns></returns>
		public static object CallMethodCom(object Object,string Method, params object[] Params)
		{
			return Object.GetType().InvokeMember(Method,wwUtils.MemberAccess | BindingFlags.InvokeMethod,null,Object,Params);
		}

		#endregion

		#region Object Serialization routines
		/// <summary>
		/// Returns a string of all the field value pairs of a given object.
		/// Works only on non-statics.
		/// </summary>
		/// <param name="Obj"></param>
		/// <param name="Separator"></param>
		/// <returns></returns>
		public static string ObjectToString(object Obj, string Separator,ObjectToStringTypes Type) 
		{
			FieldInfo[] fi = Obj.GetType().GetFields();
			
			string lcOutput = "";

			if (Type == ObjectToStringTypes.Properties || Type == ObjectToStringTypes.PropertiesAndFields)
			{
				foreach (PropertyInfo Property in Obj.GetType().GetProperties())
				{
					try
					{
						lcOutput = lcOutput + Property.Name + ":" + Property.GetValue(Obj, null).ToString() + Separator;
					}
					catch
					{
						lcOutput = lcOutput + Property.Name + ": n/a"  + Separator;
					}
				}
			}

			if (Type == ObjectToStringTypes.Fields || Type == ObjectToStringTypes.PropertiesAndFields)
			{
				foreach (FieldInfo Field in fi)
				{
					try
					{
						lcOutput = lcOutput + Field.Name + ": " + Field.GetValue(Obj).ToString() + Separator;
					}
					catch
					{
						lcOutput = lcOutput + Field.Name + ": n/a" + Separator;
					}
				}
			}
			return lcOutput;
		}

		public enum ObjectToStringTypes
		{
			Properties,
			PropertiesAndFields,
			Fields
		}

		/// <summary>
		/// Serializes an object instance to a file.
		/// </summary>
		/// <param name="Instance">the object instance to serialize</param>
		/// <param name="Filename"></param>
		/// <param name="BinarySerialization">determines whether XML serialization or binary serialization is used</param>
		/// <returns></returns>
		public static bool SerializeObject(object Instance, string Filename, bool BinarySerialization) 
		{
			bool retVal = true;

			if (!BinarySerialization) 
			{
				XmlTextWriter writer = null;
				try
				{
					XmlSerializer serializer = 
						new XmlSerializer(Instance.GetType());
		
					// Create an XmlTextWriter using a FileStream.
					Stream fs = new FileStream(Filename, FileMode.Create);
					writer = 	new XmlTextWriter(fs, new UTF8Encoding());
					writer.Formatting = Formatting.Indented;
					writer.IndentChar = ' ';
					writer.Indentation = 3;
						
					// Serialize using the XmlTextWriter.
					serializer.Serialize(writer,Instance);
				}
				catch(Exception) 
				{
					retVal = false;
				}
				finally
				{
					if (writer != null)
						writer.Close();
				}
			}
			else 
			{
				Stream fs = null;
				try
				{
					BinaryFormatter serializer = new BinaryFormatter();
					fs = new FileStream(Filename, FileMode.Create);
					serializer.Serialize(fs,Instance);
				}
				catch 
				{
					retVal = false;
				}
				finally
				{
					if (fs != null)
						fs.Close();
				}
			}
		
			return retVal;
		}

		/// <summary>
		/// Overload that supports passing in an XML TextWriter. Note the Writer is not closed
		/// </summary>
		/// <param name="Instance"></param>
		/// <param name="writer"></param>
		/// <param name="BinarySerialization"></param>
		/// <returns></returns>
		public static bool SerializeObject(object Instance, XmlTextWriter writer) 
		{
			bool retVal = true;

			try
			{
				XmlSerializer serializer = 
					new XmlSerializer(Instance.GetType());
	
				// Create an XmlTextWriter using a FileStream.
				writer.Formatting = Formatting.Indented;
				writer.IndentChar = ' ';
				writer.Indentation = 3;
					
				// Serialize using the XmlTextWriter.
				serializer.Serialize(writer,Instance);
			}
			catch(Exception ex) 
			{
				string Message = ex.Message;
				retVal = false;
			}
	
			return retVal;
		}

		/// <summary>
		/// Serializes an object into a string variable for easy 'manual' serialization
		/// </summary>
		/// <param name="Instance"></param>
		/// <returns></returns>
		public static bool SerializeObject(object Instance,out string XmlResultString) 
		{
			XmlResultString = "";
			MemoryStream ms = new MemoryStream();

			XmlTextWriter writer = 	new XmlTextWriter(ms, new UTF8Encoding());
			
			if (!SerializeObject(Instance,writer)) 
			{
				ms.Close();
				return false;
			}

			byte[] Result = new byte[ms.Length];
			ms.Position = 0;
			ms.Read(Result,0,(int)ms.Length);

			XmlResultString =  Encoding.UTF8.GetString(Result,0,(int) ms.Length);

			ms.Close();
			writer.Close();

			return true;
		}


		/// <summary>
		/// Serializes an object instance to a file.
		/// </summary>
		/// <param name="Instance">the object instance to serialize</param>
		/// <param name="Filename"></param>
		/// <param name="BinarySerialization">determines whether XML serialization or binary serialization is used</param>
		/// <returns></returns>
		public static bool SerializeObject(object Instance, out byte[] ResultBuffer)
		{
			bool retVal = true;

			MemoryStream ms = null;
			try
			{
				BinaryFormatter serializer = new BinaryFormatter();
				ms = new MemoryStream();
				serializer.Serialize(ms, Instance);
			}
			catch
			{
				retVal = false;
			}
			finally
			{
				if (ms != null)
					ms.Close();
			}

			ResultBuffer = ms.ToArray();

			return retVal;
		}

		/// <summary>
		/// Deserializes an object from file and returns a reference.
		/// </summary>
		/// <param name="Filename">name of the file to serialize to</param>
		/// <param name="ObjectType">The Type of the object. Use typeof(yourobject class)</param>
		/// <param name="BinarySerialization">determines whether we use Xml or Binary serialization</param>
		/// <returns>Instance of the deserialized object or null. Must be cast to your object type</returns>
		public static object DeSerializeObject(string Filename,Type ObjectType,bool BinarySerialization) 
		{
			object Instance = null;
			
			if (!BinarySerialization) 
			{

				XmlReader reader = null;
				XmlSerializer serializer = null;
				FileStream fs = null;
				try 
				{
					// Create an instance of the XmlSerializer specifying type and namespace.
					serializer = new XmlSerializer(ObjectType);

					// A FileStream is needed to read the XML document.
					fs = new FileStream(Filename, FileMode.Open);
					reader = new XmlTextReader(fs);
				
					Instance = serializer.Deserialize(reader);

				}
				catch 
				{
					return null;
				}
				finally
				{
					if (fs != null)
						fs.Close();

					if (reader != null)
						reader.Close();
				}
			}
			else 
			{

				BinaryFormatter serializer = null;
				FileStream fs = null;

				try 
				{
					serializer = new BinaryFormatter();
					fs = new FileStream(Filename, FileMode.Open);
					Instance = serializer.Deserialize(fs);

				}
				catch 
				{
					return null;
				}
				finally
				{
					if (fs != null)
						fs.Close();
				}
			}

			return Instance;
		}
		
		/// <summary>
		/// Deserialize an object from an XmlReader object.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="ObjectType"></param>
		/// <returns></returns>
		public static object DeSerializeObject(System.Xml.XmlReader reader,Type ObjectType) 
		{
			XmlSerializer serializer = new XmlSerializer(ObjectType);
			object Instance = serializer.Deserialize(reader);
			reader.Close();

			return Instance;
		}

		public static object DeSerializeObject(string XML,Type ObjectType) 
		{
			XmlTextReader reader = new XmlTextReader(XML,XmlNodeType.Document,null);
			return DeSerializeObject(reader,ObjectType);
		}

		public static object DeSerializeObject(byte[] Buffer, Type ObjectType)
		{
			BinaryFormatter serializer = null;
			MemoryStream ms = null;
			object Instance = null;

			try
			{
				serializer = new BinaryFormatter();
				ms = new MemoryStream(Buffer);
				Instance = serializer.Deserialize(ms);

			}
			catch
			{
				return null;
			}
			finally
			{
				if (ms != null)
					ms.Close();
			}

			return Instance;
		}
		#endregion

		#region Miscellaneous Routines 

		
		/// <summary>
		/// Returns the logon password stored in the registry if Auto-Logon is used.
		/// This function is used privately for demos when I need to specify a login username and password.
		/// </summary>
		/// <param name="GetUserName"></param>
		/// <returns></returns>
		public static string GetSystemPassword(bool GetUserName) 
		{
			RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
			if (RegKey == null)
				return "";
			
			string Password;
			if (!GetUserName)
				Password = (string) RegKey.GetValue("DefaultPassword");
			else
				Password = (string) RegKey.GetValue("DefaultUsername");

			if (Password == null) 
				return "";

			return (string) Password;
		}

		/// <summary>
		/// Converts the passed date time value to Mime formatted time string
		/// </summary>
		/// <param name="Time"></param>
		public static string MimeDateTime(DateTime Time)
		{
			TimeSpan Offset = TimeZone.CurrentTimeZone.GetUtcOffset(Time);
            
			string sOffset = Offset.Hours.ToString().PadLeft(2, '0');
			if (Offset.Hours < 0)
				sOffset = "-" + (Offset.Hours * -1).ToString().PadLeft(2, '0');

			sOffset += Offset.Minutes.ToString().PadLeft(2,'0');
            
			return "Date: " + DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss",
				System.Globalization.CultureInfo.InvariantCulture) +
				" " + sOffset ;
		}

		/// <summary>
		/// Single method to retrieve HTTP content from the Web quickly
		/// </summary>
		/// <param name="Url"></param>
		/// <param name="ErrorMessage"></param>
		/// <returns></returns>
		public static string HttpGet(string Url, ref string ErrorMessage)
		{
			string MergedText = "";

			System.Net.WebClient Http = new System.Net.WebClient();

			// Download the Web resource and save it into a data buffer.
			try
			{
				byte[] Result = Http.DownloadData(Url);
				MergedText = Encoding.Default.GetString(Result);
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
				return null;
			}

			return MergedText;
		}


		#endregion

		#region Path Functions
		/// <summary>
		/// Returns the full path of a full physical filename
		/// </summary>
		/// <param name="Path"></param>
		/// <returns></returns>
		public static string JustPath(string Path) 
		{
			FileInfo fi = new FileInfo(Path);
			return fi.DirectoryName + "\\";
		}

		/// <summary>
		/// Returns a relative path string from a full path.
		/// </summary>
		/// <param name="FullPath">The path to convert. Can be either a file or a directory</param>
		/// <param name="BasePath">The base path to truncate to and replace</param>
		/// <returns>
		/// Lower case string of the relative path. If path is a directory it's returned without a backslash at the end.
		/// 
		/// Examples of returned values:
		///  .\test.txt, ..\test.txt, ..\..\..\test.txt, ., ..
		/// </returns>
		public static string GetRelativePath(string FullPath, string BasePath ) 
		{
			// *** Start by normalizing paths
			FullPath = FullPath.ToLower();
			BasePath = BasePath.ToLower();

			if ( BasePath.EndsWith("\\") ) 
				BasePath = BasePath.Substring(0,BasePath.Length-1);
			if ( FullPath.EndsWith("\\") ) 
				FullPath = FullPath.Substring(0,FullPath.Length-1);

			// *** First check for full path
			if ( (FullPath+"\\").IndexOf(BasePath + "\\") > -1) 
				return  FullPath.Replace(BasePath,".");

			// *** Now parse backwards
			string BackDirs = "";
			string PartialPath = BasePath;
			int Index = PartialPath.LastIndexOf("\\");
			while (Index > 0) 
			{
				// *** Strip path step string to last backslash
				PartialPath = PartialPath.Substring(0,Index );
			
				// *** Add another step backwards to our pass replacement
				BackDirs = BackDirs + "..\\" ;

				// *** Check for a matching path
				if ( FullPath.IndexOf(PartialPath) > -1 ) 
				{
					if ( FullPath == PartialPath )
						// *** We're dealing with a full Directory match and need to replace it all
						return FullPath.Replace(PartialPath,BackDirs.Substring(0,BackDirs.Length-1) );
					else
						// *** We're dealing with a file or a start path
						return FullPath.Replace(PartialPath+ (FullPath == PartialPath ?  "" : "\\"),BackDirs);
				}
				Index = PartialPath.LastIndexOf("\\",PartialPath.Length-1);
			}

			return FullPath;
		}
		#endregion

		#region Shell Functions for displaying URL, HTML, Text and XML
		[DllImport("Shell32.dll")]
		private static extern int ShellExecute(int hwnd, string lpOperation, 
			string lpFile, string lpParameters, 
			string lpDirectory, int nShowCmd);

		/// <summary>
		/// Uses the Shell Extensions to launch a program based or URL moniker.
		/// </summary>
		/// <param name="lcUrl">Any URL Moniker that the Windows Shell understands (URL, Word Docs, PDF, Email links etc.)</param>
		/// <returns></returns>
		public static int GoUrl(string Url)
		{
			string TPath = Path.GetTempPath();

			int Result = ShellExecute(0,"OPEN",Url, "",TPath,1);
			return Result;
		}

		/// <summary>
		/// Displays an HTML string in a browser window
		/// </summary>
		/// <param name="HtmlString"></param>
		/// <returns></returns>
		public static int ShowString(string HtmlString,string extension) 
		{
			if (extension == null)
				extension = "htm";

			string File = Path.GetTempPath() + "\\__preview." + extension;
			StreamWriter sw = new StreamWriter(File,false,Encoding.Default);
			sw.Write( HtmlString);
			sw.Close();

			return GoUrl(File);
		}

		public static int ShowHtml(string HtmlString) 
		{
			return ShowString(HtmlString,null);
		}

		/// <summary>
		/// Displays a large Text string as a text file.
		/// </summary>
		/// <param name="TextString"></param>
		/// <returns></returns>
		public static int ShowText(string TextString) 
		{
			string File = Path.GetTempPath() + "\\__preview.txt";

			StreamWriter sw = new StreamWriter(File,false);
			sw.Write(TextString);
			sw.Close();

			return GoUrl(File);
		}
		#endregion


#if false
		/// <summary>
		/// Parses the text of a Soap Exception and returns just the error message text
		/// Ideally you'll want to have a SoapException fire on the server, otherwise
		/// this method will try to parse out the inner exception error message.
		/// </summary>
		/// <param name="SoapExceptionText"></param>
		/// <returns></returns>
		public static string ParseSoapExceptionText(string SoapExceptionText) 
		{
			string Message = wwUtils.ExtractString(SoapExceptionText,"SoapException: ","\n");
			if (Message != "")
				return Message;

			Message = wwUtils.ExtractString(SoapExceptionText,"SoapException: "," --->");
			if (Message == "Server was unable to process request.") 
			{
				Message = wwUtils.ExtractString(SoapExceptionText,"-->","\n");
				Message = Message.Substring(Message.IndexOf(":")+1);
			}

			if (Message == "")
				return "An error occurred on the server.";

			return Message;
		}
#endif
	}

}



