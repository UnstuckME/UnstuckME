namespace InsertSchoolsIntoDatabase
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.imageIDComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.filenameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.LoaderFileText = new System.Windows.Forms.TextBox();
			this.UserIdText = new System.Windows.Forms.TextBox();
			this.ProdIdText = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.imagePictureBox = new System.Windows.Forms.PictureBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.uploadButton = new System.Windows.Forms.Button();
			this.productIDTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageIDComboBox
			// 
			this.imageIDComboBox.FormattingEnabled = true;
			this.imageIDComboBox.Location = new System.Drawing.Point(89, 27);
			this.imageIDComboBox.MaxDropDownItems = 50;
			this.imageIDComboBox.Name = "imageIDComboBox";
			this.imageIDComboBox.Size = new System.Drawing.Size(175, 21);
			this.imageIDComboBox.TabIndex = 1;
			this.imageIDComboBox.Text = "1";
			this.imageIDComboBox.SelectedIndexChanged += new System.EventHandler(this.imageIDComboBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Image ID:";
			// 
			// filenameTextBox
			// 
			this.filenameTextBox.Location = new System.Drawing.Point(101, 19);
			this.filenameTextBox.Name = "filenameTextBox";
			this.filenameTextBox.Size = new System.Drawing.Size(214, 20);
			this.filenameTextBox.TabIndex = 4;
			this.filenameTextBox.Text = "pf02_cover.jpg";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.groupBox1.Controls.Add(this.pictureBox1);
			this.groupBox1.Controls.Add(this.LoaderFileText);
			this.groupBox1.Controls.Add(this.UserIdText);
			this.groupBox1.Controls.Add(this.ProdIdText);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.imagePictureBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.imageIDComboBox);
			this.groupBox1.Location = new System.Drawing.Point(14, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(676, 424);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Display images";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(17, 290);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 128);
			this.pictureBox1.TabIndex = 11;
			this.pictureBox1.TabStop = false;
			// 
			// LoaderFileText
			// 
			this.LoaderFileText.Location = new System.Drawing.Point(89, 147);
			this.LoaderFileText.Multiline = true;
			this.LoaderFileText.Name = "LoaderFileText";
			this.LoaderFileText.ReadOnly = true;
			this.LoaderFileText.Size = new System.Drawing.Size(175, 70);
			this.LoaderFileText.TabIndex = 10;
			// 
			// UserIdText
			// 
			this.UserIdText.Location = new System.Drawing.Point(89, 107);
			this.UserIdText.Name = "UserIdText";
			this.UserIdText.ReadOnly = true;
			this.UserIdText.Size = new System.Drawing.Size(175, 20);
			this.UserIdText.TabIndex = 9;
			// 
			// ProdIdText
			// 
			this.ProdIdText.Location = new System.Drawing.Point(89, 67);
			this.ProdIdText.Name = "ProdIdText";
			this.ProdIdText.ReadOnly = true;
			this.ProdIdText.Size = new System.Drawing.Size(175, 20);
			this.ProdIdText.TabIndex = 8;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 150);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Loaded File:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 110);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "User ID:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 70);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(61, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Product ID:";
			// 
			// imagePictureBox
			// 
			this.imagePictureBox.Location = new System.Drawing.Point(270, 18);
			this.imagePictureBox.Name = "imagePictureBox";
			this.imagePictureBox.Size = new System.Drawing.Size(400, 400);
			this.imagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imagePictureBox.TabIndex = 4;
			this.imagePictureBox.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.uploadButton);
			this.groupBox2.Controls.Add(this.productIDTextBox);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.filenameTextBox);
			this.groupBox2.Location = new System.Drawing.Point(14, 442);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(676, 140);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Upload new images";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Yellow;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(563, 79);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(107, 50);
			this.button1.TabIndex = 10;
			this.button1.Text = "Exit";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 116);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(255, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Note: The image file must be in the C:\\temp directory";
			// 
			// uploadButton
			// 
			this.uploadButton.BackColor = System.Drawing.Color.Lime;
			this.uploadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.uploadButton.Location = new System.Drawing.Point(26, 79);
			this.uploadButton.Name = "uploadButton";
			this.uploadButton.Size = new System.Drawing.Size(89, 34);
			this.uploadButton.TabIndex = 8;
			this.uploadButton.Text = "Upload";
			this.uploadButton.UseVisualStyleBackColor = false;
			this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
			// 
			// productIDTextBox
			// 
			this.productIDTextBox.Location = new System.Drawing.Point(101, 48);
			this.productIDTextBox.Name = "productIDTextBox";
			this.productIDTextBox.Size = new System.Drawing.Size(214, 20);
			this.productIDTextBox.TabIndex = 7;
			this.productIDTextBox.Text = "4";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Product ID:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Filename:";
			// 
			// ImageManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gold;
			this.ClientSize = new System.Drawing.Size(702, 593);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ImageManagerForm";
			this.Text = "Product Image Manager";
			this.Load += new System.EventHandler(this.Image_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ComboBox imageIDComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox filenameTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button uploadButton;
		private System.Windows.Forms.TextBox productIDTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox imagePictureBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox LoaderFileText;
		private System.Windows.Forms.TextBox UserIdText;
		private System.Windows.Forms.TextBox ProdIdText;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
	}
}

