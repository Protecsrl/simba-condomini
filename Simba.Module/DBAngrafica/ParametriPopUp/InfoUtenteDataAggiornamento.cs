using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace CAMS.Module.DBAngrafica.ParametriPopUp
{  
    [DefaultClassOptions, NonPersistent]
    [NavigationItem(false), System.ComponentModel.DisplayName("Informazioni Ultima Data di Aggiornamento")]

    public class InfoUtenteDataAggiornamento : XPObject
    {
         public InfoUtenteDataAggiornamento()
            : base()
        {
        }
         public InfoUtenteDataAggiornamento(Session session)
            : base(session)
        {
        }

         private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

         private string fDescrizione;
         [NonPersistent, Size(250), DevExpress.Xpo.DisplayName("Descrizione"), Appearance("InfoUtenteDataAggiornamento.Descrizione", Enabled = false)]
         [VisibleInListView(false)]
         public string Descrizione
         {
             get
             {
                 return fDescrizione;
             }
             set
             {
                 SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
             }
         }

         private string fCodiciSistema;
         [NonPersistent, Size(250), DevExpress.Xpo.DisplayName("Codifica Sistema Oid/RegRdL/OdL")]
         [Appearance("InfoUtenteDataAggiornamento.fCodiciSistemi", Enabled = false)]
         [VisibleInListView(false)]
         public string CodiciSistema
         {
             get
             {
                 return fCodiciSistema;
             }
             set
             {
                 SetPropertyValue<string>("CodiciSistema", ref fCodiciSistema, value);
             }
         }

         #region utente e data aggiornamento
         private string f_Utente;
         [NonPersistent, Size(100), DevExpress.Xpo.DisplayName("Utente")]
      //  [Appearance("InfoUtenteDataAggiornamento.Utente", Enabled = false)]        
         //[VisibleInListView(false)]
         //[System.ComponentModel.Browsable(false)]
         public string MostraUtente
         {
             get
             {
                 return f_Utente;
             }
             set
             {
                 SetPropertyValue<string>("MostraUtente", ref f_Utente, value);
             }
         }

         private DateTime f_DataAggiornamento;
         [NonPersistent, DevExpress.Xpo.DisplayName("Data Aggiornamento")]
         [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
         [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
         [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information )]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
         //Appearance("InfoUtenteDataAggiornamento.DataAggiornamento", Enabled = false)],DevExpress.Persistent.Base.ToolTipIconType.Information
         //[VisibleInListView(false)]
         //[System.ComponentModel.Browsable(false)]
         public DateTime MostraDataAggiornamento
         {
             get
             {
                 return f_DataAggiornamento;
             }
             set
             {
                 SetPropertyValue<DateTime>("MostraDataAggiornamento", ref f_DataAggiornamento, value);
             }
         }



         #endregion

    

    }
}
