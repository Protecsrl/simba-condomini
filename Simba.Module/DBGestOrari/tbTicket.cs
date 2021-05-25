using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBGestOrari
{
    [DefaultClassOptions, Persistent("TBTICKET")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ticket")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]

    public class tbTicket : XPObject
    {
        public tbTicket() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbTicket(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        int fid;
        //[Key]
        public int id
        {
            get { return fid; }
            set { SetPropertyValue<int>(nameof(id), ref fid, value); }
        }
        int fidutente;
        [Indexed(Name = @"idx_utente")]
        public int idutente
        {
            get { return fidutente; }
            set { SetPropertyValue<int>(nameof(idutente), ref fidutente, value); }
        }
        DateTime fdataora;
        public DateTime dataora
        {
            get { return fdataora; }
            set { SetPropertyValue<DateTime>(nameof(dataora), ref fdataora, value); }
        }
        string fstato;
        [Indexed(Name = @"idx_stato")]
        [Size(3)]
        public string stato
        {
            get { return fstato; }
            set { SetPropertyValue<string>(nameof(stato), ref fstato, value); }
        }
        int fCircuito;
        public int Circuito
        {
            get { return fCircuito; }
            set { SetPropertyValue<int>(nameof(Circuito), ref fCircuito, value); }
        }
        string fdescrizione;
        [Size(SizeAttribute.Unlimited)]
        public string descrizione
        {
            get { return fdescrizione; }
            set { SetPropertyValue<string>(nameof(descrizione), ref fdescrizione, value); }
        }
        string ftipo;
        [Size(50)]
        public string tipo
        {
            get { return ftipo; }
            set { SetPropertyValue<string>(nameof(tipo), ref ftipo, value); }
        }
        DateTime fdatachiusura;
        [Indexed(Name = @"idx_datac")]
        public DateTime datachiusura
        {
            get { return fdatachiusura; }
            set { SetPropertyValue<DateTime>(nameof(datachiusura), ref fdatachiusura, value); }
        }
        string fstagione;
        [Size(255)]
        public string stagione
        {
            get { return fstagione; }
            set { SetPropertyValue<string>(nameof(stagione), ref fstagione, value); }
        }
        int fflag_ac;
        public int flag_ac
        {
            get { return fflag_ac; }
            set { SetPropertyValue<int>(nameof(flag_ac), ref fflag_ac, value); }
        }

        string fdescrizioneope;
        [Size(SizeAttribute.Unlimited)]
        public string descrizioneope
        {
            get { return fdescrizioneope; }
            set { SetPropertyValue<string>(nameof(descrizioneope), ref fdescrizioneope, value); }
        }

    }

}