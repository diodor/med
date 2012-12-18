using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace editor
{
    abstract class Shapes
    {
        public abstract string DescriptionString { get; }

        public abstract void DrawWith(Graphics g, Pen p);
        public abstract void SaveTo(StreamWriter sw);
        public abstract bool IsNearTo(Point C);

        protected float getDistance(Point A, Point B)
        {
            return (float)Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }
    }
    class Cross : Shapes
    {
        private int X, Y; //TODO: переписать как один Point!

        public override string DescriptionString
        {
            get
            {
                return "Cross {X=" + X + ",Y=" + Y + "}";
            }
        }

        public Cross(int _X, int _Y)
        {
            this.X = _X;
            this.Y = _Y;
        }

        public Cross(StreamReader sr) // Загрузка
        {
            String line = sr.ReadLine();
            string[] foo = line.Split(' ');
            X = Convert.ToInt32(foo[0]);
            Y = Convert.ToInt32(foo[1]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, X - 5, Y - 5, X + 5, Y + 5);
            g.DrawLine(p, X + 5, Y - 5, X - 5, Y + 5);
        }

        public override void SaveTo(StreamWriter sw) //Сохранение
        {
            sw.WriteLine("Cross");
            sw.WriteLine(Convert.ToString(X) + " " + Convert.ToString(Y));
        }

        public override bool IsNearTo(Point C)
        {
            Point tmpCross = new Point();
            tmpCross.X = X;
            tmpCross.Y = Y;

            if (getDistance(tmpCross, C) <= 2) return true;
            else return false;
        }
    }
}
