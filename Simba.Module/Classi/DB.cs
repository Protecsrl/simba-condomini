using CAMS.Module.DBAudit.DC;
using CAMS.Module.DBTask.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using CAMS.Module.ClassiMSDB;
using CAMS.Module.DBControlliNormativi;
using CAMS.Module.ClassiORADB;

namespace CAMS.Module.Classi
{
    public class DB : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        //public string GetRuoloCorrente(string UserNameCorrente)
        //{
        //    try
        //    {
        //        using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_ruolouser.getapparencerule";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new OracleParameter("iUserNameCorrente", OracleType.VarChar, 100);
        //            pp.Value = UserNameCorrente;
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new OracleParameter("oRuolo", OracleType.VarChar, 100);
        //            // OracleType.
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);
        //            pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000);
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

        public int GetMasionebySkillBase(string UserNameCorrente, int OidSkillBase)
        {
            var OidManisone = 0;
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_pmp_util.get_mansione";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iUserNameCorrente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iOidSkill", OracleType.VarChar, 15) { Value = OidSkillBase };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oOidMansione", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };


                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    OidManisone = int.Parse(command.Parameters["oOidMansione"].Value.ToString());
                    var Messaggi = command.Parameters["oErrorMsg"].Value.ToString();
                    if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", Messaggi));
                    }

                    //for (int i = 0; i < 3; i++)
                    //{
                    //    (command.Parameters["parametrocollection"].Value as Array).GetValue(i);
                    //}

                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return OidManisone;
        }

        public double GetDefault(string UserNameCorrente, string kTable, int OidEqstd)
        {
            double Codk = 1;
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_pmp_util.getkdefault";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iUserNameCorrente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iktable", OracleType.VarChar) { Value = kTable };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioideqstd", OracleType.Number) { Value = OidEqstd };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("odefault", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    Codk = double.Parse(command.Parameters["odefault"].Value.ToString());
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
            return Codk;
        }

        public string GetCodicePMP(string UserNameCorrente, int OidCategoria, int OidEqstd, int OidSistema)
        {
            var CodPMP = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_pmp_util.get_cod_pmp";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iUserNameCorrente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioidcategoria", OracleType.Number) { Value = OidCategoria };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioideqstd", OracleType.Number) { Value = OidEqstd };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioidsistema", OracleType.Number) { Value = OidSistema };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ocodpmp", OracleType.VarChar, 20) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    CodPMP = command.Parameters["ocodpmp"].Value.ToString();
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
            return CodPMP;
        }


        //procedure AggiornaScenarioinEdImpAppSk(iRegPiano   in number,    iAzione     in varchar2,
        //                                 iUtente     in varchar2,      iDataUpdate in date,    oMessaggio  out varchar2) is
        public string setScenarioinEdificioImpiantoApparatoAppSk(int RegPiano, string Azione)
        {
            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            var Messaggio = "Non Vedi?.";
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.setScenarioinEdificioImpiantoApparatoAppSk(RegPiano, Azione);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.setScenarioinEdificioImpiantoApparatoAppSk(RegPiano, Azione);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_aggiorna_sk.aggiornascenarioinedimpappsk", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iRegPiano", OracleType.Number) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iazione", OracleType.VarChar, 200) { Value = Azione };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUtente", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iDataUpdate", OracleType.DateTime) { Value = DateTime.Now };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
        //        using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            var cmd = new OracleCommand("pk_aggiorna_sk.check_scenario", OrclConn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            var pp = new OracleParameter("impregpianificazione", OracleType.Number) { Value = RegPiano };
        //            cmd.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            cmd.Parameters.Add(pp);
        //            pp = null;
        //            //pp = new OracleParameter("oBool", OracleType.Int32) { Direction = ParameterDirection.Output };
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

        public int GetScenarioInseribilesuRegistroPiano(int Scenario)
        {
            string Messaggio = string.Empty;
            int OutNumber = 0;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetScenarioInseribilesuRegistroPiano(Scenario);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetScenarioInseribilesuRegistroPiano(Scenario);
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_aggiorna_sk.check_scenario_ins_mpreg", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iscenario", OracleType.Number) { Value = Scenario };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("outnumber", OracleType.Number) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

            var Messaggio = "Non Vedi?.";
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetParContatoreAppMP(OidDataContatore);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetParContatoreAppMP(OidDataContatore);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.setparcontatoreappmp", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new OracleParameter("idatacontatoreoid", OracleType.Number) { Value = OidDataContatore };
                    cmd.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        //pk_mpplanner.aggiornaletturecontatore(idcontatoreoid => :idcontatoreoid,
        //                              idata => :idata,
        //                              ivalore => :ivalore,
        //                              ioresettimana => :ioresettimana,
        //                              omessaggio => :omessaggio);
        // pippo + pluto 



        public string AggiornaLettureContatore(int OidDataContatore, DateTime Data, int Valore, int OreSettimana)
        {

            var Messaggio = "Non Vedi?.";
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.AggiornaLettureContatore(OidDataContatore, Data, Valore, OreSettimana);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.AggiornaLettureContatore(OidDataContatore, Data, Valore, OreSettimana);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.aggiornaletturecontatore", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("idcontatoreoid", OracleType.Number) { Value = OidDataContatore };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idata", OracleType.DateTime) { Value = Data };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ivalore", OracleType.Number) { Value = Valore };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioresettimana", OracleType.Number) { Value = OreSettimana };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

            //var Messaggio = "Non Vedi?.";
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetVerificaEsitoAzione(RegPiano, Stato, AzioneChiamante);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetVerificaEsitoAzione(RegPiano, Stato, AzioneChiamante);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.VerificaEsitoAzione", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new OracleParameter("iRegPiano", OracleType.Number) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iazione", OracleType.VarChar, 200) { Value = Stato.ToString() };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iazionechiamante", OracleType.VarChar, 200) { Value = AzioneChiamante };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oVerifica", OracleType.Number) { Direction = ParameterDirection.Output };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.EseguiAzione(RegPiano, Azione, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.EseguiAzione(RegPiano, Azione, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            //commento nuovo
            ///   mmmmmmmmm
            ///   stotototototototot    opppppppppppppppppppppppppppp
            ///   nulla   not null
            try
            {

                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.eseguiazione", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new OracleParameter("iRegPiano", OracleType.Number) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iazione", OracleType.VarChar, 200) { Value = Azione };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iUtente", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("omessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

            //var Messaggio = "Non Vedi?.";
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetVisibileTastiAzione(RegPiano, Stato);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetVisibileTastiAzione(RegPiano, Stato);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.GetVisibileTastiAzione", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new OracleParameter("iRegPiano", OracleType.Number) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iStato", OracleType.VarChar, 200) { Value = Stato.ToString() };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oVerifica", OracleType.VarChar, 5) { Direction = ParameterDirection.Output };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.aggiornaRegMPSchedeMP(RegPiano);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.aggiornaRegMPSchedeMP(RegPiano);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }





            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("PK_MPPLANNER.aggiornaRegMPSchedeMP", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("oidmpregpianificazione", OracleType.Number) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    //pp = new OracleParameter("oBool", OracleType.Int32) { Direction = ParameterDirection.Output };
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
        public string CalcolaKPIMTBF(DateTime DataIn, DateTime DataOut)
        {
            var Messaggio = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_aggiorna_kpi.lancioupdkpimtbf", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("datein", OracleType.DateTime) { Value = DataIn };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("dateout", OracleType.DateTime) { Value = DataOut };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("p_msg_out", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Messaggio = cmd.Parameters["p_msg_out"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggio;
        }


        public string GetRisorseLiberedaAssociare(int iRegPiano, string UserNameCorrente)
        {
            var Criterio = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.getrisorsedaassociare", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new OracleParameter("iRegPiano", OracleType.Number) { Value = iRegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUtente", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oCriteria", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    Criterio = cmd.Parameters["ocriteria"].Value.ToString();
                }
                return Criterio;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Criterio;
        }


        public System.Data.DataTable GetTRisorseLiberedaAssociareByMansione(int iGhostID)
        {
            var Criterio = string.Empty;
            DataTable dtable = new DataTable("Risorse");
            DataView dvrow = new DataView();

            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetTRisorseLiberedaAssociareByMansione(iGhostID);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetTRisorseLiberedaAssociareByMansione(iGhostID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }



            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.GetTRisorsedaAssociareManSki", OrclConn) { CommandType = CommandType.StoredProcedure };

                    OracleParameter pp = new OracleParameter("iGhost", OracleType.Number) { Value = iGhostID };
                    Guard.ArgumentNotNull(pp, "pp");
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pp);

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();

                    var dr = (OracleDataReader)cmd.Parameters["io_cursor"].Value;
                    dtable.Load((System.Data.IDataReader)dr);
                    // System.Data.IDataReader vv = (System.Data.IDataReader)dr;

                    //dvrow = new DataView(dtable);
                    //   dvrow = dtable.DefaultView;
                    //var oissel = dr.Cast<string>();
                    //Array ar = new Array(string) ;
                    //dt.Rows.CopyTo(ar, 0);

                    //  var objMPPBLListSelects = new ArrayList(dt.Rows);
                    //pp = new OracleParameter("oCriteria", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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



        public string AssociazioneCaricoCapacita(int IdRisorseTeam, int IdGhosts, string UserNameCorrente) ///string TipoAzione,
        {
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.associazionecaricocapacita"; ///pk_mpasscaricocapacita.associazionecaricocapacita
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsaTeam", OracleType.Number) { Value = IdRisorseTeam };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iOIdGhost", OracleType.Number) { Value = IdGhosts };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

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
            return string.Empty;
        }


        //  crea ordine di laroro 
        //begin
        //-- Call the procedure  pk_mpregpianificazione.creaodlghost(iregpiano => :iregpiano,
        //                     ighost => :ighost,     iutente => :iutente,          pmessaggio => :pmessaggio);
        public string CreaRegRdLbyGhost(int iRegPiano, int IdGhosts) ///string TipoAzione,
        {
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    // command.CommandText = "pk_mpregpiani_odl.crea_rdl_gg_pianifica";
                    command.CommandText = "pk_mpregpiani_odl.mpghost_rdl_lancio";
                    command.CommandType = CommandType.StoredProcedure;
                    //var pp = new OracleParameter("iRegPiano", OracleType.Number) { Value = iRegPiano };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    OracleParameter pp = new OracleParameter("impghost", OracleType.Number) { Value = IdGhosts };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iusername", OracleType.VarChar, 100) { Value = SetVarSessione.CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var Messaggi = command.Parameters["omessage"].Value.ToString();
                    if (Messaggi != null && !Messaggi.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB ({0}) non eseguita correttamente; {1}", "pk_mpregpiani_odl.mpghost_rdl_lancio", Messaggi));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
            }
            return string.Empty;
        }



        //AssegnaInEmergenzaRegRdL
        public string AggiornaRdLbySSmistamento(int OidRegRdL, string Tipo)
        {
            //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);
            var Msg = string.Empty;

            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.AggiornaRdLbySSmistamento(OidRegRdL, Tipo);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.AggiornaRdLbySSmistamento(OidRegRdL, Tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.AggiornaRdLbySSmistamento", OrclConn) { CommandType = CommandType.StoredProcedure };
                    //                                                  AggiornaRdLbySSmistamento
                    var pp = new OracleParameter("iOidRegRdl", OracleType.Number) { Value = OidRegRdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iTipo", OracleType.VarChar, 100) { Value = Tipo };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iUtente", OracleType.VarChar, 1000) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
        public string AggiornaRdLbyObjSpace(int OidRegRdL, string Tipo)
        {
            //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);
            var Msg = string.Empty;

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.AggiornaRdLbySSmistamento", OrclConn) { CommandType = CommandType.StoredProcedure };

                    var pp = new OracleParameter("iOidRegRdl", OracleType.Number) { Value = OidRegRdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iTipo", OracleType.VarChar, 100) { Value = Tipo };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iUtente", OracleType.VarChar, 1000) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
        public string AssegnaInEmergenzaRegRdL(int OidRegRdL, string Tipo)
        {
            //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);

            var Msg = string.Empty;

            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.AssegnaInEmergenzaRegRdL(OidRegRdL, Tipo);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.AssegnaInEmergenzaRegRdL(OidRegRdL, Tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.regrdlemergenzadaco", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRegRdl", OracleType.Number) { Value = OidRegRdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iTipo", OracleType.VarChar, 100) { Value = Tipo };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public string CreateRegRdLbyRdL(int OidRegRdL)
        {
            //pk_mpasscaricocapacita.crearegrdlbyrdl(ioidregrdl => :ioidregrdl,oerrormsg => :oerrormsg);
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.crearegrdlbyrdl", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRdl", OracleType.Number) { Value = OidRegRdL };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iUtente", OracleType.VarChar, 1000) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
        public string RdlSospesa(int OidRdl)
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.RdlSospesa", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRdl", OracleType.Number) { Value = OidRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
        public string RdlAnnullata(int OidRdl)
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.RdlAnnullata", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRdl", OracleType.Number) { Value = OidRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public string RdlMigrazionepmptt(int OidRdl)
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.RdlMigrazionepmptt", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRdl", OracleType.Number) { Value = OidRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = CAMS.Module.Classi.SetVarSessione.CorrenteUser };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public string RdlCambioRisorsaTeam(int OidRdl, int OidRisorsaTeam)
        {
            var Msg = string.Empty;

            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.RdlCambioRisorsaTeam(OidRdl, OidRisorsaTeam);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.RdlCambioRisorsaTeam(OidRdl, OidRisorsaTeam);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }




            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.RdlCambioRisorsaTeam", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRdl", OracleType.Number) { Value = OidRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("OidRisorsaTeam", OracleType.Number) { Value = OidRisorsaTeam };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public string RdlCompletamento(int OidRdl)
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.RdlCompletamento", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRdl", OracleType.Number) { Value = OidRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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


        public string RdLAssegnaTeamRisorse(int OidRegRdl, int OidRisorsaTeam)
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.RegRdLAssegnaRisorsaTeam", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("iOidRegRdl", OracleType.Number) { Value = OidRegRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidRisorsaTeam", OracleType.Number) { Value = OidRisorsaTeam };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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


        public string RegRdLCompletamento(int OidRegRdl)
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.RdlCompletamento", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("ioidregrdl", OracleType.Number) { Value = OidRegRdl };
                    cmd.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public string linkCompletamento()
        {
            var Msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpasscaricocapacita.Parlinkcompletamento", OrclConn) { CommandType = CommandType.StoredProcedure };


                    var pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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



        public DataView SetDataFissaDettaglio(DateTime iMPDataFissaOiD, int Apparato, int SchedaMP)
        {
            var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            var sbOid = new StringBuilder("", 32000);


            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetDataFissaDettaglio(iMPDataFissaOiD, Apparato, SchedaMP);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetDataFissaDettaglio(iMPDataFissaOiD, Apparato, SchedaMP);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.CalcolaDataFissaPianifCursor", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iApparato", OracleType.Number) { Value = Apparato };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iSchedaMP", OracleType.Number) { Value = SchedaMP };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iData", OracleType.DateTime) { Value = iMPDataFissaOiD };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pp);

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();

                    var dr = (OracleDataReader)cmd.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
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
            //var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();

            var Messaggio = string.Empty;

            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetAttivitaContatore(OidRegistroPianoMP);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetAttivitaContatore(OidRegistroPianoMP);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.insert_mpdatacontatore", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("oidmpregpianificazione", OracleType.Number) { Value = OidRegistroPianoMP };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
            //var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            var Messaggio = string.Empty;

            //var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetAttivitaPluriennali(OidRegistroPianoMP);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetAttivitaPluriennali(OidRegistroPianoMP);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.insertmpdatapluriennale", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("oidmpregpianificazione", OracleType.Number) { Value = OidRegistroPianoMP };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
            //var Messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();

            var Messaggio = "Non Vedi?.";
            string UserNameCorrente = SetVarSessione.CorrenteUser;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetAttivitaDateIniziali(OidRegistroPianoMP);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetAttivitaDateIniziali(OidRegistroPianoMP);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.insert_mpdatainiziale", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("oidmpregpianificazione", OracleType.Number) { Value = OidRegistroPianoMP };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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




        public int GetNextSettimanaODL(int RegPiano, int NumSettimanaCorrente) ///string TipoAzione,
        {
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var cmd = new OracleCommand("pk_mpplanner.GetNextSettimanaODL", OrclConn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    var pp = new OracleParameter("iRegPiano", OracleType.Number) { Value = RegPiano };
                    cmd.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iSettimana", OracleType.Number) { Value = NumSettimanaCorrente };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oNextSettimana", OracleType.Number) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    cmd.ExecuteNonQuery();
                    var NextSettimana = 0;
                    var sNextSettimana = cmd.Parameters["oNextSettimana"].Value.ToString();
                    if (int.TryParse(sNextSettimana, out NextSettimana))
                    {
                        return NextSettimana;
                    }
                    else
                    {
                        return NumSettimanaCorrente;
                    }
                }
                return NumSettimanaCorrente;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return NumSettimanaCorrente;
        }


        /// <param name="Oidpmp"></param>
        public DataView AggiornaLogSchedeMP(string UserNameCorrente, int Oidpmp)
        {
            var dv = new DataView();
            var dt = new DataTable();

            var Msg = string.Empty;

            //var Messaggio = "Non Vedi?.";


            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.AggiornaLogSchedeMP(UserNameCorrente, Oidpmp);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.AggiornaLogSchedeMP(UserNameCorrente, Oidpmp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_pmp_util.log_mail_schedemp";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iUserNameCorrente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iOid", OracleType.Number) { Value = Oidpmp };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("io_cursor", OracleType.Cursor) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
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


        /// <param name="Oidpmp"></param>
        public Dictionary<int, string> RisorsaCaricaCombo(int OidRisorsa, string UserNameCorrente)
        {
            //var List = new Dictionary<int, string>();

            var List = new Dictionary<int, string>();
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.RisorsaCaricaCombo(OidRisorsa, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.RisorsaCaricaCombo(OidRisorsa, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.RisorsaCaricaComboCoppiaLink";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsa", OracleType.Number) { Value = OidRisorsa };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);
                    OrclConn.Open();
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
        public Dictionary<int, string> RisorsaTeamCaricaCombo(int OidRisorsaTeam, string UserNameCorrente)
        {
            var List = new Dictionary<int, string>();
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.RisorsaTeamCaricaCombo(OidRisorsaTeam, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.RisorsaTeamCaricaCombo(OidRisorsaTeam, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.RisorsaTeamCaricaComboCLink";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsaTeam", OracleType.Number) { Value = OidRisorsaTeam };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);
                    OrclConn.Open();

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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CreaRisorsaTeam(OidRisorsa, Anno, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CreaRisorsaTeam(OidRisorsa, Anno, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.CreaRisorsaTeam";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iOidRisorsa", OracleType.Number) { Value = OidRisorsa };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iAnno", OracleType.Number) { Value = Anno };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CreaCoppiaLinkata(OidRisorsa, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CreaCoppiaLinkata(OidRisorsa, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.CreaCoppiaLinkata";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsa", OracleType.Number) { Value = OidRisorsa };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oCriteria", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CreaCoppiaLinkataConRisorsa(iOidCapoRisorsaTeam, OidRisorsa, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CreaCoppiaLinkataConRisorsa(iOidCapoRisorsaTeam, OidRisorsa, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.CreaCoppiaLinkataConRisorsa";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsaTeam", OracleType.Number) { Value = iOidCapoRisorsaTeam };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidRisorsa", OracleType.Number) { Value = OidRisorsa };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
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
        public string CreaCoppiaLinkataConRisorsa1(int iOidRisorsaTeam, int OidRisorsa, string UserNameCorrente)
        {
            var Messaggi = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.CreaCoppiaLinkataConRisorsa";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsaTeam", OracleType.Number) { Value = OidRisorsa };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidRisorsa", OracleType.Number) { Value = OidRisorsa };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iUtente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    Messaggi = command.Parameters["oMessaggio"].Value.ToString();
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
            return Messaggi;
        }

        /// <param name="Oidpmp"></param>
        public string RilasciaRisorse(int OidRisorsaTeam, string UserNameCorrente)
        {

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.RilasciaRisorse(OidRisorsaTeam, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.RilasciaRisorse(OidRisorsaTeam, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.rilasciarisorsedateam";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("ioidrisorsateam", OracleType.Number) { Value = OidRisorsaTeam };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iutente", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
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
        public string GetRisorsexTask(string OidRdL, string UserNameCorrente)
        {  //PROCEDURE GetRisorsexTask(iOidRdL   IN VARCHAR2,   IO_CURSOR IN OUT T_CURSOR                            )
            var Criterio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.GetRisorsexTask";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRdL", OracleType.VarChar, 4000) { Value = OidRdL };
                    command.Parameters.Add(pp);
                    pp = null;
                    //pp = new OracleParameter("oCriteria", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    System.Data.IDataReader idr = (System.Data.IDataReader)dr;
                    //  idr.t
                    dt.Load((System.Data.IDataReader)dr);
                    dv = new DataView(dt);

                    if (dv.Count > 0)
                    {
                        Criterio = string.Join(",", dv.Cast<DataRowView>().Select(rv => rv.Row["oidrisorseteam"]));
                        Criterio = string.Format("Oid In ({0})", Criterio);
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
        public string[] GetDistanzeImpiantoRisorseTeam(int OidRisorseTeam, int OidRdlImpianto)
        {
            var Risultati = new string[5];
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.GetDistanzeRisorseTeam";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsaTeam", OracleType.VarChar, 4000) { Value = OidRisorseTeam };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iOidImpianto", OracleType.VarChar, 100) { Value = OidRdlImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oDistanza", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oIncaricoImpianto", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oUltimoImpianto", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oRdLSospese", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oRdLAssegnate", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    Risultati[0] = command.Parameters["oDistanza"].Value.ToString();
                    Risultati[1] = command.Parameters["oIncaricoImpianto"].Value.ToString();
                    Risultati[2] = command.Parameters["oUltimoImpianto"].Value.ToString();
                    Risultati[3] = command.Parameters["oRdLSospese"].Value.ToString();
                    Risultati[4] = command.Parameters["oRdLAssegnate"].Value.ToString();
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
            return Risultati;
        }




        /// <param name="Oidpmp"></param>
        public string NrInterventiInEdificio(int OidRisorseTeam, int OidEdificio, ref string NrInterventi)
        {
            var Risultati = "";
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mpasscaricocapacita.NrInterventiInEdificio";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidRisorsaTeam", OracleType.VarChar, 4000) { Value = OidRisorseTeam };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iOidEdificio", OracleType.VarChar, 100) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oNrInterventi", OracleType.VarChar, 10) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    NrInterventi = command.Parameters["oNrInterventi"].Value.ToString();

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
            return Risultati;
        }

        //    public System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.npRdLRisorseTeam> GetTeamRisorse_for_RdL
        public System.ComponentModel.BindingList<DCRisorseTeamRdL> GetTeamRisorse_for_RdL_old
            (int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase, string UserNameCorrente, int OidRTeamRemove = 0)
        {
            var Criterio = string.Empty;

            System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();

            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_dc_salaoperativa.RISORSE_EDIFICIO_RDL";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iOidEdificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iIsSmartphone", OracleType.Number) { Value = IsSmartphone };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidCentroOperativoBase", OracleType.Number) { Value = iOidCentroOperativoBase };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("iusername", OracleType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    if (dr.HasRows)
                    {
                        int Newid = 0;
                        while (dr.Read())
                        {
                            // CAMS.Module.DBTask.DC.DCRisorseTeamRdL obj = xpObjectSpace.CreateObject<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();
                            if (OidRTeamRemove > 0 && OidRTeamRemove == dr.GetInt32(dr.GetOrdinal("OIDRISORSATEAM")))
                                continue;

                            CAMS.Module.DBTask.DC.DCRisorseTeamRdL obj = new CAMS.Module.DBTask.DC.DCRisorseTeamRdL()
                            {
                                Oid = Guid.NewGuid(), /*obj.ID = Newid++;*/
                                OidCentroOperativo = dr.GetInt32(dr.GetOrdinal("OIDCENTROOPERATIVO")), /*   obj.OidEdificio = dr.GetInt32(dr.GetOrdinal("OIDEDIFICIO")); // OIDEDIFICIO*/
                                OidRisorsaTeam = dr.GetInt32(dr.GetOrdinal("OIDRISORSATEAM")),
                                NumeroAttivitaAgenda = dr.GetInt32(dr.GetOrdinal("NRATTAGENDA")),
                                NumeroAttivitaSospese = dr.GetInt32(dr.GetOrdinal("NRATTSOSPESE")),
                                NumeroAttivitaEmergenza = dr.GetInt32(dr.GetOrdinal("NRATTEMERGENZA")),
                                Conduttore = dr.GetInt32(dr.GetOrdinal("CONDUTTORE")) > 0 ? true : false /* Verifica.Substring(i, 1) == "1" ? true : false;*/,
                                CoppiaLinkata = (TipoNumeroManutentori)dr.GetInt32(dr.GetOrdinal("NUMMAN")),
                                Ordinamento = dr.GetInt32(dr.GetOrdinal("ORDINAMENTO")),
                                CentroOperativo = dr.GetString(dr.GetOrdinal("CENTROOPERATIVO")),
                                UltimoStatoOperativo = dr.GetString(dr.GetOrdinal("ULTIMOSTATOOPERATIVO")),
                                RisorsaCapo = dr.GetString(dr.GetOrdinal("RISORSACAPOSQUADRA")),
                                Mansione = dr.GetString(dr.GetOrdinal("MANSIONE")),
                                Telefono = dr.GetString(dr.GetOrdinal("TELEFONO")),
                                RegistroRdL = dr.GetString(dr.GetOrdinal("REGRDLASSOCIATO")), /*  obj.UserName = dr.GetString(dr.GetOrdinal("USERNAME"));*/
                                DistanzaImpianto = dr.GetString(dr.GetOrdinal("DISTANZAIMPIANTO")),
                                UltimoEdificio = dr.GetString(dr.GetOrdinal("ULTIMOEDIFICIO")),
                                InterventosuEdificio = dr.GetString(dr.GetOrdinal("INTERVENTISUEDIFICIO")),
                                Url = dr.GetString(dr.GetOrdinal("URL")),
                                Azienda = dr.GetString(dr.GetOrdinal("AZIENDA"))
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

        //    public System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.npRdLRisorseTeam> GetTeamRisorse_for_RdL
        public System.ComponentModel.BindingList<DCRisorseTeamRdL> GetTeamRisorse_for_RdL
            (int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase, string UserNameCorrente, int OidRegoleAutoAssegnazione, int OidRTeamRemove = 0)
        {
            var Criterio = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetTeamRisorse_for_RdL(OidEdificio, IsSmartphone, iOidCentroOperativoBase, UserNameCorrente, OidRegoleAutoAssegnazione, OidRTeamRemove = 0);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetTeamRisorse_for_RdL(OidEdificio, IsSmartphone, iOidCentroOperativoBase, UserNameCorrente, OidRegoleAutoAssegnazione, OidRTeamRemove = 0);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();

            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "PK_DC_SALAOPERATIVA_1.ris_edif_rdl";
                    //command.CommandText = "pk_dc_salaoperativa.RISORSE_EDIFICIO_RDL_REV";
                    command.CommandType = CommandType.StoredProcedure;
                    //  iregolaautossegnazione  
                    var pp = new OracleParameter("iOidRegoleAutoAssegnazione", OracleType.Number) { Value = OidRegoleAutoAssegnazione };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidEdificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iIsSmartphone", OracleType.Number) { Value = IsSmartphone };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidCentroOperativoBase", OracleType.Number) { Value = iOidCentroOperativoBase };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("iusername", OracleType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
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
                            obj.Distanzakm = dr.GetInt32(dr.GetOrdinal("DISTANZAKM"));
                            obj.UltimoEdificio = dr.GetString(dr.GetOrdinal("ULTIMOEDIFICIO"));
                            obj.InterventosuEdificio = dr.GetString(dr.GetOrdinal("INTERVENTISUEDIFICIO"));
                            obj.Url = dr.GetString(dr.GetOrdinal("URL"));
                            obj.Azienda = dr.GetString(dr.GetOrdinal("AZIENDA"));
                            obj.NumerorAttivitaTotaliMP = dr.GetInt32(dr.GetOrdinal("NRATTIVITATOTALIMP"));
                            obj.NumerorAttivitaTotaliTT = dr.GetInt32(dr.GetOrdinal("NRATTIVITATOTALITT"));
                            obj.Aggiornamento = dr.GetString(dr.GetOrdinal("AGGIORNAMENTO"));


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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetTeamRisorseOttimizata(OidEdificio, IsSmartphone, iOidCentroOperativoBase,
             UserNameCorrente, OidRegoleAutoAssegnazione, ref Messaggio);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetTeamRisorseOttimizata(OidEdificio, IsSmartphone, iOidCentroOperativoBase,
             UserNameCorrente, OidRegoleAutoAssegnazione, ref Messaggio);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "PK_DC_SALAOPERATIVA_1.GET_TRISORSA_OTTIMIZZATA";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iOidRegoleAutoAssegnazione", OracleType.Number) { Value = OidRegoleAutoAssegnazione };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidEdificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iIsSmartphone", OracleType.Number) { Value = IsSmartphone };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidCentroOperativoBase", OracleType.Number) { Value = iOidCentroOperativoBase };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("iusername", OracleType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("orisorsateam", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    OrclConn.Open();
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



       
           public string GetMessaggioDestImpostatiDB(string iuser, int iavvisispedizionioid)
        {
            
            string msg = string.Empty;
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetMessaggioDestImpostati( iuser,  iavvisispedizionioid);
                    }
                }
             //   else
             //   {
             //       using (ClassiORADB.DB msdb = new ClassiORADB.DB())
             //       {
             //           return msdb.GetTeamRisorseOttimizata(OidEdificio, IsSmartphone, iOidCentroOperativoBase,
             //UserNameCorrente, OidRegoleAutoAssegnazione, ref Messaggio);

             //       }
             //   }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            return msg;


        }


        public int GetTeamRisorseOttimizata_rev(int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase,
             string UserNameCorrente, int OidRegoleAutoAssegnazione, ref string Messaggio)
        {
            int OidTRisorsa = 0;
            string msg = string.Empty;
            //procedure get_risorsa_ottim
            //(ioidregolaautoss in number,
            // omessaggio out varchar2,
            //ooidrisorsateam out number )


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_dc_salaoperativa.get_risorsa_ottim";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("ioidregolaautoss", OracleType.Number) { Value = OidRegoleAutoAssegnazione };
                    command.Parameters.Add(pp);
                    pp = null;

                    //pp = new OracleParameter("iOidEdificio", OracleType.Number) { Value = OidEdificio };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    //pp = new OracleParameter("iIsSmartphone", OracleType.Number) { Value = IsSmartphone };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    //pp = new OracleParameter("iOidCentroOperativoBase", OracleType.Number) { Value = iOidCentroOperativoBase };
                    //command.Parameters.Add(pp);
                    //pp = null;


                    //pp = new OracleParameter("iusername", OracleType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    //command.Parameters.Add(pp);
                    //pp = null;

                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("ooidrisorsateam", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    //pp.Direction = ParameterDirection.Output;
                    //command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    try
                    {
                        OidTRisorsa = int.Parse(command.Parameters["ooidrisorsateam"].Value.ToString());
                    }
                    catch
                    {
                        OidTRisorsa = 0;
                    }
                    msg = command.Parameters["omessaggio"].Value.ToString();
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



        //  REPORT_REGRDL
        public System.ComponentModel.BindingList<DCRdLListReport> GetReport_RdL
            (string icodiciRDL)
        {
            var Criterio = string.Empty;

            System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport>();

            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_dc_salaoperativa.REPORT_RDL";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("icodiciRDL", OracleType.VarChar, 4000) { Value = icodiciRDL };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("icodiciRDL_Lob", OracleType.Clob, 4000) { Value = icodiciRDL, Direction = ParameterDirection.Input };
                    command.Parameters.Add(pp);
                    pp = null;

                    //   //System.Data.OracleClient.OracleParameter clob_Param = new System.Data.OracleClient.OracleParameter();
                    //   pp.ParameterName = "icodiciRDL_Lob";
                    //   pp.OracleType = OracleType.Clob;
                    //   pp.Direction = ParameterDirection.Input;
                    //  //OracleLob aa = new OracleLob();
                    //  // System.Data.OracleClient.OracleLob clob = new System.Data.OracleClient.OracleLob(OrclConn,OracleDbType.
                    //  // clob.Write(icodiciRDL.ToArray(), 0, icodiciRDL.Length);
                    //   pp.Value = icodiciRDL;
                    ////   p_vc.Size = 32000;
                    //   command.Parameters.Add(pp);
                    //   pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
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

                                CodSchedeMp = dr.GetString(dr.GetOrdinal("CODSCHEDEMP")),//  dr.CodSchedeMp,
                                CodSchedaMpUni = dr.GetString(dr.GetOrdinal("CODSCHEDAMPUNI")),// dr.CodSchedaMpUni,
                                DescrizioneManutenzione = dr.GetString(dr.GetOrdinal("DESCRIZIONEMANUTENZIONE")),// dr.DescrizioneManutenzione,
                                FrequenzaDescrizione = dr.GetString(dr.GetOrdinal("FREQUENZADESCRIZIONE")),// dr.FrequenzaDescrizione,
                                PassoSchedaMp = dr.GetString(dr.GetOrdinal("PASSOSCHEDAMP")),//  dr.PassoSchedaMp,
                                NrOrdine = dr.GetInt32(dr.GetOrdinal("NORDINE")),//   int.Parse(dr.NrOrdine.ToString())
                                StatoSmistamento = dr.GetString(dr.GetOrdinal("STATOSMISTAMENTO")),
                                StatoOperativo = dr.GetString(dr.GetOrdinal("STATOOPERATIVO")),
                                Utente = dr.GetString(dr.GetOrdinal("UTENTE")),
                                NoteCompletamento = dr.GetString(dr.GetOrdinal("NOTECOMPLETAMENTO")),
                                FrequenzaCod_Descrizione = dr.GetString(dr.GetOrdinal("FREQUENZACOD_DESCRIZIONE")),
                                //ComponentiManutenzioneClob = Convert.ToString(((System.Data.OracleClient.OracleLob)(dr.GetOracleValue(ordClob))).Value);
                                //ComponentiManutenzione = dr.GetOracleLob(dr.GetOrdinal("ComponentiManutenzione")).Value.ToString(),
                                ComponentiManutenzione = Convert.ToString(((System.Data.OracleClient.OracleLob)
                                                        (dr.GetOracleValue(dr.GetOrdinal("ComponentiManutenzione")))).Value),
                                // ######   DEVE ESSERERE UN CLOB DI TESTO!!!!
                                //ComponentiSostegno = dr.GetString(dr.GetOrdinal("COMPONENTI_SOSTEGNO")),

                                Annotazioni = Convert.ToString(((System.Data.OracleClient.OracleLob)
                                                       (dr.GetOracleValue(dr.GetOrdinal("Annotazioni")))).Value),
                                OidCategoria = dr.GetInt32(dr.GetOrdinal("OIDCATEGORIA")),
                                Anno = dr.GetInt32(dr.GetOrdinal("ANNO")),
                                Mese = dr.GetInt32(dr.GetOrdinal("MESE")),
                                Settimana = dr.GetInt32(dr.GetOrdinal("SETTIMANA")),
                                //AreaDiPolo = dr.GetString(dr.GetOrdinal("AREADIPOLO")),
                                RefAmministrativo = dr.GetString(dr.GetOrdinal("REFAMMINISTRATIVO")),
                                CorpoMP = dr.GetString(dr.GetOrdinal("CORPOMP")),
                                OrdinePasso = dr.GetString(dr.GetOrdinal("ORDINEPASSO"))
                                //CODICE
                                //CODICEODL
                                //INSOURCING
                                //KEYPLAN
                                //OIDEDIFICIO
                                //OIDREFERENTECOFELY
                                //OIDSMISTAMENTO

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

        //  REPORT_REGRDL
        public System.ComponentModel.BindingList<DCRdLListReport> GetREPORT_REGRDL
            (string codiciREGRDL_Lob)
        {
            var Criterio = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetREPORT_REGRDL(codiciREGRDL_Lob);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetREPORT_REGRDL(codiciREGRDL_Lob);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }



            System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRdLListReport>();
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_dc_salaoperativa.REPORT_REGRDL";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("icodiciREGRDL_Lob", OracleType.Clob, 4000) { Value = codiciREGRDL_Lob, Direction = ParameterDirection.Input };
                    command.Parameters.Add(pp);
                    pp = null;

                    //   //System.Data.OracleClient.OracleParameter clob_Param = new System.Data.OracleClient.OracleParameter();
                    //   pp.ParameterName = "icodiciRDL_Lob";
                    //   pp.OracleType = OracleType.Clob;
                    //   pp.Direction = ParameterDirection.Input;
                    //  //OracleLob aa = new OracleLob();
                    //  // System.Data.OracleClient.OracleLob clob = new System.Data.OracleClient.OracleLob(OrclConn,OracleDbType.
                    //  // clob.Write(icodiciRDL.ToArray(), 0, icodiciRDL.Length);
                    //   pp.Value = icodiciRDL;
                    ////   p_vc.Size = 32000;
                    //   command.Parameters.Add(pp);
                    //   pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
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

                                CodSchedeMp = dr.GetString(dr.GetOrdinal("CODSCHEDEMP")),//  dr.CodSchedeMp,
                                CodSchedaMpUni = dr.GetString(dr.GetOrdinal("CODSCHEDAMPUNI")),// dr.CodSchedaMpUni,
                                DescrizioneManutenzione = dr.GetString(dr.GetOrdinal("DESCRIZIONEMANUTENZIONE")),// dr.DescrizioneManutenzione,
                                FrequenzaDescrizione = dr.GetString(dr.GetOrdinal("FREQUENZADESCRIZIONE")),// dr.FrequenzaDescrizione,
                                PassoSchedaMp = dr.GetString(dr.GetOrdinal("PASSOSCHEDAMP")),//  dr.PassoSchedaMp,
                                NrOrdine = dr.GetInt32(dr.GetOrdinal("NORDINE")),//   int.Parse(dr.NrOrdine.ToString())
                                StatoSmistamento = dr.GetString(dr.GetOrdinal("STATOSMISTAMENTO")),
                                StatoOperativo = dr.GetString(dr.GetOrdinal("STATOOPERATIVO")),
                                Utente = dr.GetString(dr.GetOrdinal("UTENTE")),
                                NoteCompletamento = dr.GetString(dr.GetOrdinal("NOTECOMPLETAMENTO")),
                                FrequenzaCod_Descrizione = dr.GetString(dr.GetOrdinal("FREQUENZACOD_DESCRIZIONE")),
                                //ComponentiManutenzioneClob = Convert.ToString(((System.Data.OracleClient.OracleLob)(dr.GetOracleValue(ordClob))).Value);
                                //ComponentiManutenzione = dr.GetOracleLob(dr.GetOrdinal("ComponentiManutenzione")).Value.ToString(),
                                ComponentiManutenzione = Convert.ToString(((System.Data.OracleClient.OracleLob)
                                                        (dr.GetOracleValue(dr.GetOrdinal("ComponentiManutenzione")))).Value),  // ######   DEVE ESSERERE UN CLOB DI TESTO!!!!
                                //ComponentiSostegno = dr.GetString(dr.GetOrdinal("COMPONENTI_SOSTEGNO")),

                                Annotazioni = Convert.ToString(((System.Data.OracleClient.OracleLob)
                                                        (dr.GetOracleValue(dr.GetOrdinal("Annotazioni")))).Value),
                                OidCategoria = dr.GetInt32(dr.GetOrdinal("OIDCATEGORIA")),
                                Anno = dr.GetInt32(dr.GetOrdinal("ANNO")),
                                Mese = dr.GetInt32(dr.GetOrdinal("MESE")),
                                Settimana = dr.GetInt32(dr.GetOrdinal("SETTIMANA")),
                                //AreaDiPolo = dr.GetString(dr.GetOrdinal("AREADIPOLO")),
                                RefAmministrativo = dr.GetString(dr.GetOrdinal("REFAMMINISTRATIVO")),
                                CorpoMP = dr.GetString(dr.GetOrdinal("CORPOMP")),
                                OrdinePasso = dr.GetString(dr.GetOrdinal("ORDINEPASSO"))

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


        public System.ComponentModel.BindingList<DCRegistroSmistamentoDettaglio> GetStorico_RdL
                                                                               (int OidObj, int TypeObj, DateTime DataLimite, string UserNameCorrente)
        {
            var Criterio = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetStorico_RdL(OidObj, TypeObj, DataLimite, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetStorico_RdL(OidObj, TypeObj, DataLimite, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            System.ComponentModel.BindingList<DCRegistroSmistamentoDettaglio> objects =
                             new System.ComponentModel.BindingList<DCRegistroSmistamentoDettaglio>();
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_auditdata.getauditdata";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iOidObj", OracleType.Number) { Value = OidObj };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iTypeObj", OracleType.Number) { Value = TypeObj };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iDataLimite", OracleType.DateTime) { Value = DataLimite };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("iusername", OracleType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
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


        public System.ComponentModel.BindingList<DCRegistroOperativoRisorsa> GetOperativoRisorsa
                                                                               (int OidObj, DateTime DataLimite, string UserNameCorrente)
        {
            var Criterio = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetOperativoRisorsa(OidObj, DataLimite, UserNameCorrente);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetOperativoRisorsa(OidObj, DataLimite, UserNameCorrente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }



            System.ComponentModel.BindingList<DCRegistroOperativoRisorsa> objects =
                             new System.ComponentModel.BindingList<DCRegistroOperativoRisorsa>();
            var msg = string.Empty;
            try
            {
                //procedure  GetAuditData_Risorse
                //(iUser     in varchar2,                        
                // iDataLimiteInf in date,
                // iDataLimiteSup in date,
                // oMessaggio   in out varchar,
                //IO_CURSOR   IN OUT T_CURSOR) 



                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_auditdata.GetAuditData_Risorse";
                    command.CommandType = CommandType.StoredProcedure;

                    //var pp = new OracleParameter("iOidObj", OracleType.Number) { Value = OidObj };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    //pp = new OracleParameter("iTypeObj", OracleType.Number) { Value = TypeObj };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    var pp = new OracleParameter("iUser", OracleType.VarChar, 4000) { Value = UserNameCorrente }; ;
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iOidRisorsa", OracleType.Number) { Value = OidObj }; ;
                    command.Parameters.Add(pp);
                    pp = null;

                    DateTime DataLimiteInf = DataLimite.AddDays(-7);
                    pp = new OracleParameter("iDataLimiteInf", OracleType.DateTime) { Value = DataLimiteInf };
                    command.Parameters.Add(pp);
                    pp = null;



                    pp = new OracleParameter("iDataLimiteSup", OracleType.DateTime) { Value = DataLimite };
                    command.Parameters.Add(pp);
                    pp = null;



                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("IO_CURSOR", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["IO_CURSOR"].Value;
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




        public string CloneEdificio(string UserNameCorrente, int OidEdificio, string CodEdificio, string descrizione, int OidIndirizzo, ref int OidNewEdificio)
        {
            var msg = string.Empty;
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CloneEdificio(UserNameCorrente, OidEdificio, CodEdificio, descrizione, OidIndirizzo, ref OidNewEdificio);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CloneEdificio(UserNameCorrente, OidEdificio, CodEdificio, descrizione, OidIndirizzo, ref OidNewEdificio);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }



            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_edificio.clona_edificio";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidEdificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    //pp = new OracleParameter("iSession", OracleType.VarChar, 200) { Value = Classi.SetVarSessione.CodSessioneWeb };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    pp = new OracleParameter("iUser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iCodEdificio", OracleType.VarChar, 200) { Value = CodEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iDescrizione", OracleType.VarChar, 2000) { Value = descrizione };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iOidIndirizzo", OracleType.Number) { Value = OidIndirizzo };
                    command.Parameters.Add(pp);
                    pp = null;  //inewEdificio
                    pp = new OracleParameter("oNewEdificio", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CloneApparato(UserNameCorrente, OidApparato, clone, descrizione);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CloneApparato(UserNameCorrente, OidApparato, clone, descrizione);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_apparato.clona_apparato";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidApparato", OracleType.Number) { Value = OidApparato };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iNumClone", OracleType.Number) { Value = clone };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iDescrizione", OracleType.VarChar, 200) { Value = descrizione };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CloneImpianto(UserNameCorrente, OidImpianto, CodImpianto, descrizione);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CloneImpianto(UserNameCorrente, OidImpianto, CodImpianto, descrizione);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_impianto.clona_impianto";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidImpianto", OracleType.Number) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iCodImpianto", OracleType.VarChar, 200) { Value = CodImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iUser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iDescrizione", OracleType.VarChar, 200) { Value = descrizione };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oNewImpianto", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CloneDestinatario(UserNameCorrente, OidDestinatario, email, nome, cognome);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CloneDestinatario(UserNameCorrente, OidDestinatario, email, nome, cognome);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.clona_destinatario";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("ioid", OracleType.Number) { Value = OidDestinatario };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iEmail", OracleType.VarChar, 200) { Value = email }; //Value = clone 
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iNome", OracleType.VarChar, 200) { Value = nome };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iCognome", OracleType.VarChar, 200) { Value = cognome };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public string GetRegMisureDettaglio(string UserNameCorrente, int OidRegMisure, int OidImpianto)
        {
            var msg = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetRegMisureDettaglio(UserNameCorrente, OidRegMisure, OidImpianto);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetRegMisureDettaglio(UserNameCorrente, OidRegMisure, OidImpianto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_misure.popola_det_misure";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("ireg_misure", OracleType.Number) { Value = OidRegMisure };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iimpianto", OracleType.Number) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public string CreaNuovoControlloNormativo(int OidControlloNormativo)
        {
            var msg = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CreaNuovoControlloNormativo(OidControlloNormativo);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CreaNuovoControlloNormativo(OidControlloNormativo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "PK_CONTROLLI_NORMATIVI_TXT.SHIFT_CTRL_NORM";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOidControlloNormativo", OracleType.Number) { Value = OidControlloNormativo };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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



        public DataView GetDatiInvioMailControlloNormativo(string UserNameCorrente, int OidControlloNormativo,
            int OidEdificio, DateTime Data)
        {
            //pk_controlli_normativi_txt.get_cn_x_email_by_bl(oidedificio => :oidedificio,
            //                          idatarif => :idatarif,   io_cursor => :io_cursor,   omessagg => :omessagg);
            var dv = new DataView();
            var dt = new DataTable();
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_controlli_normativi_txt.get_cn_x_email_by_bl";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("oidcontrollonormativo", OracleType.Number) { Value = OidControlloNormativo };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("idatarif", OracleType.DateTime) { Value = Data };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("omessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
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


        public string AggiornaTempi(int Oid, string type)
        {
            var msg = string.Empty;
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.AggiornaTempi(Oid, type);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.AggiornaTempi(Oid, type);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "PK_AGGIORNA_TEMPI.AGGIORNA_TEMPI";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("iOid", OracleType.Number) { Value = Oid };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iTipo", OracleType.VarChar) { Value = type };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
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


        public string CreaServiziobyServizioLibrary(int OidImpianto, int NumClone, int OidLibraryImpianto) //string Descrizione,ioidcommessa,ioidedificio,ioidpiano, izona => :izona,iuser => :iuser,//                       oerrormsg => :oerrormsg);)
        {
            var msg = string.Empty;
            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.CreaImpiantobyImpiantoLibrary(OidImpianto, NumClone, OidLibraryImpianto);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.CreaImpiantobyImpiantoLibrary(OidImpianto, NumClone, OidLibraryImpianto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            //pk_impianto.creaimpiantobylinbrary(ioidlibraryimpianto => :ioidlibraryimpianto,
            //                       inumclone => :inumclone,
            //                       idescrizione => :idescrizione,
            //                       ioidcommessa => :ioidcommessa,
            //                       ioidedificio => :ioidedificio,
            //                       ioidpiano => :ioidpiano,
            //                       izona => :izona,
            //                       iuser => :iuser,
            //                       oerrormsg => :oerrormsg);
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_impianto.creaimpiantobylinbrary";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("ioidimpianto", OracleType.Number) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("inumclone", OracleType.Number) { Value = NumClone };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioidlibraryimpianto", OracleType.Number) { Value = OidLibraryImpianto };
                    command.Parameters.Add(pp);
                    pp = null;
                    //pp = new OracleParameter("idescrizione", OracleType.Number) { Value = Oid };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("ioidcommessa", OracleType.Number) { Value = Oid };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("ioidedificio", OracleType.Number) { Value = Oid };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("ioidpiano", OracleType.Number) { Value = Oid };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("ioidpiano", OracleType.Number) { Value = Oid };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("izona", OracleType.Number) { Value = Oid };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    //pp = new OracleParameter("iuser", OracleType.Number) { Value = Oid };
                    //command.Parameters.Add(pp);
                    //pp = null;
                    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
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
        //        using (OracleConnection OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
        //        {
        //            OracleCommand command = OrclConn.CreateCommand();
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
        //            OracleParameter pp = new OracleParameter("ioidregnotificaemergenza", OracleType.Number) { Value = iOidRegNotificaEmergenza };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("irisorseteamlist", OracleType.Number) { Value = OidRisorsaTeam };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("iRisorsa", OracleType.Number) { Value = OidRisorsaCapo };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

        public FileData getFileFromViewId(string viewId)
        {

            try
            {
                using (OracleConnection OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    OracleCommand command = OrclConn.CreateCommand();

                    command.CommandText = "pk_help.getFileFromViewId";
                    command.CommandType = CommandType.StoredProcedure;
                    //----------------------------
                    //------------------------------
                    OracleParameter pp = new OracleParameter("iViewId", OracleType.VarChar, 200) { Value = viewId };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("oFile", OracleType.Blob) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    //-----------
                    return (FileData)command.Parameters["oFile"].Value;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
                return null;
            }
            //return null;
        }


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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetDatiTipi(FullNomeClasse, OidEdificio, Nuovo);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetDatiTipi(FullNomeClasse, OidEdificio, Nuovo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.GetListTipi";
                    //pk_ruolouser.getlisttipi(fnomeclasse => :fnomeclasse,
                    //     io_cursor => :io_cursor,
                    //     omessage => :omessage);
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("ifnomeclasse", OracleType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iNuovo", OracleType.Number) { Value = Nuovo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = Classi.SetVarSessione.CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetDatiPermessiMebri(FullNomeClasse, OidEdificio, Nuovo);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetDatiPermessiMebri(FullNomeClasse, OidEdificio, Nuovo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.getlistmembri";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("ifnomeclasse", OracleType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iNuovo", OracleType.Number) { Value = Nuovo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = Classi.SetVarSessione.CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetDatiPermessiOggetti(FullNomeClasse, OidEdificio, Nuovo);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetDatiPermessiOggetti(FullNomeClasse, OidEdificio, Nuovo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.getlistoggetti";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("ifnomeclasse", OracleType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioidedificio", OracleType.Number) { Value = OidEdificio };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iNuovo", OracleType.Number) { Value = Nuovo };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = Classi.SetVarSessione.CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;


                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    dt.Load((System.Data.IDataReader)dr);
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetMebri(FullNomeClasse, strMembri);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetMebri(FullNomeClasse, strMembri);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ruolouser.setlistmembri";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("ifnomeclasse", OracleType.VarChar, 200) { Value = FullNomeClasse };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("isrtmembri", OracleType.VarChar, 4000) { Value = strMembri };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("omessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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


        #endregion



        /// <param name="Oidpmp"></param>
        public DataView GetFiltrobyTesto(string Oggetto, string searchText)
        {
            var dv = new DataView();
            var dt = new DataTable();

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetFiltrobyTesto(Oggetto, searchText);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetFiltrobyTesto(Oggetto, searchText);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_ricerca.getfiltrobytesto";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("ioggetto", OracleType.VarChar, 100) { Value = Oggetto };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("isearchtext", OracleType.VarChar, 4000) { Value = searchText };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("omessagg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("io_cursor", OracleType.Cursor) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    OrclConn.Open();
                    command.ExecuteNonQuery();
                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
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




        //public string SetProblemaCausaRimedioDefault(int OidApparatoProblema) //string Descrizione,ioidcommessa,ioidedificio,ioidpiano, izona => :izona,iuser => :iuser,//                       oerrormsg => :oerrormsg);)
        //{
        //    var msg = string.Empty;



        //    //pk_albero_guasti.setalberodefault(ioid => :ioid,  omessaggio => :omessaggio);
        //    try
        //    {
        //        //using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
        //        //{
        //        //    var command = OrclConn.CreateCommand();
        //        //    command.CommandText = "pk_impianto.creaimpiantobylinbrary";
        //        //    command.CommandType = CommandType.StoredProcedure;
        //        //    var pp = new OracleParameter("ioidimpianto", OracleType.Number) { Value = OidImpianto };
        //        //    command.Parameters.Add(pp);
        //        //    pp = null;
        //        //    pp = new OracleParameter("inumclone", OracleType.Number) { Value = NumClone };
        //        //    command.Parameters.Add(pp);
        //        //    pp = null;
        //        //    pp = new OracleParameter("ioidlibraryimpianto", OracleType.Number) { Value = OidLibraryImpianto };
        //        //    command.Parameters.Add(pp);
        //        //    pp = null;
        //        //    //pp = new OracleParameter("idescrizione", OracleType.Number) { Value = Oid };
        //        //    //command.Parameters.Add(pp);
        //        //    //pp = null;
        //        //    //pp = new OracleParameter("ioidcommessa", OracleType.Number) { Value = Oid };
        //        //    //command.Parameters.Add(pp);
        //        //    //pp = null;
        //        //    //pp = new OracleParameter("ioidedificio", OracleType.Number) { Value = Oid };
        //        //    //command.Parameters.Add(pp);
        //        //    //pp = null;
        //        //    //pp = new OracleParameter("ioidpiano", OracleType.Number) { Value = Oid };
        //        //    //command.Parameters.Add(pp);
        //        //    //pp = null;
        //        //    //pp = new OracleParameter("ioidpiano", OracleType.Number) { Value = Oid };
        //        //    //command.Parameters.Add(pp);
        //        //    //pp = null;
        //        //    //pp = new OracleParameter("izona", OracleType.Number) { Value = Oid };
        //        //    //command.Parameters.Add(pp);
        //        //    //pp = null;
        //        //    //pp = new OracleParameter("iuser", OracleType.Number) { Value = Oid };
        //        //    //command.Parameters.Add(pp);
        //        //    //pp = null;
        //        //    pp = new OracleParameter("oMessage", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //        //    command.Parameters.Add(pp);
        //        //    pp = null;

        //        //    OrclConn.Open();
        //        //    command.ExecuteNonQuery();
        //        //    msg = command.Parameters["oMessage"].Value.ToString();
        //        //    if (msg != null && !msg.Equals(string.Empty))
        //        //    {
        //        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //    return msg;
        //}


        //public string SetParContatoreAppMP(int DataContatoreOid, string TipoParametro, string valore)
        //{
        //    var msg = string.Empty;
        //    //pk_albero_guasti.setalberodefault(ioid => :ioid,  omessaggio => :omessaggio);
        //    try
        //    {
        //        using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mpplanner.setparcontatoreappmp";
        //            command.CommandType = CommandType.StoredProcedure;
        //            var pp = new OracleParameter("idatacontatoreoid", OracleType.Number) { Value = DataContatoreOid };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new OracleParameter("itipoparametro", OracleType.VarChar) { Value = TipoParametro };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            pp = new OracleParameter("ivalore", OracleType.VarChar) { Value = valore };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            //pp = new OracleParameter("idescrizione", OracleType.Number) { Value = Oid };

        //            pp = new OracleParameter("oMessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.InsertSegnalazioneMail(emailFrom, emailTo, emailDate, emailSubject, emailBody, emailReceived, emailMessageID,
                                             strEdificio, strAvviso, strAssemblaggio, strDescrizione);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.InsertSegnalazioneMail(emailFrom, emailTo, emailDate, emailSubject, emailBody, emailReceived, emailMessageID,
                                             strEdificio, strAvviso, strAssemblaggio, strDescrizione);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }



            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.inssegnalazionemail";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iemailFrom", OracleType.VarChar, 100) { Value = emailFrom };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailTo", OracleType.VarChar, 100) { Value = emailTo };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailDate", OracleType.VarChar, 100) { Value = emailDate };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailSubject", OracleType.VarChar, 300) { Value = emailSubject };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailBody", OracleType.Clob) { Value = emailBody };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailReceived", OracleType.VarChar, 100) { Value = emailReceived };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("iemailMessageID", OracleType.VarChar, 100) { Value = emailMessageID };
                    command.Parameters.Add(pp);
                    ///      string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione
                    pp = null;
                    pp = new OracleParameter("istrEdificio", OracleType.VarChar, 300) { Value = strEdificio };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("istrAvviso", OracleType.Clob) { Value = strAvviso };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("istrAssemblaggio", OracleType.VarChar, 100) { Value = strAssemblaggio };
                    command.Parameters.Add(pp);

                    pp = null;
                    pp = new OracleParameter("istrDescrizione", OracleType.VarChar, 100) { Value = strDescrizione };
                    command.Parameters.Add(pp);

                    ///

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

        public DataTable GetRdLSegnalazioneMail(string CorrenteUser, string searchText) //string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string messaggio = string.Empty;
            var dv = new DataView();
            var dt = new DataTable();

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetRdLSegnalazioneMail(CorrenteUser, searchText);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetRdLSegnalazioneMail(CorrenteUser, searchText);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    //                    -- Call the procedure
                    //pk_mail.getrdlseg_send_mail(oerrormsg => :oerrormsg,
                    //                            io_cursor => :io_cursor);
                    //                    -- Call the procedure
                    //pk_mail.getrdlseg_send_mail(iuser => :iuser,
                    //                            isearchtext => :isearchtext,
                    //                            oerrormsg => :oerrormsg,
                    //                            io_cursor => :io_cursor);
                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "PK_MAIL_SEGN.getrdlseg_send_mail";
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

        public DataTable GetDestinatariMail_RdLSegnalazione(string CorrenteUser, int OidEdificio) //string emailFrom, string emailTo, string emailDate, string emailSubject, string emailBody, string emailReceived, string emailMessageID,string strEdificio, string strAvviso, string strAssemblaggio, string strDescrizione)
        {
            string messaggio = string.Empty;

            var dt = new DataTable("DestinatariMailSg");

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetDestinatariMail_RdLSegnalazione(CorrenteUser, OidEdificio);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetDestinatariMail_RdLSegnalazione(CorrenteUser, OidEdificio);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }



            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    //pk_mail.getdestinatarimail(iuser => :iuser,
                    //                           ioidedificio => :ioidedificio,
                    //                           oerrormsg => :oerrormsg,
                    //                           io_cursor => :io_cursor);
                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.getdestinatarimail";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iuser", OracleType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("ioidedificio", OracleType.Number) { Value = OidEdificio };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetDestinatariMail_TicketAssistenza(CorrenteUser, OidTicketAssistenza);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetDestinatariMail_TicketAssistenza(CorrenteUser, OidTicketAssistenza);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    //pk_mail.getdestinatarimail(iuser => :iuser,
                    //                           ioidedificio => :ioidedificio,
                    //                           oerrormsg => :oerrormsg,
                    //                           io_cursor => :io_cursor);
                    OracleCommand command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.GetDestMailTicketAss";
                    command.CommandType = CommandType.StoredProcedure;

                    var pp = new OracleParameter("iuser", OracleType.VarChar, 250) { Value = CorrenteUser };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("OidTicketAssistenza", OracleType.Number) { Value = OidTicketAssistenza };
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
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetStatoTrasmesso(UserNameCorrente, OidRdL, ref Esito);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetStatoTrasmesso(UserNameCorrente, OidRdL, ref Esito);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }

            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.SetStatoTrasmesso";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("irdl", OracleType.Number) { Value = OidRdL };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oesito", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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


        public string SetStatoTrasmessoTicket(string UserNameCorrente, int OidRdL, ref string Esito)
        {
            var msg = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.SetStatoTrasmessoTicket";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iUser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("OidTicketAssistenza", OracleType.Number) { Value = OidRdL };
                    command.Parameters.Add(pp);
                    pp = null;

                    //pp = new OracleParameter("oesito", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    //pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    //Esito = command.Parameters["oesito"].Value.ToString();

                    //msg = command.Parameters["oErrorMsg"].Value.ToString();
                    //if (msg != null && !msg.Equals(string.Empty))
                    //{
                    //    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    //}

                }
            }
            catch (Exception ex)
            {
                msg = string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message);
                //  cl.TxtLogSpedizioni(msg, true);
                throw new Exception(msg);
            }
            return Esito;

        }






        public string SechedulaPOI(string UserNameCorrente, int OidImpianto, enTrimestre tr, DateTime DataConferma)
        {
            var msg = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SechedulaPOI(UserNameCorrente, OidImpianto, tr, DataConferma);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SechedulaPOI(UserNameCorrente, OidImpianto, tr, DataConferma);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            try
            {
                using (var OrclConn = new OracleConnection(CAMS.Module.Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mail.SetStatoTrasmesso";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;
                    pp = new OracleParameter("iOidImpianto", OracleType.Number) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oesito", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("oErrorMsg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
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

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.GetPOISchedulato(UserNameCorrente, OidImpianto, Anno, DataConferma);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.GetPOISchedulato(UserNameCorrente, OidImpianto, Anno, DataConferma);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }



            System.ComponentModel.BindingList<CAMS.Module.DBTask.POI.DCListPOI> objects
                               = new System.ComponentModel.BindingList<CAMS.Module.DBTask.POI.DCListPOI>();

            try
            {
                using (var OrclConn = new OracleConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    //pk_poi.getpoi_data_conferma(iimpianto => :iimpianto,
                    //         ianno => :ianno,
                    //         idataconferma => :idataconferma,
                    //         io_cursor => :io_cursor,
                    //         omessagg => :omessagg);

                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_poi.getpoi_data_conferma";//  poi
                    command.CommandType = CommandType.StoredProcedure;

                    //OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 200) { Value = UserNameCorrente };
                    //command.Parameters.Add(pp);
                    //pp = null;

                    OracleParameter pp = new OracleParameter("iimpianto", OracleType.Number) { Value = OidImpianto };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("ianno", OracleType.Number) { Value = Anno };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("idataconferma", OracleType.DateTime) { Value = DataConferma };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("omessagg", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    msg = command.Parameters["omessagg"].Value.ToString();
                    if (msg != null && !msg.Equals(string.Empty))
                    {
                        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente; {0}", msg));
                    }

                    var dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
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
                                Gennaio = dr.GetString(dr.GetOrdinal("GENNAIO")),
                                Febbraio = dr.GetString(dr.GetOrdinal("FEBBRAIO")),
                                Marzo = dr.GetString(dr.GetOrdinal("MARZO")),
                                Aprile = dr.GetString(dr.GetOrdinal("APRILE")),
                                Maggio = dr.GetString(dr.GetOrdinal("MAGGIO")),
                                Giugno = dr.GetString(dr.GetOrdinal("GIUGNO")),
                                Luglio = dr.GetString(dr.GetOrdinal("LUGLIO")),
                                Agosto = dr.GetString(dr.GetOrdinal("AGOSTO")),
                                Settembre = dr.GetString(dr.GetOrdinal("SETTEMBRE")),
                                Ottobre = dr.GetString(dr.GetOrdinal("OTTOBRE")),
                                Novembre = dr.GetString(dr.GetOrdinal("NOVEMBRE")),
                                Dicembre = dr.GetString(dr.GetOrdinal("DICEMBRE")),
                                DurataAttivita = dr.GetString(dr.GetOrdinal("DURATAATTIVITA")),
                                MaterialeUtilizzato = dr.GetString(dr.GetOrdinal("MATERIALEUTILIZZATO")),
                                Note = dr.GetString(dr.GetOrdinal("NOTE")),
                                Priorita = dr.GetString(dr.GetOrdinal("PRIORITA")),
                                PrioritaIntervento = dr.GetString(dr.GetOrdinal("PRIORITAINTERVENTO")),
                                Trimestre = dr.GetString(dr.GetOrdinal("TRIMESTRE")),
                                Anno = dr.GetString(dr.GetOrdinal("ANNO"))

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
            var msg = string.Empty;

            try
            {
                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                {
                    using (ClassiMSDB.DB msdb = new ClassiMSDB.DB())
                    {
                        return msdb.SetCambioPassword(  UserNameCorrente,   OldPassword,   DataConferma,   Flag);
                    }
                }
                else
                {
                    using (ClassiORADB.DB msdb = new ClassiORADB.DB())
                    {
                        return msdb.SetCambioPassword(  UserNameCorrente,   OldPassword,   DataConferma,   Flag);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }


            return msg;

        }

    }
}
