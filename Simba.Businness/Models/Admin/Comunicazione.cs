using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness.Models.Admin
{
    public class Comunicazione
    {
        public Comunicazione(
            int oid, int number, string titolo, int parentCommunication,
            int user, int condominium, DateTime dateInsert, int type, string descrizione
            , string note, string vode)
        {
            this.Oid = oid;
            this.Note = note;
            this.Number = number;
            this.ParentCommunication = parentCommunication;
            this.Titolo = titolo;
            this.Type = type;
            this.User = user;
        }

        public int Oid
        {
            get; private set;
        }
        public int Number
        {
            get; private set;
        }
        public string Titolo
        {
            get; private set;
        }
        public int ParentCommunication
        {
            get; private set;
        }
        public int User
        {
            get; private set;
        }
        public int Condominium
        {
            get; private set;
        }
        public DateTime DateInsert
        {
            get; private set;
        }
        public int Type
        {
            get; private set;
        }
        public string Descrizione
        {
            get; private set;
        }
        public string Note
        {
            get; private set;
        }
        public string Code
        {
            get; private set;
        }
    }
}
