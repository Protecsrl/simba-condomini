using CAMS.Module.Classi;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using CAMS.Module.CAMSInvioMailCN;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAMS.Module.DBAudit.DC;
using CAMS.Module.DBTask.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Utils;
using CAMS.Module.ClassiMSDB;



namespace CAMS.Module.ClassiORADB
{
    public class InvioMailCNGetData : IDisposable
    {

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        CAMSInvioMailCN.Class1 cl;
        public InvioMailCNGetData()
        {
            cl = new Class1();
            Classi.SetVarSessione.OracleConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public DataTable GetParametriMail()
        {
            var dv = new DataView();
            var dt = new DataTable("Parametri");
            var msg = string.Empty;
            var iConnessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");

            try
            {
               //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", ""))
                using (var OrclConn = new OracleConnection(iConnessione))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getparametrimail";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
                    //dv = new DataView(dt);

                    msg = command.Parameters["oMessaggio"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            return dt;
        }
        #region   controlli periodici
        //GetDestinatari
        public DataTable GetEdifici(string UserNameCorrente, int UnoTutti, int OidEdificio, DateTime DataDA, DateTime DataA)
        {
            var dt = new DataTable("Edifici");
            var msg = string.Empty;
            var Connessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");
            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(Connessione))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getedifici";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("unotutti", OracleType.Number) { Value = UnoTutti };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("idatada", OracleType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idataa", OracleType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
                    //dv = new DataView(dt);

                    msg = command.Parameters["omessaggio"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            return dt;

            //OracleCommand selectCommand = new OracleCommand("pk_controlli_normativi_txt.getedifici", this.Cnn);
            //selectCommand.CommandType = CommandType.StoredProcedure;
            //DataTable dataTable = new DataTable();
            //OracleParameter parameter = new OracleParameter("p_username", OracleType.VarChar, 50);
            //parameter.Value = username;
            //selectCommand.Parameters.Add(parameter);
            //parameter = new OracleParameter("IO_CURSOR", OracleType.Cursor);
            //parameter.Direction = ParameterDirection.Output;
            //selectCommand.Parameters.Add(parameter);
            //new OracleDataAdapter(selectCommand).Fill(dataTable);
            //return dataTable;
        }

        //
        public DataTable GetDestinatari(string UserNameCorrente, int OidEdificio, DateTime DataDA, DateTime DataA)
        {
            var dt = new DataTable("GetDestinatari");
            var msg = string.Empty;
            var Connessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");


            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(Connessione))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getdestinatariedificio";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("idatada", OracleType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idataa", OracleType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
                    //dv = new DataView(dt);

                    msg = command.Parameters["omessaggio"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            return dt;

            //OracleCommand selectCommand = new OracleCommand("pk_controlli_normativi_txt.getedifici", this.Cnn);
            //selectCommand.CommandType = CommandType.StoredProcedure;
            //DataTable dataTable = new DataTable();
            //OracleParameter parameter = new OracleParameter("p_username", OracleType.VarChar, 50);
            //parameter.Value = username;
            //selectCommand.Parameters.Add(parameter);
            //parameter = new OracleParameter("IO_CURSOR", OracleType.Cursor);
            //parameter.Direction = ParameterDirection.Output;
            //selectCommand.Parameters.Add(parameter);
            //new OracleDataAdapter(selectCommand).Fill(dataTable);
            //return dataTable;
        }


        //
        public DataTable GetMessaggioEMailCN( string UserNameCorrente, int OidEdificio, DateTime DataDA, DateTime DataA)
        {
            var Connessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");
            var dt = new DataTable("Mail");
            var msg = string.Empty;
            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(Connessione))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getmessaggiomailcn";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idatada", OracleType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idataa", OracleType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
                    //dv = new DataView(dt);

                    msg = command.Parameters["omessaggio"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            return dt;

        }

        //
        public string LogMessaggioEMailCN(string UserNameCorrente, int OidEdificio, System.Net.Mail.MailMessage MessaggioMail, DateTime DataInvio, int Esito, DateTime DataDA, DateTime DataA)
        {

            var msg = string.Empty;
            StringBuilder Corpo = new StringBuilder("", 32000);
            Corpo.Append(MessaggioMail.Body.ToString());
            var Connessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");
            // Corpo.Append(MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString()); //GetStogona();
            int Intlogid = 0;
            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(Connessione))

                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.log_email_cn";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iMailDestinatari", OracleType.VarChar, 200) { Value = MessaggioMail.To.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("imailfrom", OracleType.VarChar, 200) { Value = MessaggioMail.From.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioggetto", OracleType.VarChar, 200) { Value = MessaggioMail.Subject.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("imailtocc", OracleType.VarChar, 200) { Value = PInv.MAILTOCC };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idata_invio", OracleType.VarChar, 200) { Value = DataInvio.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iEsito", OracleType.Number) { Value = Esito };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("idatada", OracleType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idataa", OracleType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("iemailBody", OracleType.Clob) { Value = Corpo.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;



                    pp = new OracleParameter("oidlog", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    string logid = command.Parameters["oidlog"].Value.ToString();

                    if (logid != null && int.TryParse(logid, out Intlogid))
                    {///   InsertCLOB(Classi.SetVarSessione.OracleConnString, Corpo, "CORPO", "LOGEMAILCTRLNORM", Intlogid);
                        msg = "Registrazione Email Avvenuta con Avvertimenti";
                    }
                    else
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            //try
            //   {
            //       InsertCLOB(Classi.SetVarSessione.OracleConnString, Corpo, "CORPO", "LOGEMAILCTRLNORM", Intlogid);
            //}
            //catch (Exception ex)
            //{
            //    msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
            //    cl.TxtLogSpedizioni(msg, true);
            //    throw new Exception(msg);
            //}
            return msg;

        }
        
        //public void InsertCLOB(string StringConnessione, StringBuilder Testolungo, string Campo, string Tabella, int Oid)
        //{
        //    // System.Data.OracleClient.OracleConnection connOracle;
        //    System.Data.OracleClient.OracleDataReader rstOracle;
        //    System.Data.OracleClient.OracleCommand sqlCommandOracle;
        //    System.Data.OracleClient.OracleTransaction txn;
        //    System.Data.OracleClient.OracleLob clob;
        //    string p_conn_db = StringConnessione;

        //    string SQLStatement = String.Format("select {0} from {1}  WHERE oid = {2} FOR UPDATE", Campo, Tabella, Oid);

        //    //connOracle = new System.Data.OracleClient.OracleConnection(p_conn_db);
        //    //using (var connOracle = new OracleConnection(Classi.SetVarSessione.OracleConnString))
        //    using (var connOracle = new OracleConnection(StringConnessione))
        //    {
        //        connOracle.Open();
        //        if (SQLStatement.Length > 0)
        //        {
        //            if (connOracle.State.ToString().Equals("Open"))
        //            {
        //                byte[] newvalue = System.Text.Encoding.Unicode.GetBytes(Testolungo.ToString());
        //                sqlCommandOracle = new System.Data.OracleClient.OracleCommand(SQLStatement, connOracle);
        //                rstOracle = sqlCommandOracle.ExecuteReader();
        //                rstOracle.Read();
        //                txn = connOracle.BeginTransaction();
        //                clob = rstOracle.GetOracleLob(0);
        //                clob.Write(newvalue, 0, newvalue.Length);
        //                txn.Commit();
        //            }
        //        }
        //    }
        //    // connOracle.Close();
        //}

        StringBuilder GetStogona()
        {
            StringBuilder pippo = new StringBuilder("", 32000);
            for (int i = 0; i < 1000; i++)
            {
                pippo.Append(" Prova di 1; \r \n");
            }
            return pippo;
        }
        #endregion


        #region procedure spedizione mail
        public DataTable GetDestinatariMail_byRdL(string Connessione, string CorrenteUser, int OidRegRdL)
        {
            string messaggio = string.Empty;
            var dt = new DataTable("GetDestinatariMail_byRdL");
            var iConnessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");
            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(iConnessione))
                {

                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestinatariMail_byRdL";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iUser", OracleType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidRegRdL", OracleType.Number) { Value = OidRegRdL };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("IO_CURSOR", OracleType.Cursor) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["IO_CURSOR"].Value;
                    dt.Load((System.Data.IDataReader)dr);

                    var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
                    if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return dt;


        }

        public System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> GetDestinatariSMSMail_byRdL
                                                      (string Connessione, string CorrenteUser, int OidRegRdL)
        //public DataTable GetDestinatariMail_byRdL(string Connessione, string CorrenteUser, int OidRegRdL)
        {
            System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objects =
                new System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            string messaggio = string.Empty;
            var iConnessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");

            try
            {
                using (
                     //var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString)
                     var OrclConn = new OracleConnection(iConnessione)
                    )
                {

                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestinatariMail_byRdL";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iUser", OracleType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidRegRdL", OracleType.Number) { Value = OidRegRdL };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("IO_CURSOR", OracleType.Cursor) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["IO_CURSOR"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    //var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
                    //if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    //{
                    //    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
                    //}               

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CAMS.Module.DBMail.DCDatiSMSMail obj = new CAMS.Module.DBMail.DCDatiSMSMail();
                            obj.Oid = Guid.NewGuid();

                            obj.AzioneSpedizione = GetIntValore("O_AZIONESPEDIZIONE", dr); // mail oppure sms oppure entranbi
                            obj.Body = GetStringValore("O_BODY", dr); //dr.GetString(dr.GetOrdinal("O_BODY"));
                            obj.TipoAzioneMail = GetStringValore("O_TIPOAZIONEMAIL", dr); //dr.GetString(dr.GetOrdinal("O_TIPOAZIONEMAIL"));
                            obj.IndirizzoSMS = GetStringValore("O_INDIRIZZO_SMS", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_SMS"));
                            obj.IndirizzoMail = GetStringValore("O_INDIRIZZO_MAIL", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_MAIL"));
                            obj.Subject = GetStringValore("O_SUBJECT", dr); //dr.GetString(dr.GetOrdinal("O_SUBJECT"));
                            obj.NomeCognome = GetStringValore("O_NOME_COGNOME", dr); // dr.GetString(dr.GetOrdinal("O_NOME_COGNOME"));

                            objects.Add(obj);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB, DESTINATARI ;" + ex.Message));
            }
            return objects;
        }

        public System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> GetDestinatariSMSMail_byRdLSoll
                                                     (string Connessione, string CorrenteUser, int OidRegRdL)
        //public DataTable GetDestinatariMail_byRdL(string Connessione, string CorrenteUser, int OidRegRdL)
        {
            System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objects =
                new System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            string messaggio = string.Empty;
            var iConnessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");
            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(iConnessione))
                {

                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestinatariMail_byRdLSoll";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iUser", OracleType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidRegRdL", OracleType.Number) { Value = OidRegRdL };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("IO_CURSOR", OracleType.Cursor) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["IO_CURSOR"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    //var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
                    //if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    //{
                    //    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
                    //}               

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CAMS.Module.DBMail.DCDatiSMSMail obj = new CAMS.Module.DBMail.DCDatiSMSMail();
                            obj.Oid = Guid.NewGuid();

                            obj.AzioneSpedizione = GetIntValore("O_AZIONESPEDIZIONE", dr); // dr.GetInt32(dr.GetOrdinal("O_AZIONESPEDIZIONE")); //dr.GetString(dr.GetOrdinal("CENTROOPERATIVO"));
                            obj.Body = GetStringValore("O_BODY", dr); //dr.GetString(dr.GetOrdinal("O_BODY"));
                            obj.TipoAzioneMail = GetStringValore("O_TIPOAZIONEMAIL", dr); //dr.GetString(dr.GetOrdinal("O_TIPOAZIONEMAIL"));
                            obj.IndirizzoSMS = GetStringValore("O_INDIRIZZO_SMS", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_SMS"));
                            obj.IndirizzoMail = GetStringValore("O_INDIRIZZO_MAIL", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_MAIL"));
                            obj.Subject = GetStringValore("O_SUBJECT", dr); //dr.GetString(dr.GetOrdinal("O_SUBJECT"));
                            obj.NomeCognome = GetStringValore("O_NOME_COGNOME", dr); // dr.GetString(dr.GetOrdinal("O_NOME_COGNOME"));

                            objects.Add(obj);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB, DESTINATARI ;" + ex.Message));
            }
            return objects;
        }



        public System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> GetDestinatariSMSMail_byTIPO
                                                   (string Connessione, string CorrenteUser, int OidObj, string Tipo)
        {
            System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objects =
                new System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            string messaggio = string.Empty;
            var iConnessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");

            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(iConnessione))
                {

                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestinatariMail_byTIPO";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iUser", OracleType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iTipo", OracleType.VarChar, 250) { Value = Tipo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidObj", OracleType.Number) { Value = OidObj };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("IO_CURSOR", OracleType.Cursor) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["IO_CURSOR"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    //var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
                    //if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    //{
                    //    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
                    //}               

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CAMS.Module.DBMail.DCDatiSMSMail obj = new CAMS.Module.DBMail.DCDatiSMSMail();
                            obj.Oid = Guid.NewGuid();

                            obj.AzioneSpedizione = GetIntValore("O_AZIONESPEDIZIONE", dr); // dr.GetInt32(dr.GetOrdinal("O_AZIONESPEDIZIONE")); //dr.GetString(dr.GetOrdinal("CENTROOPERATIVO"));
                            obj.Body = GetStringValore("O_BODY", dr); //dr.GetString(dr.GetOrdinal("O_BODY"));
                            obj.TipoAzioneMail = GetStringValore("O_TIPOAZIONEMAIL", dr); //dr.GetString(dr.GetOrdinal("O_TIPOAZIONEMAIL"));
                            obj.IndirizzoSMS = GetStringValore("O_INDIRIZZO_SMS", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_SMS"));
                            obj.IndirizzoMail = GetStringValore("O_INDIRIZZO_MAIL", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_MAIL"));
                            obj.Subject = GetStringValore("O_SUBJECT", dr); //dr.GetString(dr.GetOrdinal("O_SUBJECT"));
                            obj.NomeCognome = GetStringValore("O_NOME_COGNOME", dr); // dr.GetString(dr.GetOrdinal("O_NOME_COGNOME"));

                            objects.Add(obj);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB, DESTINATARI ;" + ex.Message));
            }
            return objects;
        }


        private string GetStringValore(string Campo, OracleDataReader dr)
        {
            string Valore = string.Empty;
            try
            {
                //Valore = dr.GetString(dr.GetOrdinal(Campo)); //dr.GetString(dr.GetOrdinal("O_NOME_COGNOME")); 		ObjVal.GetType().Name == "DBNull"	true	bool

                int indiceCampo = dr.GetOrdinal(Campo);
                var ObjVal = dr[indiceCampo];
                if (ObjVal != null && ObjVal.GetType().Name != "DBNull")
                    Valore = ObjVal.ToString();
                //	dr[2].ToString()	"Alessandro Taglioli"	string  +		ObjVal	{}	object {System.DBNull}


            }
            catch
            {
                Valore = string.Empty;
            }
            return Valore;
        }

        private int GetIntValore(string Campo, OracleDataReader dr)
        {
            int Valore = 0;
            try
            {
                Valore = dr.GetInt32(dr.GetOrdinal(Campo)); //dr.GetString(dr.GetOrdinal("O_NOME_COGNOME"));   
            }
            catch
            {
                Valore = 0;
            }
            return Valore;
        }



        public string InsertLog_DestinatariMail_byRdL(           
            string Utente, int OidRegRdL, string emailFrom, string emailTo, DateTime emailDate,
                                                       string emailSubject, string emailBody, string emailEsitoDescrizione, EsitoInvioMailSMS EsitoSpedizione
            , int vTipoInvio, string vSMSID, string vDestinaSMS)
        {
            string messaggio = string.Empty;
            var iConnessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");
            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(iConnessione))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.InsLog_DestinatariMail_byRdL";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iUser", OracleType.VarChar, 255) { Value = Utente };
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("iOidRegRdL", OracleType.VarChar, 100) { Value = OidRegRdL };
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("iemailFrom", OracleType.VarChar, 255) { Value = emailFrom };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailCC", OracleType.VarChar, 4000) { Value = "" };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailTo", OracleType.VarChar, 4000) { Value = emailTo };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailDate", OracleType.DateTime) { Value = emailDate };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailSubject", OracleType.VarChar, 500) { Value = emailSubject };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailBody", OracleType.Clob) { Value = emailBody };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailEsitoDescr", OracleType.VarChar, 300) { Value = emailEsitoDescrizione };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailEsito", OracleType.Int32) { Value = EsitoSpedizione };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iTipoInvio", OracleType.Int32) { Value = vTipoInvio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ismsid", OracleType.VarChar, 300) { Value = vSMSID };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iDestinaSMS", OracleType.VarChar, 4000) { Value = vDestinaSMS };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
                    if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return messaggio;
        }


        public DataTable GetRegRdLdaSpedireMail( string CorrenteUser, string searchText) //string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            var iConnessione = Classi.SetVarSessione.OracleConnString.Replace("XpoProvider=Oracle;", "");
            try
            {
                //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                using (var OrclConn = new OracleConnection(iConnessione))
                {
                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetRegRdLdaSpedireMail";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iuser", OracleType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("isearchtext", OracleType.VarChar, 4000) { Value = searchText };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oerrormsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("io_cursor", OracleType.Cursor) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
                    dv = new DataView(dt);
                    var Messaggi = command.Parameters["oerrormsg"].Value.ToString();
                    if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return dt;


        }

        #endregion


    }
}
