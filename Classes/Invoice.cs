using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    class Invoice
    {
        private string address;
        private string name;
        private DateTime date;
        private int number;
        private string ourReference;
        private string yourReference;
        private List<string> linedesc;
        private List<int> linecost;

        public Invoice(string address, string name, DateTime date)
        {
            this.address = address;
            this.name = name;
            this.date = date;
            linedesc = new List<string>();
            linecost = new List<int>();
        }

        public void addLine(string description, int cost)
        {
            linedesc.Add(description);
            linecost.Add(cost);
        }
    }
}
