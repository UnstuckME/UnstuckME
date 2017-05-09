using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualBasic.FileIO;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.SubWindows
{
    public enum Course { CourseName, CourseCode, CourseNumber, CourseDescItems };

    /// <summary>
    /// Importing and Creating CSV/TSV code implementation
    /// </summary>
    public partial class ImportClassesViaFile : Window
    {
        public static string[] CourseNames = { "Course Name", "Course Code", "Course Number" };
        List<UserClass> m_parsedWithWarnings = new List<UserClass>();

        public ImportClassesViaFile()
        {
            InitializeComponent();
        }        

        private void buttonCreateCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                Filter = "Comma Separated Values|*.csv",
                Title = "Save CSV File Template"
            };
            saveDialog.ShowDialog();
            
            if (saveDialog.FileName != string.Empty) // Make sure they dont try and save to empty directory
            { 
                FileStream fs = (FileStream)saveDialog.OpenFile();
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
            Close();
        }

        private async void buttonUploadCSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = ".csv|*.csv|.tsv|*tsv",
                Multiselect = true
            };

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
                            csvParser.SetDelimiters(",", "\t");
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
                                if (fields != null)
                                {
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
                                        if (string.IsNullOrEmpty(fields[i]))
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
                                            m_parsedWithWarnings.Add(new UserClass(courseName, courseCode, courseNumber));
                                            warningGenerated = true;
                                        }
                                        if (courseName.Length > 100)
                                            StackPanelWarningsErrors.Children.Add(GenerateWarning("Course Name \"" + courseName + "\" is too long", csvParser.LineNumber));
                                        if(courseCode.Length >= 5)
                                            StackPanelWarningsErrors.Children.Add(GenerateWarning("Course Code \"" + courseCode + "\" is too long", csvParser.LineNumber));
                                        if (Convert.ToInt16(courseNumber) <= 0)
                                            StackPanelWarningsErrors.Children.Add(GenerateWarning("The Course Number {" + courseNumber + "} is less than or equal to zero", csvParser.LineNumber));
                                        if (warningGenerated != true)
                                        {
                                            //This is were it need to get sent to the server
                                            bool addedSuccessfully = UnstuckME.Server.AddClass(new UserClass(courseName, courseCode, courseNumber)); 
                                            if (addedSuccessfully == false)
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
            catch (Exception ex)
            {
                UnstuckMEMessageBox error = new UnstuckMEMessageBox(UnstuckMEBox.OK, ex.Message, "CSV Upload Failure", UnstuckMEBoxImage.Warning);
                error.ShowDialog();
            }
        }

        void UpdateTextBoxAsync(long lineNumber)
        {
            Dispatcher.Invoke(() =>
            {
                textBlockCurrentLine.Text = "Reading line " + lineNumber;
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
                    foreach (UserClass newClass in m_parsedWithWarnings)
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
            TextBlock warnTextBox = new TextBlock()
            {
                Foreground = new SolidColorBrush(Colors.Yellow),
                Text = "WARNING: on line[" + lineNumber + "] " + errMsg,
                TextWrapping = TextWrapping.Wrap
            };
            return warnTextBox;
        }

        TextBlock GenerateError(string[] fields, string errorMsg, long linenumber = -1)
        {
            string errorTxt = "ERROR: ";
            if (linenumber != -1)
                errorTxt += "on line [" + linenumber + "] Unable to add";
            else
                errorTxt += "Unable to add the course containing:";

            for (int i=0; i < (int)Course.CourseDescItems; i++)
                errorTxt += " {" + fields[i] + "}";
            errorTxt += " " + errorMsg;

            TextBlock errorTextBlock = new TextBlock()
            {
                Text = errorTxt,
                Foreground = new SolidColorBrush(Colors.Red),
                TextWrapping = TextWrapping.Wrap
            };
            return errorTextBlock;
        }

        TextBlock GenerateError(string[] fields, long lineNumber)
        {
            string errorStmt = "ERROR: on line[" + lineNumber + "] ";

            bool foundError = false;
            for (int i =0; i < (int)Course.CourseDescItems; i++)
            {
                if (string.IsNullOrEmpty(fields[i]))
                {
                    if (foundError)
                        errorStmt += "and ";
                    errorStmt += CourseNames[i] + " Does not have a value ";
                    foundError = true;
                }
            }

            TextBlock errorTextBlock = new TextBlock()
            {
                Text = errorStmt,
                Foreground = new SolidColorBrush(Colors.Red),
                TextWrapping = TextWrapping.Wrap
            };
            return errorTextBlock;
        }
    }
}
