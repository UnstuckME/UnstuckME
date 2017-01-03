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
			this.SchoolIDComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.filenameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.EmailCredentialText = new System.Windows.Forms.TextBox();
			this.SchoolNameText = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SchoolName = new System.Windows.Forms.Label();
			this.imagePictureBox = new System.Windows.Forms.PictureBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.uploadButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// SchoolIDComboBox
			// 
			this.SchoolIDComboBox.FormattingEnabled = true;
			this.SchoolIDComboBox.Location = new System.Drawing.Point(89, 27);
			this.SchoolIDComboBox.MaxDropDownItems = 50;
			this.SchoolIDComboBox.Name = "SchoolIDComboBox";
			this.SchoolIDComboBox.Size = new System.Drawing.Size(175, 21);
			this.SchoolIDComboBox.TabIndex = 1;
			this.SchoolIDComboBox.Text = "1";
			this.SchoolIDComboBox.SelectedIndexChanged += new System.EventHandler(this.imageIDComboBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "School ID:";
			// 
			// filenameTextBox
			// 
			this.filenameTextBox.Location = new System.Drawing.Point(101, 32);
			this.filenameTextBox.Name = "filenameTextBox";
			this.filenameTextBox.Size = new System.Drawing.Size(214, 20);
			this.filenameTextBox.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.DimGray;
			this.groupBox1.Controls.Add(this.EmailCredentialText);
			this.groupBox1.Controls.Add(this.SchoolNameText);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.SchoolName);
			this.groupBox1.Controls.Add(this.imagePictureBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.SchoolIDComboBox);
			this.groupBox1.Location = new System.Drawing.Point(14, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(676, 424);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Display images";
			// 
			// EmailCredentialText
			// 
			this.EmailCredentialText.Location = new System.Drawing.Point(89, 147);
			this.EmailCredentialText.Name = "EmailCredentialText";
			this.EmailCredentialText.ReadOnly = true;
			this.EmailCredentialText.Size = new System.Drawing.Size(175, 20);
			this.EmailCredentialText.TabIndex = 9;
			// 
			// SchoolNameText
			// 
			this.SchoolNameText.Location = new System.Drawing.Point(89, 87);
			this.SchoolNameText.Name = "SchoolNameText";
			this.SchoolNameText.ReadOnly = true;
			this.SchoolNameText.Size = new System.Drawing.Size(175, 20);
			this.SchoolNameText.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 144);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(62, 26);
			this.label6.TabIndex = 6;
			this.label6.Text = "Email\nCredentials:";
			// 
			// SchoolName
			// 
			this.SchoolName.AutoSize = true;
			this.SchoolName.Location = new System.Drawing.Point(14, 91);
			this.SchoolName.Name = "SchoolName";
			this.SchoolName.Size = new System.Drawing.Size(74, 13);
			this.SchoolName.TabIndex = 5;
			this.SchoolName.Text = "School Name:";
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
			this.groupBox2.BackColor = System.Drawing.Color.DimGray;
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.uploadButton);
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
			this.button1.BackColor = System.Drawing.Color.Firebrick;
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
			this.uploadButton.BackColor = System.Drawing.Color.DarkCyan;
			this.uploadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.uploadButton.Location = new System.Drawing.Point(26, 79);
			this.uploadButton.Name = "uploadButton";
			this.uploadButton.Size = new System.Drawing.Size(89, 34);
			this.uploadButton.TabIndex = 8;
			this.uploadButton.Text = "Upload";
			this.uploadButton.UseVisualStyleBackColor = false;
			this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Filename:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LightSlateGray;
			this.ClientSize = new System.Drawing.Size(702, 593);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "Product Image Manager";
			this.Load += new System.EventHandler(this.Image_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox SchoolIDComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox filenameTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button uploadButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox imagePictureBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label SchoolName;
		private System.Windows.Forms.TextBox EmailCredentialText;
		private System.Windows.Forms.TextBox SchoolNameText;
		private System.Windows.Forms.Button button1;
	}
}