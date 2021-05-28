﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbaCondomini.Models
{
    public class Edificio
    {
        public Edificio(int id, string nome, int condominium)
        {
            this.Id = id;
            this.Nome = nome;
            this.Condominium = condominium;
        }
        public int Id
        {
            get;
            private set;
        }
        public string Nome
        {
            get;
            private set;
        }
        public int Condominium
        {
            get;
            private set;
        }
    }
}