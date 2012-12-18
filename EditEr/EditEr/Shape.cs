using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace EditEr
{
    public abstract class Shapes
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

    public class Cross : Shapes
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
            p = new Pen(Color.Red, 1);
            g.DrawLine(p, X - 3, Y - 3, X + 3, Y + 3);
            g.DrawLine(p, X + 3, Y - 3, X - 3, Y + 3);
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

    class Line : Shapes
    {
        private Point S, F;

        public override string DescriptionString
        {
            get
            {
                return "Line " + S + "; " + F;
            }
        }

        public Line(Point _S, Point _F)
        {
            this.S = _S;
            this.F = _F;
        }

        public Line(StreamReader sr) // Загрузка
        {
            string line = sr.ReadLine();
            string[] foo = line.Split(' ');
            S.X = Convert.ToInt32(foo[0]);
            S.Y = Convert.ToInt32(foo[1]);

            line = sr.ReadLine();
            foo = line.Split(' ');
            F.X = Convert.ToInt32(foo[0]);
            F.Y = Convert.ToInt32(foo[1]);
        }

        public override void DrawWith(Graphics g, Pen p)
        {
            p = new Pen(Color.Red, 1);
            g.DrawLine(p, S.X, S.Y, F.X, F.Y);
        }

        public override void SaveTo(StreamWriter sw) //Сохранение
        {
            sw.WriteLine("Line");
            sw.WriteLine(Convert.ToString(S.X) + " " + Convert.ToString(S.Y));
            sw.WriteLine(Convert.ToString(F.X) + " " + Convert.ToString(F.Y));
        }

        public override bool IsNearTo(Point C)
        {
            float AC = getDistance(S, C);
            float BC = getDistance(F, C);
            float AB = getDistance(S, F);

            if (((AC + BC) - AB) <= 1) return true;
            else return false;
        }
    }

}