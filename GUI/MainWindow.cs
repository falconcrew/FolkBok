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
            //Global g = new Global();
            //Console.WriteLine(g.VoucherNumber);
            //AddVoucher addVoucher = new AddVoucher();
            //addVoucher.ShowDialog();
            //AddInvoice addInvoice = new AddInvoice();
            //addInvoice.ShowDialog();

            //dbSync();

            Random r = new Random();
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < 10000; i++)
            {
                int n = r.Next(1000000);
                list1.Add(n);
                list2.Add(n);
            }
            var watch = System.Diagnostics.Stopwatch.StartNew();
            list1 = quickSort(list1);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch = System.Diagnostics.Stopwatch.StartNew();
            list2.Sort();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            for(int i = 0; i < 10000; i++)
            {
                Console.WriteLine(list1[i] + "      " + list2[i]);
            }
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

        private List<int> quickSort(List<int> pl)
        {
            if (pl.Count < 2)
            {
                return pl;
            }
            else if (pl.Count == 2)
            {
                if (pl.ElementAt(0) > pl.ElementAt(1))
                {
                    pl.Reverse();
                    return pl;
                }
            }
            else
            {
                List<int> less = new List<int>();
                List<int> more = new List<int>();
                int mid = pl.ElementAt(pl.Count / 2);
                pl.Remove(mid);
                foreach (int p in pl)
                {
                    int pl0 = p;
                    int pl1 = mid;
                    int check = pl0.CompareTo(pl1);
                    if (p < mid)
                    {
                        less.Add(p);
                    }
                    else
                    {
                        more.Add(p);
                    }
                }
                less = quickSort(less);
                more = quickSort(more);
                less.Add(mid);
                less.AddRange(more);
                pl = less;
            }
            return pl;
        }
    }
}
