using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// Syntax Check class checks syntax of each and every commands
    /// </summary>
    public class SyntaxCheck
    {
        private string error_msg = "";
        private bool syntax_status = true, if_found = false, while_found = false, method_found = false, endMethod_found = false;
        private string[] keyword = { "rectangle", "moveto", "triangle", "drawto", "circle", "fill", "pen", "if", "while", "method", "color", "endif", "endloop", "endmethod", "mymethod()", "color"};
        private string[] flash_col = { "redgreen", "blueyellow", "blackwhite" };
        private int initial_error_line;
        private Var var = Var.Instance;
        private string[] commands;
        public static int while_index = 0, endloop_index = 0;
        /// <summary>
        /// Storing user input commands in an string array
        /// </summary>
        /// <param name="txt">String array that contains user input commands</param>
        public void set_commands(string[] txt)
        {
            commands = txt;
        }
        /// <summary>
        /// It checks syntax of all the user input commands and stores error if encountered based on the line number and returns true if no error is encountered else returns false
        /// </summary>
        /// <param name="sr">Object of StringReader that contains all the user input commands from the command window so that we could perform syntax check of the commands one line at a time</param>
        /// <returns></returns>
        public bool check_Syntax(StringReader sr)
        {
            initial_error_line = 0;
            int count = 0;
            string line;
            //Reading all the contents of the 'sr' object and ending while loop when null is encountered
            while ((line = sr.ReadLine()) != null)
            {
                count++;
                string[] command = line.Split(' ', ',');
                //If user entered command doesnot exist in our array of commands then it checks if it is a variable declaration
                if (!keyword.Contains(command[0].ToLower()))
                {
                    try
                    {
                        if (Regex.IsMatch(command[0], @"^[a-zA-Z]+$") && command[1].Equals("="))
                        {
                            string[] split_syntax = command[2].Split('+', '-', '/', '*');
                            if (split_syntax.Length == 1)
                            {
                                if (command[2].All(char.IsDigit))
                                {
                                    int var_val = Convert.ToInt32(command[2]);
                                    var.Add_Update_var_val(var_val, command[0]);
                                }
                                else if (Regex.IsMatch(command[2], @"^[a-zA-Z]+$"))
                                {
                                    if (!var.Check_for_var(command[2]))
                                    {
                                        set_initial_error_line(count);
                                        syntax_status = false;
                                        set_ErrorMessage("Line" + count + ": Variable " + command[2] + " not declared");
                                    }
                                    else
                                    {
                                        int var_val = var.get_variable_value(command[2]);
                                        var.Add_Update_var_val(var_val, command[0]);
                                    }
                                }
                            }
                            else if (split_syntax.Length == 2)
                            {
                                //Checking if the splitted is a number or a string
                                if (split_syntax[0].All(char.IsDigit) && split_syntax[1].All(char.IsDigit))
                                {
                                    int var_val1 = Convert.ToInt32(split_syntax[0]);
                                    int var_val2 = Convert.ToInt32(split_syntax[1]);
                                    int result = var.Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                    var.Add_Update_var_val(result, command[0]);
                                }
                                //Check if the new variable is assigned a value by performing arithmetic opertaion between an Integer value and previously defined variable.
                                else if (split_syntax[0].All(char.IsDigit) && Regex.IsMatch(split_syntax[1], @"^[a-zA-Z]+$"))
                                {
                                    int var_val1 = Convert.ToInt32(split_syntax[0]);
                                    int var_val2;
                                    if (var.Check_for_var(split_syntax[1]))
                                    {
                                        var_val2 = var.get_variable_value(split_syntax[1]);
                                        int result = var.Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                        var.Add_Update_var_val(result, command[0]);
                                    }
                                    else
                                    {
                                        set_initial_error_line(count);
                                        syntax_status = false;
                                        set_ErrorMessage("Line" + count + ": Variable " + split_syntax[1] + " not declared");
                                    }
                                }
                                else if (split_syntax[1].All(char.IsDigit) && Regex.IsMatch(split_syntax[0], @"^[a-zA-Z]+$"))
                                {
                                    int var_val1;
                                    int var_val2 = Convert.ToInt32(split_syntax[1]);
                                    if (var.Check_for_var(split_syntax[0]))
                                    {
                                        var_val1 = var.get_variable_value(split_syntax[0]);
                                        int result = var.Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                        var.Add_Update_var_val(result, command[0]);
                                    }
                                    else
                                    {
                                        set_initial_error_line(count);
                                        syntax_status = false;
                                        set_ErrorMessage("Line" + count + ": Variable " + split_syntax[0] + " not declared");
                                    }
                                }
                                else if (Regex.IsMatch(split_syntax[0], @"^[a-zA-Z]+$") && Regex.IsMatch(split_syntax[1], @"^[a-zA-Z]+$"))
                                {
                                    int var_val1;
                                    int var_val2;
                                    if (var.Check_for_var(split_syntax[0]) && var.Check_for_var(split_syntax[1]))
                                    {
                                        var_val1 = var.get_variable_value(split_syntax[0]);
                                        var_val2 = var.get_variable_value(split_syntax[1]);
                                        int result = var.Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                        var.Add_Update_var_val(result, command[0]);
                                    }
                                    else
                                    {
                                        set_initial_error_line(count);
                                        syntax_status = false;
                                        set_ErrorMessage("Line" + count + ": Variable " + split_syntax[0] + " or " + split_syntax[1] + " not declared");
                                    }
                                }
                            }
                            else
                            {
                                set_initial_error_line(count);
                                syntax_status = false;
                                set_ErrorMessage("Line" + count + " : Incorrect Syntax for variable decleration");
                            }
                        }
                        else
                        {
                            set_initial_error_line(count);
                            set_ErrorMessage("Line" + count + " : Incorrect Syntax");
                            syntax_status = false;
                        }
                    }
                    catch(IndexOutOfRangeException)
                    {
                        syntax_status=false;
                        set_ErrorMessage("Line" + count + " Invalid command");
                    }
                }
                else if ( (command[0].ToLower().Equals(keyword[0])) || (command[0].ToLower().Equals(keyword[1])) || (command[0].ToLower().Equals(keyword[2])) || (command[0].ToLower().Equals(keyword[3])) )
                {
                    if(command.Length == 3)
                    {
                        if (!command[1].All(char.IsDigit) || !command[2].All(char.IsDigit))
                        {
                            if (command[1].All(char.IsDigit))
                            {
                                if (!var.Check_for_var(command[2]))
                                {
                                    set_initial_error_line(count);
                                    syntax_status =false;
                                    set_ErrorMessage("Line" + count + " : Parameter " + command[2] + " doesnt contain any value");
                                }
                            }
                            else if (command[2].All(char.IsDigit))
                            {
                                if (!var.Check_for_var(command[1]))
                                {
                                    set_initial_error_line(count);
                                    syntax_status = false;
                                    set_ErrorMessage("Line" + count + " : Parameter " + command[1] + " doesnt contain any value");
                                }
                            }
                            else if (!var.Check_for_var(command[1]) && !var.Check_for_var(command[2]))
                            {
                                set_initial_error_line(count);
                                syntax_status = false;
                                set_ErrorMessage("Line" + count + " : Parameter doesnt contain any value");
                            }
                        }
                        
                    }
                    else
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Incomplete Command, there must be two parameters");
                    }
                    /*This commented out block of code is for when we encounter an unknown command*/

                    //set_initial_error_line(count);
                    //syntax_status = false;
                    //set_ErrorMessage("Line" + count + ": Invalid Command_Syntax");
                }

                else if (command[0].ToLower().Equals(keyword[4]))
                {
                    if (command.Length==2)
                    {
                        if (!command[1].All(char.IsDigit))
                        {
                            if (!var.Check_for_var(command[1]))
                            {
                                set_initial_error_line(count);
                                syntax_status = false;
                                set_ErrorMessage("Line" + count + " : Parameter " + command[1] + " does not" +
                                    " contain any value");
                            }
                        }
                    }
                    else
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Improper Command, there must be one parameter");
                    }
                }
                else if (command[0].ToLower().Equals(keyword[5]))
                {
                    if(command.Length==2)
                    {
                        if (!command[1].ToLower().Equals("on") && !command[1].ToLower().Equals("off"))
                        {
                            set_initial_error_line(count);
                            syntax_status = false;
                            set_ErrorMessage("Line" + count + ": Fill command can only have \"on\" or \"off\" as an arguement");
                        }
                    }
                    else
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Improper Command, there must be \"on\" or \"off\" as an arguement");
                    }
                }
                else if(command[0].ToLower().Equals(keyword[6]))
                {
                    if (command.Length == 2)
                    {
                        if (!command[1].ToLower().Equals("red") && !command[1].ToLower().Equals("green") && !command[1].ToLower().Equals("blue") && !command[1].ToLower().Equals("yellow"))
                        {
                            set_initial_error_line(count);
                            syntax_status = false;
                            set_ErrorMessage("Line" + count + ": Please select Red, Green, Yellow or Blue for pen color");
                        }
                    }
                    else
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Improper Command, there must be Red, Green, Yellow or Blue as an arguement");
                    }
                }
                else if(command[0].ToLower().Equals(keyword[7]))
                {
                    If_Statement if_check = new If_Statement();
                    if(command.Length == 8 || command.Length == 7)
                    {
                        if (!if_check.check_SingleLine_if(line))
                        {
                            set_initial_error_line(count);
                            syntax_status = false;
                            set_ErrorMessage("Line" + count + ": Syntax of Single line if is incorrect");
                        }
                        else if(if_check.get_condition_status())
                        {
                            String[] cmd = Regex.Split(line, "then");
                            if(cmd.Length == 2)
                            {
                                StringReader str_re = new StringReader(cmd[1].Trim());
                                SyntaxCheck command_if = new SyntaxCheck();
                                if (!command_if.check_Syntax(str_re))
                                {
                                    set_initial_error_line(count);
                                    syntax_status =false;
                                    set_ErrorMessage("Line" + count + ": Invalid drawing command");
                                }
                            }
                            else
                            {
                                set_initial_error_line(count);
                                syntax_status = false;
                                set_ErrorMessage("Line" + count + ": Missing \'then\' keyword");
                            }
                        }
                    }
                    else if(command.Length==4)
                    {
                        if_found = true;
                        if(!if_check.check_MultiLine_if(commands))
                        {
                            set_initial_error_line(count);
                            syntax_status = false;
                            set_ErrorMessage("Line" + count + ": Syntax of Multi-line if is incorrect");
                        }
                    }
                    else
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Incomplete syntax for \'if\' command");
                    }
                }
                else if (command[0].ToLower().Equals(keyword[8]))
                {
                    Looping looping = new Looping();
                    if(command.Length == 4)
                    {
                        while_found = true;
                        if (!looping.loop_check(commands))
                        {
                            set_initial_error_line(count);
                            syntax_status = false;
                            set_ErrorMessage("Line" + count + ": Syntax of While loop is incorrect");
                        }
                        else
                        {
                            while_index = looping.get_index();
                            endloop_index = looping.get_endloop_index();
                        }
                    }
                }
                else if (command[0].ToLower().Equals(keyword[9]))
                {
                    Methods mthd = new Methods();
                    if(command.Length==2)
                    {
                        method_found = true;
                        if(!mthd.noPara_method(commands))
                        {
                            set_initial_error_line(count);
                            syntax_status = false;
                            set_ErrorMessage("Line" + count + ": Syntax of Non-Parameterized method is incorrect");
                        }
                    }
                    else
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Missing name of the method");
                    }
                }
                else if (command[0].ToLower().Equals(keyword[15]))
                {
                    if(command.Length==2)
                    {
                        if (!flash_col.Contains(command[1].ToLower()))
                        {
                            set_initial_error_line(count);
                            syntax_status = false;
                            set_ErrorMessage("Line" + count + ": Invalid color choice for flashing");
                        }
                    }
                    else
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Missing color for flashing");
                    }
                }
                else if(command[0].ToLower().Equals("endif") && if_found == false)
                {
                    set_initial_error_line(count);
                    syntax_status = false;
                    set_ErrorMessage("Line" + count + ": Expecting \'If\' command before \'endif\'");
                }
                else if (command[0].ToLower().Equals("endmethod") && method_found == false)
                {
                    set_initial_error_line(count);
                    syntax_status = false;
                    set_ErrorMessage("Line" + count + ": Expecting \'If\' command before \'endif\'");
                }
                else if(command[0].ToLower().Equals("endloop") && while_found == false)
                {
                    set_initial_error_line(count);
                    syntax_status = false;
                    set_ErrorMessage("Line" + count + ": Expecting \'While\' command before \'endloop\'");
                }
                else if(String.IsNullOrEmpty(error_msg))
                {
                    error_msg = "";
                }
                else
                {
                    if (!command[0].ToLower().Equals("endif") && !command[0].ToLower().Equals("endloop") && !command[0].ToLower().Equals("endmethod"))
                    {
                        set_initial_error_line(count);
                        syntax_status = false;
                        set_ErrorMessage("Line" + count + ": Invalid syntax_syntax_check");
                    }
                }
            }
            return syntax_status;
        }
        /// <summary>
        /// Method to set error messege each time and invalid or incomplete syntax is encountered
        /// </summary>
        /// <param name="error"></param>
        public void set_ErrorMessage(string error)
        {
            if (String.IsNullOrEmpty(error_msg) || error_msg.Equals(error))
            {
                error_msg = error;
            }
            else
            {
                error_msg = error_msg + Environment.NewLine + error;
            }
        }
        public void set_error_msg(string error)
        {
            error_msg = error;
        }
        public string get_ErrorMessage()
        {
            return error_msg;
        }
        public int get_ErrorLine()
        {
            return initial_error_line;
        }
        /// <summary>
        /// This method is used to set the syntax status of a command or a block of commands. The input parameter is a boolean value, which indicates whether the syntax is correct or not. If the value passed is true, it means the syntax is correct and if the value passed is false, it means the syntax is incorrect. This method is typically used in a syntax checker class to keep track of the syntax status of the commands being processed.
        /// </summary>
        /// <param name="status"></param>
        public void set_Syntax_status(bool status)
        {
            syntax_status = status;
        }
        /// <summary>
        /// This method sets the value of the private variable "initial_error_line" to the value passed as the parameter "count" only if the current value of "initial_error_line" is equal to 0. This method is used to store the line number of the first error encountered in a set of commands.
        /// </summary>
        /// <param name="count"></param>
        public void set_initial_error_line(int count)
        {
            if (initial_error_line == 0)
            {
                initial_error_line = count;
            }
        }
    }
}
