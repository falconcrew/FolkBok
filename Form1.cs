using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp;
using System.Data.SqlClient;
using System.Configuration;

namespace FolkBok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dbSync();
        }

        private void dbSync()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Git Repositories\FolkBok\FolkBok.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            /*cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Accounts (Name, Number) VALUES ('Test', 123)";
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();*/
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

    public class Voucher
    {
        private int number;
        private string description;
        private DateTime date;
        private List<Line> lines;

        public Voucher(int number, string description, DateTime date)
        {
            this.number = number;
            this.description = description;
            this.date = date;
            lines = new List<Line>();
        }
    }

    public class Line
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

    public class Account
    {
        private string name;
        private int number;

        public Account(int number, string name)
        {
            this.name = name;
            this.number = number;
        }
    }
}
