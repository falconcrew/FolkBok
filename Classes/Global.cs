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
    public class Global
    {
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
                VoucherNumber = (int)reader["VoucherNumber"];
                InvoiceNumber = (int)reader["InvoiceNumber"];
                PaymentTerm = (int)reader["PaymentTerm"];
                PenaltyInterest = (double)reader["PenaltyInterest"];
                Address = (string)reader["Address"];
                Name = (string)reader["Name"];
                PhoneNumber = (string)reader["PhoneNumber"];
                Email = (string)reader["Email"];
                OrgNumber = (string)reader["OrgNumber"];
                FSkatt = (bool)reader["FSkatt"];
                Bankgiro = (string)reader["Bankgiro"];
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
            cmd.CommandText = String.Format("insert into Globals (PaymentTerm, PenaltyInterest, Address, Name, PhoneNumber, Email, OrgNUmber, FSkatt, Bankgiro) "+
                "values ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}') where ID=1",
                PaymentTerm, PenaltyInterest, Address, Name, PhoneNumber, OrgNumber, FSkatt, Bankgiro);
            cmd.ExecuteNonQuery();
            return true;
        }

        public int VoucherNumber
        {
            get;
            private set;
        }

        public int InvoiceNumber
        {
            get;
            private set;
        }

        public int PaymentTerm
        {
            get;
            private set;
        }

        public double PenaltyInterest
        {
            get;
            private set;
        }

        public string Address
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string PhoneNumber
        {
            get;
            private set;
        }

        public string Email
        {
            get;
            private set;
        }

        public string OrgNumber
        {
            get;
            private set;
        }

        public bool FSkatt
        {
            get;
            private set;
        }

        public string Bankgiro
        {
            get;
            private set;
        }
    }
}
