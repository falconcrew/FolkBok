using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkBok
{
    class DBCommunication
    {
        private SqlConnection connection;
        private SqlCommand cmd;
        private SqlDataReader reader;

        public DBCommunication()
        {
            string DBConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = " + Directory.GetCurrentDirectory() + "\\FolkBok.mdf; Integrated Security = True; Connect Timeout = 30";
            connection = new SqlConnection(DBConnectionString);
        }

        public bool AddInvoice(Invoice invoice)
        {
            connection.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            cmd.CommandText = String.Format("insert into Invoices (Date, Address, OurReference, YourReference, Sum) values ('{0}', '{1}', '{2}', '{3}', {4})", invoice.Date, invoice.Address, invoice.OurReference, invoice.YourReference, invoice.Sum);
            cmd.ExecuteNonQuery();
            int test = (int)cmd.ExecuteScalar();
            List<int> DBLines = new List<int>();
            foreach (InvoiceLine line in invoice.Lines)
            {
                cmd.CommandText = String.Format("insert into InvoiceLines (Description, Date, Amount) values ('{0}, {1}, {2}')");
                reader = cmd.ExecuteReader();
                //reader
            }
            connection.Close();
            return true;
        }

        public List<Invoice> Invoices
        {
            get
            {
                List<Invoice> invoices = new List<Invoice>();
                connection.Open();
                cmd = new SqlCommand("select * from Invoices");
                cmd.Connection = connection;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Invoice invoice = new Invoice((string)reader["Address"], (DateTime)reader["Date"], (string)reader["OurReference"], (string)reader["YourReference"]);
                    }
                }
                connection.Close();
                return invoices;
            }
        }

        public Invoice GetInvoice(int number)
        {
            Invoice invoice;
            connection.Open();
            cmd = new SqlCommand("select * from Accounts where id=" + number);
            cmd.Connection = connection;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                invoice = new Invoice((string)reader["Address"], (DateTime)reader["Date"], (string)reader["OurReference"], (string)reader["YourReference"]);
            }
            connection.Close();
            return invoice;
        }

        public bool addVoucher(Voucher voucher)
        {
            return true;
        }

        public bool addAccount(Account account)
        {
            connection.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = String.Format("insert into Account (Name, Number) values ('{0}', {1})", account.Name, account.Number);
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public List<Account> ImportAccounts()
        {
            List<Account> accounts = new List<Account>();
            connection.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ("select * from Accounts");
            cmd.Connection = connection;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                accounts.Add(new Account(Convert.ToInt32(reader["Number"].ToString()), reader["Name"].ToString()));
            }
            connection.Close();
            return accounts;
        }
    }
}
