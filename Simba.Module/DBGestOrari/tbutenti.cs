using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBGestOrari
{
    [DefaultClassOptions, Persistent("TBUTENTI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Utenti")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]

    public class tbutenti : XPObject
    {
        public tbutenti() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbutenti(Session session) : base(session)
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
        string fnominativo;
        [Size(255)]
        public string nominativo
        {
            get { return fnominativo; }
            set { SetPropertyValue<string>(nameof(nominativo), ref fnominativo, value); }
        }
        string fusername;
        [Size(50)]
        public string username
        {
            get { return fusername; }
            set { SetPropertyValue<string>(nameof(username), ref fusername, value); }
        }
        string fpwd;
        [Size(50)]
        public string pwd
        {
            get { return fpwd; }
            set { SetPropertyValue<string>(nameof(pwd), ref fpwd, value); }
        }
        string femail;
        [Size(255)]
        public string email
        {
            get { return femail; }
            set { SetPropertyValue<string>(nameof(email), ref femail, value); }
        }
        string fente;
        [Size(255)]
        public string ente
        {
            get { return fente; }
            set { SetPropertyValue<string>(nameof(ente), ref fente, value); }
        }
        string fqualifica;
        [Size(255)]
        public string qualifica
        {
            get { return fqualifica; }
            set { SetPropertyValue<string>(nameof(qualifica), ref fqualifica, value); }
        }
        string fstato;
        [Size(3)]
        public string stato
        {
            get { return fstato; }
            set { SetPropertyValue<string>(nameof(stato), ref fstato, value); }
        }
        char ftipo;
        public char tipo
        {
            get { return ftipo; }
            set { SetPropertyValue<char>(nameof(tipo), ref ftipo, value); }
        }
        char fflagC;
        public char flagC
        {
            get { return fflagC; }
            set { SetPropertyValue<char>(nameof(flagC), ref fflagC, value); }
        }
        string fidfiliale;
        [Size(50)]
        public string idfiliale
        {
            get { return fidfiliale; }
            set { SetPropertyValue<string>(nameof(idfiliale), ref fidfiliale, value); }
        }
        char ff_nprog;
        public char f_nprog
        {
            get { return ff_nprog; }
            set { SetPropertyValue<char>(nameof(f_nprog), ref ff_nprog, value); }
        }
        char ff_mods;
        public char f_mods
        {
            get { return ff_mods; }
            set { SetPropertyValue<char>(nameof(f_mods), ref ff_mods, value); }
        }
        char ff_modg;
        public char f_modg
        {
            get { return ff_modg; }
            set { SetPropertyValue<char>(nameof(f_modg), ref ff_modg, value); }
        }
        char ff_modf;
        public char f_modf
        {
            get { return ff_modf; }
            set { SetPropertyValue<char>(nameof(f_modf), ref ff_modf, value); }
        }
        char ff_amm;
        public char f_amm
        {
            get { return ff_amm; }
            set { SetPropertyValue<char>(nameof(f_amm), ref ff_amm, value); }
        }

    }

}