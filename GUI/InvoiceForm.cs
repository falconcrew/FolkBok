using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace FolkBok
{
    public partial class InvoiceForm : Form
    {
        private int invoiceNumber;
        private DateTime invoiceDate;
        private int paymentTerm;
        private DateTime dueDate;
        private double penaltyInterest;
        private List<TextBox> descriptionTextBoxes;
        private List<DateTimePicker> dateBoxes;
        private List<TextBox> amountTextBoxes;
        private string settingsFile = Directory.GetCurrentDirectory() + "\\InvoiceSettings.txt";

        private const int up = -1;
        private const int down = 1;
        
        public InvoiceForm()
        {
            InitializeComponent();
            ImportInvoiceSettings();
            SetupLabels();
            descriptionTextBoxes = new List<TextBox>();
            descriptionTextBoxes.Add(descriptionTextBox1);
            dateTimePicker1.Value = DateTime.Now;
            dateBoxes = new List<DateTimePicker>();
            dateBoxes.Add(dateTimePicker1);
            amountTextBoxes = new List<TextBox>();
            amountTextBoxes.Add(amountTextBox1);
        }

        public InvoiceForm(Invoice invoice) : this()
        {
            ourReferenceTextBox.Text = invoice.OurReference;
            yourReferenceTextBox.Text = invoice.YourReference;
            addressTextBox.AppendText(invoice.Address);
            descriptionTextBox1.Text = invoice.Lines[0].Description;
            dateTimePicker1.Value = invoice.Lines[0].Date;
            amountTextBox1.Text = invoice.Lines[0].Amount.ToString();
            for (int i = 1; i < invoice.Lines.Count; i++)
            {
                AddRow();
                descriptionTextBoxes[i].Text = invoice.Lines[i].Description;
                dateBoxes[i].Value = invoice.Lines[i].Date;
                amountTextBoxes[i].Text = invoice.Lines[i].Amount.ToString();
            }
        }

        private void SetupLabels()
        {
            invoiceNumberLabel.Text = invoiceNumber.ToString();
            invoiceDateLabel.Text = invoiceDate.ToShortDateString();
            paymentTermLabel.Text = paymentTerm.ToString();
            dueDateLabel.Text = dueDate.ToShortDateString();
            penaltyInterestLabel.Text = penaltyInterest.ToString() + "%";
        }

        private void ImportInvoiceSettings()
        {
            StreamReader sr = new StreamReader(settingsFile);
            invoiceNumber = Convert.ToInt32(sr.ReadLine());
            paymentTerm = Convert.ToInt32(sr.ReadLine());
            penaltyInterest = Convert.ToDouble(sr.ReadLine());
            sr.Close();
            invoiceDate = DateTime.Now;
            dueDate = invoiceDate.AddDays(paymentTerm);
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void AddRow() {
            MoveButton(addRowButton, down);
            MoveButton(removeRowButton, down);
            MoveLabel(sumDescriptionLabel, down);
            MoveLabel(sumLabel, down);
            MoveLabel(lineLabel, down);

            TextBox TB = new TextBox();
            Point p = descriptionTextBoxes.Last().Location;
            p.Y += 35;
            TB.Location = p;
            TB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB.Name = "descriptionTextBox" + descriptionTextBoxes.Count;
            TB.Size = new Size(679, 29);
            TB.TabIndex = 21;
            this.Controls.Add(TB);
            descriptionTextBoxes.Add(TB);

            DateTimePicker DP = new DateTimePicker();
            p = dateBoxes.Last().Location;
            p.Y += 35;
            DP.Location = p;
            DP.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DP.Format = DateTimePickerFormat.Short;
            DP.ImeMode = ImeMode.NoControl;
            DP.Name = "dateTimePicker" + dateBoxes.Count;
            DP.Size = new Size(194, 29);
            DP.TabIndex = 32;
            Controls.Add(DP);
            dateBoxes.Add(DP);

            TB = new TextBox();
            p = amountTextBoxes.Last().Location;
            p.Y += 35;
            TB.Location = p;
            TB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB.Name = "amountTextBox" + amountTextBoxes.Count;
            TB.Size = new Size(187, 29);
            TB.TabIndex = 21;
            TB.TextAlign = HorizontalAlignment.Right;
            TB.TextChanged += new EventHandler(UpdateSumLabel);
            this.Controls.Add(TB);
            amountTextBoxes.Add(TB);
        }

        private void RemoveRowButton_Click(object sender, EventArgs e)
        {
            if (amountTextBoxes.Count > 1)
            {
                TextBox TB = descriptionTextBoxes.Last();
                descriptionTextBoxes.Remove(TB);
                Controls.Remove(TB);
                TB = amountTextBoxes.Last();
                amountTextBoxes.Remove(TB);
                Controls.Remove(TB);
                DateTimePicker DP = dateBoxes.Last();
                dateBoxes.Remove(DP);
                Controls.Remove(DP);

                MoveButton(addRowButton, up);
                MoveButton(removeRowButton, up);
                MoveLabel(sumDescriptionLabel, up);
                MoveLabel(sumLabel, up);
                MoveLabel(lineLabel, up);
            }
        }

        private void MoveButton(Button button, int direction)
        {
            Point p = button.Location;
            p.Y += direction * 35;
            button.Location = p;
        }

        private void MoveLabel(Label label, int direction)
        {
            Point p = label.Location;
            p.Y += direction * 35;
            label.Location = p;
        }

        private void UpdateSumLabel(object sender, EventArgs e)
        {
            sumLabel.Text = Sum + " kr";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string address = addressTextBox.Text;
            string ourReference = ourReferenceTextBox.Text;
            string yourReference = yourReferenceTextBox.Text;
            Invoice invoice = new Invoice("Name", address, invoiceDate, ourReference, yourReference);
            for (int i = 0; i < descriptionTextBoxes.Count; i++)
            {
                string description = descriptionTextBoxes.ElementAt(i).Text;
                DateTime date = dateBoxes.ElementAt(i).Value;
                double amount = Convert.ToDouble(amountTextBoxes.ElementAt(i).Text.Replace('.',','));
                invoice.AddLine(description, date, amount);
            }

            InvoicePDF pdf = new InvoicePDF("Faktura " + invoice.Number, invoice);
        }

        private double Sum
        {
            get
            {
                double sum = 0;
                foreach (TextBox textBox in amountTextBoxes)
                {
                    if (textBox.Text.Length > 0)
                    {
                        sum += Convert.ToDouble(textBox.Text.Replace('.', ','));
                    }
                }
                return sum;
            }
        }

        private void printPreviewButton_Click(object sender, EventArgs e)
        {
            PrintPreview printPrev = new PrintPreview();
            printDocument1.PrintPage += new PrintPageEventHandler(PrintPage);
            printPrev.Document = printDocument1;
            printPrev.Height = 1000;
            printPrev.Width = (int)(1000 / Math.Sqrt(2));
            printPrev.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            string address = addressTextBox.Text;
            string ourReference = ourReferenceTextBox.Text;
            string yourReference = yourReferenceTextBox.Text;
            Invoice invoice = new Invoice("Name", address, invoiceDate, ourReference, yourReference);
            for (int i = 0; i < descriptionTextBoxes.Count; i++)
            {
                string description = descriptionTextBoxes.ElementAt(i).Text;
                DateTime date = dateBoxes.ElementAt(i).Value;
                double amount = Convert.ToDouble(amountTextBoxes.ElementAt(i).Text.Replace('.', ','));
                invoice.AddLine(description, date, amount);
            }
            
            Graphics gfx = e.Graphics;

            float pointsPermm = (float)e.PageBounds.Height / 297;
            Font Times11 = new Font("Times New Roman", 11);
            Font Times11Bold = new Font("Times New Roman", 11, FontStyle.Bold);
            Font Times9 = new Font("Times New Roman", 9);
            StringFormat left = new StringFormat();
            left.Alignment = StringAlignment.Near;
            StringFormat right = new StringFormat();
            right.Alignment = StringAlignment.Far;
            StringFormat center = new StringFormat();
            center.Alignment = StringAlignment.Center;

            byte[] data;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FolkBok.Images.FolkLogo.png"))
            {
                int count = (int)stream.Length;
                data = new byte[count];
                stream.Read(data, 0, count);
            }
            MemoryStream mstream = new MemoryStream(data);
            Image image = Image.FromStream(mstream);
            /*image.BeginInit();
            image.StreamSource = mstream;
            image.EndInit();*/
            float topLeftX = 14.5F * pointsPermm;
            float topLeftY = 20.5F * pointsPermm;
            float height = 29.55F * pointsPermm;
            float width = 71.67F * pointsPermm;
            gfx.DrawImage(image, topLeftX, topLeftY, width, height);

            width = 181.74F * pointsPermm;
            gfx.DrawString("Faktura", new Font("Times New Roman", 24), Brushes.Black, topLeftX + width, 27.63F * pointsPermm, right);

            topLeftY = 54.84F * pointsPermm - 1;
            height = 5.67F * pointsPermm + 1;
            width = 88.16F * pointsPermm;
            gfx.FillRectangle(Brushes.Black, topLeftX, topLeftY, width, height);
            PointF lp = new PointF(16.5F * pointsPermm, topLeftY);
            PointF rp = new PointF(16.5F * pointsPermm + width / 2, topLeftY);
            gfx.DrawString("Belopp", new Font("Times New Roman", 14, FontStyle.Bold), Brushes.White, lp, left);
            gfx.DrawString(getAmountString(invoice.Sum), new Font("Times New Roman", 14, FontStyle.Bold), Brushes.White, rp, left);

            topLeftY += height;
            height = 31.25F * pointsPermm;
            gfx.FillRectangle(Brushes.Gainsboro, topLeftX, topLeftY, width, height);
            lp.Y = topLeftY;
            rp.Y = topLeftY;
            gfx.DrawString("Fakturanummer", Times11, Brushes.Black, lp, left);
            gfx.DrawString(invoice.Number.ToString(), Times11, Brushes.Black, rp, left);
            lp.Y = topLeftY + height / 7;
            rp.Y = topLeftY + height / 7;
            gfx.DrawString("Fakturadatum", Times11, Brushes.Black, lp, left);
            gfx.DrawString(invoice.Date.ToShortDateString(), Times11, Brushes.Black, rp, left);
            lp.Y = topLeftY + 2 * height / 7;
            rp.Y = topLeftY + 2 * height / 7;
            gfx.DrawString("Betalningsvillkor", Times11, Brushes.Black, lp, left);
            gfx.DrawString("30 dagar", Times11, Brushes.Black, rp, left);
            lp.Y = topLeftY + 3 * height / 7;
            rp.Y = topLeftY + 3 * height / 7;
            gfx.DrawString("Förfallodag", Times11, Brushes.Black, lp, left);
            gfx.DrawString(invoice.Date.AddDays(30).ToShortDateString(), Times11, Brushes.Black, rp, left);
            lp.Y = topLeftY + 4 * height / 7;
            rp.Y = topLeftY + 4 * height / 7;
            gfx.DrawString("Dröjsmålsränta", Times11, Brushes.Black, lp, left);
            gfx.DrawString("8%", Times11, Brushes.Black, rp, left);
            lp.Y = topLeftY + 5 * height / 7;
            rp.Y = topLeftY + 5 * height / 7;
            gfx.DrawString("Er referens", Times11, Brushes.Black, lp, left);
            gfx.DrawString(invoice.YourReference, Times11, Brushes.Black, rp, left);
            lp.Y = topLeftY + 6 * height / 7;
            rp.Y = topLeftY + 6 * height / 7;
            gfx.DrawString("Vår referens", Times11, Brushes.Black, lp, left);
            gfx.DrawString(invoice.OurReference, Times11, Brushes.Black, rp, left);

            topLeftY = 114.34F * pointsPermm;
            width = 181.74F * pointsPermm;
            height = 4.49F * pointsPermm + 1;
            gfx.FillRectangle(Brushes.Black, topLeftX, topLeftY, width, height);
            gfx.DrawString("Beskrivning", Times11, Brushes.White, new PointF(topLeftX + pointsPermm, topLeftY), left);
            gfx.DrawString("Datum", Times11, Brushes.White, new PointF(topLeftX + 5 * width / 8, topLeftY), left);
            gfx.DrawString("Belopp", Times11, Brushes.White, new PointF(topLeftX + 13 * width / 16, topLeftY), left);
            
            topLeftY += height;
            foreach (InvoiceLine line in invoice.Lines)
            {
                gfx.DrawString(line.Description, Times11, Brushes.Black, new PointF(topLeftX + pointsPermm, topLeftY), left);
                gfx.DrawString(line.Date.ToShortDateString(), Times11, Brushes.Black, new PointF(topLeftX + 5 * width / 8, topLeftY), left);
                gfx.DrawString(getAmountString(line.Amount), Times11, Brushes.Black, new PointF(topLeftX + width, topLeftY), right);
                topLeftY = topLeftY + 5 * pointsPermm;
            }

            gfx.DrawLine(Pens.Black, topLeftX + 5 * width / 8, topLeftY + pointsPermm, topLeftX + width, topLeftY + pointsPermm);
            topLeftY = topLeftY + 1 * pointsPermm;
            gfx.DrawString("Summa att betala", Times11Bold, Brushes.Black, new PointF(topLeftX + 13 * width / 16, topLeftY), right);
            gfx.DrawString(getAmountString(invoice.Sum), Times11Bold, Brushes.Black, new PointF(topLeftX + width, topLeftY), right);

            height = 5;
            PointF p1 = new PointF(lp.X, 153.56F * pointsPermm);
            PointF p2 = new PointF(p1.X + 23.55F * pointsPermm, 153.56F * pointsPermm);
            PointF p3 = new PointF(p2.X + 75.58F * pointsPermm, 153.56F * pointsPermm);
            PointF p4 = new PointF(p3.X + 20.78F * pointsPermm, 153.56F * pointsPermm);
            gfx.DrawString("Organisation", Times9, Brushes.Black, p1, left);
            gfx.DrawString("Folktetten", Times9, Brushes.Black, p2, left);
            gfx.DrawString("Tel.", Times9, Brushes.Black, p3, left);
            gfx.DrawString("0706-141866", Times9, Brushes.Black, p4, left);
            p1.Y += height * pointsPermm;
            p2.Y += height * pointsPermm;
            p3.Y += height * pointsPermm;
            p4.Y += height * pointsPermm;
            gfx.DrawString("Adress", Times9, Brushes.Black, p1, left);
            gfx.DrawString("Skarpskyttevägen 22G Lgh 1201 226 42 Lund", Times9, Brushes.Black, p2, left);
            gfx.DrawString("E-post", Times9, Brushes.Black, p3, left);
            gfx.DrawString("info@folktetten.se", Times9, Brushes.Black, p4, left);
            p1.Y += height * pointsPermm;
            p2.Y += height * pointsPermm;
            p3.Y += height * pointsPermm;
            p4.Y += height * pointsPermm;
            gfx.DrawString("Org. Nr.", Times9, Brushes.Black, p1, left);
            gfx.DrawString("802495-4656", Times9, Brushes.Black, p2, left);
            gfx.DrawString("Hemsida", Times9, Brushes.Black, p3, left);
            gfx.DrawString("www.folktetten.se", Times9, Brushes.Black, p4, left);
            p2.Y += height * pointsPermm;
            gfx.DrawString("Godkänd för F-skatt", Times9, Brushes.Black, p2, left);

            p2.X += 43.57F * pointsPermm;
            p2.Y = 172 * pointsPermm;
            gfx.DrawString("INBETALNING/GIRERING AVI Nr 1", Times11, Brushes.Black, p2, left);
            topLeftY = 176.55F * pointsPermm;
            topLeftX = 13 * pointsPermm;
            width = 186.27F * pointsPermm;
            height = 72.7F * pointsPermm;
            gfx.DrawLine(Pens.Black, topLeftX, topLeftY, topLeftX + width, topLeftY);
            gfx.DrawLine(Pens.Black, topLeftX, topLeftY + height, topLeftX + width, topLeftY + height);
            gfx.DrawLine(Pens.Black, topLeftX, topLeftY, topLeftX, topLeftY + height);
            gfx.DrawLine(Pens.Black, topLeftX + width, topLeftY, topLeftX + width, topLeftY + height);
            gfx.DrawLine(Pens.Black, topLeftX, topLeftY + height / 2, topLeftX + width, topLeftY + height / 2);
            gfx.DrawLine(Pens.Black, topLeftX + width / 2, topLeftY + height / 2, topLeftX + width / 2, topLeftY + height);

            p1.Y = 221.47F * pointsPermm;
            p1.X = 111.81F * pointsPermm;
            gfx.DrawString("Folktetten", Times11Bold, Brushes.Black, p1, left);
            p1.Y += 4 * pointsPermm;
            gfx.DrawString("Philip Jönsson", Times11, Brushes.Black, p1, left);
            p1.Y += 4 * pointsPermm;
            gfx.DrawString("Skarpskyttevägen 22G Lgh 1201", Times11, Brushes.Black, p1, left);
            p1.Y += 4 * pointsPermm;
            gfx.DrawString("226 42 Lund", Times11, Brushes.Black, p1, left);

            PointF top = new PointF(120.76F * pointsPermm, 60.51F * pointsPermm);
            PointF bottom = new PointF(20.44F * pointsPermm, 221.47F * pointsPermm);
            string[] lines = invoice.Address.Split('\n');
            Font font = Times11Bold;
            foreach (string line in lines)
            {
                gfx.DrawString(line, font, Brushes.Black, top, left);
                gfx.DrawString(line, font, Brushes.Black, bottom, left);
                bottom.Y += 4 * pointsPermm;
                top.Y += 4 * pointsPermm;
                if (font.Bold)
                {
                    font = Times11;
                }
            }

            Brush brush = new SolidBrush(Color.FromArgb(208, 206, 206));
            topLeftX = 14.5F * pointsPermm;
            topLeftY = 257.92F * pointsPermm;
            width = 181.74F * pointsPermm;
            height = 14.44F * pointsPermm;
            gfx.FillRectangle(brush, topLeftX, topLeftY, width, height);
            topLeftX += 35.15F * pointsPermm;
            topLeftY += height / 3;
            width = 52 * pointsPermm;
            height /= 3;
            gfx.FillRectangle(Brushes.White, topLeftX, topLeftY, width, height);
            p1.X = topLeftX + width / 2;
            p1.Y = topLeftY;
            gfx.DrawString(getAmountString(invoice.Sum), Times11Bold, Brushes.Black, p1, center);
            topLeftX += width + 4.78F * pointsPermm;
            gfx.FillRectangle(Brushes.White, topLeftX, topLeftY, width, height);
            p1.X = topLeftX + width / 2;
            gfx.DrawString("666-4791", Times11Bold, Brushes.Black, p1, center);
        }

        private string getAmountString(double amount)
        {
            string result = "";
            result += (int)(amount);
            int dec = (int)Math.Round((Math.Round(amount, 2) - (int)(amount)) * 100);
            if (dec < 10)
            {
                result += ",0" + dec;
            }
            else
            {
                result += "," + dec;
            }
            return result + " kr";
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(PrintPage);
            printDialog1.Document = printDocument1;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
