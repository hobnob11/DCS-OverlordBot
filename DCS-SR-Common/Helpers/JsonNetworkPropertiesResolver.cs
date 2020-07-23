using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ciribob.DCS.SimpleRadio.Standalone.Common.Helpers
{
    public class JsonNetworkPropertiesResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            List<MemberInfo> list = base.GetSerializableMembers(objectType);

            //filter out things we dont want on the TCP network sync
            list = list.Where(pi => !Attribute.IsDefined(pi, typeof(JsonNetworkIgnoreSerializationAttribute))).ToList();

            return list;
        }
    }
}
