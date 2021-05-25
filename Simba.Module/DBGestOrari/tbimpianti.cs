using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBGestOrari
{
        [DefaultClassOptions, Persistent("TBIMPIANTI")]
        [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Impianti")]
        [ImageName("Action_Inline_Edit")]
        [NavigationItem("Gestione Orari")]
    public class tbimpianti : XPObject
    {


        public tbimpianti() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbimpianti(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        int fidimpappic;
        //[Key]
        public int idimpappic
        {
            get { return fidimpappic; }
            set { SetPropertyValue<int>(nameof(idimpappic), ref fidimpappic, value); }
        }
        int fid;
        public int id
        {
            get { return fid; }
            set { SetPropertyValue<int>(nameof(id), ref fid, value); }
        }
        string fcodimp;
        [Size(255)]
        public string codimp
        {
            get { return fcodimp; }
            set { SetPropertyValue<string>(nameof(codimp), ref fcodimp, value); }
        }
        string fidfiliale;
        [Size(50)]
        public string idfiliale
        {
            get { return fidfiliale; }
            set { SetPropertyValue<string>(nameof(idfiliale), ref fidfiliale, value); }
        }
        int fidcontratto;
        public int idcontratto
        {
            get { return fidcontratto; }
            set { SetPropertyValue<int>(nameof(idcontratto), ref fidcontratto, value); }
        }
        string fcitta;
        [Size(255)]
        public string citta
        {
            get { return fcitta; }
            set { SetPropertyValue<string>(nameof(citta), ref fcitta, value); }
        }
        string findirizzo;
        [Size(255)]
        public string indirizzo
        {
            get { return findirizzo; }
            set { SetPropertyValue<string>(nameof(indirizzo), ref findirizzo, value); }
        }
        string fstato;
        [Indexed(@"stagione;idimpappic;idold", Name = @"IX_tbimpianti")]
        [Size(3)]
        public string stato
        {
            get { return fstato; }
            set { SetPropertyValue<string>(nameof(stato), ref fstato, value); }
        }
        string fstagione;
        [Size(255)]
        public string stagione
        {
            get { return fstagione; }
            set { SetPropertyValue<string>(nameof(stagione), ref fstagione, value); }
        }
        int fidold;
        public int idold
        {
            get { return fidold; }
            set { SetPropertyValue<int>(nameof(idold), ref fidold, value); }
        }
        string fidimpiantoSap;
        [Size(50)]
        public string idimpiantoSap
        {
            get { return fidimpiantoSap; }
            set { SetPropertyValue<string>(nameof(idimpiantoSap), ref fidimpiantoSap, value); }
        }
    }

}