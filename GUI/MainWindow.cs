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
            //AddVoucher addVoucher = new AddVoucher();
            //addVoucher.ShowDialog();
            //AddInvoice addInvoice = new AddInvoice();
            //addInvoice.ShowDialog();

            //dbSync();
        }

        private void dbSync()
        {
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\almar\Documents\FolkBok\FolkBok.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Accounts (Name, Number) VALUES ('Test', 123)";
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("select * from Accounts");
            cmd.Connection = connection;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["Name"] + " " + reader["Number"]);
                }
            }
        }
    }
}
