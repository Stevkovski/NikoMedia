using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class EmailRecipients
    {
        public EmailRecipients()
        {
            CcAddresses = new List<EmailAddress>();
            BccAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> CcAddresses { get; set; }
        public List<EmailAddress> BccAddresses { get; set; }

        public bool HasRecipients 
        {
            get 
            {
                var hasCC = CcAddresses != null && CcAddresses.Any();
                var hasBcc = BccAddresses != null && BccAddresses.Any();
                return hasCC || hasBcc;
            }
        }
    }
}
