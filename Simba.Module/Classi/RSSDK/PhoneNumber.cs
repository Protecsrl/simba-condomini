using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public abstract class PhoneNumber {
        protected string number;

        public PhoneNumber(string number) {
            this.number = number;
        }

        public abstract bool isValid();

        public string Number {
            get { return this.number; }
        }

    }
}
