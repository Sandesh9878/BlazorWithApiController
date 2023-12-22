using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }

        public String Message { get; set; }

        public String ErrorCode { get; set; }

        public object responseData { get; set; } = null;

    }
}
