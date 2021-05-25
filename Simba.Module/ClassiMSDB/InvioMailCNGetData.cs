using CAMS.Module.Classi;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CAMS.Module.ClassiMSDB
{
    public class InvioMailCNGetData : IDisposable
    {

        public InvioMailCNGetData()
        {
            Classi.SetVarSessione.OracleConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }



        private string ConnStr;
        CAMSInvioMailCN.Class1 cl;
        public InvioMailCNGetData(string s_ConnStr)
        {
            this.cl = new CAMSInvioMailCN.Class1();
            this.ConnStr = s_ConnStr;
        }

        public DataTable GetParametriMail()
        {
            var dv = new DataView();
            var dt = new DataTable("Prametri");
            var msg = string.Empty;
            try
            {                
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getparametrimail";
                    command.CommandType = CommandType.StoredProcedure;

                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    SqlParameter pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    //command.ExecuteNonQuery();

                    //var dr = (SqlDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);

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
            try
            {
                using (var OrclConn = new SqlConnection(this.ConnStr))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getedifici";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("unotutti", SqlDbType.Int) { Value = UnoTutti };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("idatada", SqlDbType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("idataa", SqlDbType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;

                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);
                    //pp = null;
                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    //command.ExecuteNonQuery();

                    //var dr = (SqlDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);


                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);

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

            //SqlCommand selectCommand = new SqlCommand("pk_controlli_normativi_txt.getedifici", this.Cnn);
            //selectCommand.CommandType = CommandType.StoredProcedure;
            //DataTable dataTable = new DataTable();
            //SqlParameter parameter = new SqlParameter("p_username", SqlDbType.VarChar, 50);
            //parameter.Value = username;
            //selectCommand.Parameters.Add(parameter);
            //parameter = new SqlParameter("IO_CURSOR", SqlDbType.Cursor);
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
            try
            {
                using (var OrclConn = new SqlConnection(this.ConnStr))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getdestinatariedificio";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("idatada", SqlDbType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("idataa", SqlDbType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;

                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);
                    //pp = null;
                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    //command.ExecuteNonQuery();

                    //var dr = (SqlDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
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

            //SqlCommand selectCommand = new SqlCommand("pk_controlli_normativi_txt.getedifici", this.Cnn);
            //selectCommand.CommandType = CommandType.StoredProcedure;
            //DataTable dataTable = new DataTable();
            //SqlParameter parameter = new SqlParameter("p_username", SqlDbType.VarChar, 50);
            //parameter.Value = username;
            //selectCommand.Parameters.Add(parameter);
            //parameter = new SqlParameter("IO_CURSOR", SqlDbType.Cursor);
            //parameter.Direction = ParameterDirection.Output;
            //selectCommand.Parameters.Add(parameter);
            //new OracleDataAdapter(selectCommand).Fill(dataTable);
            //return dataTable;
        }


        //
        public DataTable GetMessaggioEMailCN(string UserNameCorrente, int OidEdificio, DateTime DataDA, DateTime DataA)
        {
            var dt = new DataTable("Mail");
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(this.ConnStr))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.getmessaggiomailcn";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("idatada", SqlDbType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("idataa", SqlDbType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;

                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);
                    //pp = null;

                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    //command.ExecuteNonQuery();

                    //var dr = (SqlDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
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
            // Corpo.Append(MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString() + MessaggioMail.Body.ToString()); //GetStogona();
            int Intlogid = 0;
            try
            {
                using (var OrclConn = new SqlConnection(this.ConnStr))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.log_email_cn";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iMailDestinatari", SqlDbType.VarChar, 200) { Value = MessaggioMail.To.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("imailfrom", SqlDbType.VarChar, 200) { Value = MessaggioMail.From.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("ioggetto", SqlDbType.VarChar, 200) { Value = MessaggioMail.Subject.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("imailtocc", SqlDbType.VarChar, 200) { Value = MessaggioMail.CC };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("idata_invio", SqlDbType.VarChar, 200) { Value = DataInvio.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iEsito", SqlDbType.Int) { Value = Esito };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("idatada", SqlDbType.DateTime) { Value = DataDA };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("idataa", SqlDbType.DateTime) { Value = DataA };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new SqlParameter("iemailBody", SqlDbType.VarChar, 4000) { Value = Corpo.ToString() };
                    command.Parameters.Add(pp);
                    pp = null;



                    pp = new SqlParameter("oidlog", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    string logid = command.Parameters["oidlog"].Value.ToString();

                    if (logid != null && int.TryParse(logid, out Intlogid))
                    {///   InsertCLOB(this.ConnStr, Corpo, "CORPO", "LOGEMAILCTRLNORM", Intlogid);
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
            //       InsertCLOB(this.ConnStr, Corpo, "CORPO", "LOGEMAILCTRLNORM", Intlogid);
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
        //    // SqlConnection connOracle;
        //    SqlDataReader rstOracle;
        //    SqlCommand sqlCommandOracle;
        //    SqlTransaction txn;
        //    // OracleLob clob;
        //    string p_conn_db = StringConnessione;

        //    string SQLStatement = String.Format("select {0} from {1}  WHERE oid = {2} FOR UPDATE", Campo, Tabella, Oid);

        //    //connOracle = new SqlConnection(p_conn_db);
        //    using (var connOracle = new SqlConnection(this.ConnStr))
        //    {
        //        connOracle.Open();
        //        if (SQLStatement.Length > 0)
        //        {
        //            if (connOracle.State.ToString().Equals("Open"))
        //            {
        //                byte[] newvalue = System.Text.Encoding.Unicode.GetBytes(Testolungo.ToString());
        //                sqlCommandOracle = new SqlCommand(SQLStatement, connOracle);
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

        //StringBuilder GetStogona()
        //{
        //    StringBuilder pippo = new StringBuilder("", 32000);
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        pippo.Append(" Prova di 1; \r \n");
        //    }
        //    return pippo;
        //}
        #endregion


        #region procedure spedizione mail
        //public DataTable GetDestinatariMail_byRdL(string Connessione, string CorrenteUser, int OidRegRdL)
        //{
        //    string messaggio = string.Empty;

        //    var dt = new DataTable("GetDestinatariMail_byRdL");
        //    try
        //    {
        //        using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {

        //            var command = SqlConn.CreateCommand();
        //            command.CommandText = "pk_mail.GetDestinatariMail_byRdL";
        //            command.CommandType = CommandType.StoredProcedure;

        //            var pp = new SqlParameter("iUser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
        //            command.Parameters.Add(pp);


        //            pp = new SqlParameter("iOidRegRdL", SqlDbType.Int) { Value = OidRegRdL };
        //            command.Parameters.Add(pp);


        //            pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            SqlConn.Open();

        //            var dr = command.ExecuteReader();


        //            dt.Load((System.Data.IDataReader)dr);

        //            var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
        //            if (Messaggi != null && !Messaggi.Equals(string.Empty))
        //            {
        //                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //    return dt;


        //}

        public System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> GetDestinatariSMSMail_byRdL
                                                      (string Connessione, string CorrenteUser, int OidRegRdL)
        //public DataTable GetDestinatariMail_byRdL(string Connessione, string CorrenteUser, int OidRegRdL)
        {
            System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objects =
                new System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            string messaggio = string.Empty;

            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    //var command = SqlConn.CreateCommand();
                    SqlCommand command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestinatariMail_byRdL";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iUser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iOidRegRdL", SqlDbType.Int) { Value = OidRegRdL };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();

                    var dr = command.ExecuteReader();


                    if (dr.HasRows)
                    {
                        //int Newid = 0;
                        while (dr.Read())
                        {
                            CAMS.Module.DBMail.DCDatiSMSMail obj = new CAMS.Module.DBMail.DCDatiSMSMail();
                            obj.Oid = Guid.NewGuid();
                            obj.AzioneSpedizione = GetIntValoreSql("O_AZIONESPEDIZIONE", dr); // mail oppure sms oppure entranbi
                            obj.Body = GetStringValoreSql("O_BODY", dr); //dr.GetString(dr.GetOrdinal("O_BODY"));
                            obj.TipoAzioneMail = GetStringValoreSql("O_TIPOAZIONEMAIL", dr); //dr.GetString(dr.GetOrdinal("O_TIPOAZIONEMAIL"));
                            obj.IndirizzoSMS = GetStringValoreSql("O_INDIRIZZO_SMS", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_SMS"));
                            obj.IndirizzoMail = GetStringValoreSql("O_INDIRIZZO_MAIL", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_MAIL"));
                            obj.Subject = GetStringValoreSql("O_SUBJECT", dr); //dr.GetString(dr.GetOrdinal("O_SUBJECT"));
                            obj.NomeCognome = GetStringValoreSql("O_NOME_COGNOME", dr); // dr.GetString(dr.GetOrdinal("O_NOME_COGNOME"));

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

            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {

                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestinatariMail_byRdLSoll";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iUser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iOidRegRdL", SqlDbType.Int) { Value = OidRegRdL };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();

                    var dr = command.ExecuteReader();


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CAMS.Module.DBMail.DCDatiSMSMail obj = new CAMS.Module.DBMail.DCDatiSMSMail();
                            obj.Oid = Guid.NewGuid();

                            obj.AzioneSpedizione = GetIntValoreSql("O_AZIONESPEDIZIONE", dr); // dr.GetInt32(dr.GetOrdinal("O_AZIONESPEDIZIONE")); //dr.GetString(dr.GetOrdinal("CENTROOPERATIVO"));
                            obj.Body = GetStringValoreSql("O_BODY", dr); //dr.GetString(dr.GetOrdinal("O_BODY"));
                            obj.TipoAzioneMail = GetStringValoreSql("O_TIPOAZIONEMAIL", dr); //dr.GetString(dr.GetOrdinal("O_TIPOAZIONEMAIL"));
                            obj.IndirizzoSMS = GetStringValoreSql("O_INDIRIZZO_SMS", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_SMS"));
                            obj.IndirizzoMail = GetStringValoreSql("O_INDIRIZZO_MAIL", dr); //dr.GetString(dr.GetOrdinal("O_INDIRIZZO_MAIL"));
                            obj.Subject = GetStringValoreSql("O_SUBJECT", dr); //dr.GetString(dr.GetOrdinal("O_SUBJECT"));
                            obj.NomeCognome = GetStringValoreSql("O_NOME_COGNOME", dr); // dr.GetString(dr.GetOrdinal("O_NOME_COGNOME"));

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


        /// <summary>
        /// RIEFERIMENTO NON UTILIZZATO
        /// </summary>
        public System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> GetDestinatariSMSMail_byTIPO
                                                   (string Connessione, string CorrenteUser, int OidObj, string Tipo)
        {
            System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail> objects =
                new System.ComponentModel.BindingList<CAMS.Module.DBMail.DCDatiSMSMail>();
            string messaggio = string.Empty;

            try
            {
                using (var OrclConn = new SqlConnection(this.ConnStr))
                {

                    SqlCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestinatariMail_byTIPO";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iUser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iTipo", SqlDbType.VarChar, 250) { Value = Tipo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iOidObj", SqlDbType.Int) { Value = OidObj };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;

                    //pp = new SqlParameter("IO_CURSOR", SqlDbType.Cursor) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);

                    OrclConn.Open();


                    var dr = command.ExecuteReader();
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


        private string GetStringValore(string Campo, SqlDataReader dr)
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

        private string GetStringValoreSql(string Campo, SqlDataReader dr)
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


        private int GetIntValore(string Campo, SqlDataReader dr)
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
        private int GetIntValoreSql(string Campo, SqlDataReader dr)
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



        public string InsertLog_DestinatariMail_byRdL(string Utente, int OidRegRdL, string emailFrom, string emailTo, DateTime emailDate,
                                                       string emailSubject, string emailBody, string emailEsitoDescrizione, EsitoInvioMailSMS EsitoSpedizione
            , int vTipoInvio, string vSMSID, string vDestinaSMS)
        {
            string messaggio = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mail.InsLog_DestinatariMail_byRdL";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iUser", SqlDbType.VarChar, 255) { Value = Utente };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iOidRegRdL", SqlDbType.VarChar, 100) { Value = OidRegRdL };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailFrom", SqlDbType.VarChar, 255) { Value = emailFrom };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailCC", SqlDbType.VarChar, 4000) { Value = "" };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailTo", SqlDbType.VarChar, 4000) { Value = emailTo };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailDate", SqlDbType.DateTime) { Value = emailDate };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailSubject", SqlDbType.VarChar, 500) { Value = emailSubject };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailBody", SqlDbType.VarChar) { Value = emailBody };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailEsitoDescr", SqlDbType.VarChar, 300) { Value = emailEsitoDescrizione };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iemailEsito", SqlDbType.Int) { Value = EsitoSpedizione };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iTipoInvio", SqlDbType.Int) { Value = vTipoInvio };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("ismsid", SqlDbType.VarChar, 300) { Value = vSMSID };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iDestinaSMS", SqlDbType.VarChar, 4000) { Value = vDestinaSMS };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();
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

        /// <summary>
        /// RIFERIMENTO NON UTILIZZATO
        /// </summary>
        public DataTable GetRegRdLdaSpedireMail(string CorrenteUser, string searchText) //string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    SqlCommand command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mail.GetRegRdLdaSpedireMail";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iuser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("isearchtext", SqlDbType.VarChar, 4000) { Value = searchText };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("oerrormsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();
                    var dr = command.ExecuteReader();

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

        //FINE NUOVO CODICE








       


    }
}
