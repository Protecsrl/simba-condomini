using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBGestOrari
{
    public class tbprofili : XPObject
    {
        public tbprofili() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbprofili(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        string fkeyAppic;
        [Size(300)]
        public string keyAppic
        {
            get { return fkeyAppic; }
            set { SetPropertyValue<string>(nameof(keyAppic), ref fkeyAppic, value); }
        }

        string fstagione;
        [Indexed(@"idcommessa", Name = @"NonClusteredIndex-20200318-192555")]
        [Size(255)]
        public string stagione
        {
            get { return fstagione; }
            set { SetPropertyValue<string>(nameof(stagione), ref fstagione, value); }
        }
        string fidfiliale;
        [Size(50)]
        public string idfiliale
        {
            get { return fidfiliale; }
            set { SetPropertyValue<string>(nameof(idfiliale), ref fidfiliale, value); }
        }
        int fidcommessa;
        public int idcommessa
        {
            get { return fidcommessa; }
            set { SetPropertyValue<int>(nameof(idcommessa), ref fidcommessa, value); }
        }
        int fmaxfasce;
        public int maxfasce
        {
            get { return fmaxfasce; }
            set { SetPropertyValue<int>(nameof(maxfasce), ref fmaxfasce, value); }
        }
        string ff1start;
        [Size(5)]
        public string f1start
        {
            get { return ff1start; }
            set { SetPropertyValue<string>(nameof(f1start), ref ff1start, value); }
        }
        string ff1end;
        [Size(5)]
        public string f1end
        {
            get { return ff1end; }
            set { SetPropertyValue<string>(nameof(f1end), ref ff1end, value); }
        }
        int fmaxore1;
        public int maxore1
        {
            get { return fmaxore1; }
            set { SetPropertyValue<int>(nameof(maxore1), ref fmaxore1, value); }
        }
        string ff2start;
        [Size(5)]
        public string f2start
        {
            get { return ff2start; }
            set { SetPropertyValue<string>(nameof(f2start), ref ff2start, value); }
        }
        string ff2end;
        [Size(5)]
        public string f2end
        {
            get { return ff2end; }
            set { SetPropertyValue<string>(nameof(f2end), ref ff2end, value); }
        }
        int fmaxore2;
        public int maxore2
        {
            get { return fmaxore2; }
            set { SetPropertyValue<int>(nameof(maxore2), ref fmaxore2, value); }
        }
        string ff3start;
        [Size(5)]
        public string f3start
        {
            get { return ff3start; }
            set { SetPropertyValue<string>(nameof(f3start), ref ff3start, value); }
        }
        string ff3end;
        [Size(5)]
        public string f3end
        {
            get { return ff3end; }
            set { SetPropertyValue<string>(nameof(f3end), ref ff3end, value); }
        }
        int fmaxore3;
        public int maxore3
        {
            get { return fmaxore3; }
            set { SetPropertyValue<int>(nameof(maxore3), ref fmaxore3, value); }
        }
        string ff4start;
        [Size(5)]
        public string f4start
        {
            get { return ff4start; }
            set { SetPropertyValue<string>(nameof(f4start), ref ff4start, value); }
        }
        string ff4end;
        [Size(5)]
        public string f4end
        {
            get { return ff4end; }
            set { SetPropertyValue<string>(nameof(f4end), ref ff4end, value); }
        }
        int fmaxore4;
        public int maxore4
        {
            get { return fmaxore4; }
            set { SetPropertyValue<int>(nameof(maxore4), ref fmaxore4, value); }
        }
        string ff5start;
        [Size(5)]
        public string f5start
        {
            get { return ff5start; }
            set { SetPropertyValue<string>(nameof(f5start), ref ff5start, value); }
        }
        string ff5end;
        [Size(5)]
        public string f5end
        {
            get { return ff5end; }
            set { SetPropertyValue<string>(nameof(f5end), ref ff5end, value); }
        }
        int fmaxore5;
        public int maxore5
        {
            get { return fmaxore5; }
            set { SetPropertyValue<int>(nameof(maxore5), ref fmaxore5, value); }
        }
        string ff6start;
        [Size(5)]
        public string f6start
        {
            get { return ff6start; }
            set { SetPropertyValue<string>(nameof(f6start), ref ff6start, value); }
        }
        string ff6end;
        [Size(5)]
        public string f6end
        {
            get { return ff6end; }
            set { SetPropertyValue<string>(nameof(f6end), ref ff6end, value); }
        }
        int fmaxore6;
        public int maxore6
        {
            get { return fmaxore6; }
            set { SetPropertyValue<int>(nameof(maxore6), ref fmaxore6, value); }
        }
        //public struct CompoundKey1Struct
        //{
        //    [Persistent("idutente")]
        //    public int idutente { get; set; }
        //    [Persistent("Circuito")]
        //    public int Circuito { get; set; }
        //}
        //[Key, Persistent]
        //public CompoundKey1Struct CompoundKey1;

    }

}