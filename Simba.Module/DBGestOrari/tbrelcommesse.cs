using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBGestOrari
{
    public class tbrelcommesse : XPObject
    {
        public tbrelcommesse() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbrelcommesse(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        string femail;
        [Size(255)]
        public string email
        {
            get { return femail; }
            set { SetPropertyValue<string>(nameof(email), ref femail, value); }
        }
        public struct CompoundKey1Struct
        {
            [Persistent("idutente")]
            public int idutente { get; set; }
            [Persistent("idcommessa")]
            public int idcommessa { get; set; }
            [Size(255)]
            [Persistent("stagione")]
            public string stagione { get; set; }
        }
        [Key, Persistent]
        public CompoundKey1Struct CompoundKey1;

    }

}