using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace MbcsCentral.Email
{
    static public class Utils
    {
        
        static public string subjectRef = " <{0:00000}.{1:00000}.{2:00000}>"; // issue id, sending acct #, tenant #"
      /// <summary>
      /// Retrieves SMTP account for all users or specified user that matches the passed mailTag 
      /// </summary>
      /// <param name="mailTag">REQUIRED mailTag to retrieve</param>
      /// <param name="message">Any error message generated</param>
      /// <param name="smtp">The SMTP account found</param>
      /// <param name="fromEmail">The EmailAddress of the SMTP acount, if found</param>
      /// <param name="ByAcct">If looking for a user account this is set to TRUE</param>
      /// <returns>The SMTP account, if any, that matches the criteria</returns>
        public static bool GetEmailConfig(string mailTag,out string message, out Email.SmtpConfig smtp, out string fromEmail, bool ByAcct = false)
        {
            int accountID = (int)GlobalShared.user.Account.accountId;
            message = ""; smtp = null; fromEmail = "";
            MbcsCentral.Clients client = MbcsCentral.Clients.load(GlobalShared.user.Client.clientId);
            if (client == null)
            {
                message = "Client record could not be retrieved.";
                return false;
            }
            ArrayList emails = client.EmailsAsList;
            if (emails == null || emails.Count == 0)
            {
                message = "There are no email addresses for the organization.";
                return false;
            }
            bool emailFound = false;
            MbcsCentral.ClientEmails email = null;
            foreach (MbcsCentral.ClientEmails em in emails)
            {
                if (em.MailTag == mailTag)
                {
                    emailFound = true;
                    email = em;
                    break;
                }
            }
            if (!emailFound)
            {
                message = "No email address found for given MailTag <" + mailTag + ">";
                return false;
            }
            fromEmail = email.EmailAddress;
            if (ByAcct)
            {
                SmtpConfig[] smtps = SmtpConfig.loadByAccount(accountID, mailTag);
                if (smtps != null && smtps.Length > 0)
                {
                    smtp = null;
                    if (smtps.Length == 1)
                        smtp = smtps[0];
                    else
                    {
                        frmChooseEmailAccount frm = new frmChooseEmailAccount() { emails = smtps };
                        frm.ShowDialog();
                        if (frm.ID <0)
                        {
                            return false;
                        }

                        smtp = smtps[frm.ID];
                    }
                }
            }
            else
                smtp = SmtpConfig.load(mailTag);
            if (smtp == null)
            {
                message = "Smtp Configuration not found for given MailTag <" + mailTag + ">";
                return false;
            }
            return true;
        }


        public static ArrayList SendEmail(MbcsCentral.Email.SmtpConfig smtp, string fromEmail, string fromDisplayName, string toEmail, string subject, string body, string BCC, string attachment = null, bool keepAttachment = true, bool returnReceipt = false, string replyTo = "")
        {
            ArrayList list = new ArrayList();
            MbcsMail.MbcsMail mail = new MbcsMail.MbcsMail() { SmtpHost = smtp.server, outgoingPort = smtp.port, SmtpPassword = smtp.password, body = body };
            mail.ReturnReceipt = returnReceipt;

            mail.SmtpUsername = smtp.userName;
            mail.emailAddress = smtp.emailAddress;
            mail.displayName = fromDisplayName;
            mail.ReplyTo = replyTo;
            mail.setMessage(smtp.emailAddress, toEmail, "", subject, body);
            mail.SmtpPort = smtp.port;  
            // Header

            if (attachment != null && !attachment.Trim().Equals(""))
            {
                if (attachment != null && attachment.Trim().Equals(""))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<SendEmail> Attachment specified but there is no filename. To: " + toEmail, false);
                    list.Add("<SendEmail> Attachment specified but there is no filename. To: " + toEmail);
                    return list;
                }
                if (mail.AddFileAttachment(attachment).Trim().Equals(""))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<SendEmail> Attachment [" + attachment + "] specified but unable to add. To: " + toEmail, false);
                    list.Add("<SendEmail> Attachment [" + attachment + "] specified but unable to add. To: " + toEmail);
                    return list;
                }
            }
            if (smtp.SSL)
                mail.SmtpSsl = true;
            else if (smtp.TSL)
                mail.StartTLS = true;
            if (!BCC.Trim().Equals(""))
                mail.setAddCC(BCC);

            // Return Receipt?
            bool rc = mail.Send();
            if (!rc)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<SendEmail> Attachment [" + attachment + "] specified. \n\nTo: " + toEmail + "\n\nMessage = \n" + mail.LastErrorText, false);
                string msg;
                string pattern = @"<error>.+</error>";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = r.Match(mail.LastErrorXml);
                while (match.Success)
                {
                    msg = match.Value.ToString().Replace("<error>", "").Replace("</error>", "");
                    list.Add(msg);
                    match = match.NextMatch();
                }
                list.Add("==========\r\n");
                list.Add("SMTP Status: " + mail.LastSmtpStatus);
                list.Add(mail.LastErrorText);
                return list;
            }
            if (!keepAttachment)
            {
                try
                {
                    File.Delete(attachment);
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "SendEmail Attachment <" + attachment + ">\nCould not delete: \n" + ex.Message, false);
                }
            }
            return null;
        }

        public static ArrayList SendEmailAsync(out Chilkat.Task task, MbcsCentral.Email.SmtpConfig smtp, string fromEmail, string fromDisplayName, string toEmail, string subject, string body, string BCC, string attachment = null, bool keepAttachment = true, bool returnReceipt = false)
        {
            task = null;
            ArrayList list = new ArrayList();
            MbcsMail.MbcsMail mail = new MbcsMail.MbcsMail() { SmtpHost = smtp.server, outgoingPort = smtp.port, SmtpPassword = smtp.password, body = body };
            mail.ReturnReceipt = returnReceipt;

            mail.SmtpUsername = smtp.userName;
            mail.emailAddress = smtp.emailAddress;
            mail.displayName = fromDisplayName;
            mail.setMessage(smtp.emailAddress, toEmail, "", subject, body);
            // Header

            if (attachment != null)
            {
                if (attachment != null && attachment.Trim().Equals(""))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<SendEmail> Attachment specified but there is no filename. To: " + toEmail, false);
                    list.Add("<SendEmail> Attachment specified but there is no filename. To: " + toEmail);
                    return list;
                }
                if (mail.AddFileAttachment(attachment).Trim().Equals(""))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<SendEmail> Attachment [" + attachment + "] specified but unable to add. To: " + toEmail, false);
                    list.Add("<SendEmail> Attachment [" + attachment + "] specified but unable to add. To: " + toEmail);
                    return list;
                }
            }
            if (smtp.SSL)
                mail.SmtpSsl = true;
            else if (smtp.TSL)
                mail.StartTLS = true;
            if (!BCC.Trim().Equals(""))
                mail.setAddCC(BCC);

            // Return Receipt?
            bool rc = mail.SendAsync(ref task);
            if (!rc)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "<SendEmail> Attachment [" + attachment + "] specified. \n\nTo: " + toEmail + "\n\nMessage = \n" + mail.LastErrorText, false);
                string msg;
                string pattern = @"<error>.+</error>";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = r.Match(mail.LastErrorXml);
                while (match.Success)
                {
                    msg = match.Value.ToString().Replace("<error>", "").Replace("</error>", "");
                    list.Add(msg);
                    match = match.NextMatch();
                }
                return list;
            }
            if (!keepAttachment)
            {
                try
                {
                    File.Delete(attachment);
                }
                catch (Exception ex)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "SendEmail Attachment <" + attachment + ">\nCould not delete: \n" + ex.Message, false);
                }
            }
            return null;
        }

        public static bool TestEmail(string subEmail, string building, string tenant, string name, MbcsCentral.Email.SmtpConfig smtp, string fromEmail, string fromDisplayName, string toEmail, string subject, string body, string BCC, string attachment = null, bool keepAttachment = true, bool returnReceipt = false)
        {
            body = "<ORIGINAL EMAIL /" + toEmail + "/> <TENANT# /" + building + tenant + "/> <NAME /" + name + "/>";
            if (SendEmail(smtp, fromEmail, fromDisplayName, subEmail, subject, body, "", attachment, false, returnReceipt) == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Saves a formatted email message as a text file
        /// </summary>
        /// <param name="FromEmail"></param>
        /// <param name="DisplayName"></param>
        /// <param name="ToEmail"></param>
        /// <param name="Subject"></param>
        /// <param name="message">Body of the message</param>
        /// <param name="fileName">Full path to which to save</param>
        /// <param name="ReturnReceipt">Was there a return receipt specified</param>
        /// <returns>Null if successful otherwise error message</returns>
        public static string SaveEmail(string FromEmail, string DisplayName, string ToEmail, string Subject, string message, string fileName, string date, bool ReturnReceipt = false)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("From:    " + FromEmail);
                    sw.WriteLine("To:      " + ToEmail);
                    sw.WriteLine("Date:    " + date);
                    sw.WriteLine("Subject: " + Subject);
                    sw.WriteLine("");
                    if (ReturnReceipt)
                    {
                        sw.WriteLine("* With Return Receipt Requested *");
                        sw.WriteLine("");
                    }
                    sw.WriteLine("Message:");
                    sw.WriteLine(message);
                }
            } catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public static EmailMessage[] GetEmails(string directory, string baseName, int sequenceNumber = 0, int issueId = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            ArrayList emails = new ArrayList();
            string dirPath = directory;
            string filenames = String.Format("{0}_{1}_{2}_*.txt", baseName, (sequenceNumber == 0 ? "*" : sequenceNumber.ToString("00000")), (issueId == 0 ? "*" : issueId.ToString("00000")));
            System.Collections.ObjectModel.ReadOnlyCollection<string> files = Microsoft.VisualBasic.FileIO.FileSystem.GetFiles(directory, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, filenames);
            ArrayList sortArray = new ArrayList();

            foreach (string file in files)
            {
                sortArray.Add(file);
            }
            sortArray.Sort();
            string message;
            for (int i = sortArray.Count - 1; i >= 0; i--)
            {
                message = (string)sortArray[i];
                emails.Add(Email.EmailMessage.ParseEmailFile(message));
            }
            try
            {
                return ((EmailMessage[])(emails.ToArray(typeof(EmailMessage))));
            }
            catch
            {
                return null;
            }
        }
    }
}
