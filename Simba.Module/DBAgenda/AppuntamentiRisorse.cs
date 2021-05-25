
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;



namespace CAMS.Module.DBAgenda
{
    [DefaultClassOptions]
    [Persistent("NOTIFICA_SK_RISORSE")]
    [NavigationItem("Ticket")]
    [DefaultProperty("Caption")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Agenda Risorse")]
    [ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]    // 

    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "!IsNullOrEmpty(User)", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "IsNullOrEmpty(User)", "non Attivi", Index = 1)]

    #endregion


    public class AppuntamentiRisorse : XPObject, IResource //TruckMaster
    {
        public AppuntamentiRisorse(Session session)
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

        //[MemberDesignTimeVisibility(false)] //       [Delayed(true)]
        //[Persistent("RISORSATEAM")]
        //public RisorseTeam OidRisorseTeam
        //{
        //    get { return GetDelayedPropertyValue<RisorseTeam>("OidRisorseTeam"); }
        //    set { SetDelayedPropertyValue<RisorseTeam>("OidRisorseTeam", value); }
        //}

        [MemberDesignTimeVisibility(false)] //       [Delayed(true)]
        [Persistent("RISORSATEAM")]
        [Delayed(true)]
        public RisorseTeam RisorseTeam
        {
            get { return GetDelayedPropertyValue<RisorseTeam>("RisorseTeam"); }
            set { SetDelayedPropertyValue<RisorseTeam>("RisorseTeam", value); }
        }

        #region IResource Members
        private string _Caption;
        //[PersistentAlias("Iif(RisorseTeam is Not Null, [<RisorseTeam>][^.OidRisorseTeam == Oid].Single(RisorsaCapo.Nome + ' ' + RisorsaCapo.Cognome + '(' + Anno + ')'),'nd')")]
        [PersistentAlias("Iif(IsNull(RisorseTeam),'nd', RisorseTeam.RisorsaCapo.Nome + ' ' + RisorseTeam.RisorsaCapo.Cognome + '(' + RisorseTeam.Anno + ')')")]
        [XafDisplayName("Denominazione")]        //[VisibleInListView(false), VisibleInDetailView(false)]
        public string Caption
        {
            get
            {
                object tempObject = EvaluateAlias("Caption");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return "nd";
                }
            }
            //get { return _Caption; }
            set { SetPropertyValue("Caption", ref _Caption, value); }
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

        //private int _OleColor;    //[Persistent("COLOR")]
        //[PersistentAlias("Iif(RisorseTeam is not null, RisorseTeam.Color,0)")]
        //[Browsable(false)]
        //public int OleColor
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("OleColor");
        //        if (tempObject != null)
        //        {
        //            return (int) tempObject;
        //            //return (int)((Color)tempObject).ToArgb();
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }

        //    //get { return _OleColor; }
        //    //set { SetPropertyValue("OleColor", ref _OleColor, value); }
        //}

        //[Persistent("CENTROOPERATIVO")]
        //[PersistentAlias("Iif(RisorseTeam is not null, [<RisorseTeam>][^.OidRisorseTeam == Oid].Single(CentroOperativo),null)")]       //[VisibleInListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Iif(RisorseTeam is not null, RisorseTeam.CentroOperativo,null)")]       //[VisibleInListView(false), VisibleInDetailView(false)]
        [XafDisplayName("Centro Operativo")]
        public CentroOperativo CentroOperativo
        {
            get
            {
                object tempObject = EvaluateAlias("CentroOperativo");
                if (tempObject != null)
                {
                    return (CentroOperativo)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }





        //[PersistentAlias("RisorsaCapo.SecurityUser.UserName"), DisplayName("User")]
        //[PersistentAlias("Iif(Oid > 0, [<RisorseTeam>][^.OidRisorseTeam == Oid].Single(RisorsaCapo.SecurityUser.UserName),null)")]
        [PersistentAlias("Iif(RisorseTeam is not null, RisorseTeam.RisorsaCapo.SecurityUser.UserName,null)")]
        [XafDisplayName("User")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string User
        {
            get
            {
                object tempObject = EvaluateAlias("User");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
            }
        }



        #endregion


        [Association("NotificaRdL-Resource")]
        [XafDisplayName("Agenda Notifiche Interventi")]
        public XPCollection<NotificaRdL> NotificaRdLs
        {
            get { return GetCollection<NotificaRdL>("NotificaRdLs"); }
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
