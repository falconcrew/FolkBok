using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolkBok
{
    public partial class PrintPreview : Form
    {
        public PrintPreview()
        {
            InitializeComponent();
            Document = new PrintDocument();
            printPreviewControl1.Document = Document;
        }

        public PrintPreview(int height, int width) : this()
        {
            Height = height;
            Width = width;
        }

        private void PrintPreview_SizeChanged(object sender, EventArgs e)
        {
            toolStrip1.Width = Width - 16;
            printPreviewControl1.Width = Width - 16;
            printPreviewControl1.Height = Height - printPreviewControl1.Location.Y - 40;
        }

        public PrintDocument Document
        {
            get
            {
                return printPreviewControl1.Document;
            }
            set
            {
                printPreviewControl1.Document = value;
            }
        }
    }
}
