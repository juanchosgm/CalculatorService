using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Square
    {
        public int? Number { get; set; }

        [JsonProperty(PropertyName = "Square")]
        public double? SquareResult { get; set; }

        public override string ToString()
        {
            return $"{SquareResult}";
        }
    }
}
