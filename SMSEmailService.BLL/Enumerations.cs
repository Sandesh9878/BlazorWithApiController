using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSEmailService
{
    public class Enumerations
    {
        public enum EmailStatus
        {
            Pending,
            Success,
            Failed
        }

        public enum SMSStatus
        {
            Pending,
            Success,
            Failed
        }

        public enum SMSColumn
        {
            FirstName,
            LastName,
            MobileNo,
            Subject,
            SMSType,
            SMSTemplate,
            WebsiteUrl,
            CreatedDate,
            Status,
            FailedMessage,
            SuccessUrl,
            FailureUrl
        }

        public enum EmailColumns
        {
            SenderEmail,
            SenderName,
            ReceiverEmail,
            Subject,
            EmailContent,
            EmailType,
            WebsiteUrl,
            CreatedDate,
            TransactionDate,
            Status,
            FailedMessage,
            SuccessUrl,
            FailureUrl
        }

        public enum checker
        {
            Contains,
            GreaterThan,
            LessThan,
            Equals
        }
    }
}
