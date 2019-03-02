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
//using System.Windows;
using System.Drawing.Printing;

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
            SetupListViews();
            //ImportAccounts();
            ImportInvoices();
            //InvoiceForm i = new InvoiceForm();
            //i.ShowDialog();

            //DBCommunication dbCom = new DBCommunication();
            height = ClientSize.Height;
            width = ClientSize.Width;

            listViewWidth[0] = listView1.Width;
            listViewWidth[1] = listView2.Width;
            listViewWidth[2] = listView3.Width;

            /*PrintPreviewDialog printPrev = new PrintPreviewDialog();
            printPrev.Document = new PrintDocument();
            Graphics gfx = printPrev.CreateGraphics();
            Pen pen = new Pen(Brushes.Black, 30);
            gfx.DrawLine(pen, 0, 0, 200, 400);
            printPrev.Height = 1000;
            printPrev.Width = (int) (1000 / Math.Sqrt(2));
            printPrev.ShowDialog();*/
        }

        private void SetupListViews()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Nummer", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Namn", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Balans", -2, HorizontalAlignment.Left);

            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.Columns.Add("Nummer", -2, HorizontalAlignment.Left);
            listView2.Columns.Add("Namn", -2, HorizontalAlignment.Left);
        }

        private void ImportInvoices()
        {
            List<string> invoices = Global.DBCom.GetInvoices();
            int number = 0;
            foreach (string name in invoices)
            {
                number++;
                ListViewItem i = new ListViewItem(number.ToString());
                i.SubItems.Add(name);
                listView2.Items.Add(i);
            }
        }

        private void ImportAccounts()
        {
            DBCommunication DBCom = new DBCommunication();
            /*foreach(Account a in DBCom.ImportAccounts())
            {
                ListViewItem item = new ListViewItem(a.Number.ToString());
                item.SubItems.Add(a.Name);
                item.SubItems.Add(a.Balance.ToString());
                listView1.Items.Add(item);
            }*/
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
            listViewHeight = height - tableLayoutPanel1.Location.Y - 3 - 12 - 3 - 12;
            listView1.Height = (int)Math.Round(listViewHeight);
            listView1.Width = (int)Math.Round(listViewWidth[0]);
            listView2.Height = (int)Math.Round(listViewHeight);
            listView2.Width = (int)Math.Round(listViewWidth[1]);
            listView3.Height = (int)Math.Round(listViewHeight);
            listView3.Width = (int)Math.Round(listViewWidth[2]);
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            foreach (ListViewItem i in lv.Items)
            {
                if(i.Selected)
                {
                    int number = int.Parse(i.Text);
                    Invoice invoice = Global.DBCom.GetInvoice(number);
                    InvoiceForm IF = new InvoiceForm(invoice);
                    IF.ShowDialog();
                }
            }
        }
    }
}
