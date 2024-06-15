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
    /// DrawTo class inherits an abstract class named Shape
    /// </summary>
    class DrawTo : Shape
    {
        int start_pt, end_pt;
        string flashcolor;
        /// <summary>
        /// Default constructor of the DrawTo class calls the default constructor of it's base class i.e.
        /// Shape
        /// </summary>
        public DrawTo() : base()
        {

        }
        /// <summary>
        /// It sets the cursor position, color of the pen and co-ordinates for the line to be drawn
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="flashcolor"></param>
        /// <param name="fill"></param>
        /// <param name="flash"></param>
        /// <param name="list"></param>
        public override void set(Color colour, string flashcolor, bool fill, bool flash, params int[] list)
        {
            base.set(colour, flashcolor, fill, flash, list[0], list[1]);
            this.start_pt = list[2];
            this.end_pt = list[3];
            this.flashcolor = flashcolor;
        }
        /// <summary>
        /// This method is used to draw a line on the Graphics object passed as input.
        /// It creates a Pen object with the specified 'colour' and width of 2.
        /// If the 'flash' variable is true, it creates a new thread and starts it based on the value of 'flashcolor' variable.
        /// The thread runs the 'StartFlashing' method and passes the Graphics object and two colors as input.
        /// If the 'flashcolor' variable is "redgreen", the thread runs the 'StartFlashing' method with Color.Red and Color.Green as input.
        /// If the 'flashcolor' variable is "blueyellow", the thread runs the 'StartFlashing' method with Color.Blue and Color.Yellow as input.
        /// If the 'flashcolor' variable is "blackwhite", the thread runs the 'StartFlashing' method with Color.Black and Color.White as input.
        /// If the value of 'flashcolor' variable is anything else, it throws an InvalidCommandException with the message "Invalid value for flashing!".
        /// If 'flash' variable is false, it uses the Pen object to draw a line on the Graphics object with the coordinates specified by 'x', 'y', 'start_pt', and 'end_pt' variables.
        /// After the line is drawn, it disposes of the Pen object.
        /// </summary>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(colour, 2);
            if (flash)
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
            else
            {
                g.DrawLine(p, x, y, start_pt, end_pt);
            }
            p.Dispose(); //Disposing Object of Pen class after it has been used to draw a line
        }
        /// <summary>
        /// This method is used to flash a line on the Graphics object passed as input.
        /// It takes two Color objects as input, which represent the two colors the line should alternate between.
        /// It uses a while loop to continuously draw the line with one of the two colors and sleep for 500 milliseconds,
        /// creating the flashing effect. A lock statement is used to ensure that the graphics object is only accessed by one thread at a time.
        /// A flag variable is used to alternate between the two colors.
        /// The method continues to run until the program is closed.
        /// </summary>
        /// <param name="g">Graphics object to draw on</param>
        /// <param name="first">first color of the line</param>
        /// <param name="second">second color of the line</param>
        private void StartFlashing(Graphics g, Color first, Color second)
        {
            bool flag = false;
            while (true)
            {
                lock (g)
                {
                    if (flag == false)
                    {
                        Pen pen = new Pen(first,2);
                        g.DrawLine(pen, x, y, start_pt, end_pt);
                        flag = true;
                    }
                    else
                    {
                        Pen pen = new Pen(second,2);
                        g.DrawLine(pen, x, y, start_pt, end_pt);
                        flag = false;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
