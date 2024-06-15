using Assignment1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment1
{
    /// <summary>
    ///A partial class called Form1 that inherits from the Form class. The class has several private variables such as a Graphics object named g, a CommandParser object named cmd_prs, a boolean syntx_status, a SyntaxCheck object named synt_check, an array of strings named text, and a static list of strings named mth_cmd. It also has two integers named method_index and endMethod_index.
    /// </summary>
    public partial class Form1 : Form
    {
        private Graphics g;
        private CommandParser cmd_prs;
        private bool syntx_status = false;
        SyntaxCheck synt_check = new SyntaxCheck();
        private string[] text;
        public static List<string> mth_cmd = new List<string>();
        private int method_index = 0, endMethod_index = 0;
        /// <summary>
        /// Default constructor to initialize all the components of the form also initializes object of Graphics class and CommandParser class is initialized by passing the same initialized graphics object
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //Initializing graphic class's object
            g = pic_Output_Window.CreateGraphics();
            cmd_prs = new CommandParser(g);
        }
        /// <summary>
        /// Getter method which returns Object of graphics class that is initialized by the default constructor
        /// </summary>
        /// <returns>object of graphics class i.e g</returns>
        public Graphics get_g()
        {
            return g;
        }
        /// <summary>
        /// Creating a run button click event that splits the user entered commands based on their line and        sending each line of command to CommandParser classs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Run_Click(object sender, EventArgs e)
        {
            //A variable to keep track of no. of lines that gets executed so, that we can display line number when an error is encountered while parsing the command
            int count_line = 0;
            if (txt_Action_Cmd.Text.ToLower().Equals("run"))
            {
                set_text();
                synt_check.set_commands(get_text());
                synt_check.set_Syntax_status(true);
                //Using StringReader class to read and compile one line of command at a time
                using (StringReader sr = new StringReader(txt_Command_Window.Text))
                {
                    string line;
                    try
                    {
                        cmd_prs.set_Commands(text);
                        synt_check.set_commands(text);
                        if (synt_check.check_Syntax(get_Command()))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                //Increasing the value of count_line before we parse the command because count_line was initialized with zero
                                count_line++;
                                string[] splt = line.Split(' ');
                                if (splt[0].Trim().ToLower().Contains("method"))
                                {
                                    this.method_index = Methods.method_index;
                                    this.endMethod_index = Methods.endmethod_index;
                                }
                                else if (endMethod_index != 0)
                                {
                                    if (count_line == method_index + 1 && count_line <= endMethod_index + 1)
                                    {
                                        mth_cmd.Add(line);
                                        continue;
                                    }
                                    else if(count_line<method_index+1 || count_line>endMethod_index+1)
                                    {
                                        cmd_prs.split_commands(line);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    cmd_prs.split_commands(line);
                                }
                            }
                            //Reading commands one line at a time

                            //Showing successfull compilation messege with Custom text color if no exception is encountered
                            txt_Error_Window.ForeColor = Color.LightSeaGreen;
                            txt_Error_Window.Text = "Command compiled and executed successfully";
                        }
                        //Executing the right commands that were written before any incorrect commands were encountered
                        else if(synt_check.get_ErrorLine()>1)
                        {
                            synt_check.set_error_msg("");
                            
                            while ((line =sr.ReadLine())!=null && count_line<(synt_check.get_ErrorLine()-1))
                            {
                                count_line++;
                                cmd_prs.split_commands(line);
                            }
                            
                            txt_Error_Window.ForeColor = Color.FromArgb(197, 66, 69);
                            //txt_Error_Window.Text = synt_check.get_ErrorMessage();
                        }

                        else
                        {
                            txt_Error_Window.ForeColor = Color.FromArgb(197, 66, 69);
                            txt_Error_Window.Text = "Line 1: Invalid command_Form1";
                        }
                    }
                    catch(Exception excep)
                    {
                        //Showing error message in red color if any error is encountered while parsing the command
                        txt_Error_Window.ForeColor = Color.FromArgb(197, 66, 69);
                        txt_Error_Window.Text = "Line "+count_line+" :"+excep.Message;
                    }
                }
            }
            //Clearing all the drawn shapes from the picturebox when clear command is executed
            else if (txt_Action_Cmd.Text.ToLower().Equals("clear"))
            {
                pic_Output_Window.ImageLocation = null;
                txt_Error_Window.ForeColor = Color.LightSeaGreen;
                txt_Error_Window.Text = "All shapes cleared from the picturebox";
                synt_check.set_ErrorMessage("");
            }
            //Reseting all the attributes as well as clearing picturebox when reset command is executed
            else if (txt_Action_Cmd.Text.ToLower().Equals("reset"))
            {
                pic_Output_Window.ImageLocation = null;
                txt_Error_Window.Text = "";
                //Calling reset_all method to reset every attributs to its default value 
                cmd_prs.reset_all();
                txt_Error_Window.ForeColor = Color.LightSeaGreen;
                txt_Error_Window.Text = "Pen Color set to Black"+Environment.NewLine+"Cursor set to 0,0"+Environment.NewLine+"Fill turned off"+Environment.NewLine+"Picturebox cleared";
                synt_check.set_ErrorMessage("");
            }
            //Showing error messege when an unknown action command is encountered
            else
            {
                txt_Error_Window.ForeColor = Color.FromArgb(197, 66, 69);
                txt_Error_Window.Text = "Invalid Action Command Please Enter:"+Environment.NewLine+"Clear, Reset or Run";
            }
        }
        private void pic_Output_Window_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /*when save option is clicked it saves the commands written in command window*/
        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDrawingCommand = new SaveFileDialog();
            if (saveDrawingCommand.ShowDialog() == DialogResult.OK)
            {
                using (Stream fileName = File.Open(saveDrawingCommand.FileName, FileMode.CreateNew))
                using (StreamWriter saveFile = new StreamWriter(fileName))
                {
                    saveFile.Write(txt_Command_Window.Text);
                }
            }
        }

        private void MenuItemLoad_Click(object sender, EventArgs e)
        {
            //uses stream inbuilt class to load file from the system
            Stream streamLoadFile;
            //opens the dialouge for choosing file that we want to open
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                try
                {
                    if ((streamLoadFile = openFile.OpenFile()) != null)
                    {
                        //user chooses the filename
                        string filename = openFile.FileName;
                        //reads all the content from choosen file
                        string filetext = File.ReadAllText(filename);
                        //loaded in our commandParser text box
                        txt_Command_Window.Text = filetext;
                    }
                }
                catch (FileNotFoundException)
                {
                    //if user selects the file that doesnt exists
                    MessageBox.Show("Error", "Cannot find the asked file");
                }
            }
        }
        private void btn_syntax_check_Click(object sender, EventArgs e)
        {
            set_text();
            synt_check.set_commands(get_text());
            synt_check.set_error_msg("");
            synt_check.set_Syntax_status(true);
            if(synt_check.check_Syntax(get_Command()))
            {
                txt_Error_Window.ForeColor = Color.LightSeaGreen;
                txt_Error_Window.Text = "Command compiled successfully";
            }
            else
            {
                txt_Error_Window.ForeColor = Color.FromArgb(197, 66, 69);
                txt_Error_Window.Text = synt_check.get_ErrorMessage();
            }
        }
        /// <summary>
        /// This method is used to get the text of the txt_Command_Window control as a StringReader object.
        /// </summary>
        /// <returns>
        /// Returns a StringReader object containing the text of the txt_Command_Window control.
        /// </returns>
        public StringReader get_Command()
        {
            StringReader sr = new StringReader(txt_Command_Window.Text);
            return sr;
        }
        /// <summary>
        /// This method is used to get the value of the text variable.
        /// </summary>
        /// <returns>
        /// Returns the string array 'text'
        /// </returns>
        public string[] get_text()
        {
            return text;
        }
        /// <summary>
        /// This method is used to set the value of the text variable by splitting the text of the txt_Command_Window control by newline characters.
        /// </summary>
        public void set_text()
        {
            text = txt_Command_Window.Text.Split('\n');
        }
    }
}
