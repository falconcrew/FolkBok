﻿using System;
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
        private List<TextBox> dateTextBoxes;
        private List<TextBox> amountTextBoxes;

        private const int up = -1;
        private const int down = 1;
        
        public AddInvoice()
        {
            InitializeComponent();
            importInvoiceSettings();
            setupLabels();
            descriptionTextBoxes = new List<TextBox>();
            descriptionTextBoxes.Add(descriptionTextBox);
            dateTextBoxes = new List<TextBox>();
            dateTextBoxes.Add(dateTextBox);
            amountTextBoxes = new List<TextBox>();
            amountTextBoxes.Add(amountTextBox);
        }

        private void setupLabels()
        {
            invoiceNumberLabel.Text = invoiceNumber.ToString();
            invoiceDateLabel.Text = invoiceDate.ToShortDateString();
            paymentTermLabel.Text = paymentTerm.ToString();
            dueDateLabel.Text = dueDate.ToShortDateString();
            penaltyInterestLabel.Text = penaltyInterest.ToString() + "%";
        }

        private void importInvoiceSettings()
        {
            StreamReader sr = new StreamReader(@"C:\Users\almar\Documents\FolkBok\InvoiceSettings.txt");
            invoiceNumber = Convert.ToInt32(sr.ReadLine());
            paymentTerm = Convert.ToInt32(sr.ReadLine());
            penaltyInterest = Convert.ToDouble(sr.ReadLine());
            sr.Close();
            invoiceDate = DateTime.Now;
            dueDate = invoiceDate.AddDays(paymentTerm);
        }

        private void addRowButton_Click(object sender, EventArgs e)
        {
            moveButton(addRowButton,down);
            moveButton(removeRowButton,down);
            moveLabel(sumDescriptionLabel,down);
            moveLabel(sumLabel,down);
            moveLabel(lineLabel,down);

            TextBox TB = new TextBox();
            Point p = descriptionTextBoxes.Last().Location;
            p.Y += 29;
            TB.Location = p;
            TB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB.Name = "descriptionTextBox" + descriptionTextBoxes.Count;
            TB.Size = new Size(679, 29);
            TB.TabIndex = 21;
            this.Controls.Add(TB);
            descriptionTextBoxes.Add(TB);

            TB = new TextBox();
            p = dateTextBoxes.Last().Location;
            p.Y += 29;
            TB.Location = p;
            TB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB.Name = "dateTextBox" + dateTextBoxes.Count;
            TB.Size = new Size(195, 29);
            TB.TabIndex = 21;
            this.Controls.Add(TB);
            dateTextBoxes.Add(TB);

            TB = new TextBox();
            p = amountTextBoxes.Last().Location;
            p.Y += 29;
            TB.Location = p;
            TB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB.Name = "amountTextBox" + amountTextBoxes.Count;
            TB.Size = new Size(187, 29);
            TB.TabIndex = 21;
            TB.TextChanged += new EventHandler(updateSumLabel);
            this.Controls.Add(TB);
            amountTextBoxes.Add(TB);
        }

        private void removeRowButton_Click(object sender, EventArgs e)
        {
            TextBox TB = descriptionTextBoxes.Last();
            descriptionTextBoxes.Remove(TB);
            Controls.Remove(TB);
            TB = dateTextBoxes.Last();
            dateTextBoxes.Remove(TB);
            Controls.Remove(TB);
            TB = amountTextBoxes.Last();
            amountTextBoxes.Remove(TB);
            Controls.Remove(TB);

            moveButton(addRowButton, up);
            moveButton(removeRowButton, up);
            moveLabel(sumDescriptionLabel, up);
            moveLabel(sumLabel, up);
        }

        private void moveButton(Button button, int direction)
        {
            Point p = button.Location;
            p.Y += direction * 29;
            button.Location = p;
        }

        private void moveLabel(Label label, int direction)
        {
            Point p = label.Location;
            p.Y += direction * 29;
            label.Location = p;
        }

        private void updateSumLabel(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (TextBox textBox in amountTextBoxes)
            {
                sum += Convert.ToInt32(textBox.Text);
            }
            sumLabel.Text = sum.ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string address = addressTextBox.Text;
            string ourReference = ourReferenceTextBox.Text;
            string yourReference = yourReferenceTextBox.Text;
            Invoice invoice = new Invoice(address, invoiceDate, 1, ourReference, yourReference);
            for (int i = 0; i < descriptionTextBoxes.Count; i++)
            {

            }
        }
    }
}
