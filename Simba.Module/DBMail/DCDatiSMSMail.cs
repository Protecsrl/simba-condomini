//using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;

//namespace CAMS.Module.DBMail
//{
//    class DCDatiSMSMail
//    {
//    }
//}
namespace CAMS.Module.DBMail
{
    [DomainComponent]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Mail SMS")]
//    [Appearance("DCDatiSMSMail.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
//   Context = "DCRisorseTeamRdL_LookupListView", Criteria = "Conduttore", BackColor = "Yellow", FontColor = "Black")]
//    [Appearance("DCDatiSMSMail.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
//Context = "DCRisorseTeamRdL_LookupListView", Criteria = "Ordinamento = 1", BackColor = "PaleGreen", FontColor = "Black")]

    public class DCDatiSMSMail
    {

        public Guid Oid { get; set; }

        [XafDisplayName("Tipo Azione Mail")]
        [Index(1)]
        public string TipoAzioneMail { get; set; }

        [XafDisplayName("Indirizzo Mail")]  //Azienda
        [Index(2)]
        public string IndirizzoMail { get; set; }

        [XafDisplayName("Nome Cognome")]  //Azienda  O_SUBJECT
        [Index(16)]
        public string NomeCognome { get; set; }

        [XafDisplayName("Soggetto")]  //Azienda  O_SUBJECT
        [Index(17)]
        public string Subject { get; set; }

        [XafDisplayName("Corpo")]  //Azienda  O_SUBJECT
        [Index(18)]
        public string Body { get; set; }

        [XafDisplayName("Indirizzo SMS")]  //Azienda  O_SUBJECT
        [Index(19)]
        public string IndirizzoSMS { get; set; }

        [XafDisplayName("Azione Spedizione")]  //Azienda  O_SUBJECT
        [Index(20)]
        public int  AzioneSpedizione { get; set; }

        [XafDisplayName("File Attachment")]  //Azienda  O_SUBJECT
        [Index(21)]
        public string FileAttachment { get; set; }

    }


}


 