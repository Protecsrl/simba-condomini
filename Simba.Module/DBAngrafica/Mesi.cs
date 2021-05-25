using DevExpress.Persistent.Base;
using DevExpress.Xpo;


namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions, Persistent("MESI")]
    [ System.ComponentModel.DefaultProperty("Descrizione")]        
    [NavigationItem(false)]    
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mesi")]
    [ImageName("ShowTestReport")]
    public class Mesi : XPObject
    {
        public Mesi()
            : base()
        {
        }

        public Mesi(Session session)
            : base(session)
        {
        }

      
        private int fMese;
        [Persistent("MESI"),  DisplayName("Mese")]
        //DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0:D")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<int>("Mese", ref fMese, value);
            }
        }

        private string fDescrizione;
        [Size(25),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(25)")]
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
    

    }
}




