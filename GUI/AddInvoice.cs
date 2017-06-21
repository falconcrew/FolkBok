using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolkBok
{
    public partial class AddInvoice : Form
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
        
        public AddInvoice()
        {
            InitializeComponent();
            ImportInvoiceSettings();
            SetupLabels();
            descriptionTextBoxes = new List<TextBox>();
            descriptionTextBoxes.Add(descriptionTextBox);
            dateBoxes = new List<DateTimePicker>();
            dateBoxes.Add(dateTimePicker1);
            amountTextBoxes = new List<TextBox>();
            amountTextBoxes.Add(amountTextBox);
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
            Invoice invoice = new Invoice(address, invoiceDate, ourReference, yourReference);
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
    }
}
