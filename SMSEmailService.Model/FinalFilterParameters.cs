using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class FinalFilterParameters
    {
        public int pageNo { get; set; } = 1;
        public int size { get; set; } = 10;
        public OrderingParameter Order { get; set; }
        public List<FilteringParameter> filters { get; set; }
    }
}
