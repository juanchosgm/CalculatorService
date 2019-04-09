using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Additional
    {
        public int?[] Addends { get; set; }

        public int Sum { get; set; }

        public override string ToString()
        {
            return $"{Sum}";
        }
    }
}
