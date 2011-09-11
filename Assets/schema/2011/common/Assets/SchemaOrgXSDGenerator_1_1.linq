<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.XML.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Linq.dll</Reference>
  <Reference>C:\lib\Json35r8\Bin\DotNet\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Serialization.dll</Reference>
  <Reference>&lt;Personal&gt;\Visual Studio 2010\Projects\AIM\Dependencies\Common.Messaging.dll</Reference>
  <Namespace>System.Xml</Namespace>
  <Namespace>System.Xml.Linq</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>System.Xml.Schema</Namespace>
  <Namespace>Common.Messaging</Namespace>
  <Namespace>Common.Messaging.Http</Namespace>
</Query>

void Main()
{	
	var allClasses = File.ReadAllLines(@"C:\Users\Administrator\Downloads\all-classes.csv").Skip(1).Select(n=>new ClassDefinition(n));
	var allProperties = File.ReadAllLines(@"C:\Users\Administrator\Downloads\all-properties.csv").Skip(1).Select(n=>new PropertyDefinition(n));
	
	var processedClasses=new List<ClassDefinition>();
	allClasses.ToList().ForEach(classDef=>
	{
		classDef.ContainedProperties.ForEach(contained=>
		{			
			var prop = allProperties.Where(n=>n.Name.ToLower() == contained.ToLower());
			
			if(prop.Count()==1)
			{				
				classDef.Properties.Add(prop.First());			
			}
			
		});
		processedClasses.Add(classDef);
	});

processedClasses.ToList().ForEach(n=>
	{

		try
		{
			var schema = n.ToSchema();
			var stringValue = SchemaHelper.GetSchemaString(schema);
			XElement.Parse(stringValue).Save(String.Format(@"C:\Temp\schema\2011\common\{0}.xsd",n.Id));	
		}
		catch(System.Exception ex)
		{
			ex.Dump();
			n.Dump();
		}
		
	});
}
public class ClassDefinition
{
	public ClassDefinition(string line)
	{
		var values = line.Split(',');
		Id=values[0].Trim();
		Label=values[1].Trim();
		Comment=values[2].Trim();
		Ancestors=values[3].Split(' ').Where(n=>!String.IsNullOrWhiteSpace(n)).ToList();
		SuperTypes=values[4].Split(' ').Where(n=>!String.IsNullOrWhiteSpace(n)).ToList();
		SubTypes=values[5].Split(' ').Where(n=>!String.IsNullOrWhiteSpace(n)).ToList();
		ContainedProperties=values[6].Trim().Split(' ').Where(n=>!String.IsNullOrWhiteSpace(n)).ToList();
		Name = Label.Replace(" ","");
		Parent=Ancestors.Count>0?Ancestors.Last():String.Empty;
		Properties=new List<PropertyDefinition>();
		
	}
	public string NS{get;set;}
	public List<PropertyDefinition> Properties{get;set;}
	public string Parent{get;set;}
	public string Id{get;set;}
	public string Label{get;set;}
	public string Comment{get;set;}
	public string Name{get;set;}
	public List<string> Ancestors{get;set;}
	public List<string> SuperTypes{get;set;}
	public List<string> SubTypes{get;set;}
	public List<string> ContainedProperties{get;set;}
	public static string CamelCase(string name)
	{
			return name.Substring(0,1).ToLower()+name.Substring(1);
	}
	public XmlSchema ToSchema()
	{
		var result = SchemaHelper.GetSchema();	
		var imports = Properties.SelectMany(n=>n.Ranges).Select(n=>n).Distinct();
		if(imports != null)
			{
			
				imports.ToList().ForEach(i=>
				{
					if(i != Id)
					{
						XmlSchemaImport import = new XmlSchemaImport();		
						import.SchemaLocation = String.Format("{0}.xsd",i);
						import.Namespace = String.Format("http://schema.org/{0}",i);				
						result.Includes.Add(import);
					}
				});
			
			}
		if(!String.IsNullOrEmpty(Parent) && !imports.Contains(Parent))
		{
						XmlSchemaImport import = new XmlSchemaImport();		
						import.SchemaLocation = String.Format("{0}.xsd",Parent);
						import.Namespace = String.Format("http://schema.org/{0}",Parent);				
						result.Includes.Add(import);
		}
		result.TargetNamespace = String.Format("http://schema.org/{0}",Id);
		var complexType = new XmlSchemaComplexType();
		complexType.Name=Id;
		result.Items.Add(complexType);		
		var sequence = new XmlSchemaSequence();	
		
		if(SuperTypes.Count>0)
		{
			var complexContent = new XmlSchemaComplexContent();			
			complexType.ContentModel = complexContent;
			var extension = new XmlSchemaComplexContentExtension();
			extension.BaseTypeName=new XmlQualifiedName(Parent,String.Format("http://schema.org/{0}",Parent));
			complexContent.Content=extension;
				
			
			if(Properties.Count>0)
			{		
				
				extension.Particle = sequence;
				Properties.ToList().ForEach(prop=>
				{
					prop.AddSchemaElement(result,sequence);
				});
			}else
			{
				//result.Items.Add(extension);
			}
			
		}		
		return result;
		
	}
}
public class PropertyDefinition
{
	public PropertyDefinition(string line)
	{
		var values = line.Split(',');
		Id=(values[0].Trim().Substring(0,1).ToUpper() + values[0].Trim().Substring(1)).Trim();
		Label=values[1].Trim();
		Comment=values[2].Trim();
		Domains=values[3].Trim();
		Ranges=values[4].Split(' ').Where(n=>!String.IsNullOrWhiteSpace(n)).ToList();
		Name=Label.Replace(" ","").Trim();
		
		if(values[5]=="Collection")
		{			
			IsCollection = true;
		}
			
	}
	public bool IsCollection{get;set;}
	public string Id{get;set;}
	public string Label{get;set;}
	public string Comment{get;set;}
	public string Domains{get;set;}
	public List<string> Ranges{get;set;}
	public string Name{get;set;}
	
