using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace InsertSchoolsIntoDatabase
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void LoadImageIDComboBox()
		{
			// load the combo box
			List<int> imageIDList = DatabaseOperations.GetImageIDList();
			foreach (int i in imageIDList)
				SchoolIDComboBox.Items.Add(i);
		}

		private void Image_Load(object sender, EventArgs e)
		{
			this.LoadImageIDComboBox();
			imageIDComboBox_SelectedIndexChanged(sender, e);
			label4.Text = "Note: The image file must be in the " + InsertSchoolsIntoDatabase.DatabaseOperations.IMAGES_PATH + " directory";
		}

		private void imageIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int imageID = Convert.ToInt32(SchoolIDComboBox.Text);

				// read image bytes from the database and display in picture box
				byte[] imageByteArray = DatabaseOperations.ReadImage(imageID);
				MemoryStream ms = new MemoryStream(imageByteArray);
				imagePictureBox.Image = System.Drawing.Image.FromStream(ms);

				DatabaseOperations.ReadExtraData(imageID, SchoolNameText, EmailCredentialText);
				ms.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error");
			}
		}

		private void uploadButton_Click(object sender, EventArgs e)
		{
			try
			{
				string filename = filenameTextBox.Text;
				DatabaseOperations.WriteImage(filename);
				MessageBox.Show(this, "Image upload was successful!",
					"Upload Confirmation");

				// refresh combo box
				SchoolIDComboBox.Items.Clear();
				this.LoadImageIDComboBox();

			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error");
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.Application.Exit();
		}
	}
}
