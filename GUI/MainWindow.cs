using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FolkBok
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            label1.Text = String.Format("insert into Account (Name, Number) VALUES ('{0}', {1})", "Test", 1234);

            //AddInvoice addInvoice = new AddInvoice();
            //addInvoice.ShowDialog();
            Global g = new Global();
            Console.WriteLine(g.VoucherNumber);
        }
    }
}
