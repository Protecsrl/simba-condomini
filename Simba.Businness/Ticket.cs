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

        public void SaveTicket(AddTicket obj)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(5);

                DataLayer.simba_condomini.Ticket ticket = new DataLayer.simba_condomini.Ticket(uw)
                {
                    Descrizione = obj.Descrizione,
                    Titolo = obj.Titolo,
                    TicketStatus = 1,
                    Data = DateTime.Now,
                    Note = obj.Note,
                    Number = obj.Number,
                    DateCreation = DateTime.Now,
                    User = user
                };


                var classification = uw.GetObjectByKey<DataLayer.simba_condomini.TicketClassification>(1);

                DataLayer.simba_condomini.TicketClassifications classeTicket = new DataLayer.simba_condomini.TicketClassifications(uw)
                {
                    IdTicket = ticket,
                    IdClassification = classification
                };
                ticket.Save();
                classeTicket.Save();
                uw.CommitChanges();
            }
        }

        public void UpdateTicket(AddTicket obj)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var ticket = uw.GetObjectByKey<DataLayer.simba_condomini.Ticket>(obj.Oid);

                ticket.Data = DateTime.Now;
                ticket.DateUpdate = DateTime.Now;
                ticket.Descrizione = obj.Descrizione;
                ticket.Note = obj.Note;
                ticket.Number = obj.Number;
                ticket.Save();
                uw.CommitChanges();
            }
        }
    }
}
