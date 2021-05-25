using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlanner;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBMaps;
using CAMS.Module.Extensions;

namespace CAMS.Module.DBMaps.DC
{
    [DefaultClassOptions, Persistent("APPARATOCTRSTATICA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Verifica Staticità Sostegni")]
    [NavigationItem("Avvisi Periodici")]
    [ImageName("BO_Lead")]
    public class FindSostegniCTRStatica : XPObject
    {
        public FindSostegniCTRStatica() : base()
        {
        }

        public FindSostegniCTRStatica(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            AnnoRicerca = "Tutti";
        }
        private BindingList<SostegniCTRStatica> nonPersistentObjectBindingList; //BindingList<SomeObject> nonPersistentObjectBindingList;

        private void EnsureNonPersistentObjectBindingList()
        {

            if (nonPersistentObjectBindingList == null)
                if (this.Servizio != null)
                {
                    int OidCommessa = Servizio.Immobile.Contratti.Oid;
                    int Anno = int.Parse(AnnoRicerca);
                   
                    nonPersistentObjectBindingList = new BindingList<SostegniCTRStatica>();



                    Session Sess = this.Session;// ((XPObjectSpace)ObjectSpace).Session; //this.Session
                    XPQuery<SostegniVerificaStatica> qRegRDL = new XPQuery<SostegniVerificaStatica>(Sess);                  
                   
                    List<SostegniCTRStatica> ListaPali = qRegRDL                                           
                                            
                                              .Where(w => w.OidCommessa== OidCommessa)
                                              .Where(w => w.Anno == Anno)
                                            .Select(s => new SostegniCTRStatica
                                            {
                                                Oid = Guid.NewGuid(),
                                                Title = s.CodApparato,
                                                Latitude = Convert.ToDouble(s.Latitude),
                                                Longitude = Convert.ToDouble(s.Longitude),
                                                IndividualMarkerIcon = s.IndividualMarkerIcon,
                                                Anno = AnnoRicerca,
                                                Prescrizione=s.Prescrizione,
                                                Stato = s.Stato.ToString()=="Eseguito" ? TipoVerificaStatica.Eseguito : TipoVerificaStatica.Pianificato,
                                            })
                                  .ToList<SostegniCTRStatica>();




                    //Session Sess = this.Session;// ((XPObjectSpace)ObjectSpace).Session; //this.Session
                    //XPQuery<RdL> qRDL = new XPQuery<RdL>(Sess);
                    //XPQuery<MpAttivitaPianificateDett> qMPPianDet = new XPQuery<MpAttivitaPianificateDett>(Sess);
                    //int[] statiEseguito = new[] { 4, 5, 8, 9 };
                    //List<SostegniCTRStatica> ListaPali = qMPPianDet
                    //                         .Join(qRDL,
                    //                         pd => pd.RdL.Oid,
                    //                         rdl => rdl.Oid,
                    //                         (pd, rdl) => new { PD = pd, RDL = rdl }
                    //                         )
                    //                          .Where(w => new int[] { 1, 5 }.Contains(w.RDL.Categoria.Oid)
                    //                          && new int[] { 1238, 1870 }.Contains(w.RDL.Apparato.StdApparato.Oid)
                    //                          && new int[] { 1, 2, 3, 4, 6, 10, 11 }.Contains(w.RDL.UltimoStatoSmistamento.Oid)
                    //                          && w.RDL.Immobile.Commesse.Oid == OidCommessa
                    //                          && Anni.Contains(w.RDL.DataPianificata.Year)
                    //                          && stati.Contains(w.RDL.UltimoStatoSmistamento.Oid)  //(stati == 0 || (stati == 4 && w.RDL.UltimoStatoSmistamento.Oid == 4) || (stati == 1 && w.RDL.UltimoStatoSmistamento.Oid != 4))
                    //                          && w.RDL.Apparato.Abilitato == FlgAbilitato.Si
                    //                          && new int[] { 3856, 3857, 3858, 3859, 3855, 3860, 3968, 3969, 3970, 3971, 3972, 3973, 3974, 3975,3976,3977,3978,3979 }.Contains(w.PD.MpAttPianificate.ApparatoSchedaMP.SchedaMp.Oid))
                    //                        .Select(s => new SostegniCTRStatica
                    //                        {
                    //                            Oid = Guid.NewGuid(),
                    //                            Title = s.RDL.DataPianificata.Year.ToString(),
                    //                            Latitude = Convert.ToDouble(s.RDL.Apparato.GeoLocalizzazione.Latitude),
                    //                            Longitude = Convert.ToDouble(s.RDL.Apparato.GeoLocalizzazione.Longitude),
                    //                            IndividualMarkerIcon = @"C:\AssemblaEAMS\EAMS\CAMS.Web\Images\CS.png",
                    //                            Anno = s.RDL.DataPianificata.Year.ToString(),
                    //                            Stato = statiEseguito.Contains<int>(s.RDL.UltimoStatoSmistamento.Oid) ? TipoVerificaStatica.Eseguito : TipoVerificaStatica.Pianificato,
                    //                        })
                    //              .ToList<SostegniCTRStatica>();
                    

                    ListaPali = ListaPali.DistinctBy(x => x.Oid).ToList();

                    nonPersistentObjectBindingList = new BindingList<SostegniCTRStatica>(ListaPali.ToList());


                    //for (int i = 1; i < 10; i++)
                    //{
                    //    SostegniCTRStatica aa = new SostegniCTRStatica();
                    //    aa.Oid = Guid.NewGuid();
                    //    aa.Title = i.ToString();
                    //    aa.Anno = i.ToString();

                    //    double la = 41.80965421 + (double.Parse(i.ToString()) / 10);
                    //    double lg = 13.5588;
                    //    aa.Latitude = la;
                    //    aa.Longitude = lg;
                    //    aa.IndividualMarkerIcon = "";
                    //    nonPersistentObjectBindingList.Add(aa);
                    //}
                    //nonPersistentObjectBindingList.Add(new SostegniCTRStatica("Object 2"));
                    //nonPersistentObjectBindingList.Add(new SostegniCTRStatica("Object 3"));
                }
        }

