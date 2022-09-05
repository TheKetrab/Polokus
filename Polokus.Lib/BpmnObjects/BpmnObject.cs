using Polokus.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polokus.Lib.BpmnObjects
{
    public abstract class BpmnObject : IBpmnObject
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";

        public BpmnObject(XElement element)
        {
            Type classType = GetType();
            foreach (var prop in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var attr = element.Attributes().FirstOrDefault(x => AttributeForProperty(x, prop));
                if (attr != null)
                {
                    try
                    {
                        object val = TryConvertAttributeValue(prop.PropertyType, attr.Value);
                        prop.SetValue(this, val);
                    }
                    catch (ArgumentException exc)
                    {

                    }
                    catch (FormatException exc)
                    {

                    }
                }
            }

        }

        private object TryConvertAttributeValue(Type desiredType, string attributeValue)
        {
            if (desiredType == typeof(string)) return attributeValue;
            if (desiredType == typeof(bool)) return Convert.ToBoolean(attributeValue);
            if (desiredType == typeof(int)) return Convert.ToInt32(attributeValue);

            throw new ArgumentException($"Conversion for type {desiredType.FullName} is not implemented.");
        }

        protected static bool AttributeForProperty(XAttribute attr, PropertyInfo pi)
        {
            return attr.Name.LocalName.Equals(pi.Name, StringComparison.OrdinalIgnoreCase);
        }


    }
}
