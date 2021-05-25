using DevExpress.Xpo;

namespace CAMS.Module.Classi
{
    [NonPersistent]
    public class SessionVariabile : XPBaseObject
    {
        private string fname;
        private string fSessioneID;
        private string fValore;
        public SessionVariabile(Session session) : base(session) { }



        public string SessioneID
        {
            get { return fSessioneID; }
            set { SetPropertyValue("SessioneID", ref fSessioneID, value); }
        }

        public string Name
        {
            get { return fname; }
            set { SetPropertyValue("Name", ref fname, value); }
        }

        public string Valore
        {
            get { return fValore; }
            set { SetPropertyValue("Valore", ref fValore, value); }
        }

        public void InitializeObject(int index)
        {
            //this.Index = index;
            //this.IntegerProperty = randomGenerator.Next();
            //this.Name = "AuditPerformanceObject" + index;
            //if (this.ObjectProperty == null)
            //{
            //    ReferencedAuditedObject referencedAuditedObject = new ReferencedAuditedObject(this.Session, string.Format("Referenced Audited Object referenced by {0}", this.Name));
            //    referencedAuditedObject.Name = "ReferencedAuditedObject " + index;
            //    this.ObjectProperty = referencedAuditedObject;
            //}
            //this.StringProperty = "Some string: " + randomGenerator.Next();
            //this.BooleanProperty = (index % 2 == 0);
            //this.DateTimeProperty = DateTime.Now.AddDays(index);
            //this.DecimalProperty = (decimal)(index * 1.5);
            //this.DoubleProperty = index * 3.14;
            //this.EnumProperty = this.BooleanProperty ? SampleEnum.First : ((index % 3 == 0) ? SampleEnum.Second : SampleEnum.Third);
            this.Save();
        }

    }
}
