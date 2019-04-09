using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class JournalQuery
    {
        public int? Id { get; set; }
        public IEnumerable<Operation> Operations { get; set; }
    }
}
