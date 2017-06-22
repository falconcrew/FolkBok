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

        public List<Invoice> GetInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();
            connection.Open();
            cmd = new SqlCommand("select ID, Name from Invoices");
            cmd.Connection = connection;
            List<int> InvoiceIDs = new List<int>();
            reader = cmd.ExecuteReader();
                while (reader.Read())
            {
                InvoiceIDs.Add((int)reader["ID"]);
            }
            connection.Close();
            foreach (int ID in InvoiceIDs)
            {
                invoices.Add(GetInvoice(ID));
            }
            return invoices;
        }

        public Invoice GetInvoice(int number)
        {
            Invoice invoice;
            connection.Open();
            cmd = new SqlCommand("select * from Invoices where ID=" + number);
            cmd.Connection = connection;
            reader = cmd.ExecuteReader();
            reader.Read();
            invoice = new Invoice((string)reader["Address"], (DateTime)reader["Date"], (string)reader["OurReference"], (string)reader["YourReference"]);
            reader.Close();
            cmd.CommandText = "select Line_ID from InvoiceLines where Invoice_ID=" + number;
            reader = cmd.ExecuteReader();
            List<int> lineIds = new List<int>();
            while (reader.Read())
            {
                try
                {
                    lineIds.Add((int)reader["Line_ID"]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            reader.Close();
            foreach (int id in lineIds)
            {
                try
                {
                    cmd.CommandText = "select * from Lines where ID=" + reader["Line_ID"];
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    invoice.AddLine((string)reader["Description"], (DateTime)reader["Date"], (double)reader["Amount"]);
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
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
            cmd.CommandText = string.Format("insert into Account (Name, Number) values ('{0}', {1})", account.Name, account.Number);
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
