using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Web.Http;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Simba.Businness;

namespace SimbaCondomini.Controllers
{
    public class AmbienteDatiController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            Locali a = new Locali();
            var data = a.GetAll(1);
            var datac = new List<Simba.Businness.Models.Ambiente>();
            foreach (var cc in data)
            {
                datac.Add(new Simba.Businness.Models.Ambiente(cc.Oid, cc.Name, cc.Description, cc.Building.Oid, cc.Valid));
            }

            return Request.CreateResponse(DataSourceLoader.Load(datac, loadOptions));
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id, DataSourceLoadOptions loadOptions)
        {
            Locali a = new Locali();
            var data = a.GetAll(id);
            var datac = new List<Simba.Businness.Models.Ambiente>();
            foreach (var cc in data)
            {
                datac.Add(new Simba.Businness.Models.Ambiente(cc.Oid, cc.Name, cc.Description, cc.Building.Oid, cc.Valid));
            }

            return Request.CreateResponse(DataSourceLoader.Load(datac, loadOptions));
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}