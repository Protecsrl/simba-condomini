using System;
using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.Database;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Ticket : BusinnessBase
    {
        public Simba.DataLayer.Database.Ticket getTicketById(int idTicket)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Ticket>().
                Where(c => c.Oid == idTicket).First();
                return data;
            }
        }
        public List<Simba.DataLayer.Database.Ticket> GetUserTicket(int? userId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Ticket>().
                Where(c => !userId.HasValue || c.User.Oid == userId.Value).
                ToList();
                return data;
            }
        }

        public List<Simba.DataLayer.Database.Ticket> GetTicketCondominio(int condId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Ticket>().
                Where(c => c.Condominium.Oid == condId).
                ToList();
                return data;
            }
        }

        public List<Simba.DataLayer.Database.Ticket> GetTicketSupplier(int? idCond)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<Simba.DataLayer.Database.User>(User.GetUserId());
                var tiks = uw.Query<Simba.DataLayer.Database.TicketSuplliers>()
                .Where(t => t.IdSuplier.Oid == user.Oid)
                .Select(t => t.IdTicket.Oid);
                var data = uw.Query<Simba.DataLayer.Database.Ticket>()
                .Where(t => (t.isPublic || tiks.Contains(t.Oid)) && t.Condominium.Oid == idCond)
                .ToList();
                return data;
            }
        }

        public List<Simba.DataLayer.Database.TicketStatuses> GetTicketStatuses(int idTicket)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.TicketStatuses>().
                Where(c => c.IdTicket.Oid == idTicket).
                ToList();
                return data;
            }
        }

        public List<Simba.DataLayer.Database.TicketClassification> GetLastTicketClassification(int idTicket)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.TicketClassifications>().
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
                var user = uw.GetObjectByKey<Simba.DataLayer.Database.User>(User.GetUserId());

                var condominio = uw.GetObjectByKey<Simba.DataLayer.Database.Condominium>(obj.Condominio);
                var edificio = uw.GetObjectByKey<Simba.DataLayer.Database.Building>(obj.Edificio);
                var locale = uw.GetObjectByKey<Simba.DataLayer.Database.Environment>(obj.Locale);

                Simba.DataLayer.Database.Ticket ticket = new Simba.DataLayer.Database.Ticket(uw)
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

                var classification = uw.GetObjectByKey<Simba.DataLayer.Database.TicketClassification>(obj.ClasseTicket);
                ticket.classification = classification.Oid;
                ticket.Save();
                uw.CommitChanges();

                Simba.DataLayer.Database.TicketClassifications classeTicket = new Simba.DataLayer.Database.TicketClassifications(uw)
                {
                    IdTicket = ticket,
                    IdClassification = classification
                };
                classeTicket.Save();
                uw.CommitChanges();

                var ticketNew = uw.GetObjectByKey<Simba.DataLayer.Database.Ticket>(ticket.Oid);
                string codice = string.Concat(ticket.Condominium.Oid.ToString().PadLeft(6, '0'), ticket.Number.ToString().PadLeft(6, '0'));
                ticketNew.Code = codice;
                ticketNew.Save();
                uw.CommitChanges();
            }
        }

        public void UpdateTicket(AddTicket obj)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<Simba.DataLayer.Database.User>(User.GetUserId());
                var ticket = uw.GetObjectByKey<Simba.DataLayer.Database.Ticket>(obj.Oid);
                var condominio = uw.GetObjectByKey<Simba.DataLayer.Database.Condominium>(obj.Condominio);
                var edificio = uw.GetObjectByKey<Simba.DataLayer.Database.Building>(obj.Edificio);
                var locale = uw.GetObjectByKey<Simba.DataLayer.Database.Environment>(obj.Locale);
                var stato = uw.GetObjectByKey<Simba.DataLayer.Database.TicketStatus>(obj.Stato);
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
                    var classification = uw.GetObjectByKey<Simba.DataLayer.Database.TicketClassification>(obj.ClasseTicket);

                    Simba.DataLayer.Database.TicketClassifications classeTicket = new Simba.DataLayer.Database.TicketClassifications(uw)
                    {
                        IdTicket = ticket,
                        IdClassification = classification
                    };
                    classeTicket.Save();

                }
                if (obj.Stato != ticket.TicketStatus)
                {

                    Simba.DataLayer.Database.TicketStatuses statoTicket = new Simba.DataLayer.Database.TicketStatuses(uw)
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
