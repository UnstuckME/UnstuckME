using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;
using UnstuckME_Classes;

public enum Course { CourseName, CourseCode, CourseNumber, CourseDescItems};

namespace UnstuckMEUserGUI.SubWindows
{


    /// <summary>
    /// Importing and Creating CSV/TSV code implementation
    /// </summary>

    public partial class ImportClassesViaFile : Window
    {
        public static string[] CourseNames = { "Course Name", "Course Code", "Course Number" };
        List<DBClass> m_parsedWithWarnings = new List<DBClass>();

        public ImportClassesViaFile()
        {
            InitializeComponent();
        }

        

        private void buttonCreateCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Comma Separated Values|*.csv";
            saveDialog.Title = "Save CSV File Template";
            saveDialog.ShowDialog();

            
            if (saveDialog.FileName != "") // Make sure they dont try and save to empty directory
            { 
                System.IO.FileStream fs = (System.IO.FileStream)saveDialog.OpenFile();
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("$$$, $$$ All cells containing triple ($)  will be ignored, $$$");
                    sw.WriteLine("$$$, $$$ To include commas for a data set wrap it in quotes (ie: \"Doe. Jon\" -> Doe. Jon), $$$");
                    sw.WriteLine("$$$, $$$ To include quotes put double quotes around the element (ie: Jon \"\"THE DOE\"\" ->  Jon \"THE DOE\"), $$$");
                    sw.WriteLine("$$$, $$$ EXAMPLE:,  $$$");
                    sw.WriteLine("$$$ Introduction to Accounting,$$$ ACC,$$$ 101");
                    sw.Write("[Course Name], [Course Code], [Course Number]\n\n");
                    fs.Flush();
                }
                fs.Close();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void buttonUploadCSV_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = ".csv|*.csv|.tsv|*tsv";
            fileDialog.Multiselect = true;

