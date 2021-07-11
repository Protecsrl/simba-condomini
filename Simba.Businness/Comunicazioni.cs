using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;

namespace Simba.Businness
{
    public class Comunicazioni : BusinnessBase
    {
        public List<Communications> GetUserCommunication(int? userId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Communications>().
                Where(c => !userId.HasValue || c.User.Oid == userId.Value).
                ToList();
                return data;
            }
        }

        public Simba.DataLayer.Database.Communications getComunicazioneById(int idCom)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Communications>().
                Where(c => c.Oid == idCom).First();
                return data;
            }
        }

        public List<Simba.DataLayer.Database.Communications> getComunicazioniCondominio(int idCom)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Communications>().
                Where(c => c.Condominium.Oid == idCom).ToList();
                return data;
            }
        }


        public List<Simba.DataLayer.Database.Communications> getComunicazioniSupplier()
        {
            return new List<Communications>();
        }

        public void SaveComunicazione(AddComunicazione obj)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {

                var user = uw.GetObjectByKey<Simba.DataLayer.Database.User>(User.GetUserId());
                var condominio = user.Building.Condominium;

                Simba.DataLayer.Database.Communications communuic = new Simba.DataLayer.Database.Communications(uw)
                {
                    Descrizione = obj.Descrizione,
                    Condominium = condominio,
                    Titolo = obj.Titolo,
                    DateInsert = DateTime.Now,
                    Note = obj.Note,
                    Number = obj.Number,
                    User = user,
                    ParentCommunication = 0,
                    Code = obj.Codice
                };

                communuic.Save();
                uw.CommitChanges();

                var commNew = uw.GetObjectByKey<Simba.DataLayer.Database.Communications>(communuic.Oid);
                string codice = string.Concat(commNew.Condominium.Oid.ToString().PadLeft(6, '0'), commNew.Number.ToString().PadLeft(6, '0'));
                commNew.Code = codice;
                commNew.Save();
                uw.CommitChanges();
            }
        }

        public void UpdateComunicazione(AddComunicazione obj)
        {

            using (UnitOfWork uw = new UnitOfWork())
            {
                var commun = uw.GetObjectByKey<Simba.DataLayer.Database.Communications>(obj.Oid);
                var user = uw.GetObjectByKey<Simba.DataLayer.Database.User>(User.GetUserId());
                var condominio = user.Building.Condominium;

                commun.Descrizione = obj.Descrizione;
                commun.Condominium = condominio;
                commun.Titolo = obj.Titolo;
                commun.DateInsert = DateTime.Now;
                commun.Note = obj.Note;
                commun.Number = obj.Number;
                commun.Code = obj.Codice;

                commun.Save();
                uw.CommitChanges();
            }
        }
    }
}
