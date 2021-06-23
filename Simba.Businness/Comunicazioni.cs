using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.simba_condomini;
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
                Where(c=>!userId.HasValue || c.User.Oid == userId.Value).
                ToList();
                return data;
            }
        }

        public Simba.DataLayer.simba_condomini.Communications getComunicazioneById(int idCom)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.Communications>().
                Where(c => c.Oid == idCom).First();
                return data;
            }
        }

        public void SaveComunicazione(AddComunicazione obj)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(User.GetUserId());
                var condominio = user.Building.Condominium;

                DataLayer.simba_condomini.Communications communuic = new DataLayer.simba_condomini.Communications(uw)
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
            }
        }

        public void UpdateComunicazione(AddComunicazione obj)
        {

            using (UnitOfWork uw = new UnitOfWork())
            {
                var commun = uw.GetObjectByKey<DataLayer.simba_condomini.Communications>(obj.Oid);
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(User.GetUserId());
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
