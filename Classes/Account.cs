using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok.Classes
{
    class Account
    {
        private string name;
        private int number;

        public Account(int number, string name)
        {
            this.name = name;
            this.number = number;
        }
    }
}
