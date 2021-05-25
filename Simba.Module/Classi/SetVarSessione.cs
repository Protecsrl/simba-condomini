using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace CAMS.Module.Classi
{
    [NonPersistent]
    public class SetVarSessione : XPObject
    {
        public SetVarSessione()
            : base()
        {
        }

        public SetVarSessione(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        public static string OracleConnString;
        public static string ConfigConnString;
        public static string PhysicalPathSito;
        public static string PhysicalPathSitoCAD;
        public static string PhysicalPathSitoSegnalazioneFileLog;

        public static string CorrenteUser { get; set; }
        public static string CorrenteRuolo { get; set; }
        public static string CodSessioneWeb { get; set; }
        public static bool IsAdminRuolo { get; set; }
        public static bool IsMSSQLDB { get; set; }
        public static bool IsCCRuolo { get; set; }
        public static string PhysicalSubPathFileData{ get; set; }

        public static    int OidSel { get; set; }

        public static bool Esegui_RefreshDati = false;
        public static bool Esegui_DeSelezionaDati = false;
        public static string Esegui_DeSelezionaDatiTHISListView = string.Empty;
        public static int CountSave = 0;
        public static string MessaggioWeb { get; set; }
        public static int OidServizioDaAggiornare=0;
        public static int OidEdificioDaAggiornare=0;
        //public static int OidEdificioCalcoloDistanze=0;
        //public static double OidEdificioCalcoloDistanzeLatitudine=0;
        //public static double OidEdificioCalcoloDistanzeLongitudine=0;
        public static int OidMansioneGhost=0 ;
        public static int OidStandardAppararto = 0;

        public static string ApplicationWebURL;
        public static string ApplicationWebPath; ///SL3
        public static string ApplicationWebIconeURL;
        public static string WebPath;

        public static string WebCADPath;
        public static string WebSegnalazioneFileLogPath;
        public static string WebCADDocPath;

        public static string StringaTest;

        public static string RuoloXml;
        public static int OidIndirizzo;
        public static int OidEdificiosuIntervento = 0;
        //public static View CurrentView;
        public static int GiorniRitardoRicerca=0;

        /// visualizza  azioni tasti
        public static bool Visualizza_acCreaTRisorse = false;
        public static bool Visualizza_acCreaRegNotificheEmergenza = false;

        // ex variabili globali   public static class VarGlobali
        private const int INT_maxRangeBetween = 5;
        private const int INT_minRangeBetween = 1;


        public static DateTime dataAdessoDebug = DateTime.Now;

        private static string _CorrenteRoleApparence;
        public static string CorrenteRoleApparence
        {
            get { return _CorrenteRoleApparence; }
            set { _CorrenteRoleApparence = value; }
        }

        private static string _CorrenteRoleInserimento;
        public static string CorrenteRoleInserimento
        {
            get { return _CorrenteRoleInserimento; }
            set { _CorrenteRoleInserimento = value; }
        }

        private static int _contatore;
        public static int Contatore
        {
            get { return _contatore; }
            set { _contatore = value; }
        }

        private static int _maxRangeBetween = INT_maxRangeBetween;
        public static int MaxRangeBetween
        {
            get { return _maxRangeBetween; }
            set { _maxRangeBetween = value; }
        }

        private static int _minRangeBetween = INT_minRangeBetween;
        public static int MinRangeBetween
        {
            get { return _minRangeBetween; }
            set { _minRangeBetween = value; }
        }

        public static string HostName { get; set; }

        public static int ServerPort { get; set; }

        public static bool FlowChartAutorizzativo  { get; set; }

        public static bool VisualizzaSLA { get; set; }

        private static string _DetailViewNewTTPersonalizzata = "RdL_DetailView";
        public static string DetailViewNewTTPersonalizzata
        {
            get { return _DetailViewNewTTPersonalizzata; }
            set { _DetailViewNewTTPersonalizzata = value; }
        }
    }
}
