using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CreateADUser
{
    class Program
    {
        /* REQUIRED ARGUMENTS in order
         * 
         * action
         * domain - mbcscentral 
         * server - MbcsCentralDC1.mbcscentral.com  
         * givenname - first name
         * surname  - last name
         * accountname
         * password
         * description   same as client i.e. fishman.  There must be a client group with the same name
         * 
         * fullname will be created by the program
         */

        static private System.Diagnostics.Process exProc;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments were passed.");
                return;
            }
            switch (args[0].ToLower())
            {
                case "cr":
                    AddUser(args);
                    break;
                case "en": // Change existing user login
                    ChangeLogin(args);
                    break;
                case "ep": // Change existing user password
                    ChangePassword(args);
                    break;
                case "de": // Delete user
                    DeleteUser(args);
                    break;  
                default:
                    Console.WriteLine("Invalid action code passed: " + args[0]);
                    break;
            }
        }

        /// <summary>
        /// Add a new Active Directory user
        /// </summary>
        /// <param name="args">0 = cr</param>
        ///                    1 = domain
        ///                    2 = server (FQN)
        ///                    3 = given name
        ///                    4 = surname
        ///                    5 = account name (login)
        ///                    6 = password
        ///                    7 = description (client name)
        private static void AddUser(string[] args)
        {
            if (args.Length < 7)
            {
                Console.WriteLine("Create User powershell script: Invalid number of arguments.  Must be 8");
                return;
            }


            string script = " -noexit .\\AddADUser.ps1 " +
                            " -domain " + args[1] +
                            " -server " + args[2] +
                            " -givenname " + args[3] +
                            " -surname " + args[4] +
                            " -accountname " + args[5] +
                            " -password " + args[6] +
                            " -description " + args[7] +
                            " -fullname '" + args[3].Trim() + " " + args[4] + "'";

            /*
            Assembly _assembly;
            StreamReader _textStreamReader;
            try
            {
                _assembly = Assembly.GetExecutingAssembly();
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("CreateADUser.CreateNewUser.ps1"));
            }
            catch
            {
                return;
            }
            string script = " -noexit " +
                            " $domain='" + args[0] + "';\n" +
                            " $server='" + args[1] + "';\n" +
                            " $givenname='" + args[2] + "';\n" +
                            " $surname='" + args[3] + "';\n" +
                            " $accountname='" + args[4] + "';\n" +
                            " $password='" + args[5] + "';\n" +
                            " $description='" + args[6] + "';\n" + 
                            " $fullname='" + args[2].Trim() + " " + args[3] + "';\n";
            try
            {
                while (_textStreamReader.Peek() != -1)
                {
                    script += "'" + _textStreamReader.ReadLine() + ";'\n";
                }
                    _textStreamReader.Close();
            }
            catch
            {
                return;
            }
             */
            ExecuteScript(script);
        }

        private static void ExecuteScript(string script)
        {
            //using (PowerShell ps = PowerShell.Create())
            //{
            //    ps.AddScript(script);
            //    ps.Invoke();
            //}
            
            exProc = new System.Diagnostics.Process();
            exProc.StartInfo.FileName = "powershell.exe";
            exProc.StartInfo.Arguments = script;
            exProc.EnableRaisingEvents = true;
            exProc.Exited += new System.EventHandler(Result);
            try
            {
                exProc.Start();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Create User powershell script error: " + ex.Message);
            }
            
        }

        /// <summary>
        /// Change AD User account name
        /// </summary>
        /// <param name="args">0 = cn
        ///                    1 = current account name
        ///                    2 = new account name</param>

        private static void ChangeLogin(string[] args)
        {
            string script = "Set-ADUser -Identity " + args[1] +
                            " -Replace @{SamAccountname='" + args[2] + "'}";
            ExecuteScript(script);
        }

        /// <summary>
        /// Change Active Directory User password
        /// </summary>
        /// <param name="args">0 = cp
        ///                    1 = account name
        ///                    2 = old password
        ///                    3 = new password
        /// </param>
        private static void ChangePassword(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Change user password:  Too few arguments.  Must be 4");
                return;
            }
            string script = "Set-ADAccountPassword -Identity " +
                            args[1] +
                            " -OldPassword (ConvertTo-SecureString -AsPlainText " + args[2] + " -Force)" +
                            " -NewPassword (ConvertTo-SecureString -AsPlainText " + args[3] + " -Force)";
            ExecuteScript(script);
        }

        /// <summary>
        /// Delete Active Directory user
        /// </summary>
        /// <param name="args">0 = de
        ///                    1 = account name</param>
        private static void DeleteUser(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Delete user: Too few argument.  Must be 2");
                return;
            }
            string script = "Remove-ADUser -Identity " + args[1];
            ExecuteScript(script);
        }
       internal static void Result(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process proc = (System.Diagnostics.Process)sender;
            Console.Write("Create User powerscript exited with code= ");
            Console.WriteLine(proc.ExitCode);
        }
    }
}
