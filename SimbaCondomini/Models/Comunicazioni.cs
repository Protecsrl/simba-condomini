﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbaCondomini.Models
{
    public class Comunicazioni
    {
        public Comunicazioni(int oid, string testo, int parentCommunication, string user, string condominium, DateTime dateInsert)
        {
            this.Oid = oid;
            this.Testo = testo;
            this.ParentCommunication = parentCommunication;
            this.User = user;
            this.Condominium = condominium;
            this.DateInsert = dateInsert;
        }

        public int Oid
        {
            get;
            private set;
        }
        public string Testo
        {
            get;
            private set;
        }
        public int ParentCommunication
        {
            get;
            private set;
        }
        public string User
        {
            get;
            private set;
        }
        public string Condominium
        {
            get;
            private set;
        }
        public DateTime DateInsert
        {
            get;
            private set;
        }
    }
}