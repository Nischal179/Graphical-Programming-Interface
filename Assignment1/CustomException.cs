using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class CustomException
    {
    }

    /// <summary>
    /// invalid command exception inherits Exception class
    /// </summary>
    public class InvalidCommandException : Exception
    {
        /// <summary>
        /// invalid command exception 
        /// </summary>
        /// <param name="message">When this custom exception is raised a string type arguement describing 
        /// the appropriate message is passed which then calls the base class default constructor passing the 
        /// same string type arguement</param>
        public InvalidCommandException(string message) : base(message)
        {

        }
    }
}
