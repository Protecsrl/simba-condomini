﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Simba.DataLayer.simba_condomini
{

    public partial class Building : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>(nameof(Id), ref fId, value); }
        }
        string fNome;
        [Size(50)]
        public string Nome
        {
            get { return fNome; }
            set { SetPropertyValue<string>(nameof(Nome), ref fNome, value); }
        }
        Condominium fCondominium;
        [Association(@"BuildingReferencesCondominium")]
        public Condominium Condominium
        {
            get { return fCondominium; }
            set { SetPropertyValue<Condominium>(nameof(Condominium), ref fCondominium, value); }
        }
        [Association(@"EnvironmentReferencesBuilding")]
        public XPCollection<Environment> Environments { get { return GetCollection<Environment>(nameof(Environments)); } }
        [Association(@"UserReferencesBuilding")]
        public XPCollection<User> Users { get { return GetCollection<User>(nameof(Users)); } }
    }

}
