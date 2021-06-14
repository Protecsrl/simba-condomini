using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simba.Businness.Models
{
    public class Edificio
    {
        public Edificio(int id, string nome, int condominium)
        {
            this.Oid = id;
            this.Nome = nome;
            this.Condominium = condominium;
        }
        public int Oid
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