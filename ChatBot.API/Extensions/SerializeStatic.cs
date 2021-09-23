using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ChatBot.API.Extensions
{
    public class SerializeStatic
    {
        public static List<string> SerializeSuperClass(Type static_class)
        {
            var subclasses = static_class.GetNestedTypes(BindingFlags.Static | BindingFlags.Public);
            List<string> fields = new List<string>();
            foreach(var subclass in subclasses)
            {
                List<string> tmp = SerializeClass(subclass);
                fields.AddRange(tmp);
            }
            return fields;
        }

        public static List<string> SerializeClass(Type static_class)
        {
            var fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);

            List<string> a = new List<string>();
            foreach (FieldInfo field in fields)
            {
                a.Add((string)field.GetValue(null));
            };
            return a;
        }
    }
}
