using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// Abstract class named Shape inherits the Shapes interface
    /// </summary>
    abstract class Shape : Shapes  //internal Class Recangle

    {
        public Color colour; //shape's colour
        public int x, y;
        public bool fill = false;
        public bool flash = false;
        /// <summary>
        /// Default constructor of Shape class initializes the colour as Black and sets the x,y co-ordinates for the cursor as (0,0)
        /// </summary>
        public Shape()
        {
            colour = Color.Black;
            x = y = 0;
        }
        //the three methods below are from the Shapes interface
        //here we are passing on the obligation to implement them to the derived classes by declaring them as abstract
        public abstract void draw(Graphics g);

        //set is declared as virtual so it can be overridden by a more specific child version
        //but is here so it can be called by that child version to do the generic stuff
        //note the use of the param keyword to provide a variable parameter list to cope with some shapes having more setup information than others (in Java it is called varargs and uses the … notation
        /// <summary>
        /// It sets the color, fill status and cursor position for the shape that is to be drawn
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="flashcolor"></param>
        /// <param name="fill"></param>
        /// <param name="flash"></param>
        /// <param name="list"></param>
        public virtual void set(Color colour,string flashcolor, bool fill, bool flash, params int[] list)
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1];
            this.fill = fill;
            this.flash = flash;
        }


        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }

    }

}
