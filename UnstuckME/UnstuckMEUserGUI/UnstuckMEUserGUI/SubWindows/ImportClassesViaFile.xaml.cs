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
    /// Interaction logic for ImportClassesViaFile.xaml
    /// </summary<

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

        private void buttonUploadCSV_Click(object sender, RoutedEventArgs e)
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
                            csvParser.SetDelimiters(new string[] { "," });
                            csvParser.HasFieldsEnclosedInQuotes = true;
                            csvParser.TrimWhiteSpace = true;

                            // Skip the row with the column names
                            csvParser.ReadLine();

                            textBlockCurrentLine.Visibility = Visibility.Visible;

                            while (!csvParser.EndOfData)
                            {
                                
                                textBlockCurrentLine.Text = "Reading line " + csvParser.LineNumber.ToString();

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
                                    skipLine = true;

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

                                    bool errorGenerated = false;
                                    for (int i= 0; i < (int)Course.CourseDescItems; i++)
                                    {
                                        if(string.IsNullOrEmpty(fields[i])== true)
                                        {
                                            errorGenerated = true;
                                            i = (int)Course.CourseDescItems;
                                        }
                                    }

                                    if (errorGenerated == true)
                                        StackPanelWarningsErrors.Children.Add(GenerateError(fields, csvParser.LineNumber));
                                    else if(warningGenerated != true)
                                    {
                                        //This is were it need to get sent to the server
                                        bool addedSuccessfully = UnstuckME.Server.AddClass(new DBClass(courseName, courseCode, courseNumber)); 
                                        if(addedSuccessfully == false)
                                        {
                                            GenerateError(fields, csvParser.LineNumber, "Database rejected insertion of class (perhaps it already exits?)");
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

        void CheckForWarnings()
        {
            if (m_parsedWithWarnings.Count != 0)
            {
                UnstuckMEMessageBox msgBox =  new UnstuckMEMessageBox(UnstuckMEBox.YesNo,"Error", "potrato", UnstuckMEBoxImage.Warning);
                bool ? msgResponse = msgBox.ShowDialog();
                if (msgResponse == true)
                    MessageBox.Show("true");
                else if (msgResponse == false)
                    MessageBox.Show("false");
                else
                    MessageBox.Show("NULL");
            }
        }

        TextBlock GenerateWarning(string errMsg, long lineNumber)
        {
            TextBlock warnTextBox = new TextBlock();
            warnTextBox.Foreground = new SolidColorBrush(Colors.Red);
            warnTextBox.Text = "WARNING: on line[" + lineNumber.ToString() + "] " + errMsg;
            //warnTextBox.FontSize = Stretch.Uniform;
            warnTextBox.TextWrapping = TextWrapping.Wrap;
            return warnTextBox;
        }


        TextBlock GenerateError(string[] fields, long linenumber, string errorMsg)
        {
            string errorTxt = "ERROR: on line [" + linenumber.ToString() + "] Unable to add";
            for(int i=0; i < (int)Course.CourseDescItems; i++)
            {
                errorTxt += " {" + fields[i] + "}";
            }
            errorTxt += " " + errorMsg;

            TextBlock errorTextBlock = new TextBlock();
            errorTextBlock.Text = errorTxt;
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Yellow);
            //errorTextBlock.FontSize = 9;
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
            errorTextBlock.Foreground = new SolidColorBrush(Colors.Yellow);
            //errorTextBlock.FontSize = 9;
            errorTextBlock.TextWrapping = TextWrapping.Wrap;
            return errorTextBlock;
        }
    }
}
