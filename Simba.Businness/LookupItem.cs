using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simba.Businness
{
    public class LookupItem
    {
        public LookupItem(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
        public int Id { get; set; }
        public string Text { get; set; }
    }
}