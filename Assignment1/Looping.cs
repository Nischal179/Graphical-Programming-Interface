using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// This class is used for handling looping functionality in the program.
    /// It contains private variables to keep track of the loop syntax, whether a while loop or end loop has been found,
    /// the status of the looping condition, an instance of the Var class for evaluating expressions, the index and end_index of the loop,
    /// and value1 and value2 for storing values needed for evaluating the loop condition.
    /// </summary>
    public class Looping
    {
        private bool loop_syntax = true, whileFound = false, endLoopFound = false, condition_status = false;
        private Var var = Var.Instance;
        private int index = 0, end_index = 0;
        private int value1, value2;
        private string oper;
        /// <summary>
        /// This method is used for checking the syntax of the while loop and the endloop command in the given list of commands.
        /// It checks for the presence of a valid while loop and endloop command, the presence and values of variables used in the loop condition,
        /// and sets the index and end_index of the loop accordingly. It returns a boolean value indicating the status of the loop syntax.
        /// </summary>
        /// <param name="cmds">The list of commands to be checked for loop syntax</param>
        /// <returns>Boolean value indicating the status of the loop syntax</returns>
        public bool loop_check(string[] cmds)
        {
            int ind = 0;
            string line;
            while(ind < cmds.Length)
            {
                line = cmds[ind].Trim();
                string[] split_commands = line.Split(' ');
                if (String.IsNullOrEmpty(line))
                {
                    if (whileFound && endLoopFound)
                    {
                        loop_syntax = true;
                        break;
                    }
                    else { loop_syntax = false; }
                    break;
                }
                else if (split_commands[0].ToLower().Equals("while") && split_commands.Length == 4)
                {
                    if (var.Check_for_var(split_commands[1].ToLower()))
                    {
                        value1 = var.get_variable_value(split_commands[1].ToLower());
                        if (split_commands[3].Trim().All(char.IsDigit))
                        {
                            value2 = Convert.ToInt32(split_commands[3].Trim());
                            oper = split_commands[2];
                            check_condition(split_commands[2], value1, value2);
                            loop_syntax = true;
                        }
                        else if (var.Check_for_var(split_commands[3].Trim().ToLower()))
                        {
                            value2 = var.get_variable_value(split_commands[3].Trim().ToLower());
                            oper = split_commands[2];
                            check_condition(split_commands[2], value1, value2);
                            loop_syntax = true;
                        }
                        else
                        {
                            loop_syntax = false;
                        }
                        if(loop_syntax)
                        {
                            set_index(ind);
                        }
                        whileFound = true;
                    }
                    else
                    {
                        loop_syntax = false;
                    }
                }
                else if (split_commands[0].Trim().ToLower().Equals("endloop") && whileFound == true)
                {
                    endLoopFound = true;
                    loop_syntax = true;
                    set_endloop_index(ind);
                    break;
                }
                else
                {
                    loop_syntax=false;
                }
                ind++;
            }
            return loop_syntax;
        }
        /// <summary>
        /// This method is used to check the condition of a while loop. The method takes in a string 'condition' that represents the comparison operator, and two integer values. The method checks if the condition is met by comparing the two values using the given operator. If the condition is met, the method sets the 'condition_status' variable to true, otherwise it sets it to false. It then returns the value of 'condition_status'.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool check_condition(string condition, params int[] value)
        {

            if (condition.Equals(">"))
            {
                if (value[0] > value[1])
                    condition_status = true;
            }
            else if (condition.Equals("<"))
            {
                if (value[0] < value[1])
                    condition_status = true;
            }
            else if (condition.Equals("=="))
            {
                if (value[0] == value[1])
                    condition_status = true;
            }
            else if (condition.Equals(">="))
            {
                if (value[0] >= value[1])
                    condition_status = true;
            }
            else if (condition.Equals("<="))
            {
                if (value[0] <= value[1])
                    condition_status = true;
            }
            else
                condition_status = false;
            return condition_status;
        }
        public void set_index(int ind)
        {
            index = ind;
        }
        public int get_index()
        {
            return index;
        }
        public void set_endloop_index(int ind)
        {
            end_index = ind;
        }
        public int get_endloop_index()
        {
            return end_index;
        }
        public bool get_condition_status()
        {
            return condition_status;
        }
    }
}
