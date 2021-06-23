using System;
using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.simba_condomini;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Ticket : BusinnessBase
    {
        public Simba.DataLayer.simba_condomini.Ticket getTicketById(int idTicket)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.Ticket>().
                Where(c => c.Oid == idTicket).First();
                return data;
            }
        }
        public List<Simba.DataLayer.simba_condomini.Ticket> GetUserTicket(int? userId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.Ticket>().
                Where(c => !userId.HasValue || c.User.Oid == userId.Value).
                ToList();
                return data;
            }
        }

        public List<Simba.DataLayer.simba_condomini.TicketStatuses> GetTicketStatuses(int idTicket)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.TicketStatuses>().
                Where(c => c.IdTicket.Oid == idTicket).
                ToList();
                return data;
            }
        }

        public List<Simba.DataLayer.simba_condomini.TicketClassification> GetLastTicketClassification(int idTicket)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.TicketClassifications>().
                Where(c => c.IdTicket.Oid == idTicket).
                Select(c => c.IdClassification).
                ToList();
                return data;
            }
        }

        public void SaveTicket(AddTicket obj)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(User.GetUserId());

                var condominio = uw.GetObjectByKey<DataLayer.simba_condomini.Condominium>(obj.Condominio);
                var edificio = uw.GetObjectByKey<DataLayer.simba_condomini.Building>(obj.Edificio);
                var locale = uw.GetObjectByKey<DataLayer.simba_condomini.Environment>(obj.Locale);

                DataLayer.simba_condomini.Ticket ticket = new DataLayer.simba_condomini.Ticket(uw)
                {
                    Descrizione = obj.Descrizione,
                    Building = edificio,
                    Condominium = condominio,
                    Enviroment = locale,
                    Titolo = obj.Titolo,
                    TicketStatus = 1,
                    Data = DateTime.Now,
                    Note = obj.Note,
                    Number = obj.Number,
                    DateCreation = DateTime.Now,
                    User = user,
                    Code = obj.Codice
                };
                var classification = uw.GetObjectByKey<DataLayer.simba_condomini.TicketClassification>(obj.ClasseTicket);
                ticket.classification = classification.Oid;
                ticket.Save();
                uw.CommitChanges();

                DataLayer.simba_condomini.TicketClassifications classeTicket = new DataLayer.simba_condomini.TicketClassifications(uw)
                {
                    IdTicket = ticket,
                    IdClassification = classification
                };
                classeTicket.Save();
                uw.CommitChanges();
            }
        }

        public void UpdateTicket(AddTicket obj)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(User.GetUserId());
                var ticket = uw.GetObjectByKey<DataLayer.simba_condomini.Ticket>(obj.Oid);
                var condominio = uw.GetObjectByKey<DataLayer.simba_condomini.Condominium>(obj.Condominio);
                var edificio = uw.GetObjectByKey<DataLayer.simba_condomini.Building>(obj.Edificio);
                var locale = uw.GetObjectByKey<DataLayer.simba_condomini.Environment>(obj.Locale);
                var stato = uw.GetObjectByKey<DataLayer.simba_condomini.TicketStatus>(obj.Stato);
                ticket.Data = DateTime.Now;
                ticket.DateUpdate = DateTime.Now;
                ticket.Note = obj.Note;
                ticket.Number = obj.Number;
                ticket.Descrizione = obj.Descrizione;
                ticket.Titolo = obj.Titolo;
                ticket.Condominium = condominio;
                ticket.Building = edificio;
                ticket.Enviroment = locale;
                ticket.Code = obj.Codice;

                if (obj.ClasseTicket != ticket.classification)
                {
                    ticket.classification = obj.ClasseTicket;
                    var classification = uw.GetObjectByKey<DataLayer.simba_condomini.TicketClassification>(obj.ClasseTicket);

                    DataLayer.simba_condomini.TicketClassifications classeTicket = new DataLayer.simba_condomini.TicketClassifications(uw)
                    {
                        IdTicket = ticket,
                        IdClassification = classification
                    };
                    classeTicket.Save();

                }
                if (obj.Stato != ticket.TicketStatus)
                {

                    DataLayer.simba_condomini.TicketStatuses statoTicket = new DataLayer.simba_condomini.TicketStatuses(uw)
                    {
                        IdTicket = ticket,
                        IdStatus = stato,
                        Data = DateTime.Now,
                        IdUser = user
                    };
                    statoTicket.Save();
                }
                ticket.Save();
                uw.CommitChanges();
            }
        }
    }
}
