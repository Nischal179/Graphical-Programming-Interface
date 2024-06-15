using Assignment1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment1
{
    /// <summary>
    /// Circle class inherits Shape which is an abstract class
    /// </summary>
    class Circle : Shape
    {
        int radius;
        string flashcolor;
        /// <summary>
        /// Default constructor of the Circle class calls the default constructor of it's base class i.e.
        /// Shape
        /// </summary>
        public Circle() : base()
        {

        }
        /// <summary>
        /// It sets pen color fill status and cursor position as well as radius of the circle 
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="flashcolor"></param>
        /// <param name="fill"></param>
        /// <param name="flash"></param>
        /// <param name="list"></param>
        public override void set(Color colour,string flashcolor, bool fill, bool flash, params int[] list)
        {
            base.set(colour,flashcolor, fill, flash, list[0], list[1]);
            this.radius = list[2];
            this.flashcolor = flashcolor;
        }
        /// <summary>
        /// It draws the circle based on it's fill status, radius, pen color and cursor's position
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(colour, 2);
            SolidBrush b = new SolidBrush(colour);
            //If the fill value is true then a filled circle is drawn else outline of a circle of given radius is drawn 
            if (fill)
            {
                if (flash)
                {
                    Flashing(g);
                }
                else
                {
                    g.FillEllipse(b, x, y, radius, radius);
                }
            }
            else
            {
                if (flash)
                {
                    Flashing(g);
                }
                else
                {
                    g.DrawEllipse(p, x, y, radius, radius);
                }
            }
            //Disposing the objects of pen and solidbrush after they are being used
            p.Dispose();
            b.Dispose();
        }
        /// <summary>
        /// This method is used to create a new thread for flashing. It takes an object of the Graphics class as input.
        /// It creates a new thread and starts it based on the value of 'flashcolor' variable.
        /// The thread runs the 'StartFlashing' method and passes the Graphics object and two colors as input.
        /// If the 'flashcolor' variable is "redgreen", the thread runs the 'StartFlashing' method with Color.Red and Color.Green as input.
        /// If the 'flashcolor' variable is "blueyellow", the thread runs the 'StartFlashing' method with Color.Blue and Color.Yellow as input.
        /// If the 'flashcolor' variable is "blackwhite", the thread runs the 'StartFlashing' method with Color.Black and Color.White as input.
        /// If the value of 'flashcolor' variable is anything else, it throws an InvalidCommandException with the message "Invalid value for flashing!".
        /// </summary>
        public void Flashing(Graphics g)
        {
            Thread newThread;
            switch (flashcolor)
            {
                case "redgreen":
                    newThread = new Thread(() => StartFlashing(g, Color.Red, Color.Green));
                    newThread.IsBackground = true;
                    newThread.Start();
                    break;
                case "blueyellow":
                    newThread = new Thread(() => StartFlashing(g, Color.Blue, Color.Yellow));
                    newThread.IsBackground = true;
                    newThread.Start();
                    break;
                case "blackwhite":
                    newThread = new Thread(() => StartFlashing(g, Color.Black, Color.White));
                    newThread.IsBackground = true;
                    newThread.Start();
                    break;
                default:
                    throw new InvalidCommandException("Invalid value for flashing!");
            }
        }
        private void StartFlashing(Graphics g, Color first, Color second)
        {
            bool flag = false;
            while (true)
            {
                lock (g)
                {
                    if (flag == false && fill == true)
                    {
                        SolidBrush b = new SolidBrush(first);
                        g.FillEllipse(b, x, y, radius, radius);
                        flag = true;
                    }
                    else if (flag == false && fill == false)
                    {
                        Pen p = new Pen(first, 2);
                        g.DrawEllipse(p, x, y, radius, radius);
                        flag = true;
                    }
                    else if (flag == true && fill == false)
                    {
                        Pen p = new Pen(second, 2);
                        g.DrawEllipse(p, x, y, radius, radius);
                        flag = false;
                    }
                    else
                    {
                        SolidBrush b = new SolidBrush(second);
                        g.FillEllipse(b, x, y, radius, radius);
                        flag = false;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
