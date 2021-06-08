using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness.Models
{
    public class Login
    {
        public Login(string user, string password, string pessage)
        {
            this.User = user;
            this.Password = password;
            this.Message = Message;
        }

        public string User
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }
    }
}
