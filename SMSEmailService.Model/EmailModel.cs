using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMSEmailService.Model
{
    public class EmailModel
    {
        [EmailAddress]
        [Required]
        public string SenderEmail { get; set; }

        public string SenderName { get; set; }

        [EmailAddress]
        [Required]
        public string ReceiverEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string EmailContent { get; set; }

        [Required]
        public string WebsiteUrl { get; set; }

        [Required]
        public string EmailType { get; set; }
        public string TemplateName { get; set; }
        public string Status { get; set; }
        public string FailedMessage { get; set; }
        public string SuccessUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FailureUrl { get; set; }
    }
}