        private int ContaSostegni()
        {
            int nrpali = 0;
            if (nonPersistentObjectBindingList != null)
                if (this.Servizio != null)
                {
                    int OidCommessa = Servizio.Immobile.Contratti.Oid;
                    int Anno = int.Parse(AnnoRicerca);
                    nonPersistentObjectBindingList = new BindingList<SostegniCTRStatica>();

                    Session Sess = this.Session;// ((XPObjectSpace)ObjectSpace).Session; //this.Session
                    XPQuery<SostegniVerificaStatica> qRegRDL = new XPQuery<SostegniVerificaStatica>(Sess);
                    nrpali = qRegRDL
                    .Where(w => w.OidCommessa == OidCommessa)
                    .Where(w => w.Anno == Anno)
                    .Count()
                    ;

                }
            return nrpali;
        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), XafDisplayName("Immobile")]
        [DataSourceCriteria("[<MpAttivitaPianificateDett>][^.Oid = MpAttPianificate.Immobile.Oid].Count() > 0")]
        //[RuleRequiredField("RuleReq.RegoleAutoAssegnazioneRdL.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        //[Appearance("RegoleAutoAssegnazioneRdL.Abilita.Immobile", Criteria = "not (Impianto  is null)", Enabled = false)]
        [ImmediatePostData(true)]
        public Immobile Immobile
        {
            get { return fImmobile; }
            set { SetPropertyValue<Immobile>("Immobile", ref fImmobile, value); }
        }

        private Servizio fServizio;
        [Persistent("SERVIZIO"), XafDisplayName("Servizio")]
        //[Appearance("RegoleAutoAssegnazioneRdL.Abilita.Impianto", Criteria = "(Immobile  is null) OR (not (RisorseTeam is null))", Enabled = false)]
        //[RuleRequiredField("RuleReq.RegoleAutoAssegnazioneRdL.Impianto", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get { return fServizio; }
            set { SetPropertyValue<Servizio>("Servizio", ref fServizio, value); }
        }

        [DataSourceCriteria("Impianto = '@This.Impianto'")]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Asset Apparato
        {
            get { return GetDelayedPropertyValue<Asset>("Apparato"); }
            set { SetDelayedPropertyValue<Asset>("Apparato", value); }
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public string RicercaEdificio { get; set; }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public string RicercaApparato { get; set; }


        [NonPersistent, Size(50)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public string ContoSostegni 
        {
            get 
            {
                return ContaSostegni().ToString();
            } 
        
        }


        private string fAnnoRicerca;
        [Persistent("ANNO")]
        [XafDisplayName("Anni ricerca")]
        // [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", STR_Format)]
        //[ DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[Appearance("RegPianiMP.Anno.Enabled", Enabled = false, Criteria = "IsNullOrEmpty(Descrizione) Or Scenario != null", Priority = 1)]
        [ImmediatePostData]
        //[DataSourceCriteria("Anno > 2014")]
        public string AnnoRicerca
        {
            get { return fAnnoRicerca; }
            set { SetPropertyValue<string>("AnnoRicerca", ref fAnnoRicerca, value); }
        }
        //[System.ComponentModel.DisplayName(Captions.PropertyEditors_CollectionProperties_ReadOnlyBindingListProperty)]
        [XafDisplayName("Lista")]
        public BindingList<SostegniCTRStatica> NonPersistentBindingList  //BindingList<SomeObject> NonPersistentBindingList
        {
            get
            {
                EnsureNonPersistentObjectBindingList();
                if (nonPersistentObjectBindingList != null)
                {
                    nonPersistentObjectBindingList.AllowRemove = false;
                    nonPersistentObjectBindingList.AllowNew = false;
                    nonPersistentObjectBindingList.AllowEdit = false;
                }
                return nonPersistentObjectBindingList;
            }
        }
    }
}
