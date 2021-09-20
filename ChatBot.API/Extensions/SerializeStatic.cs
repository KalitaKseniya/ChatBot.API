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
        public static string SerializeSuperClass(Type static_class)
        {
            try
            {
                var subclasses = static_class.GetNestedTypes(BindingFlags.Static | BindingFlags.Public);
                Dictionary<string, List<string>> pairs = new Dictionary<string, List<string>>();
                foreach(var subclass in subclasses)
                {
                    string key = subclass.Name;
                    
                    List<string> value = SerializeClass(subclass);
                    pairs.Add(key, value);
                }
                return JsonConvert.SerializeObject(pairs);
            }
            catch(Exception ex)
            {
                return null;
            }
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
