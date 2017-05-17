using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok.Classes
{
    class Voucher
    {
        private int number;
        private string description;
        private DateTime date;
        private List<Line> lines;

        public Voucher(int number, string description, DateTime date)
        {
            this.number = number;
            this.description = description;
            this.date = date;
            lines = new List<Line>();
        }
    }
}
