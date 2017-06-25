using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    public class Invoice
    {
        private List<InvoiceLine> lines;

        public Invoice(string name, string address, DateTime date, string ourReference, string yourReference)
        {
            Name = name;
            Address = address;
            Date = date;
            OurReference = ourReference;
            YourReference = yourReference;
            SetNumber();
            lines = new List<InvoiceLine>();
        }

        public void AddLine(string description, DateTime date, double amount)
        {
            lines.Add(new InvoiceLine(description, date, amount));
            Sum += amount;
        }

        public void RemoveLine(int index)
        {
            InvoiceLine line = lines.ElementAt(index);
            Sum -= line.Amount;
            lines.Remove(line);
        }

        public List<InvoiceLine> Lines => lines;

        private void SetNumber()
        {
            Number = 12;
        }

        public string Name
        {
            get;
            private set;
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
            set;
        }

        public double Sum
        {
            get;
            private set;
        }
    }

    public class InvoiceLine
    {
        public InvoiceLine(string description, DateTime date, double amount)
        {
            Description = description;
            Date = date;
            Amount = amount;
        }

        public string Description
        {
            get;
            private set;
        }

        public double Amount
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
