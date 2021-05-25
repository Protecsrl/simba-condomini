using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CAMS.Module.DBAngrafica
{
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("Parametri DB")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Parametri Generali DB")]
    [DefaultClassOptions, Persistent("PARAMETRIGENDB")]
    [ImageName("GroupFieldCollection")]
    [VisibleInDashboards(false)]
    public class ParametriGeneraliDB : XPObject
    {
        public ParametriGeneraliDB() : base() { }

        public ParametriGeneraliDB(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [DbType("varchar(100)")]
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

        private string fNomeParametro;
        [Size(100),
        Persistent("NOMEPARAMETRO"),
        DisplayName("Nome Parametro")]
        [DbType("varchar(100)")]
        public string NomeParametro
        {
            get
            {
                return fNomeParametro;
            }
            set
            {
                SetPropertyValue<string>("NomeParametro", ref fNomeParametro, value);
            }
        }

        private string fValore;
        [ Persistent("VALORE"), DisplayName("Valore")]
       // [DbType("varchar(250)")]
        public string Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<string>("Valore", ref fValore, value);
            }
        }

        [Persistent("FILE"),DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File");
               // return fFile;
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value);
                //SetPropertyValue<FileData>("File", ref fFile, value);
            }
        }


    }
}
