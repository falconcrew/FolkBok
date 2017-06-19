using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
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

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Number
        {
            get => number;
            set => number = value;
        }
    }
}
