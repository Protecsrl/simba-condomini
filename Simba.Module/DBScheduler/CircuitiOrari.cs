using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;

//namespace CAMS.Module.DBScheduler
//{
//    class CircuitiOrari
//    {
//    }
//}

namespace CAMS.Module.DBScheduler
{
    [DefaultClassOptions]
    [Persistent("ORARICIRCUITI")]
    [NavigationItem("Agenda")]
    [DefaultProperty("Caption")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Circuiti Orari")]
    [ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]    // 

    #region filtro tampone
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "!IsNullOrEmpty(User)", "Attivi", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "IsNullOrEmpty(User)", "non Attivi", Index = 1)]

    #endregion


    public class CircuitiOrari : XPObject, IResource //TruckMaster
    {
        public CircuitiOrari(Session session)
            : base(session)
        {
        }

#if MediumTrust
		[Persistent("Color")]
		[Browsable(false)]
		public Int32 color;
#else
        [Persistent("Color")]
        private Int32 color;
#endif

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            color = Color.White.ToArgb();
        }


        //[MemberDesignTimeVisibility(false)] 
        [Persistent("IMMOBILE")]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }

        #region IResource Members
        private string caption;
        [Persistent("CAPTION")]
        [XafDisplayName("Denominazione")]
        public string Caption
        {
            get { return caption; }
            set { SetPropertyValue("Caption", ref caption, value); }
        }

        [NonPersistent]
        [Browsable(false)]
        public object Id { get { return Oid; } }

        [NonPersistent]
        public Color Color
        {
            get { return Color.FromArgb(color); }
            set { SetPropertyValue("color", ref color, value.ToArgb()); }
        }

        [NonPersistent, Browsable(false)]
        public Int32 OleColor
        {
            get { return ColorTranslator.ToOle(Color.FromArgb(color)); }
        }
         
        #endregion
        [Association("OrariAccensioni-CircuitiOrari")]
        [XafDisplayName("Agenda Orari")]
        public XPCollection<OrariAccensioni> OrariAccensioniS
        {
            get { return GetCollection<OrariAccensioni>("OrariAccensioniS"); }
        }

    }
}


//#region Copyright (c) 2000-2017 Developer Express Inc.
///*
//{*******************************************************************}
//{                                                                   }
//{       Developer Express .NET Component Library                    }
//{       eXpressApp Framework                                        }
//{                                                                   }
//{       Copyright (c) 2000-2017 Developer Express Inc.              }
//{       ALL RIGHTS RESERVED                                         }
//{                                                                   }
//{   The entire contents of this file is protected by U.S. and       }
//{   International Copyright Laws. Unauthorized reproduction,        }
//{   reverse-engineering, and distribution of all or any portion of  }
//{   the code contained in this file is strictly prohibited and may  }
//{   result in severe civil and criminal penalties and will be       }
//{   prosecuted to the maximum extent possible under the law.        }
//{                                                                   }
//{   RESTRICTIONS                                                    }
//{                                                                   }
//{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
//{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
//{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
//{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
//{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
//{                                                                   }
//{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
//{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
//{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
//{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
//{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
//{                                                                   }
//{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
//{   ADDITIONAL RESTRICTIONS.                                        }
//{                                                                   }
//{*******************************************************************}
//*/
//#endregion Copyright (c) 2000-2017 Developer Express Inc.

//using System;
//using System.ComponentModel;
//using System.Drawing;
//using DevExpress.Persistent.Base.General;
//using DevExpress.Xpo;
//namespace DevExpress.Persistent.BaseImpl {
//    [DefaultProperty("Caption")]
//    public class Resource : BaseObject, IResource {
//#if MediumTrust
//        [Persistent("Color")]
//        [Browsable(false)]
//        public Int32 color;
//#else
//        [Persistent("Color")]
//        private Int32 color;
//#endif
//        private string caption;
//        public Resource(Session session) : base(session) { }
//        public override void AfterConstruction() {
//            base.AfterConstruction();
//            color = Color.White.ToArgb();
//        }
//        [NonPersistent, Browsable(false)]
//        public object Id {
//            get { return Oid; }
//        }
//        public string Caption {
//            get { return caption; }
//            set { SetPropertyValue("Caption", ref caption, value); }
//        }
//        [NonPersistent, Browsable(false)]
//        public Int32 OleColor {
//            get { return ColorTranslator.ToOle(Color.FromArgb(color)); }
//        }
//        [Association("Event-Resource")]
//        public XPCollection<Event> Events {
//            get { return GetCollection<Event>("Events"); }
//        }
//        [NonPersistent]
//        public Color Color {
//            get { return Color.FromArgb(color); }
//            set { SetPropertyValue("color", ref color, value.ToArgb()); }
//        }
//    }
//}
