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
    /// Rectangle class inherits shape which is an abstract class
    /// </summary>
    class Rectangle : Shape
    {
        int width, height;
        string flashcolor;
        /// <summary>
        /// Default constructor of the Rectangle class calls the default constructor of it's base class i.e.
        /// Shape
        /// </summary>
        public Rectangle() : base()
        {
        }
        /// <summary>
        /// It sets pen color fill status and cursor position as well as height, width and flashcolor of the rectangle
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="flashcolor"></param>
        /// <param name="fill"></param>
        /// <param name="flash"></param>
        /// <param name="list"></param>
        public override void set(Color colour,string flashcolor, bool fill, bool flash, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(colour, flashcolor, fill, flash, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];
            this.flashcolor = flashcolor;
        }
        /// <summary>
        /// It draws rectangle basedd on its fill status cursor mposition as well as height and width.
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(colour, 2);
            SolidBrush b = new SolidBrush(colour);
            //If the fill value is true then a filled rectangle is drawn else outline of a rectangle of given height and width is drawn 
            if(fill)
            {
                if(flash)
                {
                    flashing(g);
                }
                else
                {
                    g.FillRectangle(b, x, y, width, height);
                }
            }
            else
            {
                if (flash)
                {
                    flashing(g);
                }
                else
                {
                    g.DrawRectangle(p, x, y, width, height);
                }
            }
            //Disposing the objects of pen and solidbrush after they are being used
            p.Dispose();
            b.Dispose();
        }
        public void flashing(Graphics g)
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
                        g.FillRectangle(b, x, y, width, height);
                        flag = true;
                    }
                    else if(flag == false && fill == false)
                    {
                        Pen p = new Pen(first, 2);
                        g.DrawRectangle(p, x, y, width, height);
                        flag = true;
                    }
                    else if(flag == true && fill == false)
                    {
                        Pen p = new Pen(second, 2);
                        g.DrawRectangle(p, x, y, width, height);
                        flag = false;
                    }
                    else
                    {
                        SolidBrush b = new SolidBrush(second);
                        g.FillRectangle(b, x, y, width, height);
                        flag = false;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
