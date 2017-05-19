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
        private DateTime date;
        private int number;
        private string ourReference;
        private string yourReference;
        private List<InvoiceLine> lines;
        private double sum;

        public Invoice(string address, DateTime date, int number, string ourReference, string yourReference)
        {
            Address = address;
            Date = date;
            OurReference = ourReference;
            YourReference = yourReference;
            this.number = number;
            lines = new List<InvoiceLine>();
        }

        public void addLine(string description, DateTime date, double cost)
        {
            lines.Add(new InvoiceLine(description, date, cost));
            sum += cost;
        }

        public void removeLine(int index)
        {
            InvoiceLine line = lines.ElementAt(index);
            sum -= line.Cost;
            lines.Remove(line);
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string OurReference
        {
            get
            {
                return ourReference;
            }

            set
            {
                ourReference = value;
            }
        }

        public string YourReference
        {
            get
            {
                return yourReference;
            }

            set
            {
                yourReference = value;
            }
        }
    }

    class InvoiceLine
    {
        private string description;
        private double cost;
        private DateTime date;

        public InvoiceLine(string description, DateTime date, double cost)
        {
            Description = description;
            Date = date;
            Cost = cost;
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public double Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }
    }
}
