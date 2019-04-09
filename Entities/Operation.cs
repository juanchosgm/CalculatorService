using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Operation
    {
        [JsonIgnore]
        public int XEviTrackingId { get; set; }
        [JsonProperty(PropertyName = "Operation", Order = 1)]
        public string OperationName { get; set; }
        [JsonProperty(Order = 2)]
        public string Calculation { get; set; }
        [JsonConverter(typeof(DateFormatConverter))]
        [JsonProperty(Order = 3)]
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"Operation: {OperationName}\tCalculation: {Calculation}\tDate: {Date.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
        }
    }
}
