using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using CAMS.Module.DBPlanner;
using CAMS.Module.DBTask;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("CLUSTEREDIFICIRISORSETEAM")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gruppo Stabili Squadra di Risorse")]
    [ImageName("TeamRisorse")]
    [NavigationItem(false)]
    public class ClusterEdificiRisorseTeam : XPObject
    {
         public ClusterEdificiRisorseTeam() : base() { }

         public ClusterEdificiRisorseTeam(Session session) : base(session) { }

         public override void AfterConstruction() { base.AfterConstruction(); }

         /// <summary>
         /// Link a RegistroRdL
         /// </summary>
         /// <param name="RegistroRdLSelezionato"></param>
         /// <returns></returns>
         //public ClusterEdificiRisorseTeam LinkRdLFrom(RegistroRdL RegistroRdLSelezionato)
         //{
             //var lstRegistroRdLSelezionati = new List<RegistroRdL>();
             //lstRegistroRdLSelezionati.Add(RegistroRdLSelezionato);
             //return LinkRdLFrom(lstRegistroRdLSelezionati);
         //}

         /// <summary>
         /// Link al RDL
         /// </summary>
         /// <param name="lstRegistroRdLSelezionati"></param>
         /// <returns></returns>
         //public ClusterEdificiRisorseTeam LinkRdLFrom(IEnumerable<RegistroRdL> lstRegistroRdLSelezionati)
         //{
             //var IDs = lstRegistroRdLSelezionati.Select(r => r.Oid).ToList();
             //var lstRegistri = Session.Query<RegistroRdL>().Where(r => IDs.Contains(r.Oid));

             //foreach (RdL rdl in lstRegistri.SelectMany(reg => reg.RdLes))
             //{
             //    rdl.ClusterEdificiRisorseTeam = this;
             //}

             //var statoSmistamento = Session.GetObjectByKey<StatoSmistamento>(2);

             //foreach (var registro in lstRegistri)
             //{
             //    registro.CambioStato(statoSmistamento);
             //}

             //return this;
         //}

         [Persistent("CLUSTEREDIFICI"), DisplayName("Cluster Edifici")]
         [Association(@"ClusterEdifici_ClusterEdificiRisorseTeam")]
         [ExplicitLoading]
         [Delayed(true)]
         public ClusterEdifici ClusterEdifici
         {
             get
             {
                 return GetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici");
             }
             set
             {
                 SetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici", value);
             }
         }

         private StatoOperativo fUltimoStatoOperativo;
         [Persistent("ULTIMOSTATOOPERATIVO"),
         DisplayName("Ultimo stato operativo")]
         //[Appearance("RisirseTeam.UltimoStatoOperativo", Enabled = false)]
         [ExplicitLoading]
         public StatoOperativo UltimoStatoOperativo
         {
             get
             {
                 return fUltimoStatoOperativo;
             }
             set
             {
                 SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value);
             }
         }

         private int fNumeroAttivitaSospese;
         [NonPersistent, DisplayName("Numero di attività sospese")]
         public int NumeroAttivitaSospese
         {
             get
             {
         //        var conta = 0;
         //        if (RdLs.Count > 0)
         //        {
         //            var flstSOS = new XPCollection<StatoOperativo>(Session);
         //            var lstSOS = flstSOS.Where(Sto => Sto.Stato.ToString().Contains("Sospeso")).ToList();
         //            var intFasi = lstSOS.Select(fo => (int)fo.Oid).ToArray();
         //            conta = RdLs.Cast<RdL>().Where(r => r.UltimoStatoOperativo != null)
         //                                          .Where(r1 => intFasi.Contains(r1.UltimoStatoOperativo.Oid)).Count();
         //        }
                 return fNumeroAttivitaSospese;
             }
             set
             {
                 SetPropertyValue<int>("NumeroAttivitaSospese", ref fNumeroAttivitaSospese, value);
             }
         }

         private int fNumeroAttivitaAgenda;
         [NonPersistent, DisplayName("Numero di attività in agenda")]
         public int NumeroAttivitaAgenda
         {
             get
             {
         //        var conta = 0;
         //        if (RdLs.Count > 0)
         //        {
         //            var flstSOS = new XPCollection<StatoOperativo>(Session);
         //            var lstSOS = flstSOS.Where(Sto => Sto.Stato.ToString().Contains("In Lavorazione")).ToList();
         //            var intFasi = lstSOS.Select(fo => (int)fo.Oid).ToArray();
         //            conta = RdLs.Cast<RdL>().Where(r => r.UltimoStatoOperativo != null)
         //                                     .Where(r1 => intFasi.Contains(r1.UltimoStatoOperativo.Oid)).Count();
         //        }
                 return fNumeroAttivitaAgenda;
             }
             set
             {
                 SetPropertyValue("NumeroAttivitaAgenda", ref fNumeroAttivitaAgenda, value);
             }
         }

         
         private string fDistanzaImpianto;
         [NonPersistent,
         DisplayName("Distanza Impianto"),
         Size(50)]
         public string DistanzaImpianto
         {
             get
             {
                 return fDistanzaImpianto;
             }
             set
             {
                 SetPropertyValue("DistanzaImpianto", ref fDistanzaImpianto, value);
             }
         }

         private string fAssegnazioneImpianto;
         [NonPersistent, DisplayName("Assegnazione Impianto"), Size(50)]
         public string AssegnazioneImpianto
         {
             get
             {
                 return fAssegnazioneImpianto;
             }
             set
             {
                 SetPropertyValue("AssegnazioneImpianto", ref fAssegnazioneImpianto, value);
             }
         }

         private string fUltimoImpianto;
         [NonPersistent,DisplayName("Ultimo Impianto Visitato"),Size(50)]
         public string UltimoImpianto
         {
             get
             {
                 return fUltimoImpianto;
             }
             set
             {
                 SetPropertyValue("UltimoImpianto", ref fUltimoImpianto, value);
             }
         }

         private Risorse fRisorsaCapo;
         [Persistent("RISORSACAPO"),DisplayName("Risorsa Capo Squadra")]
         [Appearance("RisirseTeam.RisorsaCapo", Enabled = false)]
         [ExplicitLoading]
         public Risorse RisorsaCapo
         {
             get
             {
                 return fRisorsaCapo;
             }
             set
             {
                 SetPropertyValue<Risorse>("RisorsaCapo", ref fRisorsaCapo, value);
             }
         }

         private Ghost fGhost;
         [Persistent("MPGHOST"), DisplayName("Ghost Linkato")]
         //[Association(@"RisorseTeam_Ghost")]
         [Appearance("RisirseTeam.Ghost", Enabled = false)]
         [ExplicitLoading]
         public Ghost Ghost
         {
             get
             {
                 return fGhost;
             }
             set
             {
                 SetPropertyValue<Ghost>("Ghost", ref fGhost, value);
             }
         }

         private int fAnno;
         [Persistent("ANNO"), DisplayName("Anno"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
         [Appearance("RisirseTeam.Anno", Enabled = false)]
         public int Anno
         {
             get
             {
                 return fAnno;
             }
             set
             {
                 SetPropertyValue<int>("Anno", ref fAnno, value);
             }
         }

        //[NonPersistent,
        //DisplayName("Coppia Linkata")]
        //public CoppiaLinkataGhost CoppiaLinkata
        //{
        //    get
        //    {
        //        var TotRisosrse = CoppiaLinkataGhost.NonDefinito;
        //        if (AssRisorseTeam.Count == 1)
        //        {
        //            TotRisosrse = CoppiaLinkataGhost.No;
        //        }
        //        else
        //        {
        //            if (AssRisorseTeam.Count == 2)
        //            {
        //                TotRisosrse = CoppiaLinkataGhost.Si;
        //            }
        //            else
        //            {
        //                if (AssRisorseTeam.Count > 2)
        //                {
        //                    TotRisosrse = CoppiaLinkataGhost.NonCompatibile;
        //                }
        //            }
        //        }
        //        return CoppiaLinkata;
        //    }
        //}

        [NonPersistent,
        DisplayName("Mansione")]
        public String Mansione
        {
            get
            {
                var MansioneRisorse = string.Empty;
                //if (AssRisorseTeam.Count == 0)
                //{
                //    return RisorsaCapo.Mansione.Descrizione.ToString();
                //}
                //else
                //{
                //    foreach (var ele in AssRisorseTeam)
                //    {
                //        if (!MansioneRisorse.ToString().Contains(ele.Risorsa.Mansione.Descrizione))
                //        {
                //            if (MansioneRisorse == string.Empty)
                //            {
                //                MansioneRisorse = string.Format("{0}", ele.Risorsa.Mansione.Descrizione.ToString());
                //            }
                //            else
                //            {
                //                MansioneRisorse = MansioneRisorse + string.Format(",{0}", ele.Risorsa.Mansione.Descrizione.ToString());
                //            }
                //        }
                //    }
                //}
                return MansioneRisorse;
            }
        }

         [PersistentAlias("RisorsaCapo.CentroOperativo"), DisplayName("CentroOperativo")]
         public CentroOperativo CentroOperativo
         {
             get
             {
                 var tempObject = EvaluateAlias("CentroOperativo");
                 if (tempObject != null)
                 {
                     return (CentroOperativo)tempObject;
                 }
                 else
                 {
                     return null;
                 }
             }
         }

        /// Capacita Lavoro
        [PersistentAlias("RisorsaCapo.CentroOperativo.USLG"), DisplayName("Unità Standard Lavoro Giornaliera")]
        [Appearance("RisorsaCapo.CentroOperativo.USLG.Nascondi", Criteria = "USLG==0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public double USLG
        {
            get
            {
                var tempObject = EvaluateAlias("USLG");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("RisorsaCapo.CentroOperativo.USLS"),
        DisplayName("Unità Standard Lavoro Settimanale")]
        [Appearance("RisorsaCapo.CentroOperativo.USLS.Nascondi", Criteria = "USLS==0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public double USLS
        {
            get
            {
                var tempObject = EvaluateAlias("USLS");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// </summary>
        private double fMinCapacita;
        [Persistent("MIN"),
        DisplayName("Min")]
        [Appearance("TeamRisore.MinCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public double MinCapacita
        {
            get
            {
                return fMinCapacita;
            }
            set
            {
                SetPropertyValue<double>("MinCapacita", ref fMinCapacita, value);
            }
        }

        private double fMaxCapacita;
        [Persistent("MAX"),
        DisplayName("Max")]
        [Appearance("TeamRisore.MaxCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public double MaxCapacita
        {
            get
            {
                return fMaxCapacita;
            }
            set
            {
                SetPropertyValue<double>("MaxCapacita", ref fMaxCapacita, value);
            }
        }

        private double fMediaCapacita;
        [Persistent("MEDIA"),
        DisplayName("Media")]
        [Appearance("TeamRisore.MediaCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public double MediaCapacita
        {
            get
            {
                return fMediaCapacita;
            }
            set
            {
                SetPropertyValue<double>("MediaCapacita", ref fMediaCapacita, value);
            }
        }

        private double fModaCapacita;
        [Persistent("MODA"),
        DisplayName("Moda")]
        [Appearance("TeamRisore.ModaCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public double ModaCapacita
        {
            get
            {
                return fModaCapacita;
            }
            set
            {
                SetPropertyValue<double>("ModaCapacita", ref fModaCapacita, value);
            }
        }

        private double fMedianaCapacita;
        [Persistent("MEDIANA"),
        DisplayName("Mediana")]
        [Appearance("TeamRisore.MedianaCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public double MedianaCapacita
        {
            get
            {
                return fMedianaCapacita;
            }
            set
            {
                SetPropertyValue<double>("MedianaCapacita", ref fMedianaCapacita, value);
            }
        }


        private XPCollection<RegistroPosizioniDettVista> fListaPosizioniRisorses;
        [PersistentAlias("fListaPosizioniRisorses"),
        DisplayName("Storico Posizioni")]
        [Appearance("RisorseTeam.ListaPosizioniRisorses.Visible", Criteria = "(Oid = -1 Or RisorsaCapo  is null Or fListaPosizioniRisorses.Count() = 0)", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegistroPosizioniDettVista> ListaPosizioniRisorses
        {
            get
            {
                if (fListaPosizioniRisorses == null && RisorsaCapo != null)
                {
                    fListaPosizioniRisorses = new XPCollection<RegistroPosizioniDettVista>(Session);
                    ;
                    RefreshPosizioniRisorsess();
                }
                return fListaPosizioniRisorses;
            }
        }


        private void RefreshPosizioniRisorsess()
        {
            if (fListaPosizioniRisorses == null)
            {
                return;
            }
            if (RisorsaCapo  == null)
            {
                return;
            } ///[Data Ora] Between(#{[Anno]}-01-01#, #{[Anno]}-12-31#)

            var ParCriteria = string.Format("Risorsa.Oid = {0} And DataOra Between(#{1}-01-01#, #{1}-12-31#)",
                                          RisorsaCapo.Oid.ToString(),  Anno.ToString());
            fListaPosizioniRisorses.Criteria = CriteriaOperator.Parse(ParCriteria);
            OnChanged("ListaPosizioniRisorses");
        }

        //[Association(@"CLUSTEREDIFICITEAMRISORSE_RdL", typeof(RdL)),
        //DisplayName("Elenco RdL Assegnate")]
        //[Appearance("RisirseTeam.RdL", Enabled = false)]
        //public XPCollection<RdL> RdLs
        //{
        //    get
        //    {
        //        return GetCollection<RdL>("RdLs");
        //    }
        //}

        //[Association(@"CLUSTEREDIFICITEAMRISORSE_RdL", typeof(AssRisorseTeam)),
        //DisplayName("Elenco Risorse Collegate")]
        //[Appearance("RisirseTeam.AssRisorseTeam", Enabled = false)]
        //public XPCollection<AssRisorseTeam> AssRisorseTeam
        //{
        //    get
        //    {
        //        return GetCollection<AssRisorseTeam>("AssRisorseTeam");
        //    }
        //}

    }
}
