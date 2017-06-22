namespace FolkBok
{
    partial class AddVoucher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.verNumLabel = new System.Windows.Forms.Label();
            this.removeRowButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.addRowButton = new System.Windows.Forms.Button();
            this.kreditBox1 = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.accountBox1 = new System.Windows.Forms.ComboBox();
            this.accountBox2 = new System.Windows.Forms.ComboBox();
            this.kreditBox2 = new System.Windows.Forms.TextBox();
            this.debetBox2 = new System.Windows.Forms.TextBox();
            this.debetBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.folkBokDataSet = new FolkBok.FolkBokDataSet();
            this.folkBokDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lineLabel = new System.Windows.Forms.Label();
            this.sumDescriptionLabel = new System.Windows.Forms.Label();
            this.kreditSumLabel = new System.Windows.Forms.Label();
            this.debetSumLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.folkBokDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.folkBokDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FolkBok.Properties.Resources.FolkLogo;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(334, 138);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(529, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 49);
            this.label1.TabIndex = 2;
            this.label1.Text = "Verifikation nr.";
            // 
            // verNumLabel
            // 
            this.verNumLabel.AutoSize = true;
            this.verNumLabel.Font = new System.Drawing.Font("Times New Roman", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verNumLabel.Location = new System.Drawing.Point(813, 9);
            this.verNumLabel.Name = "verNumLabel";
            this.verNumLabel.Size = new System.Drawing.Size(44, 49);
            this.verNumLabel.TabIndex = 3;
            this.verNumLabel.Text = "0";
            // 
            // removeRowButton
            // 
            this.removeRowButton.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.removeRowButton.Location = new System.Drawing.Point(118, 377);
            this.removeRowButton.Name = "removeRowButton";
            this.removeRowButton.Size = new System.Drawing.Size(116, 30);
            this.removeRowButton.TabIndex = 39;
            this.removeRowButton.Text = "Remove row";
            this.removeRowButton.UseVisualStyleBackColor = true;
            this.removeRowButton.Click += new System.EventHandler(this.RemoveRowButton_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(877, 526);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 30);
            this.button3.TabIndex = 38;
            this.button3.Text = "Print preview";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1007, 526);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 37;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(796, 526);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 30);
            this.saveButton.TabIndex = 36;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // addRowButton
            // 
            this.addRowButton.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.addRowButton.Location = new System.Drawing.Point(12, 377);
            this.addRowButton.Name = "addRowButton";
            this.addRowButton.Size = new System.Drawing.Size(100, 30);
            this.addRowButton.TabIndex = 35;
            this.addRowButton.Text = "Add row";
            this.addRowButton.UseVisualStyleBackColor = true;
            this.addRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // kreditBox1
            // 
            this.kreditBox1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kreditBox1.Location = new System.Drawing.Point(895, 307);
            this.kreditBox1.Name = "kreditBox1";
            this.kreditBox1.Size = new System.Drawing.Size(187, 29);
            this.kreditBox1.TabIndex = 34;
            this.kreditBox1.Text = "0";
            this.kreditBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.kreditBox1.TextChanged += new System.EventHandler(this.updateKreditSumLabel);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 275);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(679, 29);
            this.descriptionTextBox.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 21);
            this.label2.TabIndex = 40;
            this.label2.Text = "Beskrivning";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(697, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 21);
            this.label3.TabIndex = 41;
            this.label3.Text = "Debet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(901, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 21);
            this.label4.TabIndex = 42;
            this.label4.Text = "Kredit";
            // 
            // accountBox1
            // 
            this.accountBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountBox1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountBox1.FormattingEnabled = true;
            this.accountBox1.Location = new System.Drawing.Point(12, 307);
            this.accountBox1.Name = "accountBox1";
            this.accountBox1.Size = new System.Drawing.Size(679, 29);
            this.accountBox1.TabIndex = 43;
            // 
            // accountBox2
            // 
            this.accountBox2.BackColor = System.Drawing.SystemColors.Window;
            this.accountBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountBox2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountBox2.FormattingEnabled = true;
            this.accountBox2.Location = new System.Drawing.Point(12, 342);
            this.accountBox2.Name = "accountBox2";
            this.accountBox2.Size = new System.Drawing.Size(679, 29);
            this.accountBox2.TabIndex = 46;
            // 
            // kreditBox2
            // 
            this.kreditBox2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kreditBox2.Location = new System.Drawing.Point(895, 342);
            this.kreditBox2.Name = "kreditBox2";
            this.kreditBox2.Size = new System.Drawing.Size(187, 29);
            this.kreditBox2.TabIndex = 45;
            this.kreditBox2.Text = "0";
            this.kreditBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // debetBox2
            // 
            this.debetBox2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debetBox2.Location = new System.Drawing.Point(697, 342);
            this.debetBox2.Name = "debetBox2";
            this.debetBox2.Size = new System.Drawing.Size(192, 29);
            this.debetBox2.TabIndex = 48;
            this.debetBox2.Text = "0";
            this.debetBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.debetBox2.TextChanged += new System.EventHandler(this.updateDebetSumLabel);
            // 
            // debetBox1
            // 
            this.debetBox1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debetBox1.Location = new System.Drawing.Point(697, 307);
            this.debetBox1.Name = "debetBox1";
            this.debetBox1.Size = new System.Drawing.Size(192, 29);
            this.debetBox1.TabIndex = 47;
            this.debetBox1.Text = "0";
            this.debetBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.debetBox1.TextChanged += new System.EventHandler(this.updateDebetSumLabel);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(670, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 49);
            this.label5.TabIndex = 49;
            this.label5.Text = "Datum";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Times New Roman", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(813, 61);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(231, 57);
            this.dateTimePicker1.TabIndex = 50;
            // 
            // folkBokDataSet
            // 
            this.folkBokDataSet.DataSetName = "FolkBokDataSet";
            this.folkBokDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // folkBokDataSetBindingSource
            // 
            this.folkBokDataSetBindingSource.DataSource = this.folkBokDataSet;
            this.folkBokDataSetBindingSource.Position = 0;
            // 
            // lineLabel
            // 
            this.lineLabel.BackColor = System.Drawing.Color.Black;
            this.lineLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineLabel.Location = new System.Drawing.Point(582, 377);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(500, 2);
            this.lineLabel.TabIndex = 51;
            this.lineLabel.Text = "label12";
            // 
            // sumDescriptionLabel
            // 
            this.sumDescriptionLabel.AutoSize = true;
            this.sumDescriptionLabel.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sumDescriptionLabel.Location = new System.Drawing.Point(625, 386);
            this.sumDescriptionLabel.Name = "sumDescriptionLabel";
            this.sumDescriptionLabel.Size = new System.Drawing.Size(71, 22);
            this.sumDescriptionLabel.TabIndex = 52;
            this.sumDescriptionLabel.Text = "Summa";
            // 
            // kreditSumLabel
            // 
            this.kreditSumLabel.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kreditSumLabel.Location = new System.Drawing.Point(895, 381);
            this.kreditSumLabel.Name = "kreditSumLabel";
            this.kreditSumLabel.Size = new System.Drawing.Size(187, 22);
            this.kreditSumLabel.TabIndex = 53;
            this.kreditSumLabel.Text = "0 kr";
            this.kreditSumLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // debetSumLabel
            // 
            this.debetSumLabel.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debetSumLabel.Location = new System.Drawing.Point(702, 386);
            this.debetSumLabel.Name = "debetSumLabel";
            this.debetSumLabel.Size = new System.Drawing.Size(187, 22);
            this.debetSumLabel.TabIndex = 54;
            this.debetSumLabel.Text = "0 kr";
            this.debetSumLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // AddVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1094, 568);
            this.Controls.Add(this.debetSumLabel);
            this.Controls.Add(this.kreditSumLabel);
            this.Controls.Add(this.sumDescriptionLabel);
            this.Controls.Add(this.lineLabel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.debetBox2);
            this.Controls.Add(this.debetBox1);
            this.Controls.Add(this.accountBox2);
            this.Controls.Add(this.kreditBox2);
            this.Controls.Add(this.accountBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.removeRowButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.addRowButton);
            this.Controls.Add(this.kreditBox1);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.verNumLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AddVoucher";
            this.Text = "AddVoucher";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.folkBokDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.folkBokDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label verNumLabel;
        private System.Windows.Forms.Button removeRowButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button addRowButton;
        private System.Windows.Forms.TextBox kreditBox1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox accountBox1;
        private System.Windows.Forms.ComboBox accountBox2;
        private System.Windows.Forms.TextBox kreditBox2;
        private System.Windows.Forms.TextBox debetBox2;
        private System.Windows.Forms.TextBox debetBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private FolkBokDataSet folkBokDataSet;
        private System.Windows.Forms.BindingSource folkBokDataSetBindingSource;
        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.Label sumDescriptionLabel;
        private System.Windows.Forms.Label kreditSumLabel;
        private System.Windows.Forms.Label debetSumLabel;
    }
}