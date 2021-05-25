using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;


namespace CAMS.Module.Classi
{
    public class DBApps : IDisposable
    {
#pragma warning disable CS0618 // 'OracleConnection' è obsoleto: 'OracleConnection has been deprecated. http://go.microsoft.com/fwlink/?LinkID=144260'
        public OracleConnection OracleConnection { get; set; }
#pragma warning restore CS0618 // 'OracleConnection' è obsoleto: 'OracleConnection has been deprecated. http://go.microsoft.com/fwlink/?LinkID=144260'
        /// accessi           
        public string CorrenteUser { get; set; }

        public void Dispose()
        {
            if (OracleConnection != null)
            {
                OracleConnection.Dispose();
                OracleConnection = null;
            }
        }

        /// <summary>
        /// </summary>
        public string EseguiProceduraDB(
            string StrConnessione, string Azione,
            int RegOid_old, int RegOid_new,
            int SSoperativoOid_old, int SSoperativoOid_new, string NoteOperative,
            string NoteCompletamento, string DataCompletamento, string OraCompletamento,
            int OidTeamRisorsa, string UserName,
            double Geolat, double Geolng, string Geoindirizzo, int PCRProCausa, int PCRCausaRimedio,
            ref int StatoDisponibilita)
        {
            var Messaggi = "Operazione non riuscita";
            try
            {
                const string ora_oggi = "00:00";
                var data_oggi = DateTime.MinValue.Date.ToShortDateString();
                #region valori default
                //if (string.IsNullOrEmpty(DataInizioLavori))
                //{
                //    DataInizioLavori = data_oggi;
                //}
                //if (string.IsNullOrEmpty(DataRiavvio))
                //{
                //    DataRiavvio = data_oggi;
                //}
                if (string.IsNullOrEmpty(DataCompletamento))
                {
                    DataCompletamento = data_oggi;
                }

                //if (string.IsNullOrEmpty(OraInizioLavori))
                //{
                //    OraInizioLavori = ora_oggi;
                //}
                //if (string.IsNullOrEmpty(OraRiavvio))
                //{
                //    OraRiavvio = ora_oggi;
                //}
                if (string.IsNullOrEmpty(OraCompletamento))
                {
                    OraCompletamento = ora_oggi;
                }
                #endregion
                //         public string EseguiProceduraDB(string StrConn, string Azione, int RegOid_old,  int RegOid_new, int SSoperativoOid_old,
                //    int SSoperativoOid_new, string NoteCompletamento, int RisorsaOid, int Utente, int IdCausaRimedio,
                //    string DataInizioLavori,    string OraInizioLavori,   string DataRiavvio,   string OraRiavvio,
                //    string DataCompletamento,   string OraCompletamento,  double    Geolat,     double  Geolng, string geoindirizzo)
                //{

                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mobile.sp_gestionestatooperativoodl";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter p_azione = new OracleParameter("p_azione", OracleType.VarChar) { Value = Azione };
                    command.Parameters.Add(p_azione);

                    OracleParameter p_regrdl_old = new OracleParameter("p_regrdl_old", OracleType.Number) { Value = RegOid_old }; //regIDAttivo
                    command.Parameters.Add(p_regrdl_old);

                    OracleParameter p_regrdl_new = new OracleParameter("p_regrdl_new", OracleType.Number) { Value = RegOid_new };//model.regIDDaAttivare
                    command.Parameters.Add(p_regrdl_new);

                    OracleParameter p_statooperativo_old = new OracleParameter("p_statooperativo_old", OracleType.Number) { Value = SSoperativoOid_old }; // model.SOSospeso
                    command.Parameters.Add(p_statooperativo_old);

                    OracleParameter p_statooperativo_new = new OracleParameter("p_statooperativo_new", OracleType.Number) { Value = SSoperativoOid_new }; //model.SOAttivo
                    command.Parameters.Add(p_statooperativo_new);

                    OracleParameter p_note_completamento = new OracleParameter("p_note_completamento", OracleType.VarChar) { Value = NoteCompletamento };
                    command.Parameters.Add(p_note_completamento); //model.NoteCompletamento 

                    OracleParameter p_risorseteam_id = new OracleParameter("p_risorseteam_id", OracleType.Number) { Value = OidTeamRisorsa }; //Team.Oid 
                    command.Parameters.Add(p_risorseteam_id);

                    OracleParameter p_operatore = new OracleParameter("p_operatore", OracleType.VarChar) { Value = UserName }; //TODO: OPERATORE REALE
                    command.Parameters.Add(p_operatore);

                    //OracleParameter p_data_inizio_lavori = new OracleParameter("p_data_inizio_lavori", OracleType.VarChar) { Value = DataInizioLavori };
                    //command.Parameters.Add(p_data_inizio_lavori);

                    //OracleParameter p_ora_inizio_lavori = new OracleParameter("p_ora_inizio_lavori", OracleType.VarChar) { Value = OraInizioLavori };
                    //command.Parameters.Add(p_ora_inizio_lavori);

                    //OracleParameter p_data_riavvio = new OracleParameter("p_data_riavvio", OracleType.VarChar) { Value = DataRiavvio };
                    //command.Parameters.Add(p_data_riavvio);

                    //OracleParameter p_ora_riavvio = new OracleParameter("p_ora_riavvio", OracleType.VarChar) { Value = OraRiavvio };
                    //command.Parameters.Add(p_ora_riavvio);

                    OracleParameter p_data_completamento = new OracleParameter("p_data_completamento", OracleType.VarChar) { Value = DataCompletamento };
                    command.Parameters.Add(p_data_completamento);

                    OracleParameter p_ora_completamento = new OracleParameter("p_ora_completamento", OracleType.VarChar) { Value = OraCompletamento };
                    command.Parameters.Add(p_ora_completamento);


                    OracleParameter p_Geolat = new OracleParameter("p_geolat", OracleType.Number) { Value = Geolat };
                    command.Parameters.Add(p_Geolat);

                    OracleParameter p_Geolng = new OracleParameter("p_geolng", OracleType.Number) { Value = Geolng };
                    command.Parameters.Add(p_Geolng);

                    OracleParameter p_geoindirizzo = new OracleParameter("p_geoindirizzo", OracleType.VarChar) { Value = Geoindirizzo };
                    command.Parameters.Add(p_geoindirizzo);

                    ////--------------NUOVO   p_pcrprobcausa

                    OracleParameter p_pcrprobcausa = new OracleParameter("p_pcrprobcausa", OracleType.Number) { Value = PCRProCausa };
                    command.Parameters.Add(p_pcrprobcausa);

                    ////--------------NUOVO   p_pcrcausarimedio   p_datamobile

                    OracleParameter p_pcrcausarimedio = new OracleParameter("p_pcrcausarimedio", OracleType.Number) { Value = PCRCausaRimedio };
                    command.Parameters.Add(p_pcrcausarimedio);

                    OracleParameter p_datamobile = new OracleParameter("p_datamobile", OracleType.DateTime) { Value = DateTime.Now };
                    command.Parameters.Add(p_datamobile);

                    OracleParameter p_note_operative = new OracleParameter("p_noteoper", OracleType.VarChar) { Value = NoteOperative };
                    command.Parameters.Add(p_note_operative);

                    OracleParameter p_stdisprisorsa = new OracleParameter("p_stdisprisorsa", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(p_stdisprisorsa);

                    OracleParameter p_msg_out = new OracleParameter("p_msg_out", OracleType.VarChar, 2000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(p_msg_out);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    string sStatoDisponibilita = command.Parameters["p_stdisprisorsa"].Value.ToString();
                    if (!int.TryParse(sStatoDisponibilita, out StatoDisponibilita))
                        StatoDisponibilita = 0;


                    Messaggi = command.Parameters["p_msg_out"].Value.ToString();
                    if (Messaggi != null && Messaggi.Contains("Errore"))
                    {
                        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti; {0}", Messaggi));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggi;
        }



        public string GetStatoDisponibilita(string UserNameCorrente, int OidCategoria, int OidEqstd, int OidSistema)
        {
            var CodPMP = string.Empty;
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "SP_GETCO";
                    command.CommandType = CommandType.StoredProcedure;
                    var pp = new OracleParameter("oidRegrdl", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("onumber", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;
                    OrclConn.Open();
                    command.ExecuteNonQuery();


                    var Messaggi = command.Parameters["onumber"].Value.ToString();
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


        /// <summary>
        /// 
        public string AggiornaDateOperative(string StrConnessione,
            int RegOid,
            string NoteRegistro, int OidTeamRisorsa,
            // PCRAppaPro, int PCRProCausa, int PCRCausaRimedio,
            string DataFermo, string OraFermo,
             string DataRiavvio, string OraRiavvio,
             string DataDataSopralluogo, string OraDataSopralluogo,
            string DataDataAzioniTampone, string OraDataAzioniTampone,
            string DataDataInizioLavori, string OraDataInizioLavori,
             ref int StatoDisponibilita)
        {
            var Messaggi = "Operazione non riuscita";
            try
            {
                const string ora_oggi = "00:00";
                var data_oggi = DateTime.MinValue.Date.ToShortDateString();
                #region valori default
                if (string.IsNullOrEmpty(DataDataInizioLavori))
                {
                    DataDataInizioLavori = data_oggi;
                }
                if (string.IsNullOrEmpty(DataRiavvio))
                {
                    DataRiavvio = data_oggi;
                }
                //if (string.IsNullOrEmpty(DataCompletamento))
                //{
                //    DataCompletamento = data_oggi;
                //}

                if (string.IsNullOrEmpty(OraDataInizioLavori))
                {
                    OraDataInizioLavori = ora_oggi;
                }
                if (string.IsNullOrEmpty(OraRiavvio))
                {
                    OraRiavvio = ora_oggi;
                }
                //if (string.IsNullOrEmpty(OraCompletamento))
                //{
                //    OraCompletamento = ora_oggi;
                //}
                #endregion


                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mobile.SP_AGGDATEREGRDL";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter p_regrdl = new OracleParameter("p_regrdl", OracleType.Number) { Value = RegOid }; //regIDAttivo
                    command.Parameters.Add(p_regrdl);

                    OracleParameter p_note_registro = new OracleParameter("p_note_rdl", OracleType.VarChar) { Value = NoteRegistro };
                    command.Parameters.Add(p_note_registro); //model.NoteRegistro 

                    OracleParameter p_risorseteam_id = new OracleParameter("p_risorsateam", OracleType.Number) { Value = OidTeamRisorsa }; //Team.Oid 
                    command.Parameters.Add(p_risorseteam_id);


                    OracleParameter p_data_fermo = new OracleParameter("p_data_fermo", OracleType.VarChar) { Value = DataFermo };
                    command.Parameters.Add(p_data_fermo);

                    OracleParameter p_ora_fermo = new OracleParameter("p_ora_fermo", OracleType.VarChar) { Value = OraFermo };
                    command.Parameters.Add(p_ora_fermo);

                    OracleParameter p_data_riavvio = new OracleParameter("p_data_riavvio", OracleType.VarChar) { Value = DataRiavvio };
                    command.Parameters.Add(p_data_riavvio);

                    OracleParameter p_ora_riavvio = new OracleParameter("p_ora_riavvio", OracleType.VarChar) { Value = OraRiavvio };
                    command.Parameters.Add(p_ora_riavvio);

                    OracleParameter P_Data_Data_Sopralluogo = new OracleParameter("p_data_sopralluogo", OracleType.VarChar) { Value = DataDataSopralluogo };
                    command.Parameters.Add(P_Data_Data_Sopralluogo);

                    OracleParameter P_Ora_Data_Sopralluogo = new OracleParameter("p_ora_sopralluogo", OracleType.VarChar) { Value = OraDataSopralluogo };
                    command.Parameters.Add(P_Ora_Data_Sopralluogo);

                    OracleParameter P_Data_Data_Azioni_Tampone = new OracleParameter("p_data_azioni_tampone", OracleType.VarChar) { Value = DataDataAzioniTampone };
                    command.Parameters.Add(P_Data_Data_Azioni_Tampone);

                    OracleParameter P_Ora_Data_Azioni_Tampone = new OracleParameter("p_ora_azioni_tampone", OracleType.VarChar) { Value = OraDataAzioniTampone };
                    command.Parameters.Add(P_Ora_Data_Azioni_Tampone);

                    OracleParameter p_data_inizio_lavori = new OracleParameter("p_data_inizio_lavori", OracleType.VarChar) { Value = DataDataInizioLavori };
                    command.Parameters.Add(p_data_inizio_lavori);

                    OracleParameter p_ora_inizio_lavori = new OracleParameter("p_ora_inizio_lavori", OracleType.VarChar) { Value = OraDataInizioLavori };
                    command.Parameters.Add(p_ora_inizio_lavori);

                    //OracleParameter p_data_completamento = new OracleParameter("p_data_completamento", OracleType.VarChar) { Value = DataCompletamento };
                    //command.Parameters.Add(p_data_completamento);

                    //OracleParameter p_ora_completamento = new OracleParameter("p_ora_completamento", OracleType.VarChar) { Value = OraCompletamento };
                    //command.Parameters.Add(p_ora_completamento);
                    //--------------  le altre  date

                    //--------------NUOVO   p_pcrappprob  p_pcrprobcausa  p_pcrcausarimedio
                    //OracleParameter p_pcrappprob = new OracleParameter("p_pcrappprob", OracleType.Number) { Value = PCRAppaPro };
                    //command.Parameters.Add(p_pcrappprob);
                    //OracleParameter p_pcrprobcausa = new OracleParameter("p_pcrprobcausa", OracleType.Number) { Value = PCRProCausa };
                    //command.Parameters.Add(p_pcrprobcausa);
                    //OracleParameter p_pcrcausarimedio = new OracleParameter("p_pcrcausarimedio", OracleType.Number) { Value = PCRCausaRimedio };
                    //command.Parameters.Add(p_pcrcausarimedio);

                    //--------------NUOVO   p_datamobile
                    OracleParameter p_datamobile = new OracleParameter("p_datamobile", OracleType.DateTime) { Value = DateTime.Now };
                    command.Parameters.Add(p_datamobile);

                    OracleParameter p_stdisprisorsa = new OracleParameter("p_disprisorsa", OracleType.Number) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(p_stdisprisorsa);

                    OracleParameter p_msg_out = new OracleParameter("p_msg_out", OracleType.VarChar, 2000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(p_msg_out);

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    string sStatoDisponibilita = command.Parameters["p_disprisorsa"].Value.ToString();
                    if (!int.TryParse(sStatoDisponibilita, out StatoDisponibilita))
                        StatoDisponibilita = 0;


                    Messaggi = command.Parameters["p_msg_out"].Value.ToString();
                    if (Messaggi != null && Messaggi.Contains("Errore"))
                    {
                        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti; {0}", Messaggi));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
            }
            return Messaggi;
        }

        //public string SetLoginToken(string UserNameCorrente, string vSessionID)
        //{
        //    string Messaggi = string.Empty;
        //    try
        //    {
        //        using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mobile_accont.sp_settoken";
        //            //  pk_mobile_accont.sp_ws_settoken(iuser => :iuser, isessioneid => :isessioneid, omessaggio => :omessaggio);
        //            command.CommandType = CommandType.StoredProcedure;

        //            OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;


        //            pp = new OracleParameter("isessioneid", OracleType.VarChar, 100) { Value = vSessionID };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;
        //            OrclConn.Open();
        //            command.ExecuteNonQuery();


        //            Messaggi = command.Parameters["omessaggio"].Value.ToString();
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

        //pk_mobile_accont.sp_gettoken(iuser => :iuser,  isessioneid => :isessioneid,
        //                             io_cursor => :io_cursor, omessaggio => :omessaggio);


        //public int GetLoginTokenVariabile(string UserNameCorrente, string vSessionID, out  string Messaggio,
        //   out  string VisCompletaAttivita, out  string VisArrivoinSito, out  string VisDichiarazioneFinegiornata,
        //out  string VisFineGiornataLavorativa, out  string VisTrasfperAcquistoMateriali, out  string VisFlussoBreve,
        //    out  string RisorsainLavorazione,
        //  out  int RegRdL, out  int UltimoStatoOperativo
        //    )
        //{
        //    VisCompletaAttivita = "False";
        //    VisArrivoinSito = "False";
        //    VisDichiarazioneFinegiornata = "False";
        //    VisFineGiornataLavorativa = "False";
        //    VisTrasfperAcquistoMateriali = "False";
        //    RisorsainLavorazione = "False";

        //    VisFlussoBreve = "False";

        //    RegRdL = 0;
        //    UltimoStatoOperativo = 0;
        //    string Messaggi = string.Empty;
        //    int Attivo = 0;
        //    try
        //    {
        //        using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mobile_accont.sp_gettokenvaribili";
        //            command.CommandType = CommandType.StoredProcedure;

        //            OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("isessioneid", OracleType.VarChar, 100) { Value = vSessionID };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("io_cursor", OracleType.Cursor);
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);

        //            pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            // OracleDataReader dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
        //            //  DataTable dt = new DataTable();            // dt.Load((System.Data.IDataReader)dr);                    
        //            using (OracleDataReader reader = (OracleDataReader)command.Parameters["io_cursor"].Value)
        //            {
        //                // Always call Read before accessing data.  List.Add(dr.GetInt32(dr.GetOrdinal("OidRisorsa")),
        //                while (reader.Read())
        //                {
        //                    Attivo = reader.GetInt32(reader.GetOrdinal("ATTIVO"));
        //                    VisCompletaAttivita = reader.GetString(reader.GetOrdinal("VISCOMPLETAATTIVITA"));

        //                    VisArrivoinSito = reader.GetString(reader.GetOrdinal("VisArrivoinSito"));
        //                    VisDichiarazioneFinegiornata = reader.GetString(reader.GetOrdinal("VisDichiarazioneFinegiornata"));
        //                    VisFineGiornataLavorativa = reader.GetString(reader.GetOrdinal("VisFineGiornataLavorativa"));
        //                    VisTrasfperAcquistoMateriali = reader.GetString(reader.GetOrdinal("VisTrasfperAcquistoMateriali"));
        //                    VisCompletaAttivita = reader.GetString(reader.GetOrdinal("VISCOMPLETAATTIVITA"));

        //                    RisorsainLavorazione = reader.GetString(reader.GetOrdinal("RisorsainLavorazione"));

        //                    VisFlussoBreve = reader.GetString(reader.GetOrdinal("VisFlussoBreve")); // nuovo

        //                    RegRdL = reader.GetInt32(reader.GetOrdinal("REGRDL"));

        //                    UltimoStatoOperativo = reader.GetInt32(reader.GetOrdinal("ULTIMOSTATOOPERATIVO"));
        //                    break;
        //                }
        //            }

        //            Messaggio = command.Parameters["omessaggio"].Value.ToString();
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
        //    return Attivo;
        //}


        //public int GetLoginToken(string UserNameCorrente, string vSessionID, string vurl, out  string Messaggio)
        //{
        //    string Messaggi = string.Empty;
        //    int Attivo = 0;
        //    try
        //    {
        //        using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
        //        {
        //            var command = OrclConn.CreateCommand();
        //            command.CommandText = "pk_mobile_accont.sp_getTokenVaribiliTrace";
        //            command.CommandType = CommandType.StoredProcedure;

        //            OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 100) { Value = UserNameCorrente };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("isessioneid", OracleType.VarChar, 100) { Value = vSessionID };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("iurl", OracleType.VarChar, 100) { Value = vurl };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            pp = new OracleParameter("io_cursor", OracleType.Cursor);
        //            pp.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(pp);

        //            pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(pp);
        //            pp = null;

        //            OrclConn.Open();
        //            command.ExecuteNonQuery();

        //            // OracleDataReader dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
        //            //  DataTable dt = new DataTable();            // dt.Load((System.Data.IDataReader)dr);                    
        //            using (OracleDataReader reader = (OracleDataReader)command.Parameters["io_cursor"].Value)
        //            {
        //                // Always call Read before accessing data.  List.Add(dr.GetInt32(dr.GetOrdinal("OidRisorsa")),
        //                while (reader.Read())
        //                {
        //                    Attivo = reader.GetInt32(reader.GetOrdinal("ATTIVO"));
        //                    break;
        //                }
        //            }

        //            Messaggio = command.Parameters["omessaggio"].Value.ToString();
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
        //    return Attivo;
        //}

        public int GetLoginAutenticato(string UserNameCorrente, string vSessionID, string vurl, out  string Messaggio,
                                                                      out  int RisorsaTeamOID, out  int RisorsaOID,
                                                                       out  int AttAccOID, out  int RegRdLOID)
        {
            string Messaggi = string.Empty;
            int Attivo = 0;
            RisorsaTeamOID = 0;
            RisorsaOID = 0;
            AttAccOID = 0;
            RegRdLOID = 0;
            try
            {
                using (var OrclConn = new OracleConnection(Classi.SetVarSessione.OracleConnString))
                {
                    var command = OrclConn.CreateCommand();
                    command.CommandText = "pk_mobile_accont.sp_gettokenautenticato";
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter pp = new OracleParameter("iuser", OracleType.VarChar, 100) { Value = UserNameCorrente };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("isessioneid", OracleType.VarChar, 100) { Value = vSessionID };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("iurl", OracleType.VarChar, 100) { Value = vurl };
                    command.Parameters.Add(pp);
                    pp = null;

                    pp = new OracleParameter("io_cursor", OracleType.Cursor);
                    pp.Direction = ParameterDirection.Output;
                    command.Parameters.Add(pp);

                    pp = new OracleParameter("omessaggio", OracleType.VarChar, 4000) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(pp);
                    pp = null;

                    OrclConn.Open();
                    command.ExecuteNonQuery();

                    // OracleDataReader dr = (OracleDataReader)command.Parameters["io_cursor"].Value;
                    //  DataTable dt = new DataTable();            // dt.Load((System.Data.IDataReader)dr);                    
                    using (OracleDataReader reader = (OracleDataReader)command.Parameters["io_cursor"].Value)
                    {
                        // Always call Read before accessing data.  List.Add(dr.GetInt32(dr.GetOrdinal("OidRisorsa")),
                        while (reader.Read())
                        {
                            Attivo = reader.GetInt32(reader.GetOrdinal("ATTIVO"));
                            RisorsaTeamOID = reader.GetInt32(reader.GetOrdinal("RISORSATEAMOID"));
                            RisorsaOID = reader.GetInt32(reader.GetOrdinal("RISORSAOID"));

                            AttAccOID = reader.GetInt32(reader.GetOrdinal("ATTACCOID"));
                            RegRdLOID = reader.GetInt32(reader.GetOrdinal("REGRDLOID"));
                            break;
                        }
                    }

                    Messaggio = command.Parameters["omessaggio"].Value.ToString();
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
            return Attivo;
        }




    }
}
