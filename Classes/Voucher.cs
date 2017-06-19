using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    class Voucher
    {
        private int number;
        private string description;
        private DateTime date;
        private List<VoucherLine> lines;

        public Voucher(int number, string description, DateTime date)
        {
            this.number = number;
            this.description = description;
            this.date = date;
            lines = new List<VoucherLine>();
        }

        public int Number
        {
            get
            {
                return number;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        public List<VoucherLine> Lines
        {
            get
            {
                return lines;
            }
        }

        public void AddLine(VoucherLine line)
        {
            lines.Add(line);
        }
    }

    class VoucherLine
    {
        private Account account;
        private double debet;
        private double kredit;

        public VoucherLine(Account account, int debet, int kredit)
        {
            this.account = account;
            this.debet = debet;
            this.kredit = kredit;
        }

        public Account Account => account;

        public double Debet => debet;

        public double Kredit => kredit;
    }
}
