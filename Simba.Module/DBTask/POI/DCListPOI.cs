using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace CAMS.Module.DBTask.POI
//{
//    class DCListPOI
//    {
//    }
//}


using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System.ComponentModel;
using DevExpress.ExpressApp.DC;

namespace CAMS.Module.DBTask.POI
{
    [DomainComponent]
    [DefaultClassOptions]
    [System.ComponentModel.DisplayName("Programma Operativo Interventi")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Programma Operativo Interventi")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]

    public class DCListPOI
    {
        //public DCListPOI() : base() { }
        //public DCListPOI(Session session) : base(session) { }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        [Browsable(false)]  // Hide the entity identifier from UI.
        [DevExpress.ExpressApp.Data.Key]
        public int ID { get; set; }

        [XafDisplayName("Descrizione")]
        public string Descrizione { get; set; }

        [XafDisplayName("Immobile")]
        public string Immobile { get; set; }

        [XafDisplayName("Piano")]
        public string Piano { get; set; }

        [XafDisplayName("Stanza")]
        public string Stanza { get; set; }

        [XafDisplayName("Reparto")]
        public string Reparto { get; set; }

        [XafDisplayName("Impianto")]
        public string Impianto { get; set; }

        [XafDisplayName("Apparato")]
        public string Apparato { get; set; }

        [XafDisplayName("CodApparato")]
        public string CodApparato { get; set; }

        [XafDisplayName("TipoApparato")]
        public string TipoApparato { get; set; }

        [XafDisplayName("Categoria")]
        public string Categoria { get; set; }

        [XafDisplayName("CodProcedura")]
        public string CodProcedura { get; set; }

        [XafDisplayName("Frequenza")]
        public string Frequenza { get; set; }

        [XafDisplayName("Gennaio")]
        public string Gennaio { get; set; }

        [XafDisplayName("Febbraio")]
        public string Febbraio { get; set; }

        [XafDisplayName("Marzo")]
        public string Marzo { get; set; }

        [XafDisplayName("Aprile")]
        public string Aprile { get; set; }

        [XafDisplayName("Maggio")]
        public string Maggio { get; set; }

        [XafDisplayName("Giugno")]
        public string Giugno { get; set; }

        [XafDisplayName("Luglio")]
        public string Luglio { get; set; }

        [XafDisplayName("Agosto")]
        public string Agosto { get; set; }

        [XafDisplayName("Settembre")]
        public string Settembre { get; set; }

        [XafDisplayName("Ottobre")]
        public string Ottobre { get; set; }

        [XafDisplayName("Novembre")]
        public string Novembre { get; set; }

        [XafDisplayName("Dicembre")]
        public string Dicembre { get; set; }

        [XafDisplayName("DurataAttivita")]
        public string DurataAttivita { get; set; }

        [XafDisplayName("MaterialeUtilizzato")]
        public string MaterialeUtilizzato { get; set; }

        [XafDisplayName("Note")]
        public string Note { get; set; }

        [XafDisplayName("Priorita")]
        public string Priorita { get; set; }

        [XafDisplayName("PrioritaIntervento")]
        public string PrioritaIntervento { get; set; }
        #region  settimana mese anno

        [XafDisplayName("Trimestre")]
        public string Trimestre { get; set; }

        [XafDisplayName("Anno")]
        public string Anno { get; set; }
        #endregion
        
    }

}


