using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBGestOrari
{

    [DefaultClassOptions, Persistent("TBCIRCUITI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Circuiti")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]
    [DefaultProperty("FullName")]
    public class tbcircuiti : XPObject
    {
        public tbcircuiti() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbcircuiti(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        int fidcircappic;
        //[Key]
        public int idcircappic
        {
            get { return fidcircappic; }
            set { SetPropertyValue<int>(nameof(idcircappic), ref fidcircappic, value); }
        }
        int fid;
        public int id
        {
            get { return fid; }
            set { SetPropertyValue<int>(nameof(id), ref fid, value); }
        }
        int fidimpianto;
        public int idimpianto
        {
            get { return fidimpianto; }
            set { SetPropertyValue<int>(nameof(idimpianto), ref fidimpianto, value); }
        }
        string fcircuito;
        [Size(255)]
        public string circuito
        {
            get { return fcircuito; }
            set { SetPropertyValue<string>(nameof(circuito), ref fcircuito, value); }
        }
        int fidimpappic;
        public int idimpappic
        {
            get { return fidimpappic; }
            set { SetPropertyValue<int>(nameof(idimpappic), ref fidimpappic, value); }
        }
        string fstato;
        [Size(3)]
        public string stato
        {
            get { return fstato; }
            set { SetPropertyValue<string>(nameof(stato), ref fstato, value); }
        }
        string fuso;
        [Size(255)]
        public string uso
        {
            get { return fuso; }
            set { SetPropertyValue<string>(nameof(uso), ref fuso, value); }
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
        string fnote;
        [Size(50)]
        public string note
        {
            get { return fnote; }
            set { SetPropertyValue<string>(nameof(note), ref fnote, value); }
        }
        int fflag_ac;
        public int flag_ac
        {
            get { return fflag_ac; }
            set { SetPropertyValue<int>(nameof(flag_ac), ref fflag_ac, value); }
        }
        string fCircuitoSap;
        [Size(50)]
        public string CircuitoSap
        {
            get { return fCircuitoSap; }
            set { SetPropertyValue<string>(nameof(CircuitoSap), ref fCircuitoSap, value); }
        }

        [PersistentAlias("Iif(circuito is not null And uso is not null,circuito + ', ' +  uso  + ', ' + stagione ,circuito)")]
        [DevExpress.Xpo.DisplayName("FullName")]
        [Browsable(false)]
        public string FullName
        {
            get
            {     
                var tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return tempObject.ToString();
                }
                return null;
            }
        }
        public override string ToString()
        {
            if (this.Oid == -1) return null;
            if (this.circuito == null) return null;

            if (circuito != null)
                return string.Format("{0}",   FullName);
            else
                return string.Format("{0}", FullName);
        }

    }

}