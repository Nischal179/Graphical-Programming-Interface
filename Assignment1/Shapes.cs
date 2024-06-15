using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// Shapes interface has decelaration of set method that receives the color, fill status and integer type array as parameters
    /// </summary>
    interface Shapes
    {
        //Method declaration
        void set(Color c,string flashcolor, bool fill, bool flash, params int[] list);
        void draw(Graphics g);
        

    }

}
