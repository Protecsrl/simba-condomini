using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System;
//using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;

namespace CAMS.Module.DBTask.DC
{
    // [DevExpress.ExpressApp.DC.DomainComponent] https://www.devexpress.com/Support/Center/Question/Details/T403748, Enabled = false
    [DomainComponent]
    [NavigationItem(false)]

    [Appearance("DCRisorseTeamRdL.Ordinamento_3.evidenzia.Lime", AppearanceItemType = "ViewItem", TargetItems = "*",
Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = 3", FontStyle = FontStyle.Bold, BackColor = "Lime", FontColor = "Black")]

    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Risorse Team x RdL")]
    [Appearance("DCRisorseTeamRdL.Conduttore.evidenzia.Yellow", AppearanceItemType = "ViewItem", TargetItems = "*",
   Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = 2", BackColor = "Yellow", FontColor = "Black")]

    [Appearance("DCRisorseTeamRdL.Ordinamento.evidenzia.PaleGreen", AppearanceItemType = "ViewItem", TargetItems = "*",
Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = 1", BackColor = "PaleGreen", FontColor = "Black")]

    [Appearance("DCRisorseTeamRdL.Ordinamento.evidenzia.Salmon", AppearanceItemType = "ViewItem", TargetItems = "*",
Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = -1", BackColor = "Salmon", FontColor = "Black")]
    public class DCRisorseTeamRdL 
    {
        // [DevExpress.ExpressApp.Data.Key]  quando poassa a 16.1 @@@@@##############################àà
        // chiave        // prove
        //public  int Oid { get; set; }
        //public string x { get; set; }
        //public string y { get; set; }
        // ------------------------------
             [DevExpress.ExpressApp.Data.Key]
        public Guid Oid { get; set; }

        [XafDisplayName("Risorsa Capo Squadra")]
        [Index(1)]
   
        public string RisorsaCapo { get; set; }

        [XafDisplayName("Telefono")]  //Azienda
        [Index(2)]
        public string Telefono { get; set; }

        [XafDisplayName("Azienda")]  //Azienda
        [Index(16)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string Azienda { get; set; }

        [XafDisplayName("Coppia Linkata")]
        [Index(4)]
        [VisibleInListView(false),VisibleInLookupListView(false)]
        public TipoNumeroManutentori CoppiaLinkata { get; set; }

        [XafDisplayName("Mansione")]
        [Index(3)]
        public string Mansione { get; set; }   

        [XafDisplayName("Centro Operativo")]
        [Index(5)]
        public string CentroOperativo { get; set; }

        [XafDisplayName("Ultimo stato operativo")]
        [Index(6)]
        public string UltimoStatoOperativo { get; set; }

        [XafDisplayName(@"Attività in Agenda")]
        [Index(7)]
        public int NumeroAttivitaAgenda { get; set; }

        [XafDisplayName(@"Attività Sospese")]
        [Index(8)]
        public int NumeroAttivitaSospese { get; set; }       

        [XafDisplayName(@"Attività in Emergenza")]
        [Index(9)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumeroAttivitaEmergenza { get; set; }

        [XafDisplayName("Posizione")]
        [EditorAlias("HyperLinkPropertyEditor")]
        [Index(15)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string Url { get; set; }

        [XafDisplayName("Reg.RdL in Lavorazione")]
        [Index(10)]
        public string RegistroRdL { get; set; }

        [XafDisplayName("Conduttore")]
        [Index(11)]
        public Boolean Conduttore { get; set; }

        [XafDisplayName(@"Attività Totali MP")]
        [Index(16)]
        public int NumerorAttivitaTotaliTT { get; set; }

        [XafDisplayName(@"Attività Totali TT")]
        [Index(17)]
        public int NumerorAttivitaTotaliMP { get; set; }

        #region calcolo distanze
        [XafDisplayName("Distanza Impianto"), Size(50)]
        [Index(12)]
        public string DistanzaImpianto { get; set; }

        #region calcolo distanze
        [XafDisplayName("Distanza km")]
        [Index(20)]
        public int Distanzakm { get; set; }

        [XafDisplayName("Ultimo Immobile Visitato"), Size(50)]
        [Index(13)]
        public string UltimoEdificio { get; set; }

        [XafDisplayName("Interventi su Immobile"), Size(50)]
        [Index(14)]
        public string InterventosuEdificio { get; set; }

        [XafDisplayName("Stato Connessione"), Size(250)]
        [Index(15)]
        public string Aggiornamento { get; set; }

        #endregion

        #region indici classi e user
        [XafDisplayName("OID Centro Operativo")]
         [Browsable(false)]
        public int OidCentroOperativo { get; set; }

        [XafDisplayName("OID Immobile")]
        [Browsable(false)]
        public int OidEdificio { get; set; }

        [XafDisplayName("OID OidRisorsaTeam")]
        [Browsable(false)]
        public int OidRisorsaTeam { get; set; }

        [XafDisplayName("Ordinamento")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int Ordinamento { get; set; }

        [XafDisplayName("User")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string UserName { get; set; }
        #endregion
   
    }




}

#endregion