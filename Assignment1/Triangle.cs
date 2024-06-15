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
    /// Triangle class inherits abstract class named Shape
    /// </summary>
    class Triangle : Shape
    {
        //Created the required variables
        int first_pt, second_pt;
        string flashcolor;
        public Triangle() : base()
        {

        }
        /// <summary>
        /// It sets pen color fill status and cursor position as well as height and base of the triangle
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="flashcolor"></param>
        /// <param name="fill"></param>
        /// <param name="flash"></param>
        /// <param name="list"></param>
        public override void set(Color colour,string flashcolor, bool fill,bool flash, params int[] list)
        {
            base.set(colour,flashcolor, fill, flash, list[0], list[1]);
            this.first_pt = list[2];
            this.second_pt = list[3];
            this.flashcolor = flashcolor;
        }
        /// <summary>
        /// It draws triangle based on its fill status cursor position as well as its base and height
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(colour, 2);
            SolidBrush b = new SolidBrush(colour);
            //Making 3 co-ordinates according to the base and height given by the user
            Point p1 = new Point(x+x, y+y);
            Point p2 = new Point(x + x,second_pt);
            Point p3 = new Point(first_pt, second_pt);
            Point[] pointArray = { p1, p2, p3 };
            //If the fill value is true then a filled triangle is drawn else outline of a triangle of given base and height is drawn 
            if (fill)
            {
                if (flash)
                {
                    flashing(g, pointArray);
                }
                else
                {
                    g.FillPolygon(b, pointArray);
                }
            }
            else
            {
                if (flash)
                {
                    flashing(g, pointArray);
                }
                else
                {
                    g.DrawPolygon(p, pointArray);
                }
            }
            //Disposing pen and solid brush after they are being used
            p.Dispose();
            b.Dispose();
        }
        public void flashing(Graphics g, Point[] pointArray)
        {
            Thread newThread;
            switch (flashcolor)
            {
                case "redgreen":
                    newThread = new Thread(() => StartFlashing(g, Color.Red, Color.Green, pointArray));
                    newThread.IsBackground = true;
                    newThread.Start();
                    break;
                case "blueyellow":
                    newThread = new Thread(() => StartFlashing(g, Color.Blue, Color.Yellow, pointArray));
                    newThread.IsBackground = true;
                    newThread.Start();
                    break;
                case "blackwhite":
                    newThread = new Thread(() => StartFlashing(g, Color.Black, Color.White, pointArray));
                    newThread.IsBackground = true;
                    newThread.Start();
                    break;
                default:
                    throw new InvalidCommandException("Invalid value for flashing!");
            }
        }
        private void StartFlashing(Graphics g, Color first, Color second, Point[] pointArray)
        {
            bool flag = false;
            while (true)
            {
                lock (g)
                {
                    if (flag == false && fill == true)
                    {
                        SolidBrush b = new SolidBrush(first);
                        g.FillPolygon(b, pointArray);
                        flag = true;
                    }
                    else if (flag == false && fill == false)
                    {
                        Pen p = new Pen(first, 2);
                        g.DrawPolygon(p, pointArray);
                        flag = true;
                    }
                    else if (flag == true && fill == false)
                    {
                        Pen p = new Pen(second, 2);
                        g.DrawPolygon(p, pointArray);
                        flag = false;
                    }
                    else
                    {
                        SolidBrush b = new SolidBrush(second);
                        g.FillPolygon(b, pointArray);
                        flag = false;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
