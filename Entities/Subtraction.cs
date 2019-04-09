using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Substraction
    {
        public int? Minuend { get; set; }
        public int? Subtrahend { get; set; }
        public int? Difference { get; set; }

        public override string ToString()
        {
            return $"{Difference}";
        }
    }
}