	public void AddSchemaElement(XmlSchema schema,XmlSchemaObject parent)
	{
		
		XmlSchemaElement propertyElement = new XmlSchemaElement();
		propertyElement.Parent = parent;
		propertyElement.Name=Id;
		schema.Items.Add(propertyElement);
		if(Ranges.Count==1)
		{
				propertyElement.SchemaTypeName=new XmlQualifiedName(Ranges.First(),String.Format("http://schema.org/{0}",Ranges.First()));
		}else
		{		
			
			XmlSchemaComplexType complexType = new XmlSchemaComplexType();
			propertyElement.SchemaType = complexType;
			complexType.Parent = propertyElement;
			XmlSchemaChoice choice = new XmlSchemaChoice();	
			complexType.Particle = choice;
			Ranges.ForEach(range=>
			{
				var choiceElement = new XmlSchemaElement();
				choiceElement.Name=range;
				choiceElement.Namespaces.Add(ClassDefinition.CamelCase(range),String.Format("http://schema.org/{0}",range));
				choiceElement.SchemaTypeName=new XmlQualifiedName(range,String.Format("http://schema.org/{0}",range));
				choice.Items.Add(choiceElement);
			});		
		}
		
		
		
	}
	
}
public static class SchemaHelper
{
	public const string SCHEMA_NS = "http://www.w3.org/2001/XMLSchema";
	public static XmlSchema GetSchema()
	{
		XmlSchema result = new XmlSchema();
		result.Namespaces.Add("xs",SCHEMA_NS);
		result.ElementFormDefault = XmlSchemaForm.Qualified;
		result.AttributeFormDefault = XmlSchemaForm.Unqualified;
		return result;
	}
	public static string GetSchemaString(this XmlSchema schema)
	{
		StringBuilder b = new StringBuilder();
	
		using(StringWriter sw = new StringWriter(b))
		{
			schema.Write(sw);
		}
		return b.ToString();
	}
	
}
// Define other methods and classes here