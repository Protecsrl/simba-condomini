using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBGestOrari
{
    [DefaultClassOptions, Persistent("TBAPPROVATOREGESTIONEORARIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Approvatore Gestione Orario")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]

    public class ApprovatoreGestioneOrario : XPObject
    {
        public ApprovatoreGestioneOrario() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public ApprovatoreGestioneOrario(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();           
        }

        int fid;       
        public int id
        {
            get { return fid; }
            set { SetPropertyValue<int>(nameof(id), ref fid, value); }
        }

     
    }

}