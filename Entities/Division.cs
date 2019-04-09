using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Division
    {
        public int? Dividend { get; set; }
        public int? Divisor { get; set; }
        public int? Quotient { get; set; }
        public int? Remainder { get; set; }

        public override string ToString()
        {
            return $"Quotient: {Quotient}\tRemainder: {Remainder}";
        }
    }
}
