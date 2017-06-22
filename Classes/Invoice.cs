using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    class Invoice
    {
        private List<InvoiceLine> lines;

        public Invoice(string address, DateTime date, string ourReference, string yourReference)
        {
            Address = address;
            Date = date;
            OurReference = ourReference;
            YourReference = yourReference;
            SetNumber();
            lines = new List<InvoiceLine>();
        }

        public void AddLine(string description, DateTime date, double cost)
        {
            lines.Add(new InvoiceLine(description, date, cost));
            Sum += cost;
        }

        public void RemoveLine(int index)
        {
            InvoiceLine line = lines.ElementAt(index);
            Sum -= line.Cost;
            lines.Remove(line);
        }

        public List<InvoiceLine> Lines => lines;

        private void SetNumber()
        {
            Number = 12;
        }

        public string Address
        {
            get;
            private set;
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public string OurReference
        {
            get;
            private set;
        }

        public string YourReference
        {
            get;
            private set;
        }

        public int Number
        {
            get;
            private set;
        }

        public double Sum
        {
            get;
            private set;
        }
    }

    class InvoiceLine
    {
        public InvoiceLine(string description, DateTime date, double cost)
        {
            Description = description;
            Date = date;
            Cost = cost;
        }

        public string Description
        {
            get;
            private set;
        }

        public double Cost
        {
            get;
            private set;
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public int ID
        {
            get;
            private set;
        }
    }
}
