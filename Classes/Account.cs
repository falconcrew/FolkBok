using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    public class Account
    {
        public Account(int number, string name)
        {
            Name = name;
            Number = number;
        }

        public string Name
        {
            get;
            private set;
        }

        public int Number
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", Number, Name);
        }
    }
}
