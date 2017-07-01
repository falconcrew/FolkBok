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
        private int height;
        private int width;

        private double listViewHeight;
        private double[] listViewWidth = new double[3];

        public MainWindow()
        {
            InitializeComponent();
            Global.Init();

            DBCommunication dbCom = new DBCommunication();
            //Invoice i = dbCom.GetInvoice(2);
            //InvoiceForm InvoiceForm = new InvoiceForm(i);
            //InvoiceForm.ShowDialog();
            //Voucher v = dbCom.GetVoucher(1);
            //VoucherForm VoucherForm = new VoucherForm(v);
            height = ClientSize.Height;
            width = ClientSize.Width;

            listViewWidth[0] = listView1.Width;
            listViewWidth[1] = listView2.Width;
            listViewWidth[2] = listView3.Width;
            
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            //Global g = new Global();
            //Console.WriteLine(g.VoucherNumber);
            //VoucherForm VoucherForm = new VoucherForm();
            //VoucherForm.ShowDialog();
            //AddInvoice addInvoice = new AddInvoice();
            //addInvoice.ShowDialog();

            //dbSync();

            /*Random r = new Random();
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
            }*/

            ImportAccounts();
        }
        
        private void ImportAccounts()
        {
            DBCommunication DBCom = new DBCommunication();
            foreach(Account a in DBCom.ImportAccounts())
            {
                ListViewItem item = new ListViewItem(a.Number.ToString());
                item.SubItems.Add(a.Name);
                item.SubItems.Add(a.Balance.ToString());
                listView1.Items.Add(item);
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

        private void button1_Click(object sender, EventArgs e)
        {
            //DBCommunication dbCom = new DBCommunication();
            //Invoice i = dbCom.GetInvoice(2);
            InvoiceForm InvoiceForm = new InvoiceForm();
            InvoiceForm.ShowDialog();
        }

        private void ChangeSize(object sender, EventArgs e)
        {
            int newHeight = ClientSize.Height;
            int newWidth = ClientSize.Width;
            double heightScalingFactor = (double) newHeight / height;
            double widthScalingFactor = (double) newWidth / width;
            height = newHeight;
            width = newWidth;
            listViewWidth[0] *= widthScalingFactor;
            listViewWidth[1] *= widthScalingFactor;
            listViewWidth[2] *= widthScalingFactor;
            double addWidth = (width - listViewWidth[0] - listViewWidth[1] - listViewWidth[2] - 42) / 3;
            listViewWidth[0] += addWidth;
            listViewWidth[1] += addWidth;
            listViewWidth[2] += addWidth;
            listViewHeight = height - tableLayoutPanel1.Location.Y - 3 - 12;
            listView1.Height = (int)Math.Round(listViewHeight);
            listView1.Width = (int)Math.Round(listViewWidth[0]);
            listView2.Height = (int)Math.Round(listViewHeight);
            listView2.Width = (int)Math.Round(listViewWidth[1]);
            listView3.Height = (int)Math.Round(listViewHeight);
            listView3.Width = (int)Math.Round(listViewWidth[2]);
        }
    }
}
