using Assignment1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment1
{
    /// <summary>
    /// CommandParser class helps us to parse one line of the commands entered by the user and take actions accordingly if user enters valid command else it shows the appropriate error message.
    /// </summary>
    public class CommandParser
    {
        //Creating the necessary variables as private
        private int x_cod = 0, y_cod = 0, rad = 0, side1 = 0, side2 = 0; 
        private string color, flashcolor;
        private Color pen_colr;
        private bool fill = false, flash;
        private Graphics g;
        private Var var = Var.Instance;
        private string[] commands;
        private int while_count = 0;
        private Looping lp = new Looping();
        //private IDictionary<string, int> variable = new Dictionary<string, int>();

        /// <summary>
        /// It sets the default color of the pen to black and assigns object of Graphics class inside CommandParser class as the graphics object sent as parameter to this method.
        /// </summary>
        /// <param name="g"></param>
        public CommandParser(Graphics g)
        {
            this.g = g;
            this.pen_colr = Color.Black;
        }
        /// <summary>
        /// It resets all the drawing attributes to default i.e. pen color black, cursor position at (0,0), fill turned off as well as clears the picturebox and all the declared variables
        /// </summary>
        public void reset_all()
        {
            x_cod = 0; y_cod = 0; rad = 0; side1 = 0; side2 = 0;
            fill = false;
            this.pen_colr = Color.Black;
            Var.Instance.Clear_dict();
        }
        /// <summary>
        /// It splits the single line command into command and their respective parameters if, it encounters one using a single white space and ',' as delemiters
        /// </summary>
        /// <param name="line"></param>
        /// <returns>True if the entered commmand is valid else throws an exception</returns>
        /// <exception cref="InvalidCommandException"></exception>
        public bool split_commands(string line)
        {
            //Splitting single line command into its respective command and parameter using white space and ',' as delemiters
            string[] command = line.Split(' ', ',');
            if (command[0].ToLower().Equals("moveto"))
            {
                //Checking the command[] array length because moveto command is expected to have two parameters
                if (command.Length == 3)
                {
                    //Using try catch block because there might be IndexOutOfRangeException if the user didn't enter only one or none parameter
                    try
                    {
                        string x = command[1];
                        string y = command[2];
                        //Using try catch block because there might be FormatException if user enters non integer parameters
                        try
                        {
                            if(!x.All(char.IsDigit))
                            {
                                if(var.Check_for_var(x))
                                {
                                    x_cod = var.get_variable_value(x);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            if(!y.All(char.IsDigit))
                            {
                                if(var.Check_for_var(y))
                                {
                                    y_cod = var.get_variable_value(y);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            else if(y.All(char.IsDigit) && x.All(char.IsDigit))
                            {
                                x_cod = Convert.ToInt32(x);
                                y_cod = Convert.ToInt32(y);
                            }
                        }
                        catch (FormatException)
                        {
                            //Throws the exception if there are parameters of wrong datatype
                            throw new InvalidCommandException("Invalid parameters, parameters must be integer");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Throws exception if there are less then required parameters in the command
                        throw new InvalidCommandException("Insufficient parameters, there must be two paramenters");

                    }
                }
                else
                {
                    //Throws exception if there are more then required parameters in the command
                    throw new InvalidCommandException("MoveTo command is expected to have only two parameters not more!!");
                }
            }
            else if (command[0].ToLower().Equals("color"))
            {
                if(command.Length == 2)
                {

                    if (command[1].ToLower().Equals("redgreen"))
                    {
                        flashcolor = "redgreen";
                        flash = true;
                    }
                    else if (command[1].ToLower().Equals("blueyellow"))
                    {
                        flashcolor = "blueyellow";
                        flash = true;
                    }
                    else if (command[1].ToLower().Equals("blackwhite"))
                    {
                        flashcolor = "blackwhite";
                        flash = true;
                    }
                    else
                    {
                        flash = false;
                        throw new InvalidCommandException("Invalid color for flashing");
                    }
                }
            }
            else if (command[0].ToLower().Equals("rectangle"))
            {
                if (command.Length == 3)
                {
                    try
                    {
                        string para1 = command[1];
                        string para2 = command[2];
                        //There might occur an exception if user enters datatype other than integer so it must be handeled
                        try
                        {
                            if (!para1.All(char.IsDigit))
                            {
                                if (var.Check_for_var(para1))
                                {
                                    side1 = var.get_variable_value(para1);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            if (!para2.All(char.IsDigit))
                            {
                                if (var.Check_for_var(para2))
                                {
                                    side2 = var.get_variable_value(para2);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            else if(para1.All(char.IsDigit) && para2.All(char.IsDigit))
                            {
                                side1 = Convert.ToInt32(para1);
                                side2 = Convert.ToInt32(para2);
                            }
                            ShapeFactory spf = new ShapeFactory();
                            /*A new ShapeFactory class object is created and upon calling it's getShape method 
                             passing the user entered command to draw shape it returns object corresponding to the particular shape*/
                            Shape shp = spf.getShape(command[0]);
                            //setting attributes to draw the rectangle shape by storing rectangle class object as shp
                            shp.set(pen_colr, flashcolor, fill, flash, x_cod, y_cod, side1, side2);
                            shp.draw(g);

                        }
                        catch (FormatException)
                        {
                            //Throws the exception if there are parameters of wrong datatype
                            throw new InvalidCommandException("Invalid parameters, parameters must be integer");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Throws the exception if there is Insufficient parameters in the command
                        throw new InvalidCommandException("Insufficient parameters, there must be two paramenters");
                    }
                }
                else
                {
                    //Throws exception if there are more then required parameters in the command
                    throw new InvalidCommandException("Rectangle command is expected to have only two parameters i.e. Height and Width");
                }
            }
            else if (command[0].ToLower().Equals("pen"))
            {
                /*Checking command[] array length so, only command having valid no. of parameters gets executed*/
                if (command.Length == 2)
                {
                    try
                    {
                        color = command[1];
                        //Setting pen color according to user's command
                        if (color.ToLower().Equals("red"))
                        {
                            pen_colr = Color.Red;
                            flash = false;
                        }
                        else if (color.ToLower().Equals("blue"))
                        {
                            pen_colr = Color.Blue;
                            flash = false;
                        }
                        else if (color.ToLower().Equals("yellow"))
                        {
                            pen_colr = Color.Yellow;
                            flash = false;
                        }
                        else if (color.ToLower().Equals("green"))
                        {
                            pen_colr = Color.Green;
                            flash = false;
                        }
                        else
                        {
                            //Throws and excxeption if the color selected by the user is unavailable
                            throw new InvalidCommandException("Please select Red, Green, Yellow or Blue for pen color");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Throws the exception if there is Insufficient parameters in the command
                        throw new InvalidCommandException("Please select color of the pen");
                    }
                }
                else
                {
                    //Throws exception if there are more then required parameters in the command
                    throw new InvalidCommandException("Pen command is expected to have only one parameters i.e. color of the pen");
                }
            }
            else if (command[0].ToLower().Equals("fill"))
            {
                /*Checking command[] array length so, only command having valid no. of parameters gets executed*/
                if (command.Length == 2)
                {
                    try
                    {
                        string fill_state = command[1];
                        //Changing the boolean value for fill according to the user's command
                        if (fill_state.ToLower().Equals("on"))
                        {
                            fill = true;

                        }
                        else if (fill_state.ToLower().Equals("off"))
                        {
                            fill = false;

                        }
                        else
                        {
                            //Throws an exception if invalid argument is enetered for fill command
                            throw new InvalidCommandException("Invalid command, Please select either on or off for fill");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Throws the exception if there is Insufficient parameters in the command
                        throw new InvalidCommandException("Incomplete command, Please select either on or off for fill");
                    }
                }
                else
                {
                    //Throws exception if there are more then required parameters in the command
                    throw new InvalidCommandException("Fill is expected to have only one parameters i.e. either \"on\" or \"off\"");
                }
            }
            else if (command[0].ToLower().Equals("drawto"))
            {
                /*Checking command[] array length so, only command having valid no. of parameters gets executed*/
                if (command.Length == 3)
                {
                    try
                    {
                        string x = command[1];
                        string y = command[2];
                        //Using try catch block incase user enters non Integer arguments as parameters a FormatException exception gets thrown 
                        try
                        {
                            if (!x.All(char.IsDigit))
                            {
                                if (var.Check_for_var(x))
                                {
                                    side1 = var.get_variable_value(x);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            if (!y.All(char.IsDigit))
                            {
                                if (var.Check_for_var(y))
                                {
                                    side2 = var.get_variable_value(y);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            else if(x.All(char.IsDigit) && y.All(char.IsDigit))
                            {
                                //Parsing the parameters as everything entered inside textbox is stored as string
                                side1 = Convert.ToInt32(x);
                                side2 = Convert.ToInt32(y);
                            }
                            DrawTo dt = new DrawTo();
                            dt.set(pen_colr, flashcolor, fill, flash,x_cod, y_cod, side1, side2);
                            dt.draw(g);
                        }
                        catch (FormatException)
                        {
                            //Throws the exception if there are parameters of wrong datatype
                            throw new InvalidCommandException("Invalid parameters, parameters must be integer");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Throws the exception if there is Insufficient parameters in the command
                        throw new InvalidCommandException("Insufficient parameters, there must be two paramenters");
                    }
                }
                else
                {
                    //Throws exception if there are more then required parameters in the command
                    throw new InvalidCommandException("DrawTo command is expected to have only two parameters not more!!");
                }
            }
            else if (command[0].ToLower().Equals("triangle"))
            {
                /*Checking command[] array length so, only command having valid no. of parameters gets executed*/
                if (command.Length == 3)
                {
                    try
                    {
                        string para1 = command[1];
                        string para2 = command[2];
                        //There might occur an exception if user enters datatype other than integer so it must be handeled
                        try
                        {
                            if (!para1.All(char.IsDigit))
                            {
                                if (var.Check_for_var(para1))
                                {
                                    side1 = var.get_variable_value(para1);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            if (!para2.All(char.IsDigit))
                            {
                                if (var.Check_for_var(para2))
                                {
                                    side2 = var.get_variable_value(para2);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            else if(para1.All(char.IsDigit) && para2.All(char.IsDigit))
                            {
                                side1 = Convert.ToInt32(para1);
                                side2 = Convert.ToInt32(para2);
                            }
                            ShapeFactory spf = new ShapeFactory();
                            Shape shp = spf.getShape(command[0]);
                            shp.set(pen_colr, flashcolor, fill, flash, x_cod, y_cod, side1, side2);
                            shp.draw(g);

                        }
                        catch (FormatException)
                        {
                            //Throws the exception if there are parameters of wrong datatype
                            throw new InvalidCommandException("Invalid parameters, parameters must be integer");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Throws the exception if there is Insufficient parameters in the command
                        throw new InvalidCommandException("Insufficient parameters, there must be two paramenters");
                    }
                }
                else
                {
                    //Throws exception if there are more then required parameters in the command
                    throw new InvalidCommandException("Triangle command is expected to have only two parameters i.e Base and Height");
                }
            }
            else if (command[0].ToLower().Equals("circle"))
            {
                /*Checking command[] array length so, only command having valid no. of parameters gets executed*/
                if (command.Length == 2)
                {
                    try
                    {
                        string rad = command[1];
                        //There might occur an exception if user enters datatype other than integer so it must be handeled
                        try
                        {
                            if (!rad.All(char.IsDigit))
                            {
                                if (var.Check_for_var(rad))
                                {
                                    this.rad = var.get_variable_value(rad);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Datatype");
                                }
                            }
                            else
                            {
                                this.rad = Convert.ToInt32(rad);
                            }
                            ShapeFactory spf = new ShapeFactory();
                            Shape shp = spf.getShape(command[0]);
                            shp.set(pen_colr, flashcolor, fill, flash, x_cod, y_cod, this.rad);
                            shp.draw(g);

                        }
                        catch (FormatException)
                        {
                            //Throws the exception if there are parameters of wrong datatype
                            throw new InvalidCommandException("Invalid parameters, parameter must be integer");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //Throws the exception if there is incomplete parameters in the command
                        throw new InvalidCommandException("Insufficient parameters, there must be one paramenters for radius");
                    }
                }
                else
                {
                    //Throws exception if there are more then required parameters in the command
                    throw new InvalidCommandException("Circle command is expected to have only one parameters as radius not more!!");
                }
            }
            else if (command[0].ToLower().Equals("endif"))
            {
                
            }
            else if (command[0].ToLower().Trim().Equals("endloop"))
            {

                if(get_while_count()<=3 && lp.get_condition_status()==true)
                {
                    looping_commands();
                }
                else
                {
                    while_count = 0;
                }
            }
            else if (command.Length == 3 && Regex.IsMatch(command[0], @"^[a-zA-Z]+$") && command[1].Equals("="))
            {
                var.Declare_var(line);
            }
            else if (command[0].ToLower().Equals("if"))
            {
                If_Statement if_st = new If_Statement();
                if (if_st.check_SingleLine_if(line) && if_st.get_condition_status())
                {
                    string[] split_if = line.Split(' ');
                    string cmnd = split_if[5] + ' ' + split_if[6];
                    if (!split_commands(cmnd))
                    {
                        throw new InvalidCommandException("Invalid drawing command");
                    }
                    else
                    {
                        split_commands(cmnd);
                    }
                }
                else if (if_st.check_MultiLine_if(get_Commands()) && if_st.get_condition_status())
                {
                    int indx = if_st.get_index();
                    while (indx < if_st.get_endif_index())
                    {
                        if (!split_commands(get_Commands()[indx].Trim()))
                        {
                            throw new InvalidCommandException("Invalid drawing command");
                        }
                        else
                        {
                            split_commands(get_Commands()[indx].Trim());
                        }
                        indx++;
                    }
                }
            }
            else if (command[0].ToLower().Equals("while"))
            {
                if (command.Length == 4)
                {
                    var list = new List<string>();
                    for (int i = SyntaxCheck.while_index; i <= SyntaxCheck.endloop_index; i++)
                    {
                        list.Add(commands[i].Trim());
                    }
                    var cmd = list.ToArray();
                    if (lp.loop_check(cmd) && lp.get_condition_status())
                    {
                        set_while_count(get_while_count() + 1);
                    }
                    else
                    {
                        set_while_count(0);
                    }
                }

            }
            else if (Regex.IsMatch(command[0], "[a-zA-Z()]"))
            {
                foreach(String comm in Form1.mth_cmd)
                {
                    split_commands(comm);
                }
            }
            
            
            else
            {
                //Throws exception if unknown command is executed
                throw new InvalidCommandException("Invalid Command");
            }
            //No exception is thrown if user entered command gets executed hence returning true if valid command is encounteder
            return true;
        }
        /// <summary>
        /// It is useful for executing while loop by using its indexes
        /// </summary>
        public void looping_commands()
        {
            int while_index = SyntaxCheck.while_index;
            int endloop_index = SyntaxCheck.endloop_index;
            while(while_index <= endloop_index)
            {
                split_commands(commands[while_index].Trim());
                while_index++;
                //if(while_index == endloop_index)
                //{
                //    set_while_count(0);
                //}
            }
            set_while_count(0);
        }
        /// <summary>
        /// Setter method to set user entered commands from the command window
        /// </summary>
        /// <param name="cmds"></param>
        public void set_Commands(string[] cmds)
        {
            commands = cmds;
        }
        /// <summary>
        /// Getter method to get the user entered commands
        /// </summary>
        /// <returns>a string type array containing user entered commands</returns>
        public string[] get_Commands()
        {
            return commands;
        }
        /// <summary>
        /// Sets count for while command
        /// </summary>
        /// <param name="count"></param>
        public void set_while_count(int count)
        {
            while_count = count;
        }
        /// <summary>
        /// gets count of while command
        /// </summary>
        /// <returns></returns>
        public int get_while_count()
        {
            return while_count;
        }
    }
}
