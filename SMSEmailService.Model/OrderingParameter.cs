using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
     public class OrderingParameter
    {
        public string FieldName { get; set; }
        public bool IsOrderByAscending { get; set; } = true;
    }
}