            try
            {
           
                if (fileDialog.ShowDialog() == true)
                {
                    string[] arrAllFiles = fileDialog.FileNames;


                    foreach(string path in arrAllFiles)
                    {
                        using (TextFieldParser csvParser = new TextFieldParser(path))
                        {
                            csvParser.CommentTokens = new string[] { "$$$"};
                            csvParser.SetDelimiters(new string[] { "," , "\t" });
                            csvParser.HasFieldsEnclosedInQuotes = true;
                            csvParser.TrimWhiteSpace = true;

                            // Skip the row with the column names
                            csvParser.ReadLine();

                            textBlockCurrentLine.Visibility = Visibility.Visible;

                            while (!csvParser.EndOfData)
                            {
                                await Task.Factory.StartNew(() => UpdateTextBoxAsync(csvParser.LineNumber));
                                // Read current line fields, pointer moves to the next line.
                                string[] fields = csvParser.ReadFields();
                                string courseName = fields[0];
                                string courseCode = fields[1];
                                string courseNumber = fields[2];

                                bool skipLine = false;
                                bool warningGenerated = false;
                                bool[] emptyRow = { false, false, false };

                                for (int i = 0; i < (int)Course.CourseDescItems && skipLine == false; i++)
                                {
                                    if (fields.Contains("[" + CourseNames[i] + "]"))
                                        skipLine = true;
                                    if (string.IsNullOrEmpty(fields[i]) == true)
                                        emptyRow[i] = true;
                                }

                                if (emptyRow.Contains(true))
                                {
                                    skipLine = true;
                                    StackPanelWarningsErrors.Children.Add(GenerateError(fields, csvParser.LineNumber));
                                }

                                if (skipLine == false)
                                {

                                    if(courseName.Length > 100 || courseCode.Length >= 5 || Convert.ToInt16(courseNumber) <= 0)
                                    {
                                        m_parsedWithWarnings.Add(new DBClass(courseName, courseCode, courseNumber));
                                        warningGenerated = true;
                                    }
                                    if (courseName.Length > 100)
                                    {
                                        StackPanelWarningsErrors.Children.Add(GenerateWarning("Course Name \"" + courseName + "\" is too long", csvParser.LineNumber));
                                    }
                                    if(courseCode.Length >= 5)
                                    {
                                        StackPanelWarningsErrors.Children.Add(GenerateWarning("Course Code \"" + courseCode + "\" is too long", csvParser.LineNumber));
                                    }
                                    if (Convert.ToInt16(courseNumber) <= 0) 
                                    {
                                        StackPanelWarningsErrors.Children.Add(GenerateWarning("The Course Number {" + courseNumber + "} is less than or equal to zero", csvParser.LineNumber));
                                    }

                                    if(warningGenerated != true)
                                    {
                                        //This is were it need to get sent to the server
                                        bool addedSuccessfully = UnstuckME.Server.AddClass(new DBClass(courseName, courseCode, courseNumber)); 
                                        if(addedSuccessfully == false)
                                        {
                                            StackPanelWarningsErrors.Children.Add(GenerateError(fields,"Database rejected insertion of class (perhaps it already exits?)", csvParser.LineNumber));
                                        }
                                    }
                                }
                            }
                           
                            CheckForWarnings();
                        }
                    }   
                
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        void UpdateTextBoxAsync(long lineNumber)
        {
            this.Dispatcher.Invoke(() =>
            {
                textBlockCurrentLine.Text = "Reading line " + lineNumber.ToString();
            });
        }

        void CheckForWarnings()
        {
            if (m_parsedWithWarnings.Count != 0)
            {
                UnstuckMEMessageBox msgBox =  new UnstuckMEMessageBox(UnstuckMEBox.YesNo,"Your CSV/TSV file generated some errors. Would you like our UnstuckME Servers to attempt to add the classes anyways? \n(NOTE: This may generate some unexpected values for your classes)", "Warning: Some of your classes may not be correct", UnstuckMEBoxImage.Warning);
                bool ? msgResponse = msgBox.ShowDialog();
                if (msgResponse == true)
                {
                    foreach (DBClass newClass in m_parsedWithWarnings)
                    {
                        bool addedSuccessfully = UnstuckME.Server.AddClass(newClass);
                        if (addedSuccessfully == false)
                        {
                            string[] fields = { newClass.CourseName, newClass.CourseCode, newClass.CourseNumber.ToString() };
                            StackPanelWarningsErrors.Children.Add(GenerateError(fields, "Database rejected insertion of class (perhaps it already exits?)"));
                        }
                    }
                }
            }
        }

        TextBlock GenerateWarning(string errMsg, long lineNumber)
        {
            TextBlock warnTextBox = new TextBlock();
            warnTextBox.Foreground = new SolidColorBrush(Colors.Yellow);
            warnTextBox.Text = "WARNING: on line[" + lineNumber.ToString() + "] " + errMsg;
            warnTextBox.TextWrapping = TextWrapping.Wrap;
            return warnTextBox;
        }


        TextBlock GenerateError(string[] fields, string errorMsg, long linenumber = -1)
        {
            string errorTxt = "ERROR: ";
            if (linenumber != -1)
                errorTxt += "on line [" + linenumber.ToString() + "] Unable to add";
            else
                errorTxt += "Unable to add the course containing:";

            for (int i=0; i < (int)Course.CourseDescItems; i++)
            {
                errorTxt += " {" + fields[i] + "}";
            }
            errorTxt += " " + errorMsg;

            TextBlock errorTextBlock = new TextBlock();
            errorTextBlock.Text = errorTxt;
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            errorTextBlock.TextWrapping = TextWrapping.Wrap;
            return errorTextBlock;
        }

        TextBlock GenerateError(string[] fields, long lineNumber)
        {
            string errorStmt = "ERROR: on line[" + lineNumber.ToString() + "] ";

            bool foundError = false;
            for (int i =0; i < (int)Course.CourseDescItems; i++)
            {
                if (string.IsNullOrEmpty(fields[i]) == true)
                {
                    if (foundError == true)
                        errorStmt += "and ";
                    errorStmt += CourseNames[i] + " Does not have a value ";
                    foundError = true;
                }

            }

            TextBlock errorTextBlock = new TextBlock();
            errorTextBlock.Text = errorStmt;
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            errorTextBlock.TextWrapping = TextWrapping.Wrap;
            return errorTextBlock;
        }
    }
}
