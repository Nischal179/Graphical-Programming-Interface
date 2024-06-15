using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// The class 'Methods' has several private variables such as a boolean noPara_check, a boolean method_found, a boolean endMethod_found, a static int method_index, a static int endmethod_index, a static Dictionary method and a boolean method_call. It also has a private list cmds
    /// These variables are used to check for the method calls and handling them.
    /// The method_index and endmethod_index are used to keep track of the start and end of the method.
    /// The method_found and endMethod_found are used to check if a method and endMethod is found in the code.
    /// The noPara_check is used to check if the method has no parameter.
    /// The method_call is used to check if a method is called.
    /// The method dictionary stores the methods with their name and commands.
    /// The cmds list stores the commands of the method.
    /// </summary>
    public class Methods
    {
        private bool noPara_check = false, method_found = false, endMethod_found = false;
        public static int method_index = 0, endmethod_index = 0;
        public static Dictionary<string, string[]> method= new Dictionary<string, string[]>();
        public static bool method_call = false;
        private List<string> cmds = new List<string>();
        /// <summary>
        /// This method checks if the method has no parameter.
        /// It takes an array of strings 'commands' as input. It uses a while loop to iterate through the 'commands' array.
        /// Inside the while loop it uses the 'Split' method to split the commands on the basis of space.
        /// It checks if the first element after splitting is "method" and if the length of split_cmd is 2 and if the second element contains "()".
        /// If these conditions are true, it sets the 'noPara_check' variable to true and the 'method_found' variable to true and sets the 'method_index' variable to the current index of the loop.
        /// If these conditions are not true, it sets the 'noPara_check' variable to false and clears the 'cmds' list.
        /// It also checks if the first element after splitting is "endmethod" and if the 'method_found' is true. If these conditions are true, it sets the 'endMethod_found' variable to true and the 'endmethod_index' variable to the current index of the loop.
        /// If the 'method_found' and 'endMethod_found' are true, it breaks the loop.
        /// If these conditions are not true, it sets the 'noPara_check' variable to false and clears the 'cmds' list.
        /// Finally, if the 'method_found' or 'endMethod_found' is not true, it sets the 'noPara_check' variable to false and clears the 'cmds' list.
        /// It returns the 'noPara_check' variable
        /// </summary>
        /// <param name="commands">Array of commands</param>
        /// <returns>Return 'noPara_check' variable</returns>
        public bool noPara_method(string[] commands)
        {
            int i = 0;
            string[] split_cmd;
            while(i<commands.Length)
            {
                split_cmd = commands[i].Split(' ');
                if (split_cmd[0].Trim().ToLower().Equals("method"))
                {
                    if(split_cmd.Length == 2)
                    {
                        if (split_cmd[1].Contains("()"))
                        {
                            if(Regex.IsMatch(split_cmd[1], "[a-zA-Z()]"))
                            {
                                noPara_check = true;
                                method_found = true;
                                method_index = i;
                            }
                        }
                        else
                        {
                            noPara_check=false;
                            cmds.Clear();
                        }
                    }
                    else
                    {
                        noPara_check= false;
                        cmds.Clear();
                    }
                }
                else if(split_cmd[0].Trim().ToLower().Equals("endmethod") && method_found==true)
                {
                    noPara_check = true;
                    endMethod_found = true;
                    endmethod_index = i;
                }
                else if(method_found && endMethod_found)
                {
                    noPara_check = true;
                    break;
                }
                else
                {
                    noPara_check = false;
                    cmds.Clear();
                }
                i++;
            }
            if (!method_found || !endMethod_found)
            {
                noPara_check = false;
                cmds.Clear();
            }
            return noPara_check;
        }
        /// <summary>
        /// This method is used to get the commands stored in the private list 'cmds' as an array of strings.
        /// </summary>
        /// <returns>
        /// Returns an array of strings containing the commands stored in 'cmds'
        /// </returns>
        public string[] get_cmds()
        {
            return cmds.ToArray();
        }
        /// <summary>
        /// This property is used to get and set the value of the private variable 'method_index'
        /// </summary>
        public int Method_index
        {
            get { return method_index; }
            set { method_index = value; }
        }
        /// <summary>
        /// This property is used to get and set the value of the private variable 'method_index'
        /// </summary>
        public int Endmethod_index
        {
            set { endmethod_index = value; }
            get { return endmethod_index; }
        }
    }
}
