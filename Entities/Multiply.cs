using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Multiply
    {
        public int?[] Factors { get; set; }

        public int? Product { get; set; }

        public override string ToString()
        {
            return $"{Product}";
        }
    }
}
