using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    class Line
    {
        private Account account;
        private int debet;
        private int kredit;

        public Line(Account account, int debet, int kredit)
        {
            this.account = account;
            this.debet = debet;
            this.kredit = kredit;
        }
    }
}
