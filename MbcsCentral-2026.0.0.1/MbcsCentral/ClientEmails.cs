using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral
{
    public class ClientEmails
    {
        public ClientEmails() { }

        public ClientEmails(string mailTag, string emailAddress)
        {
            MailTag = mailTag;
            EmailAddress = emailAddress;
        }

        public string MailTag { get; set; }
        public string EmailAddress { get; set; }
    }
}
