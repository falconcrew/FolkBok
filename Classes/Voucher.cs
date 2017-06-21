using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    class Voucher
    {
        private List<VoucherLine> lines;

        public Voucher(int number, string description, DateTime voucherDate, DateTime accountingDate)
        {
            Number = number;
            Description = description;
            VoucherDate = voucherDate;
            AccountingDate = accountingDate;
            Lines = new List<VoucherLine>();
        }

        public int Number
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public DateTime VoucherDate
        {
            get;
            private set;
        }

        public DateTime AccountingDate
        {
            get;
            private set;
        }

        public List<VoucherLine> Lines
        {
            get
            {
                return lines;
            }

            private set
            {
                lines = value;
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

        public VoucherLine(Account account, double debet, double kredit)
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
