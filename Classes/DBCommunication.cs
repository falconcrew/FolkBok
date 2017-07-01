using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolkBok
{
    public class DBCommunication
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
            cmd.CommandText = String.Format("insert into Invoices (Name, Date, Address, OurReference, YourReference) output inserted.ID values" +
                " ('{0}', '{1}', '{2}', '{3}', '{4}') output inserted.ID",
                invoice.Name, invoice.Date, invoice.Address, invoice.OurReference, invoice.YourReference, invoice.Sum);
            reader = cmd.ExecuteReader();
            reader.Read();
            invoice.Number = (int)reader["ID"];
            reader.Close();
            int test = (int)cmd.ExecuteScalar();
            List<int> DBLines = new List<int>();
            foreach (InvoiceLine line in invoice.Lines)
            {
                cmd.CommandText = String.Format("insert into Lines (Description, Date, Amount) output inserted.ID values " +
                    "('{0}', '{1}', {2})", line.Description, line.Date.ToShortDateString(), line.Amount);
                reader = cmd.ExecuteReader();
                reader.Read();
                DBLines.Add((int)reader["ID"]);
                reader.Close();
            }
            connection.Close();
            return true;
        }

        public bool GetInvoices(ref List<string> invoiceNames, ref List<int> invoiceIDs)
        {
            connection.Open();
            cmd = new SqlCommand("select ID from Invoices");
            cmd.Connection = connection;
            List<int> InvoiceIDs = new List<int>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                invoiceIDs.Add((int)reader["ID"]);
                invoiceNames.Add((string)reader["Name"]);
            }
            connection.Close();
            return true;
        }

        public Invoice GetInvoice(int number)
        {
            try
            {
                connection.Open();
                cmd = new SqlCommand("select * from Invoices where ID=" + number);
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                reader.Read();
                Invoice invoice = new Invoice((string)reader["Name"], (string)reader["Address"], (DateTime)reader["Date"],
                    (string)reader["OurReference"], (string)reader["YourReference"]);
                reader.Close();
                cmd.CommandText = "select Line_ID from InvoiceLines where Invoice_ID=" + number;
                reader = cmd.ExecuteReader();
                List<int> lineIds = new List<int>();
                while (reader.Read())
                {
                    lineIds.Add((int)reader["Line_ID"]);
                }
                reader.Close();
                foreach (int id in lineIds)
                {
                    cmd.CommandText = "select * from Lines where ID=" + id;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    invoice.AddLine((string)reader["Description"], Convert.ToDateTime(reader["Date"]), Convert.ToDouble(reader["Amount"]));
                    reader.Close();
                }
                connection.Close();
                return invoice;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }

        public bool AddVoucher(Voucher voucher)
        {
            connection.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            cmd.CommandText = String.Format("insert into Vouchers (Description, AccountingDate, VoucherDate) output inserted.ID values" +
                " ('{0}', '{1}', '{2}') output inserted.ID", 
                voucher.Description, voucher.AccountingDate.ToShortDateString(), voucher.VoucherDate.ToShortDateString());
            reader = cmd.ExecuteReader();
            reader.Read();
            voucher.Number = (int)reader["ID"];
            reader.Close();
            int test = (int)cmd.ExecuteScalar();
            List<int> DBLines = new List<int>();
            foreach (VoucherLine line in voucher.Lines)
            {
                cmd.CommandText = String.Format("insert into Lines (Account, Debet, Kredit) output inserted.ID values ({0}, {1}, {2})", line.Account, line.Debet, line.Kredit);
                reader = cmd.ExecuteReader();
                reader.Read();
                DBLines.Add((int)reader["ID"]);
                reader.Close();
            }
            connection.Close();
            return true;
        }

        public Voucher GetVoucher(int number)
        {
            try
            {
                connection.Open();
                cmd = new SqlCommand("select * from Vouchers where ID=" + number);
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                reader.Read();
                Voucher voucher = new Voucher((int)reader["ID"], (string)reader["Description"], (DateTime)reader["VoucherDate"], (DateTime)reader["AccountingDate"]);
                reader.Close();
                cmd.CommandText = "select Line_ID from VoucherLines where Voucher_ID=" + number;
                reader = cmd.ExecuteReader();
                List<int> lineIds = new List<int>();
                while (reader.Read())
                {
                    lineIds.Add((int)reader["Line_ID"]);
                }
                reader.Close();
                List<Account> accounts = ImportAccounts();
                foreach (int id in lineIds)
                {
                    cmd.CommandText = "select * from Lines where ID=" + id;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    voucher.AddLine(accounts[(int)reader["ID"]], (double)reader["Debet"], (double)reader["Kredit"]);
                    reader.Close();
                }
                connection.Close();
                return voucher;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }

        public bool AddAccount(Account account)
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
