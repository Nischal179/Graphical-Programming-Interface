using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// This class checks syntax of single line as well as multiline if statement and returns true if correct syntax is encountered else returns false
    /// </summary>
    public class If_Statement
    {
        private Var var = Var.Instance;
        private bool single_if_status = true, condition_status = false, multiline_if_status = true, if_found= false, endif_found = false;
        private int index = 0, end_index;
        /// <summary>
        /// It takes a single line of command as an arguement and checks validity of single line if statement
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool check_SingleLine_if(string line)
        {
            string[] split_if = line.Split(' ');
            if (split_if.Length == 7)
            {
                //getting the values of the variable in order to check the condition
                int val1, val2;
                if (var.Check_for_var(split_if[1]))
                {
                    val1 = var.get_variable_value(split_if[1]);
                    if (split_if[3].All(char.IsDigit))
                    {
                        val2 = Convert.ToInt32(split_if[3]);
                        if(check_condition(split_if[2], val1, val2))
                            single_if_status=true;
                    }
                    else if (var.Check_for_var(split_if[3]))
                    {
                        val2 = var.get_variable_value(split_if[3]);
                        if(check_condition(split_if[2], val1, val2))
                            single_if_status=true;
                    }
                    else
                    {
                        single_if_status = false;
                    }
                }
                else
                {
                    single_if_status = false;
                }
            }
            else
            {
                single_if_status = false;
            }
            return single_if_status;
        }
        /// <summary>
        /// It takes a array string as an arguement and checks validity of single line if statement
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public bool check_MultiLine_if(string[] txt)
        {
            int count = 0;
            int idx = 0;
            string line;
            while (idx<txt.Length)
            {
                count++;
                line = txt[idx];
                string[] split_txt = line.Split(' ');
                if (String.IsNullOrEmpty(line))
                {
                    if(IfFound && EndIfFound)
                    {
                        multiline_if_status = true;
                        break;
                    }
                    else {multiline_if_status = false;}
                    break;
                }
                else if (split_txt[0].ToLower().Equals("if") && split_txt.Length == 4)
                {
                    int val1, val2;
                    if (var.Check_for_var(split_txt[1]))
                    {
                        val1 = var.get_variable_value(split_txt[1]);
                        if (split_txt[3].Trim().All(char.IsDigit))
                        {
                            val2 = Convert.ToInt32(split_txt[3].Trim());
                            if (check_condition(split_txt[2], val1, val2))
                            {
                                multiline_if_status = true;
                            }
                        }
                        else if (var.Check_for_var(split_txt[3].Trim()))
                        {
                            val2 = var.get_variable_value(split_txt[3].Trim());
                            if (check_condition(split_txt[2], val1, val2))
                            {
                                multiline_if_status = true;
                            }
                        }
                        else
                        {
                            multiline_if_status = false;
                        }
                        if (multiline_if_status)
                        {
                            set_index(count);
                        }
                        IfFound = true;
                    }
                    else
                    {
                        multiline_if_status = false;
                    }
                }
                else if (split_txt[0].ToLower().Trim().Equals("endif") && IfFound == true)
                {
                    multiline_if_status = true;
                    EndIfFound = true;
                    set_endif_index(idx);
                }
                else if(IfFound && EndIfFound)
                {
                    multiline_if_status = true;
                }
                else
                {
                    multiline_if_status = false;
                }
                idx++;
            }
            return multiline_if_status;
        }
        /// <summary>
        /// This method is used to set the value of the private variable 'index'
        /// </summary>
        /// <param name="ind">The value to set the index to</param>
        public void set_index(int ind)
        {
            index = ind;
        }
        /// <summary>
        /// This method is used to get the value of the private variable 'index'
        /// </summary>
        /// <returns>The current value of the index</returns>
        public int get_index()
        {
            return index;
        }
        /// <summary>
        /// This method is used to set the value of the private variable 'end_index'
        /// </summary>
        /// <param name="ind">The value to set the end_index to</param>
        public void set_endif_index(int ind)
        {
            end_index = ind;
        }
        /// <summary>
        /// This method is used to get the value of the private variable 'end_index'
        /// </summary>
        /// <returns>The current value of the end_index</returns>
        public int get_endif_index()
        {
            return end_index;
        }
        /// <summary>
        /// Checks condition for the if statements
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
            else if(condition.Equals("<"))
            {
                if (value[0] < value[1])
                    condition_status = true;
            }
            else if(condition.Equals("=="))
            {
                if(value[0] == value[1])
                    condition_status = true;
            }
            else if(condition.Equals(">="))
            {
                if (value[0] >= value[1])
                    condition_status = true;
            }
            else if(condition.Equals("<="))
            {
                if (value[0] <= value[1])
                    condition_status = true;
            }
            else
                condition_status=false;
            return condition_status;
        }
        /// <summary>
        /// returns the condition check status for the if statements
        /// </summary>
        /// <returns></returns>
        public bool get_condition_status()
        {
            return condition_status;
        }
        /// <summary>
        /// Property to get or set the value of private boolean variable if_found.
        /// </summary>
        public bool IfFound
        {
            get { return if_found; }
            set { if_found = value; }
        }
        /// <summary>
        /// Property to get or set the value of private boolean variable endif_found.
        /// </summary>
        public bool EndIfFound
        {
            get { return endif_found; }
            set { endif_found = value; }
        }
    }
}
