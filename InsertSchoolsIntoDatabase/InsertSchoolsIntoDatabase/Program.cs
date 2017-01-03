using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertSchoolsIntoDatabase
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// initialize data if no data exists
			List<int> imageIDList = DatabaseOperations.GetImageIDList();
			if (imageIDList.Count == 0)
			{
				DirectoryInfo Media = new DirectoryInfo(DatabaseOperations.IMAGES_PATH);

				// upload images
				for (int imageID = 0; imageID < Media.GetFiles().Length; imageID++)
				{
					FileInfo item = Media.GetFiles()[imageID];
					DatabaseOperations.WriteImage(item.Name);
				}
			}

			Application.Run(new Form1());
		}
	}
}
