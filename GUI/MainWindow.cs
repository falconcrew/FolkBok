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
            height = ClientSize.Height;
            width = ClientSize.Width;
            SetupListViews();
            ImportAccounts();

            PrintPreview prev = new PrintPreview();
            prev.ShowDialog();
        }

        private void SetupListViews()
        {

            listViewWidth[0] = listView1.Width;
            listViewWidth[1] = listView2.Width;
            listViewWidth[2] = listView3.Width;
            
            listView1.Columns.Add("Nummer", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Namn", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Balans", 100, HorizontalAlignment.Left);
        }

        private void ImportAccounts()
        {
            DBCommunication DBCom = new DBCommunication();
            foreach(Account a in DBCom.ImportAccounts())
            {
                ListViewItem item = new ListViewItem(a.Number.ToString());
                item.SubItems.Add(a.Name);
                item.SubItems.Add(a.Balance.ToString());
                item.Font = new Font("Times New Roman", 12);
                
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

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.DrawString(e.Header.Text, new Font("Times New Roman", 12), Brushes.Black, e.Bounds);
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.Graphics.DrawString(listView1.Items[e.ItemIndex].Text, listView1.Items[e.ItemIndex].Font, Brushes.Black, e.Bounds);
        }

        private void listView1_DrawSubItem_1(object sender, DrawListViewSubItemEventArgs e)
        {
            e.Graphics.DrawString(listView1.Items[e.ItemIndex].SubItems[e.ColumnIndex].Text, listView1.Items[e.ItemIndex].Font, Brushes.Black, e.Bounds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Graphics gfx = CreateGraphics();
            gfx.DrawLine(Pens.Black, width, 0, 0, height);*/
            InvoiceForm info = new InvoiceForm();
            info.ShowDialog();
        }
    }
}
