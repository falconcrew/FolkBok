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
    public static class Global
    {
        private static string ConnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = "
                + Directory.GetCurrentDirectory() + "\\FolkBok.mdf; Integrated Security = True; Connect Timeout = 30";

        public static void Init()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = String.Format("select * from Globals");
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
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
                reader.Close();
            }
            catch
            {

            }
            connection.Close();
        }

        public static bool UpdateInformation()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = String.Format("insert into Globals (PaymentTerm, PenaltyInterest, Address, Name, PhoneNumber, Email, OrgNUmber, FSkatt, Bankgiro) "+
                "values ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}') where ID=1",
                PaymentTerm, PenaltyInterest, Address, Name, PhoneNumber, OrgNumber, FSkatt, Bankgiro);
            cmd.ExecuteNonQuery();
            return true;
        }

        public static int VoucherNumber
        {
            get;
            set;
        }

        public static int InvoiceNumber
        {
            get;
            set;
        }

        public static int PaymentTerm
        {
            get;
            set;
        }

        public static double PenaltyInterest
        {
            get;
            set;
        }

        public static string Address
        {
            get;
            set;
        }

        public static string Name
        {
            get;
            set;
        }

        public static string PhoneNumber
        {
            get;
            set;
        }

        public static string Email
        {
            get;
            set;
        }

        public static string OrgNumber
        {
            get;
            set;
        }

        public static bool FSkatt
        {
            get;
            set;
        }

        public static string Bankgiro
        {
            get;
            set;
        }
    }
}
