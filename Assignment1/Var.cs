using Assignment1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// The Var class is a singleton class that creates and manages variables in the program. 
    /// It has methods for creating variables, getting the value of variables, and performing arithmetic operations on them. 
    /// It follows the thread-safe singleton design pattern to ensure that only one instance of the class exists.
    /// </summary>
    public sealed class Var
    {
        //Setting instance of Var class to null and making instance static so that it could be accessed by any part of the program
        private static Var instance = null;
        private static readonly object padlock = new object();
        private IDictionary<string, int> variable = new Dictionary<string, int>();
        private Var()
        {

        }
        /// <summary>
        /// It returns a single object of Variable class if there are no any instance of the class else it returns previously created instance following threadsafe singleton design pattern
        /// </summary>
        public static Var Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new Var();
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// Clears the value of the dictionary
        /// </summary>
        public void Clear_dict()
        {
            variable.Clear();
        }
        /// <summary>
        /// Takes a single line input and checks for variabe declaration and throws exception if any invalid syntax is encountered
        /// </summary>
        /// <param name="line"></param>
        /// <exception cref="InvalidCommandException"></exception>
        public void Declare_var(string line)
        {
            string[] command = line.Split(' ');
            if (command.Length == 3)
            {
                try
                {
                    if (!Regex.IsMatch(command[0], @"^[a-zA-Z]+$"))
                    {
                        //When name of a variable is other than english letters then it throws an exception
                        throw new InvalidCommandException("Variable name shold only Contain alphabets");
                    }
                    else if (command.Contains("="))
                    {
                        //Checking if 3rd command contains any operator
                        string[] split_syntax = command[2].Split('+', '-', '/', '*');
                        if (split_syntax.Length == 2)
                        {//Yaha xai abha arko existing variable xa ki value xa check garne ani tei aanusar necessary action perform hanne
            
                            //Checking if the splitted is a number or a string
                            if(split_syntax[0].All(char.IsDigit) && split_syntax[1].All(char.IsDigit))
                            {
                                int var_val1 = Convert.ToInt32(split_syntax[0]);
                                int var_val2 = Convert.ToInt32(split_syntax[1]);
                                int result = Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                Add_Update_var_val(result, command[0]);
                            }
                            //Check if the new variable is assigned a value by performing arithmetic opertaion between an Integer value and previously defined variable.
                            else if (split_syntax[0].All(char.IsDigit) && Regex.IsMatch(split_syntax[1], @"^[a-zA-Z]+$"))
                            {
                                int var_val1 = Convert.ToInt32(split_syntax[0]);
                                int var_val2;
                                if (Check_for_var(split_syntax[1]))
                                {
                                    var_val2 = get_variable_value(split_syntax[1]);
                                    int result = Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                    Add_Update_var_val(result, command[0]);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Data type");
                                }
                            }
                            else if (split_syntax[1].All(char.IsDigit) && Regex.IsMatch(split_syntax[0], @"^[a-zA-Z]+$"))
                            {
                                int var_val1;
                                int var_val2 = Convert.ToInt32(split_syntax[1]);
                                if (Check_for_var(split_syntax[0]))
                                {
                                    var_val1 = get_variable_value(split_syntax[0]);
                                    int result = Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                    Add_Update_var_val(result, command[0]);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Data type");
                                }
                            }
                            else if(Regex.IsMatch(split_syntax[0], @"^[a-zA-Z]+$") && Regex.IsMatch(split_syntax[1], @"^[a-zA-Z]+$"))
                            {
                                int var_val1;
                                int var_val2;
                                if (Check_for_var(split_syntax[0]) && Check_for_var(split_syntax[1]))
                                {
                                    var_val1 = get_variable_value(split_syntax[0]);
                                    var_val2 = get_variable_value(split_syntax[1]);
                                    int result = Perform_Arithmetic_Operation(line, var_val1, var_val2);
                                    Add_Update_var_val(result, command[0]);
                                }
                                else
                                {
                                    throw new FormatException("Invalid Data type");
                                }
                            }
                        }
                        else if (split_syntax.Length == 1)
                        {
                            if(command[2].All(char.IsDigit))
                            {
                                int var_val = Convert.ToInt32(command[2]);
                                Add_Update_var_val(var_val, command[0]);
                            }
                            else if (Check_for_var(command[2]))
                            {
                                int var_val = get_variable_value(command[2]);
                                Add_Update_var_val(var_val, command[0]);
                            }
                            else
                            {
                                throw new FormatException("Invalid Data Type");
                            }
                        }
                        else
                        {
                            throw new InvalidCommandException("Invaid Command syntax!!");
                        }
                    }
                    else
                    {
                        throw new InvalidCommandException("Syntax to declare a variable is incorrect");
                    }
                }
                catch (FormatException)
                {
                    //A Format Exception is thrown when command consists non-integer datatype 
                    throw new InvalidCommandException("Variable should only be assigned an Integer value");
                }
            }
        }
        /// <summary>
        /// Performs arithmetic operation basaed upon the passed command and value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns>integer type value after performing the arithmetic operation</returns>
        /// <exception cref="InvalidCommandException"></exception>
        public int Perform_Arithmetic_Operation(string command, int val1, int val2)
        {
            string[] split_command = command.Split(' ');
            if (command.Contains("+"))
            {
                return (val1 + val2);
            }
            else if (command.Contains("-"))
            {
                return ((int)val1 - val2);
            }
            else if (command.Contains("/"))
            {
                if (val2 == 0)
                {
                    throw new InvalidCommandException("Divisor cannot be Zero");
                }
                else
                {
                    return ((int)val1 / val2);
                }
            }
            else if (command.Contains("*"))
            {
                return (val1 * val2);
            }
            else
            {
                throw new InvalidCommandException("Invalid arithmetic operator Please use any one of the followig arithematic operators \"+,-,/,*\"");
            }
        }
        /// <summary>
        /// Checks if the passed string is variable and if so it returns it value else throws an exception 
        /// </summary>
        /// <param name="variable_name"></param>
        /// <returns>value of the variable from the dictionary if the passed variable is present in the dictionary</returns>
        /// <exception cref="InvalidCommandException"></exception>
        public int get_variable_value(string variable_name)
        {
            if (variable.ContainsKey(variable_name.ToLower()))
            {
                return variable[variable_name.ToLower()];
            }
            else
            {
                throw new InvalidCommandException("Variable " + variable_name + " is not declared");
            }
        }
        /// <summary>
        /// It checks for the presence of variable within the dictionary
        /// </summary>
        /// <param name="variable_name"></param>
        /// <returns></returns>
        public bool Check_for_var(string variable_name)
        {
            if (variable.ContainsKey(variable_name.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// It either adds new key and value pair to the dictionary if there is no previous key present else it updtaes the existing key's value
        /// </summary>
        /// <param name="var_val"></param>
        /// <param name="command"></param>
        public void Add_Update_var_val(int var_val, string command)
        {
            if (Check_for_var(command))
            {
                variable[command.ToLower()] = var_val;
            }
            else
            {
                variable.Add(command.ToLower(), var_val);
            }
        }
    }
}