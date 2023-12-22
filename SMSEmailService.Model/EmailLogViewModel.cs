using System;

namespace SMSEmailService.Model
{
    public class EmailLogViewModel
    {
        public int EmailId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}