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
    class Global
    {
        private int voucherNumber;
        private int invoiceNumber;
        private int paymentTerm;
        private double penaltyInterest;
        private string address;
        private string name;
        private string phoneNumber;
        private string email;
        private string orgNumber;
        private bool fskatt;
        private string bankgiro;

        public Global()
        {
            File.Copy(Directory.GetCurrentDirectory() + "\\FolkBok.mdf", Directory.GetCurrentDirectory() + "\\Test.mdf", true);
            SqlConnection connection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = "
                + Directory.GetCurrentDirectory() + "\\Test.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = String.Format("select * from Globals");
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            try
            {
                voucherNumber = (int)reader["VoucherNumber"];
                invoiceNumber = (int)reader["InvoiceNumber"];
                paymentTerm = (int)reader["PaymentTerm"];
                penaltyInterest = (double)reader["PenaltyInterest"];
                address = (string)reader["Address"];
                name = (string)reader["Name"];
                phoneNumber = (string)reader["PhoneNumber"];
                email = (string)reader["email"];
                orgNumber = (string)reader["OrgNumber"];
                fskatt = (bool)reader["FSkatt"];
                bankgiro = (string)reader["Bankgiro"];
            }
            catch
            {
                
            }
            connection.Close();
        }

        public bool updateInformation()
        {
            SqlConnection connection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = "
                + Directory.GetCurrentDirectory() + "\\FolkBok.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = String.Format("insert into Globals (PaymentTerm, PenaltyInterest, Address, Name, PhoneNumber, Email, OrgNUmber, FSkatt, Bankgiro) values ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}') where ID=1",
                PaymentTerm, PenaltyInterest, Address, Name, PhoneNumber, OrgNumber, FSkatt, Bankgiro);
            cmd.ExecuteNonQuery();
            return true;
        }

        public int VoucherNumber
        {
            get => voucherNumber;
            set => voucherNumber = value;
        }

        public int InvoiceNumber
        {
            get => invoiceNumber;
            set => invoiceNumber = value;
        }

        public int PaymentTerm
        {
            get => paymentTerm;
            set => paymentTerm = value;
        }

        public double PenaltyInterest
        {
            get => penaltyInterest;
            set => penaltyInterest = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => phoneNumber = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string OrgNumber
        {
            get => orgNumber;
            set => orgNumber = value;
        }

        public bool FSkatt
        {
            get => fskatt;
            set => fskatt = value;
        }

        public string Bankgiro
        {
            get => bankgiro;
            set => bankgiro = value;
        }
    }
}
