using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolkBok
{
    public partial class AddVoucher : Form
    {
        private List<ComboBox> accountBoxes;
        private List<TextBox> debetBoxes;
        private List<TextBox> kreditBoxes;

        private const int up = -1;
        private const int down = 1;

        public AddVoucher()
        {
            InitializeComponent();

            accountBoxes = new List<ComboBox>();
            debetBoxes = new List<TextBox>();
            kreditBoxes = new List<TextBox>();
            PopulateLists();

            verNumLabel.Text = 1.ToString();
        }

        private void PopulateLists()
        {
            accountBoxes.Add(accountBox1);
            accountBoxes.Add(accountBox2);
            debetBoxes.Add(debetBox1);
            debetBoxes.Add(debetBox2);
            kreditBoxes.Add(kreditBox1);
            kreditBoxes.Add(kreditBox2);
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            MoveButton(addRowButton, down);
            MoveButton(removeRowButton, down);
            MoveLabel(sumDescriptionLabel, down);
            MoveLabel(kreditSumLabel, down);
            MoveLabel(debetSumLabel, down);
            MoveLabel(lineLabel, down);

            ComboBox CB = new ComboBox();
            Point p = accountBoxes.Last().Location;
            p.Y += 35;
            CB.Location = p;
            CB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CB.DropDownStyle = ComboBoxStyle.DropDownList;
            CB.FormattingEnabled = true;
            CB.Name = "accountBox" + accountBoxes.Count;
            CB.Size = new Size(679, 29);
            //CB.TabIndex = 21;
            CB.Items.AddRange(getAccounts());
            Controls.Add(CB);
            accountBoxes.Add(CB);

            TextBox TB = new TextBox();
            p = debetBoxes.Last().Location;
            p.Y += 35;
            TB.Location = p;
            TB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB.TextAlign = HorizontalAlignment.Right;
            TB.Name = "debetBox" + debetBoxes.Count;
            TB.Size = new Size(194, 29);
            TB.Text = "0";
            //TB.TabIndex = 32;
            TB.TextChanged += new EventHandler(updateDebetSumLabel);
            Controls.Add(TB);
            debetBoxes.Add(TB);

            TB = new TextBox();
            p = kreditBoxes.Last().Location;
            p.Y += 35;
            TB.Location = p;
            TB.Font = new Font("Times New Roman", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TB.TextAlign = HorizontalAlignment.Right;
            TB.Name = "kreditBox" + kreditBoxes.Count;
            TB.Size = new Size(187, 29);
            TB.Text = "0";
            //TB.TabIndex = 21;
            TB.TextChanged += new EventHandler(updateKreditSumLabel);
            Controls.Add(TB);
            kreditBoxes.Add(TB);
        }

        private object[] getAccounts()
        {
            return new object[] { "Hej", "på", "dig" };
        }

        private void RemoveRowButton_Click(object sender, EventArgs e)
        {
            if (accountBoxes.Count > 2)
            {
                ComboBox CB = accountBoxes.Last();
                accountBoxes.Remove(CB);
                Controls.Remove(CB);
                TextBox TB = debetBoxes.Last();
                debetBoxes.Remove(TB);
                Controls.Remove(TB);
                TB = kreditBoxes.Last();
                kreditBoxes.Remove(TB);
                Controls.Remove(TB);

                MoveButton(addRowButton, up);
                MoveButton(removeRowButton, up);
                MoveLabel(sumDescriptionLabel, up);
                MoveLabel(kreditSumLabel, up);
                MoveLabel(debetSumLabel, up);
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

        private void updateDebetSumLabel(object sender, EventArgs e)
        {
            double debetSum = 0;
            foreach (TextBox box in debetBoxes)
            {
                try
                {
                    if (box.Text.Length > 0)
                    {
                        debetSum += Convert.ToDouble(box.Text.Replace('.', ','));
                    }
                }
                catch
                {
                    MessageBox.Show("Vänligen fyll i en summa.");
                    box.Text = box.Text.Substring(0,box.Text.Length-1);
                    box.SelectionStart = box.Text.Length;
                }
            }
            debetSumLabel.Text = debetSum.ToString() + " kr";
        }

        private void updateKreditSumLabel(object sender, EventArgs e)
        {
            double kreditSum = 0;
            foreach (TextBox box in kreditBoxes)
            {
                try
                {
                    if (box.Text.Length > 0)
                    {
                        kreditSum += Convert.ToDouble(box.Text.Replace('.', ','));
                    }
                }
                catch
                {
                    MessageBox.Show("Vänligen fyll i en summa.");
                    box.Text = box.Text.Substring(0, box.Text.Length - 1);
                    box.SelectionStart = box.Text.Length;
                }
            }
            kreditSumLabel.Text = kreditSum.ToString() + " kr";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Voucher voucher = new Voucher(1, descriptionTextBox.Text, DateTime.Now, dateTimePicker1.Value);
            for (int i=0;i<accountBoxes.Count;i++)
            {
                voucher.AddLine(new VoucherLine(new Account(1234, "test"), Convert.ToDouble(debetBoxes[i].Text.Replace('.',',')), Convert.ToDouble(kreditBoxes[i].Text.Replace('.', ','))));
            }
            VoucherPDF pdf = new VoucherPDF(descriptionTextBox.Text, voucher);
        }
    }
}
