using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Error
    {
        public string ErrorCode { get; set; }
        public int ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"{ErrorCode}{Environment.NewLine}" +
                $"{ErrorStatus}{Environment.NewLine}" +
                $"{ErrorMessage}";
        }
    }
}
