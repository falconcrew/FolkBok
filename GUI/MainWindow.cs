using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using PdfSharp;
using System.Data.SqlClient;
using System.Configuration;

namespace FolkBok
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            AddInvoice addInvoice = new AddInvoice();
            addInvoice.ShowDialog();

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

        private void addAccount(Object sender, EventArgs e)
        {

        }
    }
}
