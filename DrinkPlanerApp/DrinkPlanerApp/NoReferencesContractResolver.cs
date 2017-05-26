using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace DrinkPlanerApp
{
    public class NoReferencesContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (prop.DeclaringType.GetRuntimeProperties().Any(p => p.Name == prop.PropertyName + "Id"))
            {
                prop.ShouldSerialize = obj => false;
            }

           /* if(prop.PropertyType.GetTypeInfo().IsClass &&
            prop.PropertyType != typeof(string))
            {
                prop.ShouldSerialize = obj => false;
            }*/

            return prop;
        }
    }
}
