using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbcsCentral.Email
{
    public class EmailMessage
    {
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set;}
        public string Date { get; set; }
        public string EmailType { get; set;  }
        public string FileName { get; set; }

        public static EmailMessage ParseEmailFile(string filename)
        {
            try
            {
                string line;
                bool inMsg = false;
                string msg = "";
                StreamReader sr = new StreamReader(filename);
                EmailMessage email = new EmailMessage();
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (inMsg)
                    {
                        msg += line + "\n";
                    }
                    else
                    {
                        if (line.ToLower().StartsWith("to:"))
                            email.ToAddress = line.Substring(3).Trim();
                        else if (line.ToLower().StartsWith("from:"))
                            email.FromAddress = line.Substring(5).Trim();
                        else if (line.ToLower().StartsWith("subject:"))
                            email.Subject = line.Substring(8);
                        else if (line.ToLower().StartsWith("date:"))
                            email.Date = line.Substring(5).Trim();
                        else if (line.ToLower().StartsWith("message:"))
                        {
                            inMsg = true;
                        }
                    }
                }
                email.FileName = filename;
                email.Message = msg;
                string emailtype = filename.Substring(filename.LastIndexOf("\\") + 1);
                string[] s = emailtype.Split('_');
                if (s.Length > 0)
                    email.EmailType = s[0];
                return email;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
