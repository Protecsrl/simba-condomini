using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CAMS.Module.CAMSInvioMailCN
{


    public sealed class PInv
    {

        public static string CORPO { get; set; }
        public static string SUBJECT { get; set; } // nome dell'tente dellapplicativo

        public static string PATHFILESTORE { get; set; }// password di sopra
        public static string PATHFILEPMP { get; set; }// descrizione della connessione
        public static string USERNAME { get; set; }    // USERNAME LEGATO AI RUOLI PER DEFINIRE GLI EDIFICI DI SCHEDULAZIONE
        public static string PWDSMTP { get; set; }
        public static string USERSMTP { get; set; }// password di sopra
        public static string SMTPSERVER { get; set; }// utente per oracle
        public static int SMTPSERVERPOSTA { get; set; }// password di sopra
        public static string MAILTOCC { get; set; }// pregetto della connessione orcl

        public static string MAILTOAdmin { get; set; }// pregetto della connessione orcl
        public static string MAILFROM { get; set; }// stringa di connesione 
        public static string PROGETTO { get; set; }// identificativo della connessione
        /// <summary>
        /// /da qui variabili gobali usate
        /// </summary>
        public static string ErroriDaRegistrare { get; set; } // errori
        public static string ErroriEdificiNoInvio { get; set; } // errori
        public static string ErroriFileAllegatoNoTrovati { get; set; } // errori
        public static string ErrorMailPrecedente { get; set; } // errori

        public static string InvioConErrori { get; set; } // errori
        /// id di sessione     
        public static string IDSessione { get; set; } // sessione
        public static int InvAnno { get; set; } // sessione
        public static int InvMese { get; set; } // sessione
        public static int InvGiorno { get; set; } // sessione
        public static string InvMeseNome { get; set; } // sessione
        public static string FileLogText { get; set; } // sessione
        // report
        public static int TotEdifici { get; set; }
        // email
        public static string FullPathFileSaveMail { get; set; } // errori
        public static string FullPathMail { get; set; } // errori
        public static string NomeFileSessione { get; set; } // errori
    }

    public enum AzioneEdifici
    {
        Unico,
        Tutti
    }

    public class Class1
    {
        public  void TxtLogSpedizioni(string Testo, bool acapo)
        {
            // The using statement also closes the StreamWriter.
            if (File.Exists(PInv.FileLogText))
            {
                using (StreamWriter sw = File.AppendText(PInv.FileLogText))
                {
                    if (acapo)
                    {
                        sw.WriteLine(Testo);
                    }
                    else
                    {
                        sw.Write(Testo);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(PInv.FileLogText))
                {
                    if (acapo)
                    {
                        sw.WriteLine(Testo);
                    }
                    else
                    {
                        sw.Write(Testo);
                    }
                }
            }
        }


        public  string LeggiTxtLogSpedizioni()
        {
          //  PInv.FileLogText = @"C:\AssemblaPRT\CAMSInvioMailCN\LanciaCAMSInvioMailCN\bin\Debug\FileLog\20-08-2015-10-51-27.txt";
            String line = "";
            if (File.Exists(PInv.FileLogText))
            {
                
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(PInv.FileLogText))
                    {
                        // Read the stream to a string, and write the string to the console.
                          line = sr.ReadToEnd();
                        Console.WriteLine(line);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                } 
            }
            return line;
        }


        //public string GetConnessioneStringaOracle(string ConnessioneBase)
        //{
        //     if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
        //        {
        //            var WebConfigConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        //            WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //            CAMS.Module.Classi.SetVarSessione.ConfigConnString = WebConfigConnectionString;
        //            CAMS.Module.Classi.SetVarSessione.OracleConnString = GetConnessioneStringaOracle(WebConfigConnectionString);
        //        }
        //    var StrConn = string.Empty;
        //    var datasource = "Data Source=";
        //    if (ConnessioneBase.Contains("XpoProvider=Oracle;Data Source="))
        //    {
        //        datasource = "XpoProvider=Oracle;Data Source=";
        //    }
        //    var stringSeparators = new string[] { datasource };
        //    var ReturnLong = ConnessioneBase.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        //    var ReturnCorto = ReturnLong[0].Split(new Char[] { ';' });
        //    var DataSorce = ReturnCorto[0].ToString().Trim();

        //    var stringSeparators1 = new string[] { "User ID=" };
        //    var ReturnLong1 = ConnessioneBase.Split(stringSeparators1, StringSplitOptions.RemoveEmptyEntries);
        //    var ReturnCorto1 = ReturnLong1[1].Split(new Char[] { ';' });
        //    var User = ReturnCorto1[0].ToString().Trim();

        //    var stringSeparators2 = new string[] { "Password=" };
        //    var ReturnLong2 = ConnessioneBase.Split(stringSeparators2, StringSplitOptions.RemoveEmptyEntries);
        //    var ReturnCorto2 = ReturnLong2[1].Split(new Char[] { ';' });
        //    var pass = ReturnCorto2[0].ToString().Trim();

        //    StrConn = String.Format("Data Source={0};User Id={1};Password={2};", DataSorce, User, pass);

        //    return StrConn;
        //}


    }
}
