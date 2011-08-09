using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Common.Services.Payment.Interfaces;
namespace Common.Services.Payment
{
    public class SerializationHelper
    {
        public static void WritePaymentData(XmlWriter writer,IPaymentData target)
        {
            if(target.CardData != null)
                WritePaymentCardData(writer, target.CardData);

        }
        public static void WriteAddressType(XmlWriter writer,IAddressType target)
        {
            Write<AddressType>(writer, target as AddressType); 
        }
        public static void WritePaymentCardData(XmlWriter writer,IPaymentCardData target)
        {
            Write<PaymentCardData>(writer, target as PaymentCardData);     
        }
        static void Write<T>(XmlWriter writer,T targetType) 
        {
            string strType = targetType.GetType().FullName;
            writer.WriteAttributeString("type", strType);
            writer.WriteStartElement(targetType.GetType().Name);
            XmlSerializer serial = new XmlSerializer(Type.GetType(strType));
            writer.WriteEndElement();
        }
    }
}
