using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAMS.Module.DBAudit.DC;
using CAMS.Module.DBTask.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Utils;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using System.Text;
#pragma warning restore CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using CAMS.Module.Classi;

//namespace CAMS.Module.ClassiMSDB
//{
//    class DB
//    {
//    }
//}
namespace CAMS.Module.ClassiMSDB
{
    public class DB : IDisposable
    {

        public DB()
        {
            Classi.SetVarSessione.OracleConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        //public string GetRuoloCorrente(string UserNameCorrente)
        //{
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_ruolouser.getapparencerule";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iUserNameCorrente", SqlDbType.VarChar, 100);
        //            pp.Value = UserNameCorrente;
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oRuolo", SqlDbType.VarChar, 100);
        //            SqlDbType.
        //           pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);
        //            pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000);
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);
        //            command.ExecuteNonQuery();
        //            var Ruolo = command.Parameters["oRuolo"].Value.ToString();
        //            command.Parameters["oErrorMsg"].Value.ToString();
        //            return Ruolo;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //}

        //public int GetMasionebySkillBase(string UserNameCorrente, int OidSkillBase)
        //{
        //    var OidManisone = 0;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_pmp_util.get_mansione";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iUserNameCorrente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iOidSkill", SqlDbType.VarChar, 15) { Value = OidSkillBase };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oOidMansione", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };


        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();
        //            OidManisone = int.Parse(command.Parameters["oOidMansione"].Value.ToString());
        //            var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
        //            if (Messaggi != null && !Messaggi.Equals(string.Empty))
        //            {
        //                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
        //            }

        //            //for (int i = 0; i < 3; i++)
        //            //{
        //            //    (command.Parameters["parametrocollection"].Value as Array).GetValue(i);
        //            //}

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //    return OidManisone;
        //}

        //public double GetDefault(string UserNameCorrente, string kTable, int OidEqstd)
        //{
        //    double Codk = 1;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_pmp_util.getkdefault";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iUserNameCorrente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iktable", SqlDbType.VarChar) { Value = kTable };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("ioideqstd", SqlDbType.Int) { Value = OidEqstd };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("odefault", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();
        //            Codk = double.Parse(command.Parameters["odefault"].Value.ToString());
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
        //    return Codk;
        //}

        //public string GetCodicePMP(string UserNameCorrente, int OidCategoria, int OidEqstd, int OidSistema)
        //{
        //    var CodPMP = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_pmp_util.get_cod_pmp";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iUserNameCorrente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("ioidcategoria", SqlDbType.Int) { Value = OidCategoria };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("ioideqstd", SqlDbType.Int) { Value = OidEqstd };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("ioidsistema", SqlDbType.Int) { Value = OidSistema };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("ocodpmp", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();
        //            CodPMP = command.Parameters["ocodpmp"].Value.ToString();
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
        //    return CodPMP;
        //}


        //procedure AggiornaScenarioinEdImpAppSk(iRegPiano   in number,    iAzione     in varchar2,
        //                                 iUtente     in varchar2,      iDataUpdate in date,    oMessaggio  out varchar2) is
        public string setScenarioinEdificioImpiantoApparatoAppSk(int RegPiano, string Azione)
        {
            var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_aggiorna_sk.aggiornascenarioinedimpappsk", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iRegPiano", SqlDbType.Int) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iazione", SqlDbType.VarChar, 200) { Value = Azione };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iUtente", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iDataUpdate", SqlDbType.DateTime) { Value = DateTime.Now };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["oMessaggio"].Value.ToString();
                }
                return Messaggio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggio;
        }

        //public string GetMessaggioRegistroSchedulazioneVuoto(int RegPiano)
        //{
        //    var Messaggio = "Non Vedi?.";
        //    string UserNameCorrente = SetVarSessione.CorrenteUser;

        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_aggiorna_sk.check_scenario", OrclConn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            var pp = new SqlParameter("impregpianificazione", SqlDbType.Int) { Value = RegPiano };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;
        //            //pp = new SqlParameter("oBool", SqlDbType.Int32) { Direction = ParameterDirection.Output };
        //            //cmd.Parameters.Add(pp);
        //            //pp = null;
        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Messaggio = cmd.Parameters["oMessage"].Value.ToString();
        //        }
        //        return Messaggio;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Messaggio;
        //}
        /// <summary>
        /// RIFERIMENTO SEMBRA NON UTILIZZATO
        /// </summary>
        /// <param name="Scenario"></param>
        /// <returns></returns>
        public int GetScenarioInseribilesuRegistroPiano(int Scenario)
        {
            string Messaggio = string.Empty;
            int OutNumber = 0;

            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_aggiorna_sk.check_scenario_ins_mpreg", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iscenario", SqlDbType.Int) { Value = Scenario };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("outnumber", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();


                    OutNumber = int.Parse(cmd.Parameters["outnumber"].Value.ToString());

                    Messaggio = cmd.Parameters["oMessage"].Value.ToString();


                }
                return OutNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return OutNumber;
        }


        public string SetParContatoreAppMP(int OidDataContatore)
        {
            //  pk_mpplanner.setparcontatoreappmp(idatacontatoreoid => :idatacontatoreoid,omessaggio => :omessaggio);
            var Messaggio = "Non Vedi?.";
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.setparcontatoreappmp", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new SqlParameter("idatacontatoreoid", SqlDbType.Int) { Value = OidDataContatore };
                    cmd.Parameters.Add(pp);
                    pp = null;


                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["omessaggio"].Value.ToString();
                }
                return Messaggio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggio;
        }


        public string CreaRegrdlOdL(int iOidRdl, int iCreaRegRdL=0, int iCreaOdL=0 )
        {
           
        var Messaggio = "0;0";
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("PK_REGRDL.CreaRegRdLbyRdL", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new SqlParameter("iOidRdl", SqlDbType.Int) { Value = iOidRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iCreaRegRdL", SqlDbType.Int) { Value = iCreaRegRdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iCreaOdL", SqlDbType.Int) { Value = iCreaOdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["oMessaggio"].Value.ToString();
                }
                return Messaggio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
           
        }



        //pk_mpplanner.aggiornaletturecontatore(idcontatoreoid => :idcontatoreoid,
        //                              idata => :idata,
        //                              ivalore => :ivalore,
        //                              ioresettimana => :ioresettimana,
        //                              omessaggio => :omessaggio);



        public string AggiornaLettureContatore(int OidDataContatore, DateTime Data, int Valore, int OreSettimana)
        {
            var Messaggio = "Non Vedi?.";
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.aggiornaletturecontatore", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("idcontatoreoid", SqlDbType.Int) { Value = OidDataContatore };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("idata", SqlDbType.DateTime) { Value = Data };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("ivalore", SqlDbType.Int) { Value = Valore };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("ioresettimana", SqlDbType.Int) { Value = OreSettimana };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["omessaggio"].Value.ToString();
                }
                return Messaggio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggio;
        }

        public bool GetVerificaEsitoAzione(int RegPiano, int Stato, string AzioneChiamante) ///string TipoAzione,
        {
            var Verifica = false;
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.VerificaEsitoAzione", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new SqlParameter("iRegPiano", SqlDbType.Int) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iazione", SqlDbType.VarChar, 200) { Value = Stato.ToString() };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iazionechiamante", SqlDbType.VarChar, 200) { Value = AzioneChiamante };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oVerifica", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    var Esito = cmd.Parameters["oVerifica"].Value.ToString();
                    if (Esito == "1")
                    {
                        Verifica = true;
                    }
                }
                return Verifica;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Verifica;
        }

        public string EseguiAzione(int RegPiano, string Azione, string UserNameCorrente)
        {
            var Messaggio = "Non Vedi?.";
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.eseguiazione", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new SqlParameter("iRegPiano", SqlDbType.Int) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iazione", SqlDbType.VarChar, 200) { Value = Azione };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iUtente", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("omessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["omessage"].Value.ToString();
                }
                return Messaggio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggio;
        }

        public bool[] GetVisibileTastiAzione(int RegPiano, int Stato) ///string TipoAzione,
        {
            var Ver = new bool[3] { true, true, true };

            var Verifica = "111";
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.GetVisibileTastiAzione", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new SqlParameter("iRegPiano", SqlDbType.Int) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iStato", SqlDbType.VarChar, 200) { Value = Stato.ToString() };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oVerifica", SqlDbType.VarChar, 5) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Verifica = cmd.Parameters["oVerifica"].Value.ToString();
                    if (!Verifica.Contains("111"))
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            Ver[i] = Verifica.Substring(i, 1) == "1" ? true : false;
                        }
                    }
                }
                return Ver;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Ver;
        }

        public string aggiornaRegMPSchedeMP(int RegPiano)
        {
            var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("PK_MPPLANNER.aggiornaRegMPSchedeMP", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("oidmpregpianificazione", SqlDbType.Int) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    //pp = new SqlParameter("oBool", SqlDbType.Int32) { Direction = ParameterDirection.Output };
                    //cmd.Parameters.Add(pp);
                    //pp = null;
                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["oMessage"].Value.ToString();
                }
                return Messaggio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggio;
        }


        //pk_aggiorna_kpi.lancioupdkpimtbf(datein => :datein,
        //                         dateout => :dateout,
        //                         p_msg_out => :p_msg_out);
        //public string CalcolaKPIMTBF(DateTime DataIn, DateTime DataOut)
        //{
        //    var Messaggio = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_aggiorna_kpi.lancioupdkpimtbf", OrclConn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            SqlParameter pp = new SqlParameter("datein", SqlDbType.DateTime) { Value = DataIn };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("dateout", SqlDbType.DateTime) { Value = DataOut };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("p_msg_out", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Messaggio = cmd.Parameters["p_msg_out"].Value.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Messaggio;
        //}


        //public string GetRisorseLiberedaAssociare(int iRegPiano, string UserNameCorrente)
        //{
        //    var Criterio = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.getrisorsedaassociare", OrclConn);
        //            cmd.CommandType = CommandType.StoredProcedure;


        //            var pp = new SqlParameter("iRegPiano", SqlDbType.Int) { Value = iRegPiano };
        //            cmd.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iUtente", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oCriteria", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Criterio = cmd.Parameters["ocriteria"].Value.ToString();
        //        }
        //        return Criterio;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Criterio;
        //}


        public System.Data.DataTable GetTRisorseLiberedaAssociareByMansione(int iGhostID)
        {
            var Criterio = string.Empty;
            DataTable dtable = new DataTable("Risorse");
            DataView dvrow = new DataView();

            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpasscaricocapacita.GetTRisorsedaAssociareManSki", OrclConn) { CommandType = CommandType.StoredProcedure };

                    SqlParameter pp = new SqlParameter("iGhost", SqlDbType.Int) { Value = iGhostID };
                    Guard.ArgumentNotNull(pp, "pp");
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add(pp);

                    OrclConn.Open();
                    //cmd.ExecuteNonQuery();

                    //var dr = (OracleDataReader)cmd.Parameters["io_cursor"].Value;
                    //dtable.Load((System.Data.IDataReader)dr);

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // this will query your database and return the result to your datatable
                    da.Fill(dtable);

                    // System.Data.IDataReader vv = (System.Data.IDataReader)dr;

                    //dvrow = new DataView(dtable);
                    //   dvrow = dtable.DefaultView;
                    //var oissel = dr.Cast<string>();
                    //Array ar = new Array(string) ;
                    //dt.Rows.CopyTo(ar, 0);

                    //  var objMPPBLListSelects = new ArrayList(dt.Rows);
                    //pp = new SqlParameter("oCriteria", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    //cmd.Parameters.Add(pp);
                    //pp = null;

                    //OrclConn.Open();
                    //cmd.ExecuteNonQuery();
                    //Criterio = cmd.Parameters["ocriteria"].Value.ToString();
                }
                return dtable;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return dtable;
        }



        //public string AssociazioneCaricoCapacita(int IdRisorseTeam, int IdGhosts, string UserNameCorrente) ///string TipoAzione,
        //{
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mpasscaricocapacita.associazionecaricocapacita"; ///pk_mpasscaricocapacita.associazionecaricocapacita
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iOidRisorsaTeam", SqlDbType.Int) { Value = IdRisorseTeam };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iOIdGhost", SqlDbType.Int) { Value = IdGhosts };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            var Messaggi = command.Parameters["oMessaggio"].Value.ToString();
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
        //    return string.Empty;
        //}


        //  crea ordine di laroro 
        //begin
        //-- Call the procedure  pk_mpregpianificazione.creaodlghost(iregpiano => :iregpiano,
        //                     ighost => :ighost,     iutente => :iutente,          pmessaggio => :pmessaggio);
        //public string CreaRegRdLbyGhost(int iRegPiano, int IdGhosts) ///string TipoAzione,
        //{
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            // command.CommandText = "pk_mpregpiani_odl.crea_rdl_gg_pianifica";
        //            command.CommandText = "pk_mpregpiani_odl.mpghost_rdl_lancio";
        //            command.CommandType = CommandType.StoredProcedure;
        //            //var pp = new SqlParameter("iRegPiano", SqlDbType.Int) { Value = iRegPiano };
        //            //command.Parameters.Add(pp);
        //            //pp = null;

        //            SqlParameter pp = new SqlParameter("impghost", SqlDbType.Int) { Value = IdGhosts };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iusername", SqlDbType.VarChar, 100) { Value = SetVarSessione.CorrenteUser };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("omessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            var Messaggi = command.Parameters["omessage"].Value.ToString();
        //            if (Messaggi != null && !Messaggi.Equals(string.Empty))
        //            {
        //                throw new Exception(string.Format("Errore: Connessione al DB ({0}) non eseguita correttamente; {1}", "pk_mpregpiani_odl.mpghost_rdl_lancio", Messaggi));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //    return string.Empty;
        //}



        //AssegnaInEmergenzaRegRdL
        public string AggiornaRdLbySSmistamento(int OidRegRdL, string Tipo)
        {
            //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpasscaricocapacita.AggiornaRdLbySSmistamento", OrclConn) { CommandType = CommandType.StoredProcedure };
                    //                        pk_MPASSCARICOCAPACITA.AggiornaRdLbySSmistamento
                    var pp = new SqlParameter("iOidRegRdl", SqlDbType.Int) { Value = OidRegRdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iTipo", SqlDbType.VarChar, 100) { Value = Tipo };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iUtente", SqlDbType.VarChar, 1000) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Msg = cmd.Parameters["oMessaggio"].Value.ToString();
                }
                return Msg;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Msg;
        }

        //AssegnaInEmergenzaRegRdL
        //public string AggiornaRdLbyObjSpace(int OidRegRdL, string Tipo)
        //{
        //    //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.AggiornaRdLbySSmistamento", OrclConn) { CommandType = CommandType.StoredProcedure };

        //            var pp = new SqlParameter("iOidRegRdl", SqlDbType.Int) { Value = OidRegRdL };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iTipo", SqlDbType.VarChar, 100) { Value = Tipo };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iUtente", SqlDbType.VarChar, 1000) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}



        //AssegnaInEmergenzaRegRdL
        public string AssegnaInEmergenzaRegRdL(int OidRegRdL, string Tipo)
        {
            //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpasscaricocapacita.regrdlemergenzadaco", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new SqlParameter("iOidRegRdl", SqlDbType.Int) { Value = OidRegRdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iTipo", SqlDbType.VarChar, 100) { Value = Tipo };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Msg = cmd.Parameters["oMessaggio"].Value.ToString();
                }
                return Msg;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Msg;
        }

        //public string CreateRegRdLbyRdL(int OidRegRdL)
        //{
        //    //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.crearegrdlbyrdl", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("iOidRdl", SqlDbType.Int) { Value = OidRegRdL };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iUtente", SqlDbType.VarChar, 1000) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}
        //public string RdlSospesa(int OidRdl)
        //{
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.RdlSospesa", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("iOidRdl", SqlDbType.Int) { Value = OidRdl };
        //            cmd.Parameters.Add(pp);
        //            pp = null;


        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}
        //public string RdlAnnullata(int OidRdl)
        //{
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.RdlAnnullata", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("iOidRdl", SqlDbType.Int) { Value = OidRdl };
        //            cmd.Parameters.Add(pp);
        //            pp = null;


        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;


        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}

        //public string RdlMigrazionepmptt(int OidRdl)
        //{
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.RdlMigrazionepmptt", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("iOidRdl", SqlDbType.Int) { Value = OidRdl };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;



        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}

        public string RdlCambioRisorsaTeam(int OidRdl, int OidRisorsaTeam)
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpasscaricocapacita.RdlCambioRisorsaTeam", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new SqlParameter("iOidRdl", SqlDbType.Int) { Value = OidRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("OidRisorsaTeam", SqlDbType.Int) { Value = OidRisorsaTeam };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;



                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Msg = cmd.Parameters["oMessaggio"].Value.ToString();
                }
                return Msg;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Msg;
        }

        //public string RdlCompletamento(int OidRdl)
        //{
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.RdlCompletamento", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("iOidRdl", SqlDbType.Int) { Value = OidRdl };
        //            cmd.Parameters.Add(pp);
        //            pp = null;


        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;



        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}


        //public string RdLAssegnaTeamRisorse(int OidRegRdl, int OidRisorsaTeam)
        //{
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.RegRdLAssegnaRisorsaTeam", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("iOidRegRdl", SqlDbType.Int) { Value = OidRegRdl };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iOidRisorsaTeam", SqlDbType.Int) { Value = OidRisorsaTeam };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;



        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}


        //public string RegRdLCompletamento(int OidRegRdl)
        //{
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.RdlCompletamento", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("ioidregrdl", SqlDbType.Int) { Value = OidRegRdl };
        //            cmd.Parameters.Add(pp);
        //            pp = null;


        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;



        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}

        //public string linkCompletamento()
        //{
        //    var Msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpasscaricocapacita.Parlinkcompletamento", OrclConn) { CommandType = CommandType.StoredProcedure };


        //            var pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            Msg = cmd.Parameters["oMessaggio"].Value.ToString();
        //        }
        //        return Msg;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return Msg;
        //}



        public DataView SetDataFissaDettaglio(DateTime iMPDataFissaOiD, int Apparato, int SchedaMP)
        {
            var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            var sbOid = new StringBuilder("", 32000);
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.CalcolaDataFissaPianifCursor", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iApparato", SqlDbType.Int) { Value = Apparato };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iSchedaMP", SqlDbType.Int) { Value = SchedaMP };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iData", SqlDbType.DateTime) { Value = iMPDataFissaOiD };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add(pp);

                    OrclConn.Open();
                    //cmd.ExecuteNonQuery();

                    //var dr = (OracleDataReader)cmd.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);

                    dv = new DataView(dt);



                    Messaggio = cmd.Parameters["oMessaggio"].Value.ToString();
                    if (Messaggio != null && !Messaggio.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggio));
                    }
                }
                return dv;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return dv;
        }



        public DataView SetAttivitaContatore(int OidRegistroPianoMP)
        {
            //  pk_mpregpianificazione.insert_mpdatacontatore(oidmpregpianificazione => :oidmpregpianificazione,   omessaggio => :omessaggio);
            var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.insert_mpdatacontatore", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("oidmpregpianificazione", SqlDbType.Int) { Value = OidRegistroPianoMP };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["oMessaggio"].Value.ToString();
                    if (Messaggio != null && !Messaggio.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggio));
                    }
                }
                return dv;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return dv;
        }




        public DataView SetAttivitaPluriennali(int OidRegistroPianoMP)
        {
            //     pk_mpplanner.insertmpdatapluriennale(oidmpregpianificazione => :oidmpregpianificazione,  omessaggio => :omessaggio);
            var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.insertmpdatapluriennale", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("oidmpregpianificazione", SqlDbType.Int) { Value = OidRegistroPianoMP };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["oMessaggio"].Value.ToString();
                    if (Messaggio != null && !Messaggio.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggio));
                    }
                }
                return dv;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return dv;
        }
        //

        public DataView SetAttivitaDateIniziali(int OidRegistroPianoMP)
        {
            //    pk_mpplanner.insert_mpdatainiziale(oidmpregpianificazione => :oidmpregpianificazione, omessaggio => :omessaggio);
            var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("pk_mpplanner.insert_mpdatainiziale", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("oidmpregpianificazione", SqlDbType.Int) { Value = OidRegistroPianoMP };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["oMessaggio"].Value.ToString();
                    if (Messaggio != null && !Messaggio.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggio));
                    }
                }
                return dv;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return dv;
        }




        //public int GetNextSettimanaODL(int RegPiano, int NumSettimanaCorrente) ///string TipoAzione,
        //{
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new SqlCommand("pk_mpplanner.GetNextSettimanaODL", OrclConn);
        //            cmd.CommandType = CommandType.StoredProcedure;


        //            var pp = new SqlParameter("iRegPiano", SqlDbType.Int) { Value = RegPiano };
        //            cmd.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iSettimana", SqlDbType.Int) { Value = NumSettimanaCorrente };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oNextSettimana", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            cmd.ExecuteNonQuery();
        //            var NextSettimana = 0;
        //            var sNextSettimana = cmd.Parameters["oNextSettimana"].Value.ToString();
        //            if (int.TryParse(sNextSettimana, out NextSettimana))
        //            {
        //                return NextSettimana;
        //            }
        //            else
        //            {
        //                return NumSettimanaCorrente;
        //            }
        //        }
        //        return NumSettimanaCorrente;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
        //    }
        //    return NumSettimanaCorrente;
        //}


        ///// <param name="Oidpmp"></param>
        public DataView AggiornaLogSchedeMP(string UserNameCorrente, int Oidpmp)
        {
            var dv = new DataView();
            var dt = new DataTable();
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_pmp_util.log_mail_schedemp";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iUserNameCorrente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iOid", SqlDbType.Int) { Value = Oidpmp };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);
                    OrclConn.Open();
                    //command.ExecuteNonQuery();
                    //var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    dv = new DataView(dt);
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
            return dv;
        }


        //SQL SERVER CHIAMATA FATTA

        public Dictionary<int, string> RisorsaTeamCaricaCombo(int OidRisorsaTeam, string UserNameCorrente)
        {
            var List = new Dictionary<int, string>();
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.RisorsaTeamCaricaComboCLink";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidRisorsaTeam", SqlDbType.Int) { Value = OidRisorsaTeam };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();

                    var dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            List.Add(dr.GetInt32(dr.GetOrdinal("OidRisorsa")),
                                  dr.GetString(dr.GetOrdinal("NomeCognome"))
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return List;
        }
        public string CreaRisorsaTeam(int OidRisorsa, int Anno, string UserNameCorrente)
        {
            var Messaggi = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.CreaRisorsaTeam";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iOidRisorsa", SqlDbType.Int) { Value = OidRisorsa };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iAnno", SqlDbType.Int) { Value = Anno };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();
                    command.ExecuteNonQuery();
                    Messaggi = command.Parameters["oMessaggio"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return Messaggi;
        }
        /// <param name="Oidpmp"></param>
        public string CreaCoppiaLinkata(int OidRisorsa, string UserNameCorrente)
        {
            var Criterio = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.CreaCoppiaLinkata";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidRisorsa", SqlDbType.Int) { Value = OidRisorsa };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);


                    pp = new SqlParameter("oCriteria", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);


                    SqlConn.Open();
                    command.ExecuteNonQuery();

                    Criterio = command.Parameters["oCriteria"].Value.ToString();
                    var Messaggi = command.Parameters["oMessaggio"].Value.ToString();
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
            return Criterio;
        }

        /// <param name="Oidpmp"></param>
        public string CreaCoppiaLinkataConRisorsa(int iOidCapoRisorsaTeam, int OidRisorsa, string UserNameCorrente)
        {
            var Messaggi = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.CreaCoppiaLinkataConRisorsa";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidRisorsaTeam", SqlDbType.Int) { Value = iOidCapoRisorsaTeam };
                    command.Parameters.Add(pp);


                    pp = new SqlParameter("iOidRisorsa", SqlDbType.Int) { Value = OidRisorsa };
                    command.Parameters.Add(pp);


                    pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);


                    SqlConn.Open();
                    command.ExecuteNonQuery();
                    Messaggi = command.Parameters["oMessaggio"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return Messaggi;
        }

        /// <param name="Oidpmp"></param>
        //public string CreaCoppiaLinkataConRisorsa1(int iOidRisorsaTeam, int OidRisorsa, string UserNameCorrente)
        //{
        //    var Messaggi = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mpasscaricocapacita.CreaCoppiaLinkataConRisorsa";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iOidRisorsaTeam", SqlDbType.Int) { Value = OidRisorsa };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iOidRisorsa", SqlDbType.Int) { Value = OidRisorsa };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            Messaggi = command.Parameters["oMessaggio"].Value.ToString();
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
        //    return Messaggi;
        //}

        ///// <param name="Oidpmp"></param>
        public string RilasciaRisorse(int OidRisorsaTeam, string UserNameCorrente)
        {
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.rilasciarisorsedateam";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("ioidrisorsateam", SqlDbType.Int) { Value = OidRisorsaTeam };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iutente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    SqlConn.Open();
                    command.ExecuteNonQuery();
                    var Messaggi = command.Parameters["omessaggio"].Value.ToString();
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
            return string.Empty;
        }


        /// <param name="Oidpmp"></param>
        //public string GetRisorsexTask(string OidRdL, string UserNameCorrente)
        //{  //PROCEDURE GetRisorsexTask(iOidRdL   IN VARCHAR2,   IO_CURSOR IN OUT T_CURSOR                            )
        //    var Criterio = string.Empty;
        //    var dv = new DataView();
        //    var dt = new DataTable();
        //    var msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mpasscaricocapacita.GetRisorsexTask";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iOidRdL", SqlDbType.VarChar, 4000) { Value = OidRdL };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            //pp = new SqlParameter("oCriteria", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            //command.Parameters.Add(pp);
        //            //pp = null;
        //            pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
        //            System.Data.IDataReader idr = (System.Data.IDataReader)dr;
        //            //  idr.t
        //            dt.Load((System.Data.IDataReader)dr);
        //            dv = new DataView(dt);

        //            if (dv.Count > 0)
        //            {
        //                Criterio = string.Join(",", dv.Cast<DataRowView>().Select(rv => rv.Row["oidrisorseteam"]));
        //                Criterio = string.Format("Oid In ({0})", Criterio);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //    return Criterio;
        //}


        /// <param name="Oidpmp"></param>
        //public string[] GetDistanzeImpiantoRisorseTeam(int OidRisorseTeam, int OidRdlImpianto)
        //{
        //    var Risultati = new string[5];
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mpasscaricocapacita.GetDistanzeRisorseTeam";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iOidRisorsaTeam", SqlDbType.VarChar, 4000) { Value = OidRisorseTeam };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iOidImpianto", SqlDbType.VarChar, 100) { Value = OidRdlImpianto };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oDistanza", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oIncaricoImpianto", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oUltimoImpianto", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oRdLSospese", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oRdLAssegnate", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            Risultati[0] = command.Parameters["oDistanza"].Value.ToString();
        //            Risultati[1] = command.Parameters["oIncaricoImpianto"].Value.ToString();
        //            Risultati[2] = command.Parameters["oUltimoImpianto"].Value.ToString();
        //            Risultati[3] = command.Parameters["oRdLSospese"].Value.ToString();
        //            Risultati[4] = command.Parameters["oRdLAssegnate"].Value.ToString();
        //            var Messaggi = command.Parameters["oMessaggio"].Value.ToString();
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
        //    return Risultati;
        //}




        /// <param name="Oidpmp"></param>
        //public string NrInterventiInEdificio(int OidRisorseTeam, int OidEdificio, ref string NrInterventi)
        //{
        //    var Risultati = "";
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mpasscaricocapacita.NrInterventiInEdificio";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("iOidRisorsaTeam", SqlDbType.VarChar, 4000) { Value = OidRisorseTeam };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iOidEdificio", SqlDbType.VarChar, 100) { Value = OidEdificio };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oNrInterventi", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            NrInterventi = command.Parameters["oNrInterventi"].Value.ToString();

        //            var Messaggi = command.Parameters["oMessaggio"].Value.ToString();
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
        //    return Risultati;
        //}

        //    public System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.npRdLRisorseTeam> GetTeamRisorse_for_RdL

        //public System.ComponentModel.BindingList<DCRisorseTeamRdL> GetTeamRisorse_for_RdL_old
        //    (int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase, string UserNameCorrente, int OidRTeamRemove = 0)
        //{
        //    var Criterio = string.Empty;

        //    System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();

        //    var msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_dc_salaoperativa.RISORSE_EDIFICIO_RDL";
        //            command.CommandType = CommandType.StoredProcedure;

        //            var pp = new SqlParameter("iOidEdificio", SqlDbType.Int) { Value = OidEdificio };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iIsSmartphone", SqlDbType.Int) { Value = IsSmartphone };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iOidCentroOperativoBase", SqlDbType.Int) { Value = iOidCentroOperativoBase };
        //            command.Parameters.Add(pp);
        //            pp = null;


        //            pp = new SqlParameter("iusername", SqlDbType.VarChar, 4000) { Value = UserNameCorrente }; ;
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();
        //            var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
        //            if (dr.HasRows)
        //            {
        //                int Newid = 0;
        //                while (dr.Read())
        //                {
        //                    // CAMS.Module.DBTask.DC.DCRisorseTeamRdL obj = xpObjectSpace.CreateObject<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();
        //                    if (OidRTeamRemove > 0 && OidRTeamRemove == dr.GetInt32(dr.GetOrdinal("OIDRISORSATEAM")))
        //                        continue;

        //                    CAMS.Module.DBTask.DC.DCRisorseTeamRdL obj = new CAMS.Module.DBTask.DC.DCRisorseTeamRdL()
        //                        {
        //                            Oid = Guid.NewGuid(), /*obj.ID = Newid++;*/
        //                            OidCentroOperativo = dr.GetInt32(dr.GetOrdinal("OIDCENTROOPERATIVO")), /*   obj.OidEdificio = dr.GetInt32(dr.GetOrdinal("OIDEDIFICIO")); // OIDEDIFICIO*/
        //                            OidRisorsaTeam = dr.GetInt32(dr.GetOrdinal("OIDRISORSATEAM")),
        //                            NumeroAttivitaAgenda = dr.GetInt32(dr.GetOrdinal("NRATTAGENDA")),
        //                            NumeroAttivitaSospese = dr.GetInt32(dr.GetOrdinal("NRATTSOSPESE")),
        //                            NumeroAttivitaEmergenza = dr.GetInt32(dr.GetOrdinal("NRATTEMERGENZA")),
        //                            Conduttore = dr.GetInt32(dr.GetOrdinal("CONDUTTORE")) > 0 ? true : false /* Verifica.Substring(i, 1) == "1" ? true : false;*/,
        //                            CoppiaLinkata = (TipoNumeroManutentori)dr.GetInt32(dr.GetOrdinal("NUMMAN")),
        //                            Ordinamento = dr.GetInt32(dr.GetOrdinal("ORDINAMENTO")),
        //                            CentroOperativo = dr.GetString(dr.GetOrdinal("CENTROOPERATIVO")),
        //                            UltimoStatoOperativo = dr.GetString(dr.GetOrdinal("ULTIMOSTATOOPERATIVO")),
        //                            RisorsaCapo = dr.GetString(dr.GetOrdinal("RISORSACAPOSQUADRA")),
        //                            Mansione = dr.GetString(dr.GetOrdinal("MANSIONE")),
        //                            Telefono = dr.GetString(dr.GetOrdinal("TELEFONO")),
        //                            RegistroRdL = dr.GetString(dr.GetOrdinal("REGRDLASSOCIATO")), /*  obj.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));*/
        //                            DistanzaImpianto = dr.GetString(dr.GetOrdinal("DISTANZAIMPIANTO")),
        //                            UltimoEdificio = dr.GetString(dr.GetOrdinal("ULTIMOEDIFICIO")),
        //                            InterventosuEdificio = dr.GetString(dr.GetOrdinal("INTERVENTISUEDIFICIO")),
        //                            Url = dr.GetString(dr.GetOrdinal("URL")),
        //                            Azienda = dr.GetString(dr.GetOrdinal("AZIENDA"))
        //                        };


        //                    objects.Add(obj);

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
        //    }
        //    return objects;
        //}

        //    public System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.npRdLRisorseTeam> GetTeamRisorse_for_RdL


        //  REPORT_REGRDL

        //public System.ComponentModel.BindingList<DCRdLListReport> GetReport_RdL
        //    (string icodiciRDL)
        //{
        //    var Criterio = string.Empty;

        //    System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport>();

        //    var msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_dc_salaoperativa.REPORT_RDL";
        //            command.CommandType = CommandType.StoredProcedure;

        //            var pp = new SqlParameter("icodiciRDL", SqlDbType.VarChar, 4000) { Value = icodiciRDL };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("icodiciRDL_Lob", SqlDbType.Clob, 4000) { Value = icodiciRDL, Direction = ParameterDirection.Input };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            //   //System.Data.OracleClient.SqlParameter clob_Param = new System.Data.OracleClient.SqlParameter();
        //            //   pp.ParameterName = "icodiciRDL_Lob";
        //            //   pp.SqlDbType = SqlDbType.Clob;
        //            //   pp.Direction = ParameterDirection.Input;
        //            //  //OracleLob aa = new OracleLob();
        //            //  // System.Data.OracleClient.OracleLob clob = new System.Data.OracleClient.OracleLob(OrclConn,OracleDbType.
        //            //  // clob.Write(icodiciRDL.ToArray(), 0, icodiciRDL.Length);
        //            //   pp.Value = icodiciRDL;
        //            ////   p_vc.Size = 32000;
        //            //   command.Parameters.Add(pp);
        //            //   pp = null;

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();
        //            var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
        //            if (dr.HasRows)
        //            {
        //                int Newid = 0;
        //                while (dr.Read())
        //                {
        //                    #region prove test
        //                    //string ComponentiManutenzioneClob = string.Empty;
        //                    //int ordClob = dr.GetOrdinal("Componenti1");
        //                    //ComponentiManutenzioneClob = Convert.ToString(((System.Data.OracleClient.OracleLob)(dr.GetOracleValue(ordClob))).Value);

        //                    //string ComponentiManutenzioneLong  = string.Empty;
        //                    //int ordLong = dr.GetOrdinal("Componenti");
        //                    //ComponentiManutenzioneLong = ((System.Data.OracleClient.OracleString) dr.GetOracleValue(ordLong)).ToString();
        //                    //var aa = dr.GetOracleLob(dr.GetOrdinal("Componenti")).Value;
        //                    //string aaa = aa.ToString();
        //                    #endregion
        //                    Newid++;
        //                    CAMS.Module.DBTask.DC.DCRdLListReport obj = new CAMS.Module.DBTask.DC.DCRdLListReport()
        //                    {

        //                        ID = Newid,
        //                        codiceRdL = dr.GetInt32(dr.GetOrdinal("CODICERDL")),
        //                        CodRegRdL = dr.GetInt32(dr.GetOrdinal("CODREGRDL")),

        //                        CentroCosto = dr.GetString(dr.GetOrdinal("CENTROCOSTO")),
        //                        Descrizione = dr.GetString(dr.GetOrdinal("DESCRIZIONE")),//      dr.Descrizione,
        //                        RegRdLDescrizione = dr.GetString(dr.GetOrdinal("REGRDLDESCRIZIONE")),//      dr.RegRdLDescrizione,
        //                        Priorita = dr.GetString(dr.GetOrdinal("PRIORITA")),// dr.Priorita,
        //                        PrioritaIntervento = dr.GetString(dr.GetOrdinal("PRIORITATIPOINTERVENTO")),// dr.PrioritaIntervento,
        //                        Immobile = dr.GetString(dr.GetOrdinal("IMMOBILE")),// dr.Immobile,
        //                        Codedificio = dr.GetString(dr.GetOrdinal("CODEDIFICIO")),// dr.Codedificio,
        //                        Impianto = dr.GetString(dr.GetOrdinal("IMPIANTO")),// dr.Impianto,
        //                        Apparato = dr.GetString(dr.GetOrdinal("APPARATO")),//       dr.Apparato,
        //                        ApparatoPadre = dr.GetString(dr.GetOrdinal("APPARATOPADRE")),//           dr.ApparatoPadre,
        //                        ApparatoSostegno = dr.GetString(dr.GetOrdinal("APPARATOSOSTEGNO")),// dr.ApparatoSostegno,
        //                        Problema = dr.GetString(dr.GetOrdinal("PROBLEMA")),//  dr.Problema,
        //                        Causa = dr.GetString(dr.GetOrdinal("CAUSA")),//dr.Causa,
        //                        Rimedio = dr.GetString(dr.GetOrdinal("RIMEDIO")),//dr.Rimedio,
        //                        TipoApparato = dr.GetString(dr.GetOrdinal("TIPOAPPARATO")),//dr.TipoApparato,
        //                        Team = dr.GetString(dr.GetOrdinal("TEAM")),//                                dr.Team,
        //                        CategoriaManutenzione = dr.GetString(dr.GetOrdinal("CATEGORIAMANUTENZIONE")),//  dr.CategoriaManutenzione,

        //                        DataPianificata = dr.GetString(dr.GetOrdinal("DATAPIANIFICATA")), // dr.DataPianificata,
        //                        DataRichiesta = dr.GetString(dr.GetOrdinal("DATARICHIESTA")),// dr.DataRichiesta,
        //                        DataCreazione = dr.GetString(dr.GetOrdinal("DATACREAZIONE")),
        //                        DataCompletamento = dr.GetString(dr.GetOrdinal("DATACOMPLETAMENTO")),
        //                        DataAssegnazione = dr.GetString(dr.GetOrdinal("DATAASSEGNAZIONE")),
        //                        DataAggiornamento = dr.GetString(dr.GetOrdinal("DATAAGGIORNAMENTO")),

        //                        TeamMansione = dr.GetString(dr.GetOrdinal("TEAMMANSIONE")),// dr.TeamMansione,
        //                        Richiedente = dr.GetString(dr.GetOrdinal("RICHIEDENTE")),// dr.Richiedente,

        //                        CodSchedeMp = dr.GetString(dr.GetOrdinal("CODSCHEDEMP")),//  dr.CodSchedeMp,
        //                        CodSchedaMpUni = dr.GetString(dr.GetOrdinal("CODSCHEDAMPUNI")),// dr.CodSchedaMpUni,
        //                        DescrizioneManutenzione = dr.GetString(dr.GetOrdinal("DESCRIZIONEMANUTENZIONE")),// dr.DescrizioneManutenzione,
        //                        FrequenzaDescrizione = dr.GetString(dr.GetOrdinal("FREQUENZADESCRIZIONE")),// dr.FrequenzaDescrizione,
        //                        PassoSchedaMp = dr.GetString(dr.GetOrdinal("PASSOSCHEDAMP")),//  dr.PassoSchedaMp,
        //                        NrOrdine = dr.GetInt32(dr.GetOrdinal("NORDINE")),//   int.Parse(dr.NrOrdine.ToString())
        //                        StatoSmistamento = dr.GetString(dr.GetOrdinal("STATOSMISTAMENTO")),
        //                        StatoOperativo = dr.GetString(dr.GetOrdinal("STATOOPERATIVO")),
        //                        Utente = dr.GetString(dr.GetOrdinal("UTENTE")),
        //                        NoteCompletamento = dr.GetString(dr.GetOrdinal("NOTECOMPLETAMENTO")),
        //                        FrequenzaCod_Descrizione = dr.GetString(dr.GetOrdinal("FREQUENZACOD_DESCRIZIONE")),
        //                        //ComponentiManutenzioneClob = Convert.ToString(((System.Data.OracleClient.OracleLob)(dr.GetOracleValue(ordClob))).Value);
        //                        //ComponentiManutenzione = dr.GetOracleLob(dr.GetOrdinal("ComponentiManutenzione")).Value.ToString(),
        //                        ComponentiManutenzione = Convert.ToString(((System.Data.OracleClient.OracleLob)
        //                                                (dr.GetOracleValue(dr.GetOrdinal("ComponentiManutenzione")))).Value),
        //                        // ######   DEVE ESSERERE UN CLOB DI TESTO!!!!
        //                        //ComponentiSostegno = dr.GetString(dr.GetOrdinal("COMPONENTI_SOSTEGNO")),

        //                        Annotazioni = Convert.ToString(((System.Data.OracleClient.OracleLob)
        //                                               (dr.GetOracleValue(dr.GetOrdinal("Annotazioni")))).Value),
        //                        OidCategoria = dr.GetInt32(dr.GetOrdinal("OIDCATEGORIA")),
        //                        Anno = dr.GetInt32(dr.GetOrdinal("ANNO")),
        //                        Mese = dr.GetInt32(dr.GetOrdinal("MESE")),
        //                        Settimana = dr.GetInt32(dr.GetOrdinal("SETTIMANA")),
        //                        //AreaDiPolo = dr.GetString(dr.GetOrdinal("AREADIPOLO")),
        //                        RefAmministrativo = dr.GetString(dr.GetOrdinal("REFAMMINISTRATIVO")),
        //                        CorpoMP = dr.GetString(dr.GetOrdinal("CORPOMP")),
        //                        OrdinePasso = dr.GetString(dr.GetOrdinal("ORDINEPASSO"))
        //                        //CODICE
        //                        //CODICEODL
        //                        //INSOURCING
        //                        //KEYPLAN
        //                        //OIDEDIFICIO
        //                        //OIDREFERENTECOFELY
        //                        //OIDSMISTAMENTO

        //                    };


        //                    objects.Add(obj);

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
        //    }
        //    return objects;
        //}


        public Dictionary<int, string> RisorsaCaricaCombo(int OidRisorsa, string UserNameCorrente)
        {
            var List = new Dictionary<int, string>();
            try
            {
                using (var SqlConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.RisorsaCaricaComboCoppiaLink";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidRisorsa", SqlDbType.Int) { Value = OidRisorsa };
                    command.Parameters.Add(pp);

                    //pp = new SqlParameter("iUtente", SqlDbType.VarChar, 100) { Value = UserNameCorrente };
                    //command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();
                    var dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            List.Add(dr.GetInt32(dr.GetOrdinal("OidRisorsaTeam")),
                                  dr.GetString(dr.GetOrdinal("NomeCognomeRisorsaCapo"))
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return List;
        }
        /// <param name="Oidpmp"></param>


        public System.ComponentModel.BindingList<DCRegistroOperativoRisorsa> GetOperativoRisorsa
                                                                               (int OidObj, DateTime DataLimite, string UserNameCorrente)
        {
            var Criterio = string.Empty;
            System.ComponentModel.BindingList<DCRegistroOperativoRisorsa> objects =
                             new System.ComponentModel.BindingList<DCRegistroOperativoRisorsa>();
            var msg = string.Empty;
            //string vOidObj = OidObj.ToString();
            try
            {
                //procedure  GetAuditData_Risorse
                //(iUser     in varchar2,                        
                // iDataLimiteInf in date,
                // iDataLimiteSup in date,
                // oMessaggio   in out varchar,
                //IO_CURSOR   IN OUT T_CURSOR) 



                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_auditdata.GetAuditData_Risorse";
                    command.CommandType = CommandType.StoredProcedure;

                    //var pp = new SqlParameter("iOidObj", SqlDbType.Int) { Value = OidObj };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    //pp = new SqlParameter("iTypeObj", SqlDbType.Int) { Value = TypeObj };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    //var pp = new SqlParameter("iUser", SqlDbType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    //command.Parameters.Add(pp);


                    var pp = new SqlParameter("iOidRisorsa", SqlDbType.Int) { Value = OidObj }; ;
                    command.Parameters.Add(pp);


                    DateTime DataLimiteInf = DataLimite.AddDays(-7);
                    pp = new SqlParameter("iDataLimiteInf", SqlDbType.DateTime) { Value = DataLimiteInf };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iDataLimiteSup", SqlDbType.DateTime) { Value = DataLimite };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();
                    var dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        int Newid = 0;
                        while (dr.Read())
                        {

                            DCRegistroOperativoRisorsa obj = new DCRegistroOperativoRisorsa()
                            {
                                ID = Newid,
                                RegrdLCodice = dr.GetString(dr.GetOrdinal("CODICEREGRDL")),
                                Descrizione = dr.GetString(dr.GetOrdinal("DESCRIZIONE")),
                                DataOra = dr.GetDateTime(dr.GetOrdinal("DATAORA")),
                                Utente = dr.GetString(dr.GetOrdinal("UTENTE")),
                                newValue = dr.GetString(dr.GetOrdinal("NUOVOVALORE")),
                                oldValue = dr.GetString(dr.GetOrdinal("CAMBIOVALORE")),
                                PropertyName = dr.GetString(dr.GetOrdinal("CAMPOMODIFICATO")),
                                AzionePropertyName = dr.GetString(dr.GetOrdinal("AZIONEPROPERTYNAME")),
                                OidRisorsa = dr.GetInt32(dr.GetOrdinal("RISORSA"))
                                //,
                                //SOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVO_DESC")),
                                //OldSOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVO_DESC_OLD")),
                                //DataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA")),
                                //OldDataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA_OLD"))
                            };
                            objects.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
            }
            return objects;
        }


        /// <param name="Oidpmp"></param>
        public DataView GetFiltrobyTesto(string Oggetto, string searchText)
        {
            var dv = new DataView();
            var dt = new DataTable();
            try
            {
                using (var SqlConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_ricerca.getfiltrobytesto";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("ioggetto", SqlDbType.VarChar, 100) { Value = Oggetto };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("isearchtext", SqlDbType.VarChar, 4000) { Value = searchText };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("omessagg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();
                    command.ExecuteNonQuery();
                    var dr = command.ExecuteReader();
                    dt.Load((System.Data.IDataReader)dr);
                    dv = new DataView(dt);
                    var Messaggi = command.Parameters["omessagg"].Value.ToString();
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
            return dv;
        }



        public System.ComponentModel.BindingList<DCRegistroSmistamentoDettaglio> GetStorico_RdL
                                                                               (int OidObj, int TypeObj, DateTime DataLimite, string UserNameCorrente)
        {
            var Criterio = string.Empty;
            System.ComponentModel.BindingList<DCRegistroSmistamentoDettaglio> objects =
                             new System.ComponentModel.BindingList<DCRegistroSmistamentoDettaglio>();
            var msg = string.Empty;
            string sOidObj = OidObj.ToString();
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_auditdata.getauditdata";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iOidObj", SqlDbType.VarChar) { Value = sOidObj };
                    command.Parameters.Add(pp);


                    //pp = new SqlParameter("iTypeObj", SqlDbType.Int) { Value = TypeObj };
                    //command.Parameters.Add(pp);


                    //pp = new SqlParameter("iDataLimite", SqlDbType.DateTime) { Value = DataLimite };
                    //command.Parameters.Add(pp);



                    //pp = new SqlParameter("iusername", SqlDbType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    //command.Parameters.Add(pp);


                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);


                    pp.Direction = ParameterDirection.Output;

                    SqlConn.Open();
                    var dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int Newid = 0;
                        while (dr.Read())
                        {

                            DCRegistroSmistamentoDettaglio obj = new DCRegistroSmistamentoDettaglio()
                            {
                                ID = Newid,
                                RdLCodice = dr.GetString(dr.GetOrdinal("RDL_CODICE")),
                                Descrizione = dr.GetString(dr.GetOrdinal("DESCRIZIONE")),
                                DataOra = dr.GetDateTime(dr.GetOrdinal("DATAORA")),
                                UtenteSO = dr.GetString(dr.GetOrdinal("UTENTE")),
                                newValue = dr.GetString(dr.GetOrdinal("NUOVOVALORE")),
                                oldValue = dr.GetString(dr.GetOrdinal("CAMBIOVALORE")),
                                PropertyName = dr.GetString(dr.GetOrdinal("PROPERTYNAME")),
                                AzionePropertyName = dr.GetString(dr.GetOrdinal("AZIONEPROPERTYNAME"))
                                //,
                                //SOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVO_DESC")),
                                //OldSOperativoDescrizione = dr.GetString(dr.GetOrdinal("SOPERATIVO_DESC_OLD")),
                                //DataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA")),
                                //OldDataPianificata = dr.GetDateTime(dr.GetOrdinal("DATAPIANIFICATA_OLD"))
                            };
                            objects.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
            }
            return objects;
        }

        public System.ComponentModel.BindingList<DCRisorseTeamRdL> GetTeamRisorse_for_RdL
          (int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase, string UserNameCorrente, int OidRegoleAutoAssegnazione, int OidRTeamRemove = 0)
        {
            var Criterio = string.Empty;

            System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();

            var msg = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "PK_DC_SALAOPERATIVA_1.ris_edif_rdl_nuova";
                    //command.CommandText = "pk_dc_salaoperativa.RISORSE_EDIFICIO_RDL_REV";
                    command.CommandType = CommandType.StoredProcedure;
                    //  iregolaautossegnazione  
                    var pp = new SqlParameter("iOidRegoleAutoAssegnazione", SqlDbType.Int) { Value = OidRegoleAutoAssegnazione };
                    command.Parameters.Add(pp);


                    pp = new SqlParameter("iOidEdificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);


                    pp = new SqlParameter("iIsSmartphone", SqlDbType.Int) { Value = IsSmartphone };
                    command.Parameters.Add(pp);


                    pp = new SqlParameter("iOidCentroOperativoBase", SqlDbType.Int) { Value = iOidCentroOperativoBase };
                    command.Parameters.Add(pp);



                    pp = new SqlParameter("iusername", SqlDbType.VarChar, 50) { Value = UserNameCorrente }; ;
                    command.Parameters.Add(pp);


                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();

                    var dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        //int Newid = 0;
                        while (dr.Read())
                        {
                            // CAMS.Module.DBTask.DC.DCRisorseTeamRdL obj = xpObjectSpace.CreateObject<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();
                            if (OidRTeamRemove > 0 && OidRTeamRemove == dr.GetInt32(dr.GetOrdinal("OIDRISORSATEAM")))
                                continue;
                            //  prova
                            //CAMS.Module.DBTask.DC.DCRisorseTeamRdL obj = new CAMS.Module.DBTask.DC.DCRisorseTeamRdL()
                            //{
                            //    Oid = Guid.NewGuid(), /*obj.ID = Newid++;*/
                            //    OidCentroOperativo = dr.GetInt32(dr.GetOrdinal("OIDCENTROOPERATIVO")), /*   obj.OidEdificio = dr.GetInt32(dr.GetOrdinal("OIDEDIFICIO")); // OIDEDIFICIO*/
                            //    OidRisorsaTeam = dr.GetInt32(dr.GetOrdinal("OIDRISORSATEAM")),
                            //    NumeroAttivitaAgenda = dr.GetInt32(dr.GetOrdinal("NRATTAGENDA")),
                            //    NumeroAttivitaSospese = dr.GetInt32(dr.GetOrdinal("NRATTSOSPESE")),
                            //    NumeroAttivitaEmergenza = dr.GetInt32(dr.GetOrdinal("NRATTEMERGENZA")),
                            //    Conduttore = dr.GetInt32(dr.GetOrdinal("CONDUTTORE")) > 0 ? true : false /* Verifica.Substring(i, 1) == "1" ? true : false;*/,
                            //    CoppiaLinkata = (TipoNumeroManutentori)dr.GetInt32(dr.GetOrdinal("NUMMAN")),
                            //    Ordinamento = dr.GetInt32(dr.GetOrdinal("ORDINAMENTO")),
                            //    CentroOperativo = dr.GetString(dr.GetOrdinal("CENTROOPERATIVO")),
                            //    UltimoStatoOperativo = dr.GetString(dr.GetOrdinal("ULTIMOSTATOOPERATIVO")),
                            //    RisorsaCapo = dr.GetString(dr.GetOrdinal("RISORSACAPOSQUADRA")),
                            //    Mansione = dr.GetString(dr.GetOrdinal("MANSIONE")),
                            //    Telefono = dr.GetString(dr.GetOrdinal("TELEFONO")),
                            //    RegistroRdL = dr.GetString(dr.GetOrdinal("REGRDLASSOCIATO")), /*  obj.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));*/
                            //    DistanzaImpianto = dr.GetString(dr.GetOrdinal("DISTANZAIMPIANTO")),
                            //    UltimoEdificio = dr.GetString(dr.GetOrdinal("ULTIMOEDIFICIO")),
                            //    InterventosuEdificio = dr.GetString(dr.GetOrdinal("INTERVENTISUEDIFICIO")),
                            //    Url = dr.GetString(dr.GetOrdinal("URL")),
                            //    Azienda = dr.GetString(dr.GetOrdinal("AZIENDA"))
                            //};
                            CAMS.Module.DBTask.DC.DCRisorseTeamRdL obj = new CAMS.Module.DBTask.DC.DCRisorseTeamRdL();

                            obj.Oid = Guid.NewGuid(); /*obj.ID = Newid++;*/
                            obj.OidCentroOperativo = dr.GetInt32(dr.GetOrdinal("OIDCENTROOPERATIVO"));/*   obj.OidEdificio = dr.GetInt32(dr.GetOrdinal("OIDEDIFICIO")); // OIDEDIFICIO*/
                            obj.OidRisorsaTeam = dr.GetInt32(dr.GetOrdinal("OIDRISORSATEAM"));
                            obj.NumeroAttivitaAgenda = dr.GetInt32(dr.GetOrdinal("NRATTAGENDA"));
                            obj.NumeroAttivitaSospese = dr.GetInt32(dr.GetOrdinal("NRATTSOSPESE"));
                            obj.NumeroAttivitaEmergenza = dr.GetInt32(dr.GetOrdinal("NRATTEMERGENZA"));
                            obj.Conduttore = dr.GetInt32(dr.GetOrdinal("CONDUTTORE")) > 0 ? true : false;
                            obj.CoppiaLinkata = (TipoNumeroManutentori)dr.GetInt32(dr.GetOrdinal("NUMMAN"));
                            obj.Ordinamento = dr.GetInt32(dr.GetOrdinal("ORDINAMENTO"));
                            obj.CentroOperativo = dr.GetString(dr.GetOrdinal("CENTROOPERATIVO"));
                            obj.UltimoStatoOperativo = dr.GetString(dr.GetOrdinal("ULTIMOSTATOOPERATIVO"));
                            obj.RisorsaCapo = dr.GetString(dr.GetOrdinal("RISORSACAPOSQUADRA"));
                            obj.Mansione = dr.GetString(dr.GetOrdinal("MANSIONE"));
                            obj.Telefono = dr.GetString(dr.GetOrdinal("TELEFONO"));
                            obj.RegistroRdL = dr.GetString(dr.GetOrdinal("REGRDLASSOCIATO")); /*  obj.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));*/
                            obj.DistanzaImpianto = dr.GetString(dr.GetOrdinal("DISTANZAIMPIANTO"));
                            obj.UltimoEdificio = dr.GetString(dr.GetOrdinal("ULTIMOEDIFICIO"));
                            obj.InterventosuEdificio = dr.GetString(dr.GetOrdinal("INTERVENTISUEDIFICIO"));
                            obj.Url = dr.GetString(dr.GetOrdinal("URL"));
                            obj.Azienda = dr.GetString(dr.GetOrdinal("AZIENDA"));
                            obj.Aggiornamento = dr[dr.GetOrdinal("AGGIORNAMENTO")] != DBNull.Value ? dr.GetString(dr.GetOrdinal("AGGIORNAMENTO")) : string.Empty;

                            obj.Distanzakm = dr.GetInt32(dr.GetOrdinal("DISTANZAKM"));
                            obj.NumerorAttivitaTotaliMP = dr.GetInt32(dr.GetOrdinal("NRATTIVITATOTALIMP"));
                            obj.NumerorAttivitaTotaliTT = dr.GetInt32(dr.GetOrdinal("NRATTIVITATOTALITT"));



                            // cambia la store procedura al campo che hai detto tu trovato grazie vado da solo 
                            objects.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
            }
            return objects;
        }

        public int GetTeamRisorseOttimizata(int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase,
            string UserNameCorrente, int OidRegoleAutoAssegnazione, ref string Messaggio)
        {
            int OidTRisorsa = 0;
            string msg = string.Empty;

            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "PK_DC_SALAOPERATIVA_1.GET_TRISORSA_OTTIMIZZATA";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iOidRegoleAutoAssegnazione", SqlDbType.Int) { Value = OidRegoleAutoAssegnazione };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iOidEdificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iIsSmartphone", SqlDbType.Int) { Value = IsSmartphone };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iOidCentroOperativoBase", SqlDbType.Int) { Value = iOidCentroOperativoBase };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new SqlParameter("iusername", SqlDbType.VarChar, 50) { Value = UserNameCorrente }; ;
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("orisorsateam", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    SqlConn.Open();
                    command.ExecuteNonQuery();
                    try
                    {
                        OidTRisorsa = int.Parse(command.Parameters["orisorsateam"].Value.ToString());
                    }
                    catch
                    {
                        OidTRisorsa = 0;
                    }
                    msg = command.Parameters["oMessaggio"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
            }
            return OidTRisorsa;
        }

        //public int GetTeamRisorseOttimizata_rev(int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase,
        //     string UserNameCorrente, int OidRegoleAutoAssegnazione, ref string Messaggio)
        //{
        //    int OidTRisorsa = 0;
        //    string msg = string.Empty;
        //    //procedure get_risorsa_ottim
        //    //(ioidregolaautoss in number,
        //    // omessaggio out varchar2,
        //    //ooidrisorsateam out number )


        //    try
        //    {
        //        using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = SqlConn.CreateCommand();
        //            command.CommandText = "pk_dc_salaoperativa.get_risorsa_ottim";
        //            command.CommandType = CommandType.StoredProcedure;

        //            var pp = new SqlParameter("ioidregolaautoss", SqlDbType.Int) { Value = OidRegoleAutoAssegnazione };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            //pp = new SqlParameter("iOidEdificio", SqlDbType.Int) { Value = OidEdificio };
        //            //command.Parameters.Add(pp);
        //            //pp = null;

        //            //pp = new SqlParameter("iIsSmartphone", SqlDbType.Int) { Value = IsSmartphone };
        //            //command.Parameters.Add(pp);
        //            //pp = null;

        //            //pp = new SqlParameter("iOidCentroOperativoBase", SqlDbType.Int) { Value = iOidCentroOperativoBase };
        //            //command.Parameters.Add(pp);
        //            //pp = null;


        //            //pp = new SqlParameter("iusername", SqlDbType.VarChar, 4000) { Value = UserNameCorrente }; ;
        //            //command.Parameters.Add(pp);
        //            //pp = null;

        //            pp = new SqlParameter("omessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("ooidrisorsateam", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            //pp = null;
        //            //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
        //            //pp.Direction = ParameterDirection.Output;
        //            //command.Parameters.Add(pp);

        //            SqlConn.Open();
        //            command.ExecuteNonQuery();
        //            try
        //            {
        //                OidTRisorsa = int.Parse(command.Parameters["ooidrisorsateam"].Value.ToString());
        //            }
        //            catch
        //            {
        //                OidTRisorsa = 0;
        //            }
        //            msg = command.Parameters["omessaggio"].Value.ToString();
        //            if (msg != null && !msg.Equals(string.Empty))
        //            {
        //                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
        //    }
        //    return OidTRisorsa;
        //}

        //  REPORT_REGRDL
        public System.ComponentModel.BindingList<DCRdLListReport> GetREPORT_REGRDL
            (string codiciREGRDL_Lob)
        {
            var Criterio = string.Empty;
            System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport>();
            var msg = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_dc_salaoperativa.REPORT_REGRDL";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("icodiciREGRDL_Lob", SqlDbType.VarChar, 4000) { Value = codiciREGRDL_Lob, Direction = ParameterDirection.Input };
                    command.Parameters.Add(pp);


                    //   //System.Data.OracleClient.SqlParameter clob_Param = new System.Data.OracleClient.SqlParameter();
                    //   pp.ParameterName = "icodiciRDL_Lob";
                    //   pp.SqlDbType = SqlDbType.Clob;
                    //   pp.Direction = ParameterDirection.Input;
                    //  //OracleLob aa = new OracleLob();
                    //  // System.Data.OracleClient.OracleLob clob = new System.Data.OracleClient.OracleLob(OrclConn,OracleDbType.
                    //  // clob.Write(icodiciRDL.ToArray(), 0, icodiciRDL.Length);
                    //   pp.Value = icodiciRDL;
                    ////   p_vc.Size = 32000;
                    //   command.Parameters.Add(pp);
                    //   pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);


                    SqlConn.Open();
                    var dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int Newid = 0;
                        while (dr.Read())
                        {
                            #region prove test
                            //string ComponentiManutenzioneClob = string.Empty;
                            //int ordClob = dr.GetOrdinal("Componenti1");
                            //ComponentiManutenzioneClob = Convert.ToString(((System.Data.OracleClient.OracleLob)(dr.GetOracleValue(ordClob))).Value);

                            //string ComponentiManutenzioneLong  = string.Empty;
                            //int ordLong = dr.GetOrdinal("Componenti");
                            //ComponentiManutenzioneLong = ((System.Data.OracleClient.OracleString) dr.GetOracleValue(ordLong)).ToString();
                            //var aa = dr.GetOracleLob(dr.GetOrdinal("Componenti")).Value;
                            //string aaa = aa.ToString();
                            #endregion
                            Newid++;
                            CAMS.Module.DBTask.DC.DCRdLListReport obj = new CAMS.Module.DBTask.DC.DCRdLListReport()
                            {

                                ID = Newid,
                                codiceRdL = dr.GetInt32(dr.GetOrdinal("CODICERDL")),
                                CodRegRdL = dr.GetInt32(dr.GetOrdinal("CODREGRDL")),

                                CentroCosto = dr.GetString(dr.GetOrdinal("CENTROCOSTO")),
                                Descrizione = dr.GetString(dr.GetOrdinal("DESCRIZIONE")),//      dr.Descrizione,
                                RegRdLDescrizione = dr.GetString(dr.GetOrdinal("REGRDLDESCRIZIONE")),//      dr.RegRdLDescrizione,
                                Priorita = dr.GetString(dr.GetOrdinal("PRIORITA")),// dr.Priorita,
                                PrioritaIntervento = dr.GetString(dr.GetOrdinal("PRIORITATIPOINTERVENTO")),// dr.PrioritaIntervento,
                                Immobile = dr.GetString(dr.GetOrdinal("IMMOBILE")),// dr.Immobile,
                                Codedificio = dr.GetString(dr.GetOrdinal("CODEDIFICIO")),// dr.Codedificio,
                                Impianto = dr.GetString(dr.GetOrdinal("IMPIANTO")),// dr.Impianto,
                                Apparato = dr.GetString(dr.GetOrdinal("APPARATO")),//       dr.Apparato,
                                ApparatoPadre = dr.GetString(dr.GetOrdinal("APPARATOPADRE")),//           dr.ApparatoPadre,
                                ApparatoSostegno = dr.GetString(dr.GetOrdinal("APPARATOSOSTEGNO")),// dr.ApparatoSostegno,
                                Problema = dr.GetString(dr.GetOrdinal("PROBLEMA")),//  dr.Problema,
                                Causa = dr.GetString(dr.GetOrdinal("CAUSA")),//dr.Causa,
                                Rimedio = dr.GetString(dr.GetOrdinal("RIMEDIO")),//dr.Rimedio,
                                TipoApparato = dr.GetString(dr.GetOrdinal("TIPOAPPARATO")),//dr.TipoApparato,
                                Team = dr.GetString(dr.GetOrdinal("TEAM")),//                                dr.Team,
                                CategoriaManutenzione = dr.GetString(dr.GetOrdinal("CATEGORIAMANUTENZIONE")),//  dr.CategoriaManutenzione,

                                DataPianificata = dr.GetString(dr.GetOrdinal("DATAPIANIFICATA")), // dr.DataPianificata,
                                DataRichiesta = dr.GetString(dr.GetOrdinal("DATARICHIESTA")),// dr.DataRichiesta,
                                DataCreazione = dr.GetString(dr.GetOrdinal("DATACREAZIONE")),
                                DataCompletamento = dr.GetString(dr.GetOrdinal("DATACOMPLETAMENTO")),
                                DataAssegnazione = dr.GetString(dr.GetOrdinal("DATAASSEGNAZIONE")),
                                DataAggiornamento = dr.GetString(dr.GetOrdinal("DATAAGGIORNAMENTO")),

                                TeamMansione = dr.GetString(dr.GetOrdinal("TEAMMANSIONE")),// dr.TeamMansione,
                                Richiedente = dr.GetString(dr.GetOrdinal("RICHIEDENTE")),// dr.Richiedente,

                                CodSchedeMp = dr.GetString(dr.GetOrdinal("CODSCHEDEMP")),//  dr.CodSchedeMp, --todo
                                CodSchedaMpUni = dr.GetString(dr.GetOrdinal("CODSCHEDAMPUNI")),// dr.CodSchedaMpUni, --todo
                                DescrizioneManutenzione = dr.GetString(dr.GetOrdinal("DESCRIZIONEMANUTENZIONE")),// dr.DescrizioneManutenzione, --todo
                                FrequenzaDescrizione = dr.GetString(dr.GetOrdinal("FREQUENZADESCRIZIONE")),// dr.FrequenzaDescrizione, --todo
                                PassoSchedaMp = dr.GetString(dr.GetOrdinal("PASSOSCHEDAMP")),//  dr.PassoSchedaMp, --todo
                                NrOrdine = dr.GetInt32(dr.GetOrdinal("NORDINE")),//   int.Parse(dr.NrOrdine.ToString()) --todo
                                StatoSmistamento = dr.GetString(dr.GetOrdinal("STATOSMISTAMENTO")),
                                StatoOperativo = dr.GetString(dr.GetOrdinal("STATOOPERATIVO")),
                                Utente = dr.GetString(dr.GetOrdinal("UTENTE")),
                                NoteCompletamento = dr.GetString(dr.GetOrdinal("NOTECOMPLETAMENTO")),
                                FrequenzaCod_Descrizione = dr.GetString(dr.GetOrdinal("FREQUENZACOD_DESCRIZIONE")), //--todo
                                //ComponentiManutenzioneClob = Convert.ToString(((System.Data.OracleClient.OracleLob)(dr.GetOracleValue(ordClob))).Value);
                                //ComponentiManutenzione = dr.GetOracleLob(dr.GetOrdinal("ComponentiManutenzione")).Value.ToString(),
                                ComponentiManutenzione = dr.GetString(dr.GetOrdinal("ComponentiManutenzione")),  // ######   DEVE ESSERERE UN CLOB DI TESTO!!!!
                                //ComponentiSostegno = dr.GetString(dr.GetOrdinal("COMPONENTI_SOSTEGNO")),

                                Annotazioni = dr.GetString(dr.GetOrdinal("Annotazioni")),
                                OidCategoria = dr.GetInt32(dr.GetOrdinal("OIDCATEGORIA")),
                                Anno = dr.GetInt32(dr.GetOrdinal("ANNO")),
                                Mese = dr.GetInt32(dr.GetOrdinal("MESE")),
                                Settimana = dr.GetInt32(dr.GetOrdinal("SETTIMANA")),
                                //AreaDiPolo = dr.GetString(dr.GetOrdinal("AREADIPOLO")),
                                RefAmministrativo = dr.GetString(dr.GetOrdinal("REFAMMINISTRATIVO")),
                                CorpoMP = dr.GetString(dr.GetOrdinal("CORPOMP")),
                                OrdinePasso = dr.GetString(dr.GetOrdinal("ORDINEPASSO")) //--todo

                            };
                            objects.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; " + ex.Message));
            }
            return objects;
        }

        public string CreaNuovoControlloNormativo(int OidControlloNormativo)
        {
            var msg = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "PK_CONTROLLI_NORMATIVI_TXT.SHIFT_CTRL_NORM";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidControlloNormativo", SqlDbType.Int) { Value = OidControlloNormativo };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("omessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    SqlConn.Open();
                    command.ExecuteNonQuery();

                    msg = command.Parameters["omessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }


        public string CreaImpiantobyImpiantoLibrary(int OidImpianto, int NumClone, int OidLibraryImpianto) //string Descrizione,ioidcommessa,ioidedificio,ioidpiano, izona => :izona,iuser => :iuser,//                       oerrormsg => :oerrormsg);)
        {
            var msg = string.Empty;

            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_impianto.creaimpiantobylinbrary";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("ioidimpianto", SqlDbType.Int) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("inumclone", SqlDbType.Int) { Value = NumClone };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("ioidlibraryimpianto", SqlDbType.Int) { Value = OidLibraryImpianto };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("omessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);


                    SqlConn.Open();
                    command.ExecuteNonQuery();
                    msg = command.Parameters["oMessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }


        public string GetRegMisureDettaglio(string UserNameCorrente, int OidRegMisure, int OidImpianto)
        {
            var msg = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandText = "pk_misure.popola_det_misure";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("ireg_misure", SqlDbType.Int) { Value = OidRegMisure };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("iimpianto", SqlDbType.Int) { Value = OidImpianto };
                    command.Parameters.Add(pp);

                    pp = new SqlParameter("omessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);

                    SqlConn.Open();
                    command.ExecuteNonQuery();


                    msg = command.Parameters["omessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }


        /// <param name="Oidpmp"></param>

        public string CloneEdificio(string UserNameCorrente, int OidEdificio, string CodEdificio, string descrizione, int OidIndirizzo, ref int OidNewEdificio)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_edificio.clona_edificio";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidEdificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    //pp = new SqlParameter("iSession", SqlDbType.VarChar, 200) { Value = Classi.SetVarSessione.CodSessioneWeb };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    pp = new SqlParameter("iUser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iCodEdificio", SqlDbType.VarChar, 200) { Value = CodEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iDescrizione", SqlDbType.VarChar, 2000) { Value = descrizione };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iOidIndirizzo", SqlDbType.Int) { Value = OidIndirizzo };
                    command.Parameters.Add(pp);
                    pp = null;  //inewEdificio
                    pp = new SqlParameter("oNewEdificio", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    try
                    {
                        OidNewEdificio = int.Parse(command.Parameters["oNewEdificio"].Value.ToString());
                    }
                    catch
                    {
                        OidNewEdificio = 0;
                    }

                    msg = command.Parameters["oErrorMsg"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }
        public string CloneApparato(string UserNameCorrente, int OidApparato, uint clone, string descrizione)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_apparato.clona_apparato";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidApparato", SqlDbType.Int) { Value = OidApparato };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iNumClone", SqlDbType.Int) { Value = clone };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iUser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iDescrizione", SqlDbType.VarChar, 200) { Value = descrizione };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    msg = command.Parameters["oErrorMsg"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente{0} \r \n {1};", ex.Message.ToString(), ex.StackTrace.ToString()));
            }
            return msg;
        }
        public string CloneImpianto(string UserNameCorrente, int OidImpianto, string CodImpianto, string descrizione)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_impianto.clona_impianto";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOidImpianto", SqlDbType.Int) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iCodImpianto", SqlDbType.VarChar, 200) { Value = CodImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iUser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iDescrizione", SqlDbType.VarChar, 200) { Value = descrizione };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oNewImpianto", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    msg = command.Parameters["oErrorMsg"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }

        public string CloneDestinatario(string UserNameCorrente, int OidDestinatario, string email, string nome, string cognome)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.clona_destinatario";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("ioid", SqlDbType.Int) { Value = OidDestinatario };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iEmail", SqlDbType.VarChar, 200) { Value = email }; //Value = clone 
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iNome", SqlDbType.VarChar, 200) { Value = nome };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iCognome", SqlDbType.VarChar, 200) { Value = cognome };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    msg = command.Parameters["oMessaggio"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }


        //public DataView GetDatiInvioMailControlloNormativo(string UserNameCorrente, int OidControlloNormativo,
        //    int OidEdificio, DateTime Data)
        //{
        //    //pk_controlli_normativi_txt.get_cn_x_email_by_bl(oidedificio => :oidedificio,
        //    //                          idatarif => :idatarif,   io_cursor => :io_cursor,   omessagg => :omessagg);
        //    var dv = new DataView();
        //    var dt = new DataTable();
        //    var msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_controlli_normativi_txt.get_cn_x_email_by_bl";
        //            command.CommandType = CommandType.StoredProcedure;

        //            SqlParameter pp = new SqlParameter("oidcontrollonormativo", SqlDbType.Int) { Value = OidControlloNormativo };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("oidedificio", SqlDbType.Int) { Value = OidEdificio };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("idatarif", SqlDbType.DateTime) { Value = Data };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);

        //            pp = new SqlParameter("omessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
        //            dt.Load((System.Data.IDataReader)dr);
        //            dv = new DataView(dt);

        //            msg = command.Parameters["omessage"].Value.ToString();
        //            if (msg != null && !msg.Equals(string.Empty))
        //            {
        //                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //    return dv;
        //}


        public string AggiornaTempi(int Oid, string type)
        {
            var msg = string.Empty;
            try
            {
                using (var SqlConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = SqlConn.CreateCommand();
                    command.CommandTimeout = 300;
                    command.CommandText = "PK_AGGIORNA_TEMPI.AGGIORNA_TEMPI";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new SqlParameter("iOid", SqlDbType.Int) { Value = Oid };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("iTipo", SqlDbType.VarChar) { Value = type };
                    command.Parameters.Add(pp);
                    pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    SqlConn.Open();
                    command.ExecuteNonQuery();
                    msg = command.Parameters["oMessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }



        //public string InserisciNotificaEmergenza(int iOidRegNotificaEmergenza, int OidRisorsaTeam, int OidRisorsaCapo)
        //{

        //    try
        //    {
        //        using (SqlConnection OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            SqlCommand command = OrclConn.CreateCommand();
        //            //Store Procedure
        //            /*
        //             * procedure CREA_EMERGENZE(iOidRegNotificaEmergenza IN  number,
        //                               iRisorseTeamList         IN  number,
        //                               iRisorsa                 IN  number,
        //                               oMessage                 OUT varchar2) 
        //             * */
        //            command.CommandText = "pk_emergenze.crea_emergenze";
        //            command.CommandType = CommandType.StoredProcedure;
        //            //----------------------------
        //            //------------------------------
        //            SqlParameter pp = new SqlParameter("ioidregnotificaemergenza", SqlDbType.Int) { Value = iOidRegNotificaEmergenza };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("irisorseteamlist", SqlDbType.Int) { Value = OidRisorsaTeam };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("iRisorsa", SqlDbType.Int) { Value = OidRisorsaCapo };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            //-----------
        //            string Messaggi = command.Parameters["omessage"].Value.ToString();
        //            if (Messaggi != null && !Messaggi.Equals(""))
        //            {
        //                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));

        //    }
        //    return "";
        //}

        //public FileData getFileFromViewId(string viewId)
        //{

        //    try
        //    {
        //        using (SqlConnection OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            SqlCommand command = OrclConn.CreateCommand();

        //            command.CommandText = "pk_help.getFileFromViewId";
        //            command.CommandType = CommandType.StoredProcedure;
        //            //----------------------------
        //            //------------------------------
        //            SqlParameter pp = new SqlParameter("iViewId", SqlDbType.VarChar, 200) { Value = viewId };
        //            command.Parameters.Add(pp);
        //            pp = null;


        //            pp = new SqlParameter("oFile", SqlDbType.Blob) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            //-----------
        //            return (FileData)command.Parameters["oFile"].Value;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //        return null;
        //    }
        //    //return null;
        //}


        #region Get Dati per clonare i ruoli da latro DB


        public DataView GetDatiTipi(string FullNomeClasse, int OidEdificio, int Nuovo)
        {
            //pk_controlli_normativi_txt.get_cn_x_email_by_bl(oidedificio => :oidedificio,
            //                          idatarif => :idatarif,   io_cursor => :io_cursor,   omessagg => :omessagg);
            var dv = new DataView();
            var dt = new DataTable();
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.GetListTipi";
                    //pk_ruolouser.getlisttipi(fnomeclasse => :fnomeclasse,
                    //     io_cursor => :io_cursor,
                    //     omessage => :omessage);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("ifnomeclasse", SqlDbType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("ioidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iNuovo", SqlDbType.Int) { Value = Nuovo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = Classi.SetVarSessione.CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;


                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    //var dr = command.ExecuteReader();
                    command.ExecuteNonQuery();

                    //var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);
                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    dv = new DataView(dt);

                    msg = command.Parameters["omessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return dv;
        }


        public DataView GetDatiPermessiMebri(string FullNomeClasse, int OidEdificio, int Nuovo)
        {
            //pk_controlli_normativi_txt.get_cn_x_email_by_bl(oidedificio => :oidedificio,
            //                          idatarif => :idatarif,   io_cursor => :io_cursor,   omessagg => :omessagg);
            var dv = new DataView();
            var dt = new DataTable();
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.getlistmembri";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("ifnomeclasse", SqlDbType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("ioidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iNuovo", SqlDbType.Int) { Value = Nuovo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = Classi.SetVarSessione.CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;


                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    //var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    dv = new DataView(dt);


                    msg = command.Parameters["omessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return dv;
        }


        public DataView GetDatiPermessiOggetti(string FullNomeClasse, int OidEdificio, int Nuovo)
        {
            //pk_controlli_normativi_txt.get_cn_x_email_by_bl(oidedificio => :oidedificio,
            //                          idatarif => :idatarif,   io_cursor => :io_cursor,   omessagg => :omessagg);
            var dv = new DataView();
            var dt = new DataTable();
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.getlistoggetti";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("ifnomeclasse", SqlDbType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("ioidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iNuovo", SqlDbType.Int) { Value = Nuovo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = Classi.SetVarSessione.CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;


                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    //var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);
                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);

                    dv = new DataView(dt);

                    msg = command.Parameters["omessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return dv;
        }


        public string SetMebri(string FullNomeClasse, string strMembri)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.setlistmembri";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("ifnomeclasse", SqlDbType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("isrtmembri", SqlDbType.VarChar, 4000) { Value = strMembri };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("omessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();


                    msg = command.Parameters["omessage"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }


        public string GetMessaggioDestImpostati(string iuser, int iavvisispedizionioid)
        {
            var msg = string.Empty;
            var msgdest= string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "PK_MAIL.GETDESTINATARIMAIL_BYRDL_STR";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iuser", SqlDbType.VarChar, 50) { Value = iuser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iavvisispedizionioid", SqlDbType.Int) { Value = iavvisispedizionioid };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oemail", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("osms", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new SqlParameter("oerrormsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();


                    msg = command.Parameters["oerrormsg"].Value.ToString();
                    msgdest = "Email:"+command.Parameters["oemail"].Value.ToString() + "- Sms:" + command.Parameters["osms"].Value.ToString();

                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msgdest;

            return msg;
        }



        #endregion


        /// <param name="Oidpmp"></param>



        public string SetProblemaCausaRimedioDefault(int OidApparatoProblema) //string Descrizione,ioidcommessa,ioidedificio,ioidpiano, izona => :izona,iuser => :iuser,//                       oerrormsg => :oerrormsg);)
        {
            var msg = string.Empty;
            //pk_albero_guasti.setalberodefault(ioid => :ioid,  omessaggio => :omessaggio);
            try
            {
                //using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                //{
                //    var command = OrclConn.CreateCommand();
                //    command.CommandText = "pk_impianto.creaimpiantobylinbrary";
                //    command.CommandType = CommandType.StoredProcedure;
                //    var pp = new SqlParameter("ioidimpianto", SqlDbType.Int) { Value = OidImpianto };
                //    command.Parameters.Add(pp);
                //    pp = null;
                //    pp = new SqlParameter("inumclone", SqlDbType.Int) { Value = NumClone };
                //    command.Parameters.Add(pp);
                //    pp = null;
                //    pp = new SqlParameter("ioidlibraryimpianto", SqlDbType.Int) { Value = OidLibraryImpianto };
                //    command.Parameters.Add(pp);
                //    pp = null;
                //    //pp = new SqlParameter("idescrizione", SqlDbType.Int) { Value = Oid };
                //    //command.Parameters.Add(pp);
                //    //pp = null;
                //    //pp = new SqlParameter("ioidcommessa", SqlDbType.Int) { Value = Oid };
                //    //command.Parameters.Add(pp);
                //    //pp = null;
                //    //pp = new SqlParameter("ioidedificio", SqlDbType.Int) { Value = Oid };
                //    //command.Parameters.Add(pp);
                //    //pp = null;
                //    //pp = new SqlParameter("ioidpiano", SqlDbType.Int) { Value = Oid };
                //    //command.Parameters.Add(pp);
                //    //pp = null;
                //    //pp = new SqlParameter("ioidpiano", SqlDbType.Int) { Value = Oid };
                //    //command.Parameters.Add(pp);
                //    //pp = null;
                //    //pp = new SqlParameter("izona", SqlDbType.Int) { Value = Oid };
                //    //command.Parameters.Add(pp);
                //    //pp = null;
                //    //pp = new SqlParameter("iuser", SqlDbType.Int) { Value = Oid };
                //    //command.Parameters.Add(pp);
                //    //pp = null;
                //    pp = new SqlParameter("oMessage", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                //    command.Parameters.Add(pp);
                //    pp = null;

                //    OrclConn.Open();
                //    command.ExecuteNonQuery();
                //    msg = command.Parameters["oMessage"].Value.ToString();
                //    if (msg != null && !msg.Equals(string.Empty))
                //    {
                //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return msg;
        }


        //public string SetParContatoreAppMP(int DataContatoreOid, string TipoParametro, string valore)
        //{
        //    var msg = string.Empty;
        //    //pk_albero_guasti.setalberodefault(ioid => :ioid,  omessaggio => :omessaggio);
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mpplanner.setparcontatoreappmp";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new SqlParameter("idatacontatoreoid", SqlDbType.Int) { Value = DataContatoreOid };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("itipoparametro", SqlDbType.VarChar) { Value = TipoParametro };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("ivalore", SqlDbType.VarChar) { Value = valore };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            //pp = new SqlParameter("idescrizione", SqlDbType.Int) { Value = Oid };

        //            pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();
        //            msg = command.Parameters["oMessaggio"].Value.ToString();
        //            if (msg != null && !msg.Equals(string.Empty))
        //            {
        //                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //    return msg;
        //}



        #region MAIL - data da DB ( gestione da pop3 legge mail crea mail tramette meail)

        public string InsertSegnalazioneMail(string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,
                                            string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string messaggio = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.inssegnalazionemail";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iemailFrom", SqlDbType.VarChar, 100) { Value = emailFrom };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("iemailTo", SqlDbType.VarChar, 100) { Value = emailTo };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("iemailDate", SqlDbType.VarChar, 100) { Value = emailDate };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("iemailSubject", SqlDbType.VarChar, 300) { Value = emailSubject };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("iemailBody", SqlDbType.VarChar, 4000) { Value = emailBody };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("iemailReceived", SqlDbType.VarChar, 100) { Value = emailReceived };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("iemailMessageID", SqlDbType.VarChar, 100) { Value = emailMessageID };
                    command.Parameters.Add(pp);
                    ///      string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione
                    pp = null;
                    pp = new SqlParameter("istrEdificio", SqlDbType.VarChar, 300) { Value = strEdificio };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("istrAvviso", SqlDbType.VarChar, 4000) { Value = strAvviso };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("istrAssemblaggio", SqlDbType.VarChar, 100) { Value = strAssemblaggio };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new SqlParameter("istrDescrizione", SqlDbType.VarChar, 100) { Value = strDescrizione };
                    command.Parameters.Add(pp);

                    ///

                    pp = null;
                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public DataTable GetRdLSegnalazioneMail(string CorrenteUser, string searchText) //string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string Messaggi = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            //Da rifare tutta

            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    //                    -- Call the procedure
                    //pk_mail.getrdlseg_send_mail(oerrormsg => :oerrormsg,
                    //                            io_cursor => :io_cursor);
                    //                    -- Call the procedure
                    //pk_mail.getrdlseg_send_mail(iuser => :iuser,
                    //                            isearchtext => :isearchtext,
                    //                            oerrormsg => :oerrormsg,
                    //                            io_cursor => :io_cursor);
                    SqlCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.getrdlseg_send_mail";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iuser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("isearchtext", SqlDbType.VarChar, 4000) { Value = searchText };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oerrormsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);

                    dv = new DataView(dt);

                    Messaggi = command.Parameters["oerrormsg"].Value.ToString();



                    //OrclConn.Open();
                    //command.ExecuteNonQuery();
                    //var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    //dt.Load((System.Data.IDataReader)dr);
                    //dv = new DataView(dt);
                    //var Messaggi = command.Parameters["oerrormsg"].Value.ToString();
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

        public DataTable GetDestinatariMail_RdLSegnalazione(string CorrenteUser, int OidEdificio) //string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string messaggio = string.Empty;

            var dt = new DataTable("DestinatariMailSg");
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    //pk_mail.getdestinatarimail(iuser => :iuser,
                    //                           ioidedificio => :ioidedificio,
                    //                           oerrormsg => :oerrormsg,
                    //                           io_cursor => :io_cursor);
                    SqlCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.getdestinatarimail";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iuser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("ioidedificio", SqlDbType.Int) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oerrormsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    //var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    //dt.Load((System.Data.IDataReader)dt);

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

        public DataTable GetDestinatariMail_TicketAssistenza(string CorrenteUser, int OidTicketAssistenza) //string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string messaggio = string.Empty;

            var dt = new DataTable("DestinatariMailTicketAssistenza");
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    //pk_mail.getdestinatarimail(iuser => :iuser,
                    //                           ioidedificio => :ioidedificio,
                    //                           oerrormsg => :oerrormsg,
                    //                           io_cursor => :io_cursor);
                    SqlCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestMailTicketAss";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iuser", SqlDbType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("OidTicketAssistenza", SqlDbType.Int) { Value = OidTicketAssistenza };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("oerrormsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);
                    OrclConn.Open();
                    //command.ExecuteNonQuery();
                    //var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;

                    // fp: create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);

                    // dt.Load((System.Data.IDataReader)dr);

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


        public string SetStatoTrasmesso(string UserNameCorrente, int OidRdL, ref string Esito)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.SetStatoTrasmesso";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("irdl", SqlDbType.Int) { Value = OidRdL };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oesito", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    Esito = command.Parameters["oesito"].Value.ToString();

                    msg = command.Parameters["oErrorMsg"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }

                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                //  cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            return Esito;
            #endregion
        }


        //public string SetStatoTrasmessoTicket(string UserNameCorrente, int OidRdL, ref string Esito)
        //{
        //    var msg = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mail.SetStatoTrasmessoTicket";
        //            command.CommandType = CommandType.StoredProcedure;

        //            SqlParameter pp = new SqlParameter("iUser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new SqlParameter("OidTicketAssistenza", SqlDbType.Int) { Value = OidRdL };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            //pp = new SqlParameter("oesito", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            //command.Parameters.Add(pp);
        //            //pp = null;

        //            //pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            //command.Parameters.Add(pp);
        //            //pp = null;

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            //Esito = command.Parameters["oesito"].Value.ToString();

        //            //msg = command.Parameters["oErrorMsg"].Value.ToString();
        //            //if (msg != null && !msg.Equals(string.Empty))
        //            //{
        //            //    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
        //            //}

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
        //        //  cl.TxtLogSpedizioni(msg, true);
        //        throw new Exception(msg);
        //    }
        //    return Esito;

        //}






        public string SechedulaPOI(string UserNameCorrente, int OidImpianto, enTrimestre tr, DateTime DataConferma)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.SetStatoTrasmesso";
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iOidImpianto", SqlDbType.Int) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oesito", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oErrorMsg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    //Esito = command.Parameters["oesito"].Value.ToString();

                    msg = command.Parameters["oErrorMsg"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }

                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                //  cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            return msg;

        }


        public System.ComponentModel.BindingList<CAMS.Module.DBTask.POI.DCListPOI> GetPOISchedulato
            (string UserNameCorrente, int OidImpianto, int Anno, DateTime DataConferma)
        {
            var msg = string.Empty;
            System.ComponentModel.BindingList<CAMS.Module.DBTask.POI.DCListPOI> objects
                               = new System.ComponentModel.BindingList<CAMS.Module.DBTask.POI.DCListPOI>();

            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    //pk_poi.getpoi_data_conferma(iimpianto => :iimpianto,
                    //         ianno => :ianno,
                    //         idataconferma => :idataconferma,
                    //         io_cursor => :io_cursor,
                    //         omessagg => :omessagg);

                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_poi.getpoi_data_conferma";//  poi
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 300;
                    //SqlParameter pp = new SqlParameter("iuser", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    SqlParameter pp = new SqlParameter("iimpianto", SqlDbType.Int) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("ianno", SqlDbType.Int) { Value = Anno };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("idataconferma", SqlDbType.DateTime) { Value = DataConferma };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("omessagg", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    //pp = new SqlParameter("io_cursor", SqlDbType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    OrclConn.Open();
                    var dr = command.ExecuteReader();

                    string kk = "";

                    msg = command.Parameters["omessagg"].Value != null ? command.Parameters["omessagg"].Value.ToString() : string.Empty;
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }

                    // var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    if (dr.HasRows)
                    {
                        int Newid = 0;
                        while (dr.Read())
                        {
                            CAMS.Module.DBTask.POI.DCListPOI obj = new CAMS.Module.DBTask.POI.DCListPOI()
                            {
                                ID = Newid++,
                                Descrizione = dr.GetString(dr.GetOrdinal("DESCRIZIONE")),
                                Immobile = dr.GetString(dr.GetOrdinal("IMMOBILE")),
                                Piano = dr.GetString(dr.GetOrdinal("PIANO")),
                                Stanza = dr.GetString(dr.GetOrdinal("STANZA")),
                                Reparto = dr.GetString(dr.GetOrdinal("REPARTO")),
                                Impianto = dr.GetString(dr.GetOrdinal("IMPIANTO")),
                                Apparato = dr.GetString(dr.GetOrdinal("APPARATO")),
                                CodApparato = dr.GetString(dr.GetOrdinal("CODAPPARATO")),
                                TipoApparato = dr.GetString(dr.GetOrdinal("TIPOAPPARATO")),
                                Categoria = dr.GetString(dr.GetOrdinal("CATEGORIA")),
                                CodProcedura = dr.GetString(dr.GetOrdinal("CODPROCEDURA")),
                                Frequenza = dr.GetString(dr.GetOrdinal("FREQUENZA")),
                                Gennaio = dr.IsDBNull(dr.GetOrdinal("GENNAIO")) ? string.Empty : dr.GetString(dr.GetOrdinal("GENNAIO")),
                                Febbraio = dr.IsDBNull(dr.GetOrdinal("FEBBRAIO")) ? string.Empty : dr.GetString(dr.GetOrdinal("FEBBRAIO")),
                                Marzo = dr.IsDBNull(dr.GetOrdinal("MARZO")) ? string.Empty : dr.GetString(dr.GetOrdinal("MARZO")),
                                Aprile = dr.IsDBNull(dr.GetOrdinal("APRILE")) ? string.Empty : dr.GetString(dr.GetOrdinal("APRILE")),
                                Maggio = dr.IsDBNull(dr.GetOrdinal("MAGGIO")) ? string.Empty : dr.GetString(dr.GetOrdinal("MAGGIO")),
                                Giugno = dr.IsDBNull(dr.GetOrdinal("GIUGNO")) ? string.Empty : dr.GetString(dr.GetOrdinal("GIUGNO")),
                                Luglio = dr.IsDBNull(dr.GetOrdinal("LUGLIO")) ? string.Empty : dr.GetString(dr.GetOrdinal("LUGLIO")),
                                Agosto = dr.IsDBNull(dr.GetOrdinal("AGOSTO")) ? string.Empty : dr.GetString(dr.GetOrdinal("AGOSTO")),
                                Settembre = dr.IsDBNull(dr.GetOrdinal("SETTEMBRE")) ? string.Empty : dr.GetString(dr.GetOrdinal("SETTEMBRE")),
                                Ottobre = dr.IsDBNull(dr.GetOrdinal("OTTOBRE")) ? string.Empty : dr.GetString(dr.GetOrdinal("OTTOBRE")),
                                Novembre = dr.IsDBNull(dr.GetOrdinal("NOVEMBRE")) ? string.Empty : dr.GetString(dr.GetOrdinal("NOVEMBRE")),
                                Dicembre = dr.IsDBNull(dr.GetOrdinal("DICEMBRE")) ? string.Empty : dr.GetString(dr.GetOrdinal("DICEMBRE")),
                                DurataAttivita = dr.GetString(dr.GetOrdinal("DURATAATTIVITA")),
                                MaterialeUtilizzato = dr.GetString(dr.GetOrdinal("MATERIALEUTILIZZATO")),
                                Note = dr.GetString(dr.GetOrdinal("NOTE")),
                                Priorita = dr.GetString(dr.GetOrdinal("PRIORITA")),
                                PrioritaIntervento = dr.GetString(dr.GetOrdinal("PRIORITAINTERVENTO")),
                                Trimestre = dr.GetString(dr.GetOrdinal("TRIMESTRE")),
                                Anno = dr.GetInt32(dr.GetOrdinal("ANNO")).ToString()

                            };

                            objects.Add(obj);

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                //  cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }

            return objects;

        }

        ///
        public string SetCambioPassword(string UserNameCorrente, string OldPassword, DateTime DataConferma, int Flag)
        {
            var Messaggio = "Non Vedi?.";   
            try
            {
                using (var OrclConn = new SqlConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new SqlCommand("PK_ACCOUNT.SetCambioPassword", OrclConn);  ////[PK_ACCOUNT].SetCambioPassword
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pp = new SqlParameter("iUtente", SqlDbType.VarChar, 200) { Value = UserNameCorrente };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iOldPassword", SqlDbType.VarChar, 200) { Value = OldPassword };
                    cmd.Parameters.Add(pp);
                    pp = null;   
                    pp = new SqlParameter("iDataUpdate", SqlDbType.DateTime) { Value = DataConferma };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new SqlParameter("iFlag", SqlDbType.Int) { Value = Flag };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new SqlParameter("oMessaggio", SqlDbType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["oMessaggio"].Value.ToString();
                }
                return Messaggio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggio;
        }


    }
}
